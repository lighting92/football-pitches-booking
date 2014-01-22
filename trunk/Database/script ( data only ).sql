USE [FootballPitchesBooking]
GO
/****** Object:  Table [dbo].[Advertisement]    Script Date: 01/23/2014 00:07:18 ******/
SET IDENTITY_INSERT [dbo].[Advertisement] ON
INSERT [dbo].[Advertisement] ([Id], [Position], [AdvertiseDetail], [HasPaid]) VALUES (1, N'1', N'Quảng cáo thông qua tên thương hiệu', 1)
INSERT [dbo].[Advertisement] ([Id], [Position], [AdvertiseDetail], [HasPaid]) VALUES (2, N'2', N'Thu hút khách', 0)
INSERT [dbo].[Advertisement] ([Id], [Position], [AdvertiseDetail], [HasPaid]) VALUES (4, N'3', N'Quảng cáo thông qua hàng hóa', 1)
SET IDENTITY_INSERT [dbo].[Advertisement] OFF
/****** Object:  Table [dbo].[Stadium]    Script Date: 01/23/2014 00:07:18 ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 01/23/2014 00:07:18 ******/
SET IDENTITY_INSERT [dbo].[Role] ON
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (2, N'Staff')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (3, N'User')
SET IDENTITY_INSERT [dbo].[Role] OFF
/****** Object:  Table [dbo].[MemberRank]    Script Date: 01/23/2014 00:07:18 ******/
SET IDENTITY_INSERT [dbo].[MemberRank] ON
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (1, N'User', 50, NULL)
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (2, N'Power User', 100, NULL)
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (3, N'Elite User', 150, NULL)
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (4, N'Crazy User', 200, NULL)
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (5, N'Insane User', 400, NULL)
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (6, N'Veteran User', 600, NULL)
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (7, N'Extreme User', 1000, NULL)
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (8, N'Ultimate User ', 2000, NULL)
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (9, N'Master ', 5000, NULL)
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (10, N'Vip', 10000, N'Trùm      ')
SET IDENTITY_INSERT [dbo].[MemberRank] OFF
/****** Object:  Table [dbo].[Field]    Script Date: 01/23/2014 00:07:18 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 01/23/2014 00:07:18 ******/
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [IsReceivedReward], [JoinDate], [IsActive]) VALUES (7, N'Mr A', N'123', N'Nguyễn Văn A', N'1 Tô Ký', N'0913141516', N'nguyenvana@gmail.com', 100, 2, 1, CAST(0x0F380B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [IsReceivedReward], [JoinDate], [IsActive]) VALUES (9, N'Mr Songoku', N'111', N'Trần Văn B', N'159 Quang Trung', N'0977778899', N'tranvanb@gmail.com', 400, 5, 1, CAST(0x20380B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [IsReceivedReward], [JoinDate], [IsActive]) VALUES (10, N'Mr Mao Chạch Đông', N'456', N'Đỗ Tuấn C', N'30/28/39 Phan Xích Long', N'01689247350', N'dotuanc@yahoo.com', 200, 4, 1, CAST(0x57380B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [IsReceivedReward], [JoinDate], [IsActive]) VALUES (11, N'Mr Thạc Sỉn', N'789', N'Xinh Thị Lung Linh', N'193 Pasteur', N'0988888888', N'batungdeptrai@gmail.com', 10000, 10, 1, CAST(0x01380B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [IsReceivedReward], [JoinDate], [IsActive]) VALUES (12, N'Mr Min Tun', N'222', N'Phan Nam D', N'7731 Lạc Long Quân', N'0913178631', N'phannamd@gmail.com', 100, 2, 1, CAST(0x07380B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [IsReceivedReward], [JoinDate], [IsActive]) VALUES (13, N'Mr Khánh Đẹp Trai', N'345', N'Nguyễn Quốc Khánh', N'225 Hàn Thuyên', N'0935999243', N'khanhnq@gmail.com', 5000, 9, 1, CAST(0x8B380B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [IsReceivedReward], [JoinDate], [IsActive]) VALUES (15, N'Mr Giang Joseph', N'142', N'Giang Thị Giô Dép', N'312 Trần Văn Đang', N'0984235124', N'giangjoseph@loveline.com', 150, 3, 1, CAST(0x78380B00 AS Date), 1)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[StadiumImage]    Script Date: 01/23/2014 00:07:18 ******/
SET IDENTITY_INSERT [dbo].[StadiumImage] ON
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (2, 2, N'a', N'b', N'c')
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (3, 4, N'a', N'b', N'c')
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (6, 8, N'a', N'b', N'c')
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (7, 10, N'a', N'b', N'c')
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (8, 12, N'a', N'b', N'c')
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (9, 14, N'a', N'b', N'c')
INSERT [dbo].[StadiumImage] ([Id], [StadiumId], [Path], [Title], [Caption]) VALUES (10, 16, N'a', N'b', N'c')
SET IDENTITY_INSERT [dbo].[StadiumImage] OFF
/****** Object:  Table [dbo].[UserInRole]    Script Date: 01/23/2014 00:07:18 ******/
SET IDENTITY_INSERT [dbo].[UserInRole] ON
INSERT [dbo].[UserInRole] ([Id], [UserId], [RoleId]) VALUES (1, 11, 1)
INSERT [dbo].[UserInRole] ([Id], [UserId], [RoleId]) VALUES (2, 7, 3)
INSERT [dbo].[UserInRole] ([Id], [UserId], [RoleId]) VALUES (3, 9, 3)
SET IDENTITY_INSERT [dbo].[UserInRole] OFF
/****** Object:  Table [dbo].[StadiumStaff]    Script Date: 01/23/2014 00:07:18 ******/
SET IDENTITY_INSERT [dbo].[StadiumStaff] ON
INSERT [dbo].[StadiumStaff] ([Id], [UserId], [StadiumId], [IsOwner]) VALUES (1, 12, 12, 1)
INSERT [dbo].[StadiumStaff] ([Id], [UserId], [StadiumId], [IsOwner]) VALUES (2, 13, 18, 1)
INSERT [dbo].[StadiumStaff] ([Id], [UserId], [StadiumId], [IsOwner]) VALUES (3, 15, 22, 1)
SET IDENTITY_INSERT [dbo].[StadiumStaff] OFF
/****** Object:  Table [dbo].[StadiumReview]    Script Date: 01/23/2014 00:07:18 ******/
SET IDENTITY_INSERT [dbo].[StadiumReview] ON
INSERT [dbo].[StadiumReview] ([Id], [UserId], [StadiumId], [ReviewContent], [IsApproved]) VALUES (2, 7, 2, N'Good', 1)
INSERT [dbo].[StadiumReview] ([Id], [UserId], [StadiumId], [ReviewContent], [IsApproved]) VALUES (3, 9, 8, N'Bad', 1)
INSERT [dbo].[StadiumReview] ([Id], [UserId], [StadiumId], [ReviewContent], [IsApproved]) VALUES (4, 10, 12, N'Not Bad', 0)
SET IDENTITY_INSERT [dbo].[StadiumReview] OFF
/****** Object:  Table [dbo].[StadiumRating]    Script Date: 01/23/2014 00:07:18 ******/
SET IDENTITY_INSERT [dbo].[StadiumRating] ON
INSERT [dbo].[StadiumRating] ([Id], [UserId], [StadiumId], [Rate]) VALUES (2, 7, 2, 1)
INSERT [dbo].[StadiumRating] ([Id], [UserId], [StadiumId], [Rate]) VALUES (3, 9, 4, 5)
INSERT [dbo].[StadiumRating] ([Id], [UserId], [StadiumId], [Rate]) VALUES (4, 10, 8, 10)
SET IDENTITY_INSERT [dbo].[StadiumRating] OFF
/****** Object:  Table [dbo].[Reservation]    Script Date: 01/23/2014 00:07:18 ******/
SET IDENTITY_INSERT [dbo].[Reservation] ON
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [VerifyCode], [CreatedDate], [Approver], [Status]) VALUES (9, 3, 7, N'Nguyễn Văn A', N'0913141516', N'nguyenvana@gmail.com', CAST(0x10380B00 AS Date), 15, 2, 300000, N'qwerty', CAST(0x0000A2B500000000 AS DateTime), NULL, N'Full')
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [VerifyCode], [CreatedDate], [Approver], [Status]) VALUES (13, 4, 9, N'Trần Văn B', N'0977778899', N'tranvanb@gmail.com', CAST(0x21380B00 AS Date), 18, 2, 300000, N'trium', CAST(0x0000A2C600000000 AS DateTime), NULL, N'Blank')
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [VerifyCode], [CreatedDate], [Approver], [Status]) VALUES (14, 5, 10, N'Đỗ Tuấn C', N'01689247350', N'dotuanc@yahoo.com', CAST(0x58380B00 AS Date), 16, 1, 100000, N'hardcore', CAST(0x0000A2FD00000000 AS DateTime), NULL, N'Blank')
SET IDENTITY_INSERT [dbo].[Reservation] OFF
/****** Object:  Table [dbo].[Promotion]    Script Date: 01/23/2014 00:07:18 ******/
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount]) VALUES (1, 2, CAST(0x57380B00 AS Date), CAST(0x76380B00 AS Date), 50)
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount]) VALUES (2, 3, CAST(0x57380B00 AS Date), CAST(0x76380B00 AS Date), 50)
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount]) VALUES (3, 5, CAST(0x20380B00 AS Date), CAST(0x3C380B00 AS Date), 10)
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount]) VALUES (4, 6, CAST(0x20380B00 AS Date), CAST(0x3C380B00 AS Date), 10)
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount]) VALUES (5, 7, CAST(0x20380B00 AS Date), CAST(0x3C380B00 AS Date), 10)
/****** Object:  Table [dbo].[JoinSystemRequest]    Script Date: 01/23/2014 00:07:18 ******/
INSERT [dbo].[JoinSystemRequest] ([Id], [UserId], [FullName], [Address], [Phone], [Email], [StadiumName], [StadiumAddress], [Note], [Status]) VALUES (1, 12, N'Phan Nam D', N'7731 Lạc Long Quân', N'0913178631', N'phannamd@gmail.com', N'Sân Thép Miền Nam Cảng Sài Gòn', N'KP4 đường Tân Mỹ', NULL, N'True')
INSERT [dbo].[JoinSystemRequest] ([Id], [UserId], [FullName], [Address], [Phone], [Email], [StadiumName], [StadiumAddress], [Note], [Status]) VALUES (2, 13, N'Nguyễn Quốc Khánh', N'225 Hàn Thuyên', N'0935999243', N'khanhnq@gmail.com', N'Sân Huấn Luyện Bay', N'117 Hồng Hà', NULL, N'True')
INSERT [dbo].[JoinSystemRequest] ([Id], [UserId], [FullName], [Address], [Phone], [Email], [StadiumName], [StadiumAddress], [Note], [Status]) VALUES (3, 15, N'Giang Thị Giô Dép', N'312 Trần Văn Đang', N'0984235124', N'giangjoseph@loveline.com', N'Sân Kỳ Hòa', N'824/28Q Sư Vạn Hạnh (nối dài)', NULL, N'True')
/****** Object:  Table [dbo].[FieldPrice]    Script Date: 01/23/2014 00:07:18 ******/
SET IDENTITY_INSERT [dbo].[FieldPrice] ON
INSERT [dbo].[FieldPrice] ([Id], [FieldId], [TimeFrom], [TimeTo], [Price], [Day]) VALUES (1, 3, 15, 18, 150000, 10)
INSERT [dbo].[FieldPrice] ([Id], [FieldId], [TimeFrom], [TimeTo], [Price], [Day]) VALUES (2, 4, 15, 21, 150000, 10)
INSERT [dbo].[FieldPrice] ([Id], [FieldId], [TimeFrom], [TimeTo], [Price], [Day]) VALUES (3, 5, 6, 9, 100000, 10)
INSERT [dbo].[FieldPrice] ([Id], [FieldId], [TimeFrom], [TimeTo], [Price], [Day]) VALUES (4, 6, 15, 22, 100000, 10)
SET IDENTITY_INSERT [dbo].[FieldPrice] OFF
/****** Object:  Table [dbo].[Challenge]    Script Date: 01/23/2014 00:07:18 ******/
