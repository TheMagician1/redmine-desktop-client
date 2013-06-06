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
    public interface IProjectMember
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

    public class ProjectMember : IProjectMember
    {
        private ProjectMembership member;

        public ProjectMember()
        {
            this.member = new ProjectMembership { User = new IdentifiableName { Id = 0, Name = "" } };
        }
        public ProjectMember(User user)
        {
            this.member = new ProjectMembership { User = new IdentifiableName { Id = user.Id, Name = user.CompleteName() } };
        }
        public ProjectMember(ProjectMembership projectMember)
        {
            this.member = projectMember;
        }
        public int Id { get { if (member.User == null) return member.Group.Id; else return member.User.Id; } }
        public string Name { get { if (member.User == null) return member.Group.Name; else return member.User.Name; } }
        /// <summary>
        /// Get the inner member of the projectmembership
        /// </summary>
        public ProjectMembership Member { get { return member; } }

        public static ProjectMember MembershipToMember(ProjectMembership projectMember)
        {
            return new ProjectMember(projectMember);
        }

    }

}
