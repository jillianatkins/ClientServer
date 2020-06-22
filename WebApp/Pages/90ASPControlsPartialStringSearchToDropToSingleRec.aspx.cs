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
    public partial class _90ASPControlsPartialStringSearchToDropToSingleRec : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowMessage("","");
            if (!Page.IsPostBack)
            {
                Fetch01.Enabled = false;
                List01.Enabled = false;
            }
        }
        protected Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
        protected void ShowMessage(string message, string cssclass)
        {
            if (message == "")
            {
                MessageLabel.InnerHtml = "";
                MessageLabel.Attributes.Remove("class");
            }
            else
            {
                MessageLabel.Attributes.Add("class", cssclass);
                MessageLabel.InnerHtml = message;
            }
        }

        protected void SearchProductsPartial_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PartialProductName.Text))
            {
                ShowMessage("Enter a Product Name or Parial Name", "alert alert-warning");
            }
            else
            {
                try
                {
                    ProductController sysmgr01 = new ProductController();
                    List<Product> info01 = sysmgr01.FindByPartialName(PartialProductName.Text);
                    if (info01.Count == 0)
                    {
                        ShowMessage("No Partial Match was found", "alert alert-warning");
                    }
                    else
                    {
                        info01.Sort((x, y) => x.ProductName.CompareTo(y.ProductName));
                        Fetch01.Enabled = true;
                        List01.Enabled = true;
                        List01.DataSource = info01;
                        List01.DataTextField = nameof(Product.ProductandID);
                        List01.DataValueField = nameof(Product.ProductID);
                        List01.DataBind();
                        List01.Items.Insert(0, "select...");
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
                }
            }
        }
        protected void Fetch_Click01(object sender, EventArgs e)
        {
            if (List01.SelectedIndex == 0)
            {
                ShowMessage("Select a Product", "alert alert-warning");
            }
            else
            {
                try
                {
                    string productid = List01.SelectedValue;
                    Response.Redirect("94CRUDPageNW.aspx?page=90&pid=" + productid + "&add=" + "no");
                }
                catch (Exception ex)
                {
                    ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
                }
            }
        }
    }
}