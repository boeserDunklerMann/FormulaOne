/*
 * create some dummy data for RaceResult and
 */
 use [Bcm.Aed.FormulaOne]

 insert into bcm.RaceResultType (RaceResultShort) values
 ('DNS'),	-- did not start
 ('DNQ'),	-- did not qualify
 ('DNF'),	-- did not finish
 ('DSQ'),	-- disqualified
 ('FIN');	-- finished

--select * from bcm.RaceResultType

insert into bcm.RaceResult
(RaceID, RaceResultTypeID, DurationMS, DistanceKM) values
(1, 4, 2000, 50);