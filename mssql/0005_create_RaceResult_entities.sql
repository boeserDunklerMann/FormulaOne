/*
 * creates tables which belongs to entity "raceresult"
 */


create table bcm.RaceResultType(
	RaceResultTypeID int identity not null,
	RaceResultShort varchar(5) not null,	-- DNS/DNQ/DNF/FIN/...

	constraint PK_RaceResultType
		primary key(RaceResultTypeID)
);

create table bcm.RaceResult(
	RaceID int not null,
	RaceResultTypeID	int not null,
	DurationMS	bigint null,
	DistanceKM	decimal(4,2) null,

	constraint PK_RaceResult
		primary key(RaceID),

	constraint FK_RaceResult_Race
		foreign key(RaceID) references bcm.Race(RaceID),

	constraint FK_RaceResult_RaceResultType
		foreign key(RaceResultTypeID) references bcm.RaceResultType(RaceResultTypeID)
);
