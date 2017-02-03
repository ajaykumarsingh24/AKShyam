using SavvyGreatCLIB;
using SavvyGreatCLIB.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SavvyGreat.Admin
{
    public partial class companies : Parent_Admin_Pages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtCompanyName.Focus();
            if (!IsPostBack)
            {
                FillGrid();
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
                admin.Company.Entity.CompanyId = this.CompanyId;
                gvCompanies.DataSource = admin.Company.Details(admin.Company.Entity);
                gvCompanies.DataBind();
            }
        }

        private void ClearControls()
        {
            txtCompanyName.Text = "";
            txtaddress.Text = "";
            txtdesc.Text = "";
            hidCompanyId.Value = "";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsValidPermission("Create")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
            using (var admin = new AdminWrapper())
            {
                admin.Company.Entity.Address = txtaddress.Text;
                admin.Company.Entity.CompanyName = txtCompanyName.Text;
                admin.Company.Entity.Description = txtdesc.Text;
                admin.Company.Entity.IsActive = ChkActive.Checked;
                admin.Company.Entity.Logo = Session["filename"].ToString();
                admin.Company.Insert(admin.Company.Entity);
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
            hidCompanyId.Value = (grow.FindControl("lblCustomerID") as Label).Text;
            txtCompanyName.Text = (grow.FindControl("lblCompanyName") as Label).Text;
            txtaddress.Text = (grow.FindControl("lblAddress") as Label).Text;
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
                admin.Company.Entity.Address = txtaddress.Text;
                admin.Company.Entity.CompanyName = txtCompanyName.Text;
                admin.Company.Entity.Description = txtdesc.Text;
                admin.Company.Entity.IsActive = ChkActive.Checked;
                admin.Company.Entity.CompanyId = int.Parse(hidCompanyId.Value);
                admin.Company.Entity.Logo = Session["filename"] != null ? Session["filename"].ToString() : null;
                //hidCompanyId.Value
                // admin.Company.Entity.Logo = Session["filename"] 
                admin.Company.update(admin.Company.Entity);
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
                admin.Company.Entity.CompanyId = int.Parse((grow.FindControl("lblCustomerID") as Label).Text);
                if (admin.Company.Entity.CompanyId != this.CompanyId)
                {
                    admin.Company.Delete(admin.Company.Entity);
                    FillGrid();
                    lblMessage.Text = "Deleted Successfully.";
                }
                else
                {
                    lblMessage.Text = "You can not delete base company.";
                }
            }
        }
    }
}