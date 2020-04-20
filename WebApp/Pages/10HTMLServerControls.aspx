<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="10HTMLServerControls.aspx.cs" Inherits="WebApp.Pages._10HTMLServerControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%@ Import Namespace="System.Data" %>
    <%@ Import Namespace="System.Data.SqlClient" %>

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head>
        <script src="~/Scripts/jquery-3.4.1.js"></script>
        <link href="~/Content/bootstrap.css" rel="stylesheet" />
        <script src="~/Scripts/bootstrap.js"></script>
        <title>HTML Server Controls CodeBehindNo</title>
        <script runat="server">
            void Page_Load(Object sender, EventArgs e)
            {
                Label2.InnerHtml = "";
                if (!IsPostBack)
                {
                    Label2.InnerHtml += "IsPostBack = False";
                    string ConnectString = "server=localhost;database=Northwind_CPSC1517;integrated security=SSPI";
                    string QueryString = "select * from Products";
                    SqlConnection myConnection = new SqlConnection(ConnectString);
                    SqlDataAdapter myCommand = new SqlDataAdapter(QueryString, myConnection);
                    DataSet ds = new DataSet();
                    myCommand.Fill(ds, "Products");
                    Select1.DataSource = ds;
                    Select1.DataTextField = "ProductName";
                    Select1.DataValueField = "ProductID";
                    Select1.DataBind();
                    ListItem myitem = new ListItem();
                    myitem.Value = "0";
                    myitem.Text = "select...";
                    Select1.Items.Insert(0, myitem);

                    var numrows = ds.Tables[0].Rows.Count;
                    var numcols = ds.Tables[0].Columns.Count;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<h1>Testing</h1>");
                    var temp = label1.Parent;
                    HtmlTable mytable = new HtmlTable();
                    
                    mytable.Attributes.Add("class", "table table-striped");
                    mytable.Attributes.Add("cellspacing", "0");
                    mytable.Attributes.Add("border", "1");
                    mytable.Attributes.Add("rules", "rows");
                    mytable.Attributes.Add("style", "border-style:none; border-collapse:collapse");
                    
                    //mytable.a
                    temp.Controls.Add(mytable);

                    for (int i = 0; i < numrows; i++)
                    {
                        HtmlTableRow tr = new HtmlTableRow();
                        
                        for (int j = 0; j < numcols; j++)
                        {
                            HtmlTableCell tc = new HtmlTableCell();
                            TextBox txtBox = new TextBox();
                            tc.InnerText = ds.Tables[0].Rows[i][j].ToString();
                            //txtBox.Text = "RowNo:" + i + " " + "ColumnNo:" + " " + j;
                            // Add the control to the TableCell
                            //tc.Controls.Add(txtBox);
                            // Add the TableCell to the TableRow
                            tr.Cells.Add(tc);
                        }
                        //Add the TableRow to the Table
                        mytable.Rows.Add(tr);
                    }

                    //Table1.DataSourceControl = ds;

                }
                else
                {
                    Label2.InnerHtml += "IsPostBack = True";
                }
            }
            void Button_Click(Object sender, EventArgs e)
            {
                Label2.InnerHtml += "<br /> You selected:";
                Label2.InnerHtml += "<br /> &nbsp;&nbsp; SelectedIndex = "
                                    + Select1.SelectedIndex;
                Label2.InnerHtml += "<br /> &nbsp;&nbsp; SelectedValue = "
                                    + Select1.Value;


                for (int i = 0; i <= Select1.Items.Count - 1; i++)
                {
                    if (Select1.Items[i].Selected)
                    {
                        Label2.InnerHtml += "<br /> &nbsp;&nbsp; Item Text = "
                                    + Select1.Items[i].Text;
                        Label2.InnerHtml += "<br /> &nbsp;&nbsp; Item Value = "
                                    + Select1.Items[i].Value;
                    }
                }
            }
        </script>
    </head>
    <body id="Body1">
        <Label id="label1" 
            runat="server"/>
        <h3>HtmlSelect Example </h3>

        Select item from the list.
        <br />
        <br />
        
        <select id="Select1"
            multiple="false"
            runat="server" required/>
        <br />
        <br />
        <input type="button" 
            onserverclick="Button_Click" 
            runat="server" 
            value="Click Me!">
        <br />
        <br />
        <label ID="Label2"
            runat="server" />
        <br />
        <br />
        <table id="Table1" 
            runat="server"/>
    </body>
    </html>
</asp:Content>
