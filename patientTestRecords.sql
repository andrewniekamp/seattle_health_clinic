SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[conditions] ON 

INSERT [dbo].[conditions] ([id], [status]) VALUES (1, N'Stable')
INSERT [dbo].[conditions] ([id], [status]) VALUES (2, N'Urgent')
INSERT [dbo].[conditions] ([id], [status]) VALUES (3, N'Critical')
SET IDENTITY_INSERT [dbo].[conditions] OFF
SET IDENTITY_INSERT [dbo].[employees] ON 

INSERT [dbo].[employees] ([id], [employee_name_first], [employee_name_last], [employee_ssn], [employee_type], [employee_date_hire], [employee_salary_type], [employee_email], [employee_password]) VALUES (1, N'Doc', N'Gonzo', N'1234567890', N'Medical', CAST(N'2015-01-01' AS Date), N'Annual', N'example@example.com', N'password')
SET IDENTITY_INSERT [dbo].[employees] OFF
SET IDENTITY_INSERT [dbo].[patients] ON 

INSERT [dbo].[patients] ([id], [name], [address]) VALUES (3, N'Juliane Keena', N'Seattle')
INSERT [dbo].[patients] ([id], [name], [address]) VALUES (2, N'Jake Acuna', N'Seattle')
INSERT [dbo].[patients] ([id], [name], [address]) VALUES (4, N'Korey Gurrola', N'Los Angeles')
INSERT [dbo].[patients] ([id], [name], [address]) VALUES (5, N'Kirk Debord', N'New York')
INSERT [dbo].[patients] ([id], [name], [address]) VALUES (6, N'Mona Taff', N'Columbus, OH')
INSERT [dbo].[patients] ([id], [name], [address]) VALUES (7, N'Nell Fuselier', N'Tokyo')
INSERT [dbo].[patients] ([id], [name], [address]) VALUES (8, N'Ulrike Kroh', N'Brussels')
INSERT [dbo].[patients] ([id], [name], [address]) VALUES (9, N'Dortha Tewell', N'Shanghai')
INSERT [dbo].[patients] ([id], [name], [address]) VALUES (10, N'Emmaline Mcmakin', N'Lima')
INSERT [dbo].[patients] ([id], [name], [address]) VALUES (11, N'Leisa Vallarta', N'New York')
INSERT [dbo].[patients] ([id], [name], [address]) VALUES (12, N'Twila Lairsey', N'Seattle')
INSERT [dbo].[patients] ([id], [name], [address]) VALUES (13, N'Lawerence Royall', N'Seattle')
INSERT [dbo].[patients] ([id], [name], [address]) VALUES (14, N'Adriene Holmquist', N'Seattle')
INSERT [dbo].[patients] ([id], [name], [address]) VALUES (15, N'Laureen Weglarz', N'Seattle')
SET IDENTITY_INSERT [dbo].[patients] OFF
SET IDENTITY_INSERT [dbo].[patients_conditions] ON 

INSERT [dbo].[patients_conditions] ([id], [patient_id], [condition_id], [doctor_id], [status_date]) VALUES (3, 3, 1, 2, CAST(N'2016-08-24T00:00:00.000' AS DateTime))
INSERT [dbo].[patients_conditions] ([id], [patient_id], [condition_id], [doctor_id], [status_date]) VALUES (4, 3, 1, 1, CAST(N'2016-08-24T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[patients_conditions] OFF
SET IDENTITY_INSERT [dbo].[patients_scheduling] ON 

INSERT [dbo].[patients_scheduling] ([id], [patient_id], [doctor_id], [note], [appointment_date]) VALUES (3, 2, 1, N'Hypertension, diagnosed “years ago,” well-controlled with Metoprolol
Depression, poorly controlled; started Prozac 6 months ago but still feels depressed', CAST(N'2016-08-19T00:00:00.000' AS DateTime))
INSERT [dbo].[patients_scheduling] ([id], [patient_id], [doctor_id], [note], [appointment_date]) VALUES (4, 11, 1, N'Abdomen soft and non-distended with no scars or striations
No pulsatile masses, no abdominal bruits ascultated
Spleen not palpable, liver not palpable
Tender to palpation in epigastric region and left upper quadrant; No reflex tenderness; No guarding; Murphy’s sign negative', CAST(N'2016-08-14T00:00:00.000' AS DateTime))
INSERT [dbo].[patients_scheduling] ([id], [patient_id], [doctor_id], [note], [appointment_date]) VALUES (5, 15, 1, N'Lipitor, for high cholesterol
Multivitamin
Baby aspirin every day ', CAST(N'2016-08-20T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[patients_scheduling] OFF
