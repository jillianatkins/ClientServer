using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DBSystem.BLL;
using DBSystem.ENTITIES;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;

namespace WebApp.Exercises
{
    public partial class ProjectCRUD : System.Web.UI.Page
    {
        static string pagenum = "";
        static string pid = "";
        //static string add = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                pagenum = Request.QueryString["page"];
                pid = Request.QueryString["pid"];
                //add = Request.QueryString["add"];
                BindSchoolsList();
                if (string.IsNullOrEmpty(pid))
                {
                    Response.Redirect("~/Default.aspx");
                }
                //else if (add == "yes")
                //{
                //    UpdateButton.Enabled = false;
                //    DeleteButton.Enabled = false;
                //}
                else
                {

                    ProgramsController sysmgr = new ProgramsController();
                    Programs info = null;
                    info = sysmgr.FindByPKID(int.Parse(pid));
                    if (info == null)
                    {
                        ShowMessage("Record is not in Database.", "alert alert-info");
                        Clear(sender, e);
                    }
                    else
                    {
                        ID.Text = info.ProgramID.ToString(); //NOT NULL in Database
                        Name.Text = info.ProgramName; //NOT NULL in Database
                        SchoolsList.SelectedValue = info.SchoolCode.ToString();
                        Tuition.Text = string.Format("{0:0.00}", info.Tuition);
                        IntTuition.Text = string.Format("{0:0.00}", info.InternationalTuition);

                        if (info.DiplomaName != null) //NULL in Database
                        {
                            Diploma.Text = info.DiplomaName;
                        }
                        else
                        {
                            Diploma.Text = "";
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
        protected void BindSchoolsList()
        {
            try
            {
                SchoolsController sysmgr = new SchoolsController();
                List<Schools> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.SchoolName.CompareTo(y.SchoolName));
                SchoolsList.DataSource = info;
                SchoolsList.DataTextField = nameof(Schools.SchoolName);
                SchoolsList.DataValueField = nameof(Schools.SchoolCode);
                SchoolsList.DataBind();
                ListItem myitem = new ListItem();
                myitem.Value = "0";
                myitem.Text = "select...";
                SchoolsList.Items.Insert(0, myitem);
                //CategoryList.Items.Insert(0, "select...");

            }
            catch (Exception ex)
            {
                ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
            }
        }
        
        protected bool Validation(object sender, EventArgs e)
        {
            double tuition = 0, inttuition = 0;

            if (string.IsNullOrEmpty(Name.Text))
            {
                ShowMessage("Program Name is required", "alert alert-warning");
                return false;
            }
            else if (SchoolsList.SelectedValue == "0")
            {
                ShowMessage("School is required", "alert alert-warning");
                return false;
            }
            else if (string.IsNullOrEmpty(Tuition.Text))
            {
                ShowMessage("Tuition cost is required", "alert alert-warning");
                return false;
            }
            else if (double.TryParse(Tuition.Text, out tuition))
            {
                if (tuition < 0.00 || tuition > 7200.00)
                {
                    ShowMessage("Tuition cost must be between $0.00 and $7200.00", "alert alert-warning");
                    return false;
                }
            }
            else if (string.IsNullOrEmpty(IntTuition.Text))
            {
                ShowMessage("International Tuition cost is required", "alert alert-warning");
                return false;
            }

            else if (double.TryParse(IntTuition.Text, out inttuition))
            {
                if (inttuition < 0.00 || inttuition > 12000.0)
                {
                    ShowMessage("International Tuition cost must be between $0.00 and $12,000.00", "alert alert-warning");
                    return false;
                }
            }
            else
            {
                ShowMessage("Tuition must be a real number", "alert alert-warning");
                return false;
            }
            return true;
        }
        protected void Back_Click(object sender, EventArgs e)
        {
            if (pagenum == "50")
            {
                Response.Redirect("50ASPControlsMultiRecordDropdownToSingleRecord.aspx");
            }
            else if (pagenum == "60")
            {
                Response.Redirect("60ASPControlsMultiRecDropToCustGridViewToSingleRec.aspx");
            }
            else if (pagenum == "70")
            {
                Response.Redirect("Project.aspx");
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
            Name.Text = "";
            Diploma.Text = "";
            Tuition.Text = "";
            IntTuition.Text = "";
            SchoolsList.ClearSelection();
        }
        //protected void Add_Click(object sender, EventArgs e)
        //{
        //    var isValid = Validation(sender, e);
        //    if (isValid)
        //    {
        //        try
        //        {
        //            ProductController sysmgr = new ProductController();
        //            Product item = new Product();
        //            //No ProductID here as the database will give a new one back when we add
        //            item.ProductName = Name.Text.Trim(); //NOT NULL in Database
        //            if (SupplierList.SelectedValue == "0") //NULL in Database
        //            {
        //                item.SupplierID = null;
        //            }
        //            else
        //            {
        //                item.SupplierID = int.Parse(SupplierList.SelectedValue);
        //            }
        //            //CategoryID can be NULL in Database but NOT NULL when record is added in this CRUD page
        //            item.CategoryID = int.Parse(CategoryList.SelectedValue);
        //            //UnitPrice can be NULL in Database but NOT NULL when record is added in this CRUD page
        //            item.UnitPrice = decimal.Parse(UnitPrice.Text);
        //            item.Discontinued = Discontinued.Checked; //NOT NULL in Database
        //            int newID = sysmgr.Add(item);
        //            ID.Text = newID.ToString();
        //            ShowMessage("Record has been ADDED", "alert alert-success");
        //            AddButton.Enabled = false;
        //            UpdateButton.Enabled = true;
        //            DeleteButton.Enabled = true;
        //            Discontinued.Enabled = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
        //        }
        //    }
        //}
        protected void Update_Click(object sender, EventArgs e)
        {
            var isValid = Validation(sender, e);
            if (isValid)
            {
                try
                {
                    ProgramsController sysmgr = new ProgramsController();
                    Programs item = new Programs();
                    item.ProgramID = int.Parse(ID.Text);
                    item.ProgramName = Name.Text.Trim();
                    item.DiplomaName = Diploma.Text.Trim();
                    item.SchoolCode = SchoolsList.SelectedValue;
                    item.Tuition = decimal.Parse(Tuition.Text);
                    item.InternationalTuition = decimal.Parse(IntTuition.Text);
                    
                    int rowsaffected = sysmgr.Update(item);
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
                    ProgramsController sysmgr = new ProgramsController();
                    int rowsaffected = sysmgr.Delete(int.Parse(ID.Text));
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
                }
                catch (Exception ex)
                {
                    ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
                }
            }
        }
    }
}