using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Redmine.Net.Api.Types;

namespace Redmine.Client
{
    public interface IIssue
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id of the issue.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject of the issue.</value>
        string Subject { get; set; }
    }

    class CIssue : IIssue
    {
        private Issue issue;

        public CIssue(Issue issue)
        {
            this.issue = issue;
        }
        public int Id { get { return issue.Id; } set { issue.Id = value; } }
        public string Subject { get { return issue.Subject; } set { issue.Subject = value; } }
        public Issue Issue { get { return issue; } }
    }
}
