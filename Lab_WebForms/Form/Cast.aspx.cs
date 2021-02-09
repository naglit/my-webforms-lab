using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab_WebForms.Form
{
    public partial class Cast : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ht = new Hashtable();
            var castedValue = (string)ht["key"];
            var number = int.Parse("１２");
        }
    }
}