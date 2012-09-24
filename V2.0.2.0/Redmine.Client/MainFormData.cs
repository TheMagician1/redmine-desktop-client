using System.Collections.Generic;
using System.Collections.Specialized;
using Redmine.Net.Api.Types;

namespace Redmine.Client
{
    public enum ApiVersion
    {
        V10x,
        V11x,
        V13x,
        V14x,
        V21x,
    }

    internal class MainFormData
    {
        public IList<Project> Projects { get; set; }
        public IList<Issue> Issues { get; set; }
        public IList<ProjectMembership> Members { get; set; }

        public MainFormData(int projectId)
        {
            NameValueCollection curProject = new NameValueCollection { { "project_id", projectId.ToString() } };
            Issues = RedmineClientForm.redmine.GetTotalObjectList<Issue>(curProject);
            if (RedmineClientForm.RedmineVersion >= ApiVersion.V14x)
                Members = RedmineClientForm.redmine.GetTotalObjectList<ProjectMembership>(curProject);
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
