USE [master]
GO
/****** Object:  Database [FootballPitchesBooking]    Script Date: 04/16/2014 17:43:16 ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[Configuration]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[MemberRank]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[Stadium]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[UserDistance]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[PunishMember]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[JoinSystemRequest]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[Advertisement]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[StadiumStaff]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[StadiumReview]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[StadiumRating]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[StadiumImage]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[Notification]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[FieldPrice]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[Field]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[PriceTable]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[Promotion]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[Reservation]    Script Date: 04/16/2014 17:43:18 ******/
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
/****** Object:  Table [dbo].[ReportUser]    Script Date: 04/16/2014 17:43:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ReportUserId] [int] NOT NULL,
	[Reason] [nvarchar](50) NOT NULL,
	[Reference] [int] NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_ReportUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_User_IsActive]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  ForeignKey [FK_User_MemberRank]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_MemberRank] FOREIGN KEY([RankId])
REFERENCES [dbo].[MemberRank] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_MemberRank]
GO
/****** Object:  ForeignKey [FK_User_Role]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
/****** Object:  ForeignKey [FK_Stadium_User]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[Stadium]  WITH CHECK ADD  CONSTRAINT [FK_Stadium_User] FOREIGN KEY([MainOwner])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Stadium] CHECK CONSTRAINT [FK_Stadium_User]
GO
/****** Object:  ForeignKey [FK_UserDistance_User]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[UserDistance]  WITH CHECK ADD  CONSTRAINT [FK_UserDistance_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserDistance] CHECK CONSTRAINT [FK_UserDistance_User]
GO
/****** Object:  ForeignKey [FK_PunishMember_User]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[PunishMember]  WITH CHECK ADD  CONSTRAINT [FK_PunishMember_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[PunishMember] CHECK CONSTRAINT [FK_PunishMember_User]
GO
/****** Object:  ForeignKey [FK_PunishMember_User1]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[PunishMember]  WITH CHECK ADD  CONSTRAINT [FK_PunishMember_User1] FOREIGN KEY([StaffId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[PunishMember] CHECK CONSTRAINT [FK_PunishMember_User1]
GO
/****** Object:  ForeignKey [FK_JoinSystemRequest_User]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[JoinSystemRequest]  WITH CHECK ADD  CONSTRAINT [FK_JoinSystemRequest_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[JoinSystemRequest] CHECK CONSTRAINT [FK_JoinSystemRequest_User]
GO
/****** Object:  ForeignKey [FK_Advertisement_User]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[Advertisement]  WITH CHECK ADD  CONSTRAINT [FK_Advertisement_User] FOREIGN KEY([Creator])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Advertisement] CHECK CONSTRAINT [FK_Advertisement_User]
GO
/****** Object:  ForeignKey [FK_StadiumOwner_Stadium]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[StadiumStaff]  WITH CHECK ADD  CONSTRAINT [FK_StadiumOwner_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumStaff] CHECK CONSTRAINT [FK_StadiumOwner_Stadium]
GO
/****** Object:  ForeignKey [FK_StadiumOwner_User]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[StadiumStaff]  WITH CHECK ADD  CONSTRAINT [FK_StadiumOwner_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StadiumStaff] CHECK CONSTRAINT [FK_StadiumOwner_User]
GO
/****** Object:  ForeignKey [FK_StadiumReview_Stadium]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[StadiumReview]  WITH CHECK ADD  CONSTRAINT [FK_StadiumReview_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumReview] CHECK CONSTRAINT [FK_StadiumReview_Stadium]
GO
/****** Object:  ForeignKey [FK_StadiumReview_User]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[StadiumReview]  WITH CHECK ADD  CONSTRAINT [FK_StadiumReview_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StadiumReview] CHECK CONSTRAINT [FK_StadiumReview_User]
GO
/****** Object:  ForeignKey [FK_StadiumReview_User1]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[StadiumReview]  WITH CHECK ADD  CONSTRAINT [FK_StadiumReview_User1] FOREIGN KEY([Approver])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StadiumReview] CHECK CONSTRAINT [FK_StadiumReview_User1]
GO
/****** Object:  ForeignKey [FK_StadiumRating_Stadium]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[StadiumRating]  WITH CHECK ADD  CONSTRAINT [FK_StadiumRating_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumRating] CHECK CONSTRAINT [FK_StadiumRating_Stadium]
GO
/****** Object:  ForeignKey [FK_StadiumRating_User]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[StadiumRating]  WITH CHECK ADD  CONSTRAINT [FK_StadiumRating_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StadiumRating] CHECK CONSTRAINT [FK_StadiumRating_User]
GO
/****** Object:  ForeignKey [FK_StadiumImage_Stadium]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[StadiumImage]  WITH CHECK ADD  CONSTRAINT [FK_StadiumImage_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[StadiumImage] CHECK CONSTRAINT [FK_StadiumImage_Stadium]
GO
/****** Object:  ForeignKey [FK_Notification_Stadium]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Stadium]
GO
/****** Object:  ForeignKey [FK_Notification_User]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_User]
GO
/****** Object:  ForeignKey [FK_FieldPrice_Stadium]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[FieldPrice]  WITH CHECK ADD  CONSTRAINT [FK_FieldPrice_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[FieldPrice] CHECK CONSTRAINT [FK_FieldPrice_Stadium]
GO
/****** Object:  ForeignKey [FK_Field_Field]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_Field] FOREIGN KEY([ParentField])
REFERENCES [dbo].[Field] ([Id])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_Field]
GO
/****** Object:  ForeignKey [FK_Field_FieldPrice]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_FieldPrice] FOREIGN KEY([PriceId])
REFERENCES [dbo].[FieldPrice] ([Id])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_FieldPrice]
GO
/****** Object:  ForeignKey [FK_Field_Stadium]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_Stadium] FOREIGN KEY([StadiumId])
REFERENCES [dbo].[Stadium] ([Id])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_Stadium]
GO
/****** Object:  ForeignKey [FK_PriceTable_FieldPrice]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[PriceTable]  WITH CHECK ADD  CONSTRAINT [FK_PriceTable_FieldPrice] FOREIGN KEY([FieldPriceId])
REFERENCES [dbo].[FieldPrice] ([Id])
GO
ALTER TABLE [dbo].[PriceTable] CHECK CONSTRAINT [FK_PriceTable_FieldPrice]
GO
/****** Object:  ForeignKey [FK_Promotion_Field]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [FK_Promotion_Field] FOREIGN KEY([FieldId])
REFERENCES [dbo].[Field] ([Id])
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [FK_Promotion_Field]
GO
/****** Object:  ForeignKey [FK_Promotion_User]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [FK_Promotion_User] FOREIGN KEY([Creator])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [FK_Promotion_User]
GO
/****** Object:  ForeignKey [FK_Reservation_Field]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Field] FOREIGN KEY([FieldId])
REFERENCES [dbo].[Field] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Field]
GO
/****** Object:  ForeignKey [FK_Reservation_User]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User]
GO
/****** Object:  ForeignKey [FK_Reservation_User1]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User1] FOREIGN KEY([Approver])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User1]
GO
/****** Object:  ForeignKey [FK_Reservation_User2]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User2] FOREIGN KEY([RivalId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User2]
GO
/****** Object:  ForeignKey [FK_Reservation_User3]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User3] FOREIGN KEY([RivalFinder])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User3]
GO
/****** Object:  ForeignKey [FK_ReportUser_Reservation]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[ReportUser]  WITH CHECK ADD  CONSTRAINT [FK_ReportUser_Reservation] FOREIGN KEY([Reference])
REFERENCES [dbo].[Reservation] ([Id])
GO
ALTER TABLE [dbo].[ReportUser] CHECK CONSTRAINT [FK_ReportUser_Reservation]
GO
/****** Object:  ForeignKey [FK_ReportUser_User]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[ReportUser]  WITH CHECK ADD  CONSTRAINT [FK_ReportUser_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ReportUser] CHECK CONSTRAINT [FK_ReportUser_User]
GO
/****** Object:  ForeignKey [FK_ReportUser_User1]    Script Date: 04/16/2014 17:43:18 ******/
ALTER TABLE [dbo].[ReportUser]  WITH CHECK ADD  CONSTRAINT [FK_ReportUser_User1] FOREIGN KEY([ReportUserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ReportUser] CHECK CONSTRAINT [FK_ReportUser_User1]
GO
