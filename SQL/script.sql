USE [Order]
GO
/****** Object:  Table [dbo].[FoodOrder]    Script Date: 15/2/2020 13:17:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[FoodID] [int] NOT NULL,
	[FoodNumber] [int] NOT NULL,
	[Money] [decimal](19, 4) NOT NULL,
	[ISPlay] [int] NOT NULL,
	[CreateTime] [datetime] NULL,
	[Img] [nvarchar](250) NULL,
	[MenuName] [nvarchar](150) NULL,
	[address] [nvarchar](max) NULL,
 CONSTRAINT [PK_FoodOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 15/2/2020 13:17:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Price] [decimal](19, 4) NULL,
	[Number] [int] NULL,
	[Img] [nvarchar](255) NULL,
	[DetailedInformation] [nvarchar](max) NULL,
	[State] [int] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 15/2/2020 13:17:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UserPwd] [nvarchar](50) NOT NULL,
	[money] [decimal](19, 4) NULL,
	[mailbox] [nvarchar](50) NULL,
	[State] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
