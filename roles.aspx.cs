using SavvyGreatCLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SavvyGreat.Admin
{
    public partial class roles : Parent_Admin_Pages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtRoleName.Focus();
            if (!IsPostBack)
            {
                FillGrid();
                FillCompany();
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

        protected string UploadFolderPath = "~/Uploads/";

        protected void FileUploadComplete(object sender, EventArgs e)
        {
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            AsyncFileUpload1.SaveAs(Server.MapPath(this.UploadFolderPath) + filename);
            Session["fileName"] = filename;
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
            txtRoleName.Text = "";
            txtdesc.Text = "";
            hidRoleID.Value = "";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            ddlCompany.Enabled = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsValidPermission("create")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
            using (var admin = new AdminWrapper())
            {
                admin.Role.Entity.RoleName = txtRoleName.Text;
                admin.Role.Entity.Description = txtdesc.Text;
                admin.Role.Entity.IsActive = ChkActive.Checked;
                admin.Role.Entity.Logo = Session["filename"].ToString();
                admin.Role.Entity.CompanyId = int.Parse(ddlCompany.SelectedValue);
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (!this.IsValidPermission("update")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
            ClearControls();
            Button btn = sender as Button;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            hidRoleID.Value = (grow.FindControl("lblRoleID") as Label).Text;
            txtRoleName.Text = (grow.FindControl("lblRoleName") as Label).Text;
            txtdesc.Text = (grow.FindControl("lblDesc") as Label).Text;
            ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue((grow.FindControl("lblCompanyId") as Label).Text));
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            ddlCompany.Enabled = false;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!this.IsValidPermission("update")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
            using (var admin = new AdminWrapper())
            {
                admin.Role.Entity.RoleName = txtRoleName.Text;
                admin.Role.Entity.Description = txtdesc.Text;
                admin.Role.Entity.IsActive = ChkActive.Checked;
                admin.Role.Entity.CompanyId = int.Parse(ddlCompany.SelectedValue);
                admin.Role.Entity.RoleId = int.Parse(hidRoleID.Value);
                admin.Role.Entity.Logo = Session["filename"] != null ? Session["filename"].ToString() : null;
                //hidRoleID.Value
                // admin.Role.Entity.Logo = Session["filename"] 
                admin.Role.update(admin.Role.Entity);
                FillGrid();
                ClearControls();
                lblMessage.Text = "Updated Successfully.";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!this.IsValidPermission("delete")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
            try
            {
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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;

                throw;
            }
        }
    }
}