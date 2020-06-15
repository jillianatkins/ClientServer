<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="50ASPControlsMultiRecordDropdownToSingleRecord.aspx.cs" Inherits="WebApp.Pages._50ASPControlsMultiRecordDropdownToSingleRecord" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1> 50 Multi Record Query Dropdown to Single Record via Page Navigation (EX08)</h1>
    <div class="offset-2">
        <asp:Label ID="Label1" runat="server" Text="Select a Product "></asp:Label>&nbsp;&nbsp;   
        <asp:DropDownList ID="List01" runat="server"></asp:DropDownList>&nbsp;&nbsp;
        <asp:Button ID="ButtonFetch" runat="server" Text="Update/Delete" 
             CausesValidation="false" OnClick="Fetch_Click"/>&nbsp;&nbsp;
        <asp:Button ID="ButtonAdd" runat="server" Text="Add" 
             CausesValidation="false" OnClick="Add_Click"/>
        <br /><br />
        <asp:Label ID="MessageLabel1" runat="server" ></asp:Label>
    </div>
</asp:Content>
