using SavvyGreatCLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SavvyGreat.Admin
{
    public partial class permissions : Parent_Admin_Pages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtPermissionName.Focus();
            if (!IsPostBack)
            {
                FillGrid();
                FillPermission();
            }
        }

        private void FillPermission()
        {
            using (var admin = new AdminWrapper())
            {
                admin.Company.Entity.CompanyId = this.CompanyId;
                ddlCompany.DataSource = admin.Company.Details(admin.Company.Entity);
                ddlCompany.DataBind();
            }
        }

        private void FillGrid()
        {
            using (var admin = new AdminWrapper())
            {
                admin.Permission.Entity.CompanyId = this.CompanyId;
                gvPermissions.DataSource = admin.Permission.Details(admin.Permission.Entity);
                gvPermissions.DataBind();
            }
        }

        private void ClearControls()
        {
            txtPermissionName.Text = "";
            txtdesc.Text = "";
            hidPermissionID.Value = "";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsValidPermission("create")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
            using (var admin = new AdminWrapper())
            {
                admin.Permission.Entity.PermissionName = txtPermissionName.Text;
                admin.Permission.Entity.Description = txtdesc.Text;
                admin.Permission.Entity.IsActive = ChkActive.Checked;
                admin.Permission.Entity.CompanyId = int.Parse(ddlCompany.SelectedValue);
                admin.Permission.Insert(admin.Permission.Entity);
                FillGrid();
                ClearControls();
                lblMessage.Text = "Saved Successfully.";
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (!this.IsValidPermission("update")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
            ClearControls();
            Button btn = sender as Button;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            hidPermissionID.Value = (grow.FindControl("lblPermissionID") as Label).Text;
            txtPermissionName.Text = (grow.FindControl("lblPermissionName") as Label).Text;
            txtdesc.Text = (grow.FindControl("lblDesc") as Label).Text;
            // imgDisplay.Src =  (grow.FindControl("imgLogo") as Image).ImageUrl;
            //imgDisplay.Style.Value = @"width: 80px;height: 80px; display: block;";
            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!this.IsValidPermission("update")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
            using (var admin = new AdminWrapper())
            {
                admin.Permission.Entity.PermissionName = txtPermissionName.Text;
                admin.Permission.Entity.Description = txtdesc.Text;
                admin.Permission.Entity.IsActive = ChkActive.Checked;
                admin.Permission.Entity.PermissionId = int.Parse(hidPermissionID.Value);
                admin.Permission.Entity.CompanyId = int.Parse(ddlCompany.SelectedValue);                
                //hidPermissionID.Value
                // admin.Permission.Entity.Logo = Session["filename"] 
                admin.Permission.update(admin.Permission.Entity);
                FillGrid();
                ClearControls();
                lblMessage.Text = "Updated Successfully.";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsValidPermission("delete")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
                ClearControls();
                Button btn = sender as Button;
                GridViewRow grow = btn.NamingContainer as GridViewRow;
                using (var admin = new AdminWrapper())
                {
                    admin.Permission.Entity.PermissionId = int.Parse((grow.FindControl("lblPermissionID") as Label).Text);
                    admin.Permission.Delete(admin.Permission.Entity);
                    FillGrid();
                    lblMessage.Text = "Deleted Successfully.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}