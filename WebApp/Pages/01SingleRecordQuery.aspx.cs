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
    public partial class _01SingleRecordQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = "";
            ID.Text = "";
            Name.Text = "";
        }

        protected void Fetch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IDArg.Text))
            {
                MessageLabel.Text = "Enter a ID value.";
                ID.Text = "";
                Name.Text = "";
            }
            else
            {
                int id = 0;
                if (int.TryParse(IDArg.Text, out id))
                {
                    if (id > 0)
                    {
                        Controller01 sysmgr = new Controller01();
                        Entity01 info = null;
                        info = sysmgr.FindByPKID(id); //BLL controller method
                        if (info == null)
                        {
                            MessageLabel.Text = "ID not found.";
                            ID.Text = "";
                            Name.Text = "";
                        }
                        else
                        {
                            ID.Text = info.CategoryID.ToString();
                            Name.Text = info.CategoryName;
                        }
                    }
                    else
                    {
                        MessageLabel.Text = "ID must be greater than 0";
                        ID.Text = "";
                        Name.Text = "";
                    }

                }
                else
                {
                    MessageLabel.Text = "ID must be a number.";
                    ID.Text = "";
                    Name.Text = "";
                }
            }
        }
    }
}