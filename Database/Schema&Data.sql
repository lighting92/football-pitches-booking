USE [master]
GO
/****** Object:  Database [FootballPitchesBooking]    Script Date: 04/09/2014 13:14:47 ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 04/09/2014 13:14:47 ******/
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
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (1, N'WebsiteMaster', NULL)
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (2, N'WebsiteStaff', NULL)
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (3, N'StadiumOwner', NULL)
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (4, N'StadiumStaff', NULL)
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (5, N'Member', NULL)
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (6, N'BannedMember', NULL)
SET IDENTITY_INSERT [dbo].[Role] OFF
/****** Object:  Table [dbo].[Configuration]    Script Date: 04/09/2014 13:14:47 ******/
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
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (5, N'Nr_MostBooked', N'0')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (6, N'Nr_Nearest', N'100')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (7, N'Nr_MostPromoted', N'15')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (8, N'Pr_MostBooked', N'100')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (9, N'Pr_Nearest', N'0')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (10, N'Pr_MostPromoted', N'0')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (11, N'Mb_MostBooked', N'70')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (12, N'Mb_Nearest', N'15')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (13, N'Mb_MostPromoted', N'15')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (14, N'Rv_MostBooked', N'10')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (15, N'Rv_Periodic', N'15')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (16, N'Rv_MostPromoted', N'75')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (17, N'Gg_Periodic', N'3')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (17, N'Cfg_Email', N'footballpitchesbooking@gmail.com')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (17, N'Cfg_Password', N'contactEmail')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (17, N'Cfg_SmtpHost', N'smtp.gmail.com')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (17, N'Cfg_SmtpPort', N'587')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (17, N'Cfg_EnableSSL', N'True')
SET IDENTITY_INSERT [dbo].[Configuration] OFF
/****** Object:  Table [dbo].[MemberRank]    Script Date: 04/09/2014 13:14:47 ******/
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
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (1, N'Legend', 1000, N'Legend')
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (2, N'Beginner', 0, N'Beginner')
SET IDENTITY_INSERT [dbo].[MemberRank] OFF
/****** Object:  Table [dbo].[User]    Script Date: 04/09/2014 13:14:47 ******/
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
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (1, N'admin', N'123456', N'Admin', N'Admin', N'0111222333', N'admin@fpb.com', 1000, 1, CAST(0x38380B00 AS Date), 1, 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (2, N'phuongnd', N'123456', N'Nguyễn Duy Phương', N'1 Tô Ký, Đông Hưng Thuận, Quận 12, Hồ Chí Minh', N'01662194419', N'phuongnd@gm.com', 0, 2, CAST(0x3E380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (3, N'tester01', N'123456', N'Nguyễn Tét Tơ', N'15 PVD, Quận 1, TP.Hồ Chí Minh', N'0123456789', N'tester01@gmail.com', 0, 2, CAST(0x46380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (5, N'khanhnq', N'123456', N'aaaaaaa', N'11113ddd', N'123456789', N'aaaa@acc.com', 0, 2, CAST(0x01380B00 AS Date), 1, 5)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (6, N'aaaaaaa', N'123456', N'zczcxzxczxc', N'123 Ham Nghi, Quận 1, TP.Hồ Chí Minh', N'1234567890', N'abc@abc.cba', 0, 2, CAST(0x50380B00 AS Date), 1, 5)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (7, N'bbbbbbbbb', N'123456', N'rtyuiolkjhg', N'1111 phung, Quận 1, TP.Hồ Chí Minh', N'1234567890', N'sdfg@dfgh.vbc', 0, 2, CAST(0x50380B00 AS Date), 1, 5)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[Stadium]    Script Date: 04/09/2014 13:14:47 ******/
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
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (1, N'Phương Đô', N'18C Cộng Hòa', N'12', N'Q. Tân Bình', N'0123456789', N'PD@PD.com', 1, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (2, N'Chảo Lửa', N'18C Cộng Hòa', N'12', N'Q. Tân Bình', N'0987765431', N'CL@CL.com', 1, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (8, N'Phương Nam', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 1, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (9, N'Phương Nam 2', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 1, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (10, N'Phương Nam 3', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 1, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (11, N'Phương Nam 4', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 1, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (12, N'Phương Nam 5', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 1, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (13, N'Phương Nam 6', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 0, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (14, N'K334', N'69/96 Hoàng Hoa Thám', N'11', N'Q. Tân Bình', N'01662194419', N'k334@sanbong.com', 1, 2, 6, 4, CAST(0xB8390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (15, N'Sân Thống Nhất', N'33/3 A Thống Nhất', N'P. 10', N'Q. Gò Vấp', N'0135792468', N'thongnhat@sanbong.com', 1, 2, 0, 0, CAST(0x233A0B00 AS Date))
SET IDENTITY_INSERT [dbo].[Stadium] OFF
/****** Object:  Table [dbo].[UserDistance]    Script Date: 04/09/2014 13:14:47 ******/
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
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (1, 2, N'phuongnd.xml', CAST(0x61380B00 AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (2, 3, N'tester01.xml', CAST(0x61380B00 AS Date))
SET IDENTITY_INSERT [dbo].[UserDistance] OFF
/****** Object:  Table [dbo].[JoinSystemRequest]    Script Date: 04/09/2014 13:14:47 ******/
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
/****** Object:  Table [dbo].[ReportUser]    Script Date: 04/09/2014 13:14:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ReportUserId] [int] NOT NULL,
	[Reason] [nvarchar](50) NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_ReportUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PunishMember]    Script Date: 04/09/2014 13:14:47 ******/
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
/****** Object:  Table [dbo].[Advertisement]    Script Date: 04/09/2014 13:14:47 ******/
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
/****** Object:  Table [dbo].[StadiumStaff]    Script Date: 04/09/2014 13:14:47 ******/
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
INSERT [dbo].[StadiumStaff] ([Id], [UserId], [StadiumId], [IsOwner]) VALUES (10, 3, 1, 1)
SET IDENTITY_INSERT [dbo].[StadiumStaff] OFF
/****** Object:  Table [dbo].[StadiumReview]    Script Date: 04/09/2014 13:14:47 ******/
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
/****** Object:  Table [dbo].[StadiumRating]    Script Date: 04/09/2014 13:14:47 ******/
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
INSERT [dbo].[StadiumRating] ([Id], [UserId], [StadiumId], [Rate]) VALUES (1, 2, 1, 6)
INSERT [dbo].[StadiumRating] ([Id], [UserId], [StadiumId], [Rate]) VALUES (2, 3, 1, 6)
SET IDENTITY_INSERT [dbo].[StadiumRating] OFF
/****** Object:  Table [dbo].[StadiumImage]    Script Date: 04/09/2014 13:14:47 ******/
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
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (1, 8, N'8_0_635300061614319336.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (2, 8, N'8_1_635300061614329336.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (3, 9, N'9_0_635300065671891415.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (9, 11, N'11_0_635300105703021065.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (10, 11, N'11_1_635300105703031066.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (11, 12, N'12_0_635300109301936911.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (12, 12, N'12_1_635300109301946912.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (13, 13, N'13_0_635300109709160203.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (15, 13, N'13_0_635300110649924012.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (17, 13, N'13_2_635300110649934012.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (18, 13, N'13_0_635300115255107413.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (19, 13, N'13_1_635300115255117414.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (20, 13, N'13_2_635300115255117414.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (21, 1, N'1_0_635301249138256981.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (23, 1, N'1_2_635301249138266981.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (25, 14, N'14_1_635303881359946753.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (26, 15, N'15_0_635310015741512954.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (27, 15, N'15_1_635310015741522954.jpg', NULL, NULL)
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (28, 15, N'15_2_635310015741522954.jpg', NULL, NULL)
SET IDENTITY_INSERT [dbo].[StadiumImage] OFF
/****** Object:  Table [dbo].[Notification]    Script Date: 04/09/2014 13:14:47 ******/
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
SET IDENTITY_INSERT [dbo].[Notification] ON
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (0, 2, NULL, N'Bạn nhận yêu cầu giao hữu từ [phuongnd] <a href=''/Account/EditReservation?Id=16''>Chi tiết</a>', CAST(0x0000A30200C0220E AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (2, 2, NULL, N'Bạn nhận yêu cầu giao hữu từ [phuongnd] <a href=''/Account/EditReservation?Id=17''>Chi tiết</a>', CAST(0x0000A30200D9AABF AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (3, 2, NULL, N'[phuongnd] không đồng ý giao hữu với bạn', CAST(0x0000A30200D9CABA AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (4, 2, NULL, N'Bạn nhận yêu cầu giao hữu từ [phuongnd] <a href=''/Account/EditReservation?Id=17''>Chi tiết</a>', CAST(0x0000A30200D9DE1B AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (5, 2, NULL, N'[phuongnd] đồng ý giao hữu với bạn', CAST(0x0000A30200D9E64E AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (6, 2, NULL, N'Bạn nhận yêu cầu giao hữu từ [phuongnd] <a href=''/Account/EditReservation?Id=18''>Chi tiết</a>', CAST(0x0000A30200DA1836 AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (7, 2, NULL, N'[phuongnd] không mời giao hữu nữa', CAST(0x0000A30200DA3771 AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (8, 2, NULL, N'Bạn nhận yêu cầu giao hữu từ [tester01] <a href=''/Account/EditReservation?Id=19''>Chi tiết</a>', CAST(0x0000A30200EECE20 AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (9, 3, NULL, N'[phuongnd] đồng ý giao hữu với bạn', CAST(0x0000A30200EF6DC2 AS DateTime), N'Unread')
SET IDENTITY_INSERT [dbo].[Notification] OFF
/****** Object:  Table [dbo].[FieldPrice]    Script Date: 04/09/2014 13:14:47 ******/
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
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (1, 14, N'Sân 5 người', N'bảng giá sân 5 người cho hệ thống k334')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (4, 14, N'Sân 11 người', N'Bảng giá cho sân 11 người hệ thống sân bóng k334')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (5, 11, N'1', N'123')
SET IDENTITY_INSERT [dbo].[FieldPrice] OFF
/****** Object:  Table [dbo].[Field]    Script Date: 04/09/2014 13:14:47 ******/
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
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (5, 14, N'1', NULL, 5, 1, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (6, 14, N'2', NULL, 7, 1, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (7, 14, N'2A', 6, 5, 1, 4)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (8, 14, N'2B', 6, 5, 1, 4)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (9, 14, N'3', NULL, 11, 0, 4)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (10, 14, N'3A', 9, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (11, 14, N'3B', 9, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (12, 14, N'3C', 9, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (13, 14, N'3D', 9, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (14, 14, N'3E', 9, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (15, 14, N'3F', 9, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (16, 14, N'3G', 9, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (17, 14, N'3H', 9, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (18, 14, N'4', NULL, 11, 1, 4)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (19, 14, N'4A', 18, 7, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (20, 14, N'4B', 18, 7, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (21, 14, N'4C', 18, 7, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (22, 14, N'4D', 18, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (23, 14, N'4E', 18, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (24, 14, N'4AA', 19, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (25, 14, N'4AB', 19, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (26, 14, N'4BA', 20, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (27, 14, N'4BB', 20, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (28, 14, N'4CA', 21, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (29, 14, N'4CB', 21, 5, 0, 1)
INSERT [dbo].[Field] ([Id], [StadiumId], [Number], [ParentField], [FieldType], [IsActive], [PriceId]) VALUES (30, 11, N'1', NULL, 5, 1, 5)
SET IDENTITY_INSERT [dbo].[Field] OFF
/****** Object:  Table [dbo].[PriceTable]    Script Date: 04/09/2014 13:14:47 ******/
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
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (1, 1, 0, 0, 0, 100000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (2, 1, 0, 16, 18, 200000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (3, 1, 0, 18, 22, 250000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (4, 1, 1, 14, 16, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (5, 1, 2, 14, 16, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (6, 1, 3, 14, 16, 150000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (7, 1, 4, 14, 16, 200000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (8, 1, 5, 18, 22, 300000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (9, 1, 6, 6, 10, 200000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (10, 1, 6, 14, 16, 250000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (11, 1, 6, 16, 18, 280000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (12, 1, 6, 18, 22, 300000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (13, 1, 7, 0, 0, 250000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (62, 4, 0, 0, 0, 100000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (63, 4, 0, 2, 5, 200000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (64, 4, 2, 0, 0, 100000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (65, 4, 3, 3, 5, 300000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (66, 4, 4, 0, 0, 300000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (67, 4, 5, 4, 8, 200000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (68, 4, 6, 6, 7, 200000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (69, 4, 7, 0, 0, 300000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (70, 4, 7, 11, 13, 250000)
INSERT [dbo].[PriceTable] ([Id], [FieldPriceId], [Day], [TimeFrom], [TimeTo], [Price]) VALUES (71, 5, 0, 0, 0, 150000)
SET IDENTITY_INSERT [dbo].[PriceTable] OFF
/****** Object:  Table [dbo].[Promotion]    Script Date: 04/09/2014 13:14:47 ******/
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
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount], [Creator], [IsActive]) VALUES (1, 30, CAST(0x5B380B00 AS Date), CAST(0x69380B00 AS Date), 30, 2, 1)
SET IDENTITY_INSERT [dbo].[Promotion] OFF
/****** Object:  Table [dbo].[Reservation]    Script Date: 04/09/2014 13:14:47 ******/
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
	[PromotionId] [int] NULL,
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
SET IDENTITY_INSERT [dbo].[Reservation] ON
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (2, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x54380B00 AS Date), 15, 1, 150000, NULL, NULL, N'2635313463069959020', CAST(0x0000A2F900C8FD83 AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (3, 7, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x54380B00 AS Date), 15, 1, 100000, NULL, NULL, N'2635313464208494140', CAST(0x0000A2F900C982EF AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (4, 8, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x54380B00 AS Date), 15, 1, 100000, NULL, NULL, N'2635313464518961898', CAST(0x0000A2F900C9A751 AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (5, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x54380B00 AS Date), 17, 1, 200000, NULL, NULL, N'2635313466573779427', CAST(0x0000A2F900CA981D AS DateTime), NULL, N'Pending', 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (6, 7, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x54380B00 AS Date), 17, 1, 100000, NULL, NULL, N'2635313467111610189', CAST(0x0000A2F900CAD724 AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (7, 8, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x54380B00 AS Date), 17, 1, 100000, NULL, NULL, N'2635313467291340469', CAST(0x0000A2F900CAEC34 AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (8, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x54380B00 AS Date), 14, 1, 150000, NULL, NULL, N'2635313467996960828', CAST(0x0000A2F900CB3EE5 AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (9, 7, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x54380B00 AS Date), 14, 1, 100000, NULL, NULL, N'2635313468052414000', CAST(0x0000A2F900CB4564 AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (10, 8, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x54380B00 AS Date), 14, 1, 100000, NULL, NULL, N'2635313468097736592', CAST(0x0000A2F900CB4AB4 AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (11, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x54380B00 AS Date), 19, 1, 550000, NULL, NULL, N'2635313471408335947', CAST(0x0000A2F900CCCEAA AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (12, 7, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x54380B00 AS Date), 19, 1, 100000, NULL, NULL, N'2635313471456828721', CAST(0x0000A2F900CCD459 AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (13, 8, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x54380B00 AS Date), 19, 1, 100000, NULL, NULL, N'2635313471498981132', CAST(0x0000A2F900CCD949 AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (14, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x5F380B00 AS Date), 2, 1, 100000, NULL, NULL, N'2635320811241598979', CAST(0x0000A30200052560 AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (15, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x5E380B00 AS Date), 2, 1, 100000, NULL, NULL, N'2635320814659944497', CAST(0x0000A3020006B5F6 AS DateTime), NULL, N'Canceled', 1, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', NULL, N'Approve')
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (16, 7, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x5E380B00 AS Date), 2, 1, 400000, NULL, NULL, N'2635320824514858166', CAST(0x0000A302000B38D6 AS DateTime), NULL, N'Canceled', 1, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', NULL, N'Pending')
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (17, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x5E380B00 AS Date), 2, 1, 100000, NULL, NULL, N'2635321271620709407', CAST(0x0000A30200D7E40D AS DateTime), NULL, N'Pending', 1, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', NULL, N'Approve')
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (18, 7, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x5E380B00 AS Date), 2, 1, 400000, NULL, NULL, N'2635321276237503473', CAST(0x0000A30200DA0115 AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (19, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(0x5E380B00 AS Date), 18, 1, 300000, NULL, NULL, N'2635321319212801185', CAST(0x0000A30200EDAD40 AS DateTime), NULL, N'Pending', 1, 3, N'Nguyễn Tét Tơ', N'0123456789', N'tester01@gmail.com', NULL, N'Approve')
SET IDENTITY_INSERT [dbo].[Reservation] OFF
/****** Object:  Default [DF_User_Point]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Point]  DEFAULT ((0)) FOR [Point]
GO
/****** Object:  Default [DF_User_IsActive]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  ForeignKey [FK_User_MemberRank]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_MemberRank] FOREIGN KEY([RankId])
REFERENCES [dbo].[MemberRank] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_MemberRank]
GO
/****** Object:  ForeignKey [FK_User_Role]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
/****** Object:  ForeignKey [FK_Stadium_User]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[Stadium]  WITH CHECK ADD  CONSTRAINT [FK_Stadium_User] FOREIGN KEY([MainOwner])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Stadium] CHECK CONSTRAINT [FK_Stadium_User]
GO
/****** Object:  ForeignKey [FK_UserDistance_User]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[UserDistance]  WITH CHECK ADD  CONSTRAINT [FK_UserDistance_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserDistance] CHECK CONSTRAINT [FK_UserDistance_User]
GO
/****** Object:  ForeignKey [FK_JoinSystemRequest_User]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[JoinSystemRequest]  WITH CHECK ADD  CONSTRAINT [FK_JoinSystemRequest_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[JoinSystemRequest] CHECK CONSTRAINT [FK_JoinSystemRequest_User]
GO
/****** Object:  ForeignKey [FK_ReportUser_User]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[ReportUser]  WITH CHECK ADD  CONSTRAINT [FK_ReportUser_User] FOREIGN KEY([ReportUserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ReportUser] CHECK CONSTRAINT [FK_ReportUser_User]
GO
/****** Object:  ForeignKey [FK_ReportUser_User1]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[ReportUser]  WITH CHECK ADD  CONSTRAINT [FK_ReportUser_User1] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ReportUser] CHECK CONSTRAINT [FK_ReportUser_User1]
GO
/****** Object:  ForeignKey [FK_PunishMember_User]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[PunishMember]  WITH CHECK ADD  CONSTRAINT [FK_PunishMember_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[PunishMember] CHECK CONSTRAINT [FK_PunishMember_User]
GO
/****** Object:  ForeignKey [FK_PunishMember_User1]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[PunishMember]  WITH CHECK ADD  CONSTRAINT [FK_PunishMember_User1] FOREIGN KEY([StaffId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[PunishMember] CHECK CONSTRAINT [FK_PunishMember_User1]
GO
/****** Object:  ForeignKey [FK_Advertisement_User]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[Advertisement]  WITH CHECK ADD  CONSTRAINT [FK_Advertisement_User] FOREIGN KEY([Creator])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Advertisement] CHECK CONSTRAINT [FK_Advertisement_User]
GO
/****** Object:  ForeignKey [FK_StadiumOwner_Stadium]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[StadiumStaff]  WITH CHECK ADD  CONSTRAINT [FK_StadiumOwner_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumStaff] CHECK CONSTRAINT [FK_StadiumOwner_Stadium]
GO
/****** Object:  ForeignKey [FK_StadiumOwner_User]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[StadiumStaff]  WITH CHECK ADD  CONSTRAINT [FK_StadiumOwner_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StadiumStaff] CHECK CONSTRAINT [FK_StadiumOwner_User]
GO
/****** Object:  ForeignKey [FK_StadiumReview_Stadium]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[StadiumReview]  WITH CHECK ADD  CONSTRAINT [FK_StadiumReview_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumReview] CHECK CONSTRAINT [FK_StadiumReview_Stadium]
GO
/****** Object:  ForeignKey [FK_StadiumReview_User]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[StadiumReview]  WITH CHECK ADD  CONSTRAINT [FK_StadiumReview_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StadiumReview] CHECK CONSTRAINT [FK_StadiumReview_User]
GO
/****** Object:  ForeignKey [FK_StadiumReview_User1]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[StadiumReview]  WITH CHECK ADD  CONSTRAINT [FK_StadiumReview_User1] FOREIGN KEY([Approver])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StadiumReview] CHECK CONSTRAINT [FK_StadiumReview_User1]
GO
/****** Object:  ForeignKey [FK_StadiumRating_Stadium]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[StadiumRating]  WITH CHECK ADD  CONSTRAINT [FK_StadiumRating_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumRating] CHECK CONSTRAINT [FK_StadiumRating_Stadium]
GO
/****** Object:  ForeignKey [FK_StadiumRating_User]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[StadiumRating]  WITH CHECK ADD  CONSTRAINT [FK_StadiumRating_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StadiumRating] CHECK CONSTRAINT [FK_StadiumRating_User]
GO
/****** Object:  ForeignKey [FK_StadiumImage_Stadium]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[StadiumImage]  WITH CHECK ADD  CONSTRAINT [FK_StadiumImage_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumImage] CHECK CONSTRAINT [FK_StadiumImage_Stadium]
GO
/****** Object:  ForeignKey [FK_Notification_Stadium]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Stadium]
GO
/****** Object:  ForeignKey [FK_Notification_User]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_User]
GO
/****** Object:  ForeignKey [FK_FieldPrice_Stadium]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[FieldPrice]  WITH CHECK ADD  CONSTRAINT [FK_FieldPrice_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[FieldPrice] CHECK CONSTRAINT [FK_FieldPrice_Stadium]
GO
/****** Object:  ForeignKey [FK_Field_Field]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_Field] FOREIGN KEY([ParentField])
REFERENCES [dbo].[Field] ([Id])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_Field]
GO
/****** Object:  ForeignKey [FK_Field_FieldPrice]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_FieldPrice] FOREIGN KEY([PriceId])
REFERENCES [dbo].[FieldPrice] ([Id])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_FieldPrice]
GO
/****** Object:  ForeignKey [FK_Field_Stadium]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_Stadium]
GO
/****** Object:  ForeignKey [FK_PriceTable_FieldPrice]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[PriceTable]  WITH CHECK ADD  CONSTRAINT [FK_PriceTable_FieldPrice] FOREIGN KEY([FieldPriceId])
REFERENCES [dbo].[FieldPrice] ([Id])
GO
ALTER TABLE [dbo].[PriceTable] CHECK CONSTRAINT [FK_PriceTable_FieldPrice]
GO
/****** Object:  ForeignKey [FK_Promotion_Field]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [FK_Promotion_Field] FOREIGN KEY([FieldId])
REFERENCES [dbo].[Field] ([Id])
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [FK_Promotion_Field]
GO
/****** Object:  ForeignKey [FK_Reservation_Field]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Field] FOREIGN KEY([FieldId])
REFERENCES [dbo].[Field] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Field]
GO
/****** Object:  ForeignKey [FK_Reservation_Promotion]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Promotion] FOREIGN KEY([PromotionId])
REFERENCES [dbo].[Promotion] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Promotion]
GO
/****** Object:  ForeignKey [FK_Reservation_User]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User]
GO
/****** Object:  ForeignKey [FK_Reservation_User1]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User1] FOREIGN KEY([Approver])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User1]
GO
/****** Object:  ForeignKey [FK_Reservation_User2]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User2] FOREIGN KEY([RivalId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User2]
GO
/****** Object:  ForeignKey [FK_Reservation_User3]    Script Date: 04/09/2014 13:14:47 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User3] FOREIGN KEY([RivalFinder])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User3]
GO
