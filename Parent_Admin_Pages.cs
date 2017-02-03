using SavvyGreatCLIB.DAL.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SavvyGreat.Admin
{
    public class Parent_Admin_Pages : System.Web.UI.Page
    {
        public string ProfileId
        {
            get { return Session["ProfileId"].ToString(); }
        }

        public string UserSession
        {
            get { return Session["UserSession"].ToString(); }
        }

        public int CompanyId
        {
            get { return Session["CompanyId"] == null ? 0 : int.Parse(Session["CompanyId"].ToString()); }
        }

        private List<GetPermissionbyProfileIdResult> UserPermissions
        {
            get { return (List<GetPermissionbyProfileIdResult>)Session["userPermissions"]; }
        }

        public bool IsValidPermission(string actionName)
        {
            bool valid = false;
            try
            {
                valid = UserPermissions.Where(item => item.PermissionCode.ToLower() == (this.PageName + "/" + actionName).ToLower()).Count() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return valid;
        }

        public string PageName
        {
            get
            {
               return Path.GetFileName(Request.PhysicalPath.Split('\\')[Request.PhysicalPath.Split('\\').Length - 1]);
            }
        }
    }
}