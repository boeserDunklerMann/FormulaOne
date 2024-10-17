using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;

namespace Bcm.Aed.FormulaOne.WebAPI.WpfClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : RibbonWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			MVVM.MainVM vm = (MVVM.MainVM)this.DataContext;
			BindingOperations.EnableCollectionSynchronization(vm.Teams, vm._teamListLockObj);
		}

		private void rbnMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Ribbon ribbon = (Ribbon)sender;
			if (ribbon != null)
			{
				grdMain.Children.Clear();
				Control? child = null;
				switch (ribbon.SelectedIndex)
				{
					case 0:
						child = new Controls.StartControl();
						break;
					case 1:
						child = new Controls.TeamControl();
						break;
					case 2:
						child = new Controls.DriverControl();
						break;
					case 3:
						child = new Controls.TrackControl();
						break;
					case 4:
						child = new Controls.ResultsControl();
						break;
					default:
						throw new ArgumentException();
				}
				grdMain.Children.Add(child);
			}
			else
				throw new NullReferenceException();
		}
	}
}