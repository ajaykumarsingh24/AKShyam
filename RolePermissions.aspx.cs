using SavvyGreatCLIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SavvyGreat.Admin
{
    public partial class RolePermissions : Parent_Admin_Pages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGrid();
                FillPermissions();
                FillCompany();
            }
        }

        private void FillPermissions()
        {
            using (var admin = new AdminWrapper())
            {
                admin.Permission.Entity.CompanyId = this.CompanyId;
                chkstPermissions.DataSource = admin.Permission.Details(admin.Permission.Entity);
                chkstPermissions.DataBind();
                //   ddlRoleName.DataSource = admin.Role.Details(entity: null));
                // ddlRoleName.DataBind();
            }
        }

        private void FillCompany()
        {
            using (var admin = new AdminWrapper())
            {
                admin.Company.Entity.CompanyId = this.CompanyId;
                ddlCompany.DataSource = admin.Company.Details(entity: admin.Company.Entity);
                ddlCompany.DataBind();
            }
        }

        private void FillGrid()
        {
            using (var admin = new AdminWrapper())
            {
                admin.Role.Entity.CompanyId = this.CompanyId;
                gvRoles.DataSource = admin.Role.Details(admin.Role.Entity);
                gvRoles.DataBind();
            }
        }

        private void ClearControls()
        {
            hidRoleID.Value = "";
            //btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsValidPermission("create")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
            using (var admin = new AdminWrapper())
            {
                //admin.Role.Entity.CompanyName = txtRoleName.Text;
                //admin.Role.Entity.Description = txtdesc.Text;
                //admin.Role.Entity.IsActive = ChkActive.Checked;
                admin.Role.Entity.Logo = Session["filename"].ToString();
                admin.Role.Entity.CompanyId = ddlCompany.SelectedIndex;
                admin.Role.Insert(admin.Role.Entity);
                FillGrid();
                ClearControls();
                lblMessage.Text = "Saved Successfully.";
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void GetRolePermissions()
        {
            AdminWrapper admins = new AdminWrapper();
            admins.Role_permission.Entity.CompanyId = this.CompanyId;
            var lst = admins.Role_permission.GetRolePermissions(admins.Role_permission.Entity).Where(item => item.RoleId == int.Parse(hidRoleID.Value)).ToList();
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].PermissionId != null)
                {
                    ListItem listItem = chkstPermissions.Items.FindByValue(lst[i].PermissionId.Value.ToString());

                    if (listItem != null)
                        listItem.Selected = true;
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (!this.IsValidPermission("update")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
            ClearControls();
            Button btn = sender as Button;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            hidRoleID.Value = (grow.FindControl("lblRoleID") as Label).Text;
            string cpnyId = (grow.FindControl("lblCompanyId") as Label).Text;
            AdminWrapper admins = new AdminWrapper();
            admins.Role_permission.Entity.CompanyId = int.Parse(cpnyId);
            var lst =
                admins.Role_permission.GetRolePermissions(admins.Role_permission.Entity)
                    .Where(item => item.RoleId == int.Parse(hidRoleID.Value))
                    .ToList();
            var j = 0;
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].PermissionId != null)
                {
                    ListItem listItem = chkstPermissions.Items.FindByValue(lst[i].PermissionId.Value.ToString());

                    if (listItem != null)
                    {
                        listItem.Selected = true;
                        j += 1;
                    }
                }
            }
            if (j == 0)
                CheckUncheckAll(false);
            btnUpdate.Visible = true;
        }

        private void CheckUncheckAll(bool tf)
        {
            foreach (ListItem item in chkstPermissions.Items)
            {
                item.Selected = tf;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!this.IsValidPermission("update")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
            using (AdminWrapper admins = new AdminWrapper())
            {
                string limit = string.Empty;
                admins.Role_permission.Entity.CompanyId = int.Parse(ddlCompany.SelectedValue);
                admins.Role_permission.Entity.RoleId = int.Parse(hidRoleID.Value);
                admins.Role_permission.Delete(admins.Role_permission.Entity);

                for (int j = 0; j < chkstPermissions.Items.Count; j++)
                {
                    if (chkstPermissions.Items[j].Selected)
                    {
                        admins.Role_permission.Entity.CompanyId = int.Parse(ddlCompany.SelectedValue);
                        admins.Role_permission.Entity.RoleId = int.Parse(hidRoleID.Value);
                        admins.Role_permission.Entity.PermissionId = int.Parse(chkstPermissions.Items[j].Value);
                        admins.Role_permission.Insert(admins.Role_permission.Entity);
                    }
                }
                FillGrid();
                ClearControls();
                lblMessage.Text = "Updated Successfully.";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!this.IsValidPermission("delete")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
            ClearControls();
            Button btn = sender as Button;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            using (var admin = new AdminWrapper())
            {
                admin.Role.Entity.RoleId = int.Parse((grow.FindControl("lblRoleID") as Label).Text);
                admin.Role.Delete(admin.Role.Entity);
                FillGrid();
                lblMessage.Text = "Deleted Successfully.";
            }
        }
    }
}