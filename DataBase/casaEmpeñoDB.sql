USE [CasaDeEmpeñoDB]
GO
/****** Object:  Table [dbo].[ConfiguracionVencimiento]    Script Date: 2/6/2024 2:06:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConfiguracionVencimiento](
	[idConfiguracionVencimiento] [int] IDENTITY(1,1) NOT NULL,
	[tiempoVencimiento] [varchar](50) NULL,
	[esActual] [int] NULL,
 CONSTRAINT [PK_ConfiguracionVencimiento] PRIMARY KEY CLUSTERED 
(
	[idConfiguracionVencimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Devolucion]    Script Date: 2/6/2024 2:06:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Devolucion](
	[idDevolucion] [int] IDENTITY(1,1) NOT NULL,
	[comentarioDevolucion] [varchar](max) NULL,
	[fechaDevolucion] [datetime] NULL,
	[idProducto] [int] NULL,
 CONSTRAINT [PK_Devolucion] PRIMARY KEY CLUSTERED 
(
	[idDevolucion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Oferta]    Script Date: 2/6/2024 2:06:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Oferta](
	[idOferta] [int] IDENTITY(1,1) NOT NULL,
	[nombrePersonaOferta] [varchar](max) NULL,
	[numeroCelular] [varchar](50) NOT NULL,
	[montoOferta] [float] NULL,
	[idVenta] [int] NULL,
 CONSTRAINT [PK_Oferta] PRIMARY KEY CLUSTERED 
(
	[idOferta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 2/6/2024 2:06:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[idProducto] [int] IDENTITY(1,1) NOT NULL,
	[nombreProducto] [varchar](max) NULL,
	[fechaDeIngreso] [datetime] NULL,
	[estadoProducto] [varchar](max) NULL,
	[valorCalculado] [float] NULL,
	[idTipoProducto] [int] NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoProducto]    Script Date: 2/6/2024 2:06:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoProducto](
	[idTipoProducto] [int] IDENTITY(1,1) NOT NULL,
	[tipoProducto] [varchar](max) NULL,
 CONSTRAINT [PK_TipoProducto] PRIMARY KEY CLUSTERED 
(
	[idTipoProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 2/6/2024 2:06:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[correo] [varchar](50) NULL,
	[password] [varchar](250) NULL,
	[tipoUsuario] [varchar](50) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 2/6/2024 2:06:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta](
	[idVenta] [int] IDENTITY(1,1) NOT NULL,
	[precioVenta] [float] NULL,
	[precioEnQueSeVendio] [float] NULL,
	[vendido] [int] NOT NULL,
	[idProducto] [int] NULL,
 CONSTRAINT [PK_Venta] PRIMARY KEY CLUSTERED 
(
	[idVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ConfiguracionVencimiento] ON 

INSERT [dbo].[ConfiguracionVencimiento] ([idConfiguracionVencimiento], [tiempoVencimiento], [esActual]) VALUES (1, N'48:00:00', 0)
INSERT [dbo].[ConfiguracionVencimiento] ([idConfiguracionVencimiento], [tiempoVencimiento], [esActual]) VALUES (2, N'48:00:00', NULL)
INSERT [dbo].[ConfiguracionVencimiento] ([idConfiguracionVencimiento], [tiempoVencimiento], [esActual]) VALUES (3, N'48:00:00', 0)
INSERT [dbo].[ConfiguracionVencimiento] ([idConfiguracionVencimiento], [tiempoVencimiento], [esActual]) VALUES (4, N'48:00:00', 1)
SET IDENTITY_INSERT [dbo].[ConfiguracionVencimiento] OFF
GO
SET IDENTITY_INSERT [dbo].[Devolucion] ON 

INSERT [dbo].[Devolucion] ([idDevolucion], [comentarioDevolucion], [fechaDevolucion], [idProducto]) VALUES (1, N'Se arrepintio de venderlo', CAST(N'2024-02-04T23:48:25.000' AS DateTime), NULL)
INSERT [dbo].[Devolucion] ([idDevolucion], [comentarioDevolucion], [fechaDevolucion], [idProducto]) VALUES (4, N'Comentario ejemplo', CAST(N'2024-02-05T17:05:07.000' AS DateTime), NULL)
INSERT [dbo].[Devolucion] ([idDevolucion], [comentarioDevolucion], [fechaDevolucion], [idProducto]) VALUES (5, N'Se arrepintio de vender', CAST(N'2024-02-05T19:27:18.000' AS DateTime), NULL)
INSERT [dbo].[Devolucion] ([idDevolucion], [comentarioDevolucion], [fechaDevolucion], [idProducto]) VALUES (6, N'Se arrepintio de vender', CAST(N'2024-02-06T01:05:02.000' AS DateTime), NULL)
INSERT [dbo].[Devolucion] ([idDevolucion], [comentarioDevolucion], [fechaDevolucion], [idProducto]) VALUES (7, N'Se arrepintio de vender', CAST(N'2024-02-06T01:06:38.000' AS DateTime), NULL)
INSERT [dbo].[Devolucion] ([idDevolucion], [comentarioDevolucion], [fechaDevolucion], [idProducto]) VALUES (8, N'Cliente volvió por el', CAST(N'2024-02-06T01:34:43.000' AS DateTime), 10)
SET IDENTITY_INSERT [dbo].[Devolucion] OFF
GO
SET IDENTITY_INSERT [dbo].[Oferta] ON 

INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (1, N'Aldo Colorado', N'2731125239', 500, 1)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (2, N'Aldo Colorado', N'2731125239', 800, 1)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (3, N'Aldo Colorado', N'2731125239', 500, 1)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (4, N'Aldo Colorado', N'2731125239', 500, 11)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (5, N'Aldo Colorado', N'2731125239', 501, 11)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (6, N'Aldo Colorado', N'2731125239', 500, 11)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (7, N'Aldo Colorado', N'2731125239', 500, 12)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (8, N'Aldo Colorado', N'2731125239', 5000, 13)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (9, N'Aldo Colorado', N'2731125239', 5001, 13)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (10, N'Aldo Colorado', N'2731125239', 5099, 13)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (11, N'Aldo Díaz', N'2701125239', 5000, 14)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (12, N'Aldo Díaz', N'2701125239', 5000, 14)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (13, N'Aldo Colorado', N'2731125239', 6000, 14)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (14, N'Aldo Colorado Díaz', N'2731125239', 15000, 16)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (15, N'Albert Turner', N'2731124585', 700, 15)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (16, N'Aldo Colorado', N'2455554589', 800, 15)
INSERT [dbo].[Oferta] ([idOferta], [nombrePersonaOferta], [numeroCelular], [montoOferta], [idVenta]) VALUES (17, N'Luis Perez', N'7888554859', 15000, 16)
SET IDENTITY_INSERT [dbo].[Oferta] OFF
GO
SET IDENTITY_INSERT [dbo].[Producto] ON 

INSERT [dbo].[Producto] ([idProducto], [nombreProducto], [fechaDeIngreso], [estadoProducto], [valorCalculado], [idTipoProducto]) VALUES (1, N'Celular', CAST(N'2018-05-25T15:55:40.000' AS DateTime), N'Buen estado', 500, 1)
INSERT [dbo].[Producto] ([idProducto], [nombreProducto], [fechaDeIngreso], [estadoProducto], [valorCalculado], [idTipoProducto]) VALUES (2, N'Play Station 5', CAST(N'2018-05-25T15:55:40.000' AS DateTime), N'Buen estado', 4000, 3)
INSERT [dbo].[Producto] ([idProducto], [nombreProducto], [fechaDeIngreso], [estadoProducto], [valorCalculado], [idTipoProducto]) VALUES (3, N'Xbox Series S', CAST(N'2024-02-05T21:22:12.000' AS DateTime), N'Buen estado', 4000, 3)
INSERT [dbo].[Producto] ([idProducto], [nombreProducto], [fechaDeIngreso], [estadoProducto], [valorCalculado], [idTipoProducto]) VALUES (4, N'Licuadora', CAST(N'2018-05-25T15:55:40.000' AS DateTime), N'Buen estado', 500, 1)
INSERT [dbo].[Producto] ([idProducto], [nombreProducto], [fechaDeIngreso], [estadoProducto], [valorCalculado], [idTipoProducto]) VALUES (10, N'Iphone 13', CAST(N'2024-02-05T21:22:12.000' AS DateTime), N'Mal Estado', 10000, 1)
INSERT [dbo].[Producto] ([idProducto], [nombreProducto], [fechaDeIngreso], [estadoProducto], [valorCalculado], [idTipoProducto]) VALUES (11, N'Laptop gamer', CAST(N'2018-05-25T15:55:40.000' AS DateTime), N'Buen estado', 15000, 1)
INSERT [dbo].[Producto] ([idProducto], [nombreProducto], [fechaDeIngreso], [estadoProducto], [valorCalculado], [idTipoProducto]) VALUES (13, N'Pantalla', CAST(N'2024-02-06T01:36:50.000' AS DateTime), N'Buen estado', 5000, 1)
SET IDENTITY_INSERT [dbo].[Producto] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoProducto] ON 

INSERT [dbo].[TipoProducto] ([idTipoProducto], [tipoProducto]) VALUES (1, N'Electrodomestico')
INSERT [dbo].[TipoProducto] ([idTipoProducto], [tipoProducto]) VALUES (2, N'Linea blanca')
INSERT [dbo].[TipoProducto] ([idTipoProducto], [tipoProducto]) VALUES (3, N'Videojuegos')
INSERT [dbo].[TipoProducto] ([idTipoProducto], [tipoProducto]) VALUES (4, N'Cocina')
SET IDENTITY_INSERT [dbo].[TipoProducto] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([idUsuario], [correo], [password], [tipoUsuario]) VALUES (13, N'admin@casaempeño.com', N'722ba5d1f67c4e5dfb402d06fefc8168', N'Administrador')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Venta] ON 

INSERT [dbo].[Venta] ([idVenta], [precioVenta], [precioEnQueSeVendio], [vendido], [idProducto]) VALUES (1, 500, NULL, 1, NULL)
INSERT [dbo].[Venta] ([idVenta], [precioVenta], [precioEnQueSeVendio], [vendido], [idProducto]) VALUES (11, 600, 501, 1, NULL)
INSERT [dbo].[Venta] ([idVenta], [precioVenta], [precioEnQueSeVendio], [vendido], [idProducto]) VALUES (12, 5000, NULL, 1, NULL)
INSERT [dbo].[Venta] ([idVenta], [precioVenta], [precioEnQueSeVendio], [vendido], [idProducto]) VALUES (13, 5000, 5099, 1, NULL)
INSERT [dbo].[Venta] ([idVenta], [precioVenta], [precioEnQueSeVendio], [vendido], [idProducto]) VALUES (14, 5000, 6000, 1, 2)
INSERT [dbo].[Venta] ([idVenta], [precioVenta], [precioEnQueSeVendio], [vendido], [idProducto]) VALUES (15, 600, NULL, 0, 4)
INSERT [dbo].[Venta] ([idVenta], [precioVenta], [precioEnQueSeVendio], [vendido], [idProducto]) VALUES (16, 16000, 15000, 1, 11)
INSERT [dbo].[Venta] ([idVenta], [precioVenta], [precioEnQueSeVendio], [vendido], [idProducto]) VALUES (17, 800, NULL, 0, 1)
SET IDENTITY_INSERT [dbo].[Venta] OFF
GO
ALTER TABLE [dbo].[Devolucion]  WITH CHECK ADD  CONSTRAINT [FK_Devolucion_Producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[Producto] ([idProducto])
GO
ALTER TABLE [dbo].[Devolucion] CHECK CONSTRAINT [FK_Devolucion_Producto]
GO
ALTER TABLE [dbo].[Oferta]  WITH CHECK ADD  CONSTRAINT [FK_Oferta_Venta] FOREIGN KEY([idVenta])
REFERENCES [dbo].[Venta] ([idVenta])
GO
ALTER TABLE [dbo].[Oferta] CHECK CONSTRAINT [FK_Oferta_Venta]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_TipoProducto] FOREIGN KEY([idTipoProducto])
REFERENCES [dbo].[TipoProducto] ([idTipoProducto])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_TipoProducto]
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_Producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[Producto] ([idProducto])
GO
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_Producto]
GO
