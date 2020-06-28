using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using DBSystem.BLL;
using DBSystem.ENTITIES;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;

namespace WebApp.Exercises
{
    public partial class Exercise08CRUD : System.Web.UI.Page
    {
        static string pagenum = "";
        static string pid = "";
        static string add = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                pagenum = Request.QueryString["page"];
                pid = Request.QueryString["pid"];
                add = Request.QueryString["add"];
                BindTeamList();
                BindGuardianList();
                if (string.IsNullOrEmpty(pid))
                {
                    Response.Redirect("~/Default.aspx");
                }
                else if (add == "yes")
                {
                    UpdateButton.Enabled = false;
                    DeleteButton.Enabled = false;

                }
                else
                {
                    AddButton.Enabled = false;
                    PlayerController sysmgr = new PlayerController();
                    Player info = null;
                    info = sysmgr.FindPlayer(int.Parse(pid));
                    if (info == null)
                    {
                        ShowMessage("Record is not in Database.", "alert alert-info");
                        Clear(sender, e);
                    }
                    else
                    {
                        ID.Text = info.PlayerID.ToString(); //NOT NULL in Database
                        FirstName.Text = info.FirstName; //NOT NULL in Database
                        LastName.Text = info.LastName; //NOT NULL in Database
                        Age.Text = info.Age.ToString(); // NOT NULL in Database
                        Gender.Text = info.Gender; // NOT NULL in Database
                        ABHealth.Text = info.AlbertaHealthCareNumber; // NOT NULL in Database
                        TeamList.SelectedValue = info.TeamID.ToString(); // NOT NULL in Database
                        GuardianList.SelectedValue = info.GuardianID.ToString(); // NOT NULL in Database

                        if (MedicalAlert.Text != null) // NULL in Database
                        {
                            MedicalAlert.Text = info.MedicalAlertDetails;
                        }
                        else
                        {
                            info.MedicalAlertDetails = "";
                        }

                    }
                }
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
            MessageLabel.Attributes.Add("class", cssclass);
            MessageLabel.InnerHtml = message;
        }
        protected void BindTeamList()
        {
            try
            {
                TeamController sysmgr = new TeamController();
                List<Team> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.TeamName.CompareTo(y.TeamName));
                TeamList.DataSource = info;
                TeamList.DataTextField = nameof(Team.TeamName);
                TeamList.DataValueField = nameof(Team.TeamID);
                TeamList.DataBind();
                ListItem myitem = new ListItem();
                myitem.Value = "0";
                myitem.Text = "select...";
                TeamList.Items.Insert(0, myitem);
                //CategoryList.Items.Insert(0, "select...");

            }
            catch (Exception ex)
            {
                ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
            }
        }
        protected void BindGuardianList()
        {
            try
            {
                GuardianController sysmgr = new GuardianController();
                List<Guardian> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.LastName.CompareTo(y.LastName));
                GuardianList.DataSource = info;
                GuardianList.DataTextField = nameof(Guardian.GuardianConcat);
                GuardianList.DataValueField = nameof(Guardian.GuardianID);
                GuardianList.DataBind();
                ListItem myitem = new ListItem();
                myitem.Value = "0";
                myitem.Text = "select...";
                GuardianList.Items.Insert(0, myitem);
                //SupplierList.Items.Insert(0, "select...");

            }
            catch (Exception ex)
            {
                ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
            }
        }
        protected bool Validation(object sender, EventArgs e)
        {
            int age = 0;
            if (string.IsNullOrEmpty(FirstName.Text))
            {
                ShowMessage("First Name is required", "alert alert-warning");
                return false;
            }

            if (string.IsNullOrEmpty(LastName.Text))
            {
                ShowMessage("Last Name is required", "alert alert-alert-warning");
                return false;
            }

            if (string.IsNullOrEmpty(Age.Text))
            {
                ShowMessage("Age is required", "alert alert-alert-warning");
                return false;
            }

            if (int.TryParse(Age.Text, out age))
            {
                if (age < 6 || age > 14)
                {
                    ShowMessage("Player's age must be between 6 and 14", "alert alert-warning");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(Gender.Text))
            {
                ShowMessage("Gender is required", "alert alert-warning");
                return false;
            }

            if (Gender.Text != "M" && Gender.Text != "F")
            {
                ShowMessage("Gender must be either M or F", "alert alert-warning");
                return false;
            }

            if (string.IsNullOrEmpty(ABHealth.Text))
            {
                ShowMessage("Alberta Health Care Number is required", "alert alert-warning");
                return false;
            }

            string input1 = ABHealth.Text;
            Match match1 = Regex.Match(input1, @"^[1-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$");

            if (!match1.Success)
            {
                ShowMessage("Alberta Health Care Number must 10 digits long and must be like 1012345678", "alert alert-warning");
                return false;
            }

            if (TeamList.SelectedValue == "0")
            {
                ShowMessage("Team is required", "alert alert-warning");
                return false;
            }
            
            if (GuardianList.SelectedValue == "0")
            {
                ShowMessage("Guardian is required", "alert alert-warning");
                return false;
            }

            return true;
        }
        protected void Back_Click(object sender, EventArgs e)
        {
            if (pagenum == "50")
            {
                Response.Redirect("Exercise08.aspx");
            }
            else if (pagenum == "60")
            {
                Response.Redirect("60ASPControlsMultiRecDropToCustGridViewToSingleRec.aspx");
            }
            else if (pagenum == "70")
            {
                Response.Redirect("70ASPControlsMultiRecDropToDropToSingleRec.aspx");
            }
            else if (pagenum == "80")
            {
                Response.Redirect("80ASPControlsPartialStringSearchToCustGridViewToSingleRec.aspx");
            }
            else if (pagenum == "90")
            {
                Response.Redirect("90ASPControlsPartialStringSearchToDropToSingleRec.aspx");
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        protected void Clear(object sender, EventArgs e)
        {
            ID.Text = "";
            FirstName.Text = "";
            LastName.Text = "";
            Age.Text = "";
            Gender.Text = "";
            ABHealth.Text = "";
            TeamList.ClearSelection();
            GuardianList.ClearSelection();
            MedicalAlert.Text = "";
        }
        protected void Add_Click(object sender, EventArgs e)
        {
            var isValid = Validation(sender, e);
            if (isValid)
            {
                try
                {
                    PlayerController sysmgr = new PlayerController();
                    Player item = new Player();
                    //No PlayerID here as the database will give a new one back when we add
                    item.FirstName = FirstName.Text.Trim(); //NOT NULL in Database
                    item.LastName = LastName.Text.Trim(); // NOT NULL in Database
                    item.Age = int.Parse(Age.Text);
                    item.Gender = Gender.Text;
                    item.AlbertaHealthCareNumber = ABHealth.Text;
                    item.TeamID = int.Parse(TeamList.SelectedValue);
                    item.GuardianID = int.Parse(GuardianList.SelectedValue);

                    

                    if (string.IsNullOrEmpty(MedicalAlert.Text))
                    {
                        item.MedicalAlertDetails = null;
                    }

                    else
                    {
                        item.MedicalAlertDetails = MedicalAlert.Text;
                    }

                    int newID = sysmgr.AddPlayer(item);
                    ID.Text = newID.ToString();
                    ShowMessage("Record has been ADDED", "alert alert-success");
                    AddButton.Enabled = false;
                    UpdateButton.Enabled = true;
                    DeleteButton.Enabled = true;
                }
                catch (Exception ex)
                {
                    ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
                }
            }
        }
        protected void Update_Click(object sender, EventArgs e)
        {
            var isValid = Validation(sender, e);
            if (isValid)
            {
                try
                {
                    PlayerController sysmgr = new PlayerController();
                    Player item = new Player();
                    item.PlayerID = int.Parse(ID.Text);
                    item.FirstName = FirstName.Text.Trim(); //NOT NULL in Database
                    item.LastName = LastName.Text.Trim(); // NOT NULL in Database
                    item.Age = int.Parse(Age.Text);
                    item.Gender = Gender.Text;
                    item.AlbertaHealthCareNumber = ABHealth.Text;
                    item.TeamID = int.Parse(TeamList.SelectedValue);
                    item.GuardianID = int.Parse(GuardianList.SelectedValue);

                    if (string.IsNullOrEmpty(MedicalAlert.Text))
                    {
                        item.MedicalAlertDetails = null;
                    }

                    else
                    {
                        item.MedicalAlertDetails = MedicalAlert.Text;
                    }

                    int rowsaffected = sysmgr.PlayerUpdate(item);
                    if (rowsaffected > 0)
                    {
                        ShowMessage("Record has been UPDATED", "alert alert-success");
                    }
                    else
                    {
                        ShowMessage("Record was not found", "alert alert-warning");
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
                }
            }
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            var isValid = true;
            if (isValid)
            {
                try
                {
                    PlayerController sysmgr = new PlayerController();
                    int rowsaffected = sysmgr.PlayerDelete(int.Parse(ID.Text));
                    if (rowsaffected > 0)
                    {
                        ShowMessage("Record has been DELETED", "alert alert-success");
                        Clear(sender, e);
                    }
                    else
                    {
                        ShowMessage("Record was not found", "alert alert-warning");
                    }
                    UpdateButton.Enabled = false;
                    DeleteButton.Enabled = false;
                    AddButton.Enabled = true;
                }
                catch (Exception ex)
                {
                    ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
                }
            }
        }
    }
}