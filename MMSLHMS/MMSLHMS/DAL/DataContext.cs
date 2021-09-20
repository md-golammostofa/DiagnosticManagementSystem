using HMSBO.Models;
using HMSBO.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace MMSLHMS.DAL
{
    public class DataContext:DbContext
    {
        public DataContext():base("MMSLDMS_DEMO")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.Roles)
            //    .WithMany(r => r.Users)
            //    .Map(m =>
            //    {
            //        m.ToTable("UserRole");
            //        m.MapLeftKey("UserId");
            //        m.MapRightKey("RoleId");
            //    });

            modelBuilder.Entity<Organization>().ToTable("tblOrganizations");
            modelBuilder.Entity<User>().ToTable("tblUsers");
            modelBuilder.Entity<Module>().ToTable("tblModules");
            modelBuilder.Entity<MainMenu>().ToTable("tblMainMenus");
            modelBuilder.Entity<Submenu>().ToTable("tblSubmenus");
            modelBuilder.Entity<UserAuthorization>().ToTable("tblUserAuthorization");
            modelBuilder.Entity<Role>().ToTable("tblRoles");
            modelBuilder.Entity<RoleWiseUserMenu>().ToTable("tblRoleWiseUserMenu");
            modelBuilder.Entity<DoctorProfile>().ToTable("tblDoctorProfile");
            modelBuilder.Entity<Department>().ToTable("tblDepartment");
            modelBuilder.Entity<Designation>().ToTable("tblDesignation");
            modelBuilder.Entity<SpecializationType>().ToTable("tblSpecializationType");
            modelBuilder.Entity<Employee>().ToTable("tblEmployeeInfo");
            modelBuilder.Entity<Branch>().ToTable("tblBranchInfo");
            modelBuilder.Entity<OrgAuthorization>().ToTable("tblOrgAuthorization");
            modelBuilder.Entity<Investigation>().ToTable("tblInvestigations");
            modelBuilder.Entity<InvestigationReferrer>().ToTable("tblInvestigatinReferrer");

            //DiagnosticBillInfo
            modelBuilder.Entity<DiagnosticBillInfo>().ToTable("tblDiagnosticBillInfo");
            modelBuilder.Entity<DiagnosticBillDetail>().ToTable("tblDiagnosticBillDetails");
            modelBuilder.Entity<InvestigationChart>().ToTable("tblInvestigationChart");

            //ActionLog
            modelBuilder.Entity<ActionLog>().ToTable("tblActionLog");
            //
            modelBuilder.Entity<Specialist>().ToTable("tblSpecialists");
            modelBuilder.Entity<OtherProfessional>().ToTable("tblOtherProfessional");
            modelBuilder.Entity<Times>().ToTable("tblTimes");
            modelBuilder.Entity<Doctor>().ToTable("tblDoctors");
            modelBuilder.Entity<DoctorDetails>().ToTable("tblDoctorDetails");
            modelBuilder.Entity<Patient>().ToTable("tblPatients");
            modelBuilder.Entity<Appoinment>().ToTable("tblAppoinments");
            //Inventory
            modelBuilder.Entity<Category>().ToTable("tblCategory");
            modelBuilder.Entity<Product>().ToTable("tblProduct");
            modelBuilder.Entity<ProductStockInfo>().ToTable("tblProductStockInfo");
            modelBuilder.Entity<ProductStockDetails>().ToTable("tblProductStockDetails");
        }

        public DbSet<Organization> tblOrganizations { get; set; }
        public DbSet<User> tblUsers { get; set; }
        public DbSet<Module> tblModules { get; set; }
        public DbSet<MainMenu> tblMainMenus { get; set; }
        public DbSet<Submenu> tblSubmenus { get; set; }
        public DbSet<UserAuthorization> tblUserAuthorization { get; set; }
        public DbSet<Role> tblRoles { get; set; }
        public DbSet<RoleWiseUserMenu> tblRoleWiseUserMenu { get; set; }
        public DbSet<DoctorProfile> tblDoctorProfile { get; set; }
        public DbSet<Department> tblDepartment { get; set; }
        public DbSet<Designation> tblDesignation { get; set; }
        public DbSet<SpecializationType> tblSpecializationType { get; set; }
        public DbSet<Employee> tblEmployeeInfo { get; set; }
        public DbSet<Branch> tblBranchInfo { get; set; }
        public DbSet<OrgAuthorization> tblOrgAuthorization { get; set; }
        public DbSet<Investigation> tblInvestigations { get; set; }
        public DbSet<InvestigationReferrer> tblInvestigatinReferrer { get; set; }
        public DbSet<DiagnosticBillInfo> tblDiagnosticBillInfo { get; set; }
        public DbSet<DiagnosticBillDetail> tblDiagnosticBillDetails { get; set; }
        public DbSet<InvestigationChart> tblInvestigationChart { get; set; }
        public DbSet<ActionLog> tblActionLog { get; set; }
        //
        public DbSet<Specialist> tblSpecialists { get; set; }
        public DbSet<OtherProfessional> tblOtherProfessional { get; set; }
        public DbSet<Times> tblTimes { get; set; }
        public DbSet<Doctor> tblDoctors { get; set; }
        public DbSet<DoctorDetails> tblDoctorDetails { get; set; }
        public DbSet<Patient> tblPatients { get; set; }
        public DbSet<Appoinment> tblAppoinments { get; set; }
        //Inventory
        public DbSet<Category> tblCategory { get; set; }
        public DbSet<Product> tblProduct { get; set; }
        public DbSet<ProductStockInfo> tblProductStockInfo { get; set; }
        public DbSet<ProductStockDetails> tblProductStockDetails { get; set; }
    }
}