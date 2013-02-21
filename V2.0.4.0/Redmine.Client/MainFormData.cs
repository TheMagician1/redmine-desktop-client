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
    }

    internal class MainFormData
    {
        public List<ClientProject> Projects { get; private set; }
        public IList<Issue> Issues { get; set; }
        public IList<ProjectMembership> Members { get; set; }
        public IList<TimeEntryActivity> Activities { get; private set; }

        public MainFormData(IList<Project> projects, int projectId, bool onlyMe)
        {
            Projects = new List<ClientProject>();
            foreach(Project p in projects)
            {
                Projects.Add(new ClientProject(p));
            }
            NameValueCollection parameters = new NameValueCollection { { "project_id", projectId.ToString() } };
            if (RedmineClientForm.RedmineVersion >= ApiVersion.V14x)
            {
                Members = RedmineClientForm.redmine.GetTotalObjectList<ProjectMembership>(parameters);
                if (RedmineClientForm.RedmineVersion >= ApiVersion.V22x)
                    Activities = RedmineClientForm.redmine.GetTotalObjectList<TimeEntryActivity>(null);
            }

            if (onlyMe)
                parameters.Add("assigned_to_id", "me");
            Issues = RedmineClientForm.redmine.GetTotalObjectList<Issue>(parameters);
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
