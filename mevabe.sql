USE [master]
GO
/****** Object:  Database [MeVaBe]    Script Date: 7/22/2024 10:18:34 PM ******/
CREATE DATABASE [MeVaBe]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MeVaBe', FILENAME = N'D:\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MeVaBe.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MeVaBe_log', FILENAME = N'D:\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MeVaBe_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MeVaBe] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MeVaBe].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MeVaBe] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MeVaBe] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MeVaBe] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MeVaBe] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MeVaBe] SET ARITHABORT OFF 
GO
ALTER DATABASE [MeVaBe] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MeVaBe] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MeVaBe] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MeVaBe] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MeVaBe] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MeVaBe] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MeVaBe] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MeVaBe] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MeVaBe] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MeVaBe] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MeVaBe] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MeVaBe] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MeVaBe] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MeVaBe] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MeVaBe] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MeVaBe] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MeVaBe] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MeVaBe] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MeVaBe] SET  MULTI_USER 
GO
ALTER DATABASE [MeVaBe] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MeVaBe] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MeVaBe] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MeVaBe] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MeVaBe] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MeVaBe] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MeVaBe] SET QUERY_STORE = ON
GO
ALTER DATABASE [MeVaBe] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MeVaBe]
GO
/****** Object:  Table [dbo].[tbl_blog]    Script Date: 7/22/2024 10:18:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_blog](
	[id] [int] NOT NULL,
	[title] [nvarchar](255) NULL,
	[content] [nvarchar](255) NULL,
	[image] [nvarchar](255) NULL,
	[create_date] [date] NULL,
	[blog_category_id] [int] NOT NULL,
 CONSTRAINT [PK_tbl_blog_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_blog_category]    Script Date: 7/22/2024 10:18:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_blog_category](
	[id] [int] NOT NULL,
	[name] [nvarchar](255) NULL,
	[description] [nvarchar](255) NULL,
 CONSTRAINT [PK_tbl_blog_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_cart]    Script Date: 7/22/2024 10:18:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_cart](
	[id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[total] [float] NULL,
 CONSTRAINT [PK_tbl_cart_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_cart_item]    Script Date: 7/22/2024 10:18:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_cart_item](
	[id] [int] NOT NULL,
	[cart_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[quanity] [int] NULL,
 CONSTRAINT [PK_tbl_cart_item_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_feedback]    Script Date: 7/22/2024 10:18:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_feedback](
	[id] [int] NOT NULL,
	[content] [nvarchar](255) NULL,
	[rate] [float] NULL,
	[create_date] [date] NULL,
	[user_id] [int] NULL,
	[product_id] [int] NULL,
 CONSTRAINT [PK_tbl_feedback] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_order_detail]    Script Date: 7/22/2024 10:18:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_order_detail](
	[id] [int] NOT NULL,
	[payment_id] [int] NOT NULL,
	[total] [float] NULL,
	[order_satus] [nvarchar](255) NULL,
	[create_date] [date] NULL,
	[end_date] [date] NULL,
	[user_id] [int] NOT NULL,
	[phone] [nvarchar](255) NULL,
 CONSTRAINT [PK_tbl_order_detail_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_order_item]    Script Date: 7/22/2024 10:18:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_order_item](
	[id] [int] NOT NULL,
	[order_detail_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[quantity] [int] NULL,
	[price] [float] NULL,
 CONSTRAINT [PK_tbl_order_item_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_payment]    Script Date: 7/22/2024 10:18:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_payment](
	[id] [int] NOT NULL,
	[type_payment] [nvarchar](255) NULL,
	[total] [float] NULL,
	[order_detail_id] [int] NOT NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_tbl_payment_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_product]    Script Date: 7/22/2024 10:18:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_product](
	[id] [int] NOT NULL,
	[product_name] [nvarchar](255) NULL,
	[cover_image] [nvarchar](255) NULL,
	[price] [float] NULL,
	[status] [bit] NULL,
	[description] [nvarchar](255) NULL,
	[product_category_id] [int] NOT NULL,
 CONSTRAINT [PK_tbl_product_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_product_category]    Script Date: 7/22/2024 10:18:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_product_category](
	[id] [int] NOT NULL,
	[name] [nvarchar](255) NULL,
 CONSTRAINT [PK_tbl_product_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_product_sub_image]    Script Date: 7/22/2024 10:18:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_product_sub_image](
	[id] [int] NOT NULL,
	[url] [nvarchar](255) NULL,
	[product_id] [int] NOT NULL,
 CONSTRAINT [PK_tbl_product_sub_image_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_user]    Script Date: 7/22/2024 10:18:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_user](
	[id] [int] NOT NULL,
	[email] [nvarchar](255) NULL,
	[phone] [nvarchar](255) NULL,
	[avatar] [nvarchar](255) NULL,
	[dob] [date] NULL,
	[status] [bit] NULL,
	[password] [nvarchar](255) NULL,
	[fullname] [nvarchar](255) NULL,
	[role] [nvarchar](255) NULL,
 CONSTRAINT [PK_tbl_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_user_address]    Script Date: 7/22/2024 10:18:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_user_address](
	[id] [int] NOT NULL,
	[user_id] [int] NULL,
	[province] [nvarchar](255) NULL,
	[district] [nvarchar](255) NULL,
	[ward] [nvarchar](255) NULL,
	[address] [nvarchar](255) NULL,
 CONSTRAINT [PK_tbl_user_address] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tbl_cart] ([id], [user_id], [total]) VALUES (1, 1, 0)
GO
INSERT [dbo].[tbl_feedback] ([id], [content], [rate], [create_date], [user_id], [product_id]) VALUES (1, N'oke', 3, CAST(N'2024-07-19' AS Date), 1, 1)
INSERT [dbo].[tbl_feedback] ([id], [content], [rate], [create_date], [user_id], [product_id]) VALUES (2, N'tốt cho bé', 4, CAST(N'2024-07-19' AS Date), 1, 1)
GO
INSERT [dbo].[tbl_order_detail] ([id], [payment_id], [total], [order_satus], [create_date], [end_date], [user_id], [phone]) VALUES (1, 0, 938000, N'Complete', CAST(N'2024-05-17' AS Date), CAST(N'2024-06-20' AS Date), 1, N'227 Hùng Vương, KP1,Thị trấn Tân Thạnh,Tân Thạnh,Long An')
INSERT [dbo].[tbl_order_detail] ([id], [payment_id], [total], [order_satus], [create_date], [end_date], [user_id], [phone]) VALUES (2, 0, 469000, N'Pending', CAST(N'2024-06-17' AS Date), CAST(N'2024-06-20' AS Date), 1, N'227 Hùng Vương, KP1,Thị trấn Tân Thạnh,Tân Thạnh,Long An')
INSERT [dbo].[tbl_order_detail] ([id], [payment_id], [total], [order_satus], [create_date], [end_date], [user_id], [phone]) VALUES (3, 0, 938000, N'Pending', CAST(N'2024-07-17' AS Date), CAST(N'2024-06-20' AS Date), 1, N'227 Hùng Vương, KP1,Thị trấn Tân Thạnh,Tân Thạnh,Long An')
INSERT [dbo].[tbl_order_detail] ([id], [payment_id], [total], [order_satus], [create_date], [end_date], [user_id], [phone]) VALUES (4, 0, 469000, N'Pending', CAST(N'2024-07-17' AS Date), CAST(N'2024-07-20' AS Date), 1, N'227 Hùng Vương, KP1,Thị trấn Tân Thạnh,Tân Thạnh,Long An')
INSERT [dbo].[tbl_order_detail] ([id], [payment_id], [total], [order_satus], [create_date], [end_date], [user_id], [phone]) VALUES (5, 0, 1876000, N'Complete', CAST(N'2024-08-19' AS Date), CAST(N'2024-07-22' AS Date), 1, N'227 Hùng Vương, KP1,Thị trấn Tân Thạnh,Tân Thạnh,Long An')
INSERT [dbo].[tbl_order_detail] ([id], [payment_id], [total], [order_satus], [create_date], [end_date], [user_id], [phone]) VALUES (6, 0, 790800, N'Complete', CAST(N'2024-08-21' AS Date), CAST(N'2024-08-24' AS Date), 1, N'227 Hùng Vương, KP1,Thị trấn Tân Thạnh,Tân Thạnh,Long An')
INSERT [dbo].[tbl_order_detail] ([id], [payment_id], [total], [order_satus], [create_date], [end_date], [user_id], [phone]) VALUES (7, 0, 938000, N'Pending', CAST(N'2024-07-22' AS Date), CAST(N'2024-07-25' AS Date), 1, N'227 Hùng Vương, KP1,Thị trấn Tân Thạnh,Tân Thạnh,Long An')
GO
INSERT [dbo].[tbl_order_item] ([id], [order_detail_id], [product_id], [quantity], [price]) VALUES (1, 1, 1, 2, 469000)
INSERT [dbo].[tbl_order_item] ([id], [order_detail_id], [product_id], [quantity], [price]) VALUES (2, 2, 1, 1, 469000)
INSERT [dbo].[tbl_order_item] ([id], [order_detail_id], [product_id], [quantity], [price]) VALUES (3, 3, 1, 2, 469000)
INSERT [dbo].[tbl_order_item] ([id], [order_detail_id], [product_id], [quantity], [price]) VALUES (4, 4, 1, 1, 469000)
INSERT [dbo].[tbl_order_item] ([id], [order_detail_id], [product_id], [quantity], [price]) VALUES (5, 5, 1, 2, 469000)
INSERT [dbo].[tbl_order_item] ([id], [order_detail_id], [product_id], [quantity], [price]) VALUES (6, 6, 17, 5, 99000)
INSERT [dbo].[tbl_order_item] ([id], [order_detail_id], [product_id], [quantity], [price]) VALUES (7, 6, 11, 3, 98600)
INSERT [dbo].[tbl_order_item] ([id], [order_detail_id], [product_id], [quantity], [price]) VALUES (8, 7, 1, 2, 469000)
GO
INSERT [dbo].[tbl_payment] ([id], [type_payment], [total], [order_detail_id], [status]) VALUES (1, N'COD', 938000, 1, 0)
INSERT [dbo].[tbl_payment] ([id], [type_payment], [total], [order_detail_id], [status]) VALUES (2, N'COD', 469000, 2, 0)
INSERT [dbo].[tbl_payment] ([id], [type_payment], [total], [order_detail_id], [status]) VALUES (3, N'COD', 938000, 3, 0)
INSERT [dbo].[tbl_payment] ([id], [type_payment], [total], [order_detail_id], [status]) VALUES (4, N'VNPay', 469000, 4, 0)
INSERT [dbo].[tbl_payment] ([id], [type_payment], [total], [order_detail_id], [status]) VALUES (5, N'VNPay', 1876000, 5, 0)
INSERT [dbo].[tbl_payment] ([id], [type_payment], [total], [order_detail_id], [status]) VALUES (6, N'VNPay', 790800, 6, 0)
INSERT [dbo].[tbl_payment] ([id], [type_payment], [total], [order_detail_id], [status]) VALUES (7, N'VNPay', 938000, 7, 1)
GO
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (1, N'Sữa Nestle Nan Optipro Plus 3 800g (1-2 tuổi)', N'Optipro_3.png', 469000, 1, N'Sữa Nestle Nan Optipro Plus 3 800g (1-2 tuổi)', 1)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (2, N'Sữa Nestle Nan Optipro Plus 4 800g (2-6 tuổi)', N'Optipro_4.jpg', 105000, 1, N'Sữa Morinaga số 1 dạng thanh 130g (Hagukumi, 0-6 tháng)
', 1)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (3, N'Sữa Nan A2 InfiniPro 400g số 2 (1-2 tuổi)', N'sua-nan-a2-infinipro-400g-so-2-1-2-tuoi.jpg', 385000, 1, N'Sữa Nan A2 InfiniPro 400g số 2 (1-2 tuổi)', 1)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (4, N'Sữa Bubs Supreme Junior Nutrition 800g (3-12 tuổi)', N'review-sua-bubs-supreme-junior.webp', 780000, 1, N'Sữa Bubs Supreme Junior Nutrition 800g (3-12 tuổi)', 1)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (5, N'Sữa Enfagrow A+ số 4 1700g (2-6 tuổi) 2Flex', N'sua-enfagrow-a-4-360-brain-dha-vo-i-mfgm-pro-1700g-.jpg', 835000, 1, N'Sữa Enfagrow A+ số 4 1700g (2-6 tuổi) 2Flex', 1)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (6, N'Tã quần Merries Ultra Jumbo (L, 9-14kg, 56 miếng)
', N'merries_Jumbo.webp', 387000, 1, N'Tã quần Merries Ultra Jumbo (L, 9-14kg, 56 miếng)
', 2)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (7, N'Tã quần Merries (M, 6-11kg, 52 miếng)
', N'Merries_M.webp', 346500, 1, N'Tã quần Merries (M, 6-11kg, 52 miếng)
', 2)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (8, N'Tã dán Merries (L, 9-14kg, 48 miếng)
', N'ta-dan-merries-size-l-48-mieng-9-14-kg-1.jpg', 346500, 1, N'Tã dán Merries (L, 9-14kg, 48 miếng)
', 2)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (9, N'Tã dán Merries (Newborn, dưới 5kg, 76 miếng)
', N'Merries_Newborn.jpg', 346500, 1, N'Tã dán Merries (Newborn, dưới 5kg, 76 miếng)
', 2)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (10, N'Tã dán Merries (S, 4-8kg, 70 miếng)
', N'Merries_S.jpg', 346500, 1, N'Tã dán Merries (S, 4-8kg, 70 miếng)
', 2)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (11, N'Sữa Hữu Cơ Sangha Farm Vị Tự Nhiên', N'sua-huu-co-sangha-farm-vi-tu-nhien.png', 98600, 1, N'Sữa Hữu Cơ Sangha Farm Vị Tự Nhiên', 3)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (12, N'Sữa chua hoa quả Helio vị Dâu', N'pho-mai-tuoi-trai-cay-helio-vi-vani-dau-2.jpg', 52000, 1, N'Sữa chua hoa quả Helio vị Dâu', 3)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (13, N'Sữa chua hoa quả Helio vị Dâu', N'pho-mai-tuoi-trai-cay-helio-vi-vani-dau-2.jpg', 171000, 1, N'Thực phẩm bảo vệ sức khỏe - Nước yến Kids Nest Plus+ Hương Tự nhiên (Lốc 6 lọ)', 3)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (14, N'Combo 2 TUI NUOC YEN GENNEST 105ML 5% Trẻ em Hương Táo
', N'Gennest_Apple.jpg', 29000, 1, N'Combo 2 TUI NUOC YEN GENNEST 105ML 5% Trẻ em Hương Táo
', 3)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (15, N'Combo 4 Sữa tươi tiệt trùng Oldenburger ít đường 180ml (lốc 4 hộp)
', N'sua-tuoi-tiet-trung-oldenburger-it-duong-180ml-loc-4-hop.png', 112000, 1, N'Combo 4 Sữa tươi tiệt trùng Oldenburger ít đường 180ml (lốc 4 hộp)
', 3)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (16, N'Dầu cá hồi Ecologyfood', N'dau-ca-hoi-nguyen-chat-ecofood-150ml-1.jpg', 168000, 1, N'Dầu cá hồi Ecologyfood', 4)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (17, N'Chà bông heo Goldkids 50gr', N'cha-bong-heo-goldkids-50gr.png', 99000, 1, N'Chà bông heo Goldkids 50gr', 4)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (18, N'Combo 2 Nêm cá ngừ MARUTOMO', N'MarutoMo.webp', 58000, 1, N'Combo 2 Nêm cá ngừ MARUTOMO', 4)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (19, N'Combo 2 Mì nui trứng Egg Pasta hình khủng long 90g', N'Egg_Pasta.jpg', 74000, 1, N'Combo 2 Mì nui trứng Egg Pasta hình khủng long 90g', 4)
INSERT [dbo].[tbl_product] ([id], [product_name], [cover_image], [price], [status], [description], [product_category_id]) VALUES (20, N'Combo 2 Xúc Xích Tiệt Trùng Goldkids Cá hồi & Phô Mai', N'Xuc_Xich_GOldKids.png', 82600, 1, N'Combo 2 Xúc Xích Tiệt Trùng Goldkids Cá hồi & Phô Mai', 4)
GO
INSERT [dbo].[tbl_product_category] ([id], [name]) VALUES (1, N'Sữa bột')
INSERT [dbo].[tbl_product_category] ([id], [name]) VALUES (2, N'Bỉm tã')
INSERT [dbo].[tbl_product_category] ([id], [name]) VALUES (3, N'Sữa tươi')
INSERT [dbo].[tbl_product_category] ([id], [name]) VALUES (4, N'Ăn dặm, dinh dưỡng')
INSERT [dbo].[tbl_product_category] ([id], [name]) VALUES (5, N'Vitamin & Sức khỏe')
INSERT [dbo].[tbl_product_category] ([id], [name]) VALUES (6, N'Đồ dùng mẹ')
GO
INSERT [dbo].[tbl_user] ([id], [email], [phone], [avatar], [dob], [status], [password], [fullname], [role]) VALUES (1, N'thang2k2la@gmail.com', N'0822818119', N'null', CAST(N'2002-10-04' AS Date), 1, N'12345', N'Đặng Minh Thắng', N'USER')
GO
INSERT [dbo].[tbl_user_address] ([id], [user_id], [province], [district], [ward], [address]) VALUES (0, 1, N'TEST', N'TEST', N'TEST', N'TEST')
INSERT [dbo].[tbl_user_address] ([id], [user_id], [province], [district], [ward], [address]) VALUES (1, 1, N'Long An', N'Tân Thạnh', N'Thị trấn Tân Thạnh', N'227 Hùng Vương, KP1')
INSERT [dbo].[tbl_user_address] ([id], [user_id], [province], [district], [ward], [address]) VALUES (2, 1, N'TP.HCM', N'Quận 9', NULL, N'KTX Khu B ĐHQG ')
GO
ALTER TABLE [dbo].[tbl_blog]  WITH CHECK ADD  CONSTRAINT [FK_tbl_blog_tbl_blog_category] FOREIGN KEY([blog_category_id])
REFERENCES [dbo].[tbl_blog_category] ([id])
GO
ALTER TABLE [dbo].[tbl_blog] CHECK CONSTRAINT [FK_tbl_blog_tbl_blog_category]
GO
ALTER TABLE [dbo].[tbl_cart]  WITH CHECK ADD  CONSTRAINT [FK_tbl_cart_tbl_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[tbl_user] ([id])
GO
ALTER TABLE [dbo].[tbl_cart] CHECK CONSTRAINT [FK_tbl_cart_tbl_user]
GO
ALTER TABLE [dbo].[tbl_cart_item]  WITH CHECK ADD  CONSTRAINT [FK_tbl_cart_item_tbl_cart] FOREIGN KEY([cart_id])
REFERENCES [dbo].[tbl_cart] ([id])
GO
ALTER TABLE [dbo].[tbl_cart_item] CHECK CONSTRAINT [FK_tbl_cart_item_tbl_cart]
GO
ALTER TABLE [dbo].[tbl_cart_item]  WITH CHECK ADD  CONSTRAINT [FK_tbl_cart_item_tbl_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[tbl_product] ([id])
GO
ALTER TABLE [dbo].[tbl_cart_item] CHECK CONSTRAINT [FK_tbl_cart_item_tbl_product]
GO
ALTER TABLE [dbo].[tbl_feedback]  WITH CHECK ADD  CONSTRAINT [FK_tbl_feedback_tbl_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[tbl_product] ([id])
GO
ALTER TABLE [dbo].[tbl_feedback] CHECK CONSTRAINT [FK_tbl_feedback_tbl_product]
GO
ALTER TABLE [dbo].[tbl_feedback]  WITH CHECK ADD  CONSTRAINT [FK_tbl_feedback_tbl_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[tbl_user] ([id])
GO
ALTER TABLE [dbo].[tbl_feedback] CHECK CONSTRAINT [FK_tbl_feedback_tbl_user]
GO
ALTER TABLE [dbo].[tbl_order_detail]  WITH CHECK ADD  CONSTRAINT [FK_tbl_order_detail_tbl_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[tbl_user] ([id])
GO
ALTER TABLE [dbo].[tbl_order_detail] CHECK CONSTRAINT [FK_tbl_order_detail_tbl_user]
GO
ALTER TABLE [dbo].[tbl_order_item]  WITH CHECK ADD  CONSTRAINT [FK_tbl_order_item_tbl_order_detail] FOREIGN KEY([order_detail_id])
REFERENCES [dbo].[tbl_order_detail] ([id])
GO
ALTER TABLE [dbo].[tbl_order_item] CHECK CONSTRAINT [FK_tbl_order_item_tbl_order_detail]
GO
ALTER TABLE [dbo].[tbl_order_item]  WITH CHECK ADD  CONSTRAINT [FK_tbl_order_item_tbl_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[tbl_product] ([id])
GO
ALTER TABLE [dbo].[tbl_order_item] CHECK CONSTRAINT [FK_tbl_order_item_tbl_product]
GO
ALTER TABLE [dbo].[tbl_payment]  WITH CHECK ADD  CONSTRAINT [FK_tbl_payment_tbl_order_detail] FOREIGN KEY([order_detail_id])
REFERENCES [dbo].[tbl_order_detail] ([id])
GO
ALTER TABLE [dbo].[tbl_payment] CHECK CONSTRAINT [FK_tbl_payment_tbl_order_detail]
GO
ALTER TABLE [dbo].[tbl_product]  WITH CHECK ADD  CONSTRAINT [FK_tbl_product_tbl_product_category] FOREIGN KEY([product_category_id])
REFERENCES [dbo].[tbl_product_category] ([id])
GO
ALTER TABLE [dbo].[tbl_product] CHECK CONSTRAINT [FK_tbl_product_tbl_product_category]
GO
ALTER TABLE [dbo].[tbl_product_sub_image]  WITH CHECK ADD  CONSTRAINT [FK_tbl_product_sub_image_tbl_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[tbl_product] ([id])
GO
ALTER TABLE [dbo].[tbl_product_sub_image] CHECK CONSTRAINT [FK_tbl_product_sub_image_tbl_product]
GO
ALTER TABLE [dbo].[tbl_user_address]  WITH CHECK ADD  CONSTRAINT [FK_tbl_user_address_tbl_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[tbl_user] ([id])
GO
ALTER TABLE [dbo].[tbl_user_address] CHECK CONSTRAINT [FK_tbl_user_address_tbl_user]
GO
USE [master]
GO
ALTER DATABASE [MeVaBe] SET  READ_WRITE 
GO
