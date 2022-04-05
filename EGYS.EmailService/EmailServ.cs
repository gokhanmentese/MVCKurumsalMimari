using Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace EGYS.EmailService
{
    partial class EmailServ : ServiceBase
    {
        #region Global Parameters
        private Timer GCTimer;
        private Timer mainTimer;
        private readonly IEmailService _emailService;
        #endregion

        #region Constructor
        public EmailServ(IEmailService emailService)
        {
            try
            {
                _emailService = emailService;
                InitializeComponent();

                Init();
            }
            catch (Exception ex)
            {

            }
            _emailService = emailService;
        }

        private void Init()
        {
            try
            {
                mainTimer = new Timer();
                mainTimer.Interval = 1000 * 60 * 1;
                mainTimer.Elapsed += new ElapsedEventHandler(mainTimer_Elapsed);
                mainTimer.Enabled = false;

                GCTimer = new System.Timers.Timer(60000 * 13);
                GCTimer.Elapsed += new ElapsedEventHandler(GCTimer_Elapsed);
                GCTimer.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Timer Elapseds
        private void mainTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            mainTimer.Elapsed -= mainTimer_Elapsed;
            mainTimer.Enabled = false;

            try
            {
                _emailService.SendMailForNotSended();
            }
            catch (Exception ex)
            {
            }

            mainTimer.Interval = 1000 * 60 * 33;
            mainTimer.Elapsed += new ElapsedEventHandler(mainTimer_Elapsed);
            mainTimer.Enabled = true;
        }

        private void GCTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock ("Service1_temizleTimer")
            {
                GC.Collect(0);

                if (!mainTimer.Enabled)
                {
                    mainTimer.Enabled = true;
                }
            }
        }
        #endregion

        #region Windows Service Methods
        protected override void OnStart(string[] args)
        {
            mainTimer.Enabled = true;
            GCTimer.Enabled = true;
        }

        protected override void OnStop()
        {
            try
            {
                mainTimer.Stop();
                GCTimer.Stop();
            }
            catch (Exception ex)
            {

            }

            base.OnStop();
        }

        protected override void OnContinue()
        {
            mainTimer.Start();
            GCTimer.Start();
            base.OnContinue();
        }

        protected override void OnPause()
        {
            try
            {
                mainTimer.Stop();
                GCTimer.Stop();
            }
            catch (Exception ex)
            {

            }

            base.OnPause();
        }

        protected override void OnShutdown()
        {
            try
            {
                mainTimer.Stop();
                GCTimer.Stop();
                mainTimer.Dispose();
                mainTimer = null;
                GCTimer.Dispose();
                GCTimer = null;

            }
            catch (Exception ex)
            {

            }

            base.OnShutdown();
        }
        #endregion
    }
}
