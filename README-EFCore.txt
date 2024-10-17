adding EF Core support
* run from PM: dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.1
* Maybe change directory 
* add ref to Microsoft.EntityFrameworkCore.Design
* REBUILD

start SQL Server Configuration Manager as Admin, enable "SQL Server-Netzwerkkonfiguration"->Named Pipes and TCP/IP -> restart the machine


or look at this: https://learn.microsoft.com/en-us/ef/core/cli/dotnet
* run from PM: dotnet tool install --global dotnet-ef

scaffolding look here: https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/?tabs=dotnet-core-cli
 try scaffolding with this:
 dotnet-ef.exe dbcontext scaffold "Data Source=(local)\SQLEXPRESS;Initial Catalog=Bcm.Aed.FormulaOne;Persist Security Info=True;User ID=sa;Password=<INSERT SA PASSWORD HERE>;Encrypt=True;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer

IMPORTANT!!
* after scaffolding add JsonIgnore-attrib to 
- Country.Drivers, Country.Teams, Country.Racetracks
- Team.TeamCountry, Team.Drivers
- RaceType.Races
- Driver.Races
- Racetrack.Races
- Vehicle.Drivers