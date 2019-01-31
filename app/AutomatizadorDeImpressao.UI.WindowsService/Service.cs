using AutomatizadorDeImpressao.Domain;
using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace AutomatizadorDeImpressao.UI.WindowsService
{
    public partial class Service : ServiceBase
    {

        Timer timer = new Timer();
        Orquestrador orquestrador = new Domain.Orquestrador();

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Serviço iniciou em " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000;
            timer.Enabled = true;
        }
        protected override void OnStop()
        {
            WriteToFile("Serviço parou em " + DateTime.Now);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Serviço chamado em " + DateTime.Now);

            orquestrador.Iniciar();
            
        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {

                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}