GO
CREATE TABLE [dbo].[MasterView]
(
	Id int primary key identity(1,1),
	ColumnName nvarchar(50),
	DisplayName nvarchar(50),
	ColumnIndex int,
	IsIncluded bit,
	IsExcluded bit,
	PageName nvarchar(50)
)

GO
Insert into MasterView values 
('Id','Student Id',1,1,0,'StudentView'),
('StudentName','Student Name',2,1,0,'StudentView'),
('Email','Email',3,1,0,'StudentView'),
('Contact','Contact Number',4,1,0,'StudentView'),
('City','City',5,1,0,'StudentView'),
('State','State',6,1,0,'StudentView'),
('Country','Country',7,1,0,'StudentView'),
('Zip','Zip',8,1,0,'StudentView'),
('Gender','Gender',9,1,0,'StudentView'),
('BloodGroup','Blood Group',10,1,0,'StudentView')

GO
CREATE TABLE [dbo].[Students]
(
	Id int primary key identity(1,1),
	StudentName nvarchar(50),
	Email nvarchar(50),
	Contact nvarchar(20),
	City nvarchar(30),
	[State] nvarchar(30),
	Country nvarchar(30),
	Zip nvarchar(20),
	Gender nvarchar(20),
	BloodGroup nvarchar(10)
)

GO
Insert into Students values 
('Virat Kohli','test@abc.com','123456','Delhi','Delhi','India','1234','Male','A+'),
('Rohit Sharma','test@abc.com','123456','Mumbai','Maharashtra','India','1234','Male','B+'),
('Ravindra Jadeja','test@abc.com','123456','Ahmedabad','Gujrat','India','1234','Male','B+'),
('Hardik Pandya','test@abc.com','123456','Mumbai','Maharashtra','India','1234','Male','A+'),
('Shubman Gill','test@abc.com','123456','Chandigarh','Punjab','India','1234','Male','AB+'),
('Surya Kumar Yadav','test@abc.com','123456','Mumbai','Maharashtra','India','1234','Male','A'),
('Jhulan Goswami','test@abc.com','123456','Kolkata','West Bengal','India','1234','FeMale','A+'),
('Mithali Raj','test@abc.com','123456','Hyderabad','Telengana','India','1234','FeMale','A+')

