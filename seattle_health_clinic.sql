CREATE DATABASE seattle_health_clinic
GO
USE [seattle_health_clinic]
GO
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
	[employee_data_access_level] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employees_medical](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[license_type] [varchar](255) NULL,
	[license_number] [varchar](255) NULL,
	[license_expiration] [date] NULL
) ON [PRIMARY]
GO