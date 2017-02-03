using SavvyGreatCLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SavvyGreat.Admin
{
    public partial class Ads : Parent_Admin_Pages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtAdName.Focus();
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
                ddlCompany.DataSource = admin.Company.Details(admin.Company.Entity);
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
                admin.Ad.Entity.CompanyId = this.CompanyId;
                gvAds.DataSource = admin.Ad.Details(admin.Ad.Entity);
                gvAds.DataBind();
            }
        }

        private void ClearControls()
        {
            txtAdName.Text = "";
            txtdesc.Text = "";
            hidAdID.Value = "";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsValidPermission("create")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
            using (var admin = new AdminWrapper())
            {
                admin.Ad.Entity.AdName = txtAdName.Text;
                admin.Ad.Entity.Description = txtdesc.Text;
                admin.Ad.Entity.IsActive = ChkActive.Checked;
                admin.Ad.Entity.Logo = Session["filename"].ToString();
                admin.Ad.Entity.CompanyId = int.Parse(ddlCompany.SelectedValue);
                admin.Ad.Insert(admin.Ad.Entity);
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
            hidAdID.Value = (grow.FindControl("lblAdID") as Label).Text;
            txtAdName.Text = (grow.FindControl("lblAdName") as Label).Text;
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
                admin.Ad.Entity.AdName = txtAdName.Text;
                admin.Ad.Entity.Description = txtdesc.Text;
                admin.Ad.Entity.IsActive = ChkActive.Checked;
                admin.Ad.Entity.AdId = int.Parse(hidAdID.Value);
                admin.Ad.Entity.Logo = Session["filename"] != null ? Session["filename"].ToString() : null;
                admin.Ad.Entity.CompanyId = int.Parse(ddlCompany.SelectedValue);
                //hidAdID.Value
                // admin.Ad.Entity.Logo = Session["filename"] 
                admin.Ad.update(admin.Ad.Entity);
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
                admin.Ad.Entity.AdId = int.Parse((grow.FindControl("lblAdID") as Label).Text);
                admin.Ad.Delete(admin.Ad.Entity);
                FillGrid();
                lblMessage.Text = "Deleted Successfully.";
            }
        }
    }
}