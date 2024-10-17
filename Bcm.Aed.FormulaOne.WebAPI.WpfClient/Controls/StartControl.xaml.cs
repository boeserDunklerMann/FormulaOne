using Bcm.Aed.FormulaOne.MVVM;
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

namespace Bcm.Aed.FormulaOne.WebAPI.WpfClient.Controls
{
    /// <summary>
    /// Interaction logic for StartControl.xaml
    /// </summary>
    public partial class StartControl : UserControl
    {
        public StartControl()
        {
            InitializeComponent();
        }

		private async void btnUpload_Click(object sender, RoutedEventArgs e)
		{
            var dlgOpen = new Microsoft.Win32.OpenFileDialog();
            bool? result = dlgOpen.ShowDialog();
            if (result == true)
            {
                string filename = dlgOpen.FileName;
                await ((MainVM)DataContext).SelectImageAsync(filename);
            }
        }
    }
}
