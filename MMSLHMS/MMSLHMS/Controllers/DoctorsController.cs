using MMSLHMS.DAL;
using MMSLHMS.DAL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMSBO.ViewModels;
using MMSLHMS.CustomFiles.Filters;
using System.IO;
using System.Web.Helpers;
using HMSBO.Models;


namespace MMSLHMS.Controllers
{
    [CustomAuthorize]
    public class DoctorsController : BaseController
    {
        // GET: Doctors
        private DataContext db = new DataContext();
        private BLCommon BL = new BLCommon();

        [HttpGet]
        public ActionResult AllDoctorProfileList(int? initialize)
        {
            if(initialize == null)
            {
                return View();
            }
            else
            {
                var doctorProfilList = db.tblDoctorProfile.ToList();
                List<vmDoctor> doctorLists = new List<vmDoctor>();
                foreach (var item in doctorProfilList)
                {
                    vmDoctor vmDoctor = new vmDoctor();
                    vmDoctor.Id = item.Id;
                    vmDoctor.DoctorId = item.DoctorId;
                    vmDoctor.Name = item.FirstName + " " + item.MiddleName +" "+item.LastName;
                    vmDoctor.City = item.City;
                    vmDoctor.MobileNumber = item.ContactNumber;
                    vmDoctor.ParmanentAddress = item.ParmanentAddress;
                    vmDoctor.ImageSrc = Utility.GetImage(item.PhotoUrl);
                    doctorLists.Add(vmDoctor);
               }
                return Json(doctorLists, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult CreateProfile(int? id)
        {
            List<SelectListItem> ddlDepartment = BLCommon.GetDepartmentsByOrgForDDL(User.OrgId).OrderBy(d=>d.Text).ToList();
            ViewBag.DepartmentList = ddlDepartment;

            List<SelectListItem> ddlDesignation = BLCommon.GetDesignationByOrgId(User.OrgId).OrderBy(d => d.Text).ToList();
            ViewBag.DesignationList = ddlDesignation;

            List<SelectListItem> ddlSpecialization = BLCommon.GetSpecializationTypeByOrgId(User.OrgId).OrderBy(d => d.Text).ToList();
            ViewBag.SpecializationList = ddlSpecialization;

            List<SelectListItem> ddlBranches = BLCommon.GetBranchByOrgId(User.OrgId).OrderBy(d => d.Text).ToList();
            ViewBag.BrancheList = ddlBranches;

            if (id == null || id <= 0)
            {
                return View();
            }
            else
            {
                var doctor =db.tblDoctorProfile.FirstOrDefault(d => d.Id == id);
                if(doctor == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    vmCreateDoctorProfile vmDoctor = new vmCreateDoctorProfile()
                    {
                        Id = doctor.Id,
                        Prefix = doctor.Prefix,
                        FirstName = doctor.FirstName,
                        MiddleName = doctor.MiddleName,
                        LastName = doctor.LastName,
                        FatherName = doctor.FatherName,
                        MotherName = doctor.MotherName,
                        SpouseName = doctor.SpouseName,
                        MaritalStatus=doctor.MaritalStatus,
                        AllowToLogin=doctor.AllowToLogin,
                        Email=doctor.Email,
                        ParmanentAddress = doctor.ParmanentAddress,
                        PresentAddress = doctor.PresentAddress,
                        ContactNumber = doctor.ContactNumber,
                        PhoneNumber = doctor.PhoneNumber,
                        Gender = doctor.Gender,
                        City = doctor.City,
                        DateOfBirth = doctor.DateOfBirth,
                        Degrees = doctor.Degrees,
                        Experiences = doctor.Experiences,
                        CurrentJobLocation = doctor.CurrentJobLocation,
                        YearsOfExperience = doctor.YearsOfExperience,
                        About = doctor.About,
                        DepartmentId = doctor.DepartmentId,
                        DoctorId = doctor.DoctorId,
                        IsActive = doctor.IsActive,
                        PhotoUrl = doctor.PhotoUrl,
                        DesignationId = doctor.DesignationId,
                        SpecializationTypeId = doctor.SpecializationTypeId,
                        LicenseNo = doctor.LicenseNo,
                        InPatientChargeType = doctor.InPatientChargeType,
                        InPatientChargeValue = doctor.InPatientChargeValue,
                        OutPatientChargeType = doctor.OutPatientChargeType,
                        OutPatientChargeValue = doctor.OutPatientChargeValue,
                        BranchId=doctor.BranchId,
                        Nationality=doctor.Nationality,
                       
                    };
                    
                    return View(vmDoctor);
                }
            }
        }

        [HttpPost, ValidateInput(true), ValidateJsonAntiForgeryToken]
        public ActionResult SaveDoctorProfile(vmCreateDoctorProfile model)
        {
            bool isSuccess = false;
            if (ModelState.IsValid) {
                if (model.Id == 0) {
                    //--- Start New Entry ---//
                    DoctorProfile doctorProfile = new DoctorProfile();
                    doctorProfile.Prefix = model.Prefix;
                    doctorProfile.FirstName = model.FirstName;
                    doctorProfile.MiddleName = model.MiddleName;
                    doctorProfile.LastName = model.LastName;
                    doctorProfile.FatherName = model.FatherName;
                    doctorProfile.MotherName = model.MotherName;
                    doctorProfile.SpouseName = model.SpouseName;
                    doctorProfile.ParmanentAddress = model.ParmanentAddress;
                    doctorProfile.PresentAddress = model.PresentAddress;
                    doctorProfile.ContactNumber = model.ContactNumber;
                    doctorProfile.PhoneNumber = model.PhoneNumber;
                    doctorProfile.Gender = model.Gender;
                    doctorProfile.City = model.City;
                    doctorProfile.DateOfBirth = model.DateOfBirth;
                    doctorProfile.Degrees = model.Degrees;
                    doctorProfile.Experiences = model.Experiences;
                    doctorProfile.CurrentJobLocation = model.CurrentJobLocation;
                    doctorProfile.YearsOfExperience = model.YearsOfExperience;
                    doctorProfile.About = model.About;
                    doctorProfile.MaritalStatus = model.MaritalStatus;
                    doctorProfile.Email = model.Email;
                    doctorProfile.AllowToLogin = model.AllowToLogin;
                    
                    WebImage imgFile = new WebImage(model.Photo.InputStream);
                    imgFile.Resize(140, 150, false, true);
                    string fileName = model.Photo.FileName.Substring(0, (model.Photo.FileName.LastIndexOf(".")));
                    string extension = imgFile.ImageFormat;//GetExtension(imgFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssff");//+ extension;
                    imgFile.Save(Path.Combine(@"E:/ProfilePhoto", fileName));

                    doctorProfile.PhotoUrl = "E:/ProfilePhoto/" + fileName+"."+ extension;
                    doctorProfile.DepartmentId = model.DepartmentId;
                    doctorProfile.IsActive = model.IsActive;
                    doctorProfile.EntryDate = DateTime.Now;
                    doctorProfile.EntryUser = User.UserName;
                    doctorProfile.ApprovedUser = User.UserName;
                    doctorProfile.ApprovedDate = DateTime.Now;
                    doctorProfile.DoctorId = GenerateDoctorId(model.DepartmentId);
                    doctorProfile.DesignationId = model.DesignationId;
                    doctorProfile.SpecializationTypeId = model.SpecializationTypeId;
                    doctorProfile.LicenseNo = model.LicenseNo;
                    doctorProfile.InPatientChargeType = model.InPatientChargeType;
                    doctorProfile.InPatientChargeValue = model.InPatientChargeValue;
                    doctorProfile.OutPatientChargeType = model.OutPatientChargeType;
                    doctorProfile.OutPatientChargeValue = model.OutPatientChargeValue;
                    doctorProfile.BranchId = model.BranchId;
                    string empCode = GenerateEmployeeCode(model.BranchId, model.DepartmentId);
                    doctorProfile.EmployeeCode = empCode;
                    doctorProfile.OrgId= User.OrgId;
                    doctorProfile.Nationality = model.Nationality;
                    db.tblDoctorProfile.Add(doctorProfile);
                    isSuccess = true;
                    //--- End New Entry ---//

                    //--- Employee Entry ----//
                    Employee employee = new Employee();
                    employee.FirstName = model.FirstName;
                    employee.MiddleName = model.MiddleName;
                    employee.LastName = model.LastName;
                    employee.Gender = model.Gender;
                    employee.OrgId = User.OrgId;
                    employee.MaritalStatus = model.MaritalStatus;
                    employee.MobileNo = model.PhoneNumber;
                    employee.HomeContactNo = model.ContactNumber;
                    employee.BranchId = model.BranchId;
                    employee.DepartmentId = model.DepartmentId;
                    employee.DesignationId = model.DesignationId;
                    employee.DOB = model.DateOfBirth;
                    employee.ParmanentAddress = model.ParmanentAddress;
                    employee.PresentAddress = model.PresentAddress;
                    employee.EmpType = "Doctor";
                    employee.Email = model.Email;
                    employee.EmployeeCode = empCode;
                    employee.EntryUser = User.UserName;
                    employee.EntryDate = DateTime.Now;
                    employee.Nationality = model.Nationality;
                    employee.PhotoUrl = doctorProfile.PhotoUrl;
                    employee.IsActive = model.IsActive;
                    employee.AllowToLogin = model.AllowToLogin;
                    db.tblEmployeeInfo.Add(employee);
                    //--- Employee Entry ----//
                }
                else if (model.Id > 0)
                {
                    var doctorIndb = db.tblDoctorProfile.FirstOrDefault(d => d.Id == model.Id);
                    if(doctorIndb == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        doctorIndb.Prefix = model.Prefix;
                        doctorIndb.FirstName = model.FirstName;
                        doctorIndb.MiddleName = model.MiddleName;
                        doctorIndb.LastName = model.LastName;
                        doctorIndb.FatherName = model.FatherName;
                        doctorIndb.MotherName = model.MotherName;
                        doctorIndb.SpouseName = model.SpouseName;
                        doctorIndb.ParmanentAddress = model.ParmanentAddress;
                        doctorIndb.PresentAddress = model.PresentAddress;
                        doctorIndb.ContactNumber = model.ContactNumber;
                        doctorIndb.PhoneNumber = model.PhoneNumber;
                        doctorIndb.Gender = model.Gender;
                        doctorIndb.City = model.City;
                        doctorIndb.DateOfBirth = model.DateOfBirth;
                        doctorIndb.Degrees = model.Degrees;
                        doctorIndb.Experiences = model.Experiences;
                        doctorIndb.CurrentJobLocation = model.CurrentJobLocation;
                        doctorIndb.YearsOfExperience = model.YearsOfExperience;
                        doctorIndb.About = model.About;
                        doctorIndb.Nationality = model.Nationality;
                        doctorIndb.MaritalStatus = model.MaritalStatus;

                        if(model.Photo != null)
                        {
                            WebImage imgFile = new WebImage(model.Photo.InputStream);
                            imgFile.Resize(140, 150, false, true);
                            string fileName = model.Photo.FileName.Substring(0, (model.Photo.FileName.LastIndexOf(".")));
                            string extension = imgFile.ImageFormat;//GetExtension(imgFile.FileName);
                            fileName = fileName + DateTime.Now.ToString("yymmssff");//+ extension;
                            imgFile.Save(Path.Combine(@"E:/ProfilePhoto", fileName));
                            doctorIndb.PhotoUrl = "E:/ProfilePhoto/" + fileName + "." + extension;
                        }
                        
                        doctorIndb.IsActive = model.IsActive;
                        doctorIndb.LicenseNo = model.LicenseNo;

                        doctorIndb.InPatientChargeType = model.InPatientChargeType;
                        doctorIndb.InPatientChargeValue = model.InPatientChargeValue;
                        doctorIndb.OutPatientChargeType = model.OutPatientChargeType;
                        doctorIndb.OutPatientChargeValue = model.OutPatientChargeValue;

                        var employeeInDb = db.tblEmployeeInfo.FirstOrDefault(emp=> emp.EmployeeCode == doctorIndb.EmployeeCode);
                        //var userInDb = db.tblUsers.FirstOrDefault(us => us.EmpId == doctorIndb.EmployeeCode);

                        if (!model.IsActive)
                        {
                            doctorIndb.IsActive = false;
                            doctorIndb.AllowToLogin = false;
                            if (employeeInDb != null)
                            {
                                employeeInDb.IsActive = false;
                                employeeInDb.AllowToLogin = false;
                            }
                            //if (userInDb != null)
                            //{

                            //}
                        }
                        else if (!model.AllowToLogin)
                        {
                            doctorIndb.IsActive = model.IsActive;
                            doctorIndb.AllowToLogin = model.AllowToLogin;
                            if (employeeInDb != null)
                            {
                                employeeInDb.IsActive = model.IsActive;
                                employeeInDb.AllowToLogin = false;
                            }
                            //if (userInDb != null)
                            //{

                            //}
                        }
                        else
                        {
                            doctorIndb.IsActive = model.IsActive;
                            doctorIndb.AllowToLogin = model.AllowToLogin;
                            if (employeeInDb != null)
                            {
                                employeeInDb.IsActive = model.IsActive;
                                employeeInDb.AllowToLogin = model.AllowToLogin;
                            }
                        }
                        if (employeeInDb != null)
                        {
                            employeeInDb.FirstName = model.FirstName;
                            employeeInDb.MiddleName = model.MiddleName;
                            employeeInDb.LastName = model.LastName;
                            employeeInDb.ParmanentAddress = model.ParmanentAddress;
                            employeeInDb.PresentAddress = model.PresentAddress;
                            employeeInDb.MobileNo = model.ContactNumber;
                            employeeInDb.HomeContactNo= model.PhoneNumber;
                            employeeInDb.Gender = model.Gender;
                            employeeInDb.DOB = model.DateOfBirth;
                            employeeInDb.Nationality = model.Nationality;
                            employeeInDb.MaritalStatus = model.MaritalStatus;
                        }
                        isSuccess = true;
                    }
                }
                db.SaveChanges();
            }
            return Json(isSuccess);
        }

        [HttpPost,ValidateJsonAntiForgeryToken()]
        public ActionResult DeleteDoctor(int id)
        {
            bool IsSuccess = false;
            var deleteDoctor = db.tblDoctorProfile.FirstOrDefault(d => d.Id == id);
            if(deleteDoctor == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.tblDoctorProfile.Remove(deleteDoctor);
                db.SaveChanges();
                IsSuccess = true;
            }
            return Json(IsSuccess);
        }
        //----------- No Action ------------//

        [NonAction]
        private string GenerateDoctorId(int? departmentId)
        {
            
            string dcId = string.Empty;
            if (departmentId == null) return dcId;
            var MaxDoctorId = db.tblDoctorProfile.Where(d => d.DepartmentId == departmentId).OrderByDescending(d=>d.Id).FirstOrDefault();
            //var deptShortName = db.tblDepartment.SingleOrDefault(s => s.DepartmentId == departmentId).ShortName;
            if(MaxDoctorId != null)
            {
                var maxId = (Convert.ToInt32(MaxDoctorId.DoctorId.Substring(MaxDoctorId.DoctorId.LastIndexOf("-") + 1)) + 1);
                if (!string.IsNullOrEmpty(MaxDoctorId.DoctorId) && !string.IsNullOrEmpty(MaxDoctorId.DoctorId))
                {
                    dcId = MaxDoctorId.DoctorId.Substring(0, MaxDoctorId.DoctorId.LastIndexOf("-") + 1) + (maxId.ToString().PadLeft(5, '0'));
                }
            }
            else
            {
                var dept =db.tblDepartment.FirstOrDefault(d => d.DepartmentId == departmentId);
                dcId = "DOC-"+dept.ShortName+"-"+ ((1).ToString().PadLeft(5, '0'));
            }
            return dcId;
        }
        

        [NonAction]
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