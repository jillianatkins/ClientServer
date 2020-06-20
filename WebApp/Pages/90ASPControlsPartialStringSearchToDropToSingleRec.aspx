<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="90ASPControlsPartialStringSearchToDropToSingleRec.aspx.cs" Inherits="WebApp.Pages._90ASPControlsPartialStringSearchToDropToSingleRec" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1> 90 Partial String Search to Dropdown to Single Record via Page Navigation (Project)</h1>
    <div class="offset-2">
        <asp:Label ID="Label1" runat="server" Text="Enter a Partial Product Name "></asp:Label>&nbsp;&nbsp
        <asp:TextBox ID="PartialProductNameV2" runat="server"></asp:TextBox>
        <asp:Button ID="SearchProductsPartial" runat="server" Text="Search Products"
            OnClick="SearchProductsPartial_Click" />
        <br />
        <br />
        <label ID="MessageLabel" runat="server" ></label>
        <br />
    </div>
</asp:Content>
