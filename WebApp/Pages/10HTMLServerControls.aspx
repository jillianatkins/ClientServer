<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="10HTMLServerControls.aspx.cs" Inherits="WebApp.Pages._10HTMLServerControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%@ Import Namespace="System.Data" %>
    <%@ Import Namespace="System.Data.SqlClient" %>

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head>
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
    <body>
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
    </body>
    </html>
</asp:Content>
