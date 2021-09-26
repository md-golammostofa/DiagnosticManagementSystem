using HMSBO.Models;
using HMSBO.ReportModels;
using HMSBO.ViewModels;
using Microsoft.Reporting.WebForms;
using MMSLHMS.CustomFiles.Filters;
using MMSLHMS.DAL;
using MMSLHMS.DAL.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMSLHMS.Controllers
{
    [CustomAuthorize]
    public class AppoinmentController : BaseController
    {
        private DataContext db = new DataContext();
        // GET: Appoinment
        #region Doctors
        [HttpGet]
        public ActionResult GetDoctorList(string flag,string doctorName,string specialist, string mobile,string sex)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlSpecialist = db.tblSpecialists.Select(s => new SelectListItem { Text = s.Specialization, Value = s.Specialization }).ToList();
                ViewBag.txtDoctorName = db.tblDoctors.Select(s => new SelectListItem { Text = s.DocName, Value = s.DocName }).ToList();
                return View();
            }
            else
            {
                var doctor = db.tblDoctors.Where(d=>d.OrgId==User.OrgId && (doctorName == null || doctorName.Trim() == "" || d.DocName.ToLower().Contains(doctorName.ToLower().Trim())) && (specialist == null || specialist.Trim() == "" || d.DocSpecialist.ToLower().Contains(specialist.ToLower().Trim())) && (mobile == null || mobile.Trim() == "" || d.DocMobile.ToLower().Contains(mobile.ToLower().Trim())) && (sex == null || sex.Trim() == "" || d.Sex.ToLower().Contains(sex.ToLower().Trim()))).ToList();
                List<VmDoctors> doctors = new List<VmDoctors>();
                foreach(var item in doctor)
                {
                    VmDoctors vm = new VmDoctors();
                    vm.DocId = item.DocId;
                    vm.DocName = item.DocName;
                    vm.DocDegree = item.DocDegree;
                    vm.DocRegNo = item.DocRegNo;
                    vm.DocAddress = item.DocAddress;
                    vm.DocType = item.DocType;
                    vm.DocSpecialist = item.DocSpecialist;
                    vm.DocMobile = item.DocMobile;
                    vm.DocEmail = item.DocEmail;
                    vm.HospitalName = item.HospitalName;
                    vm.Remarks = item.Remarks;
                    vm.GenerelFee = item.GenerelFee;
                    vm.ReportFee = item.ReportFee;
                    vm.CounselingFee = item.CounselingFee;
                    vm.FollowUpFee = item.FollowUpFee;
                    vm.Sex = item.Sex;
                    vm.DOB = item.DOB;
                    doctors.Add(vm);
                }
                return PartialView("_GetDoctorList", doctors);
            }
        }

        public ActionResult CreateDoctor()
        {
            ViewBag.ddlSpecialist = db.tblSpecialists.Select(s => new SelectListItem { Text = s.Specialization, Value = s.Specialization }).ToList();

            ViewBag.ddlOtherProfession = db.tblOtherProfessional.Select(s => new SelectListItem { Text = s.Profession, Value = s.Profession }).ToList();

            ViewBag.ddlStartTime = db.tblTimes.Select(s => new SelectListItem { Text = s.ScendTimeF, Value = s.Time.ToString() }).ToList();

            ViewBag.ddlEndTime = db.tblTimes.Select(s => new SelectListItem { Text = s.ScendTimeF, Value = s.Time.ToString() }).ToList();
            return View();
        }

        public ActionResult SaveDoctorProfile(VmDoctors model)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                Doctor doctor = new Doctor();
                doctor.DocName = model.DocName;
                doctor.DocDegree = model.DocDegree;
                doctor.DocRegNo = model.DocRegNo;
                doctor.DocAddress = model.DocAddress;
                doctor.DocType = model.DocType;
                doctor.DocSpecialist = model.DocSpecialist;
                doctor.DocMobile = model.DocMobile;
                doctor.DocEmail = model.DocEmail;
                doctor.HospitalName = model.HospitalName;
                doctor.Remarks = model.Remarks;
                doctor.GenerelFee = model.GenerelFee;
                doctor.ReportFee = model.ReportFee;
                doctor.FollowUpFee = model.FollowUpFee;
                doctor.CounselingFee = model.CounselingFee;
                doctor.OtherProfession = model.OtherProfession;
                doctor.Sex = model.Sex;
                doctor.DOB = model.DOB;
                doctor.EntryUser = User.UserName;
                doctor.EntryDate = DateTime.Now;
                doctor.OrgId = User.OrgId;
                //if (model.DoctorImage != null)
                //{
                //    doctor.DocImage = Utility.SaveImage(model.DoctorImage.InputStream, model.DoctorImage.FileName, Utility.DoctorImage, User.OrgId);
                //}

                List<DoctorDetails> detailslist = new List<DoctorDetails>();
                foreach (var item in model.doctorDetails)
                {
                    DoctorDetails details = new DoctorDetails
                    {
                        DocName = model.DocName,
                        DocSpecialist = model.DocSpecialist,
                        DocDegree = model.DocDegree,
                        GenerelFee = model.GenerelFee,
                        DocMobile = model.DocMobile,
                        Day = item.Day,
                        DayOfTime = item.DayOfTime,
                        StartTime = item.StartTime,
                        EndTime = item.EndTime,
                        EntryUser = User.UserName,
                        EntryDate = DateTime.Now,
                        OrgId = User.OrgId
                    };
                    detailslist.Add(details);
                }
                doctor.DoctorDetails = detailslist;
                db.tblDoctors.Add(doctor);
                IsSuccess = true;
                db.SaveChanges();
            }
            return Json(IsSuccess);
        }
        public ActionResult SaveDoctorFile(VmDoctors model)
        {
            bool IsSuccess = false;
            var dt = db.tblDoctors.Where(d => d.OrgId == User.OrgId).ToList();
            var dot = dt.LastOrDefault().DocId;
            if (dot > 0 && model.DoctorImage != null)
            {
                var docctor = db.tblDoctors.Where(d => d.DocId == dot && d.OrgId == User.OrgId).FirstOrDefault();
                docctor.DocImage = Utility.SaveImage(model.DoctorImage.InputStream, model.DoctorImage.FileName, Utility.DoctorImage, User.OrgId);
                IsSuccess = true;
                db.SaveChanges();
            }
            return Json(IsSuccess);
        }

        public ActionResult DoctorScheduleList(string flag,string name,string specialist,string days,string shift)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlDoctorName = db.tblDoctors.Where(d => d.OrgId == User.OrgId).Select(d => new SelectListItem { Text = d.DocName, Value = d.DocName }).ToList();
                ViewBag.ddlSpecialist = db.tblSpecialists.Where(s=>s.OrgId==User.OrgId).Select(s => new SelectListItem { Text = s.Specialization, Value = s.Specialization }).ToList();
                return View();
            }
            else
            {
                var doctorDetails = db.tblDoctorDetails.Where(d => d.OrgId == User.OrgId && (name == null || name.Trim() == "" || d.DocName.ToLower().Contains(name.ToLower().Trim())) && (specialist == null || specialist.Trim() == "" || d.DocSpecialist.ToLower().Contains(specialist.ToLower().Trim())) && (days == null || days.Trim() == "" || d.Day.ToLower().Contains(days.ToLower().Trim())) && (shift == null || shift.Trim() == "" || d.DayOfTime.ToLower().Contains(shift.ToLower().Trim())) ).ToList();
                List<VmDoctorDetails> list = new List<VmDoctorDetails>();
                foreach(var item in doctorDetails)
                {
                    VmDoctorDetails details = new VmDoctorDetails()
                    {
                        DocName=item.DocName,
                        DocDegree=item.DocDegree,
                        DocSpecialist=item.DocSpecialist,
                        DocMobile=item.DocMobile,
                        GenerelFee=item.GenerelFee,
                        Day=item.Day,
                        DayOfTime=item.DayOfTime,
                        StartTime=item.StartTime,
                        EndTime=item.EndTime
                    };
                    list.Add(details);
                }
                return PartialView("_DoctorScheduleList", list);
            }
        }

        public ActionResult DoctorTimeDetails(long docId)
        {
            var doctorTime = db.tblDoctorDetails.Where(d => d.DocId == docId).ToList();
            List<VmDoctorDetails> doclist = new List<VmDoctorDetails>();
            foreach(var item in doctorTime)
            {
                VmDoctorDetails details = new VmDoctorDetails
                {
                    DocName = item.DocName,
                    DocSpecialist = item.DocSpecialist,
                    DocDegree = item.DocDegree,
                    DocMobile = item.DocMobile,
                    Day = item.Day,
                    DayOfTime=item.DayOfTime,
                    StartTime=item.StartTime,
                    EndTime=item.EndTime,
                };
                doclist.Add(details);
            }
            return PartialView("_DoctorTimeDetails", doclist);
        }
        #endregion

        #region - Base64 Image String
        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult GetImageBase64String(long docId)
        {
            string docImg = string.Empty;
            try
            {
                if (docId > 0)
                {
                    var alldoctor = db.tblDoctors.Where(o => o.DocId == docId).ToList();
                    var doctor = alldoctor.FirstOrDefault();
                    if (doctor != null)
                    {
                        if (!string.IsNullOrEmpty(doctor.DocImage))
                        {
                            docImg = Utility.GetImage(string.Format(@"{0}", doctor.DocImage));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { doctorImg = docImg});
        }
        #endregion

        #region Appoinment
        public ActionResult GetAppoinmentList(string flag,string doctorName,string shift,string patientName,string patientMobile, string fromDate="", string toDate="")
        {

            DateTime dtimeFromDate = new DateTime();
            DateTime dtimeToDate = new DateTime();
            if (fromDate.Trim() != "")
            {
                dtimeFromDate = Convert.ToDateTime(fromDate);
            }
            if (toDate.Trim() != "")
            {
                dtimeToDate = Convert.ToDateTime(toDate);
            }

            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlDoctorName = db.tblDoctors.Where(d => d.OrgId == User.OrgId).Select(d => new SelectListItem { Text = d.DocName, Value = d.DocName }).ToList();
                return View();
            }
            else
            {
                var appoinment = db.tblAppoinments.OrderBy(a => a.EntryDate).Where(a => a.OrgId == User.OrgId && (doctorName == null || doctorName.Trim() == "" || a.DoctorName.ToLower().Contains(doctorName.ToLower().Trim())) && (patientName == null || patientName.Trim() == "" || a.PatientName.ToLower().Contains(patientName.ToLower().Trim())) && (patientMobile == null || patientMobile.Trim() == "" || a.PatientMobile.ToLower().Contains(patientMobile.ToLower().Trim())) && (shift == null || shift.Trim() == "" || a.Shift.ToLower().Contains(shift.ToLower().Trim()))
                &&
                               (
                                    (fromDate == "" && toDate == "") ||

                                    (((fromDate != "" && fromDate != null) && (toDate != "" && toDate != null)) && DbFunctions.TruncateTime(a.AppoinmentDate) >= DbFunctions.TruncateTime(dtimeFromDate) && DbFunctions.TruncateTime(a.AppoinmentDate) <= DbFunctions.TruncateTime(dtimeToDate)) ||

                                    ((fromDate != "" && fromDate != null) && DbFunctions.TruncateTime(a.AppoinmentDate) == DbFunctions.TruncateTime(dtimeFromDate)) ||

                                    ((toDate != "" && toDate != null) && DbFunctions.TruncateTime(a.AppoinmentDate) == DbFunctions.TruncateTime(dtimeToDate))
                               )
                ).ToList();
                List<VmAppoinment> list = new List<VmAppoinment>();
                foreach(var item in appoinment)
                {
                    VmAppoinment appoin= new VmAppoinment();
                    appoin.AppId = item.AppId;
                    appoin.DoctorName = item.DoctorName;
                    appoin.PatientName = item.PatientName;
                    appoin.PatientAddress = item.PatientAddress;
                    appoin.PatientMobile = item.PatientMobile;
                    appoin.Age = item.Age;
                    appoin.SateStatus = item.SateStatus;
                    appoin.Shift = item.Shift;
                    appoin.AppoinmentDate = item.AppoinmentDate;
                    list.Add(appoin);
                }
                return PartialView("_GetAppoinmentList", list);
            }
        }
        public ActionResult GetAppoinmentListReport(string flag, string rptType, string ddlDoctorName, string shift, string fromDate = "", string toDate = "")
        {
            bool IsSuccess = false;


            DateTime dtimeFromDate = new DateTime();
            DateTime dtimeToDate = new DateTime();
            if (fromDate.Trim() != "")
            {
                dtimeFromDate = Convert.ToDateTime(fromDate);
            }
            if (toDate.Trim() != "")
            {
                dtimeToDate = Convert.ToDateTime(toDate);
            }

            var appoinment = db.tblAppoinments.OrderBy(a => a.EntryDate).Where(a => a.OrgId == User.OrgId && (ddlDoctorName == null || ddlDoctorName.Trim() == "" || a.DoctorName.ToLower().Contains(ddlDoctorName.ToLower().Trim())) && (shift == null || shift.Trim() == "" || a.Shift.ToLower().Contains(shift.ToLower().Trim()))
                &&
                               (
                                    (fromDate == "" && toDate == "") ||

                                    (((fromDate != "" && fromDate != null) && (toDate != "" && toDate != null)) && DbFunctions.TruncateTime(a.AppoinmentDate) >= DbFunctions.TruncateTime(dtimeFromDate) && DbFunctions.TruncateTime(a.AppoinmentDate) <= DbFunctions.TruncateTime(dtimeToDate)) ||

                                    ((fromDate != "" && fromDate != null) && DbFunctions.TruncateTime(a.AppoinmentDate) == DbFunctions.TruncateTime(dtimeFromDate)) ||

                                    ((toDate != "" && toDate != null) && DbFunctions.TruncateTime(a.AppoinmentDate) == DbFunctions.TruncateTime(dtimeToDate))
                               )
                ).ToList();
            List<CommonReport> commons = new List<CommonReport>();
            CommonReport com = new CommonReport();
            com.FromDate = fromDate;
            com.ToDate = toDate;
            com.Name = ddlDoctorName;
            commons.Add(com);


            LocalReport localReport = new LocalReport();
            string reportPath = Server.MapPath("~/Reports/rptAppoinmentReport.rdlc");
            if (System.IO.File.Exists(reportPath))
            {
                localReport.ReportPath = reportPath;
                ReportDataSource dataSource1 = new ReportDataSource("Appoinment", appoinment);
                ReportDataSource dataSource2 = new ReportDataSource("Common", commons);
                localReport.DataSources.Clear();
                localReport.DataSources.Add(dataSource1);
                localReport.DataSources.Add(dataSource2);
                localReport.Refresh();
                localReport.DisplayName = "Appoinment";

                string mimeType;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = localReport.Render(
                    rptType,
                    "",
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);
                return File(renderedBytes, mimeType);
            }
            return new EmptyResult();

        }
        public ActionResult CreateAppoinment()
        {
            ViewBag.ddlDoctorName = db.tblDoctors.Where(d => d.OrgId == User.OrgId).Select(d => new SelectListItem { Text = d.DocName, Value = d.DocName }).ToList();
            return View();
        }

        public ActionResult SaveAppoinment(VmAppoinment model)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                Appoinment appoinment = new Appoinment
                {
                    DoctorName = model.DoctorName,
                    PatientName = model.PatientName,
                    PatientAddress = model.PatientAddress,
                    PatientMobile = model.PatientMobile,
                    Age = model.Age,
                    Shift = model.Shift,
                    SateStatus = model.SateStatus,
                    AppoinmentDate = model.AppoinmentDate,
                    OrgId = User.OrgId,
                    EntryDate = DateTime.Now,
                    EntryUser = User.UserName,
                };
                db.tblAppoinments.Add(appoinment);
                db.SaveChanges();
                IsSuccess = true;
            }
            return Json(IsSuccess);
        }
        #endregion

    }
}