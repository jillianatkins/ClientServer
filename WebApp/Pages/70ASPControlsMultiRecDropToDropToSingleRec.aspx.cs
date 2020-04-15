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
                Controller01 sysmgr = new Controller01();
                List<Entity01> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.CategoryName.CompareTo(y.CategoryName));
                List01.DataSource = info;
                List01.DataTextField = nameof(Entity01.CategoryName);
                List01.DataValueField = nameof(Entity01.CategoryID);
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
                    Controller02 sysmgr02 = new Controller02();
                    List<Entity02> info02 = null;
                    info02 = sysmgr02.FindByID(int.Parse(List01.SelectedValue));
                    //info02 = sysmgr02.List();
                    info02.Sort((x, y) => x.ProductName.CompareTo(y.ProductName));
                    Fetch02.Enabled = true;
                    List02.Enabled = true;
                    List02.DataSource = info02;
                    List02.DataTextField = nameof(Entity02.ProductandID);
                    List02.DataValueField = nameof(Entity02.ProductID);
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
                    Response.Redirect("CRUDPage.aspx?pid=" + productid);
                }
                catch (Exception ex)
                {
                    MessageLabel.Text = ex.Message;
                }
            }
        }
    }
}