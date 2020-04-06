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
    public partial class CRUDPage : System.Web.UI.Page
    {
        static string pagenum = "";
        static string pid = "";
        static string add = "";
        List<string> errormsgs = new List<string>();
        private static List<Entity02> Entity02List = new List<Entity02>();
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    Discontinued.Enabled = false;
                }
                else
                {
                    Controller02 sysmgr = new Controller02();
                    Entity02 info = null;
                    info = sysmgr.FindByPKID(int.Parse(pid));
                    if (info == null)
                    {
                        errormsgs.Add("Product is no longer on file.");
                        LoadMessageDisplay(errormsgs, "alert alert-info");
                        Clear_Click(sender, e);
                    }
                    else
                    {
                        ID.Text = info.ProductID.ToString();
                        Name.Text = info.ProductName;
                        QuantityPerUnit.Text =
                            info.QuantityPerUnit == null ? "" : info.QuantityPerUnit;
                        UnitPrice.Text =
                            info.UnitPrice.HasValue ? string.Format("{0:0.00}", info.UnitPrice.Value) : "";
                        UnitsInStock.Text =
                            info.UnitsInStock.HasValue ? info.UnitsInStock.Value.ToString() : "";
                        UnitsOnOrder.Text =
                            info.UnitsOnOrder.HasValue ? info.UnitsOnOrder.Value.ToString() : "";
                        ReorderLevel.Text =
                            info.ReorderLevel.HasValue ? info.ReorderLevel.Value.ToString() : "";
                        Discontinued.Checked = info.Discontinued;
                        if (info.CategoryID.HasValue)
                        {
                            CategoryList.SelectedValue = info.CategoryID.ToString();
                        }
                        else
                        {
                            CategoryList.SelectedIndex = 0;
                        }
                        if (info.SupplierID.HasValue)
                        {
                            SupplierList.SelectedValue = info.SupplierID.ToString();
                        }
                        else
                        {
                            SupplierList.SelectedIndex = 0;
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
                Controller01 sysmgr = new Controller01();
                List<Entity01> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.CategoryName.CompareTo(y.CategoryName));
                CategoryList.DataSource = info;
                CategoryList.DataTextField = nameof(Entity01.CategoryName);
                CategoryList.DataValueField = nameof(Entity01.CategoryID);
                CategoryList.DataBind();
                CategoryList.Items.Insert(0, "select...");

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
                Controller03 sysmgr = new Controller03();
                List<Entity03> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.ContactName.CompareTo(y.ContactName));
                SupplierList.DataSource = info;
                SupplierList.DataTextField = nameof(Entity03.ContactName);
                SupplierList.DataValueField = nameof(Entity03.SupplierID);
                SupplierList.DataBind();
                SupplierList.Items.Insert(0, "select...");

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
                errormsgs.Add("Product Name is required");
            }
            if (CategoryList.SelectedIndex == 0)
            {
                errormsgs.Add("Category is required");
            }
            if (QuantityPerUnit.Text.Length > 20)
            {
                errormsgs.Add("Quantity per Unit is limited to 20 characters");
            }
            double unitprice = 0;
            if (!string.IsNullOrEmpty(UnitPrice.Text))
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
            if (pagenum == "4")
            {
                Response.Redirect("08MultiRecordDropdownToSingleRecord.aspx");
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        protected void Clear_Click(object sender, EventArgs e)
        {
            ID.Text = "";
            Name.Text = "";
            QuantityPerUnit.Text = "";
            UnitPrice.Text = "";
            UnitsInStock.Text = "";
            UnitsOnOrder.Text = "";
            ReorderLevel.Text = "";
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
                    Controller02 sysmgr = new Controller02();
                    Entity02 item = new Entity02();
                    item.ProductName = Name.Text.Trim();
                    if (SupplierList.SelectedIndex == 0)
                    {
                        item.SupplierID = null;
                    }
                    else
                    {
                        item.SupplierID = int.Parse(SupplierList.SelectedValue);
                    }
                    item.QuantityPerUnit =
                        string.IsNullOrEmpty(QuantityPerUnit.Text) ? null : QuantityPerUnit.Text;
                    if (string.IsNullOrEmpty(UnitPrice.Text))
                    {
                        item.UnitPrice = null;
                    }
                    else
                    {
                        item.UnitPrice = decimal.Parse(UnitPrice.Text);
                    }
                    if (string.IsNullOrEmpty(UnitsInStock.Text))
                    {
                        item.UnitsInStock = null;
                    }
                    else
                    {
                        item.UnitsInStock = Int16.Parse(UnitsInStock.Text);
                    }
                    if (string.IsNullOrEmpty(UnitsOnOrder.Text))
                    {
                        item.UnitsOnOrder = null;
                    }
                    else
                    {
                        item.UnitsOnOrder = Int16.Parse(UnitsOnOrder.Text);
                    }
                    if (string.IsNullOrEmpty(ReorderLevel.Text))
                    {
                        item.ReorderLevel = null;
                    }
                    else
                    {
                        item.ReorderLevel = Int16.Parse(ReorderLevel.Text);
                    }
                    item.Discontinued = false;
                    int newID = sysmgr.Add(item);
                    ID.Text = newID.ToString();
                    errormsgs.Add("Product has been added");
                    LoadMessageDisplay(errormsgs, "alert alert-success");
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
            int id = 0;
            if (string.IsNullOrEmpty(ID.Text))
            {
                errormsgs.Add("Search for a product to update");
            }
            else if (!int.TryParse(ID.Text, out id))
            {
                errormsgs.Add("Product id is invalid");
            }
            Validation(sender, e);
            if (errormsgs.Count > 0)
            {
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {

            }
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (string.IsNullOrEmpty(ID.Text))
            {
                errormsgs.Add("Search for a product to delete");
            }
            else if (!int.TryParse(ID.Text, out id))
            {
                errormsgs.Add("Product id is invalid");
            }
            if (errormsgs.Count > 0)
            {
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                try
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallFunction", "CallFunction();", true);
                    Controller02 sysmgr = new Controller02();
                    int rowsaffected = sysmgr.Delete(id);
                    if (rowsaffected > 0)
                    {
                        errormsgs.Add("Product has been deleted");
                        LoadMessageDisplay(errormsgs, "alert alert-success");
                        Clear_Click(sender, e);
                    }
                    else
                    {
                        errormsgs.Add("Product was not found");
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
    }
}