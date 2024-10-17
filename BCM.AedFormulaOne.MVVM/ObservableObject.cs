using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcm.Aed.FormulaOne.MVVM
{
	/// <ChangeLog>
	/// <Create Datum="29.01.2024" Entwickler="AED" />
	/// </ChangeLog>
	public class ObservableObject : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		protected void RaisePropertyChangedEvent(string propertyName = "") =>
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}