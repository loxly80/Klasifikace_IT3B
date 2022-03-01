create table Student(
	Id int identity(1,1) primary key,
	Firstname nvarchar(50),
    Lastname nvarchar(50),
    Birthday date
)