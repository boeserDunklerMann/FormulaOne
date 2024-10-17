/*
 * creates tables which belongs to entity "racetrack"
 */
use [Bcm.Aed.FormulaOne]
go

create table bcm.Racetrack
(
	RacetrackID int identity not null,
	RacetrackName nvarchar(150) not null,
	RacetrackDistanceKM decimal(4,2) null,
	RacetrackCountryID int not null,

	constraint PK_Racetrack 
		primary key (RacetrackID),
	constraint FK_Racetrack_Country
		foreign key (RacetrackCountryID) references bcm.Country(CountryID)
);

create table bcm.RaceType
(
	RaceTypeID int identity not null,
	RaceTypeName nvarchar(50) not null,	-- Qualifying/Free Practise/Grand Prix/Sprint
	RaceTypeShort varchar(5) not null,	-- Q/FP/GP/S

	constraint PK_RaceType
		primary key(RaceTypeID)
);

/*
 * store racetrack, driver and year/date
 * so you have a race 
 * raceresults in separate table
 */

create table bcm.Race
(
	RaceID	int identity not null,
	RacetrackID int not null,
	DriverID int not null,
	RaceDate datetime not null,
	RaceTypeID int not null,
	Comment nvarchar(max) null,

	constraint PK_Race
		--primary key (RacetrackID, DriverID),	--Scheiﬂe! Denkfehler! Was ist, wenn der selbe Fahrer irgendwann nochmal am selben Ring f‰hrt?
		primary key (RaceID),
	constraint FK_Race_Racetrack
		foreign key (RacetrackID) references bcm.Racetrack(RacetrackID),
	constraint FK_Race_Driver
		foreign key (DriverID) references bcm.Driver(DriverID),
	constraint FK_Race_RaceType
		foreign key(RaceTypeID) references bcm.RaceType(RaceTypeID)
);
