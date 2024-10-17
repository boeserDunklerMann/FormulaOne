using Bcm.Aed.FormulaOne.Model;
using Microsoft.EntityFrameworkCore;


namespace Bcm.Aed.FormulaOne.EFTest.Cons
{
	internal class Program
	{
		static void Main(string[] args)
		{
			using (var ctx = new BcmAedFormulaOneContext())
			{
				//_ = ctx.Countries;
				//_ = ctx.Teams;

				ctx.Drivers
					.Include(c => c.DriverCountry)
					.Include(t => t.DriverTeam)
					.ToList().ForEach(d =>
				{
					Console.WriteLine($"{d.DriverName} ({d.DriverCountry?.CountryName}) - {d.DriverTeam?.TeamName}");
				});
			}
		}
	}
}
