using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reset_Service
{
    public partial class Form1 : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public Form1()
        {
            InitializeComponent();

            Log.Info("Serviço Iniciado!!!");

            try
            {
                
                Log.Info("Iniciado Serviço Reeinicio Service");
                //Reset_Program(); //Primeiro reset do programa mesmo o service não tenha sido reeiniciado ele reeinicia pela primeira vez.
                int tempoMin = Convert.ToInt32(ConfigurationManager.AppSettings["TimeProcess"].ToString());
                int tempoMS = (tempoMin * 60) * 1000;
                timer1.Interval = tempoMS;
                timer1.Enabled = true;

            }
            catch (Exception ex)
            {
                Log.Error("Erro Reset Service " + ex.ToString());
            }
            finally
            {
                Log.Debug("Finalizou do Service Reset Service");
            }
        }

        public void Reset_Program()
        {
            ServiceController sc = new ServiceController();
            sc.ServiceName = ConfigurationManager.AppSettings["NameKEPProcess"].ToString();
            if (sc.Status == ServiceControllerStatus.Running)
            {
                try
                {

                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped);
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);

                    Log.Debug("O Serviço Foi Reiniciado." + sc.Status.ToString() + "Hora Reinicio: " + DateTime.Now);
                }
                catch (InvalidOperationException ex)
                {
                    Log.Debug("O Serviço não pode ser Reiniciado" + sc.Status.ToString() + ex.Message);
                }
            }
        }



        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Reset_Program();
        }
    }
}
