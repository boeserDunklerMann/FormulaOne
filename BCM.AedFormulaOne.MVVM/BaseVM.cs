using Bcm.Aed.FormulaOne.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BCM.AedFormulaOne.MVVM
{
	/// <ChangeLog>
	/// <Create Datum="29.01.2024" Entwickler="AED" />
	/// </ChangeLog>
	public abstract class BaseVM : ObservableObject, IDisposable
	{
		#region protected fields
		/// <summary>
		/// client for WebAPI
		/// </summary>
		protected HttpClient? _httpClient;
		#endregion

		#region Ctor
		public BaseVM()
		{
			_httpClient = new HttpClient();
		}
		#endregion

		public void Dispose()
		{
			if (_httpClient != null)
			{
				_httpClient.Dispose();
				_httpClient = null;
			}
		}
	}
}