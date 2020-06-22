<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="90ASPControlsPartialStringSearchToDropToSingleRec.aspx.cs" Inherits="WebApp.Pages._90ASPControlsPartialStringSearchToDropToSingleRec" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1> 90 Partial String Search to Dropdown to Single Record via Page Navigation (Project)</h1>
    <div class="row">
        <div class="col-md-4 text-right">
            <asp:Label ID="Label1" runat="server" Text="Enter a Partial Product Name "></asp:Label>
        </div>
        <div class="col-md-4 text-left">
            <asp:TextBox ID="PartialProductName" runat="server"></asp:TextBox>&nbsp;&nbsp
            <asp:Button ID="SearchProductsPartial" runat="server" Text="Search Products"
                OnClick="SearchProductsPartial_Click" />
            <br />
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-4 text-right">
            <asp:Label ID="Label2" runat="server" Text="Select a Product"></asp:Label>
        </div>
        <div class="col-md-4 text-left">
            <asp:DropDownList ID="List01" runat="server"></asp:DropDownList>&nbsp;&nbsp;
            <asp:Button ID="Fetch01" runat="server" Text="Fetch" 
                 CausesValidation="false" OnClick="Fetch_Click01"/>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-4 text-right">
        </div>
        <div class="col-md-4 text-left">
            <label ID="MessageLabel" runat="server" ></label>
        </div>
    </div>
</asp:Content>
