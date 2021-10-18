using System;
using System.IO;
using System.Web.Services;
using System.Web.UI;

namespace Lab_WebForms
{
	public partial class _Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
		}
		[WebMethod]
		public static bool TestJquery()
		{
			var a = 1;
			return true;
		}
	}
}