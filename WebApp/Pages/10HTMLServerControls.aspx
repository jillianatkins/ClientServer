<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="10HTMLServerControls.aspx.cs" Inherits="WebApp.Pages._10HTMLServerControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%@ Import Namespace="System.Data" %>
    <%@ Import Namespace="System.Data.SqlClient" %>
    <%@ Import Namespace="System.Web.UI.HtmlControls" %>
    <%@ Import Namespace="DBSystem.BLL" %>
    <%@ Import Namespace="DBSystem.ENTITIES" %>

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


        var sel = document.createElement('select');
        sel.name = 'drop1';
        sel.id = 'Select1';

        var cars = [
            "volvo",
            "saab",
            "mercedes",
            "audi"
        ];

        var options_str = "";

        cars.forEach(function (car) {
            options_str += '<option value="' + car + '">' + car + '</option>';
        });

        sel.innerHTML = options_str;


        window.onload = function () {
            document.body.appendChild(sel);
        };

        function generate_table() {
            var body = document.getElementsByTagName("body")[0];
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
            for (var i = 0; i < 20; i++) {
                var row = document.createElement("tr");
                for (var j = 0; j < 2; j++) {
                    var cell = document.createElement("td");
                    var cellText = document.createTextNode("cell in row "+i+", column "+j);
                    cell.appendChild(cellText);
                    row.appendChild(cell);
                }
            tblBody.appendChild(row);
            }
            tbl.appendChild(tblHead);
            tbl.appendChild(tblBody);
            body.appendChild(tbl);
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

                BindList();

                string fileIndex = "This is fileIndex";
                string FileNames = "FileNames";
                string FileExtensions = "FileExtensions";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "paramFN1", "getFileDetailsToHidden('" + fileIndex + "','" + FileNames + "','" + FileExtensions + "');", true);

                string val = Request.Form["hdnFileIndex"];

                //Response.Write("fileIndex's value:  " + val);

                Label2.InnerHtml = "";
                if (!IsPostBack)
                {




                    Label2.InnerHtml += "IsPostBack = False";
                    string ConnectString = "server=localhost;database=Northwind_CPSC1517;integrated security=SSPI";
                    string QueryString = "select * from Products";
                    //var QueryString = "SELECT ProductID, ProductName, "
                    //    + "CategoryID, UnitPrice, UnitsInStock, Discontinued "
                    //    + "FROM Products "
                    //    + "WHERE ProductName LIKE @0 "
                    //    + "ORDER BY ProductName";
                    SqlConnection myConnection = new SqlConnection(ConnectString);
                    SqlDataAdapter myCommand = new SqlDataAdapter(QueryString, myConnection);
                    DataSet ds = new DataSet();
                    myCommand.Fill(ds, "Products");
                    //Select1.DataSource = ds;
                    //Select1.DataTextField = "ProductName";
                    //Select1.DataValueField = "ProductID";
                    //Select1.DataBind();
                    //ListItem myitem = new ListItem();
                    //myitem.Value = "0";
                    //myitem.Text = "select...";
                    //Select1.Items.Insert(0, myitem);

                    var numrows = ds.Tables[0].Rows.Count;
                    var numcols = ds.Tables[0].Columns.Count;
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
                    //label1.InnerHtml = html;
                }
                else
                {
                    Label2.InnerHtml += "IsPostBack = True";
                }
            }
            protected void BindList()
            {
                try
                {
                    CategoryController sysmgr = new CategoryController();
                    List<Category> info = null;
                    info = sysmgr.List();
                    info.Sort((x, y) => x.CategoryName.CompareTo(y.CategoryName));
                    var html = "";
                    html += "<label class='label' for='CategoryList'>Category:  </label>&nbsp;&nbsp;";
                    html += "<select id='CategoryList' name='CategoryList' runat='server'>";
                    html += "<option value='0'>select category...</option>";
                    for (int i = 0; i < info.Count; i++)
                    {
                        html += "<option value='";
                        html += info[i].CategoryID.ToString();
                        html += "'>";
                        html += info[i].CategoryName.ToString();
                        html += "</option>";
                    }
                    html += "</select>";
                    label1.InnerHtml = html;





                    //            @foreach (var item in categoryrecords)
                    //    {

                    //                if (choice == item.CategoryID.ToString())
                    //                {
                    //            <option value="@item.CategoryID" selected>@item.CategoryName</option>
                    //                }
                    //                else
                    //                {
                    //            <option value="@item.CategoryID">@item.CategoryName</option>
                    //                }
                    //            }
                    //</select>





                    //Select1.DataSource = info;
                    //Select1.DataTextField = "CategoryName";
                    //Select1.DataValueField = "CategoryID";
                    //Select1.DataBind();
                    //ListItem myitem = new ListItem();
                    //myitem.Value = "0";
                    //myitem.Text = "select...";
                    //Select1.Items.Insert(0, myitem);
                }
                catch (Exception ex)
                {
                    MessageLabel.InnerHtml = ex.Message;
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
            runat="server" required>
        </select>
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
        <label ID="MessageLabel"
            runat="server" />
        <br />
    </body>
    </html>
</asp:Content>
