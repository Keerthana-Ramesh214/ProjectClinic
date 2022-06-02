CREATE TABLE UserPassword(
	[Staff_Id] [int] ,
	[Staff_Name] [varchar](15) ,
	[UserName] [varchar](15) ,
	[PassWord1] [varchar](15) 
) 

select * from UserPassword
insert into UserPassword values(1,'Keerthana','Keerthana','Keerthana@19')
insert into UserPassword values(2,'John','John','John@19')
insert into UserPassword values(3,'Rakesh','Rakesh','Rakesh@19')


create table Doctors(
Doctor_Id int primary key identity(100,1),
First_Name varchar(15),
Last_Name varchar(15),
Sex varchar(10),
Specialization varchar(25),
Visiting_Hours varchar(30)
)

Select * from Doctors

insert into Doctors values('Ramesh','Raj','Male','General','From 1Pm To 3Pm')
insert into Doctors values('Rajesh','Kumar','Male','Internal Medicine','From 1Pm To 3Pm') 
insert into Doctors values('Janani','Ram','Female','Pediatrics','From 1Pm To 3Pm')
insert into Doctors values('Kavya','G','Female','Orthopedics','From 1Pm To 3Pm')
insert into Doctors values('Krish','John','Male','Ophthalmology','From 1Pm To 3Pm')


create table Patient(
Patient_Id int primary key identity(1,1),
First_Name varchar(15),
Last_Name varchar(15),
Sex varchar(10),
Age int
)

select * from Patient

EXEC SelectPatientId @Fst_Name = 'Keerthana'


create proc SelectPatientId(@Fst_Name varchar(15))
as 
select Patient_Id from Patient where First_Name=@Fst_Name
go


create proc SelectDoctor(@Spec varchar(25))
as
select Doctor_Id from Doctors where Specialization=@Spec
go
EXEC SelectDoctor @Spec = 'General'


create table AppointMent(
AppointMent_Id int primary key identity(1,1),
Doctor_Id int references Doctors(Doctor_Id),
DateOfAppoint Date,
Slot varchar(20),
PatientId int
)

insert into AppointMent values(100,CONVERT(date,GETDATE()),'From_1pm_2pm',1)

create proc AppointMentForDoc(@DocId int)
as
select * from AppointMent where Doctor_Id=@DocId
go
EXEC AppointMentForDoc @DocId=100

create proc AppointMentForDoctor(@DocId int,@DOA date)
as
select * from AppointMent where Doctor_Id=@DocId and DateOfAppoint=@DOA
go


EXEC AppointMentForDoctor @DocId=100 ,@DOA='6/1/2022'

select * from AppointMent

delete from AppointMent where PatientId=2

create proc AvailabilityOfDoctor(@DOA Date,@Doc_Id int)
as
SELECT COUNT(*) FROM AppointMent WHERE DateOfAppoint=@DOA and Doctor_Id=@Doc_Id
go

create proc AvailabilityOfDoc(@DOA Date,@Doc_Id int)
as
SELECT * FROM AppointMent WHERE DateOfAppoint=@DOA and Doctor_Id=@Doc_Id
go

EXec AvailabilityOfDoctor @DOA='6/1/2022' ,@Doc_Id=100


create proc CancelAppointMent(@PtntId int )
as
Delete from AppointMent where PatientId=@PtntId
go