using HMSBO.Models;
using HMSBO.ViewModels;
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

        #region ProductStock
        public ActionResult GetProductStockList(string flag)
        {
            if (string.IsNullOrEmpty(flag))
            {
                return View();
            }
            else
            {
                var AllProduct = db.tblProductStockInfo.Where(i => i.OrgId == User.OrgId).ToList();
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
            var dropDown = db.tblProduct.Where(p => p.OrgId == User.OrgId && p.CategoryName == category).Select(p => new DropDown { text = p.ProductName.ToString(), value = p.ProductName.ToString() }).ToList();
            return Json(dropDown);
        }
        #endregion
    }
}