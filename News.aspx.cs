using SavvyGreatCLIB;
using SavvyGreatCLIB.BAL;
using System;
using System.Data;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Web.UI.WebControls;

namespace SavvyGreat.Admin
{
    public partial class News : Parent_Admin_Pages
    {
        private BL_Admin_NewUpdate blobj;
        private DataTable dt = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (!IsPostBack)
                    {
                        txtNewsDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        txtexpDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        if (Session["ProfileId"] == null)
                            Response.Redirect("~/UserLogin.aspx");
                        else
                        {
                            BindCompany();
                            BindCountry();
                            BindNewsCategory();
                            BindNewsSubCategory();
                            GetNewsUpdateData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindCompany()
        {
            using (var admin = new AdminWrapper())
            {
                admin.Company.Entity.CompanyId = this.CompanyId;
                ddlCompany.DataSource = admin.Company.Details(admin.Company.Entity);
                ddlCompany.DataBind();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    byte[] Image = FileUpload1.FileBytes;
                    Session["NewsUpdate"] = Image;
                    ImageMap1.ImageUrl = "NewsImage.ashx";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetNewsUpdateData()
        {
            try
            {
                DateTimeFormatInfo FormatInfo = DateTimeFormatInfo.CurrentInfo.Clone() as DateTimeFormatInfo;
                FormatInfo.ShortDatePattern = "dd/MM/yyyy";

                blobj = new BL_Admin_NewUpdate();
                blobj.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                dt = new DataTable();
                dt = blobj.GetNewsUpdateData(blobj);
                GrdNewsUpdate.DataSource = dt;
                GrdNewsUpdate.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindNewsCategory()
        {
            try
            {
                blobj = new BL_Admin_NewUpdate();
                dt = new DataTable();
                dt = blobj.BindNewsCategory(blobj);
                blobj.BindDropDownControl(dt, ddlCategory, "NewsCatName", "NewsCatCode", 1);
                ddlCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindNewsSubCategory()
        {
            try
            {
                blobj = new BL_Admin_NewUpdate();
                dt = new DataTable();
                dt = blobj.BindNewsSubCategory(blobj);
                blobj.BindDropDownControl(dt, ddlSubCategory, "NewsSubCatName", "NewsSubCatCode", 1);
                ddlSubCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindCountry()
        {
            try
            {
                blobj = new BL_Admin_NewUpdate();
                dt = new DataTable();
                dt = blobj.BindCountry(blobj);
                blobj.BindDropDownControl(dt, ddlCountry, "Country", "CountryId", 1);
                ddlCountry.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsValidPermission("Create"))
                {
                    lblMessage.Text = "User don't have permission to perform this action";
                    return;
                }
                DateTimeFormatInfo FormatInfo = DateTimeFormatInfo.CurrentInfo.Clone() as DateTimeFormatInfo;
                FormatInfo.ShortDatePattern = "dd/MM/yyyy";

                blobj = new BL_Admin_NewUpdate();
                blobj.ImageTitle = txtTitle.Text.Trim();
                blobj.CountryId = Convert.ToInt32(ddlCountry.SelectedIndex <= 0 ? "0" : ddlCountry.SelectedValue);
                blobj.UniqueSrNo = Convert.ToInt32(lblSrNo.Text == string.Empty ? "0" : lblSrNo.Text);
                blobj.Category = ddlCategory.SelectedValue;
                blobj.SubCategory = ddlSubCategory.SelectedValue;
                blobj.NewsDesc = txtNewsDescription.Text.Trim();
                blobj.Image = (Session["NewsUpdate"] == null ? null : (byte[])Session["NewsUpdate"]);
                string Time = "" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" +
                              DateTime.Now.TimeOfDay.Minutes.ToString() + "";
                blobj.DateCreated = Convert.ToDateTime(txtNewsDate.Text, FormatInfo).ToString("MM/dd/yyyy " + Time + "");
                blobj.DateExpired = Convert.ToDateTime(txtexpDate.Text, FormatInfo).ToString("MM/dd/yyyy " + Time + "");
                blobj.UserCreated = Session["ProfileId"].ToString();
                blobj.UserModified = Session["ProfileId"].ToString();
                blobj.MacIP =
                    System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
                blobj.MacName = System.Net.Dns.GetHostName();
                blobj.ServerIP = Request.ServerVariables["remote_host"];
                blobj.ServerName = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_host"]).HostName;
                blobj.isBreackingNew = chlisbreakingnew.Checked;
                blobj.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

                blobj.MacAddress = GetMACAddress();
                int Ins = blobj.InsertUpdate_NewsUpdate(blobj);
                if (Ins > 0)
                {
                    ClearData();
                    GetNewsUpdateData();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearData()
        {
            try
            {
                txtTitle.Text = string.Empty;
                BindNewsCategory();
                BindNewsSubCategory();
                txtNewsDescription.Text = string.Empty;
                Session["NewsUpdate"] = null;
                ImageMap1.ImageUrl = string.Empty;
                btnSubmit.Text = "Submit";
                lblSrNo.Text = string.Empty;
                ddlCountry.SelectedIndex = 0;
                GrdNewsUpdate.DataSource = string.Empty;
                GrdNewsUpdate.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetMACAddress()
        {
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                String sMacAddress = string.Empty;
                foreach (NetworkInterface adapter in nics)
                {
                    if (sMacAddress == String.Empty) // only return MAC Address from first card
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        sMacAddress = adapter.GetPhysicalAddress().ToString();
                    }
                }
                return sMacAddress;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {

                if (!this.IsValidPermission("update"))
                {
                    lblMessage.Text = "User don't have permission to perform this action";
                    return;
                }

                int i = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                blobj = new BL_Admin_NewUpdate();

                lblSrNo.Text = ((Label)GrdNewsUpdate.Rows[i].FindControl("lblUniqueSrNo")).Text;
                txtTitle.Text = ((Label)GrdNewsUpdate.Rows[i].FindControl("lblImageTitle")).Text;

                BindNewsCategory();
                var NewsCatCode = ((Label)GrdNewsUpdate.Rows[i].FindControl("lblNewsCatCode")).Text;
                if (!String.IsNullOrEmpty(NewsCatCode) && ddlCategory.Items.Count > 0)
                    //ddlCategory.Items.FindByValue(NewsCatCode).Selected = true;
                    ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue(NewsCatCode));



                BindNewsSubCategory();
                var NewsSubCatCode = ((Label)GrdNewsUpdate.Rows[i].FindControl("lblNewsSubCatCode")).Text;
                if (!String.IsNullOrEmpty(NewsSubCatCode) && ddlSubCategory.Items.Count > 0)

                    ddlSubCategory.SelectedIndex = ddlSubCategory.Items.IndexOf(ddlSubCategory.Items.FindByValue(NewsSubCatCode));

                txtNewsDescription.Text = ((Label)GrdNewsUpdate.Rows[i].FindControl("lblNewsDesc")).Text;

                BindCountry();
                var CountryId = ((Label)GrdNewsUpdate.Rows[i].FindControl("lblCountryId")).Text;
                if (!String.IsNullOrEmpty(CountryId) && ddlCountry.Items.Count > 0)

                    ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(CountryId));


                dt = new DataTable();
                blobj.UniqueSrNo = Convert.ToInt32(lblSrNo.Text);
                ddlCompany.Enabled = false;

                //blobj.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

                dt = blobj.GetImage_NewsUpdate(blobj);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Image"].ToString() != string.Empty)
                    {
                        byte[] Image = (byte[])dt.Rows[0]["Image"];
                        Session["NewsUpdate"] = Image;
                        ImageMap1.ImageUrl = "NewsImage.ashx";
                    }
                }

                btnSubmit.Text = "Update";
                chlisbreakingnew.Checked =
                    string.IsNullOrEmpty(((Label)GrdNewsUpdate.Rows[i].FindControl("lblbrakingnew")).Text)
                        ? false
                        : bool.Parse(((Label)GrdNewsUpdate.Rows[i].FindControl("lblbrakingnew")).Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {

                if (!this.IsValidPermission("delete"))
                {
                    lblMessage.Text = "User don't have permission to perform this action";
                    return;
                }
                int i = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                blobj = new BL_Admin_NewUpdate();
                blobj.UniqueSrNo = Convert.ToInt32(((Label)GrdNewsUpdate.Rows[i].FindControl("lblUniqueSrNo")).Text);

                int Ins = blobj.Delete_NewsUpdate(blobj);
                if (Ins > 0)
                {
                    ClearData();
                    GetNewsUpdateData();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}