USE [master]
GO
/****** Object:  Database [qlbanhang]    Script Date: 8/20/2021 3:33:36 PM ******/
CREATE DATABASE [qlbanhang]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'qlbanhang', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\qlbanhang.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'qlbanhang_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\qlbanhang_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [qlbanhang] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [qlbanhang].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [qlbanhang] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [qlbanhang] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [qlbanhang] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [qlbanhang] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [qlbanhang] SET ARITHABORT OFF 
GO
ALTER DATABASE [qlbanhang] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [qlbanhang] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [qlbanhang] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [qlbanhang] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [qlbanhang] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [qlbanhang] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [qlbanhang] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [qlbanhang] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [qlbanhang] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [qlbanhang] SET  ENABLE_BROKER 
GO
ALTER DATABASE [qlbanhang] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [qlbanhang] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [qlbanhang] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [qlbanhang] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [qlbanhang] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [qlbanhang] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [qlbanhang] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [qlbanhang] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [qlbanhang] SET  MULTI_USER 
GO
ALTER DATABASE [qlbanhang] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [qlbanhang] SET DB_CHAINING OFF 
GO
ALTER DATABASE [qlbanhang] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [qlbanhang] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [qlbanhang] SET DELAYED_DURABILITY = DISABLED 
GO
USE [qlbanhang]
GO
/****** Object:  User [XUANTHAO\pc]    Script Date: 8/20/2021 3:33:36 PM ******/
CREATE USER [XUANTHAO\pc] FOR LOGIN [XUANTHAO\pc] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[CTDH]    Script Date: 8/20/2021 3:33:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTDH](
	[MaDH] [int] NOT NULL,
	[MaSP] [nvarchar](4) NOT NULL,
	[Soluong] [int] NULL,
	[DongiaBan] [int] NULL,
	[Giamgia] [real] NULL,
 CONSTRAINT [PK_CTHD] PRIMARY KEY CLUSTERED 
(
	[MaDH] ASC,
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 8/20/2021 3:33:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang](
	[MaDH] [int] IDENTITY(1,1) NOT NULL,
	[MaKH] [int] NULL,
	[MaNV] [int] NULL,
	[NgayLapHD] [datetime] NULL,
	[NgayGiaoHang] [datetime] NULL,
	[DiaChiGiaoHang] [nchar](50) NULL,
 CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED 
(
	[MaDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 8/20/2021 3:33:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKH] [int] IDENTITY(1,1) NOT NULL,
	[TenKH] [nvarchar](30) NULL,
	[DiaChi] [nvarchar](30) NULL,
	[DienThoai] [nvarchar](7) NULL,
	[Fax] [nvarchar](12) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiSP]    Script Date: 8/20/2021 3:33:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiSP](
	[MaLoaiSP] [int] NOT NULL,
	[TenLoaiSP] [nvarchar](255) NULL,
 CONSTRAINT [PK_LoaiSP] PRIMARY KEY CLUSTERED 
(
	[MaLoaiSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Nhanvien]    Script Date: 8/20/2021 3:33:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nhanvien](
	[MaNV] [int] IDENTITY(1,1) NOT NULL,
	[HoNV] [nvarchar](50) NULL,
	[Ten] [nvarchar](50) NULL,
	[Diachi] [nvarchar](50) NULL,
	[Dienthoai] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_Nhanvien] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 8/20/2021 3:33:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSP] [nvarchar](4) NOT NULL,
	[TenSP] [nvarchar](20) NULL,
	[DonGia] [int] NULL,
	[MaLoaiSP] [int] NULL,
	[HinhSP] [nvarchar](max) NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[CTDH] ([MaDH], [MaSP], [Soluong], [DongiaBan], [Giamgia]) VALUES (1, N'SM01', 100, 15000, NULL)
SET IDENTITY_INSERT [dbo].[DonHang] ON 

INSERT [dbo].[DonHang] ([MaDH], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [DiaChiGiaoHang]) VALUES (1, 5, 1, CAST(N'2000-08-05 00:00:00.000' AS DateTime), CAST(N'2000-08-10 00:00:00.000' AS DateTime), N'123 Lê Lợi                                        ')
INSERT [dbo].[DonHang] ([MaDH], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [DiaChiGiaoHang]) VALUES (2, 9, 1, CAST(N'2000-10-21 00:00:00.000' AS DateTime), CAST(N'2000-10-30 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[DonHang] ([MaDH], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [DiaChiGiaoHang]) VALUES (3, 1, 1, NULL, CAST(N'2000-08-10 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[DonHang] ([MaDH], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [DiaChiGiaoHang]) VALUES (4, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[DonHang] ([MaDH], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [DiaChiGiaoHang]) VALUES (5, 29, 1, CAST(N'2021-08-19 10:53:58.580' AS DateTime), NULL, N'b                                                 ')
INSERT [dbo].[DonHang] ([MaDH], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [DiaChiGiaoHang]) VALUES (6, 29, 1, CAST(N'2021-08-19 10:55:18.903' AS DateTime), NULL, N'b                                                 ')
INSERT [dbo].[DonHang] ([MaDH], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [DiaChiGiaoHang]) VALUES (7, 1, 1, CAST(N'2021-08-19 10:56:06.733' AS DateTime), NULL, N'120 Ha Ton Quyen                                  ')
INSERT [dbo].[DonHang] ([MaDH], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [DiaChiGiaoHang]) VALUES (8, 29, 1, CAST(N'2021-08-19 11:55:59.357' AS DateTime), NULL, N'b                                                 ')
INSERT [dbo].[DonHang] ([MaDH], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [DiaChiGiaoHang]) VALUES (9, 1, 1, CAST(N'2021-08-19 11:58:37.920' AS DateTime), NULL, N'120 Ha Ton Quyen                                  ')
INSERT [dbo].[DonHang] ([MaDH], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [DiaChiGiaoHang]) VALUES (10, 29, 1, CAST(N'2021-08-19 14:29:55.870' AS DateTime), NULL, N'b                                                 ')
INSERT [dbo].[DonHang] ([MaDH], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [DiaChiGiaoHang]) VALUES (11, 29, 1, CAST(N'2021-08-19 16:14:53.297' AS DateTime), NULL, N'b                                                 ')
SET IDENTITY_INSERT [dbo].[DonHang] OFF
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (1, N'Nguyễn Xuân Thao', N'120 Ha Ton Quyen', N'8171717', N'084088171717', N'1851010123thao@ou.edu.vn', N'1')
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (2, N'Bong Hong', N'24 Ky Con', N'', N'084088800256', N'b', N'b')
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (3, N'Em Anh', N'6 Ky Hoa', N'', N'084088852258', NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (4, N'Ho Han', N'8 Pham van Khoe', N'8430156', N'084088430156', NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (5, N'Koko Company', N'90 An Duong Vuong', N'8250102', N'', NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (6, N'Queen Cozinha', N'891 An Duong Vuong', N'', N'', NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (7, N'Quoc Cuong', N'10 Tan Da', N'8950203', N'', NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (8, N'Suoi Tra', N'2817 Minh Phung', N'8356210', N'084088356210', NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (9, N'Song Trang', N'187 Lao Tu', N'9450210', N'', NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (10, N'Vinh Vien', N'45 Su Van hanh', N'', N'084088654790', NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (11, N'Trần Vinh Viễn', N'87 Trần Hưng Đạo', N'8855464', NULL, NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (12, N'Lý Văn Trung', N'123 Tân Tấn', NULL, NULL, NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (13, N'Cty Minh Hưng', N'456 Công Hòa', NULL, NULL, NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (14, N'Cty Nghia Đình', N'12 Lý chính Thắng', N'9874564', NULL, NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (15, N'Minh Anh', N'15/1 Hữu Giang', N'6548797', NULL, NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (16, N'Trần Tùng', N'12/15 Hậu Nghia', N'6547898', NULL, NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (17, N'Cty Vinh Lợi', N'1812 Trần Quang Khải', NULL, NULL, NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (18, N'Cty Thái Bình Duong', N'145 Nguyễn Hiền', N'6548797', NULL, NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (19, N'Cty Thần Tài', N'14 Lê Lợi', N'6523154', NULL, NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (20, N'Cty Trần Văn Trị', N'45 Nguyễn Thị Minh Khai', N'4561234', NULL, NULL, NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (21, N'Công ty Thành Lợi', N'Q1 TP.HCM', N'0913123', N'0913123456', N'hung@gmail.com', NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (22, N'Trần Hùng', N'Công ty TNHH Thành Lợi', N'0913123', N'0913123456', N'hung@gmail.com', NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (23, N'aaa', N'aaa', N'0913123', N'0913123456', N'hung@gmail.com', NULL)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (24, N'Cty Minh Hưng', N'87 Trần Hưng Đạo', N'123', N'123', N'thao@ou.edu.vn', N'123')
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (25, N'a', N'a', N'123', N'123', N'thao@ou.edu.vn', N'21')
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (26, N'b', N'b', N'123', N'123', N'124@gmail.com', N'12345')
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (27, N'Trần Vinh Viễn', N'123 Tân Tấn', N'123', N'123', N'thao@ou.edu.vn', N'1')
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (29, N'a', N'b', N'123', N'123', N'a', N'a')
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (30, N'a', N'87 Trần Hưng Đạo', N'123', N'123', N'1851010123thao@ou.edu.vn', N'7')
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (31, N'b', N'b', N'123', N'123', N'b', N'b')
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (32, N'Trần Vinh Viễn', N'b', N'123', N'123', N'a', N'a')
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (33, N'Trần Vinh Viễn', N'87 Trần Hưng Đạo', N'123', N'123', N'a', N'a')
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [Password]) VALUES (34, N'Trần Vinh Viễn', N'87 Trần Hưng Đạo', N'123', N'123', N'a', N'a')
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP]) VALUES (1, N'Áo thun')
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP]) VALUES (2, N'Áo Hoodie')
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP]) VALUES (3, N'Áo Sơ Mi')
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP]) VALUES (4, N'Túi sách')
SET IDENTITY_INSERT [dbo].[Nhanvien] ON 

INSERT [dbo].[Nhanvien] ([MaNV], [HoNV], [Ten], [Diachi], [Dienthoai], [Email], [Password]) VALUES (1, N'Nguyễn Xuân ', N'Thao', N'45 Trần Phú', N'8663447', N'1851010123thao@gmail.com.vn', N'1         ')
SET IDENTITY_INSERT [dbo].[Nhanvien] OFF
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'AT01', N'Oldskull Dungz White', 455000, 1, N'images/AoThunTrang.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'AT02', N'Oldskull Gori Rocket', 399000, 1, N'images/AoThunXam.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'AT03', N'Gori Season 3/ Xám', 225000, 1, N'images/AoThunXamNhat.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'AT04', N'Oldskull Destroy', 199000, 1, N'images/AoThunDen.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'AT05', N'Oldskull Helmet/ Nâu', 250000, 1, N'images/AoThunNau.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'AT06', N'Gori Basic Tee / Kem', 255000, 1, N'images/AoThunNauNhat.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'AT07', N'Gori Season 03/ Xám', 179000, 1, N'images/AoThunXam2.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'AT08', N'Basic Tee/ Green', 225000, 1, N'images/AoThunXanhLa.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'AT09', N'Oldskull Apparel', 299000, 1, N'images/AoThunCam.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'HD01', N'Hoodie With Mistake', 599000, 2, N'images/AoHoodieCam.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'HD02', N'Hoodie Life Gori', 555000, 2, N'images/AoHoodieXam.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'HD03', N'Hoodie Ss04/ Đen', 499000, 2, N'images/AoHoodieDen.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'SM01', N'Flannel / Đỏ Mới', 400000, 3, N'images/AoSoMiCaRoDo.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'SM02', N'Flannel / Vàng Mới', 355000, 3, N'images/AoSoMiCaRoVang.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'TS01', N'Gori Bag', 155000, 4, N'images/TuiKhongHoaTiet.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'TS02', N'Túi Tote/ Rocket', 199000, 4, N'images/TuiHoaTietTrang.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'TS03', N'Túi Tote/ Gori Brown', 250000, 4, N'images/TuiHoaTietCam.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonGia], [MaLoaiSP], [HinhSP]) VALUES (N'TS04', N'Túi Tote/ Gori Black', 199000, 4, N'images/TuiHoaTietDen.jpg')
ALTER TABLE [dbo].[CTDH]  WITH CHECK ADD  CONSTRAINT [FK_CTDH_DonHang] FOREIGN KEY([MaDH])
REFERENCES [dbo].[DonHang] ([MaDH])
GO
ALTER TABLE [dbo].[CTDH] CHECK CONSTRAINT [FK_CTDH_DonHang]
GO
ALTER TABLE [dbo].[CTDH]  WITH CHECK ADD  CONSTRAINT [FK_CTDH_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[CTDH] CHECK CONSTRAINT [FK_CTDH_SanPham]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_KhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_KhachHang]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_Nhanvien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[Nhanvien] ([MaNV])
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_Nhanvien]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_LoaiSP] FOREIGN KEY([MaLoaiSP])
REFERENCES [dbo].[LoaiSP] ([MaLoaiSP])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_LoaiSP]
GO
USE [master]
GO
ALTER DATABASE [qlbanhang] SET  READ_WRITE 
GO
