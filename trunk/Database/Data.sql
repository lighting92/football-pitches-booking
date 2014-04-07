USE [FootballPitchesBooking]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 04/07/2014 16:02:44 ******/
SET IDENTITY_INSERT [dbo].[Role] ON
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (1, N'WebsiteMaster', NULL)
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (2, N'WebsiteStaff', NULL)
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (3, N'StadiumOwner', NULL)
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (4, N'StadiumStaff', NULL)
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (5, N'Member', NULL)
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (6, N'BannedMember', NULL)
SET IDENTITY_INSERT [dbo].[Role] OFF
/****** Object:  Table [dbo].[Configuration]    Script Date: 04/07/2014 16:02:44 ******/
SET IDENTITY_INSERT [dbo].[Configuration] ON
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (1, N'Bs_MostBooked', N'55')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (2, N'Bs_Nearest', N'25')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (3, N'Bs_MostPromoted', N'20')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (5, N'Ap_MostBooked', N'30')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (6, N'Ap_Nearest', N'55')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (7, N'Ap_MostPromoted', N'15')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (8, N'Pr_MostBooked', N'25')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (9, N'Pr_Nearest', N'30')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (10, N'Pr_MostPromoted', N'45')
INSERT [dbo].[Configuration] ([Id], [Name], [Value]) VALUES (11, N'Gg_Periodic', N'3')
SET IDENTITY_INSERT [dbo].[Configuration] OFF
/****** Object:  Table [dbo].[MemberRank]    Script Date: 04/07/2014 16:02:44 ******/
SET IDENTITY_INSERT [dbo].[MemberRank] ON
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (1, N'Legend', 1000, N'Legend')
INSERT [dbo].[MemberRank] ([Id], [RankName], [Point], [Promotion]) VALUES (2, N'Beginner', 0, N'Beginner')
SET IDENTITY_INSERT [dbo].[MemberRank] OFF
/****** Object:  Table [dbo].[User]    Script Date: 04/07/2014 16:02:44 ******/
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (1, N'admin', N'123456', N'Admin', N'Admin', N'0111222333', N'admin@fpb.com', 1000, 1, CAST(0x38380B00 AS Date), 1, 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (2, N'phuongnd', N'123456', N'Nguyễn Duy Phương', N'1 Tô Ký, Đông Hưng Thuận, Quận 12, Hồ Chí Minh', N'01662194419', N'phuongnd@gm.com', 0, 2, CAST(0x3E380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (3, N'tester01', N'123456', N'Nguyễn Tét Tơ', N'15 PVD, Quận 1, TP.Hồ Chí Minh', N'0123456789', N'tester01@gmail.com', 0, 2, CAST(0x46380B00 AS Date), 1, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (5, N'khanhnq', N'123456', N'aaaaaaa', N'11113ddd', N'123456789', N'aaaa@acc.com', 0, 2, CAST(0x01380B00 AS Date), 1, 5)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (6, N'aaaaaaa', N'123456', N'zczcxzxczxc', N'123 Ham Nghi, Quận 1, TP.Hồ Chí Minh', N'1234567890', N'abc@abc.cba', 0, 2, CAST(0x50380B00 AS Date), 1, 5)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [Point], [RankId], [JoinDate], [IsActive], [RoleId]) VALUES (7, N'bbbbbbbbb', N'123456', N'rtyuiolkjhg', N'1111 phung, Quận 1, TP.Hồ Chí Minh', N'1234567890', N'sdfg@dfgh.vbc', 0, 2, CAST(0x50380B00 AS Date), 1, 5)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[UserDistance]    Script Date: 04/07/2014 16:02:44 ******/
/****** Object:  Table [dbo].[JoinSystemRequest]    Script Date: 04/07/2014 16:02:44 ******/
/****** Object:  Table [dbo].[Advertisement]    Script Date: 04/07/2014 16:02:44 ******/
/****** Object:  Table [dbo].[Stadium]    Script Date: 04/07/2014 16:02:44 ******/
SET IDENTITY_INSERT [dbo].[Stadium] ON
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (1, N'Phương Đô', N'18C Cộng Hòa', N'12', N'Q. Tân Bình', N'0123456789', N'PD@PD.com', 1, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (2, N'Chảo Lửa', N'18C Cộng Hòa', N'12', N'Q. Tân Bình', N'0987765431', N'CL@CL.com', 1, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (8, N'Phương Nam', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 1, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (9, N'Phương Nam 2', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 1, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (10, N'Phương Nam 3', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 1, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (11, N'Phương Nam 4', N'44/5 Phạm Văn Chiêu', N'9', N'Q. 2', N'01662194419', N'phuongnam@phuongnam.com', 1, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (12, N'Phương Nam 5', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 1, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (13, N'Phương Nam 6', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 0, 2, 0, 0, CAST(0x6E390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (14, N'K334', N'69/96 Hoàng Hoa Thám', N'11', N'Q. Tân Bình', N'01662194419', N'k334@sanbong.com', 1, 2, 6, 4, CAST(0xB8390B00 AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (15, N'Sân Thống Nhất', N'33/3 A Thống Nhất', N'P. 10', N'Q. Gò Vấp', N'0135792468', N'thongnhat@sanbong.com', 1, 2, 0, 0, CAST(0x233A0B00 AS Date))
SET IDENTITY_INSERT [dbo].[Stadium] OFF
/****** Object:  Table [dbo].[StadiumStaff]    Script Date: 04/07/2014 16:02:44 ******/
SET IDENTITY_INSERT [dbo].[StadiumStaff] ON
INSERT [dbo].[StadiumStaff] ([Id], [UserId], [StadiumId], [IsOwner]) VALUES (10, 3, 1, 1)
SET IDENTITY_INSERT [dbo].[StadiumStaff] OFF
/****** Object:  Table [dbo].[StadiumReview]    Script Date: 04/07/2014 16:02:44 ******/
/****** Object:  Table [dbo].[StadiumRating]    Script Date: 04/07/2014 16:02:44 ******/
/****** Object:  Table [dbo].[StadiumImage]    Script Date: 04/07/2014 16:02:44 ******/
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
/****** Object:  Table [dbo].[Notification]    Script Date: 04/07/2014 16:02:44 ******/
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
/****** Object:  Table [dbo].[FieldPrice]    Script Date: 04/07/2014 16:02:44 ******/
SET IDENTITY_INSERT [dbo].[FieldPrice] ON
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (1, 14, N'Sân 5 người', N'bảng giá sân 5 người cho hệ thống k334')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (4, 14, N'Sân 11 người', N'Bảng giá cho sân 11 người hệ thống sân bóng k334')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (5, 11, N'1', N'123')
SET IDENTITY_INSERT [dbo].[FieldPrice] OFF
/****** Object:  Table [dbo].[Field]    Script Date: 04/07/2014 16:02:44 ******/
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
/****** Object:  Table [dbo].[PriceTable]    Script Date: 04/07/2014 16:02:44 ******/
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
/****** Object:  Table [dbo].[Promotion]    Script Date: 04/07/2014 16:02:44 ******/
SET IDENTITY_INSERT [dbo].[Promotion] ON
INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount], [Creator], [IsActive]) VALUES (1, 30, CAST(0x5B380B00 AS Date), CAST(0x69380B00 AS Date), 30, 2, 1)
SET IDENTITY_INSERT [dbo].[Promotion] OFF
/****** Object:  Table [dbo].[Reservation]    Script Date: 04/07/2014 16:02:44 ******/
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
