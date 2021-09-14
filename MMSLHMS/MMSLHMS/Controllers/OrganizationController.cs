using HMSBO.Models;
using HMSBO.ViewModels;
using MMSLHMS.CustomFiles.Filters;
using MMSLHMS.DAL;
using MMSLHMS.DAL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMSLHMS.Controllers
{
    [CustomAuthorize]
    public class OrganizationController : BaseController
    {
        // GET: Organization

        private DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View();
        }

        // User By Only System Admin //
        [HttpGet]
        public ActionResult GetDepartmentList(int? OrgId, string DeptName = null) // Param DeptName is For Search funationlity throught the Ajax
        {
            if (OrgId == null || DeptName == null)
            {
                return View(); // Calling View at the first time 
            }
            else
            {
                // Retreiving data througth the AJAX Call.
                var departmentList = (from dept in db.tblDepartment
                                      join org in db.tblOrganizations on dept.OrgId equals org.OrgId
                                      select new { org.Name, org.OrgId, dept.DepartmentId, dept.DepartmentName, dept.ShortName, dept.EntryUser }).AsEnumerable();
                departmentList = departmentList.OrderBy(d => d.DepartmentName).ToList();
                if (OrgId > 0 && !string.IsNullOrEmpty(DeptName.Trim()))
                {
                    departmentList = departmentList.Where(d => d.OrgId == OrgId && d.DepartmentName.ToLower().StartsWith(DeptName.ToLower())).OrderBy(d => d.DepartmentName).ToList();
                }
                else if (OrgId > 0)
                {
                    departmentList = departmentList.Where(d => d.OrgId == OrgId).OrderBy(d => d.DepartmentName).ToList();
                }
                else if (!string.IsNullOrEmpty(DeptName.Trim()))
                {
                    departmentList = departmentList.Where(d => d.DepartmentName.ToLower().StartsWith(DeptName.ToLower())).OrderBy(d => d.DepartmentName).ToList();
                }
                return Json(departmentList, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveDepartment(VmDepartment vmDepartment)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                if (vmDepartment.DepartmentId <= 0)
                {
                    Department department = new Department();
                    department.DepartmentName = vmDepartment.DepartmentName;
                    department.ShortName = vmDepartment.ShortName;
                    department.OrgId = vmDepartment.OrgId;
                    department.EntryUser = User.UserName;
                    department.EntryDate = DateTime.Now;
                    db.tblDepartment.Add(department);
                    IsSuccess = true;
                }
                else
                {
                    var departmentInDb = db.tblDepartment.FirstOrDefault(d => d.DepartmentId == vmDepartment.DepartmentId);
                    if (departmentInDb != null)
                    {
                        departmentInDb.DepartmentName = vmDepartment.DepartmentName;
                        departmentInDb.ShortName = vmDepartment.ShortName;
                        departmentInDb.OrgId = vmDepartment.OrgId;
                        IsSuccess = true;
                    }
                }
                db.SaveChanges();
            }
            return Json(IsSuccess);
        }
        // 
        [HttpGet]
        public ActionResult GetSpecializationType(int? OrgId, string SpecType)
        {
            if (OrgId == null || SpecType == null)
            {
                return View();
            }
            else
            {
                var SpecializationType = (from st in db.tblSpecializationType
                                          join org in db.tblOrganizations on st.OrgId equals org.OrgId
                                          select new { st.Id, st.TypeName, org.OrgId, org.Name, st.EntryUser }).OrderBy(s => s.Name).ToList();

                if (OrgId > 0 && !string.IsNullOrEmpty(SpecType.Trim()))
                {
                    SpecializationType = SpecializationType.Where(s => s.OrgId == OrgId && s.TypeName.ToLower().StartsWith(SpecType.ToLower())).ToList();
                }
                else if (OrgId > 0)
                {
                    SpecializationType = SpecializationType.Where(s => s.OrgId == OrgId).ToList();
                }
                else if (!string.IsNullOrEmpty(SpecType.Trim()))
                {
                    SpecializationType = SpecializationType.Where(s => s.TypeName.ToLower().StartsWith(SpecType.ToLower())).ToList();
                }

                return Json(SpecializationType, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveSpecializationType(VmSpecialization model)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                if (model.Id <= 0)
                {
                    // Saving New Data
                    SpecializationType spType = new SpecializationType();
                    spType.OrgId = model.OrgId;
                    spType.TypeName = model.TypeName;
                    spType.EntryUser = User.UserName;
                    spType.EntryDate = DateTime.Now;
                    IsSuccess = true;
                    db.tblSpecializationType.Add(spType);
                }
                else if (model.Id > 0)
                {
                    // Editing Data..
                    var spTypeInDb = db.tblSpecializationType.FirstOrDefault(sp => sp.Id == model.Id);
                    if (spTypeInDb != null)
                    {
                        spTypeInDb.TypeName = model.TypeName;
                        spTypeInDb.OrgId = model.OrgId;
                        IsSuccess = true;
                    }
                }
                db.SaveChanges();
            }
            return Json(IsSuccess);
        }

        [HttpGet]
        public ActionResult GetDesignationList(int? OrgId, string DesigName)
        {
            if (OrgId == null || DesigName == null)
            {
                return View();
            }
            else
            {
                var designationList = (from des in db.tblDesignation
                                       join org in db.tblOrganizations on des.OrgId equals org.OrgId
                                       let OrgName = org.Name
                                       where org.OrgId == OrgId
                                       select new { des.DesigId, des.Name, des.ShortName, org.OrgId, OrgName, des.EntryUser }).OrderBy(d => d.Name).ToList();

                if (!string.IsNullOrEmpty(DesigName.Trim()))
                {
                    designationList = designationList.Where(d => d.Name.ToLower().StartsWith(DesigName.ToLower().Trim())).ToList();
                }
                return Json(designationList, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveNewDesignation(VmDesignation model)
        {
            bool IsValid = false;
            if (ModelState.IsValid) // Model State
            {
                if (model.DesigId <= 0)
                {
                    Designation designation = new Designation();
                    designation.Name = model.DesignationName;
                    designation.ShortName = model.ShortName;
                    designation.OrgId = User.OrgId;
                    designation.EntryDate = DateTime.Now;
                    designation.EntryUser = User.UserName;
                    db.tblDesignation.Add(designation);
                    IsValid = true;
                }
                else if (model.DesigId > 0)
                {
                    var designationInDb = db.tblDesignation.FirstOrDefault(d => d.DesigId == model.DesigId);
                    designationInDb.Name = model.DesignationName;
                    designationInDb.ShortName = model.ShortName;
                    IsValid = true;
                }
                db.SaveChanges();
            }
            return Json(IsValid);
        }

        [HttpGet]
        public ActionResult GetBranchList()
        {
            var branch = (from b in db.tblBranchInfo
                          join o in db.tblOrganizations on b.OrgId equals o.OrgId
                          where o.OrgId == User.OrgId
                          select new { b.BranchId, b.BranchName, b.BranchCode, b.ContactNumber, b.Address, b.EntryUser, b.EntryDate, o.OrgId, o.Name,b.ShortName }).OrderByDescending(b => b.BranchId).ToList();

            IList<VmBranch> ListVmBranch = new List<VmBranch>();

            foreach (var item in branch)
            {
                VmBranch vmBranch = new VmBranch { BranchId = item.BranchId, BranchName = item.BranchName, BranchCode = item.BranchCode, ShortName=item.ShortName,ContactNumber = item.ContactNumber, Address = item.Address, EntryUser = item.EntryUser, EntryDate = item.EntryDate, OrgId = item.OrgId, OrgName = item.Name };
                ListVmBranch.Add(vmBranch);
            }

            return View(ListVmBranch);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveBranch(VmBranch model)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                if (model.BranchId <= 0)
                {
                    Branch branch = new Branch
                    {
                        BranchName = model.BranchName,
                        BranchCode = model.BranchCode,
                        ShortName=model.ShortName,
                        Address = model.Address,
                        ContactNumber = model.ContactNumber,
                        OrgId = User.OrgId,
                        EntryUser = User.UserName,
                        EntryDate = DateTime.Now,
                    };
                    db.tblBranchInfo.Add(branch);
                    IsSuccess = true;
                }
                else if (model.BranchId > 0)
                {
                    var branchInDb = db.tblBranchInfo.Find(model.BranchId);
                    if (branchInDb != null)
                    {
                        branchInDb.BranchName = model.BranchName;
                        branchInDb.BranchCode = model.BranchCode;
                        branchInDb.Address = model.Address;
                        branchInDb.ContactNumber = model.ContactNumber;
                        branchInDb.UpdateBy = User.UserName;
                        branchInDb.UpdateDate = DateTime.Now;
                        IsSuccess = true;
                    }
                }
                db.SaveChanges();
            }
            return Json(IsSuccess);
        }

        [HttpGet]
        public ActionResult GetBranchById(int Id)
        {
            var branch = db.tblBranchInfo.Find(Id);
            if (branch != null)
            {
                return Json(branch, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(branch, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetEmployeeList()
        {
            var employees = (from emp in db.tblEmployeeInfo
                             join brn in db.tblBranchInfo on emp.BranchId equals brn.BranchId
                             join dept in db.tblDepartment on emp.DepartmentId equals dept.DepartmentId
                             join desig in db.tblDesignation on emp.DesignationId equals desig.DesigId
                             where emp.EmpType=="General"
                             select new { emp.EmployeeId, EmployeeName = (emp.FirstName + " " + emp.MiddleName + " " + emp.LastName), brn.BranchName, dept.DepartmentName, Designation = desig.Name, ActiveStatus = ((emp.IsActive) ? "Active" : "Inactive"), emp.Email }).OrderByDescending(emp => emp.EmployeeId).ToList();
            List<VmEmployee> EmployeeList = new List<VmEmployee>();
            foreach (var item in employees)
            {
                VmEmployee em = new VmEmployee();
                em.EmployeeId = item.EmployeeId;
                em.EmployeeName = item.EmployeeName;
                em.BranchName = item.BranchName;
                em.DepertmentName = item.DepartmentName;
                em.ActiveStatus = item.ActiveStatus;
                em.Designation = item.Designation;
                em.Email = item.Email;
                EmployeeList.Add(em);

            }
            return View(EmployeeList);
        }
        [HttpGet]
        public ActionResult CreateEmployee(int? id)
        {
            List<SelectListItem> ddlDepartment = BLCommon.GetDepartmentsByOrgForDDL(User.OrgId).OrderBy(d => d.Text).ToList();
            ViewBag.DepartmentList = ddlDepartment;

            List<SelectListItem> ddlDesignation = BLCommon.GetDesignationByOrgId(User.OrgId).OrderBy(d => d.Text).ToList();
            ViewBag.DesignationList = ddlDesignation;

            List<SelectListItem> ddlBranches = BLCommon.GetBranchByOrgId(User.OrgId).OrderBy(d => d.Text).ToList();
            ViewBag.BrancheList = ddlBranches;

            if (id == null || id <= 0)
            {
                ViewBag.heading = "Create New Employee";
                return View();
            }
            else
            {
                var emp = db.tblEmployeeInfo.Find(id);
                if (emp == null)
                {
                    return RedirectToAction("NotFound", "Error");
                }
                else
                {
                    ViewBag.heading = "Update Employee";
                    VmEmployee vmEmployee = new VmEmployee() { EmployeeId = emp.EmployeeId, FirstName = emp.FirstName, MiddleName = emp.MiddleName, LastName = emp.LastName, MaritalStatus = emp.MaritalStatus, Gender = emp.Gender, Email = emp.Email, HomeContactNo = emp.HomeContactNo, DOB = emp.DOB, MobileNo = emp.MobileNo, Nationality = emp.Nationality, PresentAddress = emp.PresentAddress, ParmanentAddress = emp.ParmanentAddress, IsActive = emp.IsActive, BranchId = emp.BranchId, DepartmentId = emp.DepartmentId, DesignationId = emp.DesignationId, AllowToLogin = emp.AllowToLogin };

                    return View(vmEmployee);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEmployee(VmEmployee vmEmployee)
        {
            if (ModelState.IsValid)
            {
                if (vmEmployee.EmployeeId == null || vmEmployee.EmployeeId <= 0)
                {
                    Employee employee = new Employee();
                    employee.FirstName = vmEmployee.FirstName;
                    employee.MiddleName = vmEmployee.MiddleName;
                    employee.LastName = vmEmployee.LastName;
                    employee.BranchId = vmEmployee.BranchId;
                    employee.DepartmentId = vmEmployee.DepartmentId;
                    employee.DesignationId = vmEmployee.DesignationId;
                    employee.OrgId = User.OrgId;
                    employee.MaritalStatus = vmEmployee.MaritalStatus;
                    employee.Gender = vmEmployee.Gender;
                    employee.Nationality = vmEmployee.Nationality;
                    employee.PresentAddress = vmEmployee.PresentAddress;
                    employee.ParmanentAddress = vmEmployee.ParmanentAddress;
                    employee.Email = vmEmployee.Email;
                    employee.MobileNo = vmEmployee.MobileNo;
                    employee.HomeContactNo = vmEmployee.HomeContactNo;
                    employee.DOB = vmEmployee.DOB;
                    employee.IsActive = vmEmployee.IsActive;
                    employee.AllowToLogin = vmEmployee.AllowToLogin;
                    employee.EntryUser = User.UserName;
                    employee.EntryDate = DateTime.Now;
                    employee.EmployeeCode = GenerateEmployeeCode(vmEmployee.BranchId,vmEmployee.DepartmentId);
                    employee.EmpType = "General";
                    db.tblEmployeeInfo.Add(employee);
                }
                else if (vmEmployee.EmployeeId != null && vmEmployee.EmployeeId > 0)
                {
                    // Edit Part
                    var employeeInDb = db.tblEmployeeInfo.Find(vmEmployee.EmployeeId);
                    if (employeeInDb == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        employeeInDb.FirstName = vmEmployee.FirstName;
                        employeeInDb.MiddleName = vmEmployee.MiddleName;
                        employeeInDb.LastName = vmEmployee.LastName;
                        employeeInDb.BranchId = vmEmployee.BranchId;
                        //employeeInDb.DepartmentId = vmEmployee.DepartmentId;
                        //employeeInDb.DesignationId = vmEmployee.DesignationId;
                        employeeInDb.MaritalStatus = vmEmployee.MaritalStatus;
                        employeeInDb.Gender = vmEmployee.Gender;
                        employeeInDb.Nationality = vmEmployee.Nationality;
                        employeeInDb.PresentAddress = vmEmployee.PresentAddress;
                        employeeInDb.ParmanentAddress = vmEmployee.ParmanentAddress;
                        employeeInDb.Email = vmEmployee.Email;
                        employeeInDb.MobileNo = vmEmployee.MobileNo;
                        employeeInDb.HomeContactNo = vmEmployee.HomeContactNo;
                        employeeInDb.DOB = vmEmployee.DOB;
                        employeeInDb.IsActive = vmEmployee.IsActive;
                        employeeInDb.AllowToLogin = vmEmployee.AllowToLogin;
                        //employeeInDb.EmployeeCode = GenerateEmployeeCode(vmEmployee.BranchId, vmEmployee.DepartmentId);
                        employeeInDb.UpdateBy = User.UserName;
                        employeeInDb.UpdateDate = DateTime.Now;
                    }
                }
                db.SaveChanges();
                return RedirectToAction("GetEmployeeList");
            }
            List<SelectListItem> ddlDepartment = BLCommon.GetDepartmentsByOrgForDDL(User.OrgId).OrderBy(d => d.Text).ToList();
            ViewBag.DepartmentList = ddlDepartment;

            List<SelectListItem> ddlDesignation = BLCommon.GetDesignationByOrgId(User.OrgId).OrderBy(d => d.Text).ToList();
            ViewBag.DesignationList = ddlDesignation;

            List<SelectListItem> ddlBranches = BLCommon.GetBranchByOrgId(User.OrgId).OrderBy(d => d.Text).ToList();
            ViewBag.BrancheList = ddlBranches;
            return View("CreateEmployee", vmEmployee);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult GetEmployeeDetails(int? id)
        {
            if(id == null || id <= 0)
            {
                return RedirectToAction("NotFound", "Error");
            }
            else
            {
                var obj = from emp in db.tblEmployeeInfo
                          join br in db.tblBranchInfo on emp.BranchId equals br.BranchId
                          join dept in db.tblDepartment on emp.DepartmentId equals dept.DepartmentId
                          join desig in db.tblDesignation on emp.DesignationId equals desig.DesigId
                          where emp.EmployeeId == id && emp.EmpType=="General"
                          select new
                          {
                              firstName=emp.FirstName,middleName=emp.MiddleName,lastName=emp.LastName,
                              branch=br.BranchName,department = dept.DepartmentName,designation = desig.Name,
                              maritalStatus=emp.MaritalStatus,gender = emp.Gender,nationality=emp.Nationality,
                              parmanentAdd=emp.ParmanentAddress,presentAdd=emp.PresentAddress,
                              email=emp.Email,mobile=emp.MobileNo,homeContact= emp.HomeContactNo,
                              dob= emp.DOB,active =emp.IsActive,allowLogin=emp.AllowToLogin
                          };
                
                
                return Json(obj);
            }
        }

        private string GenerateEmployeeCode(int? BranchId, int? departmentId)
        {
            string empCode = string.Empty;
            int totalEmpByThisDept = db.tblEmployeeInfo.Where(emp => emp.DepartmentId == departmentId && emp.BranchId == BranchId).Count();
            empCode = (BranchId.ToString().Length > 1 ? BranchId.ToString() : "0" + BranchId.ToString()) +
                          (departmentId.ToString().Length > 1 ? departmentId.ToString() : "0" + departmentId.ToString()) +
                          (totalEmpByThisDept + 1).ToString().PadLeft(4, '0');
            //string shortName = db.tblBranchInfo.FirstOrDefault(b => b.BranchId == BranchId).ShortName.ToString();
            //string deptShortName = db.tblDepartment.FirstOrDefault(d => d.DepartmentId == departmentId).ShortName.ToString();
            //if (!string.IsNullOrEmpty(shortName.Trim()) && !string.IsNullOrEmpty(deptShortName.Trim()))
            //{

            //}
            //empCode = "EMP-" + shortName + "-" + deptShortName + "-" + (totalEmpByThisDept + 1).ToString().PadLeft(5, '0');
            return empCode;
        }


    }
}