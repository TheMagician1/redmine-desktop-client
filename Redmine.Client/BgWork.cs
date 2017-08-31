using System;
using System.Threading.Tasks;

namespace Redmine.Client
{
    /// <summary>
    /// Class for storing asynchronous functions
    /// </summary>
    public class BgWork
    {
        /// <summary>
        /// Constructor of Background work
        /// </summary>
        /// <param name="name">a descriptive name for the job</param>
        /// <param name="work">the actual function to call</param>
        public BgWork(String name, Task work)
        {
            m_name = name;
            m_task = work;
        }
        public String m_name;
        public Task m_task;
    };
}
