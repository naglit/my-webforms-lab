using Model;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab_WebForms.Form
{
    public partial class VisibilitySwitching : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.IsLoginUser = true;
            if (!IsPostBack)
            {
                var shoes = new Product
                {
                    ProductId = 0,
                    ProductName = "shoes",
                    Price = 5000,
                    IsAvailable = true,
                };
                var cap = new Product
                {
                    ProductId = 1,
                    ProductName = "cap",
                    Price = 1000,
                    IsAvailable = true,
                };
                var tshirts = new Product
                {
                    ProductId = 2,
                    ProductName = "t-shirts",
                    Price = 2500,
                    IsAvailable = false,
                };
                var products = new Product[] { shoes, cap, tshirts };
                this.DisplayProductPrice = true;

                rProduct.DataSource = products;
                rProduct.DataBind();
            }
            else
            {
                this.IsLoginUser = false;
                var shoes = new Product
                {
                    ProductId = 0,
                    ProductName = "shoes",
                    Price = 5000,
                    IsAvailable = true,
                };
                var cap = new Product
                {
                    ProductId = 1,
                    ProductName = "cap",
                    Price = 3000,
                    IsAvailable = true,
                };
                var tshirts = new Product
                {
                    ProductId = 2,
                    ProductName = "t-shirts",
                    Price = 2500,
                    IsAvailable = false,
                };
                var products = new Product[] { shoes, cap, tshirts };

                rProduct.DataSource = products;
                rProduct.DataBind();
            }




        }
        protected void cbDisplayPrice_CheckedChanged(object sender, EventArgs e)
        {
            var page = this.Master.FindControl("MainContent");
            var r = (Repeater)page.FindControl("rProduct");
            foreach (RepeaterItem ri in r.Items)
            {
                var pp = (Literal)ri.FindControl("lProductPrice");
                pp.Visible = cbDisplayPrice.Checked;
            }

            this.DisplayProductPrice = cbDisplayPrice.Checked;

            DataBind();

        }


        protected bool IsLoginUser { get; set; }
        protected bool DisplayProductPrice { get; set; }
    }
}