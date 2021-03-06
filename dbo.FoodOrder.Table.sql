USE [Order]
GO
/****** Object:  Table [dbo].[FoodOrder]    Script Date: 15/2/2020 13:21:19 ******/
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
