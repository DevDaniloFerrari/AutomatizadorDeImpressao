using System.Reflection;
using System.ServiceProcess;

namespace AutomatizadorDeImpressao.UI.WindowsService
{
    static class Program
    {
        static void Main(string[] args)
        {

            if (args.Length > 0)
            {
                //Install service
                if (args[0].Trim().ToLower() == "--install")
                {
                    System.Configuration.Install.ManagedInstallerClass.InstallHelper(new string[] { "--install", Assembly.GetExecutingAssembly().Location });
                }

                //Uninstall service                 
                else if (args[0].Trim().ToLower() == "--uninstall")
                {
                    System.Configuration.Install.ManagedInstallerClass.InstallHelper(new string[] { "--uninstall", Assembly.GetExecutingAssembly().Location });
                }
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new Service()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
