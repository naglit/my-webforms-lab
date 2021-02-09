using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab_WebForms.Form
{
    public partial class ClickFromCodeBehind : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lUser.Text = "John";
            }
        }

        protected void lbRunClientScript_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(
                this.GetType(), 
                "ProcessCompletionNotification", 
                "processCompletionNotification()", 
                true);
        }

        protected void lbClearOutput_Click(object sender, EventArgs e)
        {
            lUser.Text = "";
        }
    }
}