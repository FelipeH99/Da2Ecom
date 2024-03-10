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
