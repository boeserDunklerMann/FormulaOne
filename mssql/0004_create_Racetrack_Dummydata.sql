/*
 * create some dummy data for Racetrack and
 */
use [Bcm.Aed.FormulaOne]
go

insert into bcm.Racetrack
(RacetrackName, RacetrackDistanceKM, RacetrackCountryID) values
(N'Sachsenring Hohenstein-Ernstthal', 3.671, 1),
(N'Red Bull Ring Österreich - Spielberg', 4.318, 2),
(N'Nürburgring GP-Strecke', 5.148, 1)

select * from bcm.Racetrack

--delete from bcm.Racetrack

insert into bcm.RaceType (RaceTypeName, RaceTypeShort) values
(N'Qualifying', 'Q'),
(N'Free Practice', 'FP'),
(N'Grand Prix', 'GP'),
(N'Sprint', 'S'),
(N'Qualifying #1', 'Q1'),
(N'Qualifying #2', 'Q2');

select * from bcm.RaceType


insert into bcm.Race
(RacetrackID, DriverID, RaceDate, RaceTypeID, Comment) values
(1, 4, '2012-06-01', 2, N'mit Kawasaki ZX-10R Ninja')

--select * from bcm.Racetype
