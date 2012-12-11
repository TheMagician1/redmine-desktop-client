using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Redmine.Net.Api.Types;

namespace Redmine.Client
{
    /// <summary>
    /// Interface for showing Assignee information
    /// </summary>
    public interface IAssignee
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id of the assignee.</value>
        int Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name of the assignee.</value>
        string Name { get; }
    }

    public class Assignee : IAssignee
    {
        private ProjectMembership member;

        public Assignee(ProjectMembership projectMember)
        {
            this.member = projectMember;
        }
        public int Id { get { if (member.User == null) return member.Group.Id; else return member.User.Id; } }
        public string Name { get { if (member.User == null) return member.Group.Name; else return member.User.Name; } }
        /// <summary>
        /// Get the inner member of the projectmembership
        /// </summary>
        public ProjectMembership Member { get { return member; } }
    }

}
