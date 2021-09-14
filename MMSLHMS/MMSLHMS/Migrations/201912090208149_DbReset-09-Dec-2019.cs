namespace MMSLHMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbReset09Dec2019 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblActionLog",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ActionName = c.String(maxLength: 50),
                        TableName = c.String(maxLength: 200),
                        PrimaryKeyId = c.String(maxLength: 50),
                        SubKey = c.String(maxLength: 50),
                        MacID = c.String(),
                        OrgId = c.Int(),
                        UserId = c.String(maxLength: 50),
                        UserName = c.String(maxLength: 50),
                        DataValues = c.String(),
                        DeleteTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblBranchInfo",
                c => new
                    {
                        BranchId = c.Int(nullable: false, identity: true),
                        OrgId = c.Int(),
                        BranchName = c.String(maxLength: 50),
                        ShortName = c.String(maxLength: 5),
                        BranchCode = c.String(maxLength: 15),
                        Address = c.String(maxLength: 100),
                        ContactNumber = c.String(maxLength: 50),
                        EntryUser = c.String(maxLength: 50),
                        EntryDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.BranchId);
            
            CreateTable(
                "dbo.tblDepartment",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        ShortName = c.String(),
                        OrgId = c.Int(nullable: false),
                        EntryUser = c.String(),
                        EntryDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.tblDesignation",
                c => new
                    {
                        DesigId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        ShortName = c.String(maxLength: 10),
                        OrgId = c.Int(),
                        EntryUser = c.String(maxLength: 80),
                        EntryDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.DesigId);
            
            CreateTable(
                "dbo.tblDiagnosticBillDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BillInfoId = c.Long(),
                        InvoiceNo = c.String(),
                        InvestigationId = c.Long(),
                        Fee = c.Decimal(precision: 18, scale: 2),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        DiscountPercent = c.Decimal(precision: 18, scale: 2),
                        SubTotal = c.Decimal(precision: 18, scale: 2),
                        NetTotal = c.Decimal(precision: 18, scale: 2),
                        EntryUser = c.String(maxLength: 50),
                        EntryDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50),
                        UpdateDate = c.DateTime(),
                        ReferrerId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblDiagnosticBillInfo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrgId = c.Long(),
                        BranchId = c.Long(),
                        InvoiceNo = c.String(),
                        PatientId = c.String(maxLength: 100),
                        PatientName = c.String(maxLength: 100),
                        Address = c.String(maxLength: 150),
                        MobileNo = c.String(maxLength: 50),
                        Year = c.Short(),
                        Months = c.Short(),
                        Days = c.Short(),
                        Sex = c.String(maxLength: 10),
                        ReferrerId = c.Long(nullable: false),
                        PaymentMode = c.String(maxLength: 50),
                        DiscountPercent = c.Decimal(precision: 18, scale: 2),
                        BankId = c.Long(),
                        CashAmount = c.Decimal(precision: 18, scale: 2),
                        CardAmount = c.Decimal(precision: 18, scale: 2),
                        DiscountAmount = c.Decimal(precision: 18, scale: 2),
                        ReceivedAmount = c.Decimal(precision: 18, scale: 2),
                        DueAmount = c.Decimal(precision: 18, scale: 2),
                        SubTotal = c.Decimal(precision: 18, scale: 2),
                        NetAmount = c.Decimal(precision: 18, scale: 2),
                        Status = c.String(maxLength: 20),
                        EntryUser = c.String(maxLength: 50),
                        EntryDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50),
                        UpdateDate = c.DateTime(),
                        InvestigationQty = c.Short(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblDoctorProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorId = c.String(maxLength: 30),
                        Prefix = c.String(),
                        FirstName = c.String(maxLength: 70),
                        MiddleName = c.String(maxLength: 70),
                        LastName = c.String(maxLength: 50),
                        LicenseNo = c.String(maxLength: 20),
                        FatherName = c.String(maxLength: 70),
                        MotherName = c.String(maxLength: 70),
                        SpouseName = c.String(maxLength: 70),
                        ContactNumber = c.String(maxLength: 50),
                        PhoneNumber = c.String(maxLength: 50),
                        ParmanentAddress = c.String(maxLength: 150),
                        PresentAddress = c.String(maxLength: 150),
                        Gender = c.String(maxLength: 10),
                        City = c.String(maxLength: 30),
                        Degrees = c.String(maxLength: 250),
                        Experiences = c.String(maxLength: 200),
                        CurrentJobLocation = c.String(maxLength: 100),
                        YearsOfExperience = c.Int(),
                        About = c.String(maxLength: 150),
                        DateOfBirth = c.DateTime(),
                        DepartmentId = c.Int(),
                        PhotoUrl = c.String(maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        EntryUser = c.String(maxLength: 50),
                        EntryDate = c.DateTime(),
                        ApprovedUser = c.String(maxLength: 50),
                        ApprovedDate = c.DateTime(),
                        InPatientChargeType = c.String(maxLength: 15),
                        OutPatientChargeType = c.String(maxLength: 15),
                        InPatientChargeValue = c.Double(),
                        OutPatientChargeValue = c.Double(),
                        DesignationId = c.Int(),
                        SpecializationTypeId = c.Int(),
                        BranchId = c.Int(),
                        Email = c.String(maxLength: 150),
                        MaritalStatus = c.String(maxLength: 30),
                        AllowToLogin = c.Boolean(nullable: false),
                        EmployeeCode = c.String(maxLength: 100),
                        OrgId = c.Int(nullable: false),
                        Nationality = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblEmployeeInfo",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        BranchId = c.Int(),
                        DepartmentId = c.Int(),
                        DesignationId = c.Int(),
                        FirstName = c.String(maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        MaritalStatus = c.String(maxLength: 20),
                        Gender = c.String(maxLength: 15),
                        Email = c.String(maxLength: 150),
                        HomeContactNo = c.String(maxLength: 15),
                        DOB = c.DateTime(),
                        MobileNo = c.String(maxLength: 15),
                        Nationality = c.String(maxLength: 50),
                        PresentAddress = c.String(maxLength: 150),
                        ParmanentAddress = c.String(maxLength: 150),
                        EmpType = c.String(maxLength: 50),
                        PhotoUrl = c.String(maxLength: 200),
                        StateStatus = c.String(maxLength: 20),
                        IsActive = c.Boolean(nullable: false),
                        EntryUser = c.String(maxLength: 100),
                        EntryDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 100),
                        UpdateDate = c.DateTime(),
                        EmployeeCode = c.String(maxLength: 100),
                        AllowToLogin = c.Boolean(nullable: false),
                        OrgId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.tblInvestigatinReferrer",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Address = c.String(maxLength: 150),
                        PhoneNumber = c.String(maxLength: 50),
                        OrgId = c.Long(),
                        BranchId = c.Long(),
                        EntryUser = c.String(maxLength: 50),
                        EntryDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblInvestigationChart",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InvestigationName = c.String(maxLength: 100),
                        EntryUser = c.String(maxLength: 50),
                        EntryDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 50),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblInvestigations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        Fee = c.Double(),
                        OrgId = c.Int(),
                        BranchId = c.Int(),
                        ChartId = c.Int(),
                        EntryUser = c.String(maxLength: 100),
                        EntryDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 100),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblMainMenus",
                c => new
                    {
                        MMId = c.Int(nullable: false, identity: true),
                        MenuName = c.String(),
                        ModuleID = c.Int(nullable: false),
                        EntryUser = c.String(),
                        EntryDate = c.DateTime(),
                        ApprovedUser = c.String(),
                        ApprovedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MMId);
            
            CreateTable(
                "dbo.tblModules",
                c => new
                    {
                        MId = c.Int(nullable: false, identity: true),
                        ModuleName = c.String(),
                        IconName = c.String(),
                        IconColor = c.String(),
                        EntryUser = c.String(),
                        EntryDate = c.DateTime(),
                        ApprovedBy = c.String(),
                        ApprovedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MId);
            
            CreateTable(
                "dbo.tblOrganizations",
                c => new
                    {
                        OrgId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        ShortName = c.String(maxLength: 150),
                        Address = c.String(maxLength: 200),
                        Telephone = c.String(maxLength: 100),
                        MobileNumber = c.String(maxLength: 100),
                        Fax = c.String(maxLength: 100),
                        Email = c.String(maxLength: 150),
                        IsActive = c.Boolean(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUser = c.String(maxLength: 50),
                        ApprovedBy = c.String(maxLength: 50),
                        ApprovedDate = c.DateTime(),
                        OrgLogoPath = c.String(maxLength: 250),
                        ReportLogoPath = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.OrgId);
            
            CreateTable(
                "dbo.tblOrgAuthorization",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrgId = c.Int(nullable: false),
                        SubmenuId = c.Int(nullable: false),
                        MainmenuId = c.Int(nullable: false),
                        ModuleId = c.Int(nullable: false),
                        EntryUser = c.String(maxLength: 50),
                        EntryDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                        OrgId = c.Int(),
                        EntryUser = c.String(maxLength: 50),
                        EntryDate = c.DateTime(),
                        ApprovedBy = c.String(maxLength: 50),
                        ApprovedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.tblRoleWiseUserMenu",
                c => new
                    {
                        RoleWiseMenuId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        OrgId = c.Int(nullable: false),
                        ModuleId = c.Int(nullable: false),
                        MainmenuId = c.Int(nullable: false),
                        SubmenuId = c.Int(nullable: false),
                        Add = c.Boolean(nullable: false),
                        Edit = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        Approval = c.Boolean(nullable: false),
                        Report = c.Boolean(nullable: false),
                        EntryUser = c.String(maxLength: 50),
                        EntryDate = c.DateTime(),
                        ApprovedBy = c.String(maxLength: 50),
                        ApprovedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.RoleWiseMenuId);
            
            CreateTable(
                "dbo.tblSpecializationType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                        OrgId = c.Int(),
                        EntryUser = c.String(),
                        EntryDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblSubmenus",
                c => new
                    {
                        SubMenuId = c.Int(nullable: false, identity: true),
                        SubMenuName = c.String(),
                        MainMenuId = c.Int(nullable: false),
                        ControllName = c.String(),
                        ActionName = c.String(),
                        Area = c.String(),
                        IsShow = c.Boolean(nullable: false),
                        EntryUser = c.String(),
                        EntryDate = c.DateTime(),
                        ApprovedUser = c.String(),
                        ApprovedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.SubMenuId);
            
            CreateTable(
                "dbo.tblUserAuthorization",
                c => new
                    {
                        TaskId = c.Long(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(),
                        ModuleId = c.Int(nullable: false),
                        MainmenuId = c.Int(nullable: false),
                        SubmenuId = c.Int(nullable: false),
                        Add = c.Boolean(nullable: false),
                        Edit = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        Approval = c.Boolean(nullable: false),
                        Report = c.Boolean(nullable: false),
                        EntryUser = c.String(),
                        EntryDate = c.DateTime(),
                        ApprovedBy = c.String(),
                        ApprovedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.TaskId);
            
            CreateTable(
                "dbo.tblUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 30),
                        Email = c.String(maxLength: 150),
                        Password = c.String(nullable: false),
                        FirstName = c.String(maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        RoleId = c.Int(),
                        IsRoleActive = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        EntryUser = c.String(maxLength: 30),
                        EntryDate = c.DateTime(),
                        ApprovedBy = c.String(maxLength: 30),
                        ApprovedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 30),
                        UpdateDate = c.DateTime(),
                        OrgId = c.Int(nullable: false),
                        BranchId = c.Int(),
                        EmpId = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblUsers");
            DropTable("dbo.tblUserAuthorization");
            DropTable("dbo.tblSubmenus");
            DropTable("dbo.tblSpecializationType");
            DropTable("dbo.tblRoleWiseUserMenu");
            DropTable("dbo.tblRoles");
            DropTable("dbo.tblOrgAuthorization");
            DropTable("dbo.tblOrganizations");
            DropTable("dbo.tblModules");
            DropTable("dbo.tblMainMenus");
            DropTable("dbo.tblInvestigations");
            DropTable("dbo.tblInvestigationChart");
            DropTable("dbo.tblInvestigatinReferrer");
            DropTable("dbo.tblEmployeeInfo");
            DropTable("dbo.tblDoctorProfile");
            DropTable("dbo.tblDiagnosticBillInfo");
            DropTable("dbo.tblDiagnosticBillDetails");
            DropTable("dbo.tblDesignation");
            DropTable("dbo.tblDepartment");
            DropTable("dbo.tblBranchInfo");
            DropTable("dbo.tblActionLog");
        }
    }
}
