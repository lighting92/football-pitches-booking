USE [master]
GO
/****** Object:  Database [FootballPitchesBooking]    Script Date: 01/23/2014 01:03:42 ******/
CREATE DATABASE [FootballPitchesBooking] ON  PRIMARY 
( NAME = N'FootballPitchesBooking', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\FootballPitchesBooking.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FootballPitchesBooking_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\FootballPitchesBooking_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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
ALTER DATABASE [FootballPitchesBooking] SET  DISABLE_BROKER
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
/****** Object:  Table [dbo].[Advertisement]    Script Date: 01/23/2014 01:03:44 ******/
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
 CONSTRAINT [PK_Advertisement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Advertisement] ON
INSERT [dbo].[Advertisement] ([Id], [Position], [AdvertiseDetail], [CreateDate], [ExpiredDate], [Status]) VALUES (1, N'H1', N'Advertisement 1', CAST(0x17380B00 AS Date), CAST(0x8F380B00 AS Date), N'Active')
INSERT [dbo].[Advertisement] ([Id], [Position], [AdvertiseDetail], [CreateDate], [ExpiredDate], [Status]) VALUES (2, N'S1', N'Advertisement 2', CAST(0x17380B00 AS Date), CAST(0x8F380B00 AS Date), N'Active')
INSERT [dbo].[Advertisement] ([Id], [Position], [AdvertiseDetail], [CreateDate], [ExpiredDate], [Status]) VALUES (3, N'G1', N'Advertisement 3', CAST(0x17380B00 AS Date), CAST(0x8F380B00 AS Date), N'Active')
SET IDENTITY_INSERT [dbo].[Advertisement] OFF
/****** Object:  Table [dbo].[Stadium]    Script Date: 01/23/2014 01:03:44 ******/
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
	[City] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Stadium] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Stadium] ON
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [City], [Description], [Phone], [Email]) VALUES (2, N'CLB Bóng Đá Phương Đô', N'37 Nguyễn Văn Lượng', N'10', N'Gò Vấp', N'Hồ Chí Minh', N'Số lượng sân: Sân 5 Người: 8', N'08 66789487', N'clbphuongdo@gmail.com')
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [City], [Description], [Phone], [Email]) VALUES (4, N'Trung Tâm Thể Thao A2', N'18D Cộng Hòa', N'4', N'Tân Bình', N'Hồ Chí Minh', N'Số lượng sân: 1 sân 7 người, 8 sân 5 người ', N'093 385 5050', N'a2stadium@yahoo.com')
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [City], [Description], [Phone], [Email]) VALUES (8, N'Sân Thống Nhất', N'30 Nguyễn Kim', N'6', N'10', N'Hồ Chí Minh', N'Số lượng sân: Sân 11 Người: 1', N'0958888079 ', N'thongnhatstadium@gmail.com')
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [City], [Description], [Phone], [Email]) VALUES (10, N'Sân Quốc Phòng 2 - Quân Khu 7', N' 2 Sân Vận Động Quân Khu 7 Đường Phổ Quang', N'2', N'Tân Bình', N'Hồ Chí Minh', N'Số lượng sân: Sân 7 Người: 2, Sân 11 Người: 1', N' 08 9973812 ', N'quocphongstadium@gmail.com')
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [City], [Description], [Phone], [Email]) VALUES (12, N'Sân Thép Miền Nam Cảng Sài Gòn', N'KP4 đường Tân Mỹ', N'Tân Thuận Tây', N'7', N'Hồ Chí Minh', N'Số lượng sân: Sân 11 Người:1.', N'08 8720903', N'cangsaigonstadium@gmail.com')
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [City], [Description], [Phone], [Email]) VALUES (14, N'Sân Phú Thọ', N' 2 Lê Đại Hành', N'10', N'11', N'Hồ Chí Minh', N'Số lượng sân: Sân 7 Người: 6, Sân 11 Người: 4', N'0908 777856', N'phuthostadium@gmail.com')
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [City], [Description], [Phone], [Email]) VALUES (16, N'Sân Phú Nhuận', N'3 Hoàng Minh Giám', N'9', N'Phú Nhuận', N'Hồ Chí Minh', N'Số lượng sân: Sân 7 Người: 1,Sân 11 Người:1', N'08 8477668', N'phunhuanstadium@gmail.com')
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [City], [Description], [Phone], [Email]) VALUES (17, N'Sân Đạt Đức', N'5A Nguyễn Văn Lượng', N'16', N'Gò Vấp', N'Hồ Chí Minh', N'Số lượng sân: Sân 11 Người : 1, Sân 7 Người : 2', N'0907 971971', N'datducstadium@gmail.com')
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [City], [Description], [Phone], [Email]) VALUES (18, N'Sân Huấn Luyện Bay', N'117 Hồng Hà', N'2', N'Tân Bình', N'Hồ Chí Minh', N'Số lượng sân: Sân 7 Người : 1, Sân 11 Người : 2 ', N'0902995527', N'baystadium@gmail.com')
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [City], [Description], [Phone], [Email]) VALUES (19, N'Sân Phường Tây Thạnh', N'55 Dương Đức Hiển', N'Tây Thạnh', N'Tân Phú', N'Hồ Chí Minh', N'Số lượng sân: Sân 11 Người: 1', N'0982 152798', N'taythanhstadium@gmail.com')
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [City], [Description], [Phone], [Email]) VALUES (21, N'Sân CLb 367', N' 367 Hoàng Hoa Thám', N'12', N'Tân Bình', N'Hồ Chí Minh', N'Số lượng sân: Sân 11 Người: 3', N'08 8117244', N'367stadium@gmail.com')
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [City], [Description], [Phone], [Email]) VALUES (22, N'Sân Kỳ Hòa', N'824/28Q Sư Vạn Hạnh (nối dài)', N'12', N'10', N'Hồ Chí Minh', N'Số lượng sân: Sân 11 Người: 1, Sân 7 Người: 4', N'0907 887887 ', N'kyhoastadium@gmail.com')
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [City], [Description], [Phone], [Email]) VALUES (23, N'Sân Bóng Đá Mini Cỏ Nhân Tạo K334', N'Hẻm 119 Trần Văn Dư', N'13', N'Tân Bình', N'Hồ Chí Minh', N'Số lượng sân: Sân 5 Người: 4', N'0989922734', N'k334stadium@gmail.com')
SET IDENTITY_INSERT [dbo].[Stadium] OFF
/****** Object:  Table [dbo].[Role]    Script Date: 01/23/2014 01:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Role] ON
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (2, N'Staff')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (3, N'User')
SET IDENTITY_INSERT [dbo].[Role] OFF
/****** Object:  Table [dbo].[MemberRank]    Script Date: 01/23/2014 01:03:44 ******/
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
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (1, N'User', 0, NULL)
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (2, N'Power User', 100, N'Tặng 1 con Gấu Bông')
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (3, N'Elite User', 150, N'Tặng 1 con Gấu Bông')
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (4, N'Crazy User', 200, N'Tặng 1 con Gấu Bông')
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (5, N'Insane User', 400, N'Tặng 1 trái bóng')
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (6, N'Veteran User', 600, N'Tặng 1 trái bóng')
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (7, N'Extreme User', 1000, N'Tặng 1 trái bóng')
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (8, N'Ultimate User ', 2000, N'Tặng 1 trái bóng')
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (9, N'Master ', 5000, N'Tặng 1 trái bóng')
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (10, N'Vip', 10000, N'Tặng 1 xe hơi')
SET IDENTITY_INSERT [dbo].[MemberRank] OFF
/****** Object:  Table [dbo].[Field]    Script Date: 01/23/2014 01:03:44 ******/
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
 CONSTRAINT [PK_Field] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Field] ON
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType]) VALUES (2, 2, N'1', NULL, 5)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType]) VALUES (3, 2, N'2', NULL, 7)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType]) VALUES (4, 2, N'3', NULL, 7)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType]) VALUES (5, 4, N'1', NULL, 5)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType]) VALUES (6, 4, N'2', NULL, 5)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType]) VALUES (7, 4, N'3', NULL, 5)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType]) VALUES (8, 4, N'4', NULL, 5)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType]) VALUES (9, 4, N'5', NULL, 7)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType]) VALUES (10, 12, N'1', NULL, 5)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType]) VALUES (11, 12, N'2', NULL, 5)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType]) VALUES (12, 12, N'3', NULL, 7)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType]) VALUES (13, 12, N'4', NULL, 7)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType]) VALUES (14, 12, N'5', NULL, 7)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType]) VALUES (15, 12, N'6', NULL, 7)
SET IDENTITY_INSERT [dbo].[Field] OFF
/****** Object:  Table [dbo].[User]    Script Date: 01/23/2014 01:03:44 ******/
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
	[IsReceivedReward] [bit] NOT NULL,
	[JoinDate] [date] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [IsReceivedReward], [JoinDate], [IsActive]) VALUES (7, N'Mr A', N'123', N'Nguyễn Văn A', N'1 Tô Ký', N'0913141516', N'nguyenvana@gmail.com', 100, 2, 1, CAST(0x0F380B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [IsReceivedReward], [JoinDate], [IsActive]) VALUES (9, N'Mr Songoku', N'111', N'Trần Văn B', N'159 Quang Trung', N'0977778899', N'tranvanb@gmail.com', 400, 5, 1, CAST(0x20380B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [IsReceivedReward], [JoinDate], [IsActive]) VALUES (10, N'Mr Mao Chạch Đông', N'456', N'Đỗ Tuấn C', N'30/28/39 Phan Xích Long', N'01689247350', N'dotuanc@yahoo.com', 200, 4, 1, CAST(0x57380B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [IsReceivedReward], [JoinDate], [IsActive]) VALUES (11, N'Mr Thạc Sỉn', N'789', N'Xinh Thị Lung Linh', N'193 Pasteur', N'0988888888', N'batungdeptrai@gmail.com', 10000, 10, 1, CAST(0x01380B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [IsReceivedReward], [JoinDate], [IsActive]) VALUES (12, N'Mr Min Tun', N'222', N'Phan Nam D', N'7731 Lạc Long Quân', N'0913178631', N'phannamd@gmail.com', 100, 2, 1, CAST(0x07380B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [IsReceivedReward], [JoinDate], [IsActive]) VALUES (13, N'Mr Khánh Đẹp Trai', N'345', N'Nguyễn Quốc Khánh', N'225 Hàn Thuyên', N'0935999243', N'khanhnq@gmail.com', 5000, 9, 1, CAST(0x8B380B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [IsReceivedReward], [JoinDate], [IsActive]) VALUES (15, N'Mr Giang Joseph', N'142', N'Giang Thị Giô Dép', N'312 Trần Văn Đang', N'0984235124', N'giangjoseph@loveline.com', 150, 3, 1, CAST(0x78380B00 AS Date), 1)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[StadiumImage]    Script Date: 01/23/2014 01:03:44 ******/
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
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (2, 2, N'a', N'b', N'c')
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (3, 4, N'a', N'b', N'c')
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (6, 8, N'a', N'b', N'c')
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (7, 10, N'a', N'b', N'c')
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (8, 12, N'a', N'b', N'c')
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (9, 14, N'a', N'b', N'c')
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (10, 16, N'a', N'b', N'c')
SET IDENTITY_INSERT [dbo].[StadiumImage] OFF
/****** Object:  Table [dbo].[UserInRole]    Script Date: 01/23/2014 01:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserInRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserInRole] ON
INSERT [dbo].[UserInRole] ([Id], [UserId], [RoleId]) VALUES (1, 11, 1)
INSERT [dbo].[UserInRole] ([Id], [UserId], [RoleId]) VALUES (2, 7, 3)
INSERT [dbo].[UserInRole] ([Id], [UserId], [RoleId]) VALUES (3, 9, 3)
SET IDENTITY_INSERT [dbo].[UserInRole] OFF
/****** Object:  Table [dbo].[StadiumStaff]    Script Date: 01/23/2014 01:03:44 ******/
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
SET IDENTITY_INSERT [dbo].[StadiumStaff] ON
INSERT [dbo].[StadiumStaff] ([Id], [UserId], [StadiumId], [IsOwner]) VALUES (1, 12, 12, 1)
INSERT [dbo].[StadiumStaff] ([Id], [UserId], [StadiumId], [IsOwner]) VALUES (2, 13, 18, 1)
INSERT [dbo].[StadiumStaff] ([Id], [UserId], [StadiumId], [IsOwner]) VALUES (3, 15, 22, 1)
SET IDENTITY_INSERT [dbo].[StadiumStaff] OFF
/****** Object:  Table [dbo].[StadiumReview]    Script Date: 01/23/2014 01:03:44 ******/
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
 CONSTRAINT [PK_StadiumReview] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[StadiumReview] ON
INSERT [dbo].[StadiumReview] ([Id], [UserId], [StadiumId], [ReviewContent], [IsApproved]) VALUES (2, 7, 2, N'Good', 1)
INSERT [dbo].[StadiumReview] ([Id], [UserId], [StadiumId], [ReviewContent], [IsApproved]) VALUES (3, 9, 8, N'Bad', 1)
INSERT [dbo].[StadiumReview] ([Id], [UserId], [StadiumId], [ReviewContent], [IsApproved]) VALUES (4, 10, 12, N'Not Bad', 0)
SET IDENTITY_INSERT [dbo].[StadiumReview] OFF
/****** Object:  Table [dbo].[StadiumRating]    Script Date: 01/23/2014 01:03:44 ******/
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
SET IDENTITY_INSERT [dbo].[StadiumRating] ON
INSERT [dbo].[StadiumRating] ([Id], [UserId], [StadiumId], [Rate]) VALUES (2, 7, 2, 1)
INSERT [dbo].[StadiumRating] ([Id], [UserId], [StadiumId], [Rate]) VALUES (3, 9, 4, 5)
INSERT [dbo].[StadiumRating] ([Id], [UserId], [StadiumId], [Rate]) VALUES (4, 10, 8, 10)
SET IDENTITY_INSERT [dbo].[StadiumRating] OFF
/****** Object:  Table [dbo].[Reservation]    Script Date: 01/23/2014 01:03:44 ******/
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
	[VerifyCode] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Approver] [int] NULL,
	[Status] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Reservation] ON
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [VerifyCode], [CreatedDate], [Approver], [Status]) VALUES (9, 3, 7, N'Nguyễn Văn A', N'0913141516', N'nguyenvana@gmail.com', CAST(0x10380B00 AS Date), 15, 2, 300000, N'qwerty', CAST(0x0000A2B500000000 AS DateTime), NULL, N'New')
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [VerifyCode], [CreatedDate], [Approver], [Status]) VALUES (13, 4, 9, N'Trần Văn B', N'0977778899', N'tranvanb@gmail.com', CAST(0x21380B00 AS Date), 18, 2, 300000, N'zxcvbn', CAST(0x0000A2C600000000 AS DateTime), NULL, N'New')
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [VerifyCode], [CreatedDate], [Approver], [Status]) VALUES (14, 5, 10, N'Đỗ Tuấn C', N'01689247350', N'dotuanc@yahoo.com', CAST(0x58380B00 AS Date), 16, 1, 100000, N'asdfgh', CAST(0x0000A2FD00000000 AS DateTime), NULL, N'New')
SET IDENTITY_INSERT [dbo].[Reservation] OFF
/****** Object:  Table [dbo].[Promotion]    Script Date: 01/23/2014 01:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[Id] [int] NOT NULL,
	[FieldId] [int] NOT NULL,
	[PromotionFrom] [date] NOT NULL,
	[PromotionTo] [date] NOT NULL,
	[Discount] [float] NOT NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount]) VALUES (1, 2, CAST(0x57380B00 AS Date), CAST(0x76380B00 AS Date), 10)
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount]) VALUES (2, 3, CAST(0x57380B00 AS Date), CAST(0x76380B00 AS Date), 10)
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount]) VALUES (3, 5, CAST(0x20380B00 AS Date), CAST(0x3C380B00 AS Date), 10)
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount]) VALUES (4, 6, CAST(0x20380B00 AS Date), CAST(0x3C380B00 AS Date), 10)
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount]) VALUES (5, 7, CAST(0x20380B00 AS Date), CAST(0x3C380B00 AS Date), 10)
/****** Object:  Table [dbo].[JoinSystemRequest]    Script Date: 01/23/2014 01:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JoinSystemRequest](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[StadiumName] [nvarchar](100) NOT NULL,
	[StadiumAddress] [nvarchar](100) NOT NULL,
	[Note] [nvarchar](1000) NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_JoinSystemRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[JoinSystemRequest] ([Id], [UserId], [FullName], [Address], [Phone], [Email], [StadiumName], [StadiumAddress], [Note], [Status]) VALUES (1, 12, N'Phan Nam D', N'7731 Lạc Long Quân', N'0913178631', N'phannamd@gmail.com', N'Sân Thép Miền Nam Cảng Sài Gòn', N'KP4 đường Tân Mỹ', NULL, N'New')
INSERT [dbo].[JoinSystemRequest] ([Id], [UserId], [FullName], [Address], [Phone], [Email], [StadiumName], [StadiumAddress], [Note], [Status]) VALUES (2, 13, N'Nguyễn Quốc Khánh', N'225 Hàn Thuyên', N'0935999243', N'khanhnq@gmail.com', N'Sân Huấn Luyện Bay', N'117 Hồng Hà', NULL, N'New')
INSERT [dbo].[JoinSystemRequest] ([Id], [UserId], [FullName], [Address], [Phone], [Email], [StadiumName], [StadiumAddress], [Note], [Status]) VALUES (3, 15, N'Giang Thị Giô Dép', N'312 Trần Văn Đang', N'0984235124', N'giangjoseph@loveline.com', N'Sân Kỳ Hòa', N'824/28Q Sư Vạn Hạnh (nối dài)', NULL, N'New')
/****** Object:  Table [dbo].[FieldPrice]    Script Date: 01/23/2014 01:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldPrice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FieldId] [int] NOT NULL,
	[TimeFrom] [float] NOT NULL,
	[TimeTo] [float] NOT NULL,
	[Price] [float] NOT NULL,
	[Day] [int] NOT NULL,
 CONSTRAINT [PK_FieldPrice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[FieldPrice] ON
INSERT [dbo].[FieldPrice] ([Id], [FieldId], [TimeFrom], [TimeTo], [Price], [Day]) VALUES (1, 3, 0, 6, 150000, 0)
INSERT [dbo].[FieldPrice] ([Id], [FieldId], [TimeFrom], [TimeTo], [Price], [Day]) VALUES (2, 3, 6, 10, 200000, 0)
INSERT [dbo].[FieldPrice] ([Id], [FieldId], [TimeFrom], [TimeTo], [Price], [Day]) VALUES (3, 3, 10, 15, 180000, 0)
INSERT [dbo].[FieldPrice] ([Id], [FieldId], [TimeFrom], [TimeTo], [Price], [Day]) VALUES (4, 3, 15, 18, 220000, 0)
INSERT [dbo].[FieldPrice] ([Id], [FieldId], [TimeFrom], [TimeTo], [Price], [Day]) VALUES (5, 3, 18, 24, 250000, 0)
SET IDENTITY_INSERT [dbo].[FieldPrice] OFF
/****** Object:  Table [dbo].[Challenge]    Script Date: 01/23/2014 01:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Challenge](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Host] [int] NOT NULL,
	[HostFullName] [nvarchar](50) NOT NULL,
	[HostEmail] [nvarchar](100) NULL,
	[HostPhone] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Status] [nvarchar](10) NOT NULL,
	[Rival] [int] NULL,
	[RivalFullName] [nvarchar](50) NOT NULL,
	[RivalEmail] [nvarchar](100) NULL,
	[RivalPhone] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Challenge] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_User_Point]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Point]  DEFAULT ((0)) FOR [Point]
GO
/****** Object:  Default [DF_User_IsActive]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  ForeignKey [FK_Field_Field]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_Field] FOREIGN KEY([ParentField])
REFERENCES [dbo].[Field] ([Id])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_Field]
GO
/****** Object:  ForeignKey [FK_Field_Stadium]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_Stadium]
GO
/****** Object:  ForeignKey [FK_User_MemberRank]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_MemberRank] FOREIGN KEY([RankId])
REFERENCES [dbo].[MemberRank] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_MemberRank]
GO
/****** Object:  ForeignKey [FK_StadiumImage_Stadium]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[StadiumImage]  WITH CHECK ADD  CONSTRAINT [FK_StadiumImage_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumImage] CHECK CONSTRAINT [FK_StadiumImage_Stadium]
GO
/****** Object:  ForeignKey [FK_UserInRole_Role]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[UserInRole]  WITH CHECK ADD  CONSTRAINT [FK_UserInRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[UserInRole] CHECK CONSTRAINT [FK_UserInRole_Role]
GO
/****** Object:  ForeignKey [FK_UserInRole_User]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[UserInRole]  WITH CHECK ADD  CONSTRAINT [FK_UserInRole_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserInRole] CHECK CONSTRAINT [FK_UserInRole_User]
GO
/****** Object:  ForeignKey [FK_StadiumOwner_Stadium]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[StadiumStaff]  WITH CHECK ADD  CONSTRAINT [FK_StadiumOwner_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumStaff] CHECK CONSTRAINT [FK_StadiumOwner_Stadium]
GO
/****** Object:  ForeignKey [FK_StadiumOwner_User]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[StadiumStaff]  WITH CHECK ADD  CONSTRAINT [FK_StadiumOwner_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StadiumStaff] CHECK CONSTRAINT [FK_StadiumOwner_User]
GO
/****** Object:  ForeignKey [FK_StadiumReview_Stadium]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[StadiumReview]  WITH CHECK ADD  CONSTRAINT [FK_StadiumReview_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumReview] CHECK CONSTRAINT [FK_StadiumReview_Stadium]
GO
/****** Object:  ForeignKey [FK_StadiumReview_User]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[StadiumReview]  WITH CHECK ADD  CONSTRAINT [FK_StadiumReview_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StadiumReview] CHECK CONSTRAINT [FK_StadiumReview_User]
GO
/****** Object:  ForeignKey [FK_StadiumRating_Stadium]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[StadiumRating]  WITH CHECK ADD  CONSTRAINT [FK_StadiumRating_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumRating] CHECK CONSTRAINT [FK_StadiumRating_Stadium]
GO
/****** Object:  ForeignKey [FK_StadiumRating_User]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[StadiumRating]  WITH CHECK ADD  CONSTRAINT [FK_StadiumRating_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StadiumRating] CHECK CONSTRAINT [FK_StadiumRating_User]
GO
/****** Object:  ForeignKey [FK_Reservation_Field]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Field] FOREIGN KEY([FieldId])
REFERENCES [dbo].[Field] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Field]
GO
/****** Object:  ForeignKey [FK_Reservation_User]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User]
GO
/****** Object:  ForeignKey [FK_Reservation_User1]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User1] FOREIGN KEY([Approver])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User1]
GO
/****** Object:  ForeignKey [FK_Promotion_Field]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [FK_Promotion_Field] FOREIGN KEY([FieldId])
REFERENCES [dbo].[Field] ([Id])
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [FK_Promotion_Field]
GO
/****** Object:  ForeignKey [FK_JoinSystemRequest_User]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[JoinSystemRequest]  WITH CHECK ADD  CONSTRAINT [FK_JoinSystemRequest_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[JoinSystemRequest] CHECK CONSTRAINT [FK_JoinSystemRequest_User]
GO
/****** Object:  ForeignKey [FK_FieldPrice_Field]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[FieldPrice]  WITH CHECK ADD  CONSTRAINT [FK_FieldPrice_Field] FOREIGN KEY([FieldId])
REFERENCES [dbo].[Field] ([Id])
GO
ALTER TABLE [dbo].[FieldPrice] CHECK CONSTRAINT [FK_FieldPrice_Field]
GO
/****** Object:  ForeignKey [FK_Challenge_User]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[Challenge]  WITH CHECK ADD  CONSTRAINT [FK_Challenge_User] FOREIGN KEY([Host])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Challenge] CHECK CONSTRAINT [FK_Challenge_User]
GO
/****** Object:  ForeignKey [FK_Challenge_User1]    Script Date: 01/23/2014 01:03:44 ******/
ALTER TABLE [dbo].[Challenge]  WITH CHECK ADD  CONSTRAINT [FK_Challenge_User1] FOREIGN KEY([Rival])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Challenge] CHECK CONSTRAINT [FK_Challenge_User1]
GO
