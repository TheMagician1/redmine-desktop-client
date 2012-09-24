using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redmine.Client
{
    class BgWork
    {
        public BgWork(String name, RunAsync work)
        {
            m_name = name;
            m_work = work;
        }
        public String m_name;
        public RunAsync m_work;
    };
}
