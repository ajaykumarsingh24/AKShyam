using SavvyGreatCLIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SavvyGreat.Admin
{
    public partial class UsersDetails : Parent_Admin_Pages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FillGrid();
                    FillCompany();
                }
            }
            catch
            {
                throw;
            }
        }

        private void FillCompany()
        {
            try
            {
                using (var admin = new AdminWrapper())
                {
                    admin.Company.Entity.CompanyId = this.CompanyId;
                    ddlCompany.DataSource = admin.Company.Details(admin.Company.Entity);
                    ddlCompany.DataBind();
                }
            }
            catch
            {
                throw;
            }
        }


        private void FillGrid()
        {
            try
            {
                using (var admin = new AdminWrapper())
                {
                    admin.User_Detail.Entity.CompanyId = this.CompanyId;
                    gvUsers.DataSource = admin.User_Detail.Details(admin.User_Detail.Entity);
                    gvUsers.DataBind();
                }
            }
            catch
            {
                throw;
            }
        }

        private void ClearControls()
        {
            try
            {
                txtEmail.Text = string.Empty;
                txtfname.Text = string.Empty;
                txtMobile.Text= string.Empty;
                txtpass.Text = string.Empty;
                txlname.Text = string.Empty;
                
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                txtEmail.Enabled = true;
                ddlCompany.Enabled = true;

            }
            catch
            {
                throw;
            }
        }

        private static Random random = new Random((int)DateTime.Now.Ticks);

        private string RandomString(int Size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < Size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public string AutoGenNo()
        {
            var dt = new DataTable();
            var balobj2 = new BL_SGreats_UserLogin();
            dt = balobj2.GetProfileId();
            string ProfileId = dt.Rows[0]["ProfileId"].ToString();
            return ProfileId;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
            }
            catch
            {
                throw;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsValidPermission("update")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
                ClearControls();
                Button btn = sender as Button;
                GridViewRow grow = btn.NamingContainer as GridViewRow;
                txtfname.Text = (grow.FindControl("lblfname") as Label).Text;
                txlname.Text = (grow.FindControl("lbllname") as Label).Text;
                txtMobile.Text = (grow.FindControl("lblmobno") as Label).Text;
                txtpass.Text = (grow.FindControl("lblpwd") as Label).Text;
                txtEmail.Text = (grow.FindControl("lblemail") as Label).Text;
                hidProfileId.Value = (grow.FindControl("lblProfileId") as Label).Text;
                ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue((grow.FindControl("lblCompanyId") as Label).Text));
                txtEmail.Enabled = false;
                btnSave.Visible = false;
                btnUpdate.Visible = true;
                ddlCompany.Enabled = false;
            }
            catch
            {
                throw;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsValidPermission("update")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
                using (AdminWrapper admins = new AdminWrapper())
                {
                    //string limit = string.Empty;
                    admins.User_Detail.Entity.CompanyId = int.Parse(ddlCompany.SelectedValue);
                    admins.User_Detail.Entity.First_Name = txtfname.Text;
                    admins.User_Detail.Entity.Last_Name = txlname.Text;
                    admins.User_Detail.Entity.MobileNo = txtMobile.Text;
                    admins.User_Detail.Entity.New_Password = txtpass.Text;
                    admins.User_Detail.Entity.IsActive = ChkActive.Checked ? 1 : 0;
                    admins.User_Detail.Entity.ProfileId = hidProfileId.Value;
                    admins.User_Detail.update(admins.User_Detail.Entity);
                    FillGrid();
                    ClearControls();
                    lblMessage.Text = "Updated Successfully.";
                }
            }
            catch
            {
                throw;
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
                    admin.User_Detail.Entity.ProfileId = (grow.FindControl("lblprofileid") as Label).Text;
                    admin.User_Detail.Delete(admin.User_Detail.Entity);
                    FillGrid();
                    lblMessage.Text = "Deleted Successfully.";
                }
            }
            catch
            {
                throw;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsValidPermission("create")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
                var balobj = new BL_SGreats_UserLogin();
                balobj.ProfileId = AutoGenNo();
                balobj.Your_Email = txtEmail.Text.Trim();
                balobj.first_name = txtfname.Text.Trim();
                balobj.last_name = txlname.Text.Trim();
                balobj.Mobile_no = txtMobile.Text.Trim();
                balobj.New_Password = txtpass.Text.Trim();

                //Check duplicate email Id---------
                bool DupEmail = false;
                DupEmail = balobj.IsEmailIdExist(balobj);
                if (DupEmail == true)
                {
                    MessageBox.Show("Email ID already registered");
                    return;
                }

                //Check duplicate Mobile No---------
                bool DupMobNo = false;
                DupMobNo = balobj.IsMobileNoExist(balobj);
                if (DupMobNo == true)
                {
                    MessageBox.Show("Mobile No already registered");
                    return;
                }

                balobj.GenderID = ddlgender.SelectedIndex <= 0 ? (Guid?)null : Guid.Parse(ddlgender.SelectedValue);
                balobj.Birth_Date = DateTime.Now.ToShortDateString();
                balobj.VrfyNo = RandomString(10);
                balobj.Country = "113";
                balobj.CompanyId = int.Parse(ddlCompany.SelectedValue);
                int i = balobj.Insert_User(balobj);
                if (i > 0)
                {
                    MailMessage message = new MailMessage();
                    message.From = new System.Net.Mail.MailAddress("newdelhicafe@gmail.com");
                    message.To.Add(new System.Net.Mail.MailAddress(txtEmail.Text));
                    message.Subject = "Savvy Great Registration Confirmation";

                    MailBody MBody = new MailBody();
                    string Domain = Context.Request.Url.Authority;
                    message.Body = MBody.EmailVerification(Domain, txtEmail.Text.Trim(), balobj.VrfyNo);
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = true;
                    client.Send(message);
                    MessageBox.Show("Verification code sent to given email id");
                    //  ClearData();
                }
                else
                    MessageBox.Show("Some technical error, Please try again later");
            }
            catch
            {
                throw;
            }
        }
    }
}