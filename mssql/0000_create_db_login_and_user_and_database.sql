/*
 * Creates login und user for FormulaOne database
 * creates db itself and schema
 * Run me as DB-root
 */

create login FormulaOne
with password = 'f1.08151';	-- TODO: secure this for future use!
go

create database [Bcm.Aed.FormulaOne];
go

use [Bcm.Aed.FormulaOne];
go

create user FormulaOne for login FormulaOne;	-- create db-user
go

-- TODO: secure this for future use, too!
GRANT CREATE schema TO FormulaOne;
GRANT CREATE table TO FormulaOne;
grant VIEW DEFINITION to FormulaOne;
grant all to FormulaOne;
go

create schema bcm authorization dbo;
go
ALTER AUTHORIZATION ON SCHEMA::[bcm] TO [FormulaOne]
GO
