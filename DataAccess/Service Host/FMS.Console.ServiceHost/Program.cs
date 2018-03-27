using FMS.ServiceHosting.WCFSpecific;
using FMS.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FMS.ConsoleProj.ServiceHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceManager instance = ServiceManager.Instance)
            {
                instance.Load();
                //instance.Initialize(null);
                //instance.Start();
                Console.WriteLine("Services Published...");
                Console.WriteLine("Press Enter To Terminate");
                Console.ReadLine();
            }

            //try
            //{
            //    ServiceHost hostFMS = new ServiceHost(typeof(FMSService));

            //    hostFMS.Open();

            //    Console.WriteLine("Services started, Press [Enter] to exit");
            //    Console.ReadLine();
            //    hostFMS.Close();
            //}
            //catch (Exception ex)
            //{

            //}
        }
    }
}
