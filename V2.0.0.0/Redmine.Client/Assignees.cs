using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Redmine.Net.Api.Types;

namespace Redmine.Client
{
    public interface IAssignee
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id of the issue.</value>
        int Id { get; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject of the issue.</value>
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
        public ProjectMembership Member { get { return member; } }
    }

}
