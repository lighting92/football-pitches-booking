USE [FootballPitchesBooking]
GO
/****** Object:  Table [dbo].[Configuration]    Script Date: 04/02/2014 22:05:24 ******/
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
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (18, N'Cfg_Email', N'footballpitchesbooking@gmail.com')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (19, N'Cfg_Password', N'contactEmail')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (20, N'Cfg_SmtpHost', N'smtp.gmail.com')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (21, N'Cfg_SmtpPort', N'587')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (22, N'Cfg_EnableSSL', N'True')
SET IDENTITY_INSERT [dbo].[Configuration] OFF
