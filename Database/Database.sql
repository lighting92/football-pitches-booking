USE [master]
GO
/****** Object:  Database [FootballPitchesBooking]    Script Date: 05/06/2014 05:19:07 ******/
CREATE DATABASE [FootballPitchesBooking] ON  PRIMARY 
( NAME = N'FootballPitchesBooking', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\FootballPitchesBooking.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FootballPitchesBooking_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\FootballPitchesBooking_log.LDF' , SIZE = 576KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FootballPitchesBooking] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FootballPitchesBooking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FootballPitchesBooking] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET ANSI_NULLS OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET ANSI_PADDING OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET ARITHABORT OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [FootballPitchesBooking] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [FootballPitchesBooking] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [FootballPitchesBooking] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET  ENABLE_BROKER
GO
ALTER DATABASE [FootballPitchesBooking] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [FootballPitchesBooking] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [FootballPitchesBooking] SET  READ_WRITE
GO
ALTER DATABASE [FootballPitchesBooking] SET RECOVERY FULL
GO
ALTER DATABASE [FootballPitchesBooking] SET  MULTI_USER
GO
ALTER DATABASE [FootballPitchesBooking] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [FootballPitchesBooking] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'FootballPitchesBooking', N'ON'
GO
USE [FootballPitchesBooking]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
	[DisplayName] [nvarchar](50) NULL,
 CONSTRAINT [PK_WebsiteStaff] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Role] ON
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (1, N'WebsiteMaster', N'Website Master')
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (2, N'WebsiteStaff', N'Website Staff')
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (3, N'StadiumOwner', N'Stadium Owner')
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (4, N'StadiumStaff', N'Stadium Staff')
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (5, N'Member', N'Member')
SET IDENTITY_INSERT [dbo].[Role] OFF
/****** Object:  Table [dbo].[Configuration]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configuration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Configuration] ON
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (1, N'Bs_MostBooked', N'50')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (2, N'Bs_Nearest', N'30')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (3, N'Bs_MostPromoted', N'20')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (5, N'Nr_MostBooked', N'20')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (6, N'Nr_Nearest', N'70')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (7, N'Nr_MostPromoted', N'10')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (8, N'Pr_MostBooked', N'20')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (9, N'Pr_Nearest', N'10')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (10, N'Pr_MostPromoted', N'70')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (11, N'Gg_Periodic', N'3')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (13, N'Mb_MostBooked', N'70')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (14, N'Mb_Nearest', N'20')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (15, N'Mb_MostPromoted', N'10')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (17, N'Rv_MostBooked', N'30')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (18, N'Rv_Nearest', N'20')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (19, N'Rv_Expired', N'50')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (20, N'MinTimeBooking', N'30')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (21, N'MinTimeCancel', N'120')
SET IDENTITY_INSERT [dbo].[Configuration] OFF
/****** Object:  Table [dbo].[MemberRank]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberRank](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RankName] [nvarchar](50) NULL,
	[Point] [int] NULL,
	[Promotion] [nvarchar](500) NULL,
 CONSTRAINT [PK_MemberRank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[MemberRank] ON
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (1, N'Người mới', 0, NULL)
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (2, N'Khách vãng lai', 50, NULL)
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (3, N'Khách thường xuyên', 100, N'1 trái bóng động lực')
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (4, N'Khách mối', 200, N'1 đôi giày sân cỏ nhân tạo')
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (5, N'Thượng khách', 500, N'Phiếu ưu đãi giảm giá hằng năm')
SET IDENTITY_INSERT [dbo].[MemberRank] OFF
/****** Object:  Table [dbo].[User]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Point] [int] NOT NULL,
	[RankId] [int] NOT NULL,
	[JoinDate] [date] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (1, N'admin', N'123456', N'Đặt Sân Bóng Đá Online', N'51 Trần Hưng Đạo, 6, Quận 5, TP.Hồ Chí Minh', N'01662194419', N'webmaster@fpb.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (2, N'sanphuongdo', N'123456', N'Phương Đô', N'37 Nguyễn Văn Lượng, 10, Q. Gò Vấp, TP.Hồ Chí Minh', N'0918303875', N'sanbongphuongdo@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (3, N'sanbongk334', N'123456', N'Sân K334', N'119 Trần Văn Dư, 13, Q. Gò Vấp, TP.Hồ Chí Minh', N'01213727414', N'sanbongk334@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (4, N'sanhaison', N'123456', N'Sân Hải Sơn', N'Đường số 20, 5, Q. Gò Vấp, TP. Hồ Chí Minh', N'0909277792', N'sanhaison@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (5, N'sanbinhan', N'123456', N'Sân Bình An', N'17 Cây Trâm, 11, Q. Gò Vấp, TP. Hồ Chí Minh', N'0862722830', N'sanbinhan@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (6, N'sanphuongnam', N'123456', N'Sân Phương Nam', N'44/5 Phạm Văn Chiêu, 9, Q. Gò Vấp, TP. Hồ Chí Minh', N'0822149048', N'sanphuongnam@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (7, N'sana41', N'123456', N'Sân A41', N'18C Cộng hòa, 12, Q. Tân Bình, TP. Hồ Chí Minh', N'0902386860', N'sana41@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (8, N'san367', N'123456', N'Sân 367', N'367 Hoàng Hoa Thám, 12, Q. Tân Bình, TP. Hồ Chí Minh', N'088117244', N'san367@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (10, N'sanhoanggia', N'123456', N'Sân Hoàng Gia', N'4 Trường Chinh, 4, Q.Tân Bình, TP. Hồ Chí Minh', N'0938948887', N'sanhoanggia@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (11, N'sanhuynhtan', N'123456', N'Sân Huỳnh Tấn', N'18C Cộng hòa, 4, Q. Tân Bình, TP. Hồ Chí Minh', N'0866589794', N'sanhuynhtan@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (13, N'sanchaolua', N'123456', N'Sân Chảo Lửa', N'18C Cộng Hòa, 12, Q. Tân Bình, TP. Hồ Chí Minh', N'0955800800', N'sanchaolua@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (14, N'sandaoduyanh', N'123456', N'Sân Đào Duy Anh', N'21 Đào Duy Anh, 14, Q. Phú Nhuận, TP. Hồ Chí Minh', N'0838447700', N'sandaoduyanh@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (15, N'sanbongd3', N'123456', N'Sân D3', N'44 Đường D3, 25, Bình Thạnh, TP. HCM  ', N'0909060202', N'sanbongd3@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (16, N'sanhieptan', N'123456', N'Sân Hiệp Tân', N'161/3 Lũy Bán Bích, P. Hiệp Tân, Q.Tân Phú, TP. HCM', N'0909552255', N'sanhieptan@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (17, N'sansake', N'123456', N'Sân Sake', N'12 Đường 42, P.Linh Đông, Q.Thủ Đức, Tp.HCM', N'0979466161', N'sansake@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (19, N'sancdcntd', N'123456', N'Sân Cao Đẳng Công Nghệ Thủ Đức', N'53 Võ Văn Ngân, P. Linh Chiểu, Q.Thủ Đức, TP. HCM', N'0979825050 ', N'sancdcntd@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (20, N'sanntntd', N'123456', N'Sân Nhà Thiếu Nhi Thủ Đức', N'281 Võ Văn Ngân, P. Linh Chiểu, Q.Thủ Đức, TP.HCM ', N'0919709197', N'sanntntd@gmail.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (21, N'phuongnd', N'123456', N'Nguyễn Duy Phương', N'30 Phạm Văn Chiêu, phường 12, Gò Vấp, Hồ Chí Minh', N'01662194419', N'phuongnd6592@live.com', 0, 1, CAST(0x7D380B00 AS Date), 1, 5)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (23, N'giangnhh', N'123456', N'Nguyễn Hữu Hoàng Giang', N'406 Nguyễn Văn Công, 3, Gò Vấp, TP. Hồ Chí Minh', N'0167378045', N'giangnhhse60606@fpt.edu.vn', 0, 1, CAST(0x7D380B00 AS Date), 1, 5)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (24, N'khanhnq', N'123456', N'Nguyễn Quốc Khánh', N'140 Phan Xích Long, 2, Q. Phú Nhuận, TP. Hồ Chí Minh', N'0935939792', N'khanhnq60556@fpt.edu.vn', 0, 1, CAST(0x7D380B00 AS Date), 1, 5)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (25, N'thinhnd', N'123456', N'Nguyễn Đức Thịnh', N'13 Khổng Tử, P. Bình Thọ, Q. Thủ Đức, TP. Hồ Chí Minh', N'0982555036', N'thinhnd60398@fpt.edu.vn', 0, 1, CAST(0x7D380B00 AS Date), 1, 5)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[Stadium]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stadium](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Street] [nvarchar](50) NOT NULL,
	[Ward] [nvarchar](50) NOT NULL,
	[District] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[MainOwner] [int] NOT NULL,
	[OpenTime] [float] NOT NULL,
	[CloseTime] [float] NOT NULL,
	[ExpiredDate] [date] NOT NULL,
 CONSTRAINT [PK_Stadium] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Stadium] ON
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (1, N'Phương Đô', N'37 Nguyễn Văn Lượng', N'10', N'Q. Gò Vấp', N'0918303875', N'sanphuongdo@gmail.com', 1, 2, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (2, N'K334', N'119 Trần Văn Dư', N'13', N'Q. Gò Vấp', N'01213727414', N'sanbongk334@gmail.com', 1, 3, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (3, N'Hải Sơn', N'Đường số 20', N'5', N'Q. Gò Vấp', N'0909277792', N'sanhaison@gmail.com', 1, 4, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (4, N'Bình An', N'17 Cây Trâm', N'11', N'Q. Gò Vấp', N'0862722830', N'sanbinhan@gmail.com', 1, 5, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (5, N'Phương Nam', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'0822149048', N'sanphuongnam@gmail.com', 1, 6, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (6, N'A41', N'18C Cộng hòa', N'12', N'Q. Tân Bình', N'0902386860', N'sana41@gmail.com', 1, 7, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (7, N'367', N'367 Hoàng Hoa Thám', N'12', N'Q. Tân Bình', N'088117244', N'san367@gmail.com', 1, 8, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (8, N'Hoàng Gia', N'4 Trường Chinh', N'4', N'Q. Tân Bình', N'0938948887', N'sanhoanggia@gmail.com', 1, 10, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (9, N'Huỳnh Tấn', N'18C Cộng hòa', N'4', N'Q. Tân Bình', N'0866589794', N'sanhuynhtan@gmail.com', 1, 11, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (10, N'Chảo Lửa', N'18C Cộng Hòa', N'12', N'Q. Tân Bình', N'0955800800', N'sanchaolua@gmail.com', 1, 13, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (11, N'Đào Duy Anh', N'21 Đào Duy Anh', N'14', N'Q. Phú Nhuận', N'0838447700', N'sandaoduyanh@gmail.com', 1, 14, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (12, N'D3', N'44 Đường D3', N'25', N'Q. Bình Thạnh', N'0909060202', N'sanbongd3@gmail.com', 1, 15, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (13, N'Hiệp Tân', N'161/3 Lũy Bán Bích', N'P. Hiệp Tân', N'Q. Tân Phú', N'0909552255', N'sanhieptan@gmail.com', 1, 16, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (14, N'Sake', N'12 Đường 42', N'P. Linh Đông', N'Q. Thủ Đức', N'0979466161', N'sansake@gmail.com', 1, 17, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (15, N'Cao Đẳng Công Nghệ Thủ Đức', N'53 Võ Văn Ngân', N'P. Linh Chiểu', N'Q. Thủ Đức', N'0979825050 ', N'sancdcntd@gmail.com', 1, 19, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (16, N'Nhà Thiếu Nhi Thủ Đức', N'281 Võ Văn Ngân', N'P. Linh Chiểu', N'Q. Thủ Đức', N'0919709197', N'sanntntd@gmail.com', 1, 20, 0, 0, CAST(0xEB390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (17, N'Phương Nam 2', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'0822149048', N'sanphuongnam@gmail.com', 1, 6, 0, 0, CAST(0xEB390B00 AS Date))
SET IDENTITY_INSERT [dbo].[Stadium] OFF
/****** Object:  Table [dbo].[UserDistance]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDistance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Path] [nvarchar](50) NOT NULL,
	[UpdateDate] [date] NOT NULL,
 CONSTRAINT [PK_UserDistance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserDistance] ON
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (1, 6, N'sanphuongnam.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (2, 1, N'admin.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (3, 2, N'sanphuongdo.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (4, 3, N'sanbongk334.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (5, 4, N'sanhaison.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (6, 5, N'sanbinhan.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (7, 7, N'sana41.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (8, 8, N'san367.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (9, 10, N'sanhoanggia.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (10, 11, N'sanhuynhtan.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (11, 13, N'sanchaolua.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (12, 14, N'sandaoduyanh.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (13, 15, N'sanbongd3.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (14, 16, N'sanhieptan.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (15, 17, N'sansake.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (16, 19, N'sancdcntd.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (17, 20, N'sanntntd.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (18, 23, N'giangnhh.xml', CAST(0x7E380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (19, 21, N'phuongnd.xml', CAST(0x7E380B00 AS Date))
SET IDENTITY_INSERT [dbo].[UserDistance] OFF
/****** Object:  Table [dbo].[PunishMember]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PunishMember](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Reason] [nvarchar](200) NOT NULL,
	[Date] [datetime] NULL,
	[ExpiredDate] [datetime] NULL,
	[IsForever] [bit] NULL,
	[StaffId] [int] NOT NULL,
 CONSTRAINT [PK_PunishMember] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JoinSystemRequest]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JoinSystemRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[StadiumName] [nvarchar](100) NOT NULL,
	[StadiumAddress] [nvarchar](100) NOT NULL,
	[Note] [nvarchar](1000) NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_JoinSystemRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[JoinSystemRequest] ON
INSERT [dbo].[JoinSystemRequest] ([Id], [UserId], [FullName], [Address], [Phone], [Email], [StadiumName], [StadiumAddress], [Note], [Status], [CreateDate]) VALUES (1, 6, N'Sân Phương Nam', N'0822149048', N'0822149048', N'sanphuongnam@gmail.com', N'Sân Phương Nam 2', N'44/5 Phạm Văn Chiêu, 9, Q. Gò Vấp', N'Xin hỗ trợ thêm cụm sân', N'CHẤP NHẬN', CAST(0x0000A3230025A1BF AS DateTime))
SET IDENTITY_INSERT [dbo].[JoinSystemRequest] OFF
/****** Object:  Table [dbo].[Advertisement]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Advertisement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Position] [nvarchar](10) NOT NULL,
	[AdvertiseDetail] [ntext] NOT NULL,
	[CreateDate] [date] NOT NULL,
	[ExpiredDate] [date] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Creator] [int] NULL,
 CONSTRAINT [PK_Advertisement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StadiumStaff]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StadiumStaff](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[StadiumId] [int] NOT NULL,
	[IsOwner] [bit] NOT NULL,
 CONSTRAINT [PK_StadiumOwner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StadiumReview]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StadiumReview](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[StadiumId] [int] NOT NULL,
	[ReviewContent] [nvarchar](500) NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[Approver] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StadiumReview] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StadiumRating]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StadiumRating](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[StadiumId] [int] NOT NULL,
	[Rate] [float] NOT NULL,
 CONSTRAINT [PK_StadiumRating] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StadiumImage]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StadiumImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StadiumId] [int] NOT NULL,
	[Path] [nvarchar](100) NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Caption] [nvarchar](200) NULL,
 CONSTRAINT [PK_StadiumImage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[StadiumImage] ON
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (1, 1, N'1_0_635349377285429127.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (2, 2, N'2_0_635349377983799071.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (3, 3, N'3_0_635349379178987432.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (4, 4, N'4_0_635349379762440804.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (5, 5, N'5_0_635349380492462559.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (6, 7, N'7_0_635349381809497889.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (7, 8, N'8_0_635349382485216538.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (8, 9, N'9_0_635349383039768256.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (9, 10, N'10_0_635349383937039577.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (10, 11, N'11_0_635349384571025839.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (11, 12, N'12_0_635349385238093993.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (12, 13, N'13_0_635349385880120715.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (13, 14, N'14_0_635349386712198307.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (14, 15, N'15_0_635349387731836627.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (15, 16, N'16_0_635349388183202444.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (16, 17, N'17_0_635349399562473301.jpg', NULL, NULL)
SET IDENTITY_INSERT [dbo].[StadiumImage] OFF
/****** Object:  Table [dbo].[Notification]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[StadiumId] [int] NULL,
	[Message] [nvarchar](2000) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserNotification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldPrice]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldPrice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StadiumId] [int] NOT NULL,
	[FieldPriceName] [nvarchar](50) NOT NULL,
	[FieldPriceDescription] [nvarchar](200) NULL,
 CONSTRAINT [PK_FieldPrice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[FieldPrice] ON
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (1, 5, N'5 người', N'Bảng giá sân 5 người sân Phương Nam')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (2, 17, N'5 người', N'Bảng giá sân 5 người hệ thống sân Phương Nam 2')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (3, 17, N'7 người', N'Bảng giá sân 7 người sân Phương Nam 2')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (4, 1, N'5 người', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (5, 2, N'Giá sân 5', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (6, 3, N'Giá sân 5', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (7, 4, N'Giá sân 5', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (8, 5, N'Giá sân 5', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (9, 17, N'Giá sân 5', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (10, 6, N'Sân 5 người', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (11, 7, N'Giá sân 5', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (12, 8, N'Giá sân 5', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (13, 9, N'Giá sân 5', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (14, 10, N'Giá sân 5', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (15, 11, N'Giá sân 5', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (16, 12, N'Giá sân 5', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (17, 13, N'Giá sân 5', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (18, 14, N'Giá sân 5', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (19, 15, N'Giá sân 5', N'Bảng giá sân 5')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (20, 16, N'Giá sân 5', N'Bảng giá sân 5')
SET IDENTITY_INSERT [dbo].[FieldPrice] OFF
/****** Object:  Table [dbo].[Field]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Field](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StadiumId] [int] NOT NULL,
	[Number] [nvarchar](5) NOT NULL,
	[ParentField] [int] NULL,
	[FieldType] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[PriceId] [int] NOT NULL,
 CONSTRAINT [PK_Field] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Field] ON
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (1, 5, N'1', NULL, 5, 1, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (2, 5, N'2', NULL, 5, 1, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (3, 5, N'3', NULL, 5, 1, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (4, 17, N'1', NULL, 7, 1, 3)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (5, 17, N'2', NULL, 7, 1, 3)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (6, 17, N'1A', 4, 5, 1, 2)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (7, 17, N'1B', 4, 5, 1, 2)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (8, 17, N'2A', 5, 5, 1, 2)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (9, 17, N'2B', 5, 5, 1, 2)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (10, 1, N'1', NULL, 5, 1, 4)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (11, 1, N'2', NULL, 5, 1, 4)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (12, 1, N'3', NULL, 5, 1, 4)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (13, 1, N'4', NULL, 5, 1, 4)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (14, 2, N'1', NULL, 5, 1, 5)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (15, 2, N'2', NULL, 5, 1, 5)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (16, 2, N'3', NULL, 5, 1, 5)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (17, 2, N'4', NULL, 5, 1, 5)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (18, 3, N'1', NULL, 5, 1, 6)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (19, 3, N'2', NULL, 5, 1, 6)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (20, 3, N'3', NULL, 5, 1, 6)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (21, 3, N'4', NULL, 5, 1, 6)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (22, 3, N'5', NULL, 5, 1, 6)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (23, 3, N'6', NULL, 5, 1, 6)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (24, 4, N'1', NULL, 5, 1, 7)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (25, 4, N'2', NULL, 5, 1, 7)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (26, 4, N'3', NULL, 5, 1, 7)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (27, 4, N'4', NULL, 5, 1, 7)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (28, 5, N'1', NULL, 5, 1, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (29, 5, N'2', NULL, 5, 1, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (30, 5, N'3', NULL, 5, 1, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (31, 5, N'4', NULL, 5, 1, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (32, 5, N'5', NULL, 5, 1, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (33, 5, N'6', NULL, 5, 1, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (34, 17, N'1', NULL, 5, 1, 2)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (35, 17, N'2', NULL, 5, 1, 2)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (36, 17, N'3', NULL, 5, 1, 2)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (37, 17, N'4', NULL, 5, 1, 2)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (38, 6, N'1', NULL, 5, 1, 10)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (39, 6, N'2', NULL, 5, 1, 10)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (40, 6, N'3', NULL, 5, 1, 10)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (41, 6, N'4', NULL, 5, 1, 10)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (42, 6, N'5', NULL, 5, 1, 10)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (43, 6, N'6', NULL, 5, 1, 10)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (44, 7, N'1', NULL, 5, 1, 11)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (45, 7, N'2', NULL, 5, 1, 11)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (46, 7, N'3', NULL, 5, 1, 11)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (47, 7, N'4', NULL, 5, 1, 11)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (48, 7, N'5', NULL, 5, 1, 11)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (49, 7, N'6', NULL, 5, 1, 11)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (50, 8, N'1', NULL, 5, 1, 12)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (51, 8, N'2', NULL, 5, 1, 12)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (52, 8, N'3', NULL, 5, 1, 12)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (53, 8, N'4', NULL, 5, 1, 12)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (54, 8, N'5', NULL, 5, 1, 12)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (55, 8, N'6', NULL, 5, 1, 12)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (56, 9, N'1', NULL, 5, 1, 13)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (57, 9, N'2', NULL, 5, 1, 13)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (58, 9, N'3', NULL, 5, 1, 13)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (59, 9, N'4', NULL, 5, 1, 13)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (60, 10, N'1', NULL, 5, 1, 14)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (61, 10, N'2', NULL, 5, 1, 14)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (62, 10, N'3', NULL, 5, 1, 14)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (63, 10, N'4', NULL, 5, 1, 14)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (64, 10, N'5', NULL, 5, 1, 14)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (65, 10, N'6', NULL, 5, 1, 14)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (66, 11, N'1', NULL, 5, 1, 15)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (67, 11, N'2', NULL, 5, 1, 15)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (68, 11, N'3', NULL, 5, 1, 15)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (69, 11, N'4', NULL, 5, 1, 15)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (70, 11, N'5', NULL, 5, 1, 15)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (71, 11, N'6', NULL, 5, 1, 15)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (72, 12, N'1', NULL, 5, 1, 16)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (73, 12, N'2', NULL, 5, 1, 16)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (74, 12, N'3', NULL, 5, 1, 16)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (75, 12, N'4', NULL, 5, 1, 16)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (76, 12, N'5', NULL, 5, 1, 16)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (77, 12, N'6', NULL, 5, 1, 16)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (78, 13, N'1', NULL, 5, 1, 17)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (79, 13, N'2', NULL, 5, 1, 17)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (80, 13, N'3', NULL, 5, 1, 17)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (81, 13, N'4', NULL, 5, 1, 17)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (82, 14, N'1', NULL, 5, 1, 18)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (83, 14, N'2', NULL, 5, 1, 18)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (84, 14, N'3', NULL, 5, 1, 18)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (85, 14, N'4', NULL, 5, 1, 18)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (86, 15, N'1', NULL, 5, 1, 19)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (87, 15, N'2', NULL, 5, 1, 19)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (88, 15, N'3', NULL, 5, 1, 19)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (89, 15, N'4', NULL, 5, 1, 19)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (90, 15, N'5', NULL, 5, 1, 19)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (91, 15, N'6', NULL, 5, 1, 19)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (92, 16, N'1', NULL, 5, 1, 20)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (93, 16, N'2', NULL, 5, 1, 20)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (94, 16, N'3', NULL, 5, 1, 20)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (95, 16, N'4', NULL, 5, 1, 20)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (96, 16, N'5', NULL, 5, 1, 20)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (97, 16, N'6', NULL, 5, 1, 20)
SET IDENTITY_INSERT [dbo].[Field] OFF
/****** Object:  Table [dbo].[PriceTable]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FieldPriceId] [int] NOT NULL,
	[Day] [int] NOT NULL,
	[TimeFrom] [float] NOT NULL,
	[TimeTo] [float] NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_PriceTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[PriceTable] ON
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (1, 1, 0, 0, 0, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (2, 1, 0, 16, 18, 180000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (3, 1, 0, 18, 22, 200000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (4, 2, 0, 0, 0, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (5, 2, 0, 16, 18, 180000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (6, 2, 0, 18, 22, 200000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (7, 3, 0, 0, 0, 250000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (8, 3, 0, 16, 18, 300000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (9, 3, 0, 18, 22, 350000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (10, 4, 0, 0, 0, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (11, 4, 0, 16, 18, 200000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (12, 4, 0, 18, 22, 250000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (13, 5, 0, 0, 0, 120000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (14, 5, 0, 18, 20, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (15, 5, 0, 20, 23, 180000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (16, 6, 0, 0, 0, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (17, 6, 0, 18, 20, 200000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (18, 6, 0, 20, 23, 230000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (19, 7, 0, 0, 0, 100000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (20, 7, 0, 18, 20, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (21, 7, 0, 20, 23, 180000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (22, 8, 0, 0, 0, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (23, 8, 0, 18, 20, 180000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (24, 8, 0, 20, 23, 220000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (25, 9, 0, 0, 0, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (26, 9, 0, 18, 20, 180000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (27, 9, 0, 20, 23, 220000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (28, 10, 0, 0, 0, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (29, 10, 0, 18, 20, 180000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (30, 10, 0, 20, 23, 200000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (31, 11, 0, 0, 0, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (32, 12, 0, 0, 0, 170000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (33, 12, 0, 18, 23, 200000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (34, 13, 0, 0, 0, 120000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (35, 13, 0, 18, 20, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (36, 13, 0, 20, 23, 180000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (37, 14, 0, 0, 0, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (38, 14, 0, 18, 23, 200000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (39, 15, 0, 0, 0, 180000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (40, 15, 0, 17, 20, 200000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (41, 15, 0, 20, 23, 230000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (42, 16, 0, 0, 0, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (43, 16, 0, 18, 20, 170000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (44, 16, 0, 20, 23, 200000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (45, 17, 0, 0, 0, 120000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (46, 17, 0, 18, 23, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (47, 18, 0, 0, 0, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (48, 18, 0, 18, 23, 180000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (49, 19, 0, 0, 0, 120000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (50, 20, 0, 0, 0, 100000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (51, 20, 0, 18, 20, 130000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (52, 20, 0, 20, 23, 150000)
SET IDENTITY_INSERT [dbo].[PriceTable] OFF
/****** Object:  Table [dbo].[Promotion]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FieldId] [int] NOT NULL,
	[PromotionFrom] [date] NOT NULL,
	[PromotionTo] [date] NOT NULL,
	[Discount] [float] NOT NULL,
	[Creator] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Promotion] ON
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount], [Creator], [IsActive]) VALUES (1, 80, CAST(0x79380B00 AS Date), CAST(0x96380B00 AS Date), 20, 16, 1)
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount], [Creator], [IsActive]) VALUES (2, 92, CAST(0x78380B00 AS Date), CAST(0x96380B00 AS Date), 30, 20, 1)
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount], [Creator], [IsActive]) VALUES (3, 57, CAST(0x7E380B00 AS Date), CAST(0x97380B00 AS Date), 25, 11, 1)
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount], [Creator], [IsActive]) VALUES (4, 12, CAST(0x79380B00 AS Date), CAST(0x98380B00 AS Date), 50, 2, 1)
SET IDENTITY_INSERT [dbo].[Promotion] OFF
/****** Object:  Table [dbo].[Reservation]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FieldId] [int] NOT NULL,
	[UserId] [int] NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Date] [date] NOT NULL,
	[StartTime] [float] NOT NULL,
	[Duration] [float] NOT NULL,
	[Price] [float] NOT NULL,
	[Discount] [float] NULL,
	[VerifyCode] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Approver] [int] NULL,
	[Status] [nvarchar](20) NOT NULL,
	[NeedRival] [bit] NOT NULL,
	[RivalId] [int] NULL,
	[RivalName] [nvarchar](50) NULL,
	[RivalPhone] [nvarchar](20) NULL,
	[RivalEmail] [nvarchar](50) NULL,
	[RivalFinder] [int] NULL,
	[RivalStatus] [nvarchar](50) NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportUser]    Script Date: 05/06/2014 05:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ReportUserId] [int] NULL,
	[Reason] [nvarchar](50) NOT NULL,
	[Reference] [int] NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_ReportUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_User_IsActive]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  ForeignKey [FK_User_MemberRank]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_MemberRank] FOREIGN KEY([RankId])
REFERENCES [dbo].[MemberRank] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_MemberRank]
GO
/****** Object:  ForeignKey [FK_User_Role]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
/****** Object:  ForeignKey [FK_Stadium_User]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[Stadium]  WITH CHECK ADD  CONSTRAINT [FK_Stadium_User] FOREIGN KEY([MainOwner])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Stadium] CHECK CONSTRAINT [FK_Stadium_User]
GO
/****** Object:  ForeignKey [FK_UserDistance_User]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[UserDistance]  WITH CHECK ADD  CONSTRAINT [FK_UserDistance_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserDistance] CHECK CONSTRAINT [FK_UserDistance_User]
GO
/****** Object:  ForeignKey [FK_PunishMember_User]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[PunishMember]  WITH CHECK ADD  CONSTRAINT [FK_PunishMember_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[PunishMember] CHECK CONSTRAINT [FK_PunishMember_User]
GO
/****** Object:  ForeignKey [FK_PunishMember_User1]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[PunishMember]  WITH CHECK ADD  CONSTRAINT [FK_PunishMember_User1] FOREIGN KEY([StaffId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[PunishMember] CHECK CONSTRAINT [FK_PunishMember_User1]
GO
/****** Object:  ForeignKey [FK_JoinSystemRequest_User]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[JoinSystemRequest]  WITH CHECK ADD  CONSTRAINT [FK_JoinSystemRequest_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[JoinSystemRequest] CHECK CONSTRAINT [FK_JoinSystemRequest_User]
GO
/****** Object:  ForeignKey [FK_Advertisement_User]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[Advertisement]  WITH CHECK ADD  CONSTRAINT [FK_Advertisement_User] FOREIGN KEY([Creator])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Advertisement] CHECK CONSTRAINT [FK_Advertisement_User]
GO
/****** Object:  ForeignKey [FK_StadiumOwner_Stadium]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[StadiumStaff]  WITH CHECK ADD  CONSTRAINT [FK_StadiumOwner_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumStaff] CHECK CONSTRAINT [FK_StadiumOwner_Stadium]
GO
/****** Object:  ForeignKey [FK_StadiumOwner_User]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[StadiumStaff]  WITH CHECK ADD  CONSTRAINT [FK_StadiumOwner_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StadiumStaff] CHECK CONSTRAINT [FK_StadiumOwner_User]
GO
/****** Object:  ForeignKey [FK_StadiumReview_Stadium]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[StadiumReview]  WITH CHECK ADD  CONSTRAINT [FK_StadiumReview_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumReview] CHECK CONSTRAINT [FK_StadiumReview_Stadium]
GO
/****** Object:  ForeignKey [FK_StadiumReview_User]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[StadiumReview]  WITH CHECK ADD  CONSTRAINT [FK_StadiumReview_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StadiumReview] CHECK CONSTRAINT [FK_StadiumReview_User]
GO
/****** Object:  ForeignKey [FK_StadiumReview_User1]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[StadiumReview]  WITH CHECK ADD  CONSTRAINT [FK_StadiumReview_User1] FOREIGN KEY([Approver])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StadiumReview] CHECK CONSTRAINT [FK_StadiumReview_User1]
GO
/****** Object:  ForeignKey [FK_StadiumRating_Stadium]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[StadiumRating]  WITH CHECK ADD  CONSTRAINT [FK_StadiumRating_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumRating] CHECK CONSTRAINT [FK_StadiumRating_Stadium]
GO
/****** Object:  ForeignKey [FK_StadiumRating_User]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[StadiumRating]  WITH CHECK ADD  CONSTRAINT [FK_StadiumRating_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StadiumRating] CHECK CONSTRAINT [FK_StadiumRating_User]
GO
/****** Object:  ForeignKey [FK_StadiumImage_Stadium]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[StadiumImage]  WITH CHECK ADD  CONSTRAINT [FK_StadiumImage_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumImage] CHECK CONSTRAINT [FK_StadiumImage_Stadium]
GO
/****** Object:  ForeignKey [FK_Notification_Stadium]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Stadium]
GO
/****** Object:  ForeignKey [FK_Notification_User]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_User]
GO
/****** Object:  ForeignKey [FK_FieldPrice_Stadium]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[FieldPrice]  WITH CHECK ADD  CONSTRAINT [FK_FieldPrice_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[FieldPrice] CHECK CONSTRAINT [FK_FieldPrice_Stadium]
GO
/****** Object:  ForeignKey [FK_Field_Field]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_Field] FOREIGN KEY([ParentField])
REFERENCES [dbo].[Field] ([Id])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_Field]
GO
/****** Object:  ForeignKey [FK_Field_FieldPrice]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_FieldPrice] FOREIGN KEY([PriceId])
REFERENCES [dbo].[FieldPrice] ([Id])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_FieldPrice]
GO
/****** Object:  ForeignKey [FK_Field_Stadium]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_Stadium]
GO
/****** Object:  ForeignKey [FK_PriceTable_FieldPrice]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[PriceTable]  WITH CHECK ADD  CONSTRAINT [FK_PriceTable_FieldPrice] FOREIGN KEY([FieldPriceId])
REFERENCES [dbo].[FieldPrice] ([Id])
GO
ALTER TABLE [dbo].[PriceTable] CHECK CONSTRAINT [FK_PriceTable_FieldPrice]
GO
/****** Object:  ForeignKey [FK_Promotion_Field]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [FK_Promotion_Field] FOREIGN KEY([FieldId])
REFERENCES [dbo].[Field] ([Id])
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [FK_Promotion_Field]
GO
/****** Object:  ForeignKey [FK_Promotion_User]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [FK_Promotion_User] FOREIGN KEY([Creator])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [FK_Promotion_User]
GO
/****** Object:  ForeignKey [FK_Reservation_Field]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Field] FOREIGN KEY([FieldId])
REFERENCES [dbo].[Field] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Field]
GO
/****** Object:  ForeignKey [FK_Reservation_User]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User]
GO
/****** Object:  ForeignKey [FK_Reservation_User1]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User1] FOREIGN KEY([Approver])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User1]
GO
/****** Object:  ForeignKey [FK_Reservation_User2]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User2] FOREIGN KEY([RivalId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User2]
GO
/****** Object:  ForeignKey [FK_Reservation_User3]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User3] FOREIGN KEY([RivalFinder])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User3]
GO
/****** Object:  ForeignKey [FK_ReportUser_Reservation]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[ReportUser]  WITH CHECK ADD  CONSTRAINT [FK_ReportUser_Reservation] FOREIGN KEY([Reference])
REFERENCES [dbo].[Reservation] ([Id])
GO
ALTER TABLE [dbo].[ReportUser] CHECK CONSTRAINT [FK_ReportUser_Reservation]
GO
/****** Object:  ForeignKey [FK_ReportUser_User]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[ReportUser]  WITH CHECK ADD  CONSTRAINT [FK_ReportUser_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ReportUser] CHECK CONSTRAINT [FK_ReportUser_User]
GO
/****** Object:  ForeignKey [FK_ReportUser_User1]    Script Date: 05/06/2014 05:19:08 ******/
ALTER TABLE [dbo].[ReportUser]  WITH CHECK ADD  CONSTRAINT [FK_ReportUser_User1] FOREIGN KEY([ReportUserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ReportUser] CHECK CONSTRAINT [FK_ReportUser_User1]
GO
