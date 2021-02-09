using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab_WebForms.Form
{
    public partial class Output : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var a = "12345";
            //var c = a.Length;
            //var d = string.Format("{0: ##-####-####}", 0466833678);
            //var f = Regex.Replace("0466833678", @"(\d{2})(\d{4})(\d{4})", "$1-$2-$3");
            if (!IsPostBack)
            {
                this.Message = "a";
                lTest.Text = "visit here for the first time";
            }


        }
        protected string Message { get; set; }

        protected void test_Click(object sender, EventArgs e)
        {
            // do nothing
        }
    }
}