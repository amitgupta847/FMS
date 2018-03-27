using FMS.Business;
using FMS.DataAccess;
using FMS.DataAccess.Common;
using FMS.Services;
using Unity;

namespace FMS.ServiceHosting.Unity
{
    public class UnityAsContainer
    {
        private IUnityContainer theContainer;

        public UnityAsContainer(IUnityContainer container)
        {
            theContainer = container;
            RegisterTypes();
        }

        private void RegisterTypes()
        {

            theContainer.RegisterType<IUnitOfWorkFMS, UOF_FMS>();
            theContainer.RegisterType<IFMSService, FMSService>();
                
                    //.RegisterType<IBankRepository, BankRepository>()
                    //.RegisterType<IAccountRepository, AccountRepository>()
                    //.RegisterType<IDepositRepository, DepositRepository>();


            //theContainer.RegisterType<IAdministrator, Administrator>(
              //  new InjectionConstructor(typeof(IAdministrationDAS), typeof(IEmployeeManager), typeof(IStdClassStudentManager)));
        }
    }
}
