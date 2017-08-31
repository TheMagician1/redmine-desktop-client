using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Redmine.Net.Api.Types;
using Redmine.Net.Api;
using Redmine.Net.Api.Async;
using System.Threading.Tasks;

namespace Redmine.Client
{
    public class ClientProject : Project
    {
        public ClientProject(Project p) {
            this.Id = p.Id;
            this.Name = p.Name;
            this.Identifier = p.Identifier;
            this.Description = p.Description;
            this.Parent = p.Parent;
            this.HomePage = p.HomePage;
            this.CreatedOn = p.CreatedOn;
            this.UpdatedOn = p.UpdatedOn;
            this.Trackers = p.Trackers;
            this.CustomFields = p.CustomFields;
        }

        public string DisplayName {
            get {
                if (Parent != null)
                    return Parent.Name + " - " + Name;
                return Name;
            }
        }
    }

    public enum ApiVersion
    {
        V10x,
        V11x,
        V12x,
        V13x,
        V14x,
        V20x,
        V21x,
        V22x,
        V23x,
        V24x,
        V25x,
    }

    public class Filter : ICloneable
    {
        public int TrackerId = 0;
        public int StatusId = 0;
        public int PriorityId = 0;
        public string Subject = "";
        public int AssignedToId = 0;
        public int VersionId = 0;
        public int CategoryId = 0;

        #region ICloneable Members

        public object Clone()
        {
            return new Filter { TrackerId = TrackerId, StatusId = StatusId, PriorityId = PriorityId, Subject = Subject, AssignedToId = AssignedToId, VersionId = VersionId, CategoryId = CategoryId };
        }

        #endregion ICloneable Members
    }

    internal class LoadException : Exception
    {
        public LoadException(String action, Exception innerException) : base(action, innerException)
        {
        }
    }

    internal class MainFormData
    {
        private List<ProjectTracker> trackers1;

        public List<ClientProject> Projects { get; private set; }
        public IList<Issue> Issues { get; set; }
        public IList<CustomField> CustomFields { get; private set; }

        // search data
        public List<ProjectTracker> Trackers { get; private set; }

        public List<IssueCategory> Categories { get; private set; }
        public List<IssueStatus> Statuses { get; private set; }
        public List<Redmine.Net.Api.Types.Version> Versions { get; private set; }
        public List<ProjectMember> ProjectMembers { get; private set; }
        public List<Enumerations.EnumerationItem> IssuePriorities { get; private set; }
        public List<Enumerations.EnumerationItem> Activities { get; private set; }
        public int ProjectId { get; }

        public static async Task<MainFormData> Init(IList<Project> projects, int projectId, bool onlyMe, Filter filter)
        {
            Func<NameValueCollection> InitParameters = () =>
            {
                NameValueCollection parameters = new NameValueCollection();
                if (projectId != -1)
                    parameters.Add(RedmineKeys.PROJECT_ID, projectId.ToString());
                return parameters;
            };

            List<Tracker> allTrackers = null;
            List<IssueCategory> categories = null;
            List<Net.Api.Types.Version> versions = null;
            List<ProjectTracker> trackers = null;
            List<IssueStatus> statuses = null;
            List<ProjectMember> projectMembers = null;
            List<Enumerations.EnumerationItem> issuePriorities = null;
            List<Enumerations.EnumerationItem> activities = null;
            List<CustomField> customFields = null;
            if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
            {
                try
                {
                    allTrackers = await RedmineClientForm.redmine.GetObjectsAsync<Tracker>(InitParameters());
                }
                catch (Exception e)
                {
                    throw new LoadException(Languages.Lang.BgWork_LoadTrackers, e);
                }
                if (projectId >= 0)
                {
                    try
                    {
                        NameValueCollection projectParameters = new NameValueCollection { { "include", "trackers" } };
                        Project project = await RedmineClientForm.redmine.GetObjectAsync<Project>(projectId.ToString(), projectParameters);
                        trackers = new List<ProjectTracker>(project.Trackers);
                    }
                    catch (Exception e)
                    {
                        throw new LoadException(Languages.Lang.BgWork_LoadProjectTrackers, e);
                    }

                    try
                    {
                        categories = new List<IssueCategory>(await RedmineClientForm.redmine.GetObjectsAsync<IssueCategory>(InitParameters()));
                        categories.Insert(0, new IssueCategory { Id = 0, Name = "" });
                    }
                    catch (Exception e)
                    {
                        throw new LoadException(Languages.Lang.BgWork_LoadCategories, e);
                    }

                    try
                    {
                        versions = await RedmineClientForm.redmine.GetObjectsAsync<Redmine.Net.Api.Types.Version>(InitParameters());
                        versions.Insert(0, new Redmine.Net.Api.Types.Version { Id = 0, Name = "" });
                    }
                    catch (Exception e)
                    {
                        throw new LoadException(Languages.Lang.BgWork_LoadVersions, e);
                    }
                }
                trackers.Insert(0, new ProjectTracker { Id = 0, Name = "" });

                try
                {
                    statuses = await RedmineClientForm.redmine.GetObjectsAsync<IssueStatus>(InitParameters());
                    statuses.Insert(0, new IssueStatus { Id = 0, Name = Languages.Lang.AllOpenIssues });
                    statuses.Add(new IssueStatus { Id = -1, Name = Languages.Lang.AllClosedIssues });
                    statuses.Add(new IssueStatus { Id = -2, Name = Languages.Lang.AllOpenAndClosedIssues });
                }
                catch (Exception e)
                {
                    throw new LoadException(Languages.Lang.BgWork_LoadStatuses, e);
                }

                try
                {
                    if (RedmineClientForm.RedmineVersion >= ApiVersion.V14x
                        && projectId > 0)
                    {
                        List<ProjectMembership> projectMembership = await RedmineClientForm.redmine.GetObjectsAsync<ProjectMembership>(InitParameters());
                        projectMembers = projectMembership.ConvertAll(new Converter<ProjectMembership, ProjectMember>(ProjectMember.MembershipToMember));
                    }
                    else
                    {
                        List<User> allUsers = await RedmineClientForm.redmine.GetObjectsAsync<User>(InitParameters());
                        projectMembers = allUsers.ConvertAll(new Converter<User, ProjectMember>(UserToProjectMember));
                    }
                    projectMembers.Insert(0, new ProjectMember());
                }
                catch (Exception)
                {
                    projectMembers = null;
                    //throw new LoadException(Languages.Lang.BgWork_LoadProjectMembers, e);
                }

                try
                {
                    if (RedmineClientForm.RedmineVersion >= ApiVersion.V22x)
                    {
                        Enumerations.UpdateIssuePriorities(await RedmineClientForm.redmine.GetObjectsAsync<IssuePriority>(InitParameters()));
                        Enumerations.SaveIssuePriorities();

                        Enumerations.UpdateActivities(await RedmineClientForm.redmine.GetObjectsAsync<TimeEntryActivity>(InitParameters()));
                        Enumerations.SaveActivities();
                    }
                    issuePriorities = new List<Enumerations.EnumerationItem>(Enumerations.IssuePriorities);
                    issuePriorities.Insert(0, new Enumerations.EnumerationItem { Id = 0, Name = "", IsDefault = false });

                    activities = new List<Enumerations.EnumerationItem>(Enumerations.Activities);
                    activities.Insert(0, new Enumerations.EnumerationItem { Id = 0, Name = "", IsDefault = false });
                }
                catch (Exception e)
                {
                    throw new LoadException(Languages.Lang.BgWork_LoadPriorities, e);
                }

                try
                {
                    if (RedmineClientForm.RedmineVersion >= ApiVersion.V24x)
                    {
                        customFields = await RedmineClientForm.redmine.GetObjectsAsync<CustomField>(InitParameters());
                    }
                }
                catch (Exception e)
                {
                    throw new LoadException(Languages.Lang.BgWork_LoadCustomFields, e);
                }
            }

            try
            {
                NameValueCollection parameters = InitParameters();
                if (onlyMe)
                    parameters.Add(RedmineKeys.ASSIGNED_TO_ID, "me");
                else if (filter.AssignedToId > 0)
                    parameters.Add(RedmineKeys.ASSIGNED_TO_ID, filter.AssignedToId.ToString());

                if (filter.TrackerId > 0)
                    parameters.Add(RedmineKeys.TRACKER_ID, filter.TrackerId.ToString());

                if (filter.StatusId > 0)
                    parameters.Add(RedmineKeys.STATUS_ID, filter.StatusId.ToString());
                else if (filter.StatusId < 0)
                {
                    switch (filter.StatusId)
                    {
                        case -1: // all closed issues
                            parameters.Add(RedmineKeys.STATUS_ID, "closed");
                            break;

                        case -2: // all open and closed issues
                            parameters.Add(RedmineKeys.STATUS_ID, " *");
                            break;
                    }
                }

                if (filter.PriorityId > 0)
                    parameters.Add(RedmineKeys.PRIORITY_ID, filter.PriorityId.ToString());

                if (filter.VersionId > 0)
                    parameters.Add(RedmineKeys.FIXED_VERSION_ID, filter.VersionId.ToString());

                if (filter.CategoryId > 0)
                    parameters.Add(RedmineKeys.CATEGORY_ID, filter.CategoryId.ToString());

                if (!String.IsNullOrEmpty(filter.Subject))
                    parameters.Add(RedmineKeys.SUBJECT, "~" + filter.Subject);

                var issues = await RedmineClientForm.redmine.GetObjectsAsync<Issue>(parameters);
                return new MainFormData(projects, projectId, issues, allTrackers, categories,
                    versions, trackers, statuses, projectMembers, issuePriorities,
                    activities, customFields);
            }
            catch (Exception e)
            {
                throw new LoadException(Languages.Lang.BgWork_LoadIssues, e);
            }
        }

        private MainFormData(IList<Project> projects, int projectId, IList<Issue> issues, List<Tracker> trackers, List<IssueCategory> categories, List<Net.Api.Types.Version> versions, List<ProjectTracker> trackers1, List<IssueStatus> statuses, List<ProjectMember> projectMembers, List<Enumerations.EnumerationItem> issuePriorities, List<Enumerations.EnumerationItem> activities, List<CustomField> customFields)
        {
            ProjectId = projectId;
            Projects = new List<ClientProject>();
            Projects.Add(new ClientProject(new Project { Id = -1, Name = Languages.Lang.ShowAllIssues }));
            foreach (Project p in projects)
            {
                Projects.Add(new ClientProject(p));
            }
            Issues = issues;
            Trackers = trackers.ConvertAll(new Converter<Tracker, ProjectTracker>(TrackerToProjectTracker));
            Categories = categories;
            Versions = versions;
            Statuses = statuses;
            ProjectMembers = projectMembers;
            IssuePriorities = issuePriorities;
            Activities = activities;
            CustomFields = customFields;
        }

        private static ProjectTracker TrackerToProjectTracker(Tracker tracker)
        {
            return new ProjectTracker { Id = tracker.Id, Name = tracker.Name };
        }

        private static ProjectMember UserToProjectMember(User user)
        {
            return new ProjectMember(user);
        }

        public static Dictionary<int, T> ToDictionaryId<T>(IList<T> list) where T : Identifiable<T>, System.IEquatable<T>
        {
            Dictionary<int, T> dict = new Dictionary<int,T>();
            foreach (T element in list)
            {
                dict.Add(element.Id, element);
            }
            return dict;
        }

        public static Dictionary<int, Y> ToDictionaryName<Y>(IList<Y> list) where Y : IdentifiableName
        {
            Dictionary<int, Y> dict = new Dictionary<int, Y>();
            foreach (Y element in list)
            {
                dict.Add(element.Id, element);
            }
            return dict;
        }
    }
}
