using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Redmine.Client
{
    public delegate void OnDone();
    public delegate OnDone RunAsync();

    public class BgWorker : Form
    {
        private System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
        Queue<BgWork> m_WorkQueue = new Queue<BgWork>();
        public BgWorker()
        {
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_DoWork);
            this.worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_Complete);
        }
        /// <summary>
        /// Add a new background job
        /// </summary>
        /// <param name="name">The name used to display in the statusbar when executing this item</param>
        /// <param name="work">The function to execute in the background</param>
        public void AddBgWork(String name, RunAsync work)
        {
            m_WorkQueue.Enqueue(new BgWork(name, work));
            TriggerWork();
        }

        /// <summary>
        /// Start the background worker to process the workqueue
        /// </summary>
        /// <param name="bForce">Even if the worker is already started, start the next item</param>
        private void TriggerWork(bool bForce = false)
        {
            if (m_WorkQueue.Count == 0)
                return; //No work
            if (!bForce && m_WorkQueue.Count != 1)
                return; //Already busy...

            worker.RunWorkerAsync(m_WorkQueue.Peek().m_work);
            WorkTriggered(m_WorkQueue.Peek());
        }

        virtual protected void WorkTriggered(BgWork CurrentWork) {}

        /// <summary>
        /// Execute the background function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (e.Argument != null)
                e.Result = ((RunAsync)e.Argument)();
        }

        /// <summary>
        /// Current Backgroundwork is complete. Start next item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_Complete(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
                ((OnDone)e.Result)();
            m_WorkQueue.Dequeue();
            WorkTriggered(null);
            TriggerWork(true);
        }
    }
}
