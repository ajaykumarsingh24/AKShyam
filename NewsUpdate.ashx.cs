using SavvyGreatCLIB.BAL;
using System;
using System.Data;
using System.Web;
using System.Web.Services;

namespace SavvyGreat.Admin
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class NewsUpdate : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {
        private DataTable dt = null;
        private BL_Admin_NewUpdate blobj;

        public void ProcessRequest(HttpContext context)
        {
            blobj = new BL_Admin_NewUpdate();
            string strVal = context.Request.QueryString["val"];
            blobj.UniqueSrNo = Convert.ToInt32(strVal);
            dt = new DataTable();
            dt = blobj.GetImage_NewsUpdate(blobj);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Image"].ToString() != string.Empty)
                {
                    byte[] Image = (byte[])dt.Rows[0]["Image"];
                    context.Response.ContentType = "Image/JPEG";
                    context.Response.BinaryWrite(Image);
                }
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}