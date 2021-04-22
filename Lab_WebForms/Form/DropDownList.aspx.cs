using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab_WebForms.Form
{
	public partial class DropDownList : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var listItems = Enumerable.Range(0, 10).Select(i => new ListItem(i.ToString(), i.ToString())).ToArray();
			foreach(var listItem in listItems){
				ddlSelectedValueTest.Items.Add(listItem);
			}
			
			this.SelectedValue = "<script>";
			DataBind();
			//ddlSelectedValueTest.SelectedValue = "";
		}
		
		protected string SelectedValue{ get; set; }
	}
}