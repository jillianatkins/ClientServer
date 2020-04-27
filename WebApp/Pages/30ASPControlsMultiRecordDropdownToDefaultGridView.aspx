<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="30ASPControlsMultiRecordDropdownToDefaultGridView.aspx.cs" Inherits="WebApp.Pages._30ASPControlsMultiRecordDropdownToDefaultGridView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1> 30 Multi Record Query Dropdown to Default GridView</h1>
    <div class="offset-2">
        <asp:Label ID="Label1" runat="server" Text="Select a Category "></asp:Label>&nbsp;&nbsp;   
        <asp:DropDownList ID="List01" runat="server"></asp:DropDownList>&nbsp;&nbsp;
        <asp:Button ID="Fetch" runat="server" Text="Fetch" 
             CausesValidation="false" OnClick="Fetch_Click"/>
        <br /><br />
        <asp:Label ID="MessageLabel" runat="server" ></asp:Label>
        <br />
        <asp:GridView ID="List02" runat="server"></asp:GridView>
    </div>
</asp:Content>
