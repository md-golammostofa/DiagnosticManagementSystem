using HMSBLL;
using HMSBO.Models;
using HMSBO.ViewModels;
//using Microsoft.Reporting.WinForms;
using Microsoft.Reporting.WebForms;
using MMSLHMS.CustomFiles.Filters;
using MMSLHMS.DAL;
using MMSLHMS.DAL.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMSBO.ReportModels;
using System.Drawing;
using PagedList.Mvc;
using PagedList;

namespace MMSLHMS.Controllers
{
    [CustomAuthorize]
    public class DiagnosticController : BaseController
    {
        // GET: Diagnostic
        private DataContext db = new DataContext();

        [HttpGet]
        public ActionResult GetInvestigationList(string InvestigationName, int? Fees)
        {
            var investigations = (from diag in db.tblInvestigations
                                  join org in db.tblOrganizations on diag.OrgId equals org.OrgId
                                  where diag.OrgId == User.OrgId && (InvestigationName == null || InvestigationName.Trim() == "" || diag.Name.ToLower().Contains(InvestigationName.ToLower().Trim()))
                                  && (Fees == null || Fees == 0 || diag.Fee == Fees)
                                  select new { diag.Id, diag.Name, diag.Fee,diag.RefFee, diag.EntryUser, diag.EntryDate, diag.UpdateBy, diag.UpdateDate, org.OrgId, OrgName = org.Name, BranchId = diag.BranchId }).ToList();

            List<VmInvestigation> invList = new List<VmInvestigation>();
            foreach (var item in investigations)
            {
                VmInvestigation inv = new VmInvestigation();
                inv.Id = item.Id;
                inv.Name = item.Name;
                inv.Fee = item.Fee;
                inv.RefFee = item.RefFee;
                inv.EntryUser = item.EntryUser;
                inv.EntryDate = item.EntryDate;
                inv.UpdateBy = item.UpdateBy;
                inv.UpdateDate = item.UpdateDate;
                inv.OrgId = item.OrgId;
                inv.OrgName = item.OrgName;
                inv.BranchId = item.BranchId;
                invList.Add(inv);
            }

            if (InvestigationName == null && Fees == null)
            {
                return View(invList);
            }
            else
            {
                return Json(invList, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveInvestigation(VmInvestigation model)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                if (model.Id <= 0)
                {
                    Investigation inv = new Investigation();
                    //inv.Name = model.Name;
                    inv.Fee = model.Fee;
                    inv.OrgId = User.OrgId;
                    inv.RefFee = model.RefFee;
                    inv.EntryUser = User.UserName;
                    inv.EntryDate = DateTime.Now;
                    db.tblInvestigations.Add(inv);
                    IsSuccess = true;
                }
                else if (model.Id > 0)
                {
                    var invInDb = db.tblInvestigations.FirstOrDefault(inv => inv.Id == model.Id);
                    if (invInDb != null)
                    {
                        //invInDb.Name = model.Name;
                        invInDb.Fee = model.Fee;
                        invInDb.UpdateBy = User.UserName;
                        invInDb.UpdateDate = DateTime.Now;
                        invInDb.RefFee = model.RefFee;
                        IsSuccess = true;
                    }
                }
                db.SaveChanges();
            }
            return Json(IsSuccess);
        }
        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveInvestigationFromChart(List<VmInvestigation> model)
        {
            bool IsSuccess = false;
            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var item in model)
                    {
                        Investigation investigation = new Investigation()
                        {
                            Name = item.Name,
                            Fee = item.Fee,
                            RefFee=item.RefFee,
                            OrgId = User.OrgId,
                            ChartId = item.ChartId,
                            EntryUser = User.UserName,
                            EntryDate = DateTime.Now
                        };
                        db.tblInvestigations.Add(investigation);
                    }
                    db.SaveChanges();
                    IsSuccess = true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return Json(IsSuccess);
        }

        [HttpGet]
        public JsonResult GetInvestigationById(int id)
        {
            var inv = db.tblInvestigations.Find(id);
            return Json(inv, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetReferrerList(string ReferrerName, string ReferrerAddress, string ReferrerMobile)
        {
            var data = (from inv in db.tblInvestigatinReferrer
                        join org in db.tblOrganizations
                        on inv.OrgId equals org.OrgId
                        where org.OrgId == User.OrgId
                        && (ReferrerName == null || ReferrerName.Trim() == "" || inv.Name.ToLower().Contains(ReferrerName.ToLower().Trim()))
                        && (ReferrerAddress == null || ReferrerAddress.Trim() == "" || inv.Address.ToLower().Contains(ReferrerAddress.Trim().ToLower()))
                        && (ReferrerMobile == null || ReferrerMobile.Trim() == "" || inv.PhoneNumber.ToLower().Contains(ReferrerMobile.Trim().ToLower()))
                        select new { Id = inv.Id, inv.Name, inv.Address, inv.PhoneNumber, inv.EntryUser, inv.EntryDate, inv.UpdateBy, inv.UpdateDate, OrgName = org.Name, OrgId = org.OrgId }).ToList();

            List<VmInvestigationReferrar> invList = new List<VmInvestigationReferrar>();
            foreach (var item in data)
            {
                VmInvestigationReferrar invRef = new VmInvestigationReferrar();
                invRef.Id = item.Id;
                invRef.Name = item.Name;
                invRef.Address = item.Address;
                invRef.PhoneNumber = item.PhoneNumber;
                invRef.OrgId = item.OrgId;
                invRef.OrgName = item.OrgName;
                invRef.EntryUser = item.EntryUser;
                invRef.EntryDate = item.EntryDate;
                invRef.UpdateBy = item.UpdateBy;
                invRef.UpdateDate = item.UpdateDate;
                invList.Add(invRef);
            }
            if (ReferrerName == null && ReferrerAddress == null && ReferrerMobile == null)
            {
                return View(invList);
            }
            else
            {
                return Json(invList, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveReferrer(VmInvestigationReferrar model)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                if (model.Id <= 0)
                {
                    InvestigationReferrer invRef = new InvestigationReferrer();
                    invRef.Name = model.Name;
                    invRef.Address = model.Address;
                    invRef.PhoneNumber = model.PhoneNumber;
                    invRef.EntryUser = User.UserName;
                    invRef.EntryDate = DateTime.Now;
                    invRef.OrgId = User.OrgId;
                    db.tblInvestigatinReferrer.Add(invRef);
                    IsSuccess = true;
                }
                else if (model.Id > 0)
                {
                    var invRefInDb = db.tblInvestigatinReferrer.Find(model.Id);
                    if (invRefInDb != null)
                    {
                        invRefInDb.Name = model.Name;
                        invRefInDb.Address = model.Address;
                        invRefInDb.PhoneNumber = model.PhoneNumber;
                        invRefInDb.UpdateBy = User.UserName;
                        invRefInDb.UpdateDate = DateTime.Now;
                        IsSuccess = true;
                    }
                }
                db.SaveChanges();
            }
            return Json(IsSuccess);
        }

        [HttpGet]
        public JsonResult GetReferrerById(int id)
        {
            var referrer = db.tblInvestigatinReferrer.Find(id);
            return Json(referrer, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBillInformation()
        {
            var billInfoData = (from bi in db.tblDiagnosticBillInfo
                                    //join bd in db.tblDiagnosticBillDetails on bi.Id equals bd.BillInfoId
                                join org in db.tblOrganizations on bi.OrgId equals org.OrgId
                                where org.OrgId == User.OrgId
                                select new { bi.Id, bi.InvoiceNo, bi.PatientId, bi.OrgId, OrgName = org.Name, bi.PatientName, bi.Address, bi.MobileNo, bi.Year, bi.Sex, bi.NetAmount, bi.Status, bi.ReceivedAmount, bi.DueAmount, bi.DiscountAmount, bi.DiscountPercent, bi.PaymentMode }).ToList();

            List<VmDiagnosisBillInfo> billInfos = new List<VmDiagnosisBillInfo>();
            foreach (var item in billInfoData)
            {
                VmDiagnosisBillInfo billInfo = new VmDiagnosisBillInfo();
                billInfo.Id = item.Id;
                billInfo.InvoiceNo = item.InvoiceNo;
                billInfo.PatientId = item.PatientId;
                billInfo.OrgId = item.OrgId;
                billInfo.OrgName = item.OrgName;
                billInfo.PatientName = item.PatientName;
                billInfo.Address = item.Address;
                billInfo.MobileNo = item.MobileNo;
                billInfo.Year = item.Year;
                billInfo.Sex = item.Sex;
                billInfo.DiscountAmount = item.DiscountAmount;
                billInfo.DiscountPercent = item.DiscountPercent;
                billInfo.ReceivedAmount = item.ReceivedAmount;
                billInfo.DueAmount = item.DueAmount;
                billInfo.NetAmount = item.NetAmount;
                billInfo.PaymentMode = item.PaymentMode;
                billInfos.Add(billInfo);
            }

            return View(billInfos);
        }

        [HttpGet]
        public ActionResult CreateDiagnosticBill()
        {
            return View();
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveDiagnosticBill(VmDiagnosticBill model)
        {
            bool isSuccess = false;
            string billNo = string.Empty;
            if (ModelState.IsValid)
            {
                DiagnosticBillInfo billInfo = new DiagnosticBillInfo();
                billInfo.PatientName = model.BillInfo.PatientName;
                billInfo.MobileNo = model.BillInfo.MobileNo;
                billInfo.Address = model.BillInfo.Address;
                billInfo.Sex = model.BillInfo.Sex;
                billInfo.Year = model.BillInfo.Year;
                billInfo.Months = model.BillInfo.Months;
                billInfo.Days = model.BillInfo.Days;
                billInfo.ReferrerId = model.BillInfo.ReferrerId;

                billInfo.TotalRefFee = model.BillDetails.Sum(s => s.RefFee);


                billInfo.PaymentMode = model.BillInfo.PaymentMode;
                billInfo.CashAmount = model.BillInfo.PaymentMode == "Cash" ? model.BillInfo.ReceivedAmount : 0;
                billInfo.CardAmount = model.BillInfo.PaymentMode == "Card" ? model.BillInfo.ReceivedAmount : 0;
                billInfo.BankId = 0;
                billInfo.DiscountAmount = model.BillInfo.DiscountAmount;
                billInfo.DiscountPercent = model.BillInfo.DiscountPercent;
                billInfo.DueAmount = model.BillInfo.DueAmount;

                billInfo.SubTotal = model.BillInfo.SubTotal;
                billInfo.NetAmount = model.BillInfo.NetAmount;
                billInfo.ReceivedAmount = model.BillInfo.ReceivedAmount;
                billInfo.Status = model.BillInfo.Status;
                billInfo.InvestigationQty = model.BillInfo.InvestigationQty;
                billInfo.OrgId = User.OrgId;


                billInfo.InvoiceNo = "INV-" + User.OrgId.ToString().PadLeft(3, '0') + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                billInfo.PatientId = "PNT-" + User.OrgId.ToString().PadLeft(3, '0') + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                billNo = billInfo.InvoiceNo;


                billInfo.EntryUser = User.UserName;
                var entryDate = DateTime.Now;
                billInfo.EntryDate = entryDate;
                 
                // Bill Invoice
                model.BillInfo.InvoiceNo = billInfo.InvoiceNo;

                db.tblDiagnosticBillInfo.Add(billInfo);

                //var discountAmountPertest = getDiscoumtAmountForPerItem(billInfo.DiscountAmount.Value, model.BillDetails.Count());

                foreach (var item in model.BillDetails)
                {
                    DiagnosticBillDetail billDetail = new DiagnosticBillDetail
                    {
                        InvoiceNo = billInfo.InvoiceNo,
                        InvestigationId = item.InvestigationId,
                        Fee = item.Fee,
                        RefFee=item.RefFee,
                        Discount = Calculate.InvestigationDiscountAmount(item.Fee.Value, billInfo.DiscountPercent.Value),
                        EntryUser = User.UserName,
                        EntryDate = entryDate,
                        DiscountPercent = item.Discount,
                        ReferrerId = model.BillInfo.ReferrerId
                    };
                    db.tblDiagnosticBillDetails.Add(billDetail);
                }

                db.SaveChanges();
                isSuccess = true;
            }

            if (isSuccess)
            {
                // model.BillInfo.InvoiceNo
                var reportData = GetInvoiceReport(model.BillInfo.InvoiceNo);

                LocalReport localReport = new LocalReport();
                string reportPath = Server.MapPath("~/Reports/rptDiagnosticBill.rdlc");


                if (System.IO.File.Exists(reportPath))
                {
                    localReport.ReportPath = reportPath;
                    ReportDataSource rptSource = new ReportDataSource("DiagnosticBill", reportData);
                    localReport.DataSources.Clear();
                    localReport.DataSources.Add(rptSource);
                    localReport.Refresh();
                    localReport.DisplayName = billNo;

                    // Print to printer
                    //localReport.PrintToPrinter();


                    string mimeType;
                    string encoding;
                    string fileNameExtension;

                    string deviceInfo =
                    "<DeviceInfo>" +
                    "<OutputFormat>PDF</OutputFormat>" +
                    "</DeviceInfo>";

                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;

                    renderedBytes = localReport.Render(
                        "PDF",
                        deviceInfo,
                        out mimeType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings);

                    var base64 = Convert.ToBase64String(renderedBytes);
                    var fs = String.Format("data:application/pdf;base64,{0}", base64);

                    return Json(new { isSuccess = isSuccess, file = fs, fileName = billNo });

                }
            }
            return Json(new { isSuccess = isSuccess, file = "" });
        }

        [HttpGet]
        public ActionResult GetDiagnosticDueList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetDueList(string invoiceNo = "", string mobileNo = "", string patientName = "")
        {
            //var viewData = db.tblDiagnosticBillInfo.GroupBy(bi => bi.Id)
            //    // PARTITION BY ^^^^
            //    .Select(bi => bi.OrderBy(o => o.Id).Select((v, i) => new { i, v })).ToList()
            //    //ORDER BY ^^
            //    .SelectMany(bi => bi).Select(bi => new vmDianosticBillDueAdjustment {Id= bi.v.Id, InvoiceNo= bi.v.InvoiceNo, InvestigationQty= bi.v.InvestigationQty, PatientName= bi.v.PatientName,  MobileNo=bi.v.MobileNo, NetAmount=bi.v.NetAmount,ReceiveAmount= bi.v.ReceivedAmount,  DueAmount=bi.v.DueAmount,EntryDate= bi.v.EntryDate, SLNo = bi.i + 1 }).Where(bi => bi.DueAmount > 0 && (invoiceNo == "" || invoiceNo == null || bi.InvoiceNo == invoiceNo) && (mobileNo == "" || mobileNo == null || bi.MobileNo == mobileNo) && (patientName == "" || patientName == null || bi.PatientName == patientName)).ToList();

            var viewData = db.tblDiagnosticBillInfo.Where(bi => bi.OrgId == User.OrgId && bi.DueAmount > 0 && (invoiceNo == "" || invoiceNo == null || bi.InvoiceNo.Contains(invoiceNo)) && (mobileNo == "" || mobileNo == null || bi.MobileNo.Contains(mobileNo)) && (patientName == "" || patientName == null || bi.PatientName.Contains(patientName))).Select(bi => new vmDianosticBillDueAdjustment { Id = bi.Id, InvoiceNo = bi.InvoiceNo, InvestigationQty = bi.InvestigationQty, PatientName = bi.PatientName, MobileNo = bi.MobileNo, NetAmount = bi.NetAmount, ReceiveAmount = bi.ReceivedAmount, DueAmount = bi.DueAmount, EntryDate = bi.EntryDate }).ToList();

            //List<vmDianosticBillDueAdjustment> viewDataList = new List<vmDianosticBillDueAdjustment>();
            //foreach (var item in Data)

            //{

            //}

            return PartialView("_GetDueList", viewData);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveBillDueAmount(int id, string invoiceNo, string mobileNo, decimal receiveAmount, decimal dueAmount)
        {
            bool isSuccess = false;
            if (id > 0 && !string.IsNullOrEmpty(invoiceNo.Trim()) && receiveAmount > 0 && dueAmount > 0 && (receiveAmount == dueAmount))
            {
                var billInfo = db.tblDiagnosticBillInfo.FirstOrDefault(bi => bi.Id == id && bi.InvoiceNo == invoiceNo && bi.DueAmount > 0 && bi.DueAmount == dueAmount);

                if (billInfo != null)
                {
                    billInfo.CashAmount = billInfo.CashAmount + receiveAmount;
                    billInfo.ReceivedAmount = billInfo.ReceivedAmount + receiveAmount;
                    billInfo.DueAmount = 0;
                    billInfo.Status = "Paid";
                    billInfo.UpdateBy = User.UserName;
                    billInfo.UpdateDate = DateTime.Now;
                    db.SaveChanges();
                    isSuccess = true;
                    if (isSuccess)
                    {
                        //billInfo.InvoiceNo
                        var reportData = GetInvoiceReport(billInfo.InvoiceNo);

                        LocalReport localReport = new LocalReport();
                        string reportPath = Server.MapPath("~/Reports/rptDiagnosticBill.rdlc");

                        string billNo = billInfo.InvoiceNo;
                        if (System.IO.File.Exists(reportPath))
                        {
                            localReport.ReportPath = reportPath;
                            ReportDataSource rptSource = new ReportDataSource("DiagnosticBill", reportData);
                            localReport.DataSources.Clear();
                            localReport.DataSources.Add(rptSource);
                            localReport.Refresh();
                            localReport.DisplayName = billNo;


                            string mimeType;
                            string encoding;
                            string fileNameExtension;

                            string deviceInfo =
                            "<DeviceInfo>" +
                            "<OutputFormat>PDF</OutputFormat>" +
                            "</DeviceInfo>";

                            Warning[] warnings;
                            string[] streams;
                            byte[] renderedBytes;

                            renderedBytes = localReport.Render(
                                "PDF",
                                deviceInfo,
                                out mimeType,
                                out encoding,
                                out fileNameExtension,
                                out streams,
                                out warnings);

                            var base64 = Convert.ToBase64String(renderedBytes);
                            var fs = String.Format("data:application/pdf;base64,{0}", base64);

                            return Json(new { isSuccess = isSuccess, file = fs, fileName = billNo });
                        }
                    }
                }

            }
            return Json(isSuccess);
        }

        public FileContentResult ReportFile(byte[] fileContents, string contentType)
        {
            return File(fileContents, contentType);
        }

        #region  Report-panel

        [HttpGet]
        public ActionResult ReportPanel()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetDateRageWiseBillInfo(string invoiceNo = "", string billType = "", string patientName = "", string fromDate = "", string toDate = "", string phoneNo = "", long refById = 0, int? page = 1)
        {
            DateTime dtimeFromDate = new DateTime();
            DateTime dtimeToDate = new DateTime();

            fromDate = !string.IsNullOrEmpty(fromDate.Trim()) ? (DateTime.TryParse(fromDate, out dtimeFromDate) ? dtimeFromDate.ToString("yyyy-MM-dd") : "") : "";
            toDate = !string.IsNullOrEmpty(toDate.Trim()) ? (DateTime.TryParse(toDate, out dtimeToDate) ? dtimeToDate.ToString("yyyy-MM-dd") : "") : "";


            var tblBillInfo = db.tblDiagnosticBillInfo.Where(bi => bi.OrgId == User.OrgId).ToList();
            //List<vmDianosticBillDueAdjustment>
            var reportData = (from bi in db.tblDiagnosticBillInfo
                              join refe in db.tblInvestigatinReferrer on bi.ReferrerId equals refe.Id
                              into refe
                              from biR in refe.DefaultIfEmpty() // Left Join
                              where (bi.OrgId == User.OrgId) && ((patientName.Trim() == "") || (patientName.Trim().ToLower().Contains(patientName))) && (invoiceNo.Trim() == "" || invoiceNo == null || bi.InvoiceNo.ToLower().Contains(invoiceNo.ToLower())) && (patientName.Trim() == "" || patientName == null || bi.PatientName.Trim().ToLower().Contains(patientName.Trim().ToLower()))
                              && (
                       (billType == "") || (billType.Trim().ToLower() == "due" && bi.DueAmount > 0) ||
                       (billType.Trim().ToLower() == "paid" && bi.DueAmount <= 0))
                       && (
                           (fromDate.Trim() == "" && toDate.Trim() == "") ||
                           (
                               (fromDate.Trim() != "" && toDate.Trim() != "") && DbFunctions.TruncateTime(bi.EntryDate) >= DbFunctions.TruncateTime(dtimeFromDate) && DbFunctions.TruncateTime(bi.EntryDate) <= dtimeToDate
                           ) ||
                           (fromDate.Trim() != "" && DbFunctions.TruncateTime(bi.EntryDate) == DbFunctions.TruncateTime(dtimeFromDate)) ||
                           (toDate.Trim() != "" && DbFunctions.TruncateTime(bi.EntryDate) == DbFunctions.TruncateTime(dtimeToDate))
                        ) &&
                        ((phoneNo.Trim() == "") || (phoneNo.Trim() != "" && bi.MobileNo.Contains(phoneNo.Trim())))
                        && ((refById == 0) || (bi.ReferrerId == refById))
                              select new vmDianosticBillDueAdjustment
                              {
                                  Id = bi.Id,
                                  InvoiceNo = bi.InvoiceNo,
                                  PatientName = bi.PatientName,
                                  InvestigationQty = bi.InvestigationQty,
                                  NetAmount = bi.NetAmount,
                                  TotalRefFee=bi.TotalRefFee,
                                  ReceiveAmount = bi.ReceivedAmount,
                                  DueAmount = bi.DueAmount,
                                  EntryDate = bi.EntryDate,
                                  Status = bi.Status,
                                  MobileNo = bi.MobileNo,
                                  RefId = bi.ReferrerId,
                                  RefName = biR.Name,
                                  SubTotal = bi.SubTotal,
                                  DiscountAmount = bi.DiscountAmount
                              }).ToList().ToPagedList(page ?? 1, 15);


            //List<vmDianosticBillDueAdjustment> reportData = db.tblDiagnosticBillInfo.Where(bi =>
            //bi.OrgId == User.OrgId &&
            //((patientName.Trim()=="") || (patientName.Trim().ToLower().Contains(patientName))) &&
            //(invoiceNo.Trim() == "" || invoiceNo == null || bi.InvoiceNo.ToLower().Contains(invoiceNo.ToLower()))
            //&& (patientName.Trim() == "" || patientName == null || bi.PatientName.Trim().ToLower().Contains(patientName.Trim().ToLower()))

            //&& (
            //    (billType == "") || (billType.Trim().ToLower() == "due" && bi.DueAmount > 0) || 
            //    (billType.Trim().ToLower() == "paid" && bi.DueAmount <= 0)
            //)
            //&& (
            //    (fromDate.Trim() == "" && toDate.Trim() == "") ||
            //    (
            //        (fromDate.Trim() != "" && toDate.Trim() != "") && DbFunctions.TruncateTime(bi.EntryDate) >= DbFunctions.TruncateTime(dtimeFromDate) && DbFunctions.TruncateTime(bi.EntryDate) <= dtimeToDate
            //    ) ||
            //    (fromDate.Trim() != "" && DbFunctions.TruncateTime(bi.EntryDate) == DbFunctions.TruncateTime(dtimeFromDate)) ||
            //    (toDate.Trim() != "" && DbFunctions.TruncateTime(bi.EntryDate) == DbFunctions.TruncateTime(dtimeToDate))
            // )
            //).Select(bi => new vmDianosticBillDueAdjustment
            //{
            //    Id = bi.Id,
            //    InvoiceNo = bi.InvoiceNo,
            //    PatientName = bi.PatientName,
            //    InvestigationQty=bi.InvestigationQty,
            //    NetAmount =bi.NetAmount,
            //    ReceiveAmount=bi.ReceivedAmount,
            //    DueAmount =bi.DueAmount,
            //    EntryDate =bi.EntryDate,
            //    Status = bi.Status,
            //    MobileNo=bi.MobileNo
            //}).ToList();

            return PartialView("_GetDateRageWiseBillInfo", reportData);
        }

        [HttpGet] // Json
        public ActionResult GetBillInvoiceDetails(string invoiceNo, Int64 billId)
        {
            var data = from bi in db.tblDiagnosticBillInfo
                       join bd in db.tblDiagnosticBillDetails on bi.InvoiceNo equals bd.InvoiceNo
                       join invs in db.tblInvestigations on bd.InvestigationId equals invs.Id
                       where bi.Id == billId && bi.OrgId == User.OrgId
                       select new { bi, bd, invs };

            VmDiagnosisBillInfo billInfo = new VmDiagnosisBillInfo();
            List<VmDiagnosticBillDetail> billDetailList = new List<VmDiagnosticBillDetail>();
            foreach (var item in data)
            {
                billInfo.InvoiceNo = item.bi.InvoiceNo;
                billInfo.PatientName = item.bi.PatientName;
                billInfo.MobileNo = item.bi.MobileNo;
                billInfo.Address = item.bi.Address;
                billInfo.Sex = item.bi.Sex;
                billInfo.PaymentMode = item.bi.PaymentMode;
                billInfo.SubTotal = item.bi.SubTotal;
                billInfo.DiscountAmount = item.bi.DiscountAmount;
                billInfo.NetAmount = item.bi.NetAmount;
                billInfo.ReceivedAmount = item.bi.ReceivedAmount;
                billInfo.DueAmount = item.bi.DueAmount;
                billInfo.EntryDate = item.bi.EntryDate;
                break;
            }
            //int count = 0;
            foreach (var item in data)
            {
                VmDiagnosticBillDetail billDetail = new VmDiagnosticBillDetail();
                //count = count = 1;
                billDetail.Id = item.bd.Id;
                billDetail.InvestigationName = item.invs.Name;
                billDetail.Fee = item.bd.Fee;
                billDetailList.Add(billDetail);
            }

            return Json(new { billInfo, billDetailList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMonthAndInvestigationWiseRpt(string date = "", int? investigationId = 0, int? page = 1)
        {
            DateTime dateTime = new DateTime();
            date = !string.IsNullOrEmpty(date.Trim()) ? (DateTime.TryParse(date, out dateTime) ? dateTime.ToString("yyyy-MM-dd") : "") : "";

            var investCount = (from bd in db.tblDiagnosticBillDetails
                               join invs in db.tblInvestigations on bd.InvestigationId equals invs.Id
                               where invs.OrgId == User.OrgId && (date.Trim() == "" || (date.Trim() != "" && dateTime.Month.ToString() == bd.EntryDate.Value.Month.ToString() && dateTime.Year.ToString() == bd.EntryDate.Value.Year.ToString())) && (investigationId == 0 || investigationId == null || invs.Id == investigationId)
                               group bd by new { month = bd.EntryDate.Value.Month, year = bd.EntryDate.Value.Year, invesId = invs.Id, invesName = invs.Name } into d
                               select new
                               { m = d.Key.month, y = d.Key.year, invId = d.Key.invesId, invName = d.Key.invesName, invqty = d.Count(), netInvAmt = (d.Select(amt => amt.Fee).Sum()) - (d.Select(dis => dis.Discount).Sum()) }).OrderBy(i => i.invName).ToList();

            List<vmMonthAndInvestigationReport> listReport = new List<vmMonthAndInvestigationReport>();

            foreach (var item in investCount)
            {
                vmMonthAndInvestigationReport report = new vmMonthAndInvestigationReport();
                report.InvestigationName = item.invName;
                report.MonthAndYear = DateFormater.GetMonthName(item.m) + " " + item.y;
                report.TotalQty = item.invqty;
                report.InvestigationId = item.invId;
                report.TotalAmount = item.netInvAmt.Value;
                listReport.Add(report);
            }
            var pagedList = listReport.ToPagedList(page ?? 1, 15);

            return PartialView("_GetMonthAndInvestigationWiseRpt", pagedList);
        }

        [HttpGet]
        public ActionResult GetReferrerWiseBillInfo(string fromDate = "", string toDate = "", long? refId = 0, int? page = 1)
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

            var refBillInfo = (from bi in db.tblDiagnosticBillInfo
                               join bd in db.tblDiagnosticBillDetails on bi.InvoiceNo equals bd.InvoiceNo
                               join invs in db.tblInvestigations on bd.InvestigationId equals invs.Id
                               join refe in db.tblInvestigatinReferrer on bi.ReferrerId equals refe.Id
                               //into biRef from biR in biRef.DefaultIfEmpty() // Left Join
                               where bi.OrgId == User.OrgId && (refId == null || refId == 0 || (bi.ReferrerId == refId))
                               &&
                               (
                                    (fromDate == "" && toDate == "") ||

                                    (((fromDate != "" && fromDate != null) && (toDate != "" && toDate != null)) && DbFunctions.TruncateTime(bi.EntryDate) >= DbFunctions.TruncateTime(dtimeFromDate) && DbFunctions.TruncateTime(bi.EntryDate) <= DbFunctions.TruncateTime(dtimeToDate)) ||

                                    ((fromDate != "" && fromDate != null) && DbFunctions.TruncateTime(bi.EntryDate) == DbFunctions.TruncateTime(dtimeFromDate)) ||

                                    ((toDate != "" && toDate != null) && DbFunctions.TruncateTime(bi.EntryDate) == DbFunctions.TruncateTime(dtimeToDate))
                               )
                               group refe by new { RefId = refe.Id, RefName = refe.Name, RefPhone = refe.PhoneNumber } into refData
                               select new vmReferrerWiseBillInfo
                               {
                                   RefId = refData.Key.RefId,
                                   RefName = refData.Key.RefName,
                                   RefPhone = refData.Key.RefPhone,
                                   TotalRefData = refData.Count(),
                                   TotalAmount = (db.tblDiagnosticBillInfo.Where(rbi => rbi.ReferrerId == refData.Key.RefId && (
(fromDate == "" && toDate == "") ||

(((fromDate != "" && fromDate != null) && (toDate != "" && toDate != null)) && DbFunctions.TruncateTime(rbi.EntryDate) >= DbFunctions.TruncateTime(dtimeFromDate) && DbFunctions.TruncateTime(rbi.EntryDate) <= DbFunctions.TruncateTime(dtimeToDate)) ||

((fromDate != "" && fromDate != null) && DbFunctions.TruncateTime(rbi.EntryDate) == DbFunctions.TruncateTime(dtimeFromDate)) ||

((toDate != "" && toDate != null) && DbFunctions.TruncateTime(rbi.EntryDate) == DbFunctions.TruncateTime(dtimeToDate))
)).Select(rbi => rbi.NetAmount).Sum()),

                                 TotalRefFeeAmount= (db.tblDiagnosticBillInfo.Where(rbi => rbi.ReferrerId == refData.Key.RefId && (
(fromDate == "" && toDate == "") ||

(((fromDate != "" && fromDate != null) && (toDate != "" && toDate != null)) && DbFunctions.TruncateTime(rbi.EntryDate) >= DbFunctions.TruncateTime(dtimeFromDate) && DbFunctions.TruncateTime(rbi.EntryDate) <= DbFunctions.TruncateTime(dtimeToDate)) ||

((fromDate != "" && fromDate != null) && DbFunctions.TruncateTime(rbi.EntryDate) == DbFunctions.TruncateTime(dtimeFromDate)) ||

((toDate != "" && toDate != null) && DbFunctions.TruncateTime(rbi.EntryDate) == DbFunctions.TruncateTime(dtimeToDate))
)).Select(rbi => rbi.TotalRefFee).Sum())


                               }).ToList().ToPagedList(page ?? 1, 10);

            return PartialView("_GetReferrerWiseBillInfo", refBillInfo);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult GetReferrerBillDetails(long refId, string fromDate = "", string toDate = "")
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

            var refBillInfo = (from bi in db.tblDiagnosticBillInfo
                               join bd in db.tblDiagnosticBillDetails on bi.InvoiceNo equals bd.InvoiceNo
                               join invs in db.tblInvestigations on bd.InvestigationId equals invs.Id
                               join refe in db.tblInvestigatinReferrer on bi.ReferrerId equals refe.Id
                               //into biRef from biR in biRef.DefaultIfEmpty() // Left Join
                               where bi.OrgId == User.OrgId && bi.ReferrerId == refId
                               &&
                               (
                                    (fromDate == "" && toDate == "") ||

                                    (((fromDate != "" && fromDate != null) && (toDate != "" && toDate != null)) && DbFunctions.TruncateTime(bi.EntryDate) >= DbFunctions.TruncateTime(dtimeFromDate) && DbFunctions.TruncateTime(bi.EntryDate) <= DbFunctions.TruncateTime(dtimeToDate)) ||

                                    ((fromDate != "" && fromDate != null) && DbFunctions.TruncateTime(bi.EntryDate) == DbFunctions.TruncateTime(dtimeFromDate)) ||

                                    ((toDate != "" && toDate != null) && DbFunctions.TruncateTime(bi.EntryDate) == DbFunctions.TruncateTime(dtimeToDate))
                               )
                               group bd by new { RefId = refe.Id, RefName = refe.Name, RefPhone = refe.PhoneNumber, investId = invs.Id, investName = invs.Name, DbInv = bd.InvestigationId } into refData
                               select new
                               {
                                   RefId = refData.Key.RefId,
                                   RefName = refData.Key.RefName,
                                   RefPhone = refData.Key.RefPhone,
                                   investId = refData.Key.investId,
                                   investName = refData.Key.investName,
                                   TotalRefData = refData.Count(),
                                   TotalAmount = ((refData.Where(r => r.InvestigationId == refData.Key.investId && ((fromDate == "" && toDate == "") ||

(((fromDate != "" && fromDate != null) && (toDate != "" && toDate != null)) && DbFunctions.TruncateTime(r.EntryDate) >= DbFunctions.TruncateTime(dtimeFromDate) && DbFunctions.TruncateTime(r.EntryDate) <= DbFunctions.TruncateTime(dtimeToDate)) ||

((fromDate != "" && fromDate != null) && DbFunctions.TruncateTime(r.EntryDate) == DbFunctions.TruncateTime(dtimeFromDate)) ||

((toDate != "" && toDate != null) && DbFunctions.TruncateTime(r.EntryDate) == DbFunctions.TruncateTime(dtimeToDate)))).Select(r => r.Fee).Sum()) - (refData.Where(r => r.InvestigationId == refData.Key.investId && ((fromDate == "" && toDate == "") ||

(((fromDate != "" && fromDate != null) && (toDate != "" && toDate != null)) && DbFunctions.TruncateTime(r.EntryDate) >= DbFunctions.TruncateTime(dtimeFromDate) && DbFunctions.TruncateTime(r.EntryDate) <= DbFunctions.TruncateTime(dtimeToDate)) ||

((fromDate != "" && fromDate != null) && DbFunctions.TruncateTime(r.EntryDate) == DbFunctions.TruncateTime(dtimeFromDate)) ||

((toDate != "" && toDate != null) && DbFunctions.TruncateTime(r.EntryDate) == DbFunctions.TruncateTime(dtimeToDate)))).Select(r => r.Discount).Sum()))
                               }).ToList();

            List<vmReferrerWiseBillInfo> vmRefBillList = new List<vmReferrerWiseBillInfo>();
            foreach (var item in refBillInfo)
            {
                vmReferrerWiseBillInfo refBill = new vmReferrerWiseBillInfo()
                {
                    RefId = item.RefId,
                    RefName = item.RefName,
                    RefPhone = item.RefPhone,
                    TotalRefData = item.TotalRefData,
                    InvestId = item.investId,
                    InvestName = item.investName,
                    TotalAmount = item.TotalAmount
                };
                vmRefBillList.Add(refBill);
            }
            return Json(vmRefBillList);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult DeleteInvoice(int Id, string InvoiceNo)
        {
            bool IsSuccess = false;
            if (Id > 0)
            {
                var invoiceItem = db.tblDiagnosticBillInfo.Find(Id);
                if (invoiceItem != null)
                {
                    ActionLog log = new ActionLog()
                    {
                        ActionName="Delete",
                        TableName= "tblDiagnosticBillInfo,tblDiagnosticBillDetails",
                        OrgId= User.OrgId,
                        PrimaryKeyId=invoiceItem.Id.ToString(),
                        SubKey =invoiceItem.InvoiceNo,
                        MacID=User.MacID,
                        UserId= User.UserId.ToString(),
                        UserName= User.UserName,
                        DataValues= "PatientName="+invoiceItem.PatientName+",PatientMobile="+invoiceItem.MobileNo+",RefId="+invoiceItem.ReferrerId.ToString()+",Sub Total="+invoiceItem.SubTotal.ToString()+",Receive Amount="+invoiceItem.ReceivedAmount+",Net Amount="+invoiceItem.NetAmount.ToString()+",Due Amount="+invoiceItem.DueAmount.ToString()+",Discount amount="+invoiceItem.DiscountAmount.ToString()+",Discount Percentage="+invoiceItem.DiscountPercent.ToString()+",Total Test="+invoiceItem.InvestigationQty.ToString(),
                        DeleteTime = DateTime.Now
                    };

                    db.tblDiagnosticBillInfo.Remove(invoiceItem);
                    
                    var invoiceDetails = db.tblDiagnosticBillDetails.Where(inv => inv.InvoiceNo == InvoiceNo).ToList();
                    foreach (var item in invoiceDetails)
                    {
                        db.tblDiagnosticBillDetails.Remove(item);
                    }
                    db.tblActionLog.Add(log);
                    db.SaveChanges();
                    IsSuccess = true;
                }
            }
            return Json(IsSuccess);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult GetInvoiceReport(int Id, string InvoiceNo)
        {
            if (Id > 0 && !string.IsNullOrEmpty(InvoiceNo.Trim()))
            {
                var reportData = GetInvoiceReport(InvoiceNo);

                LocalReport localReport = new LocalReport();
                string reportPath = Server.MapPath("~/Reports/rptDiagnosticBill.rdlc");

                string billNo = InvoiceNo;
                if (System.IO.File.Exists(reportPath))
                {
                    localReport.ReportPath = reportPath;
                    ReportDataSource rptSource = new ReportDataSource("DiagnosticBill", reportData);
                    localReport.DataSources.Clear();
                    localReport.DataSources.Add(rptSource);
                    localReport.Refresh();
                    localReport.DisplayName = billNo;
                    //localReport.PrintToPrinter();


                    string mimeType;
                    string encoding;
                    string fileNameExtension;

                    string deviceInfo =
                    "<DeviceInfo>" +
                    "<OutputFormat>PDF</OutputFormat>" +
                    "</DeviceInfo>";

                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;

                    renderedBytes = localReport.Render(
                        "PDF",
                        deviceInfo,
                        out mimeType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings);

                    var base64 = Convert.ToBase64String(renderedBytes);
                    var fs = String.Format("data:application/pdf;base64,{0}", base64);

                    return Json(new { isSuccess = true, file = fs, fileName = billNo });
                }
                return Json(new { isSuccess = false });
            }
            else
            {
                return Json(new { isSuccess = false });
            }

        }

        #endregion

        [HttpGet]
        public ActionResult GetInvestigationChart(string Flag = "", string Name = "")
        {
            if (string.IsNullOrEmpty(Flag.Trim()))
            {
                var investigationChart = db.tblInvestigationChart.OrderBy(chart => chart.InvestigationName).ToList();
                return View(investigationChart);
            }
            else
            {
                var investigationChart = db.tblInvestigationChart.Where(i => (Name == "") || i.InvestigationName.Trim().ToLower().Contains(Name.ToLower())).OrderBy(i => i.InvestigationName).ToList();
                return Json(investigationChart, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveInvestigationForChart(string[] items, int? id = 0, string name = "", string flag = "")
        {
            bool isSuccess = false;
            try
            {
                if (flag == "add")
                {
                    if (items.Length > 0)
                    {
                        foreach (var item in items)
                        {
                            // Checking into the database
                            var itemInDb = db.tblInvestigationChart.FirstOrDefault(i => i.InvestigationName == item);
                            if (itemInDb == null)
                            {
                                InvestigationChart chart = new InvestigationChart()
                                {
                                    InvestigationName = item,
                                    EntryUser = User.UserName,
                                    EntryDate = DateTime.Now
                                };
                                db.tblInvestigationChart.Add(chart);
                                db.SaveChanges();
                                isSuccess = true;
                            }
                        }
                    }
                }
                else if (flag == "edit")
                {
                    if (!string.IsNullOrEmpty(name.Trim()) && id > 0)
                    {
                        var investigation = db.tblInvestigationChart.FirstOrDefault(i => i.InvestigationName == name && i.Id != id);
                        if (investigation == null)
                        {
                            var investigationInDb = db.tblInvestigationChart.FirstOrDefault(i => i.Id == id);
                            if (investigationInDb != null)
                            {
                                investigationInDb.InvestigationName = name;
                                db.SaveChanges();
                                isSuccess = true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                // Write code here..
            }
            return Json(isSuccess);
        }

        #region Report Actions

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult GetListOfInvoiceReport(string invoiceNo, string patientName, string mobileNo, string status, int? refId, string fromDate, string toDate)
        {
            List<DateRageWiseBillInfo> reportData = new List<DateRageWiseBillInfo>();
            bool isSuccess = false;
            try
            {
                string paramData = ParamListOfInvoice(invoiceNo, patientName, mobileNo, status, refId, fromDate, toDate);
                reportData = ListOfInvoice(User.OrgId, Utility.ParamChecker(paramData));
                var logo = Utility.GetImageBytes(User.LogoPaths[0]);

                List<ReportHead> reportHead = new List<ReportHead>()
                {
                    new ReportHead(){OrganizationName= User.OrgName,Address =User.Address,MobileNo=User.MobileNo,Telephone = User.Telephone,Fax=User.Fax,OrgLogo=logo}
                };

                LocalReport localReport = new LocalReport();
                string reportPath = Server.MapPath("~/Reports/rptInvoiceInformation.rdlc");
                if (System.IO.File.Exists(reportPath))
                {
                    localReport.ReportPath = reportPath;
                    ReportDataSource dataSource = new ReportDataSource("InvoiceInformation", reportData);
                    localReport.DataSources.Clear();
                    localReport.DataSources.Add(dataSource);
                    ReportDataSource dataSource2 = new ReportDataSource("ReportHead", reportHead);
                    localReport.DataSources.Add(dataSource2);
                    localReport.Refresh();
                    localReport.DisplayName = "Invoice Information";

                    string deviceInfo =
                            "<DeviceInfo>" +
                            "<OutputFormat>PDF</OutputFormat>" +
                            "</DeviceInfo>";
                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;

                    renderedBytes = localReport.Render(
                        "PDF",
                        deviceInfo,
                        out mimeType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings);

                    var base64 = Convert.ToBase64String(renderedBytes);
                    var fs = String.Format("data:application/pdf;base64,{0}", base64);
                    isSuccess = true;
                    return Json(new { isSuccess = isSuccess, file = fs, fileName = "Invoice Information" });
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { isSuccess = isSuccess, file = "", fileName = "" });
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult GetMonthWiseInvestigationReport(string date = "", int? InvestigationId = 0)
        {
            bool isSuccess = false;
            try
            {
              List<MonthWiseInvestigationReport> reportData = MonthWiseInvestigationReport(User.OrgId, Utility.ParamChecker(ParamMonthWiseInvestigationReport(date, InvestigationId)));
                if (reportData != null)
                {
                    var logo = Utility.GetImageBytes(User.LogoPaths[0]);
                    List<ReportHead> reportHead = new List<ReportHead>()
                {
                    new ReportHead(){OrganizationName= User.OrgName,Address =User.Address,MobileNo=User.MobileNo,Telephone = User.Telephone,Fax=User.Fax,OrgLogo=logo}
                };

                    LocalReport localReport = new LocalReport();
                    string reportPath = Server.MapPath("~/Reports/rptMonthWiseInvestigation.rdlc");

                    if (System.IO.File.Exists(reportPath))
                    {
                        localReport.ReportPath = reportPath;
                        ReportDataSource dataSource1 = new ReportDataSource("MonthWiseInvestigation", reportData);
                        localReport.DataSources.Clear();
                        localReport.DataSources.Add(dataSource1);
                        ReportDataSource dataSource2 = new ReportDataSource("ReportHead", reportHead);
                        localReport.DataSources.Add(dataSource2);
                        localReport.Refresh();
                        localReport.DisplayName = "Month Wise Invesitgation Report";

                        string deviceInfo =
                            "<DeviceInfo>" +
                            "<OutputFormat>PDF</OutputFormat>" +
                            "</DeviceInfo>";
                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        Warning[] warnings;
                        string[] streams;
                        byte[] renderedBytes;

                        renderedBytes = localReport.Render(
                            "PDF",
                            deviceInfo,
                            out mimeType,
                            out encoding,
                            out fileNameExtension,
                            out streams,
                            out warnings);

                        var base64 = Convert.ToBase64String(renderedBytes);
                        var fs = String.Format("data:application/pdf;base64,{0}", base64);
                        isSuccess = true;
                        return Json(new { isSuccess = isSuccess, file = fs, fileName = "Month Wise Investigation Report", msg="Report has been loaded successfully." });
                    }
                    return Json(new { isSuccess = isSuccess, file = "", fileName = "", msg = "Report File not found." });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Json(new { isSuccess = isSuccess, file = "", fileName = "" ,msg="Data not found."});
        }

        #endregion

        #region Non-Action Methods

        #region Formula Actions
        [NonAction]
        private int getMonthNumber(DateTime startDate, DateTime endDate)
        {
            int monthNo = 0;
            startDate = startDate.Day == DateTime.DaysInMonth(startDate.Date.Year, startDate.Date.Month) ? startDate.AddDays(1) : startDate;
            endDate = endDate.Day == 1 ? startDate.AddDays(-1) : endDate;

            // 12 * (2019-2020)+ 9 - 1
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            monthNo = Math.Abs(monthsApart) == 0 ? 0 : Math.Abs(monthsApart) / 2;

            return monthNo;
        }

        [NonAction]
        public decimal getDiscoumtAmountForPerItem(decimal discountAmount, int totalTestQty)
        {
            decimal amountPerItem = 0;
            if (discountAmount > 0)
            {
                amountPerItem = (discountAmount / totalTestQty);
                amountPerItem = Math.Ceiling(amountPerItem);
            }
            return amountPerItem;
        }
        #endregion

        #region Data Actions
        [NonAction]
        private List<InvoiceBill> GetInvoiceReport(string invoiceNo)
        {
            byte[] orgLogo = Utility.GetImageBytes(User.LogoPaths[0]);
            byte[] reportLogo = Utility.GetImageBytes(User.LogoPaths[1]);

            //MemoryStream msOrg = new MemoryStream(orgLogo);
            //Bitmap bitOrg = (Bitmap)Image.FromStream(msOrg);

            //MemoryStream msReport = new MemoryStream(reportLogo);
            //Bitmap bitReport = (Bitmap)Image.FromStream(msReport);

            var data = (from billDetail in db.tblDiagnosticBillDetails
                        join bi in db.tblDiagnosticBillInfo
                        on billDetail.InvoiceNo equals bi.InvoiceNo
                        join org in db.tblOrganizations on bi.OrgId equals org.OrgId
                        join invs in db.tblInvestigations on billDetail.InvestigationId equals invs.Id
                        join refe in db.tblInvestigatinReferrer on bi.ReferrerId equals refe.Id
                        into refe
                        from biR in refe.DefaultIfEmpty() // Left Join
                        where billDetail.InvoiceNo == invoiceNo
                        select new InvoiceBill
                        {
                            InvoiceNo = bi.InvoiceNo,
                            OrgName = org.Name,
                            OrgId = org.OrgId,
                            BranchId = 0,
                            BranchName = "",
                            PatientId = 0,
                            PatientName = bi.PatientName,
                            Address = bi.Address,
                            MobileNo = bi.MobileNo,
                            Sex = bi.Sex,
                            Year = bi.Year,
                            Months = bi.Months,
                            Days = bi.Days,
                            ReferrerId = bi.ReferrerId,
                            ReferrerName = biR.Name,
                            PaymentMode = bi.PaymentMode,
                            BankId = bi.BankId,
                            BankName = "",
                            CashAmount = bi.CashAmount,
                            CardAmount = bi.CardAmount,
                            DiscountAmount = bi.DiscountAmount,
                            ReceivedAmount = bi.ReceivedAmount,
                            DueAmount = bi.DueAmount,
                            SubTotal = bi.SubTotal,
                            NetAmount = bi.NetAmount,
                            Status = bi.Status,
                            InvestigationQty = bi.InvestigationQty,
                            InvestigationId = billDetail.InvestigationId,
                            InvestigationName = invs.Name,
                            Fee = billDetail.Fee,
                            EntryDate = bi.EntryDate,
                            OrgLogo = orgLogo,
                            ReportLogo = reportLogo
                        }).ToList();

            return data;
        }

        [NonAction]
        private List<DateRageWiseBillInfo> ListOfInvoice(int? orgId, string param)
        {
            List<DateRageWiseBillInfo> reportDate = new List<DateRageWiseBillInfo>();
            try
            {
                using (DataContext context = new DataContext())
                {
                    string query = string.Format(@"Select ROW_NUMBER() OVER(Order By dbi.id) 'SLNo',dbi.Id,dbi.InvoiceNo,(select Count(*) from tblDiagnosticBillDetails 
where InvoiceNo = dbi.InvoiceNo) 'InvestigationQty',dbi.PatientName,dbi.MobileNo,dbi.NetAmount,dbi.ReceivedAmount,dbi.DueAmount,
ISNULL(ref.Id,0) 'RefId',ISNULL(ref.Name,'N/A') 'RefName',
dbi.SubTotal,dbi.DiscountPercent,dbi.DiscountAmount,
dbi.EntryDate
,dbi.[Status],0 'Amount'
from tblDiagnosticBillInfo dbi

Left Join tblInvestigatinReferrer ref on dbi.ReferrerId = ref.Id
where 1=1 And dbi.OrgId={0} {1}
", orgId, param);
                    context.Database.Connection.Open();
                    reportDate = context.Database.SqlQuery<DateRageWiseBillInfo>(query).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return reportDate;
        }

        private List<MonthWiseInvestigationReport> MonthWiseInvestigationReport(int? orgId, string param)
        {
            List<MonthWiseInvestigationReport> reportData = new List<MonthWiseInvestigationReport>();
            try
            {
                using (DataContext context = new DataContext())
                {
                    string query = string.Format(@"Select ((
	Case 
	when MONTH(dbd.EntryDate) = 1 then 'January' 
	when MONTH(dbd.EntryDate) = 2 then 'February' 
	when MONTH(dbd.EntryDate) = 3 then 'March' 
	when MONTH(dbd.EntryDate) = 4 then 'April' 
	when MONTH(dbd.EntryDate) = 5 then 'May' 
	when MONTH(dbd.EntryDate) = 6 then 'June' 
	when MONTH(dbd.EntryDate) = 7 then 'July' 
	when MONTH(dbd.EntryDate) = 8 then 'Augest' 
	when MONTH(dbd.EntryDate) = 9 then 'September' 
	when MONTH(dbd.EntryDate) = 10 then 'October' 
	when MONTH(dbd.EntryDate) = 11 then 'November' 
	when MONTH(dbd.EntryDate) = 12 then 'December' 

End) + ' ' + Convert(varchar(4),(YEAR(dbd.EntryDate)))) 'MonthAndYear',inv.Id 'InvestigationId', inv.Name 'InvestigationName',Count(dbd.InvestigationId) 'TestQty',(SUM(dbd.Fee)-SUM(Discount)) 'Amount' From tblDiagnosticBillDetails dbd
Inner Join tblInvestigations inv on dbd.InvestigationId = inv.Id
Where 1 = 1 and inv.OrgId={0} {1}
Group By MONTH(dbd.EntryDate), YEAR(dbd.EntryDate), inv.Id,inv.Name
Order By MONTH(dbd.EntryDate), YEAR(dbd.EntryDate),inv.Name
", orgId,param);
                    context.Database.Connection.Open();
                    reportData = context.Database.SqlQuery<MonthWiseInvestigationReport>(query).ToList();
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return reportData;
        }
        #endregion

        #region Params Method
        [NonAction]
        private string ParamListOfInvoice(string invoiceNo, string patientName, string mobileNo, string status, int? refId, string fromDate, string toDate)
        {
            string param = string.Empty;
            if (!string.IsNullOrEmpty(invoiceNo.Trim()))
            {
                param += string.Format(@" And dbi.InvoiceNo Like'{0}%' ", invoiceNo.Trim());
            }
            if (!string.IsNullOrEmpty(patientName.Trim()))
            {
                param += string.Format(@" And dbi.PatientName Like'{0}%' ", patientName.Trim());
            }
            if (!string.IsNullOrEmpty(mobileNo.Trim()))
            {
                param += string.Format(@" And dbi.MobileNo Like'{0}%' ", mobileNo.Trim());
            }
            if (!string.IsNullOrEmpty(status.Trim()))
            {
                param += string.Format(@" And dbi.[Status]='{0}'", status.Trim());
            }
            if (refId != null && refId > 0)
            {
                param += string.Format(@" And ref.Id={0}", refId);
            }

            // From Date and To Date Implementation //
            if (!string.IsNullOrEmpty(fromDate.Trim()) && !string.IsNullOrEmpty(toDate.Trim()))
            {
                param += string.Format(@" And Cast(dbi.EntryDate as Date) Between '{0}' And '{1}' ", fromDate.Trim(), toDate.Trim());
            }
            else if (!string.IsNullOrEmpty(fromDate.Trim()))
            {
                param += string.Format(@" And Cast(dbi.EntryDate as Date) ='{0}' ", fromDate.Trim());
            }
            else if (!string.IsNullOrEmpty(toDate.Trim()))
            {
                param += string.Format(@" And Cast(dbi.EntryDate as Date) ='{0}' ", toDate.Trim());
            }
            return param;
        }

        [NonAction]
        private string ParamMonthWiseInvestigationReport(string date = "", int? InvestigationId = 0)
        {
            string param = string.Empty;
            DateTime anydate = new DateTime();
            if (!string.IsNullOrEmpty(date.Trim()) && DateTime.TryParse(date, out anydate) == true)
            {
                param += string.Format(@" and MONTH(dbd.EntryDate)={0} and YEAR(dbd.EntryDate)={1} ", anydate.Month, anydate.Year);
            }
            if (InvestigationId != null && InvestigationId > 0)
            {
                param += string.Format(@" and inv.Id = {0} ", InvestigationId);
            }
            return param;
        }
        #endregion

        #region Specilist
        [HttpGet]
        public ActionResult GetSpecialist(string special)
        {
            var specialist = (from sp in db.tblSpecialists
                              join org in db.tblOrganizations
                                on sp.OrgId equals org.OrgId
                              where sp.OrgId == User.OrgId &&
                              (special == null || special.Trim() == "" || sp.Specialization.ToLower().Contains(special.ToLower().Trim()))
                              select new { sp.SpId, sp.Specialization, sp.Remarks, sp.EntryUser, sp.EntryDate, sp.UpdateBy, sp.UpdateDate, OrgName = org.Name }).ToList();

            List<VmSpecialist> splist = new List<VmSpecialist>();
            foreach(var item in specialist)
            {
                VmSpecialist sp = new VmSpecialist();
                sp.SpId = item.SpId;
                sp.Specialization = item.Specialization;
                sp.Remarks = item.Remarks;
                sp.EntryUser = item.EntryUser;
                sp.OrgName = item.OrgName;
                sp.EntryDate = item.EntryDate;
                splist.Add(sp);
            }
            if (special == null)
            {
                return View(splist);
            }
            else
            {
                return Json(splist, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveSpecialist(VmSpecialist model)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                if (model.SpId == 0)
                {
                    Specialist spe = new Specialist();
                    //inv.Name = model.Name;
                    spe.Specialization = model.Specialization;
                    spe.OrgId = User.OrgId;
                    spe.Remarks = model.Remarks;
                    spe.EntryUser = User.UserName;
                    spe.EntryDate = DateTime.Now;
                    db.tblSpecialists.Add(spe);
                    IsSuccess = true;
                }
                else
                {
                    var special = db.tblSpecialists.FirstOrDefault(sp => sp.SpId == model.SpId);
                    if (special != null)
                    {
                        //invInDb.Name = model.Name;
                        special.Specialization = model.Specialization;
                        special.Remarks = model.Remarks;
                        special.UpdateBy = User.UserName;
                        special.UpdateDate = DateTime.Now;
                        IsSuccess = true;
                    }
                }
                db.SaveChanges();
            }
            return Json(IsSuccess);
        }
        #endregion

        #region OtherProfession
        public ActionResult GetOtherProfessional(string flag)
        {
            if (string.IsNullOrEmpty(flag))
            {
                return View();
            }
            else
            {
                var othp = db.tblOtherProfessional.ToList();
                List<VmOtherProfessional> proflist = new List<VmOtherProfessional>();
                foreach(var item in othp)
                {
                    VmOtherProfessional op = new VmOtherProfessional();
                    op.OPId = item.OPId;
                    op.Profession = item.Profession;
                    op.Remarks = item.Remarks;
                    op.EntryDate = item.EntryDate;
                    op.EntryUser = item.EntryUser;
                    proflist.Add(op);
                }
                return PartialView("_GetOtherProfessional", proflist);
            }
        }
        public ActionResult SaveProfession(VmOtherProfessional model)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                if (model.OPId == 0)
                {
                    OtherProfessional other = new OtherProfessional
                    {
                        Profession = model.Profession,
                        Remarks = model.Remarks,
                        OrgId = User.OrgId,
                        EntryUser = User.UserName,
                        EntryDate = DateTime.Now,
                    };
                    db.tblOtherProfessional.Add(other);
                    IsSuccess = true;
                }
                else
                {
                    var otherp = db.tblOtherProfessional.Where(p => p.OPId == model.OPId).FirstOrDefault();
                    if(otherp != null)
                    {
                        otherp.Profession = model.Profession;
                        otherp.Remarks = model.Remarks;
                        otherp.UpdateBy = User.UserName;
                        otherp.UpdateDate = DateTime.Now;
                        IsSuccess = true;
                    }
                }
                db.SaveChanges();
            }
            return Json(IsSuccess);
        }
        #endregion

        #endregion
    }
}