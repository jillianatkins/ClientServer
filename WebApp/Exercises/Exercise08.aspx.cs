using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBSystem.BLL;
using DBSystem.ENTITIES;

namespace WebApp.Exercises
{
    public partial class Exercise08 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //MessageLabel1.Text = "";
            if (!Page.IsPostBack)
            {
                BindList();
            }
        }

        protected void ShowMessage(string message, string cssclass)
        {
            MessageLabel.Attributes.Add("class", cssclass);
            MessageLabel.InnerHtml = message;
        }
        protected void BindList()
        {
            try
            {
                PlayerController sysmgr = new PlayerController();
                List<Player> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.LastName.CompareTo(y.LastName));
                List01.DataSource = info;
                List01.DataTextField = nameof(Player.NameConcat);
                List01.DataValueField = nameof(Player.PlayerID);
                List01.DataBind();
                List01.Items.Insert(0, "select...");
            }
            catch (Exception ex)
            {

                ShowMessage(ex.ToString(), "alert alert-danger");
            }
        }
        protected void Fetch_Click(object sender, EventArgs e)
        {
            if (List01.SelectedIndex == 0)
            {
                ShowMessage("Select a Player", "alert alert-warning");
            }
            else
            {
                try
                {
                    string playerID = List01.SelectedValue;
                    Response.Redirect("Exercise08CRUD.aspx?page=50&pid=" + playerID + "&add=" + "no");
                }
                catch (Exception ex)
                {
                    ShowMessage(ex.ToString(), "alert alert-danger");
                }
            }
        }
        protected void Add_Click(object sender, EventArgs e)
        {
            try
            {
                string playerID = List01.SelectedValue;
                Response.Redirect("Exercise08CRUD.aspx?page=50&pid=" + playerID + "&add=" + "yes");
            }
            catch (Exception ex)
            {
                ShowMessage(ex.ToString(), "alert alert-danger");
            }
        }
    }
}