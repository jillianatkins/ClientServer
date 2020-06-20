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
            if (string.IsNullOrEmpty(PartialProductNameV2.Text))
            {
                ShowMessage("Enter a Product Name or Parial Name", "alert alert-info");
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
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
                }
            }
        }
    }
}