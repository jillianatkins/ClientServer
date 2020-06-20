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
        protected void Page_Load(object sender, EventArgs e)
        {
            
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
                    Discontinued.Enabled = false;
                }
                else
                {
                    AddButton.Enabled = false;
                    ProductController sysmgr = new ProductController();
                    Product info = null;
                    info = sysmgr.FindByPKID(int.Parse(pid));
                    if (info == null)
                    {
                        ShowMessage("Record is not in Database.", "alert alert-info");
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
        protected void ShowMessage(string message, string cssclass)
        {
            MessageLabel.Attributes.Add("class", cssclass);
            MessageLabel.InnerHtml = message;
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
                ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
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
                ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
            }
        }
        protected bool Validation(object sender, EventArgs e)
        {
            double unitprice = 0;
            if (string.IsNullOrEmpty(Name.Text))
            {
                ShowMessage("Name is required", "alert alert-info");
                return false;
            }
            else if (CategoryList.SelectedValue == "0")
            {
                ShowMessage("Category is required", "alert alert-info");
                return false;
            }
            else if (string.IsNullOrEmpty(UnitPrice.Text))
            {
                ShowMessage("Unit Price is required", "alert alert-info");
                return false;
            }
            else if (double.TryParse(UnitPrice.Text, out unitprice))
            {
                if (unitprice < 0.00 || unitprice > 200.00)
                {
                    ShowMessage("Unit Price must be between $0.00 and $200.00", "alert alert-info");
                    return false;
                }
            }
            else
            {
                ShowMessage("Unit Price must be a real number", "alert alert-info");
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
            var isValid = Validation(sender, e);
            if (isValid)
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
                    ShowMessage("Record has been ADDED", "alert alert-success");
                    AddButton.Enabled = false;
                    UpdateButton.Enabled = true;
                    DeleteButton.Enabled = true;
                    Discontinued.Enabled = true;
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
                    ProductController sysmgr = new ProductController();
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
                    Discontinued.Enabled = false;
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