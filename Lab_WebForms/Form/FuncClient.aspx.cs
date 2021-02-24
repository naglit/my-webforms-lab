using System;
using System.Web.UI;
using Lab.Utility.Encryption;

namespace Lab_WebForms.Form
{
	public partial class FuncClient : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var enc = new Encryption();
			//var bytes = enc.Testing("im testing GetBytes func");
		}
	}
}