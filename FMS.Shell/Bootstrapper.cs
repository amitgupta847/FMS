
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.Practices.Unity;
using System.Windows.Controls;

using Prism.Unity;
using Prism.Modularity;
using Prism.Regions;
using FMS.Infrastructure;

namespace FMS.Shell
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IShellViewModel, ShellViewModel>();
        }

        //to read modules from app.config file
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }



        // to read module from resource dictionary.
        //protected override Microsoft.Practices.Prism.Modularity.IModuleCatalog CreateModuleCatalog()
        //{
        //    ModuleCatalog catalog = Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml
        //        (new Uri("/PluralsightPrismDemo;component/XamlCatalog.xaml", UriKind.Relative));

        //    return catalog;
        //}

        //// to read module from directory.
        //protected override Microsoft.Practices.Prism.Modularity.IModuleCatalog CreateModuleCatalog()
        //{
        //    ModuleCatalog catalog = new ModuleCatalog();
        //    catalog.AddModule(typeof(ModuleAModule));
        //    return catalog;
        //}

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
            return mappings;
        }
    }
}
