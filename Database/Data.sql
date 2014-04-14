USE [FootballPitchesBooking]
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (1, N'WebsiteMaster', NULL)
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (2, N'WebsiteStaff', NULL)
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (3, N'StadiumOwner', NULL)
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (4, N'StadiumStaff', NULL)
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (5, N'Member', NULL)
INSERT [dbo].[Role] ([Id], [Role], [DisplayName]) VALUES (6, N'BannedMember', NULL)
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [JoinDate], [IsActive], [RoleId]) VALUES (1, N'admin', N'111111', N'Admin', N'1 Tô Ký, Đông Hưng Thuận, Quận 12, Hồ Chí Minh', N'0111222333', N'admin@fpb.com', CAST(N'2014-02-25' AS Date), 1, 1)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [JoinDate], [IsActive], [RoleId]) VALUES (2, N'phuongnd', N'123456', N'Phương Duy Nguyễn', N'1 Tô Ký, Đông Hưng Thuận, Quận 12, Hồ Chí Minh', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-03-03' AS Date), 0, 3)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [JoinDate], [IsActive], [RoleId]) VALUES (3, N'tester01', N'123456', N'Nguyễn Tét Tơ', N'15 PVD, Quận 1, TP.Hồ Chí Minh', N'0123456789', N'tester01@gmail.com', CAST(N'2014-03-11' AS Date), 1, 2)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [JoinDate], [IsActive], [RoleId]) VALUES (5, N'khanhnq', N'123456', N'aaaaaaa', N'11113ddd', N'123456789', N'aaaa@acc.com', CAST(N'2014-01-01' AS Date), 1, 5)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [JoinDate], [IsActive], [RoleId]) VALUES (6, N'thinhnd', N'123456', N'zczcxzxczxc', N'123 Ham Nghi, Quận 1, TP.Hồ Chí Minh', N'1234567890', N'thesunofvn@gmail.com', CAST(N'2014-03-21' AS Date), 1, 5)
INSERT [dbo].[User] ([Id], [UserName], [Password], [FullName], [Address], [PhoneNumber], [Email], [JoinDate], [IsActive], [RoleId]) VALUES (7, N'bbbbbbbbb', N'123456', N'rtyuiolkjhg', N'1111 phung, Quận 1, TP.Hồ Chí Minh', N'1234567890', N'sdfg@dfgh.vbc', CAST(N'2014-03-21' AS Date), 1, 5)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[Stadium] ON 

INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (1, N'Phương Đô', N'18C Cộng Hòa', N'12', N'Q. Tân Bình', N'0123456789', N'PD@PD.com', 1, 2, 0, 0, CAST(N'2015-01-01' AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (2, N'Chảo Lửa', N'18C Cộng Hòa', N'12', N'Q. Tân Bình', N'0987765431', N'CL@CL.com', 1, 2, 0, 0, CAST(N'2015-01-01' AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (8, N'Phương Nam', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 1, 2, 0, 0, CAST(N'2015-01-01' AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (9, N'Phương Nam 2', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 1, 2, 0, 0, CAST(N'2015-01-01' AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (10, N'Phương Nam 3', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 1, 2, 0, 0, CAST(N'2015-01-01' AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (11, N'Phương Nam 4', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 1, 2, 0, 0, CAST(N'2015-01-01' AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (12, N'Phương Nam 5', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 1, 2, 0, 0, CAST(N'2015-01-01' AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (13, N'Phương Nam 6', N'44/5 Phạm Văn Chiêu', N'9', N'Q. Gò Vấp', N'01662194419', N'phuongnam@phuongnam.com', 0, 2, 0, 0, CAST(N'2015-01-01' AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (14, N'K334', N'69/96 Hoàng Hoa Thám', N'11', N'Q. Tân Bình', N'01662194419', N'k334@sanbong.com', 1, 2, 6, 4, CAST(N'2015-03-16' AS Date))
INSERT [dbo].[Stadium] ([Id], [Name], [Street], [Ward], [District], [Phone], [Email], [IsActive], [MainOwner], [OpenTime], [CloseTime], [ExpiredDate]) VALUES (15, N'Sân Thống Nhất', N'33/3 A Thống Nhất', N'P. 10', N'Q. Gò Vấp', N'0135792468', N'thongnhat@sanbong.com', 1, 2, 0, 0, CAST(N'2015-07-01' AS Date))
SET IDENTITY_INSERT [dbo].[Stadium] OFF
SET IDENTITY_INSERT [dbo].[UserDistance] ON 

INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (1, 2, N'phuongnd.xml', CAST(N'2014-04-11' AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (2, 3, N'tester01.xml', CAST(N'2014-04-13' AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (3, 1, N'admin.xml', CAST(N'2014-04-11' AS Date))
INSERT [dbo].[UserDistance] ([Id], [UserId], [Path], [UpdateDate]) VALUES (4, 6, N'thinhnd.xml', CAST(N'2014-04-13' AS Date))
SET IDENTITY_INSERT [dbo].[UserDistance] OFF
SET IDENTITY_INSERT [dbo].[PunishMember] ON 

INSERT [dbo].[PunishMember] ([Id], [UserId], [Reason], [Date], [ExpiredDate], [IsForever], [StaffId]) VALUES (1, 6, N'Xàm lông', CAST(N'2014-04-12 00:00:00.000' AS DateTime), CAST(N'2014-04-15 00:00:00.000' AS DateTime), 0, 1)
SET IDENTITY_INSERT [dbo].[PunishMember] OFF
SET IDENTITY_INSERT [dbo].[StadiumStaff] ON 

INSERT [dbo].[StadiumStaff] ([Id], [UserId], [StadiumId], [IsOwner]) VALUES (10, 3, 1, 1)
SET IDENTITY_INSERT [dbo].[StadiumStaff] OFF
SET IDENTITY_INSERT [dbo].[StadiumReview] ON 

INSERT [dbo].[StadiumReview] ([Id], [UserId], [StadiumId], [ReviewContent], [IsApproved], [Approver], [CreateDate]) VALUES (1, 1, 2, N'con lợn gợi tình', 1, 1, CAST(N'2014-04-11 17:39:05.453' AS DateTime))
SET IDENTITY_INSERT [dbo].[StadiumReview] OFF
SET IDENTITY_INSERT [dbo].[StadiumRating] ON 

INSERT [dbo].[StadiumRating] ([Id], [UserId], [StadiumId], [Rate]) VALUES (1, 2, 1, 5)
INSERT [dbo].[StadiumRating] ([Id], [UserId], [StadiumId], [Rate]) VALUES (2, 3, 1, 1)
INSERT [dbo].[StadiumRating] ([Id], [UserId], [StadiumId], [Rate]) VALUES (3, 1, 1, 2)
INSERT [dbo].[StadiumRating] ([Id], [UserId], [StadiumId], [Rate]) VALUES (4, 1, 2, 5)
SET IDENTITY_INSERT [dbo].[StadiumRating] OFF
SET IDENTITY_INSERT [dbo].[Notification] ON 

INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (0, 2, NULL, N'Bạn nhận yêu cầu giao hữu từ [phuongnd] <a href=''/Account/EditReservation?Id=16''>Chi tiết</a>', CAST(N'2014-04-03 11:39:32.100' AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (2, 2, NULL, N'Bạn nhận yêu cầu giao hữu từ [phuongnd] <a href=''/Account/EditReservation?Id=17''>Chi tiết</a>', CAST(N'2014-04-03 13:12:30.077' AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (3, 2, NULL, N'[phuongnd] không đồng ý giao hữu với bạn', CAST(N'2014-04-03 13:12:57.367' AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (4, 2, NULL, N'Bạn nhận yêu cầu giao hữu từ [phuongnd] <a href=''/Account/EditReservation?Id=17''>Chi tiết</a>', CAST(N'2014-04-03 13:13:13.903' AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (5, 2, NULL, N'[phuongnd] đồng ý giao hữu với bạn', CAST(N'2014-04-03 13:13:20.900' AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (6, 2, NULL, N'Bạn nhận yêu cầu giao hữu từ [phuongnd] <a href=''/Account/EditReservation?Id=18''>Chi tiết</a>', CAST(N'2014-04-03 13:14:03.487' AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (7, 2, NULL, N'[phuongnd] không mời giao hữu nữa', CAST(N'2014-04-03 13:14:30.137' AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (8, 2, NULL, N'Bạn nhận yêu cầu giao hữu từ [tester01] <a href=''/Account/EditReservation?Id=19''>Chi tiết</a>', CAST(N'2014-04-03 14:29:27.787' AS DateTime), N'Unread')
INSERT [dbo].[Notification] ([Id], [UserId], [StadiumId], [Message], [CreateDate], [Status]) VALUES (9, 3, NULL, N'[phuongnd] đồng ý giao hữu với bạn', CAST(N'2014-04-03 14:31:44.007' AS DateTime), N'Unread')
SET IDENTITY_INSERT [dbo].[Notification] OFF
SET IDENTITY_INSERT [dbo].[FieldPrice] ON 

INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (1, 14, N'Sân 5 người', N'bảng giá sân 5 người cho hệ thống k334')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (4, 14, N'Sân 11 người', N'Bảng giá cho sân 11 người hệ thống sân bóng k334')
INSERT [dbo].[FieldPrice] ([Id], [StadiumId], [FieldPriceName], [FieldPriceDescription]) VALUES (5, 11, N'1', N'123')
SET IDENTITY_INSERT [dbo].[FieldPrice] OFF
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
SET IDENTITY_INSERT [dbo].[Promotion] ON 

INSERT [dbo].[Promotion] ([Id], [FieldId], [PromotionFrom], [PromotionTo], [Discount], [Creator], [IsActive]) VALUES (1, 30, CAST(N'2014-04-01' AS Date), CAST(N'2014-04-15' AS Date), 30, 2, 1)
SET IDENTITY_INSERT [dbo].[Promotion] OFF
SET IDENTITY_INSERT [dbo].[Reservation] ON 

INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (2, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-03-25' AS Date), 15, 1, 150000, NULL, NULL, N'2635313463069959020', CAST(N'2014-03-25 12:11:46.997' AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (3, 7, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-03-25' AS Date), 15, 1, 100000, NULL, NULL, N'2635313464208494140', CAST(N'2014-03-25 12:13:40.850' AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (4, 8, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-03-25' AS Date), 15, 1, 100000, NULL, NULL, N'2635313464518961898', CAST(N'2014-04-25 12:14:11.897' AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (5, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-04-25' AS Date), 17, 1, 200000, NULL, NULL, N'2635313466573779427', CAST(N'2014-04-25 12:17:37.377' AS DateTime), 1, N'Approved', 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (6, 7, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-04-25' AS Date), 17, 1, 100000, NULL, NULL, N'2635313467111610189', CAST(N'2014-04-25 12:18:31.160' AS DateTime), 1, N'Approved', 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (7, 8, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-04-25' AS Date), 17, 1, 100000, NULL, NULL, N'2635313467291340469', CAST(N'2014-04-25 12:18:49.133' AS DateTime), 1, N'Approved', 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (8, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-04-25' AS Date), 14, 1, 150000, NULL, NULL, N'2635313467996960828', CAST(N'2014-03-25 12:19:59.697' AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (9, 7, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-03-25' AS Date), 14, 1, 100000, NULL, NULL, N'2635313468052414000', CAST(N'2014-03-25 12:20:05.240' AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (10, 8, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-03-25' AS Date), 14, 1, 100000, NULL, NULL, N'2635313468097736592', CAST(N'2014-03-25 12:20:09.773' AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (11, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-03-25' AS Date), 19, 1, 550000, NULL, NULL, N'2635313471408335947', CAST(N'2014-03-25 12:25:40.833' AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (12, 7, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-03-25' AS Date), 19, 1, 100000, NULL, NULL, N'2635313471456828721', CAST(N'2014-03-25 12:25:45.683' AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (13, 8, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-03-25' AS Date), 19, 1, 100000, NULL, NULL, N'2635313471498981132', CAST(N'2014-03-25 12:25:49.897' AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (14, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-04-05' AS Date), 2, 1, 100000, NULL, NULL, N'2635320811241598979', CAST(N'2014-04-03 00:18:44.160' AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (15, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-04-04' AS Date), 2, 1, 100000, NULL, NULL, N'2635320814659944497', CAST(N'2014-04-03 00:24:25.993' AS DateTime), NULL, N'Canceled', 1, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', NULL, N'Approve')
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (16, 7, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-04-04' AS Date), 2, 1, 400000, NULL, NULL, N'2635320824514858166', CAST(N'2014-04-03 00:40:51.487' AS DateTime), NULL, N'Canceled', 1, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', NULL, N'Pending')
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (17, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-04-04' AS Date), 2, 1, 100000, NULL, NULL, N'2635321271620709407', CAST(N'2014-04-03 13:06:02.070' AS DateTime), NULL, N'Pending', 1, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', NULL, N'Approve')
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (18, 7, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-04-04' AS Date), 2, 1, 400000, NULL, NULL, N'2635321276237503473', CAST(N'2014-04-03 13:13:43.750' AS DateTime), NULL, N'Pending', 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Reservation] ([Id], [FieldId], [UserId], [FullName], [PhoneNumber], [Email], [Date], [StartTime], [Duration], [Price], [Discount], [PromotionId], [VerifyCode], [CreatedDate], [Approver], [Status], [NeedRival], [RivalId], [RivalName], [RivalPhone], [RivalEmail], [RivalFinder], [RivalStatus]) VALUES (19, 5, 2, N'Nguyễn Duy Phương', N'01662194419', N'phuongnd@gm.com', CAST(N'2014-04-04' AS Date), 18, 1, 300000, NULL, NULL, N'2635321319212801185', CAST(N'2014-04-03 14:25:21.280' AS DateTime), NULL, N'Pending', 1, 3, N'Nguyễn Tét Tơ', N'0123456789', N'tester01@gmail.com', NULL, N'Approve')
SET IDENTITY_INSERT [dbo].[Reservation] OFF
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
SET IDENTITY_INSERT [dbo].[Configuration] OFF
