
go
use Data_XDPM
go
create table KhachHang
(
	Id int identity primary key,
	Ten nvarchar(40) null,
	DiaChi nvarchar(40) null,
	Sdt nvarchar(40) null,
	TrangThai bit null
)
go
create table NhanVien
(
	Id int identity primary key,
	Password nvarchar(40) null,
	Ten nvarchar(40) null,
	DiaChi nvarchar(40) null,
	Sdt nvarchar(40) null,
)
go
create table Title
(
	Id int identity primary key,
	Ten nvarchar(40) null,
	TheLoai bit null, /*0-Game, 1-Phim*/
	Gia int null,
	PhiTre int null,
	ThoiGianThue int null,
	TrangThai bit null
)
go
create table DVD
(
	Id int identity primary key,
	IdTitle int not null,
	TrangThai bit null,
	TinhTrang nvarchar(40) not null check(TinhTrang in ('TrenKe', 'Thue', 'DatTruoc' )),
	FOREIGN KEY (IdTitle) REFERENCES dbo.Title(Id)
)
go

create table PhieuMuon
(
	Id int identity primary key,
	IdKhachHang int not null,
	NgayTao DATETIME
    FOREIGN KEY (IdKhachHang) REFERENCES dbo.KhachHang(Id)
)
GO
create table ChiTietPhieuMuon
(
	IdPhieuMuon int not null,
	IdDVD int not null,
	NgayTra datetime null,
	PhiTre int NOT NULL,
	TinhTrang BIT, /*0-Da thanh toan_1-Chua thanh toan*/
	primary key (IdPhieuMuon, IdDVD),
	FOREIGN KEY (IdPhieuMuon) REFERENCES PhieuMuon(Id),
	FOREIGN KEY (IdDVD) REFERENCES dbo.DVD(Id)
)
go
create table PhieuDatTruoc
(
	Id int identity primary key,
	IdKhachHang int not null,
	NgayTao DATETIME
    FOREIGN KEY (IdKhachHang) REFERENCES dbo.KhachHang(Id)
)
GO
create table ChiTietPhieuDatTruoc
(
	IdPhieuDatTruoc int not null,
	IdTitle int not null,
	TrangThai bit not null,
	Huy bit null,
	primary key (IdPhieuDatTruoc, IdTitle),
	FOREIGN KEY (IdPhieuDatTruoc) REFERENCES dbo.PhieuDatTruoc(Id),
	FOREIGN KEY (IdTitle) REFERENCES dbo.Title(Id)
)
go


insert into KhachHang values ('Maria', 'Berlin', '030-0074321',1)
insert into KhachHang values ('Ana', 'Mexico', '(5) 555-4729',1)
insert into KhachHang values ('Antonio', 'London', '(171) 555-7788',1)
insert into KhachHang values ('Thomas', 'Lule', '0921-12 34 65',1)

insert into NhanVien values ('11111', 'Frederique', 'Strasbourg', '88.60.15.31')
insert into NhanVien values ('11111', 'Marten', 'Madrid', '(91) 555 22 82')
insert into NhanVien values ('11111', 'Laurence', 'Marseille', '91.24.45.40')

insert into Title values ('Cuoc chien giua cac vi sao', 1, 20000, 10000, 5,1)
insert into Title values ('Khung long khung khing', 1, 50000, 15000, 5,1)
insert into Title values ('GTA V', 0, 500000, 100000, 30,1)
insert into Title values ('Minecraft', 0, 200000, 30000, 50,1)
insert into Title values ('LOL', 0, 200000, 30000, 50,1)

insert into DVD values (1, 'TrenKe',1)
insert into DVD values (1, 'TrenKe',1)
insert into DVD values (1, 'TrenKe',1)
insert into DVD values (2, 'TrenKe',1)
insert into DVD values (2, 'TrenKe',1)
insert into DVD values (3, 'TrenKe',1)
insert into DVD values (3, 'TrenKe',1)
insert into DVD values (4, 'TrenKe',1)
insert into DVD values (4, 'TrenKe',1)

