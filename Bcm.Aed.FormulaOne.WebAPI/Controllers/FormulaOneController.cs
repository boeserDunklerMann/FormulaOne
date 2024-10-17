using Bcm.Aed.FormulaOne.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bcm.Aed.FormulaOne.WebAPI.Controllers
{
	/// <ChangeLog>
	/// <Create Datum="??.02.2024" Entwickler="AED" />
	/// </ChangeLog>
	/// <summary>
	/// Controller base class with DBContext
	/// </summary>
	public class FormulaOneController : ControllerBase, IDisposable
	{
		protected BcmAedFormulaOneContext? _context;
		public FormulaOneController()
		{
			_context = new BcmAedFormulaOneContext();
		}
		public void Dispose()
		{
			if (_context != null)
			{
				_context.Dispose();
				_context = null;
			}
		}
	}
}