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
    public class UserController : BaseController
    {
        // GET: User
        private DataContext db = new DataContext();
        public ActionResult Index()
        {
            ViewBag.TotalAmountThisMonth = db.tblDiagnosticBillInfo.Where(bi => bi.OrgId == User.OrgId && bi.EntryDate.Value.Month == DateTime.Now.Month && bi.EntryDate.Value.Year == DateTime.Now.Year).Sum(i => i.NetAmount).ToString();
            ViewBag.TotalInvestigationThisMonth = db.tblDiagnosticBillInfo.Where(bi => bi.OrgId == User.OrgId && bi.EntryDate.Value.Month == DateTime.Now.Month && bi.EntryDate.Value.Year == DateTime.Now.Year).Sum(i => i.InvestigationQty).ToString();

            ViewBag.TotalAmountThisYear = db.tblDiagnosticBillInfo.Where(bi => bi.OrgId == User.OrgId && bi.EntryDate.Value.Year == DateTime.Now.Year).Sum(i => i.NetAmount).ToString();
            ViewBag.TotalInvestigationThisYear = db.tblDiagnosticBillInfo.Where(bi => bi.OrgId == User.OrgId  && bi.EntryDate.Value.Year == DateTime.Now.Year).Sum(i => i.InvestigationQty).ToString();

            ViewBag.TotalInvestigationToday = db.tblDiagnosticBillInfo.Where(bi => bi.OrgId == User.OrgId && DbFunctions.TruncateTime(bi.EntryDate.Value) == DbFunctions.TruncateTime(DateTime.Today)).Sum(i => i.InvestigationQty).ToString();
            ViewBag.TotalAmountToday = db.tblDiagnosticBillInfo.Where(bi => bi.OrgId == User.OrgId && DbFunctions.TruncateTime(bi.EntryDate.Value) == DbFunctions.TruncateTime(DateTime.Today)).Sum(i => i.NetAmount).ToString();

            ViewBag.TotalDueAmountTillNow = db.tblDiagnosticBillInfo.Where(bi => bi.OrgId == User.OrgId).Sum(i => i.DueAmount).ToString();

            return View();
        }
    }
}