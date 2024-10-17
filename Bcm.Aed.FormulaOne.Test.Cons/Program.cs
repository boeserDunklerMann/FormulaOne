namespace Bcm.Aed.FormulaOne.Test.Cons
{
	internal class Program
	{
		static void Main(string[] args)
		{
			using (HttpClient client = new HttpClient())
			{
				Bcm.Aed.FormulaOne.WebAPI.MetaClient meta = new WebAPI.MetaClient("http://localhost:5122/", client);
				var cs = meta.CountriesGetAsync().Result;
			}
		}
	}
}