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

namespace WebApp.Pages
{
    public partial class _94CRUDPageNW : System.Web.UI.Page
    {
        static string pagenum = "";
        static string pid = "";
        static string add = "";
        List<string> errormsgs = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            errormsgs.Clear();
            Message.DataSource = null;
            Message.DataBind();
            if (!Page.IsPostBack)
            {
                pagenum = Request.QueryString["page"];
                pid = Request.QueryString["pid"];
                add = Request.QueryString["add"];
                BindCategoryList();
                BindSupplierList();
                if (string.IsNullOrEmpty(pid))
                {
                    Response.Redirect("~/Default.aspx");
                }
                else if(add == "yes")
                {
                    UpdateButton.Enabled = false;
                    DeleteButton.Enabled = false;
                }
                else
                {
                    AddButton.Enabled = false;
                    ProductController sysmgr = new ProductController();
                    Product info = null;
                    info = sysmgr.FindByPKID(int.Parse(pid));
                    if (info == null)
                    {
                        errormsgs.Add("Record is not in Database.");
                        LoadMessageDisplay(errormsgs, "alert alert-info");
                        Clear(sender, e);
                    }
                    else
                    {
                        ID.Text = info.ProductID.ToString(); //NOT NULL in Database
                        Name.Text = info.ProductName; //NOT NULL in Database
                        if (info.CategoryID.HasValue) //NULL in Database
                        {
                            CategoryList.SelectedValue = info.CategoryID.ToString();
                        }
                        else
                        {
                            CategoryList.SelectedValue = "0";
                        }
                        if (info.SupplierID.HasValue) //NULL in Database
                        {
                            SupplierList.SelectedValue = info.SupplierID.ToString();
                        }
                        else
                        {
                            SupplierList.SelectedValue = "0";
                        }
                        if (info.UnitPrice.HasValue) //NULL in Database
                        {
                            UnitPrice.Text = string.Format("{0:0.00}", info.UnitPrice.Value);
                        }
                        else
                        {
                            UnitPrice.Text = "";
                        }
                        Discontinued.Checked = info.Discontinued; //NOT NULL in Database
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
        protected void LoadMessageDisplay(List<string> errormsglist, string cssclass)
        {
            Message.CssClass = cssclass;
            Message.DataSource = errormsglist;
            Message.DataBind();
        }
        protected void BindCategoryList()
        {
            try
            {
                CategoryController sysmgr = new CategoryController();
                List<Category> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.CategoryName.CompareTo(y.CategoryName));
                CategoryList.DataSource = info;
                CategoryList.DataTextField = nameof(Category.CategoryName);
                CategoryList.DataValueField = nameof(Category.CategoryID);
                CategoryList.DataBind();
                ListItem myitem = new ListItem();
                myitem.Value = "0";
                myitem.Text = "select...";
                CategoryList.Items.Insert(0, myitem);
                //CategoryList.Items.Insert(0, "select...");

            }
            catch (Exception ex)
            {
                errormsgs.Add(GetInnerException(ex).ToString());
                LoadMessageDisplay(errormsgs, "alert alert-danger");
            }
        }
        protected void BindSupplierList()
        {
            try
            {
                SupplierController sysmgr = new SupplierController();
                List<Supplier> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.ContactName.CompareTo(y.ContactName));
                SupplierList.DataSource = info;
                SupplierList.DataTextField = nameof(Supplier.ContactName);
                SupplierList.DataValueField = nameof(Supplier.SupplierID);
                SupplierList.DataBind();
                ListItem myitem = new ListItem();
                myitem.Value = "0";
                myitem.Text = "select...";
                SupplierList.Items.Insert(0, myitem);
                //SupplierList.Items.Insert(0, "select...");

            }
            catch (Exception ex)
            {
                errormsgs.Add(GetInnerException(ex).ToString());
                LoadMessageDisplay(errormsgs, "alert alert-danger");
            }
        }
        protected void Validation(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Name.Text))
            {
                errormsgs.Add("Name is required");
            }
            if (CategoryList.SelectedValue == "0")
            {
                errormsgs.Add("Category is required");
            }
            double unitprice = 0;
            if (string.IsNullOrEmpty(UnitPrice.Text))
            {
                errormsgs.Add("Unit Price is required");
            }
            else
            {
                if (double.TryParse(UnitPrice.Text, out unitprice))
                {
                    if (unitprice < 0.00 || unitprice > 200.00)
                    {
                        errormsgs.Add("Unit Price must be between $0.00 and $200.00");
                    }
                }
                else
                {
                    errormsgs.Add("Unit Price must be a real number");
                }
            }
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
            Name.Text = "";
            UnitPrice.Text = "";
            Discontinued.Checked = false;
            CategoryList.ClearSelection();
            SupplierList.ClearSelection();
        }
        protected void Add_Click(object sender, EventArgs e)
        {
            Validation(sender, e);
            if (errormsgs.Count > 0)
            {
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                try
                {
                    ProductController sysmgr = new ProductController();
                    Product item = new Product();
                    //No ProductID here as the database will give a new one back when we add
                    item.ProductName = Name.Text.Trim(); //NOT NULL in Database
                    if (SupplierList.SelectedValue == "0") //NULL in Database
                    {
                        item.SupplierID = null;
                    }
                    else
                    {
                        item.SupplierID = int.Parse(SupplierList.SelectedValue);
                    }
                    //CategoryID can be NULL in Database but NOT NULL when record is added in this CRUD page
                    item.CategoryID = int.Parse(CategoryList.SelectedValue);
                    //UnitPrice can be NULL in Database but NOT NULL when record is added in this CRUD page
                    item.UnitPrice = decimal.Parse(UnitPrice.Text);
                    item.Discontinued = Discontinued.Checked; //NOT NULL in Database
                    int newID = sysmgr.Add(item); 
                    ID.Text = newID.ToString();
                    errormsgs.Add("Record has been ADDED");
                    LoadMessageDisplay(errormsgs, "alert alert-success");
                    UpdateButton.Enabled = true;
                    DeleteButton.Enabled = true;
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
            }
        }
        protected void Update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ID.Text))
            {
                errormsgs.Add("Search for a record to UPDATE");
            }
            Validation(sender, e);
            if (errormsgs.Count > 0)
            {
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                try
                {
                    ProductController sysmgr = new ProductController();
                    Product item = new Product();
                    item.ProductID = int.Parse(ID.Text);
                    item.ProductName = Name.Text.Trim();
                    if (SupplierList.SelectedValue == "0")
                    {
                        item.SupplierID = null;
                    }
                    else
                    {
                        item.SupplierID = int.Parse(SupplierList.SelectedValue);
                    }
                    item.CategoryID = int.Parse(CategoryList.SelectedValue);
                    item.UnitPrice = decimal.Parse(UnitPrice.Text);
                    item.Discontinued = Discontinued.Checked;
                    int rowsaffected = sysmgr.Update(item);
                    if (rowsaffected > 0)
                    {
                        errormsgs.Add("Record has been UPDATED");
                        LoadMessageDisplay(errormsgs, "alert alert-success");
                    }
                    else
                    {
                        errormsgs.Add("Record was not found");
                        LoadMessageDisplay(errormsgs, "alert alert-warning");
                    }
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
            }
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ID.Text))
            {
                errormsgs.Add("Search for a record to DELETE");
            }
            if (errormsgs.Count > 0)
            {
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                try
                {
                    ProductController sysmgr = new ProductController();
                    int rowsaffected = sysmgr.Delete(int.Parse(ID.Text));
                    if (rowsaffected > 0)
                    {
                        errormsgs.Add("Record has been DELETED");
                        LoadMessageDisplay(errormsgs, "alert alert-success");
                        Clear(sender, e);
                    }
                    else
                    {
                        errormsgs.Add("Record was not found");
                        LoadMessageDisplay(errormsgs, "alert alert-warning");
                    }
                    UpdateButton.Enabled = false;
                    DeleteButton.Enabled = false;
                    AddButton.Enabled = true;

                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
            }
        }
    }
}