CREATE TABLE AdminUser (
	ID int identity(1,1) Primary key,
	NameUser nvarchar(max),
	RoleUser NVARCHAR(MAX),
	PasswordUser VARCHAR(MAX)
);
