using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DBSystem.BLL;
using DBSystem.ENTITIES;

namespace WebApp.Pages
{
    public partial class _70ASPControlsMultiRecordDropToDropToSingleRec : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = "";
            if (!Page.IsPostBack)
            {
                BindList();
            }
        }
        protected void BindList()
        {
            try
            {
                Fetch02.Enabled = false;
                List02.Enabled = false;
                CategoryController sysmgr = new CategoryController();
                List<Category> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.CategoryName.CompareTo(y.CategoryName));
                List01.DataSource = info;
                List01.DataTextField = nameof(Category.CategoryName);
                List01.DataValueField = nameof(Category.CategoryID);
                List01.DataBind();
                List01.Items.Insert(0, "select...");
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }
        protected void Fetch_Click01(object sender, EventArgs e)
        {
            if (List01.SelectedIndex == 0)
            {
                MessageLabel.Text = "Select a Category to view its Products";
            }
            else
            {
                try
                {
                    ProductController sysmgr02 = new ProductController();
                    List<Product> info02 = null;
                    info02 = sysmgr02.FindByID(int.Parse(List01.SelectedValue));
                    info02.Sort((x, y) => x.ProductName.CompareTo(y.ProductName));
                    Fetch02.Enabled = true;
                    List02.Enabled = true;
                    List02.DataSource = info02;
                    List02.DataTextField = nameof(Product.ProductandID);
                    List02.DataValueField = nameof(Product.ProductID);
                    List02.DataBind();
                    List02.Items.Insert(0, "select...");

                }
                catch (Exception ex)
                {
                    MessageLabel.Text = ex.Message;
                }
            }
        }
        protected void Fetch_Click02(object sender, EventArgs e)
        {
            if (List02.SelectedIndex == 0)
            {
                MessageLabel.Text = "Select a Product";
            }
            else
            {
                try
                {
                    string productid = List02.SelectedValue;
                    Response.Redirect("94CRUDPageNW.aspx?page=70&pid=" + productid + "&add=" + "no");
                }
                catch (Exception ex)
                {
                    MessageLabel.Text = ex.Message;
                }
            }
        }
    }
}