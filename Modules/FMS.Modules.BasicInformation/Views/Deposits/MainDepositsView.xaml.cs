using System;
using System.Collections.Generic;
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
    public partial class MainDepositsView
    {
        IMainDepositsViewViewModel _viewModel;

        public MainDepositsView(IMainDepositsViewViewModel viewModel):base(viewModel)
        {
           _viewModel = viewModel;
            InitializeComponent();
          
           Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           await _viewModel.LoadAsync();
        }

        private void Hyperlink_OnClick(object sender, RoutedEventArgs e)
        {
            //var hl = e.OriginalSource as System.Windows.Documents.Hyperlink;
            //Process.Start(hl.NavigateUri.AbsoluteUri);
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowDetails();
        }
    }
}
