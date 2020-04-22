<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="10HTMLServerControls.aspx.cs" Inherits="WebApp.Pages._10HTMLServerControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%@ Import Namespace="System.Data" %>
    <%@ Import Namespace="System.Data.SqlClient" %>
    <%@ Import Namespace="System.Web.UI.HtmlControls" %>

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head>
        <script src="~/Scripts/jquery-3.4.1.js"></script>
        <link href="~/Content/bootstrap.css" rel="stylesheet" />
        <script src="~/Scripts/bootstrap.js"></script>
        <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.css">
  
        <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.js"></script>

        <title>HTML Server Controls CodeBehindNo</title>

    <script type="text/javascript">
        $(document).ready(function () {
            generate_table();
            var table = $('#table5').DataTable({
                "columnDefs": [{
                    "targets": -1,
                    "data": null,
                    "defaultContent": "<button>Click!</button>"
                }]
            });

            $('#table5 tbody').on('click', 'button', function () {
                var data = table.row($(this).parents('tr')).data();
                alert(data[0] + "'s salary is: " + data[0]);
            });
        });


        function generate_table() {
            // get the reference for the body
            var body = document.getElementsByTagName("body")[0];
            // creates a <table> element and a <tbody> element
            var tbl = document.createElement("table");
            var tblHead = document.createElement("thead");
            var row = document.createElement("tr");
            var cell = document.createElement("th");
            var cellText = document.createTextNode("header1");
            cell.appendChild(cellText);
            row.appendChild(cell);
            cell = document.createElement("th");
            cellText = document.createTextNode("header2");
            cell.appendChild(cellText);
            row.appendChild(cell);
            tblHead.appendChild(row);
            var tblBody = document.createElement("tbody");
            // creating all cells
            for (var i = 0; i < 20; i++) {
            // creates a table row
                var row = document.createElement("tr");
                for (var j = 0; j < 2; j++) {
                // Create a <td> element and a text node, make the text
                // node the contents of the <td>, and put the <td> at
                // the end of the table row
                var cell = document.createElement("td");
                var cellText = document.createTextNode("cell in row "+i+", column "+j);
                cell.appendChild(cellText);
                row.appendChild(cell);
                }
            // add the row to the end of the table body
            tblBody.appendChild(row);
            }
            tbl.appendChild(tblHead);
            // put the <tbody> in the <table>
            tbl.appendChild(tblBody);
            // appends <table> into <body>
            body.appendChild(tbl);
            // sets the border attribute of tbl to 2;
            //tbl.setAttribute("border", "2");
            tbl.setAttribute("class", "display");
            tbl.setAttribute("id", "table1");
            var table = $('#table1').DataTable({
                "columnDefs": [{
                    "targets": -1,
                    "data": null,
                    "defaultContent": "<button>Click!</button>"
                }]
            });

            $('#table1 tbody').on('click', 'button', function () {
                var data = table.row($(this).parents('tr')).data();
                alert(data[0] + "'s salary is: " + data[0]);
            });
        }
        </script>

        <script runat="server">
            void Page_Load(Object sender, EventArgs e)
            {

                string fileIndex = "This is fileIndex";
                string FileNames = "FileNames";
                string FileExtensions = "FileExtensions";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "paramFN1", "getFileDetailsToHidden('" + fileIndex + "','" + FileNames + "','" + FileExtensions + "');", true);

                string val = Request.Form["hdnFileIndex"];

                Response.Write("fileIndex's value:  " + val);

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
                    var temp = label1.Parent;
                    HtmlTable mytable = new HtmlTable();
                    mytable.Attributes.Add("class", "display");
                    mytable.Attributes.Add("id", "table_id1");
                    mytable.Attributes.Add("cellspacing", "0");
                    mytable.Attributes.Add("border", "1");
                    mytable.Attributes.Add("rules", "rows");
                    mytable.Attributes.Add("style", "border-style:none; border-collapse:collapse");
                    temp.Controls.Add(mytable);
                    //mytable.InnerHtml += "<thead>";
                    HtmlTableRow trh = new HtmlTableRow();
                    for (int i = 0; i < numcols; i++)
                    {
                        HtmlTableCell tc = new HtmlTableCell();
                        tc.InnerText = ds.Tables[0].Columns[i].ColumnName.ToString();
                        trh.Cells.Add(tc);
                    }
                    mytable.Rows.Add(trh);
                    //mytable.InnerHtml += "</thead>";
                    for (int i = 0; i < numrows; i++)
                    {
                        HtmlTableRow tr = new HtmlTableRow();
                        for (int j = 0; j < numcols; j++)
                        {
                            HtmlTableCell tc = new HtmlTableCell();
                            tc.InnerText = ds.Tables[0].Rows[i][j].ToString();
                            tr.Cells.Add(tc);
                        }
                        mytable.Rows.Add(tr);
                    }


                    var html = "";
                    html += "<table class='display' id='table5'>";
                    html += "<thead>";
                    html += "<tr>";
                    for (int i = 0; i < numcols; i++)
                    {
                        html += "<th>";
                        html += ds.Tables[0].Columns[i].ColumnName.ToString();
                        html += "</th>";
                    }
                    html += "</tr>";
                    html += "</thead>";
                    html += "<tbody>";
                    for (int i = 0; i < numrows; i++)
                    {
                        html += "<tr>";
                        for (int j = 0; j < numcols; j++)
                        {
                            html += "<td>";
                            html += ds.Tables[0].Rows[i][j].ToString();
                            html += "</td>";
                        }
                        html += "</tr>";
                    }
                    html += "</tbody>";
                    html += "</table>";
                    label1.InnerHtml = html;
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
            void Button1_Click(Object sender, EventArgs e)
            {

            }
        </script>
    </head>
    <body id="Body1">
        <Label id="label1" 
            runat="server"/>
        <h3>HtmlSelect Example </h3>
        <input type="button" value="Generate a table." onclick="generate_table()">
        Select item from the list.
        <br />
        

    <input id="hdnFileIndex" name="hdnFileIndex" type="text" />
    <input id="hdnFileNames" type="text" />
    <input id="hdnFileExtensions" type="text" />
    <input id="Submit1" type="submit" value="submit" runat="server" /> 
        <br />
        <script type="text/javascript">
            function getFileDetailsToHidden(pFileIndex, pFileNames, pFileExtensions) {
                document.getElementById('hdnFileIndex').value = pFileIndex;
                document.getElementById('hdnFileNames').value = pFileNames;
                document.getElementById('hdnFileExtensions').value = pFileExtensions;
            }
    </script>
        
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
        <label ID="Label3"
            runat="server" />
        <br />
    </body>
    </html>
</asp:Content>
