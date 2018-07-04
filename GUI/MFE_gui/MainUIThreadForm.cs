using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

public class MainUIThreadForm : Form
{
    private IntPtr formHandler;

    public MainUIThreadForm()
    {
        formHandler = IntPtr.Zero;
    }

    public void run()
    {
        if (formHandler == IntPtr.Zero)
        {
            this.HandleCreated += SecondFormHandleCreated;
            this.HandleDestroyed += SecondFormHandleDestroyed;
            this.RunInNewThread(false);
        }
    }

    //void EnableStopButton(bool enabled)
    //{
    //    if (InvokeRequired)
    //        BeginInvoke((Action)(() => EnableStopButton(enabled)));
    //    else
    //    {
    //        Control stopButton = Controls["Stop"];
    //        if (stopButton != null)
    //            stopButton.Enabled = enabled;
    //    }
    //}

    void SecondFormHandleCreated(object sender, EventArgs e)
    {
        Control second = sender as Control;
        formHandler = second.Handle;
        second.HandleCreated -= SecondFormHandleCreated;
    }

    void SecondFormHandleDestroyed(object sender, EventArgs e)
    {
        Control second = sender as Control;
        formHandler = IntPtr.Zero;
        second.HandleDestroyed -= SecondFormHandleDestroyed;
    }

    //const int WM_CLOSE = 0x0010;
    //[DllImport("User32.dll")]
    //extern static IntPtr PostMessage(IntPtr hWnd, int message, IntPtr wParam, IntPtr lParam);
}

internal static class FormExtensions
{
    private static void ApplicationRunProc(object state)
    {
        Application.Run(state as Form);
    }

    public static void RunInNewThread(this Form form, bool isBackground)
    {
        if (form == null)
            throw new ArgumentNullException("form");
        if (form.IsHandleCreated)
            throw new InvalidOperationException("Form is already running.");
        Thread thread = new Thread(ApplicationRunProc);
        thread.SetApartmentState(ApartmentState.STA);
        thread.IsBackground = isBackground;
        thread.Start(form);
    }
}