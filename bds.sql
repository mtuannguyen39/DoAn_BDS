CREATE TABLE Properties (
    ID INT PRIMARY KEY identity(1,1),
    TenBatDongSan NVARCHAR(max),
    DiaChi NVARCHAR(max),
    DienTich NVARCHAR(max), -- Kiểu dữ liệu chuỗi cho diện tích
    Gia float, -- Kiểu dữ liệu chuỗi cho giá
    DVT nvarchar(max),
    SoPhongNgu INT,
    SoPhongTam INT,
    TienIch NVARCHAR(max),
    MoTa NVARCHAR(max),
    HinhAnh NVARCHAR(max),
    NguoiDangTinID INT, -- Khóa ngoại liên kết với bảng "Người dùng"
	IDdanhmuc int
	Foreign key (NguoiDangTinID) references  Users(ID),
	Foreign key (IDdanhmuc) references  Categories(ID)

);
CREATE TABLE Users (
    ID INT PRIMARY KEY identity(1,1),
    TenDangNhap VARCHAR(max),
    MatKhau VARCHAR(max),
    HoTen NVARCHAR(max),
    DienThoai VARCHAR(20),
    DiaChi NVARCHAR(max),
    QuyenHan NVARCHAR(max),
    TrangThai bit
);