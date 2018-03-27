using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FMS.Modules.BasicInformation
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MainBanksView
    {
        IMainBankViewModel _viewModel;
        public MainBanksView(IMainBankViewModel viewModel):base(viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
          
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var task= _viewModel.LoadAsync();
       
            task.ContinueWith((t) =>
            {
                if (t.IsCompleted)
                {
                    Debug.WriteLine("Main window loaded");
                }
            });
            //task.c
        }


    }
}
