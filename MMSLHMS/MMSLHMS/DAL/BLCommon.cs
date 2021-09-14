using HMSBO.Models;
using HMSBO.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMSLHMS.DAL
{
    public class BLCommon
    {

        public static List<SelectListItem> GetDepartmentsByOrgForDDL(int OrgId)
        {
            DataContext db = new DataContext();
            List<Department> departments =db.tblDepartment.Where(d => d.OrgId == OrgId).ToList();
            List<SelectListItem> ddlDepartment = new List<SelectListItem>();
            foreach (var item in departments)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = item.DepartmentName;
                selectListItem.Value = item.DepartmentId.ToString();
                ddlDepartment.Add(selectListItem);
            }

            return ddlDepartment;
        }

        public static List<SelectListItem> GetDesignationByOrgId(int OrgId)
        {
            DataContext db = new DataContext();
            List<Designation> designations = db.tblDesignation.Where(d => d.OrgId == OrgId).ToList();
            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            foreach (var item in designations)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = item.Name;
                selectListItem.Value = item.DesigId.ToString();
                ddlDesignation.Add(selectListItem);
            }

            return ddlDesignation;
        }

        public static List<SelectListItem> GetSpecializationTypeByOrgId(int OrgId)
        {
            DataContext db = new DataContext();
            List<SpecializationType> specializationType = db.tblSpecializationType.Where(d => d.OrgId == OrgId).ToList();
            List<SelectListItem> ddlSpecializationType = new List<SelectListItem>();
            foreach (var item in specializationType)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = item.TypeName;
                selectListItem.Value = item.Id.ToString();
                ddlSpecializationType.Add(selectListItem);
            }

            return ddlSpecializationType;
        }

        public static List<SelectListItem> GetBranchByOrgId(int OrgId)
        {
            DataContext db = new DataContext();
            List<Branch> branches = db.tblBranchInfo.Where(b => b.OrgId == OrgId).ToList();
            List<SelectListItem> ddlBranches = new List<SelectListItem>();
            foreach (var item in branches)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = item.BranchName;
                selectListItem.Value = item.BranchId.ToString();
                ddlBranches.Add(selectListItem);
            }

            return ddlBranches;
        }
    }
}