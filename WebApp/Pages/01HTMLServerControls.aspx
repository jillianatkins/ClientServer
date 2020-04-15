<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="01HTMLServerControls.aspx.cs" Inherits="WebApp.Pages._01HTMLServerControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%@ Import Namespace="System.Data" %>
    <%@ Import Namespace="System.Data.SqlClient" %>

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head>
        <title>HTML Server Controls without CodeBehind</title>
        <script runat="server">
            void Page_Load(Object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
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
                }
            }
            void Button_Click(Object sender, EventArgs e)
            {
                Label1.Text = "You selected:";

                for (int i = 0; i <= Select1.Items.Count - 1; i++)
                {
                    if (Select1.Items[i].Selected)
                    {
                        Label1.Text += "<br /> &nbsp;&nbsp; - "
                                    + Select1.Items[i].Text;
                        Label1.Text += "<br /> &nbsp;&nbsp; ID = "
                                    + Select1.Items[i].Value;
                    }
                }
            }
        </script>
    </head>
    <body>
        <h3>HtmlSelect Example </h3>

        Select items from the list.
        <br />
        Use the Control or Shift key to select multiple items.
        <br />
        <br />

        <select id="Select1"
            multiple="false"
            runat="server" />
        <br />
        <br />
        <button id="Button1"
            onserverclick="Button_Click"
            runat="server">
            Submit
        </button>
        <br />
        <br />
        <asp:Label ID="Label1"
            runat="server" />
    </body>
    </html>
</asp:Content>
