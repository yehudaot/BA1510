using System;
using System.Windows.Forms;

using log4net.Appender;
using log4net.Core;

namespace log4net
{
    public class ListBoxAppender : AppenderSkeleton
    {
        private ListBox _listBox;
        public string FormName { get; set; }
        public string ListBoxName { get; set; }

        private delegate void addLogToControlDelegate(string Log);

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (_listBox == null)
            {
                if (String.IsNullOrEmpty(FormName) ||
                String.IsNullOrEmpty(ListBoxName))
                    return;

                Form form = Application.OpenForms[FormName];
                if (form == null)
                    return;

                _listBox = form.Controls[ListBoxName] as ListBox;
                if (_listBox == null)
                    return;

                form.FormClosing += (s, e) => _listBox = null;
            }
            
            addLogToControlDelegate logDelegate = (x) =>
            {
                if (_listBox != null)
                {
                    string[] lList = x.Split('\n');
                    foreach (string line in lList)
                    {
                        string l = line.Trim();
                        if (l != "")
                        {
                            _listBox.Items.Add(l);
                        }
                    }
                    
                    _listBox.TopIndex = _listBox.Items.Count - 1;
                }
            };
            if(_listBox.InvokeRequired)
            {
                _listBox.Invoke(logDelegate, RenderLoggingEvent(loggingEvent));
            } else
            {
                logDelegate(RenderLoggingEvent(loggingEvent));
            }
                

            
        }
    } 
}
