using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redmine.Client
{
    /// <summary>
    /// Class for storing asynchronous functions
    /// </summary>
    class BgWork
    {
        /// <summary>
        /// Constructor of Background work
        /// </summary>
        /// <param name="name">a descriptive name for the job</param>
        /// <param name="work">the actual function to call</param>
        public BgWork(String name, RunAsync work)
        {
            m_name = name;
            m_work = work;
        }
        public String m_name;
        public RunAsync m_work;
    };
}
