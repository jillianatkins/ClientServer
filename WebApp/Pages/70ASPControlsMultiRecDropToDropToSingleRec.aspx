<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="70ASPControlsMultiRecDropToDropToSingleRec.aspx.cs" Inherits="WebApp.Pages._70ASPControlsMultiRecordDropToDropToSingleRec" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1> 70 Multi Record Query Dropdown to another Dropdown to a Single Record via Page Navigation (Project)</h1>
    <div class="row">
        <div class="col-md-4 text-right">
            <asp:Label ID="Label1" runat="server" Text="Select a Category "></asp:Label>
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
            <asp:Label ID="Label2" runat="server" Text="Select a Product "></asp:Label>
        </div>
        <div class="col-md-4 text-left">
            <asp:DropDownList ID="List02" runat="server"></asp:DropDownList>&nbsp;&nbsp;
            <asp:Button ID="Fetch02" runat="server" Text="Fetch" 
                 CausesValidation="false" OnClick="Fetch_Click02"/>
        </div>
    </div>
    <div>
        <br /><br />
        <asp:Label ID="MessageLabel" runat="server" ></asp:Label>
    </div>
</asp:Content>
