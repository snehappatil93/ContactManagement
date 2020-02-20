
--CREATE DATABASE ContactManagement;

USE ContactsManagement;
GO

CREATE TABLE [tblContact](
	[id] [int] IDENTITY(1,1) primary key,
	[first_name] [nvarchar](50) NOT NULL,
	[last_name] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NULL,
	[phone_number] [nvarchar](10) NULL,
	[status] [BIT] not null,
) ON [PRIMARY]
GO

select * from [tblContact];


