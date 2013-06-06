using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Redmine.Net.Api.Types;

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

        #endregion
    }

    internal class MainFormData
    {
        public List<ClientProject> Projects { get; private set; }
        public IList<Issue> Issues { get; set; }
        public IList<TimeEntryActivity> Activities { get; private set; }
        // search data
        public List<ProjectTracker> Trackers { get; private set; }
        public List<IssueCategory> Categories { get; private set; }
        public List<IssueStatus> Statuses { get; private set; }
        public List<Redmine.Net.Api.Types.Version> Versions { get; private set; }
        public List<ProjectMember> ProjectMembers { get; private set; }
        public List<IdentifiableName> IssuePriorities { get; private set; }

        public MainFormData(IList<Project> projects, int projectId, bool onlyMe, Filter filter)
        {
            Projects = new List<ClientProject>();
            Projects.Add(new ClientProject(new Project { Id = -1, Name = Languages.Lang.ShowAllIssues }));
            foreach(Project p in projects)
            {
                Projects.Add(new ClientProject(p));
            }
            NameValueCollection parameters = new NameValueCollection();
            if (projectId != -1)
                parameters.Add("project_id", projectId.ToString());

            if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
            {
                if (projectId < 0)
                {
                    List<Tracker> allTrackers = (List<Tracker>)RedmineClientForm.redmine.GetTotalObjectList<Tracker>(null);
                    Trackers = allTrackers.ConvertAll(new Converter<Tracker, ProjectTracker>(TrackerToProjectTracker));

                    Categories = null;
                    Versions = null;
                }
                else
                {
                    NameValueCollection projectParameters = new NameValueCollection { { "include", "trackers" } };
                    Project project = RedmineClientForm.redmine.GetObject<Project>(projectId.ToString(), projectParameters);
                    Trackers = new List<ProjectTracker>(project.Trackers);

                    Categories = new List<IssueCategory>(RedmineClientForm.redmine.GetTotalObjectList<IssueCategory>(parameters));
                    Categories.Insert(0, new IssueCategory { Id = 0, Name = "" });

                    Versions = (List<Redmine.Net.Api.Types.Version>)RedmineClientForm.redmine.GetTotalObjectList<Redmine.Net.Api.Types.Version>(parameters);
                    Versions.Insert(0, new Redmine.Net.Api.Types.Version { Id = 0, Name = "" });

                }
                Trackers.Insert(0, new ProjectTracker { Id = 0, Name = "" });

                Statuses = new List<IssueStatus>(RedmineClientForm.redmine.GetTotalObjectList<IssueStatus>(parameters));
                Statuses.Insert(0, new IssueStatus { Id = 0, Name = "" });

                try
                {
                    if (RedmineClientForm.RedmineVersion >= ApiVersion.V14x
                        && projectId > 0)
                    {
                        List<ProjectMembership> projectMembers = (List<ProjectMembership>)RedmineClientForm.redmine.GetTotalObjectList<ProjectMembership>(parameters);
                        //RedmineClientForm.DataCache.Watchers = projectMembers.ConvertAll(new Converter<ProjectMembership, Assignee>(MemberToAssignee));
                        ProjectMembers = projectMembers.ConvertAll(new Converter<ProjectMembership, ProjectMember>(ProjectMember.MembershipToMember));
                    }
                    else
                    {
                        List<User> allUsers = (List<User>)RedmineClientForm.redmine.GetTotalObjectList<User>(null);
                        ProjectMembers = allUsers.ConvertAll(new Converter<User, ProjectMember>(UserToProjectMember));
                    }
                    ProjectMembers.Insert(0, new ProjectMember());
                }
                catch (Exception)
                {
                    ProjectMembers = null;
                }

                if (RedmineClientForm.RedmineVersion >= ApiVersion.V22x)
                {
                    Enumerations.UpdateIssuePriorities(RedmineClientForm.redmine.GetTotalObjectList<IssuePriority>(null));
                    Enumerations.SaveIssuePriorities();

                    Activities = RedmineClientForm.redmine.GetTotalObjectList<TimeEntryActivity>(null);
                }

                IssuePriorities = new List<IdentifiableName>(Enumerations.IssuePriorities);
                IssuePriorities.Insert(0, new IdentifiableName { Id = 0, Name = "" });
            }

            if (onlyMe)
                parameters.Add("assigned_to_id", "me");
            else if (filter.AssignedToId > 0)
                parameters.Add("assigned_to_id", filter.AssignedToId.ToString());

            if (filter.TrackerId > 0)
                parameters.Add("tracker_id", filter.TrackerId.ToString());

            if (filter.StatusId > 0)
                parameters.Add("status_id", filter.StatusId.ToString());

            if (filter.PriorityId > 0)
                parameters.Add("priority_id", filter.PriorityId.ToString());

            if (filter.VersionId > 0)
                parameters.Add("fixed_version_id", filter.VersionId.ToString());

            if (filter.CategoryId > 0)
                parameters.Add("category_id", filter.CategoryId.ToString());

            if (!String.IsNullOrEmpty(filter.Subject))
                parameters.Add("subject", "~" + filter.Subject);

            Issues = RedmineClientForm.redmine.GetTotalObjectList<Issue>(parameters);
        }

        private static ProjectTracker TrackerToProjectTracker(Tracker tracker)
        {
            return new ProjectTracker { Id = tracker.Id, Name = tracker.Name };
        }
        private static ProjectMember UserToProjectMember(User user)
        {
            return new ProjectMember(user);
        }

        public static Dictionary<int, T> ToDictionaryId<T>(IList<T> list) where T : Identifiable<T>
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
