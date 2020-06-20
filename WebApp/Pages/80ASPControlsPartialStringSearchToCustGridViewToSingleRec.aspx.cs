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
    public partial class _80ASPControlsPartialStringSearchToCustGridViewToSingleRec : System.Web.UI.Page
    {
        //List<string> errormsgs = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowMessage("" , "");
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
            if(message == "")
            {
                MessageLabel1.InnerHtml = "";
                MessageLabel1.Attributes.Remove("class");
            }
            else
            {
                MessageLabel1.Attributes.Add("class", cssclass);
                MessageLabel1.InnerHtml = message;
            }
        }
        
        protected void SearchProductsPartial_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PartialProductNameV2.Text))
            {
                ShowMessage("Enter a Product Name or Parial Name", "alert alert-info");
                ProductGridViewV2.DataSource = null;
                ProductGridViewV2.DataBind();
            }
            else
            {
                try
                {
                    ProductController sysmgr = new ProductController();
                    List<Product> info = sysmgr.FindByPartialName(PartialProductNameV2.Text);
                    if (info.Count == 0)
                    {
                        ShowMessage("Record was not found", "alert alert-warning");
                    }
                    else
                    {
                        info.Sort((x, y) => x.ProductName.CompareTo(y.ProductName));
                        ProductGridViewV2.DataSource = info;
                        ProductGridViewV2.DataBind();

                    }
                }
                catch (Exception ex)
                {
                    ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
                }
            }
        }
        protected void List02_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProductGridViewV2.PageIndex = e.NewPageIndex;
            SearchProductsPartial_Click(sender, new EventArgs());
        }
        protected void List02_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow agvrow = ProductGridViewV2.Rows[ProductGridViewV2.SelectedIndex];
            string productid = (agvrow.FindControl("ProductID") as Label).Text;
            Response.Redirect("94CRUDPageNW.aspx?page=80&pid=" + productid + "&add=" + "no");
        }
    }
}