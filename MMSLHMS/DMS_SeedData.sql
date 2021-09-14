--USE MMSLDMS

---- Add Orgnaization ----

--INSERT INTO [dbo].[tblOrganizations]
--           ([Name],[Address],[Telephone],[MobileNumber],[Fax],[Email],[IsActive],[EntryDate],[EntryUser],[ApprovedBy],[ApprovedDate],[ShortName])
--     VALUES
--           ('MM Services Limited','Dhanmondi,Dhaka','0291415181','01515101010',NULL,NULL,1,GETDATE(),'SYSTEM',NULL,NULL,'MMSL')
--GO

---- Add Role ----

--INSERT [dbo].[tblRoles] ([OrgId], [RoleName], [Description], [EntryUser], [EntryDate], [ApprovedBy], [ApprovedDate]) VALUES (NULL, N'Admin', N'Admin Operation Role', N'SYSTEM', CAST(N'2019-06-18 15:39:30.517' AS DateTime), N'Admin', CAST(N'2019-06-18 15:39:30.517' AS DateTime))
--INSERT [dbo].[tblRoles] ([OrgId], [RoleName], [Description], [EntryUser], [EntryDate], [ApprovedBy], [ApprovedDate]) VALUES (NULL, N'User', N'User operational role', N'SYSTEM', CAST(N'2019-06-18 17:04:10.160' AS DateTime), N'Admin', CAST(N'2019-06-18 17:04:10.160' AS DateTime))
--INSERT [dbo].[tblRoles] ([OrgId], [RoleName], [Description], [EntryUser], [EntryDate], [ApprovedBy], [ApprovedDate]) VALUES (NULL, N'System Admin', N'Super Admin Role', N'SYSTEM', CAST(N'2019-08-10 12:19:18.397' AS DateTime), N'Admin', CAST(N'2019-08-10 12:19:18.397' AS DateTime))

---- Add User ----

--INSERT [dbo].[tblUsers] ([UserName], [Email], [Password], [FirstName], [MiddleName], [LastName], [EmpId], [OrgId], [RoleId], [IsActive], [IsRoleActive], [EntryUser], [EntryDate], [ApprovedBy], [ApprovedDate], [UpdateBy], [UpdateDate], [BranchId]) VALUES (N'SuperAdmin', N'yeasin@super.com', N'NRmacANXZLxcP3FyLn0u+fQEfntDUOQiRuhYv8lVjYc=', N'Mohammad', N'Yeasin', N'Ahmed', N'03020002', 1, 1, 1, 1, N'Admin', CAST(N'2019-08-10 12:21:45.363' AS DateTime), N'SYSTEM', CAST(N'2019-08-10 12:21:45.363' AS DateTime), NULL, NULL, 0)

---- Add Module ----
--INSERT [dbo].[tblModules] ([ModuleName], [EntryUser], [EntryDate], [ApprovedBy], [ApprovedDate], [IconName], [IconColor]) VALUES (N'Admin Panel', N'SYSTEM', CAST(N'2019-05-23 10:58:12.167' AS DateTime), N'SYSTEM', CAST(N'2019-05-23 10:58:12.170' AS DateTime), N'fa fa-user-secret fa-5x', N'#764d4d')
--INSERT [dbo].[tblModules] ([ModuleName], [EntryUser], [EntryDate], [ApprovedBy], [ApprovedDate], [IconName], [IconColor]) VALUES ( N'Diagnostic', N'SYSTEM', CAST(N'2019-09-17 17:04:45.353' AS DateTime), N'SYSTEM', CAST(N'2019-09-17 17:04:45.353' AS DateTime), N'N/A', N'N/A')
--INSERT [dbo].[tblModules] ([ModuleName], [EntryUser], [EntryDate], [ApprovedBy], [ApprovedDate], [IconName], [IconColor]) VALUES ( N'Diagnostic', N'SYSTEM', CAST(N'2019-09-17 17:04:45.353' AS DateTime), N'SYSTEM', CAST(N'2019-09-17 17:04:45.353' AS DateTime), N'N/A', N'N/A')

---- Add Mainmenu ----
--INSERT [dbo].[tblMainMenus] ([MenuName], [ModuleID], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate]) VALUES (N'Module Management', 1, N'SYSTEM', CAST(N'2019-06-02 10:42:22.127' AS DateTime), N'SYSTEM', CAST(N'2019-06-02 10:42:22.127' AS DateTime))
--INSERT [dbo].[tblMainMenus] ([MenuName], [ModuleID], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate]) VALUES (N'User Management', 1, N'SYSTEM', CAST(N'2019-06-09 13:00:24.657' AS DateTime), N'SYSTEM', CAST(N'2019-06-09 13:00:24.657' AS DateTime))
--INSERT [dbo].[tblMainMenus] ([MenuName], [ModuleID], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate]) VALUES (N'Diagnostic Management', 2, N'SYSTEM', CAST(N'2019-09-17 17:05:06.887' AS DateTime), N'SYSTEM', CAST(N'2019-09-17 17:05:06.887' AS DateTime))


---- Add Submenu -----
---- Module Management
--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'Module List', 1, N'Admin', N'ModuleList', NULL, N'SYSTEM', CAST(N'2019-06-02 10:43:19.463' AS DateTime), N'SYSTEM', CAST(N'2019-06-02 10:43:19.463' AS DateTime), 1)
--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'Mainmenu List', 1, N'Admin', N'MainMenuList', NULL, N'SYSTEM', CAST(N'2019-06-02 10:43:48.827' AS DateTime), N'SYSTEM', CAST(N'2019-06-02 10:43:48.827' AS DateTime), 1)
--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'Submenu List', 1, N'Admin', N'SubMenuList', NULL, N'SYSTEM', CAST(N'2019-06-02 10:44:33.180' AS DateTime), N'SYSTEM', CAST(N'2019-06-02 10:44:33.180' AS DateTime), 1)
--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'All Menu Item', 1, N'Admin', N'GetAllMenuItem', NULL, N'SYSTEM', CAST(N'2019-06-02 10:49:44.810' AS DateTime), N'SYSTEM', CAST(N'2019-06-02 10:49:44.810' AS DateTime), 1)



---- User Management ----
--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'User List', 2, N'Admin', N'UserList', NULL, N'SYSTEM', CAST(N'2019-06-09 13:04:49.343' AS DateTime), N'SYSTEM', CAST(N'2019-06-09 13:04:49.343' AS DateTime), 1)
--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'Organization List', 2, N'Admin', N'OrganizationList', NULL, N'SYSTEM', CAST(N'2019-06-11 10:31:27.243' AS DateTime), N'SYSTEM', CAST(N'2019-06-11 10:31:27.243' AS DateTime), 1)
--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'User Authorization', 2, N'Admin', N'SetUserMenu', NULL, N'SYSTEM', CAST(N'2019-06-18 12:12:05.047' AS DateTime), N'SYSTEM', CAST(N'2019-06-18 12:12:05.047' AS DateTime), 1)
--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'Role List', 2, N'Admin', N'RoleList', NULL, N'SYSTEM', CAST(N'2019-06-22 11:48:59.097' AS DateTime), N'SYSTEM', CAST(N'2019-06-22 11:48:59.097' AS DateTime), 1)
--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'Role Wise Authorization', 2, N'Admin', N'RoleWiseUserMenu', NULL, N'SYSTEM', CAST(N'2019-06-22 11:49:32.790' AS DateTime), N'SYSTEM', CAST(N'2019-06-22 11:49:32.790' AS DateTime), 1)
--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'Change Password', 2, N'Account', N'ChangePassword', NULL, N'SYSTEM', CAST(N'2019-07-10 15:49:21.653' AS DateTime), N'SYSTEM', CAST(N'2019-07-10 15:49:21.653' AS DateTime), 0)
--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'Org Authorization', 2, N'Admin', N'GetAllMenuItemForOrgAuth', NULL, N'SYSTEM', CAST(N'2019-09-14 15:30:00.697' AS DateTime), N'SYSTEM', CAST(N'2019-09-14 15:30:00.697' AS DateTime), 1)

---- Diagnostic Management -----
--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'Investigation List', 3, N'Diagnostic', N'GetInvestigationList', NULL, N'SYSTEM', CAST(N'2019-09-17 17:05:57.167' AS DateTime), N'SYSTEM', CAST(N'2019-09-17 17:05:57.167' AS DateTime), 1)

--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'Referrer List', 3, N'Diagnostic', N'GetReferrerList', NULL, N'SYSTEM', CAST(N'2019-09-21 12:07:33.603' AS DateTime), N'SYSTEM', CAST(N'2019-09-21 12:07:33.603' AS DateTime), 1)

--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'Investigation Bill', 3, N'Diagnostic', N'CreateDiagnosticBill', NULL, N'SYSTEM', CAST(N'2019-09-23 09:37:49.817' AS DateTime), N'SYSTEM', CAST(N'2019-09-23 09:37:49.817' AS DateTime), 1)

--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'Bill Due Adjustment', 3, N'Diagnostic', N'GetDiagnosticDueList', NULL, N'SYSTEM', CAST(N'2019-09-30 11:42:46.290' AS DateTime), N'SYSTEM', CAST(N'2019-09-30 11:42:46.290' AS DateTime), 1)

--INSERT [dbo].[tblSubmenus] ([SubMenuName], [MainMenuId], [ControllName], [ActionName], [Area], [EntryUser], [EntryDate], [ApprovedUser], [ApprovedDate], [IsShow]) VALUES (N'Report Panel', 3, N'Diagnostic', N'ReportPanel', NULL, N'SYSTEM', CAST(N'2019-10-01 10:27:21.700' AS DateTime), N'SYSTEM', CAST(N'2019-10-01 10:27:21.700' AS DateTime), 1)