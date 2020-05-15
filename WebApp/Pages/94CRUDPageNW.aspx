<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="94CRUDPageNW.aspx.cs" Inherits="WebApp.Pages._94CRUDPageNW" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-4"></div>
        <div class="col-8 text-left">
            <h3>Product Maintenance Page</h3>
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                <asp:Label ID="Label1" runat="server" Text="ID"
                     AssociatedControlID="ID">
                </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="ID" runat="server" ReadOnly="true">
                </asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                  <asp:Label ID="Label2" runat="server" Text="Name"
                     AssociatedControlID="Name"></asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="Name" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                <asp:Label ID="Label6" runat="server" Text="Supplier"
                     AssociatedControlID="SupplierList">
                </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:DropDownList ID="SupplierList" runat="server" Width="300px">
                </asp:DropDownList> 
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                <asp:Label ID="Label7" runat="server" Text="Category"
                     AssociatedControlID="CategoryList">
                </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:DropDownList ID="CategoryList" runat="server" Width="300px" >
                </asp:DropDownList> 
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                  <asp:Label ID="Label4" runat="server" Text="Unit Price"
                     AssociatedControlID="UnitPrice">
                  </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="UnitPrice" runat="server"> 
                </asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                  <asp:Label ID="Label11" runat="server" Text="Discontinued"
                     AssociatedControlID="Discontinued">
                  </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:CheckBox ID="Discontinued" runat="server">
                </asp:CheckBox> 
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-4"></div>
        <div class="col-8 text-left">
            <asp:Button ID="BackButton" runat="server" Text="Back" OnClick="Back_Click" />&nbsp;
            <asp:Button ID="AddButton" runat="server" OnClick="Add_Click" Text="Add"/>&nbsp;
            <asp:Button ID="UpdateButton" runat="server" OnClick="Update_Click" Text="Update"/>&nbsp;
            <asp:Button ID="DeleteButton" runat="server" OnClick="Delete_Click" Text="Delete"
              OnClientClick="return CallFunction();"/>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-4"></div>
        <div class="col-8">
            <label ID="MessageLabel" runat="server" />
        </div>
    </div>
    <script type="text/javascript">
        function CallFunction() {
            return confirm("Are you sure you wish to delete this record?");
       }
   </script>
</asp:Content>
