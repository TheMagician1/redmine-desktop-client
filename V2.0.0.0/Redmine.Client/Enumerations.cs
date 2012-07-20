
using System;
using System.Collections.Generic;
using System.Text;
using Redmine.Net.Api.Types;

namespace Redmine.Client
{
    internal class Enumerations
    {
        public List<IdentifiableName> IssuePriorities { get; private set; }
        public List<IdentifiableName> DocumentCategories { get; private set; }
        public List<IdentifiableName> Activities { get; private set; }

        public Enumerations()
        {
            DocumentCategories = new List<IdentifiableName> {
                new IdentifiableName { Id = 1, Name = "User Documentation" },
                new IdentifiableName { Id = 2, Name = "Technical Documentation" }
                                   };
            IssuePriorities = new List<IdentifiableName> {
                new IdentifiableName { Id = 3, Name = "Low" },
                new IdentifiableName { Id = 4, Name = "Normal" },
                new IdentifiableName { Id = 5, Name = "High" },
                new IdentifiableName { Id = 6, Name = "Urgent" },
                new IdentifiableName { Id = 7, Name = "Immediate" }
                                   };
            Activities = new List<IdentifiableName> {
                new IdentifiableName { Id = 8, Name = "Design" },
                new IdentifiableName { Id = 9, Name = "Development" }
                                   };
        }
    }
}
