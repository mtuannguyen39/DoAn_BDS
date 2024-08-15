create database DOAN_BDS

use DOAN_BDS 
go

CREATE TABLE Properties (
    ID INT PRIMARY KEY,
    TenBatDongSan NVARCHAR(max),
    DiaChi NVARCHAR(max),
    DienTich NVARCHAR(max), -- Kiểu dữ liệu chuỗi cho diện tích
    Gia FLOAT, -- Kiểu dữ liệu chuỗi cho giá
    DVT NVARCHAR(max),
    LoaiBatDongSan NVARCHAR(max),
    SoPhongNgu INT,
    SoPhongTam INT,
    TienIch NVARCHAR(max),
    MoTa TEXT,
    HinhAnh NVARCHAR(max),
    NguoiDangTinID INT, -- Khóa ngoại liên kết với bảng "Người dùng"
    PhiDangTin NVARCHAR(max), -- Kiểu dữ liệu chuỗi cho phí đăng tin
	IDdanhmuc int
	Foreign key (NguoiDangTinID) references  Users(ID),
	Foreign key (IDdanhmuc) references  Categories(ID)

);

CREATE TABLE Categories (
    ID INT PRIMARY KEY,
    TenDanhMuc NVARCHAR(Max),
);

CREATE TABLE Users (
    ID INT PRIMARY KEY,
    TenDangNhap VARCHAR(max),
    MatKhau VARCHAR(max),
    HoTen NVARCHAR(max),
    DienThoai VARCHAR(20),
    DiaChi NVARCHAR(max),
    QuyenHan NVARCHAR(max),
    TrangThai bit
);


CREATE TABLE Transactions (
    ID INT PRIMARY KEY,
    NgayGiaoDich DATE,
    GiaGiaoDich FLOAT,
    LoaiGiaoDich NVARCHAR(max),
    NguoiDungID INT, -- Khóa ngoại liên kết với bảng "Người dùng"
    BatDongSanID INT -- Khóa ngoại liên kết với bảng "Bất động sản",
	Foreign key (NguoiDungID) references  Users(ID),
	Foreign key (BatDongSanID) references  Properties(ID)
);

CREATE TABLE AdminUsers (
    ID int PRIMARY KEY,
    NameUser NVARCHAR(max),
    RoleUser NVARCHAR(max),
    PasswordUser NVARCHAR(max),
)