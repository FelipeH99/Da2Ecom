USE [ECommerce]
GO
/**DROP TABLE STATEMENT**/
/**Comment following lines if needed(AHRE INGLES EL TIPO)**/

drop table if exists dbo.Permissions;
drop table if exists dbo.AdminTokens;
drop table if exists dbo.Roles;
drop table if exists dbo.ColorDiscounts;
drop table if exists dbo.Colors;
drop table if exists dbo.BrandDiscounts;
drop table if exists dbo.Products;
drop table if exists dbo.Brands;
drop table if exists dbo.Purchase;
drop table if exists dbo.Users;
drop table if exists dbo.QuantityDiscounts;
drop table if exists dbo.PercentageDiscounts;
drop table if exists dbo.PaymentMethods;
drop table if exists dbo.__EFMigrationsHistory;

CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdminTokens]    Script Date: 11/1/2023 11:12:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AdminTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BrandDiscounts]    Script Date: 11/1/2023 11:12:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrandDiscounts](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[BrandId] [uniqueidentifier] NOT NULL,
	[MinProductsForPromotion] [int] NOT NULL,
	[NumberOfProductsForFree] [int] NOT NULL,
	[ProductToBeDiscounted] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_BrandDiscounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 11/1/2023 11:12:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ColorDiscounts]    Script Date: 11/1/2023 11:12:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ColorDiscounts](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ColorId] [uniqueidentifier] NOT NULL,
	[PercentageDiscount] [float] NOT NULL,
	[ProductToBeDiscounted] [nvarchar](max) NOT NULL,
	[MinProductsNeededForDiscount] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ColorDiscounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colors]    Script Date: 11/1/2023 11:12:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colors](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ProductId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Colors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethods]    Script Date: 11/1/2023 11:12:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethods](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PaymentMethods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PercentageDiscounts]    Script Date: 11/1/2023 11:12:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PercentageDiscounts](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PercentageDiscounted] [float] NOT NULL,
	[ProductToBeDiscounted] [nvarchar](max) NOT NULL,
	[MinProductsNeededForDiscount] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_PercentageDiscounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 11/1/2023 11:12:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[RoleId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/1/2023 11:12:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[Price] [float] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ProductCategory] [int] NOT NULL,
	[BrandId] [uniqueidentifier] NOT NULL,
	[PurchaseId] [uniqueidentifier] NULL,
	[AvailableForPromotion] [bit] NOT NULL,
	[Stock] [int] NOT NULL,
	[ImageURL] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase]    Script Date: 11/1/2023 11:12:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[PurchaseDate] [datetime2](7) NOT NULL,
	[ProductsPrice] [float] NOT NULL,
	[DiscountApplied] [nvarchar](max) NOT NULL,
	[FinalPrice] [float] NOT NULL,
	[PaymentMethodId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Purchase] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuantityDiscounts]    Script Date: 11/1/2023 11:12:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuantityDiscounts](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ProductCategory] [int] NOT NULL,
	[MinProductsNeededForDiscount] [int] NOT NULL,
	[NumberOfProductsToBeFree] [int] NOT NULL,
	[ProductToBeDiscounted] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_QuantityDiscounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11/1/2023 11:12:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[UserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/1/2023 11:12:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[DeliveryAdress] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_AdminTokens_UserId]    Script Date: 11/1/2023 11:12:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_AdminTokens_UserId] ON [dbo].[AdminTokens]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BrandDiscounts_BrandId]    Script Date: 11/1/2023 11:12:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_BrandDiscounts_BrandId] ON [dbo].[BrandDiscounts]
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ColorDiscounts_ColorId]    Script Date: 11/1/2023 11:12:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_ColorDiscounts_ColorId] ON [dbo].[ColorDiscounts]
(
	[ColorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Colors_ProductId]    Script Date: 11/1/2023 11:12:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_Colors_ProductId] ON [dbo].[Colors]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Permissions_RoleId]    Script Date: 11/1/2023 11:12:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_Permissions_RoleId] ON [dbo].[Permissions]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_BrandId]    Script Date: 11/1/2023 11:12:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_Products_BrandId] ON [dbo].[Products]
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_PurchaseId]    Script Date: 11/1/2023 11:12:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_Products_PurchaseId] ON [dbo].[Products]
(
	[PurchaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Purchase_PaymentMethodId]    Script Date: 11/1/2023 11:12:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_Purchase_PaymentMethodId] ON [dbo].[Purchase]
(
	[PaymentMethodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Purchase_UserId]    Script Date: 11/1/2023 11:12:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_Purchase_UserId] ON [dbo].[Purchase]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Roles_UserId]    Script Date: 11/1/2023 11:12:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_Roles_UserId] ON [dbo].[Roles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BrandDiscounts] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[ColorDiscounts] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[PercentageDiscounts] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (CONVERT([bit],(0))) FOR [AvailableForPromotion]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0)) FOR [Stock]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (N'') FOR [ImageURL]
GO
ALTER TABLE [dbo].[Purchase] ADD  DEFAULT (N'') FOR [DiscountApplied]
GO
ALTER TABLE [dbo].[Purchase] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [FinalPrice]
GO
ALTER TABLE [dbo].[Purchase] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [PaymentMethodId]
GO
ALTER TABLE [dbo].[QuantityDiscounts] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[AdminTokens]  WITH CHECK ADD  CONSTRAINT [FK_AdminTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AdminTokens] CHECK CONSTRAINT [FK_AdminTokens_Users_UserId]
GO
ALTER TABLE [dbo].[BrandDiscounts]  WITH CHECK ADD  CONSTRAINT [FK_BrandDiscounts_Brands_BrandId] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brands] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BrandDiscounts] CHECK CONSTRAINT [FK_BrandDiscounts_Brands_BrandId]
GO
ALTER TABLE [dbo].[ColorDiscounts]  WITH CHECK ADD  CONSTRAINT [FK_ColorDiscounts_Colors_ColorId] FOREIGN KEY([ColorId])
REFERENCES [dbo].[Colors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ColorDiscounts] CHECK CONSTRAINT [FK_ColorDiscounts_Colors_ColorId]
GO
ALTER TABLE [dbo].[Colors]  WITH CHECK ADD  CONSTRAINT [FK_Colors_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Colors] CHECK CONSTRAINT [FK_Colors_Products_ProductId]
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_Roles_RoleId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Brands_BrandId] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brands] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Brands_BrandId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Purchase_PurchaseId] FOREIGN KEY([PurchaseId])
REFERENCES [dbo].[Purchase] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Purchase_PurchaseId]
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_PaymentMethods_PaymentMethodId] FOREIGN KEY([PaymentMethodId])
REFERENCES [dbo].[PaymentMethods] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [FK_Purchase_PaymentMethods_PaymentMethodId]
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [FK_Purchase_Users_UserId]
GO
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [FK_Roles_Users_UserId]
GO

INSERT [dbo].[PaymentMethods] ([Id],[Name]) VALUES ('21366917-ef98-4dfe-8627-2748159605d0','Visa Credito');
INSERT [dbo].[PaymentMethods] ([Id],[Name]) VALUES ('21366916-ef98-4dfe-8627-2748159605d0','MasterCard Credito');
INSERT [dbo].[PaymentMethods] ([Id],[Name]) VALUES ('21366915-ef98-4dfe-8627-2748159605d0','Santander Debito');
INSERT [dbo].[PaymentMethods] ([Id],[Name]) VALUES ('21366914-ef98-4dfe-8627-2748159605d0','ITAU Debito');
INSERT [dbo].[PaymentMethods] ([Id],[Name]) VALUES ('21366913-ef98-4dfe-8627-2748159605d0','BBVA Debito');
INSERT [dbo].[PaymentMethods] ([Id],[Name]) VALUES ('21366912-ef98-4dfe-8627-2748159605d0','Paypal');
INSERT [dbo].[PaymentMethods] ([Id],[Name]) VALUES ('21366911-ef98-4dfe-8627-2748159605d0','Paganza');

INSERT [dbo].[Users] ([Id],[Name],[DeliveryAdress],[Password],[Email],[IsDeleted])	VALUES ('7da54593-1b75-45e8-a210-355f650910c5','Pedro','123 Main St, Cityville','securepassword123','alice@example.com',0)
INSERT [dbo].[Users] ([Id],[Name],[DeliveryAdress],[Password],[Email],[IsDeleted])	VALUES ('139bfcfa-14ec-41c2-81b9-dd4e64bdd2e9','Jacinto',N'456 Oak St, Townsville',N'strongpassword456',N'bob@example.com',0)
INSERT [dbo].[Users] ([Id],[Name],[DeliveryAdress],[Password],[Email],[IsDeleted])	VALUES ('9bc6bacb-7d29-41af-b4de-93d8c09c435a','Felipe',N'789 Pine St, Villagetown',N'safepassword789',N'charlie@example.com',1)
INSERT [dbo].[Users] ([Id],[Name],[DeliveryAdress],[Password],[Email],[IsDeleted])	VALUES ('1bc6bacb-7d29-41af-b4de-93d8c09c435a','Gonzalo',N'789 Pine St, Villagetown',N'password123',N'generic_buyer@example.com',1)


INSERT [dbo].[Brands] ([Id],[Name]) VALUES ('959f068f-8662-4b98-b7d5-8923bfdef304',N'Nike')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('859f068f-8662-4b98-b7d5-8923bfdef304',N'Adidas')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('759f068f-8662-4b98-b7d5-8923bfdef304',N'Puma')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('659f068f-8662-4b98-b7d5-8923bfdef304',N'Vans')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('559f068f-8662-4b98-b7d5-8923bfdef304',N'Converse')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('459f068f-8662-4b98-b7d5-8923bfdef304',N'Gucci')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('359f068f-8662-4b98-b7d5-8923bfdef304',N'Louis Vuitton')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('259f068f-8662-4b98-b7d5-8923bfdef304',N'H&M')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('159f068f-8662-4b98-b7d5-8923bfdef304',N'Zara')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('059f068f-8662-4b98-b7d5-8923bfdef304',N'Forever 21')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('aeb62096-083a-4e68-9c65-7555f8da1139',N'Abercrombie & Fitch')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('aeb62090-083a-4e68-9c65-7555f8da1132',N'Ralph Lauren')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('aeb62091-083a-4e68-9c65-7555f8da1133',N'Calvin Klein')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('aeb62092-083a-4e68-9c65-7555f8da1134',N'Tommy Hilfiger')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('aeb62093-083a-4e68-9c65-7555f8da1135',N'Levi''s')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('aeb62094-083a-4e68-9c65-7555f8da1136',N'Diesel')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('aeb62095-083a-4e68-9c65-7555f8da1137',N'Gap')
INSERT [dbo].[Brands] ([Id],[Name]) VALUES	('aeb62096-083a-4e68-9c65-7555f8da1138',N'Guess')

INSERT [dbo].[Purchase] ([Id],[UserId],[PurchaseDate],[ProductsPrice],[DiscountApplied],[FinalPrice],[PaymentMethodId]) VALUES ('02df2a3f-c07e-4187-a562-81e9a313e7f5','7da54593-1b75-45e8-a210-355f650910c5','2023-01-15 00:00:00',799.99,'Discount1',799.99,'21366917-ef98-4dfe-8627-2748159605d0')
INSERT [dbo].[Purchase] ([Id],[UserId],[PurchaseDate],[ProductsPrice],[DiscountApplied],[FinalPrice],[PaymentMethodId]) VALUES ('01df2a3f-c07e-4187-a562-81e9a313e7f5','7da54593-1b75-45e8-a210-355f650910c5','2023-01-15 00:00:00',799.99,'Discount2',799.99,'21366916-ef98-4dfe-8627-2748159605d0')
INSERT [dbo].[Purchase] ([Id],[UserId],[PurchaseDate],[ProductsPrice],[DiscountApplied],[FinalPrice],[PaymentMethodId]) VALUES ('12df2a3f-c07e-4187-a562-81e9a313e7f5','9bc6bacb-7d29-41af-b4de-93d8c09c435a','2023-01-15 00:00:00',49.99,'Discount1',49.99,'21366917-ef98-4dfe-8627-2748159605d0')
INSERT [dbo].[Purchase] ([Id],[UserId],[PurchaseDate],[ProductsPrice],[DiscountApplied],[FinalPrice],[PaymentMethodId]) VALUES ('22df2a3f-c07e-4187-a562-81e9a313e7f5','139bfcfa-14ec-41c2-81b9-dd4e64bdd2e9','2023-02-28 00:00:00',29.99,'Discount3',29.99,'21366911-ef98-4dfe-8627-2748159605d0')
INSERT [dbo].[Purchase] ([Id],[UserId],[PurchaseDate],[ProductsPrice],[DiscountApplied],[FinalPrice],[PaymentMethodId]) VALUES ('03df2a3f-c07e-4187-a562-81e9a313e7f5','9bc6bacb-7d29-41af-b4de-93d8c09c435a','2023-03-10 00:00:00',49.99,'Discount4',49.99,'21366912-ef98-4dfe-8627-2748159605d0')

INSERT [dbo].[Products] ([Id],[Price],[Name],[Description],[ProductCategory],[BrandId],[PurchaseId],[AvailableForPromotion],[Stock],[ImageURL]) VALUES ('40894ba6-48e6-4266-b45f-8c28a901c6f3',799.99,N'Cazadora corte normal',N'Cazadora normal sin ningun detalle particular',0,'159f068f-8662-4b98-b7d5-8923bfdef304','01df2a3f-c07e-4187-a562-81e9a313e7f5',1,12,'https://shorturl.at/auGLV')
INSERT [dbo].[Products] ([Id],[Price],[Name],[Description],[ProductCategory],[BrandId],[PurchaseId],[AvailableForPromotion],[Stock],[ImageURL]) VALUES ('30894ba6-48e6-4266-b45f-8c28a901c6f3',232.99,N'Camisa con flow(o swag)',N'Camisa propia para sacarle brillo a la pista de baile ',1,'159f068f-8662-4b98-b7d5-8923bfdef304','01df2a3f-c07e-4187-a562-81e9a313e7f5',1,23,'https://shorturl.at/gvJKU')
INSERT [dbo].[Products] ([Id],[Price],[Name],[Description],[ProductCategory],[BrandId],[PurchaseId],[AvailableForPromotion],[Stock],[ImageURL]) VALUES ('20894ba6-48e6-4266-b45f-8c28a901c6f3',123.99,N'Pantalon Azul desgastado',N'Vaquero super punk para romper la disco',2,'759f068f-8662-4b98-b7d5-8923bfdef304','12df2a3f-c07e-4187-a562-81e9a313e7f5',1,25,'https://shorturl.at/zCKO7')
INSERT [dbo].[Products] ([Id],[Price],[Name],[Description],[ProductCategory],[BrandId],[PurchaseId],[AvailableForPromotion],[Stock],[ImageURL]) VALUES ('10894ba6-48e6-4266-b45f-8c28a901c6f3',99.99,N'Bolso',N'Bolso ideal para viajes',3,'aeb62096-083a-4e68-9c65-7555f8da1139','03df2a3f-c07e-4187-a562-81e9a313e7f5',1,0,'https://shorturl.at/bqQSX')

INSERT [dbo].[Colors] ([Id],[Name],[ProductId]) VALUES ('8edae56b-4362-4dab-81b8-697aef2219b9',N'Red','40894ba6-48e6-4266-b45f-8c28a901c6f3')
INSERT [dbo].[Colors] ([Id],[Name],[ProductId]) VALUES	('7edae56b-4362-4dab-81b8-697aef2219b9',N'Blue','40894ba6-48e6-4266-b45f-8c28a901c6f3')
INSERT [dbo].[Colors] ([Id],[Name],[ProductId]) VALUES	('6edae56b-4362-4dab-81b8-697aef2219b9',N'Green','30894ba6-48e6-4266-b45f-8c28a901c6f3')
INSERT [dbo].[Colors] ([Id],[Name],[ProductId]) VALUES	('5d461de1-c6e3-479c-8a39-216109d53df5',N'Lime','20894ba6-48e6-4266-b45f-8c28a901c6f3')
INSERT [dbo].[Colors] ([Id],[Name],[ProductId]) VALUES	('3d461de1-c6e3-479c-8a39-216109d53df5',N'Indigo','20894ba6-48e6-4266-b45f-8c28a901c6f3')
INSERT [dbo].[Colors] ([Id],[Name],[ProductId]) VALUES	('1d461de1-c6e3-479c-8a39-216109d53df5',N'Turquoise','10894ba6-48e6-4266-b45f-8c28a901c6f3')

INSERT [dbo].[Roles] ([Id],[Name],[UserId]) VALUES ('b710ffd6-6546-4b70-b800-88e9f274c57a',N'Administrator','7da54593-1b75-45e8-a210-355f650910c5')
INSERT [dbo].[Roles] ([Id],[Name],[UserId]) VALUES	('ca1fa1f6-14db-4e1d-9f0e-bec53be9efb0',N'Administrator','7da54593-1b75-45e8-a210-355f650910c5')
INSERT [dbo].[Roles] ([Id],[Name],[UserId]) VALUES	('81f7bc64-8286-4785-9c8a-59e050c1f6b5',N'Moderator','139bfcfa-14ec-41c2-81b9-dd4e64bdd2e9')
INSERT [dbo].[Roles] ([Id],[Name],[UserId]) VALUES	('b0c65b7c-7554-44bc-8f5b-1f27c21c3b63',N'Editor','9bc6bacb-7d29-41af-b4de-93d8c09c435a')
INSERT [dbo].[Roles] ([Id],[Name],[UserId]) VALUES	('b50fad08-0317-4020-93ad-efec4dd04660',N'User','7da54593-1b75-45e8-a210-355f650910c5')
INSERT [dbo].[Roles] ([Id],[Name],[UserId]) VALUES	('1531e20a-0290-4f40-a194-0223894af557',N'Guest','9bc6bacb-7d29-41af-b4de-93d8c09c435a')
INSERT [dbo].[Roles] ([Id],[Name],[UserId]) VALUES	('1231e20a-0290-4f40-a194-0223894af557','Buyer','1bc6bacb-7d29-41af-b4de-93d8c09c435a')

/*Super Admin*/
  insert dbo.Users ([Id],[Name],[DeliveryAdress],[Password],[IsDeleted],[Email]) values ('25792766-4742-4284-a574-ce6bc5b11515','SUPER ADMIN','Direccion01','admin123',0,'admin@gmail.com')

  insert dbo.Roles ([Id],[Name],[UserId]) values ('c70a1902-d8f2-4818-9eee-27567f6b0273','SUPER ADMING PRIVILEGES','25792766-4742-4284-a574-ce6bc5b11515')

  insert [dbo].[Permissions] ([Id],[Name],[RoleId]) values ('eeaa6172-a8b8-4db2-b536-887b5d80dd99','GET/USERS','c70a1902-d8f2-4818-9eee-27567f6b0273')
  insert [dbo].[Permissions] ([Id],[Name],[RoleId]) values ('eeaa6162-a8b8-4db2-b536-887b5d80dd99','GET/USERS{ID}','c70a1902-d8f2-4818-9eee-27567f6b0273')
  insert [dbo].[Permissions] ([Id],[Name],[RoleId]) values ('eeaa6152-a8b8-4db2-b536-887b5d80dd99','DELETE/USERS','c70a1902-d8f2-4818-9eee-27567f6b0273')

  insert [dbo].[Permissions] ([Id],[Name],[RoleId]) values ('eeaa6142-a8b8-4db2-b536-887b5d80dd99','PUT/PRODUCT','c70a1902-d8f2-4818-9eee-27567f6b0273')
  insert [dbo].[Permissions] ([Id],[Name],[RoleId]) values ('eeaa6132-a8b8-4db2-b536-887b5d80dd99','POST/PRODUCT','c70a1902-d8f2-4818-9eee-27567f6b0273')
  insert [dbo].[Permissions] ([Id],[Name],[RoleId]) values ('eeaa6122-a8b8-4db2-b536-887b5d80dd99','DELETE/PRODUCT','c70a1902-d8f2-4818-9eee-27567f6b0273')

  insert [dbo].[Permissions] ([Id],[Name],[RoleId]) values ('eeaa6112-a8b8-4db2-b536-887b5d80dd99','GET/PURCHASE','c70a1902-d8f2-4818-9eee-27567f6b0273')
  insert [dbo].[Permissions] ([Id],[Name],[RoleId]) values ('eeaa6102-a8b8-4db2-b536-887b5d80dd99','GET/PURCHASE{ID}','c70a1902-d8f2-4818-9eee-27567f6b0273')
  insert [dbo].[Permissions] ([Id],[Name],[RoleId]) values ('eeaa6191-a8b8-4db2-b536-887b5d80dd99','POST/PURCHASE','c70a1902-d8f2-4818-9eee-27567f6b0273')


INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('217f6cd5-7f35-4ad2-8ba6-748fb369960d','POST/PURCHASE','1231e20a-0290-4f40-a194-0223894af557')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('eac32d67-4983-4283-96b4-da6586a5f01c','GET/PURCHASE{ID}','1231e20a-0290-4f40-a194-0223894af557')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('017f6cd5-7f35-4ad2-8ba6-748fb369960d',N'Post/Purchase','81f7bc64-8286-4785-9c8a-59e050c1f6b5')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('37175035-3220-43a3-ab54-016d3c5f7938',N'Read/Profile','81f7bc64-8286-4785-9c8a-59e050c1f6b5')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('37175035-3220-43a3-ab54-016d3c5f7937',N'Edit/Settings','b0c65b7c-7554-44bc-8f5b-1f27c21c3b63')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('37175035-3220-43a3-ab54-016d3c5f7936',N'Delete/Comment','b0c65b7c-7554-44bc-8f5b-1f27c21c3b63')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('37175035-3220-43a3-ab54-016d3c5f7935',N'Create/Event','81f7bc64-8286-4785-9c8a-59e050c1f6b5')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('37175035-3220-43a3-ab54-016d3c5f7934',N'View/Invoice','b50fad08-0317-4020-93ad-efec4dd04660')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('37175035-3220-43a3-ab54-016d3c5f7933',N'Manage/Users','b0c65b7c-7554-44bc-8f5b-1f27c21c3b63')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('37175035-3220-43a3-ab54-016d3c5f7932',N'Approve/Request','81f7bc64-8286-4785-9c8a-59e050c1f6b5')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('37175035-3220-43a3-ab54-016d3c5f7931',N'Send/Message','1531e20a-0290-4f40-a194-0223894af557')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('37175035-3220-43a3-ab54-016d3c5f7930',N'Review/Product','b50fad08-0317-4020-93ad-efec4dd04660')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('0696d467-b8b2-489d-88b0-38847eeb6722',N'Access/Dashboard','b50fad08-0317-4020-93ad-efec4dd04660')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('0696d467-b8b2-489d-88b0-38847eeb6721',N'Modify/Template','81f7bc64-8286-4785-9c8a-59e050c1f6b5')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('0696d467-b8b2-489d-88b0-38847eeb6720',N'Download/File','1531e20a-0290-4f40-a194-0223894af557')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('7d4fba9e-d7f6-469f-bf80-5b3e8c96e75e',N'Upload/Image','b50fad08-0317-4020-93ad-efec4dd04660')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('8d4fba9e-d7f6-469f-bf80-5b3e8c96e75e',N'Sync/Data','1531e20a-0290-4f40-a194-0223894af557')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('9d4fba9e-d7f6-469f-bf80-5b3e8c96e75e',N'Share/Link','81f7bc64-8286-4785-9c8a-59e050c1f6b5')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('6d4fba9e-d7f6-469f-bf80-5b3e8c96e75e',N'Lock/Document','1531e20a-0290-4f40-a194-0223894af557')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('5d4fba9e-d7f6-469f-bf80-5b3e8c96e75e',N'Unlock/Device','b0c65b7c-7554-44bc-8f5b-1f27c21c3b63')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('4d4fba9e-d7f6-469f-bf80-5b3e8c96e75e',N'Submit/Form','1531e20a-0290-4f40-a194-0223894af557')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('3d4fba9e-d7f6-469f-bf80-5b3e8c96e75e',N'Authorize/Payment','b0c65b7c-7554-44bc-8f5b-1f27c21c3b63')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('2d4fba9e-d7f6-469f-bf80-5b3e8c96e75e',N'Withdraw/Funds','1531e20a-0290-4f40-a194-0223894af557')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('1d4fba9e-d7f6-469f-bf80-5b3e8c96e75e',N'Execute/Script','1531e20a-0290-4f40-a194-0223894af557')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('0d4fba9e-d7f6-469f-bf80-5b3e8c96e75e',N'Grant/Permission','1531e20a-0290-4f40-a194-0223894af557')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('e5871d36-c222-4848-81d7-43fa1fd82098',N'Revoke/Access','b50fad08-0317-4020-93ad-efec4dd04660')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('e5871d36-c222-4848-81d7-43fa1fd82099',N'Print/Document','b0c65b7c-7554-44bc-8f5b-1f27c21c3b63')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('e5871d36-c222-4848-81d7-43fa1fd82090',N'Archive/Record','b0c65b7c-7554-44bc-8f5b-1f27c21c3b63')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('e5871d36-c222-4848-81d7-43fa1fd82097',N'Restore/Backup','ca1fa1f6-14db-4e1d-9f0e-bec53be9efb0')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('e5871d36-c222-4848-81d7-43fa1fd82096',N'Suspend/Account','b50fad08-0317-4020-93ad-efec4dd04660')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('e5871d36-c222-4848-81d7-43fa1fd82095',N'Resume/Service','b50fad08-0317-4020-93ad-efec4dd04660')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('e5871d36-c222-4848-81d7-43fa1fd82094',N'Close/Ticket','ca1fa1f6-14db-4e1d-9f0e-bec53be9efb0')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('e5871d36-c222-4848-81d7-43fa1fd82093',N'Cancel/Order','81f7bc64-8286-4785-9c8a-59e050c1f6b5')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('e5871d36-c222-4848-81d7-43fa1fd82092',N'Renew/Subscription','b710ffd6-6546-4b70-b800-88e9f274c57a')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('e5871d36-c222-4848-81d7-43fa1fd82091',N'Change/Password','b710ffd6-6546-4b70-b800-88e9f274c57a')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('ba49ac20-5e6f-4f21-9b54-36462da7c749',N'Reset/Security','81f7bc64-8286-4785-9c8a-59e050c1f6b5')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('ba49ac20-5e6f-4f21-9b54-36462da7c740',N'Verify/Identity','ca1fa1f6-14db-4e1d-9f0e-bec53be9efb0')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('ba49ac20-5e6f-4f21-9b54-36462da7c748',N'Upgrade/Plan','b710ffd6-6546-4b70-b800-88e9f274c57a')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('ba49ac20-5e6f-4f21-9b54-36462da7c747',N'Downgrade/Version','b710ffd6-6546-4b70-b800-88e9f274c57a')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('ba49ac20-5e6f-4f21-9b54-36462da7c746',N'Install/Application','ca1fa1f6-14db-4e1d-9f0e-bec53be9efb0')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('ba49ac20-5e6f-4f21-9b54-36462da7c745',N'Uninstall/Software','b710ffd6-6546-4b70-b800-88e9f274c57a')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('EEAA6162-A8B2-4DB2-B536-887B5D80DD99','POST/DISCOUNT','C70A1902-D8F2-4818-9EEE-27567F6B0273')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('EEAA6162-A8B0-4DB2-B536-887B5D80DD99','PUT/DISCOUNT','C70A1902-D8F2-4818-9EEE-27567F6B0273')
INSERT [dbo].[Permissions] ([Id],[Name],[RoleId]) VALUES	('EEAA6162-A8B9-4DB2-B536-887B5D80DD99','DELETE/DISCOUNT','C70A1902-D8F2-4818-9EEE-27567F6B0273')


INSERT [dbo].[QuantityDiscounts] ([Id],[Name],[ProductCategory],[MinProductsNeededForDiscount],[NumberOfProductsToBeFree],[ProductToBeDiscounted],[isActive])VALUES ('78a3d5fc-0690-4f1a-939c-d6741ce7ca55',N'BulkDiscount1',7,10,2,N'MaxValue',1)
INSERT [dbo].[QuantityDiscounts] ([Id],[Name],[ProductCategory],[MinProductsNeededForDiscount],[NumberOfProductsToBeFree],[ProductToBeDiscounted],[isActive])VALUES ('58a3d5fc-0690-4f1a-939c-d6741ce7ca55',N'ClearanceSale',6,5,1,N'MinValue',1)
INSERT [dbo].[QuantityDiscounts] ([Id],[Name],[ProductCategory],[MinProductsNeededForDiscount],[NumberOfProductsToBeFree],[ProductToBeDiscounted],[isActive])VALUES ('68a3d5fc-0690-4f1a-939c-d6741ce7ca55',N'HolidaySpecial',5,8,3,N'MaxValue',0)

INSERT [dbo].[PercentageDiscounts] ([Id],[Name],[PercentageDiscounted],[ProductToBeDiscounted],[MinProductsNeededForDiscount],[isActive])VALUES ('f9756830-aa49-45c2-988a-1da4822a6f9d',N'SummerSale',0.15,N'MaxValue',5,1)
INSERT [dbo].[PercentageDiscounts] ([Id],[Name],[PercentageDiscounted],[ProductToBeDiscounted],[MinProductsNeededForDiscount],[isActive])VALUES ('f8756830-aa49-45c2-988a-1da4822a6f9d',N'FlashDeal',0.20,N'MinValue',3,0)
INSERT [dbo].[PercentageDiscounts] ([Id],[Name],[PercentageDiscounted],[ProductToBeDiscounted],[MinProductsNeededForDiscount],[isActive])VALUES ('f7756830-aa49-45c2-988a-1da4822a6f9d',N'YearEndClearance',0.30,N'MaxValue',10,1)

INSERT [dbo].[BrandDiscounts]([Id],[Name],[BrandId],[MinProductsForPromotion],[NumberOfProductsForFree],[ProductToBeDiscounted],[isActive])VALUES ('eac40bd9-0eba-4b5c-9d2d-18d658e714c8',N'BrandPromo1','959f068f-8662-4b98-b7d5-8923bfdef304',8,2,N'MaxValue',1)
INSERT [dbo].[BrandDiscounts]([Id],[Name],[BrandId],[MinProductsForPromotion],[NumberOfProductsForFree],[ProductToBeDiscounted],[isActive])VALUES	('eac30bd9-0eba-4b5c-9d2d-18d658e714c8',N'BrandPromo2','859f068f-8662-4b98-b7d5-8923bfdef304',6,1,N'MinValue',0)
INSERT [dbo].[BrandDiscounts]([Id],[Name],[BrandId],[MinProductsForPromotion],[NumberOfProductsForFree],[ProductToBeDiscounted],[isActive])VALUES	('eac20bd9-0eba-4b5c-9d2d-18d658e714c8',N'BrandPromo3','759f068f-8662-4b98-b7d5-8923bfdef304',10,3,N'MaxValue',1)

INSERT [dbo].[ColorDiscounts] ([Id],[Name],[ColorId],[PercentageDiscount],[ProductToBeDiscounted],[MinProductsNeededForDiscount],[isActive]) VALUES ('2ce4b9b2-c6de-4a64-89f7-4f3947415f1c',N'ColorPromo1','8edae56b-4362-4dab-81b8-697aef2219b9',0.125,N'MaxValue',5,1)
INSERT [dbo].[ColorDiscounts] ([Id],[Name],[ColorId],[PercentageDiscount],[ProductToBeDiscounted],[MinProductsNeededForDiscount],[isActive]) VALUES ('3ce4b9b2-c6de-4a64-89f7-4f3947415f1c',N'ColorPromo2','5d461de1-c6e3-479c-8a39-216109d53df5',0.18,N'MinValue',8,0)
INSERT [dbo].[ColorDiscounts] ([Id],[Name],[ColorId],[PercentageDiscount],[ProductToBeDiscounted],[MinProductsNeededForDiscount],[isActive]) VALUES ('4ce4b9b2-c6de-4a64-89f7-4f3947415f1c',N'ColorPromo3','1d461de1-c6e3-479c-8a39-216109d53df5',0.25,N'MaxValue',10,1)

