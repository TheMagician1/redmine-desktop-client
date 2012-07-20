using System.Collections.Generic;
using Redmine.Net.Api.Types;

namespace Redmine.Client
{
    internal class MainFormData
    {
        public IList<Project> Projects { get; set; }
        public IList<Issue> Issues { get; set; }
        public IList<ProjectMembership> Members { get; set; }
    }
}
