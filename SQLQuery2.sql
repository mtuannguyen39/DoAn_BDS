CREATE TABLE POST (
	MaBD int identity(1,1) Primary key,
	Ten nvarchar(max),
	SDT nvarchar(11),
	Email nvarchar(max),
	DiaChi nvarchar(max),
	GiaBan float,
	DienTich float,
	TinhThanh nvarchar(max),
	QuanHuyen nvarchar(max),
	PhuongXa nvarchar(max),
	TinNhan nvarchar(max),
	img nvarchar(max)
)

alter table Transactions 
add constraint FK_Post_MaPost
foreign key (MaPost)
references Post(MaBD)