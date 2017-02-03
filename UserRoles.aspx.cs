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
    public partial class UserRoles : Parent_Admin_Pages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillCompany();
                FillUsers();
                FillRoles();
                FillGrid();
            }
        }


        private void FillUsers()
        {
            using (var admin = new AdminWrapper())
            {
                admin.User_Detail.Entity.CompanyId = this.CompanyId;
                gvUsers.DataSource = admin.User_Detail.Details(admin.User_Detail.Entity);
                gvUsers.DataBind();
            }
        }

        private void FillRoles()
        {
            using (var admin = new AdminWrapper())
            {
                admin.Role.Entity.CompanyId = this.CompanyId;
                chklstRoles.DataSource = admin.Role.Details(admin.Role.Entity);
                chklstRoles.DataBind();
            }
        }

        private void FillCompany()
        {
            try
            {
                using (var admin = new AdminWrapper())
                {
                    admin.Company.Entity.CompanyId = this.CompanyId;
                    ddlCompany.DataSource = admin.Company.Details(entity: admin.Company.Entity);
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
                    admin.User_Role.Entity.CompanyId = this.CompanyId;
                    gvUsers.DataSource = admin.User_Role.Details(admin.User_Role.Entity);
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
                btnSave.Visible = true;
                btnUpdate.Visible = false;
            }
            catch
            {
                throw;
            }
        }

        private static Random random = new Random((int) DateTime.Now.Ticks);

        private string RandomString(int Size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < Size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26*random.NextDouble() + 65)));
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
                hidProfileId.Value = (grow.FindControl("lblProfileId") as Label).Text;
                var j = 0;
                //using ()
                //{
                AdminWrapper admins = new AdminWrapper();
                admins.User_Role.Entity.CompanyId = this.CompanyId;
                var lst =
                    admins.User_Role.GetUserRoles(admins.User_Role.Entity)
                        .Where(
                            item =>
                                item.ProfileId == hidProfileId.Value.ToString())
                        .ToList();
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].RoleId != null)
                    {
                        ListItem listItem = chklstRoles.Items.FindByValue(lst[i].RoleId.Value.ToString());

                        if (listItem != null)
                        {
                            listItem.Selected = true;
                            j += 1;
                        }
                    }
                }
                //}
                if (j == 0)
                    CheckUncheckAll(false);
                btnUpdate.Visible = true;
            }
            catch
            {
                throw;
            }
        }

        private void CheckUncheckAll(bool tf)
        {
            foreach (ListItem item in chklstRoles.Items)
            {
                item.Selected = tf;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsValidPermission("update")) { lblMessage.Text = "User don't have permission to perform this action"; return; }
                using (AdminWrapper admins = new AdminWrapper())
                {
                    admins.User_Role.Entity.CompanyId = int.Parse(ddlCompany.SelectedValue);
                    admins.User_Role.Entity.RoleId = int.Parse(hidProfileId.Value);
                    admins.User_Role.Delete(admins.User_Role.Entity);
                    for (int j = 0; j < chklstRoles.Items.Count; j++)
                    {
                        if (chklstRoles.Items[j].Selected)
                        {
                            admins.User_Role.Entity.CompanyId = int.Parse(ddlCompany.SelectedValue);
                            admins.User_Role.Entity.ProfileId = hidProfileId.Value;
                            admins.User_Role.Entity.RoleId = int.Parse(chklstRoles.Items[j].Value);
                            admins.User_Role.Insert(admins.User_Role.Entity);
                        }
                    }
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
                    admin.Role.Entity.CompanyId = int.Parse((grow.FindControl("lblCustomerID") as Label).Text);
                    admin.Role.Delete(admin.Role.Entity);
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
                using (AdminWrapper admins = new AdminWrapper())
                {
                    string limit = string.Empty;

                    for (int j = 0; j < chklstRoles.Items.Count; j++)
                    {
                        if (chklstRoles.Items[j].Selected)
                        {
                            admins.User_Role.Entity.CompanyId = int.Parse(ddlCompany.SelectedValue);
                            admins.User_Role.Entity.RoleId = int.Parse(chklstRoles.Items[j].Value);
                            admins.User_Role.Entity.ProfileId = Session["ProfileId"].ToString();
                            admins.User_Role.Insert(admins.User_Role.Entity);
                        }
                    }
                }
                FillGrid();
                ClearControls();
                lblMessage.Text = "Saved Successfully.";
            }
            catch
            {
                throw;
            }
        }
    }
}