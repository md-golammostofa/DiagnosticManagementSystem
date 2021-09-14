namespace MMSLHMS.Migrations
{
    using HMSBO.Models;
    using HMSBO.ViewModels;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MMSLHMS.DAL.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MMSLHMS.DAL.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //Organization Org1 = new Organization() { Name = "MM Services Limited", Address = "Dhanmondi,Dhaka", MobileNumber = "01515101010", Email = "mmservicesltd@mm.com", IsActive = true, EntryDate = DateTime.UtcNow, EntryUser = "Admin", ApprovedBy = "Admin", ApprovedDate = DateTime.UtcNow };

            ////Organization Org2 = new Organization() { Name = "Square Hospital Limited", Address = "Panthopath,Dhaka", MobileNumber = "01717101010", Email = "square@hospital.com", IsActive = true, EntryDate = DateTime.UtcNow, EntryUser = "Admin", ApprovedBy = "Admin", ApprovedDate = DateTime.UtcNow };

            //User user1 = new User()
            //{
            //    UserName = "Admin",
            //    Password = "admin123",
            //    FirstName = "Yeasin",
            //    LastName = "Ahmed",
            //    EntryUser = "Admin",
            //    EntryDate = DateTime.UtcNow,
            //    ApprovedBy = "Admin",
            //    ApprovedDate = DateTime.UtcNow,
            //    IsActive = true,
            //    Email = "admin@mm.com",
            //    OrgId = 1
            //};

            ////User user2 = new User()
            ////{
            ////    UserName = "User1",
            ////    Password = "user1abc",
            ////    FirstName = "Lukman",
            ////    LastName = "Hakim",
            ////    EntryUser = "Admin",
            ////    EntryDate = DateTime.UtcNow,
            ////    ApprovedBy = "Admin",
            ////    ApprovedDate = DateTime.UtcNow,
            ////    IsActive = true,
            ////    Email = "lukman@haim.com",
            ////    OrgId = 2
            ////};

            ////context.tblOrganizations.Add(Org1);
            ////context.tblOrganizations.Add(Org2);

            ////context.tblUsers.Add(user1);
            ////context.tblUsers.Add(user2);

            //Module module1 = new Module() { ModuleName = "Admin", EntryUser = "Admin", EntryDate = DateTime.Now, ApprovedBy = "Admin", ApprovedDate = DateTime.Now };
            //Module module2 = new Module() { ModuleName = "Patients", EntryUser = "Admin", EntryDate = DateTime.Now, ApprovedBy = "Admin", ApprovedDate = DateTime.Now };
            //Module module3 = new Module() { ModuleName = "Doctors", EntryUser = "Admin", EntryDate = DateTime.Now, ApprovedBy = "Admin", ApprovedDate = DateTime.Now };

            ////context.tblModules.Add(module1);
            ////context.tblModules.Add(module2);
            ////context.tblModules.Add(module3);

            //MainMenu mainMenu1 = new MainMenu() { MenuName = "APP User", ModuleID = 1, EntryUser = "Admin", EntryDate = DateTime.Now, ApprovedUser = "Admin", ApprovedDate = DateTime.Now };

            //MainMenu mainMenu2 = new MainMenu() { MenuName = "Outdoor Patients", ModuleID = 2, EntryUser = "Admin", EntryDate = DateTime.Now, ApprovedUser = "Admin", ApprovedDate = DateTime.Now };

            //MainMenu mainMenu3 = new MainMenu() { MenuName = "Doctors Info", ModuleID = 3, EntryUser = "Admin", EntryDate = DateTime.Now, ApprovedUser = "Admin", ApprovedDate = DateTime.Now };

            ////context.tblMainMenus.Add(mainMenu1);
            ////context.tblMainMenus.Add(mainMenu2);
            ////context.tblMainMenus.Add(mainMenu3);

            //Submenu submenu1 = new Submenu() { SubMenuName = "Loged-In Loged-Out Time", MainMenuId = 1, ControllName = "", ActionName = "", Area = "", EntryUser = "Admin", EntryDate = DateTime.Now, ApprovedUser = "Admin", ApprovedDate = DateTime.Now };

            //Submenu submenu2 = new Submenu() { SubMenuName = "Today's Patients", MainMenuId = 2, ControllName = "", ActionName = "", Area = "", EntryUser = "Admin", EntryDate = DateTime.Now, ApprovedUser = "Admin", ApprovedDate = DateTime.Now };

            //Submenu submenu3 = new Submenu() { SubMenuName = "Doctor Profile", MainMenuId = 3, ControllName = "", ActionName = "", Area = "", EntryUser = "Admin", EntryDate = DateTime.Now, ApprovedUser = "Admin", ApprovedDate = DateTime.Now };

            ////context.tblSubmenus.Add(submenu1);
            ////context.tblSubmenus.Add(submenu2);
            ////context.tblSubmenus.Add(submenu3);
        }
    }
}
