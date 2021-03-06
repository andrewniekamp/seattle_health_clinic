USE [seattle_health_clinic_test]
GO
/****** Object:  Table [dbo].[conditions]    Script Date: 8/8/2016 5:30:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[conditions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[status] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[critical_patients]    Script Date: 8/8/2016 5:30:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[critical_patients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[patient_id] [int] NULL,
	[frequency] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[diagnosis]    Script Date: 8/8/2016 5:30:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[diagnosis](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[patient_id] [int] NULL,
	[doctor_id] [int] NULL,
	[symptom_id] [int] NULL,
	[diagnosis_date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[employees]    Script Date: 8/8/2016 5:30:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[employees](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[employee_name_first] [varchar](255) NULL,
	[employee_name_last] [varchar](255) NULL,
	[employee_ssn] [varchar](255) NULL,
	[employee_type] [varchar](255) NULL,
	[employee_date_hire] [date] NULL,
	[employee_salary_type] [varchar](255) NULL,
	[employee_data_access_level] [int] NULL,
	[employee_specialty] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[employees_medical]    Script Date: 8/8/2016 5:30:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[employees_medical](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[license_type] [varchar](255) NULL,
	[license_number] [varchar](255) NULL,
	[license_issue] [date] NULL,
	[license_expiration] [date] NULL,
	[license_issue_state] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[patients]    Script Date: 8/8/2016 5:30:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[patients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[address] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[patients_conditions]    Script Date: 8/8/2016 5:30:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[patients_conditions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[patient_id] [int] NULL,
	[condition_id] [int] NULL,
	[doctor_id] [int] NULL,
	[status_date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[symptoms]    Script Date: 8/8/2016 5:30:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[symptoms](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[classification] [varchar](255) NULL,
	[contagious] [bit] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[critical_patients] ON 

INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (1, 63, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (2, 64, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (3, 70, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (4, 76, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (5, 84, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (6, 90, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (7, 94, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (8, 105, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (9, 106, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (10, 117, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (11, 120, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (12, 126, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (13, 134, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (14, 148, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (15, 159, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (16, 170, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (17, 176, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (18, 193, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (19, 194, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (20, 206, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (21, 223, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (22, 230, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (23, 253, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (24, 263, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (25, 273, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (26, 277, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (27, 301, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (28, 312, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (31, 347, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (32, 364, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (29, 323, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (30, 337, 2)
SET IDENTITY_INSERT [dbo].[critical_patients] OFF
