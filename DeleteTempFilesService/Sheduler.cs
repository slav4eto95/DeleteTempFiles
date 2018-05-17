using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace DeleteTempFilesService
{
    public partial class Sheduler : ServiceBase
    {
        private Timer timer = null;
        private bool executed;
        private DateTime executeDate;

        public Sheduler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            executeDate = DateTime.Now;
            executed = false;
            timer = new Timer();
            timer.AutoReset = true;
            // The interval is calculated in milliseconds.
            timer.Interval = Properties.Settings.Default.TimerInterval * 60 * 1000;
            // Add an event to the timer that will perform the DeleteTempFiles() method.
            timer.Elapsed += new ElapsedEventHandler(this.timer_Tick);
            timer.Start();

            DeleteClass.WriteErrorLog("####### SERVICE STARTED #######");
        }

        private void timer_Tick(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Today > executeDate)
            {
                executeDate = DateTime.Today;
                executed = false;
            }
            // If the current hour equals to the number, set in Settings and if the deletion is not executed yet,
            // delete the temp files.
            if (!executed && DateTime.Now.Hour == Properties.Settings.Default.ExecuteTime)
            {
                timer.Stop();
                DeleteClass.WriteErrorLog("Delete temp files started: " + DateTime.Now.ToString());
                string tempPath = Environment.GetEnvironmentVariable("TEMP");
                DeleteClass.DeleteTempFiles(tempPath);
                
                timer.Start();
            }
        }

        protected override void OnStop()
        {
            timer.Stop();
            DeleteClass.WriteErrorLog("####### SERVICE STOPPED #######");
        }
    }
}
