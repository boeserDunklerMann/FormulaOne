/*
 * creates tables which belongs to "vehicle"
 */

create table bcm.Vehicle
(
	VehicleID	int identity not null,
	VehicleName nvarchar(200) not null,
	VehiclePhoto varbinary(max) null,

	constraint PK_Vehicle
		primary key(VehicleID)
);

alter table bcm.Driver add
	DriverVehicleID int null,
	constraint FK_Driver_Vehicle
		foreign key(DriverVehicleID) references bcm.Vehicle(VehicleID);