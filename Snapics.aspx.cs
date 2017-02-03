using SavvyGreatCLIB;
using SavvyGreatCLIB.BAL;
using System;
using System.Data;

namespace SavvyGreat.Admin
{
    public partial class Snapics : Parent_Admin_Pages
    {
        private BL_Admin_Snapics blobj;
        private DataTable dt, dt2;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FillCompany();
                    if (Session["ProfileId"] == null)
                        Response.Redirect("~/UserLogin.aspx");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnUpload1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsValidPermission("create"))
                { lblMessage.Text = "User don't have permission to perform this action"; return; }
                if (FileUpload1.HasFile)
                {
                    byte[] Image = FileUpload1.FileBytes;
                    Session["Snapics1"] = Image;
                    ImageMap1.ImageUrl = "SnapicsImage.ashx?val=1";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnUpload2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsValidPermission("create"))
                { lblMessage.Text = "User don't have permission to perform this action"; return; }
                if (FileUpload2.HasFile)
                {
                    byte[] Image = FileUpload2.FileBytes;
                    Session["Snapics2"] = Image;
                    ImageMap2.ImageUrl = "SnapicsImage.ashx?val=2";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnUpload3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsValidPermission("create"))
                { lblMessage.Text = "User don't have permission to perform this action"; return; }
                if (FileUpload3.HasFile)
                {
                    byte[] Image = FileUpload3.FileBytes;
                    Session["Snapics3"] = Image;
                    ImageMap3.ImageUrl = "SnapicsImage.ashx?val=3";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnUpload4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsValidPermission("create"))
                { lblMessage.Text = "User don't have permission to perform this action"; return; }
                if (FileUpload4.HasFile)
                {
                    byte[] Image = FileUpload4.FileBytes;
                    Session["Snapics4"] = Image;
                    ImageMap4.ImageUrl = "SnapicsImage.ashx?val=4";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsValidPermission("create"))
                { lblMessage.Text = "User don't have permission to perform this action"; return; }

                int InsSnap = 0;

                blobj = new BL_Admin_Snapics();
                blobj.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);

                blobj.CreatedBy = Session["UserSession"].ToString();
                if (Session["Snapics1"] != null)
                {
                    blobj.ImageURL = (Session["Snapics1"] == null ? null : (byte[])Session["Snapics1"]);
                    blobj.ImageName = txtImage1.Text == string.Empty ? null : txtImage1.Text.Trim();
                    blobj.ImageNo = 1;
                    InsSnap = blobj.Insert_Snapics(blobj);
                }
                if (Session["Snapics2"] != null)
                {
                    blobj.ImageURL = (Session["Snapics2"] == null ? null : (byte[])Session["Snapics2"]);
                    blobj.ImageName = txtImage2.Text == string.Empty ? null : txtImage2.Text.Trim();
                    blobj.ImageNo = 2;
                    InsSnap = blobj.Insert_Snapics(blobj);
                }
                if (Session["Snapics3"] != null)
                {
                    blobj.ImageURL = (Session["Snapics3"] == null ? null : (byte[])Session["Snapics3"]);
                    blobj.ImageName = txtImage3.Text == string.Empty ? null : txtImage3.Text.Trim();
                    blobj.ImageNo = 3;
                    InsSnap = blobj.Insert_Snapics(blobj);
                }
                if (Session["Snapics4"] != null)
                {
                    blobj.ImageURL = (Session["Snapics4"] == null ? null : (byte[])Session["Snapics4"]);
                    blobj.ImageName = txtImage4.Text == string.Empty ? null : txtImage4.Text.Trim();
                    blobj.ImageNo = 4;
                    InsSnap = blobj.Insert_Snapics(blobj);
                }

                Session["Snapics1"] = null;
                Session["Snapics2"] = null;
                Session["Snapics3"] = null;
                Session["Snapics4"] = null;

                txtImage1.Text = string.Empty;
                txtImage2.Text = string.Empty;
                txtImage3.Text = string.Empty;
                txtImage4.Text = string.Empty;

                ImageMap1.ImageUrl = string.Empty;
                ImageMap2.ImageUrl = string.Empty;
                ImageMap3.ImageUrl = string.Empty;
                ImageMap4.ImageUrl = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
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
    }
}