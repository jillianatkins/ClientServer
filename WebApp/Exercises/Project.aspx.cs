﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DBSystem.BLL;
using DBSystem.ENTITIES;

namespace WebApp.Exercises
{
    public partial class Project : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //MessageLabel.Text = "";
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
                Fetch02.Enabled = false;
                List02.Enabled = false;
                SchoolsController sysmgr = new SchoolsController();
                List<Schools> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.SchoolName.CompareTo(y.SchoolName));
                List01.DataSource = info;
                List01.DataTextField = nameof(Schools.SchoolName);
                List01.DataValueField = nameof(Schools.SchoolCode);
                List01.DataBind();
                List01.Items.Insert(0, "select...");
            }
            catch (Exception ex)
            {
                ShowMessage(ex.ToString(), "alert alert-danger");
            }
        }
        protected void Fetch_Click01(object sender, EventArgs e)
        {
            if (List01.SelectedIndex == 0)
            {
                ShowMessage("Select a school to view its programs", "alert alert-warning");
            }
            else
            {
                ShowMessage("", "");
                try
                {
                    ProgramsController sysmgr02 = new ProgramsController();
                    List<Programs> info02 = null;
                    info02 = sysmgr02.FindByID(List01.SelectedValue);
                    info02.Sort((x, y) => x.ProgramName.CompareTo(y.ProgramName));
                    Fetch02.Enabled = true;
                    List02.Enabled = true;
                    List02.DataSource = info02;
                    List02.DataTextField = nameof(Programs.ProgramName);
                    List02.DataValueField = nameof(Programs.ProgramID);
                    List02.DataBind();
                    List02.Items.Insert(0, "select...");

                }
                catch (Exception ex)
                {
                    ShowMessage(ex.ToString(), "alert alert-danger");
                }
            }
        }
        protected void Fetch_Click02(object sender, EventArgs e)
        {
            if (List02.SelectedIndex == 0)
            {
                ShowMessage("Select a program", "alert alert-warning");
            }
            else
            {
                try
                {
                    string productid = List02.SelectedValue;
                    Response.Redirect("ProjectCRUD.aspx?page=70&pid=" + productid);
                }
                catch (Exception ex)
                {
                    ShowMessage(ex.ToString(), "alert alert-danger");
                }
            }
        }
    }
}