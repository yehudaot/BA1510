using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using System;
using System.Threading;
using System.Collections.Concurrent;

namespace Log4Net
{
    /// <summary>
    /// Description of RichTextBoxAppender.
    /// </summary>
    public class RichTextBoxAppender : AppenderSkeleton
    {
        private RichTextBox _richtextBox;
        
        private delegate void UpdateControlDelegate(log4net.Core.LoggingEvent loggingEvent);

        private ConcurrentQueue <log4net.Core.LoggingEvent> queue;
        Thread queueThread;
        bool killQueueThread = false;

        public string FormName { get; set; }
        public string RichTextBoxName { get; set; }

        private void UpdateControl(log4net.Core.LoggingEvent loggingEvent)
        {
            // I looked at the TortoiseCVS code to figure out how
            // to use the RTB as a colored logger.  It noted a performance
            // problem when the buffer got long, so it cleared it every 100K.
            if (_richtextBox.TextLength > 100000)
            {
                _richtextBox.Clear();
                _richtextBox.SelectionColor = Color.Gray;
                _richtextBox.AppendText("(earlier messages cleared because of log length)\n\n");
            }

            switch (loggingEvent.Level.ToString())
            {
                case "INFO":
                    _richtextBox.SelectionColor = Color.Black;
                    break;
                case "WARN":
                    _richtextBox.SelectionColor = Color.Blue;
                    break;
                case "ERROR":
                    _richtextBox.SelectionColor = Color.Red;
                    break;
                case "FATAL":
                    _richtextBox.SelectionColor = Color.DarkOrange;
                    break;
                case "DEBUG":
                    _richtextBox.SelectionColor = Color.DarkGreen;
                    break;
                default:
                    _richtextBox.SelectionColor = Color.Black;
                    break;
            }

            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
                Layout.Format(sw, loggingEvent);

            
            if(loggingEvent.ExceptionObject!=null)
            {
                sb.Append(loggingEvent.ExceptionObject.ToString());
                sb.AppendLine();
            }
            _richtextBox.AppendText(sb.ToString());
        }

        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            // prevent exceptions
            //if (_richtextBox != null && _richtextBox.Created)
            if (_richtextBox == null)
            {
                if (String.IsNullOrEmpty(FormName) ||
                String.IsNullOrEmpty(RichTextBoxName))
                    return;

                Form form = Application.OpenForms[FormName];
                if (form == null)
                    return;

                _richtextBox = form.Controls[RichTextBoxName] as RichTextBox;
                if (_richtextBox == null)
                    return;

                _richtextBox.ReadOnly = true;
                _richtextBox.HideSelection = false;      // allows rtb to allways append at the end
                _richtextBox.Clear();

                queue = new ConcurrentQueue<log4net.Core.LoggingEvent>();
                queueThread = new Thread(new ThreadStart(queueThreadFunc));
                queueThread.IsBackground = true;
                queueThread.Start();
                //queueThread.Priority = ThreadPriority.Lowest;
            }
            queue.Enqueue(loggingEvent);
        }

        protected override void OnClose()
        {
            killQueueThread = true;
            queueThread.Join(1000);
        }

        public void queueThreadFunc()
        {

            while(!killQueueThread)
            {
                log4net.Core.LoggingEvent e;
                try
                {
                    while (queue.TryDequeue(out e))
                    {
                        if (_richtextBox != null)
                        {
                            // make thread safe
                            if (_richtextBox.InvokeRequired)
                            {
                                _richtextBox.Invoke(
                                        new UpdateControlDelegate(UpdateControl),
                                        new object[] { e });
                            }
                            else
                            {
                                UpdateControl(e);
                            }
                        }
                    }
                    Thread.Yield();
                    Thread.Sleep(10);
                }
                catch
                {
                    /* silently kill this thread */
                    killQueueThread = true;
                }
            }
        }

    }
}