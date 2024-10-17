/*
 * creates tables which belongs to entity "driver"
 */
use [Bcm.Aed.FormulaOne];
go

create table bcm.Country
(
	CountryID int identity not null,
	CountryName nvarchar(100) not null,
	CountryFlag varbinary(max) null,
	
	constraint PK_Country
		primary key (CountryID)
);

create table bcm.Team
(
	TeamID int identity not null,
	TeamName nvarchar(150) not null,
	TeamCountryID int null,

	constraint PK_Team
		primary key (TeamID),
	constraint FK_Team_Country
		foreign key (TeamCountryID) references bcm.Country(CountryID)
);

create table bcm.Driver
(
	DriverID int identity not null,
	DriverCountryID int null,
	DriverName nvarchar(200) not null,
	DateOfBirth date null,
	StartingNumber int null,
	DriverTeamID int null,

	constraint PK_Driver
		primary key (DriverID),
	constraint FK_Driver_Country
		foreign key (DriverCountryID) references bcm.Country(CountryID),
	constraint FK_Driver_Team
		foreign key (DriverTeamID) references bcm.Team(TeamID)
);
