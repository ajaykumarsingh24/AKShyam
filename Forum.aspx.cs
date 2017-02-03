using SavvyGreatCLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SavvyGreat.Admin
{
    public partial class Forum : Parent_Admin_Pages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtArticleName.Focus();
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
                admin.Article.Entity.CompanyId = this.CompanyId;
                gvArticles.DataSource = admin.Article.Details(admin.Article.Entity);
                gvArticles.DataBind();
            }
        }

        private void ClearControls()
        {
            txtArticleName.Text = "";
            txtdesc.Text = "";
            hidArticleID.Value = "";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (var admin = new AdminWrapper())
            {
                admin.Article.Entity.ArticleName = txtArticleName.Text;
                admin.Article.Entity.Description = txtdesc.Text;
                admin.Article.Entity.IsActive = ChkActive.Checked;
                admin.Article.Entity.Logo = Session["filename"].ToString();
                admin.Article.Entity.CompanyId = int.Parse(ddlCompany.SelectedValue);
                admin.Article.Insert(admin.Article.Entity);
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
            ClearControls();
            Button btn = sender as Button;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            hidArticleID.Value = (grow.FindControl("lblArticleID") as Label).Text;
            txtArticleName.Text = (grow.FindControl("lblArticleName") as Label).Text;
            txtdesc.Text = (grow.FindControl("lblDesc") as Label).Text;
            // imgDisplay.Src =  (grow.FindControl("imgLogo") as Image).ImageUrl;
            //imgDisplay.Style.Value = @"width: 80px;height: 80px; display: block;";
            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            using (var admin = new AdminWrapper())
            {
                admin.Article.Entity.ArticleName = txtArticleName.Text;
                admin.Article.Entity.Description = txtdesc.Text;
                admin.Article.Entity.IsActive = ChkActive.Checked;
                admin.Article.Entity.ArticleId = int.Parse(hidArticleID.Value);
                admin.Article.Entity.Logo = Session["filename"] != null ? Session["filename"].ToString() : null;
                admin.Article.Entity.CompanyId = int.Parse(ddlCompany.SelectedValue);
                //hidArticleID.Value
                // admin.Article.Entity.Logo = Session["filename"] 
                admin.Article.update(admin.Article.Entity);
                FillGrid();
                ClearControls();
                lblMessage.Text = "Updated Successfully.";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClearControls();
            Button btn = sender as Button;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            using (var admin = new AdminWrapper())
            {
                admin.Article.Entity.ArticleId = int.Parse((grow.FindControl("lblArticleID") as Label).Text);
                admin.Article.Delete(admin.Article.Entity);
                FillGrid();
                lblMessage.Text = "Deleted Successfully.";
            }
        }
    }
}