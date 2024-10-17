using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bcm.Aed.FormulaOne.MVVM
{
		/// <ChangeLog>
		/// <Create Datum="29.01.2024" Entwickler="AED" />
		/// </ChangeLog>
		/// <summary>
		/// Implement ICommand, this is what the views actions are bound to.
		/// </summary>
	public class DelegateCommand : ICommand
	{
		private readonly Action _action;

		public DelegateCommand(Action action) => _action = action;

		public void Execute(object parameter) => _action();

		public bool CanExecute(object parameter) => true;
		/*
		 * One thing to note is that CanExecute always returns true. Don’t you just hate it when a button or a menuitem is greyed-out for no apparent reason? Don’t do it! Much better to allow the button to be clicked and then give some informative feedback as to why the intended action cannot be carried out.
		 * http://www.markwithall.com/programming/2013/03/01/worlds-simplest-csharp-wpf-mvvm-example.html
		 */
#pragma warning disable 67
		public event EventHandler CanExecuteChanged;
#pragma warning restore 67
	}
}