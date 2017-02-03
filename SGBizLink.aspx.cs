using SavvyGreatCLIB.BAL;
using System;
using System.Data;
using System.Net.NetworkInformation;
using System.Web.UI.WebControls;
using SavvyGreat.Admin;

namespace SavvyGreat.Admin
{
    public partial class SGBizLink : Parent_Admin_Pages
    {
        private DataTable dt = null;
        private BL_SGBizLink blobj;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMesg.Text = string.Empty;
            if (!IsPostBack)
            {
                if (Session["ProfileId"] == null)
                    Response.Redirect("~/UserLogin.aspx");
                else
                {
                    GetSGData();
                }
            }
        }

        private void GetSGData()
        {
            blobj = new BL_SGBizLink();
            dt = new DataTable();
            dt = blobj.GetSGBizLink(blobj);
            GrdSGBizLink.DataSource = dt;
            GrdSGBizLink.DataBind();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
           
            if (FileUpload1.HasFile)
            {
                byte[] Image = FileUpload1.FileBytes;
                Session["SGBizLink"] = Image;
                ImageMap1.ImageUrl = "SGBizLink.ashx";
            }
            else
            {
                lblMesg.Text = "You did not specify a file to upload";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsValidPermission("Create"))
            {
                lblMesg.Text = "User don't have permission to perform this action";
                return;
            }
            if (Session["SGBizLink"] == null)
                lblMesg.Text = "You did not specify a file to upload";
            else if (txtURL.Text == string.Empty)
                lblMesg.Text = "Please enter URL";
            else if (txtTitle.Text == string.Empty)
                lblMesg.Text = "Please enter Title";
            else
            {
                blobj = new BL_SGBizLink();
                blobj.Image = (byte[])Session["SGBizLink"];
                blobj.ImageTitle = txtTitle.Text.Trim();
                blobj.URL = txtURL.Text.Trim();
                blobj.UserCreated = Session["ProfileId"].ToString();
                blobj.MacIP =
                    System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
                blobj.MacName = System.Net.Dns.GetHostName();
                blobj.ServerIP = Request.ServerVariables["remote_host"];
                blobj.ServerName = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_host"]).HostName;
                blobj.MacAddress = GetMACAddress();

                int InsUpd = blobj.Insert_SGBizLink(blobj);
                if (InsUpd > 0)
                    lblMesg.Text = "SG Biz Link successfully saved";

                GetSGData();
            }
        }

        private void ClearData()
        {
            Session["SGBizLink"] = null;
            ImageMap1.ImageUrl = "~/Images/BlankImage.jpg";
            txtURL.Text = string.Empty;
            txtTitle.Text = string.Empty;
        }

        public string GetMACAddress()
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

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            if (!this.IsValidPermission("delete"))
            {
                lblMesg.Text = "User don't have permission to perform this action";
                return;
            }
            int i = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            blobj = new BL_SGBizLink();
            blobj.UniqueSrNo = Convert.ToInt32(((Label)GrdSGBizLink.Rows[i].FindControl("lblUniqueSrNo")).Text);
            int Del = blobj.Delete_SGBizLink(blobj);
            if (Del > 0)
                GetSGData();
            else
                lblMesg.Text = "Error occured";
        }
    }
}