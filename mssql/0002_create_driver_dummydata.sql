/*
 * creates some driver's dummy data
 */
USE [Bcm.Aed.FormulaOne]
go

insert into bcm.Country (CountryName) values (N'Germany'), (N'Austria');
go
select * from bcm.country

insert into bcm.Team (TeamName, TeamCountryID) values (N'Red Bull Racing', 2), (N'BMW Sauber', 1), (N'Mercedes', 1)
--select * from bcm.team

insert into bcm.Driver (DriverCountryID, DriverName, DateOfBirth, DriverTeamID, StartingNumber) values
(1, N'Nick Heidfeld', null /*unknown*/, 2, 99),
(1, N'Sebastian Vettel', '19780721', 1, 1),
(2, N'Michael Schumacher', '19711231', 3, 2);

--select * from bcm.Driver
--truncate table bcm.Driver
