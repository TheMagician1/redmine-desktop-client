using System.Collections.Generic;
using Redmine.Net.Api.Types;

namespace Redmine.Client
{
    internal class MainFormData
    {
        public IList<Project> Projects { get; set; }
        public IList<Issue> Issues { get; set; }
        public IList<ProjectMembership> Members { get; set; }

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
