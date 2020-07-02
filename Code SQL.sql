-- bảng bệnh nhân ------------
create table benhnhan
(
ma_bn char(10) primary key,
ho_bn nvarchar(10) not null,
ten_bn nvarchar(30) not null,
gioi nvarchar(5),
ngay_sinh date,
dia_chi nvarchar(100),
doi_tuong nvarchar(12), -- có BHYT / không BHYT
)
go
--------------------------------



-- bảng cơ quan ---------------  nhảy key
create table coquan
(
ma_cq int identity primary key,
ten_cq nvarchar(200),
dia_chi nvarchar(100),
sdt char(15),
fax char(15),
)
go
--------------------------------
ALTER TABLE coquan
ADD CONSTRAINT unique_ma_cq UNIQUE (ma_cq)


-- bảng BHYT --------------------
create table baohiem
( 
so_the char(15) primary key,
ma_bn char(10) not null,
ma_cq int,
thoi_gian date,
hieu_luc int,
ptram float,
noi_kham nvarchar(100),
foreign key (ma_cq) references coquan(ma_cq),
foreign key (ma_bn) references benhnhan(ma_bn),
)
go
----------------------------------


-- bảng nhân viên -----------------
create table nhanvien
(
ma_nv char(10) primary key,
ho_nv nvarchar(10) not null,
ten_nv nvarchar(30) not null,
gioi nvarchar(5),
ngay_sinh date,
noi_sinh nvarchar(100),
dia_chi nvarchar(100),
dan_toc nvarchar(10),
trinh_do nvarchar(100),
don_vi nvarchar(100),
chuc_vu nvarchar(50),
)
go
------------------------------------


-- bảng dịch vu --------------------  nhảy key
create table dichvu
(
ma_dv int identity primary key,
ten_dv nvarchar(100),
don_gia float,
loai nvarchar(20),
)
go
-------------------------------------
ALTER TABLE dichvu
ADD CONSTRAINT unique_ten_dv UNIQUE (ten_dv)

-- bảng khám bệnh -------------------
create table khambenh
(
ma_so char(10) primary key,
ma_bn char(10) not null,
bat_dau date,
ket_thuc date,
cd_vao nvarchar(200),
cd_ra nvarchar(200),
qua_trinh ntext,
phuong_phap ntext,
ket_qua ntext,
ghi_chu ntext,
foreign key (ma_bn) references benhnhan(ma_bn), 
)
go
-----------------------------------------


-- bảng chi tiết khám bệnh --------------
create table ctkhambenh
(
ma_so char(10) not null,
ma_nv char(10) not null,
primary key clustered (ma_so, ma_nv),
vai_tro nvarchar(50),
foreign key (ma_so) references khambenh(ma_so),
foreign key (ma_nv) references nhanvien(ma_nv),
)
go
-----------------------------------------


-- bảng hóa đơn --------------------------
create table hoadon
(
ma_hd char(10) primary key,
ma_so char(10) not null,
ma_nv char(10) not null,
tong_tien float,
tinh_trang nvarchar(20),  -- thanh toán / chưa thanh toán
thoi_gian time,
ngay date,
foreign key (ma_so) references khambenh(ma_so),
foreign key (ma_nv) references nhanvien(ma_nv),
)
go
---------------------------------------------


-- chi tiết hóa đơn -------------------------
create table cthoadon
(
ma_hd char(10) not null,
ma_dv int not null,
primary key clustered (ma_hd, ma_dv),
so_luong float,
thanh_tien float,
--tinh_trang nvarchar(20), -- hoàn thành/ chưa hoàn thành
foreign key (ma_hd) references hoadon(ma_hd),
foreign key (ma_dv) references dichvu(ma_dv),
)
go
---------------------------------------------


-- bảng tài khoản ----------------------------
create table taikhoan
(
nguoi_dung nvarchar(100),
tai_khoan nvarchar(100) primary key,
mat_khau nvarchar(100),
kieu int,
)
go
----------------------------------------------

CREATE PROC USP_doanhthu
@dau date, @cuoi date
AS
BEGIN
SELECT dichvu.ma_dv, dichvu.ten_dv, SUM(cthoadon.so_luong) AS so, SUM(cthoadon.thanh_tien) AS tong
FROM dichvu INNER JOIN cthoadon ON dichvu.ma_dv = cthoadon.ma_dv INNER JOIN hoadon ON hoadon.ma_hd = cthoadon.ma_hd
WHERE hoadon.ngay BETWEEN @dau AND @cuoi GROUP BY dichvu.ma_dv, dichvu.ten_dv
END