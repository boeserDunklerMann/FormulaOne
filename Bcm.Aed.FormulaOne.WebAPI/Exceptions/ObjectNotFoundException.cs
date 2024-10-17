namespace Bcm.Aed.FormulaOne.WebAPI.Exceptions
{
	/// <ChangeLog>
		/// <Create Datum="12.02.2024" Entwickler="AED" />
		/// </ChangeLog>
		/// <summary>
		/// Will be thrown if an Object (driver, racetrack, etc.) cannot be found
		/// </summary>
	public class ObjectNotFoundException : Exception
	{
		private string? _entityName;
		private int? _id;
		public ObjectNotFoundException(string entityName, int? id)
		{
			_entityName = entityName;
			_id = id;
		}

		public override string ToString()
		{
			return $"Entity {_entityName} with ID {_id} not found.";
		}
	}
}
