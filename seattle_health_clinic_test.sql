USE [seattle_health_clinic_test]
GO
/****** Object:  Table [dbo].[certifications]    Script Date: 8/9/2016 9:57:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[certifications](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[employee_id] [int] NULL,
	[license_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[conditions]    Script Date: 8/9/2016 9:57:04 AM ******/
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
/****** Object:  Table [dbo].[critical_patients]    Script Date: 8/9/2016 9:57:04 AM ******/
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
/****** Object:  Table [dbo].[diagnosis]    Script Date: 8/9/2016 9:57:04 AM ******/
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
/****** Object:  Table [dbo].[employees]    Script Date: 8/9/2016 9:57:04 AM ******/
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
	[employee_specialty] [varchar](255) NULL,
	[employee_email] [varchar](255) NULL,
	[employee_password] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[licenses]    Script Date: 8/9/2016 9:57:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[licenses](
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
/****** Object:  Table [dbo].[patients]    Script Date: 8/9/2016 9:57:04 AM ******/
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
/****** Object:  Table [dbo].[patients_conditions]    Script Date: 8/9/2016 9:57:04 AM ******/
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
/****** Object:  Table [dbo].[patients_scheduling]    Script Date: 8/9/2016 9:57:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[patients_scheduling](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[patient_id] [int] NULL,
	[doctor_id] [int] NULL,
	[note] [text] NULL,
	[appointment_date] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[symptoms]    Script Date: 8/9/2016 9:57:04 AM ******/
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
SET IDENTITY_INSERT [dbo].[certifications] ON 

INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (1, 3, 1)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (2, 4, 3)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (3, 4, 4)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (4, 9, 9)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (5, 10, 11)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (6, 10, 12)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (7, 15, 13)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (8, 16, 15)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (9, 16, 16)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (10, 21, 21)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (11, 22, 23)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (12, 22, 24)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (13, 27, 27)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (14, 28, 29)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (15, 28, 30)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (16, 33, 31)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (17, 34, 33)
INSERT [dbo].[certifications] ([id], [employee_id], [license_id]) VALUES (18, 34, 34)
SET IDENTITY_INSERT [dbo].[certifications] OFF
SET IDENTITY_INSERT [dbo].[critical_patients] ON 

INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (1, 1, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (2, 28, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (3, 35, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (4, 59, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (5, 69, 2)
INSERT [dbo].[critical_patients] ([id], [patient_id], [frequency]) VALUES (6, 96, 2)
SET IDENTITY_INSERT [dbo].[critical_patients] OFF
