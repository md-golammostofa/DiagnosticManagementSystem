using HMSBO.Models;
using HMSBO.ReportModels;
using HMSBO.ViewModels;
using Microsoft.Reporting.WebForms;
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
    public class InventoryController : BaseController
    {
        private DataContext db = new DataContext();
        // GET: Inventory
        #region Category
        public ActionResult GetCategoryList(string flag)
        {
            if (string.IsNullOrEmpty(flag))
            {
                return View();
            }
            else
            {
                var catList = db.tblCategory.Where(c => c.OrgId == User.OrgId).ToList();
                List<VmCategory> allCat = new List<VmCategory>();
                foreach (var item in catList)
                {
                    VmCategory category = new VmCategory
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        Remarks = item.Remarks,
                        EntryDate = item.EntryDate,
                        EntryUser = item.EntryUser
                    };
                    allCat.Add(category);
                }
                return PartialView("_GetCategoryList", allCat);
            }

        }
        public ActionResult IsDuplicateCategoryName(string category)
        {
            bool IsSuccess = false;
            if (category != null)
            {
                IsSuccess = db.tblCategory.Where(p => p.OrgId == User.OrgId && p.CategoryName == category).FirstOrDefault() != null ? true : false;
            }
            return Json(IsSuccess);
        }
        public ActionResult SaveCategory(VmCategory model)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                if (model.CategoryId == 0)
                {
                    Category category = new Category
                    {
                        CategoryName = model.CategoryName,
                        Remarks = model.Remarks,
                        OrgId = User.OrgId,
                        EntryUser = User.UserName,
                        EntryDate = DateTime.Now
                    };
                    db.tblCategory.Add(category);
                    db.SaveChanges();
                    IsSuccess = true;
                }
            }
            return Json(IsSuccess);
        }
        #endregion

        #region Product
        public ActionResult GetProductList(string flag)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlCategory = db.tblCategory.Where(c => c.OrgId == User.OrgId).Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryName }).ToList();
                return View();
            }
            else
            {
                var allproduct = db.tblProduct.Where(p => p.OrgId == User.OrgId).ToList();
                List<VmProduct> productList = new List<VmProduct>();
                foreach(var item in allproduct)
                {
                    VmProduct product = new VmProduct
                    {
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        CategoryName = item.CategoryName,
                        BarCode = item.BarCode,
                        Remarks = item.Remarks,
                        EntryDate = item.EntryDate,
                        EntryUser = item.EntryUser
                    };
                    productList.Add(product);
                }
                return PartialView("_GetProductList", productList);
            }
        }
        public ActionResult IsDuplicateProductName(string product)
        {
            bool IsSuccess = false;
            if (product != null)
            {
                 IsSuccess = db.tblProduct.Where(p => p.OrgId == User.OrgId && p.ProductName == product).FirstOrDefault() != null ? true : false;
            }
            return Json(IsSuccess);
        }
        public ActionResult SaveProduct(VmProduct model)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                if (model.ProductId == 0)
                {
                    Product product = new Product
                    {
                        CategoryName=model.CategoryName,
                        ProductName=model.ProductName,
                        BarCode=model.BarCode,
                        Remarks=model.Remarks,
                        OrgId=User.OrgId,
                        EntryUser=User.UserName,
                        EntryDate=DateTime.Now,
                    };
                    db.tblProduct.Add(product);
                    db.SaveChanges();
                    IsSuccess = true;
                }
            }
            return Json(IsSuccess);
        }
        #endregion

        #region SalesCustomer
        public ActionResult CustomerList(string flag)
        {
            if (string.IsNullOrEmpty(flag))
            {
                return View();
            }
            else
            {
                var allCustomer = db.tblSalesCustomer.Where(c => c.OrgId == User.OrgId).ToList();
                List<VmSalesCustomer> customerList = new List<VmSalesCustomer>();
                foreach(var item in allCustomer)
                {
                    VmSalesCustomer customer = new VmSalesCustomer
                    {
                        CustomerId = item.CustomerId,
                        CustomerName = item.CustomerName,
                        CustomerAddress = item.CustomerAddress,
                        CustomerMobile = item.CustomerMobile,
                        CustomerEmail = item.CustomerEmail,
                        Remarks = item.Remarks,
                        EntryDate = item.EntryDate,
                        EntryUser = item.EntryUser,
                    };
                    customerList.Add(customer);
                }
                return PartialView("_CustomerList", customerList);
            }
        }
        public ActionResult SaveCustomer(VmSalesCustomer model)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                if (model.CustomerId == 0)
                {
                    SalesCustomer customer = new SalesCustomer
                    {
                        CustomerName = model.CustomerName,
                        CustomerMobile = model.CustomerMobile,
                        CustomerAddress = model.CustomerAddress,
                        CustomerEmail = model.CustomerEmail,
                        Remarks = model.Remarks,
                        OrgId = User.OrgId,
                        EntryUser = User.UserName,
                        EntryDate = DateTime.Now,
                    };
                    db.tblSalesCustomer.Add(customer);
                    db.SaveChanges();
                    IsSuccess = true;
                }
            }
            return Json(IsSuccess);
        }
        public ActionResult GetCustomer(long customerId)
        {
            VmSalesCustomer salesCustomer = new VmSalesCustomer();
            var customer = db.tblSalesCustomer.Where(c => c.OrgId == User.OrgId && c.CustomerId == customerId).FirstOrDefault();
            if (customer !=null)
            {
                salesCustomer.CustomerMobile = customer.CustomerMobile;
                salesCustomer.CustomerAddress = customer.CustomerAddress;
            }
            return Json(salesCustomer);
        }
        #endregion

        #region ProductStock
        public ActionResult GetProductStockList(string flag,string category,string product)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlCategory = db.tblCategory.Where(c => c.OrgId == User.OrgId).Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryName }).ToList();

                ViewBag.ddlProductName = db.tblProduct.Where(c => c.OrgId == User.OrgId).Select(c => new SelectListItem { Text = c.ProductName, Value = c.ProductName }).ToList();
                return View();
            }
            else
            {
                var AllProduct = db.tblProductStockInfo.Where(i => i.OrgId == User.OrgId && (category == "" || category == null || i.Category.Contains(category)) && (product == "" || product == null || i.ProductName.Contains(product))).ToList();
                List<VmProductStockInfo> productList = new List<VmProductStockInfo>();
                foreach(var item in AllProduct)
                {
                    VmProductStockInfo info = new VmProductStockInfo
                    {
                        PSInfoId=item.PSInfoId,
                        Category=item.Category,
                        ProductName=item.ProductName,
                        StockInQty=item.StockInQty,
                        StockOutQty=item.StockOutQty,
                        StockQty=(item.StockInQty-item.StockOutQty),
                        CostPrice=item.CostPrice,
                        SellPrice=item.SellPrice,
                        Remarks=item.Remarks,
                        EntryUser=item.EntryUser,
                        EntryDate=item.EntryDate,
                    };
                    productList.Add(info);
                }
                return PartialView("_GetProductStockList", productList);
            }
        }
        public ActionResult CreateProductStock()
        {
            ViewBag.ddlCategory = db.tblCategory.Where(c => c.OrgId == User.OrgId).Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryName }).ToList();
            return View();
        }
        public ActionResult GetProductName(string category)
        {
            var dropDown = db.tblProduct.Where(p => p.OrgId == User.OrgId && p.CategoryName == category).Select(p => new DropDown { text = p.ProductName, value = p.ProductName.ToString() }).ToList();
            return Json(dropDown);
        }
        public ActionResult SaveProductStockIn(List<VmProductStockDetails> details)
        {
            bool IsSuccess = true;
            if(ModelState.IsValid && details.Count() > 0)
            {
                foreach(var item in details)
                {
                    var infoInStock = db.tblProductStockInfo.Where(i => i.OrgId == User.OrgId && i.Category == item.Category && i.ProductName == item.ProductName && i.CostPrice == item.CostPrice).FirstOrDefault();
                    if(infoInStock != null)
                    {
                        infoInStock.StockInQty += item.Quantity;
                        infoInStock.UpdateDate = DateTime.Now;
                        infoInStock.UpdateUser = User.UserName;
                        db.SaveChanges();
                    }
                    else
                    {
                        ProductStockInfo info = new ProductStockInfo
                        {
                            Category = item.Category,
                            ProductName = item.ProductName,
                            CostPrice = item.CostPrice,
                            SellPrice = item.SellPrice,
                            StockInQty = item.Quantity,
                            StockOutQty = 0,
                            OrgId=User.OrgId,
                            EntryDate=DateTime.Now,
                            EntryUser=User.UserName,
                        };
                        db.tblProductStockInfo.Add(info);
                        db.SaveChanges();
                    }

                    ProductStockDetails detail = new ProductStockDetails
                    {
                        Category = item.Category,
                        ProductName = item.ProductName,
                        CostPrice = item.CostPrice,
                        SellPrice = item.SellPrice,
                        Quantity = item.Quantity,
                        StockStatus = "Stock-In",
                        Remarks = item.Remarks,
                        OrgId=User.OrgId,
                        EntryUser=User.UserName,
                        EntryDate=DateTime.Now
                    };
                    db.tblProductStockDetails.Add(detail);
                    db.SaveChanges();
                    IsSuccess = true;
                }
            }
            return Json(IsSuccess);
        }
        public ActionResult GetProductStockDetailsList(string flag, string category, string product)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlCategory = db.tblCategory.Where(c => c.OrgId == User.OrgId).Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryName }).ToList();

                ViewBag.ddlProductName = db.tblProduct.Where(c => c.OrgId == User.OrgId).Select(c => new SelectListItem { Text = c.ProductName, Value = c.ProductName }).ToList();
                return View();
            }
            else
            {
                var allDetail = db.tblProductStockDetails.Where(d => d.OrgId == User.OrgId && (category == "" || category == null || d.Category.Contains(category)) && (product == "" || product == null || d.ProductName.Contains(product))).OrderByDescending(d=>d.EntryDate).ToList();
                List<VmProductStockDetails> detailList = new List<VmProductStockDetails>();
                foreach(var item in allDetail)
                {
                    VmProductStockDetails details = new VmProductStockDetails
                    {
                        PSDetailsId = item.PSDetailsId,
                        Category = item.Category,
                        ProductName = item.ProductName,
                        CostPrice = item.CostPrice,
                        SellPrice = item.SellPrice,
                        Quantity = item.Quantity,
                        StockStatus = item.StockStatus,
                        Remarks=item.Remarks,
                        EntryUser = item.EntryUser,
                        EntryDate = item.EntryDate,
                    };
                    detailList.Add(details);
                }
                return PartialView("_GetProductStockDetailsList", detailList);
            }
        }
        public bool StockOutBySales(List<VmProductStockDetails> details)
        {
            bool IsSuccess = true;
            if (ModelState.IsValid && details.Count() > 0)
            {
                foreach (var item in details)
                {
                    var infoInStock = db.tblProductStockInfo.Where(i => i.OrgId == User.OrgId && i.Category == item.Category && i.ProductName == item.ProductName && i.SellPrice == item.SellPrice && (i.StockInQty-i.StockOutQty) > 0).FirstOrDefault();
                    if (infoInStock != null)
                    {
                        infoInStock.StockOutQty += item.Quantity;
                        infoInStock.UpdateDate = DateTime.Now;
                        infoInStock.UpdateUser = User.UserName;
                        db.SaveChanges();
                    }
                    ProductStockDetails detail = new ProductStockDetails
                    {
                        Category = item.Category,
                        ProductName = item.ProductName,
                        CostPrice = infoInStock.CostPrice,
                        SellPrice = item.SellPrice,
                        Quantity = item.Quantity,
                        StockStatus = "Stock-Out",
                        Remarks = item.Remarks,
                        OrgId = User.OrgId,
                        EntryUser = User.UserName,
                        EntryDate = DateTime.Now
                    };
                    db.tblProductStockDetails.Add(detail);
                    db.SaveChanges();
                    IsSuccess = true;
                }
            }
            return IsSuccess;
        }
        #endregion

        #region Invoice List
        public ActionResult GetInvoiceInfoList(string flag,string invoice)
        {
            if (string.IsNullOrEmpty(flag))
            {
                return View();
            }
            else
            {
                var allInvoice = db.tblSalesInvoiceInfo.Where(i => i.OrgId == User.OrgId && (invoice == "" || invoice == null || i.InvoiceNo.Contains(invoice))).ToList();
                List<VmSalesInvoiceInfo> infoList = new List<VmSalesInvoiceInfo>();
                foreach(var item in allInvoice)
                {
                    VmSalesInvoiceInfo info = new VmSalesInvoiceInfo
                    {
                        IInfoId=item.IInfoId,
                        CustomerName=item.CustomerName,
                        CustomerMobile=item.CustomerMobile,
                        CustomerAddress=item.CustomerAddress,
                        InvoiceNo=item.InvoiceNo,
                        CustomerType=item.CustomerType,
                        PaymentStatus=item.PaymentStatus,
                        PaymentMethod=item.PaymentMethod,
                        SubTotalAmount=item.SubTotalAmount,
                        VATAmount=item.VATAmount,
                        TAXAmount=item.TAXAmount,
                        DisCountAmount=item.DisCountAmount,
                        NetAmount=item.NetAmount,
                        DueAmount=item.DueAmount,
                        ReceiveAmount=item.ReceiveAmount,
                        EntryDate=item.EntryDate,
                        EntryUser=item.EntryUser,
                        
                    };
                    infoList.Add(info);
                }
                return PartialView("_GetInvoiceInfoList", infoList);
            }
        }
        public ActionResult GetInvoiceDetailsList(long infoId)
        {
            var allDetails = db.tblSalesInvoiceDetails.Where(d => d.OrgId == User.OrgId && d.IInfoId == infoId).ToList();
            List<VmSalesInvoiceDetails> detailList = new List<VmSalesInvoiceDetails>();
            foreach(var item in allDetails)
            {
                VmSalesInvoiceDetails details = new VmSalesInvoiceDetails
                {
                    IDetailsId = item.IDetailsId,
                    IInfoId = item.IInfoId,
                    CategoryName = item.CategoryName,
                    ProductName = item.ProductName,
                    SellPrice = item.SellPrice,
                    Quantity = item.Quantity,
                    Total = item.Total,
                };
                detailList.Add(details);
            }
            return PartialView("_GetInvoiceDetailsList", detailList);
        }
        public ActionResult CreateInvoice()
        {
            ViewBag.ddlCategory = db.tblCategory.Where(c => c.OrgId == User.OrgId).Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryName }).ToList();

            ViewBag.ddlProductName = db.tblProduct.Where(c => c.OrgId == User.OrgId).Select(c => new SelectListItem { Text = c.ProductName, Value = c.ProductName }).ToList();
            ViewBag.ddlDealerName = db.tblSalesCustomer.Where(c => c.OrgId == User.OrgId).Select(c => new SelectListItem { Text = c.CustomerName, Value = c.CustomerId.ToString() }).ToList();
            return View();
        }
        public ActionResult GetProductNameAndSellPrice(string category)
        {
            var dropDown = db.tblProductStockInfo.Where(p => p.OrgId == User.OrgId && p.Category == category).Select(p => new DropDown { text = p.ProductName+"-("+p.SellPrice+" TK)", value = p.PSInfoId.ToString() }).ToList();
            return Json(dropDown);
        }
        public ActionResult GetSellPriceByProductName(long productId)
        {
            VmProductStockInfo stockInfo = new VmProductStockInfo();
            if (productId > 0)
            {
                var qty = db.tblProductStockInfo.Where(s => s.OrgId == User.OrgId && s.PSInfoId == productId).FirstOrDefault();
                if (qty != null)
                {
                    stockInfo.StockQty = (qty.StockInQty - qty.StockOutQty);
                    stockInfo.SellPrice = qty.SellPrice;
                    stockInfo.ProductName = qty.ProductName;
                }
            }
            return Json(stockInfo);
        }
        public ActionResult SaveSalesInvoice(VmSalesInvoiceInfo info)
        {
            bool IsSuccess = false;
            var status = string.Empty;
            var customerName = string.Empty;
            double dueAmount = 0;
            var invoice = "INV-" + User.OrgId.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            if (ModelState.IsValid)
            {
                
                if (info.ReceiveAmount > info.NetAmount || info.ReceiveAmount==info.NetAmount)
                {
                    status = "Paid";
                }
                else
                {
                    status = "Due";
                    dueAmount = info.NetAmount - info.ReceiveAmount;
                }
                if (info.CustomerType == "Dealer")
                {
                    var cust = db.tblSalesCustomer.Where(c => c.OrgId == User.OrgId && c.CustomerId == info.DealerName).FirstOrDefault();
                    customerName = cust.CustomerName;
                }
                else
                {
                    customerName = info.CustomerName;
                }
                if (info.salesInvoiceDetals.Count() > 0)
                {
                    SalesInvoiceInfo infos = new SalesInvoiceInfo();
                    infos.InvoiceNo = invoice;
                    infos.CustomerType = info.CustomerType;
                    infos.CustomerName = customerName;
                    infos.CustomerMobile = info.CustomerMobile;
                    infos.CustomerAddress = info.CustomerAddress;
                    infos.PaymentMethod = info.PaymentMethod;
                    infos.PaymentStatus = status;
                    infos.SubTotalAmount = info.SubTotalAmount;
                    infos.VATAmount = info.VATAmount;
                    infos.TAXAmount = info.TAXAmount;
                    infos.DisCountAmount = info.DisCountAmount;
                    infos.NetAmount = info.NetAmount;
                    infos.DueAmount = dueAmount;
                    infos.ReceiveAmount = info.ReceiveAmount;
                    infos.OrgId = User.OrgId;
                    infos.EntryUser = User.UserName;
                    infos.EntryDate = DateTime.Now;

                    List<VmProductStockDetails> vmStockOutlist = new List<VmProductStockDetails>();
                    List<SalesInvoiceDetails> detailsList = new List<SalesInvoiceDetails>();
                    foreach(var item in info.salesInvoiceDetals)
                    {
                        SalesInvoiceDetails details = new SalesInvoiceDetails
                        {
                            CategoryName=item.CategoryName,
                            ProductName=item.ProductName,
                            SellPrice=item.SellPrice,
                            Quantity=item.Quantity,
                            Total=item.Total,
                            OrgId=User.OrgId,
                            EntryUser=User.UserName,
                            EntryDate=DateTime.Now,
                        };
                        detailsList.Add(details);
                        VmProductStockDetails detail = new VmProductStockDetails
                        {
                            Category = item.CategoryName,
                            ProductName = item.ProductName,
                            SellPrice = item.SellPrice,
                            Quantity = item.Quantity,
                            StockStatus = "Stock-Out",
                            Remarks = item.Remarks,
                            OrgId = User.OrgId,
                            EntryUser = User.UserName,
                            EntryDate = DateTime.Now
                        };
                        vmStockOutlist.Add(detail);
                    }
                    
                    infos.salesInvoiceDetals = detailsList;
                    db.tblSalesInvoiceInfo.Add(infos);
                    db.SaveChanges();
                    IsSuccess = StockOutBySales(vmStockOutlist);
                }
            }
            if (IsSuccess)
            {
                var invoiceli = db.tblSalesInvoiceInfo.Where(i => i.OrgId == User.OrgId && i.InvoiceNo == invoice).ToList();
                var invoiceId = db.tblSalesInvoiceInfo.Where(i => i.OrgId == User.OrgId && i.InvoiceNo == invoice).FirstOrDefault();
                var invoiceDetails = db.tblSalesInvoiceDetails.Where(d => d.OrgId == User.OrgId && d.IInfoId == invoiceId.IInfoId).ToList();

                List<CommonReport> commons = new List<CommonReport>();
                CommonReport com = new CommonReport();
                com.OrgName = User.OrgName;
                com.Address = User.Address;
                commons.Add(com);

                LocalReport localReport = new LocalReport();
                string reportPath = Server.MapPath("~/Reports/rptSalesInvoiceReceipt.rdlc");
                if (System.IO.File.Exists(reportPath))
                {
                    localReport.ReportPath = reportPath;
                    ReportDataSource dataSource1 = new ReportDataSource("InvoiceInfo", invoiceli);
                    ReportDataSource dataSource2 = new ReportDataSource("InvoiceDetails", invoiceDetails);
                    ReportDataSource dataSource3 = new ReportDataSource("Common", commons);
                    localReport.DataSources.Clear();
                    localReport.DataSources.Add(dataSource1);
                    localReport.DataSources.Add(dataSource2);
                    localReport.DataSources.Add(dataSource3);
                    localReport.Refresh();
                    localReport.DisplayName = "Receipt";

                    string mimeType;
                    string encoding;
                    string fileNameExtension = ".pdf";
                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;

                    renderedBytes = localReport.Render(
                        "Pdf",
                        "",
                        out mimeType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings);
                    var base64 = Convert.ToBase64String(renderedBytes);
                    var fs = String.Format("data:application/pdf;base64,{0}", base64);

                    return Json(new { IsSuccess = IsSuccess, file = fs, fileName = invoice });
                }
            }
            return Json(IsSuccess);
        }
        public ActionResult GetInvoiceDueAdjustment(string flag,string invoice,string customerName,string mobile)
        {
            if (string.IsNullOrEmpty(flag))
            {
                return View();
            }
            else
            {
                var allInvoice = db.tblSalesInvoiceInfo.Where(i => i.OrgId == User.OrgId && i.PaymentStatus=="Due" && (invoice == "" || invoice == null || i.InvoiceNo.Contains(invoice)) && (customerName == "" || customerName == null || i.CustomerName.Contains(customerName)) && (mobile == "" || mobile == null || i.CustomerMobile.Contains(mobile))).ToList();
                List<VmSalesInvoiceInfo> infoList = new List<VmSalesInvoiceInfo>();
                foreach (var item in allInvoice)
                {
                    VmSalesInvoiceInfo info = new VmSalesInvoiceInfo
                    {
                        IInfoId = item.IInfoId,
                        CustomerName = item.CustomerName,
                        CustomerMobile = item.CustomerMobile,
                        CustomerAddress = item.CustomerAddress,
                        InvoiceNo = item.InvoiceNo,
                        CustomerType = item.CustomerType,
                        PaymentStatus = item.PaymentStatus,
                        PaymentMethod = item.PaymentMethod,
                        SubTotalAmount = item.SubTotalAmount,
                        VATAmount = item.VATAmount,
                        TAXAmount = item.TAXAmount,
                        DisCountAmount = item.DisCountAmount,
                        NetAmount = item.NetAmount,
                        DueAmount = item.DueAmount,
                        ReceiveAmount = item.ReceiveAmount,
                        EntryDate = item.EntryDate,
                        EntryUser = item.EntryUser,

                    };
                    infoList.Add(info);
                }
                return PartialView("_GetInvoiceDueAdjustment", infoList);
            }
        }
        public ActionResult UpdateInvoiceDueAdjust(long invoiceId,double preRecAmt,double dueAmt,double newRecAmt)
        {
            bool IsSuccess = false;
            var invoice = db.tblSalesInvoiceInfo.Where(i => i.OrgId == User.OrgId && i.IInfoId == invoiceId).FirstOrDefault();
            if (invoiceId > 0)
            {
                if(invoice != null)
                {
                    invoice.ReceiveAmount = invoice.ReceiveAmount + dueAmt;
                    invoice.DueAmount = 0;
                    invoice.PaymentStatus = "Paid";
                    invoice.UpdateUser = User.UserName;
                    invoice.UpdateDate = DateTime.Now;
                }
                db.SaveChanges();
                IsSuccess = true;
            }
            if (IsSuccess)
            {
                var invoiceli = db.tblSalesInvoiceInfo.Where(i => i.OrgId == User.OrgId && i.InvoiceNo == invoice.InvoiceNo).ToList();
                var invoiceDetails = db.tblSalesInvoiceDetails.Where(d => d.OrgId == User.OrgId && d.IInfoId == invoice.IInfoId).ToList();

                List<CommonReport> commons = new List<CommonReport>();
                CommonReport com = new CommonReport();
                com.OrgName = User.OrgName;
                com.Address = User.Address;
                commons.Add(com);

                LocalReport localReport = new LocalReport();
                string reportPath = Server.MapPath("~/Reports/rptSalesInvoiceReceipt.rdlc");
                if (System.IO.File.Exists(reportPath))
                {
                    localReport.ReportPath = reportPath;
                    ReportDataSource dataSource1 = new ReportDataSource("InvoiceInfo", invoiceli);
                    ReportDataSource dataSource2 = new ReportDataSource("InvoiceDetails", invoiceDetails);
                    ReportDataSource dataSource3 = new ReportDataSource("Common", commons);
                    localReport.DataSources.Clear();
                    localReport.DataSources.Add(dataSource1);
                    localReport.DataSources.Add(dataSource2);
                    localReport.DataSources.Add(dataSource3);
                    localReport.Refresh();
                    localReport.DisplayName = "Receipt";

                    string mimeType;
                    string encoding;
                    string fileNameExtension = ".pdf";
                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;

                    renderedBytes = localReport.Render(
                        "Pdf",
                        "",
                        out mimeType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings);
                    var base64 = Convert.ToBase64String(renderedBytes);
                    var fs = String.Format("data:application/pdf;base64,{0}", base64);

                    return Json(new { IsSuccess = IsSuccess, file = fs, fileName = invoice.InvoiceNo });
                }
            }
            return Json(IsSuccess);
        }
        public ActionResult GetSalesInvoiceReport(string invoice,string rptType)
        {
            var allInvoice = db.tblSalesInvoiceInfo.Where(i => i.OrgId == User.OrgId && (invoice == "" || invoice == null || i.InvoiceNo.Contains(invoice))).ToList();
            List<VmSalesInvoiceInfo> infoList = new List<VmSalesInvoiceInfo>();
            foreach (var item in allInvoice)
            {
                VmSalesInvoiceInfo info = new VmSalesInvoiceInfo
                {
                    IInfoId = item.IInfoId,
                    CustomerName = item.CustomerName,
                    CustomerMobile = item.CustomerMobile,
                    CustomerAddress = item.CustomerAddress,
                    InvoiceNo = item.InvoiceNo,
                    CustomerType = item.CustomerType,
                    PaymentStatus = item.PaymentStatus,
                    PaymentMethod = item.PaymentMethod,
                    SubTotalAmount = item.SubTotalAmount,
                    VATAmount = item.VATAmount,
                    TAXAmount = item.TAXAmount,
                    DisCountAmount = item.DisCountAmount,
                    NetAmount = item.NetAmount,
                    DueAmount = item.DueAmount,
                    ReceiveAmount = item.ReceiveAmount,
                    EntryDate = item.EntryDate,
                    EntryUser = item.EntryUser,

                };
                infoList.Add(info);
            }
            List<CommonReport> commons = new List<CommonReport>();
            CommonReport com = new CommonReport();
            com.OrgName = User.OrgName;
            com.Address = User.Address;
            commons.Add(com);

            LocalReport localReport = new LocalReport();
            string reportPath = Server.MapPath("~/Reports/rptInvoicelistReport.rdlc");
            if (System.IO.File.Exists(reportPath))
            {
                localReport.ReportPath = reportPath;
                ReportDataSource dataSource1 = new ReportDataSource("InvoiceInfo", infoList);
                ReportDataSource dataSource2 = new ReportDataSource("Common", commons);
                localReport.DataSources.Clear();
                localReport.DataSources.Add(dataSource1);
                localReport.DataSources.Add(dataSource2);
                localReport.Refresh();
                localReport.DisplayName = "Invoice";

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
        #endregion
    }
}