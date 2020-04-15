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
        List<string> errormsgs = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = "";
            if (!Page.IsPostBack)
            {
                //BindList();
            }
        }


        protected Exception GetInnerException(Exception ex)
        {
            //drill down to the inner most exception
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }

        protected void LoadMessageDisplay(List<string> errormsglist, string cssclass)
        {
            Message.CssClass = cssclass;
            Message.DataSource = errormsglist;
            Message.DataBind();
        }


        protected void SearchProductsPartial_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PartialProductNameV2.Text))
            {
                errormsgs.Add("Please enter a partial product name for the search");
                LoadMessageDisplay(errormsgs, "alert alert-info");
                ProductGridViewV2.DataSource = null;
                ProductGridViewV2.DataBind();
            }
            else
            {
                try
                {
                    Controller02 sysmgr = new Controller02();
                    List<Entity02> info = sysmgr.FindByPartialName(PartialProductNameV2.Text);
                    if (info.Count == 0)
                    {
                        errormsgs.Add("No data found for the partial product name search");
                        LoadMessageDisplay(errormsgs, "alert alert-info");
                    }
                    else
                    {
                        info.Sort((x, y) => x.ProductName.CompareTo(y.ProductName));
                        //load the multiple record control

                        //GridView
                        ProductGridViewV2.DataSource = info;
                        ProductGridViewV2.DataBind();

                    }
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
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
            Response.Redirect("CRUDPage.aspx?pid=" + productid);
        }
    }
}