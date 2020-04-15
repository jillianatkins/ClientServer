<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="01SingleRecordQuery.aspx.cs" Inherits="WebApp.Pages._01SingleRecordQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Simple Query by Primary Key (Ex05)</h1>
    

    <div class="row">
        <div class="col-md-6">
            <div class="offset-1">
                <div class="alert alert-primary" role="alert" id="hey" runat="server">
  This is a primary alert—check it out!
</div>
                <span id="labelIDArg">Enter a Category ID: </span>
                <input type="text" name="IDArg" id="IDArg" runat="server"/>
                <input type="submit" id="buttonFetch" name="buttonFetch" runat="server" onserverclick="Fetch_Click" />    
                <br /><br />
                <input id="labelMessage" name="labelMessage" type="text" readonly runat="server">
                <label id="MessageLabel" runat="server"></label> />
                <%--<asp:Label runat="server" Text="Enter a Category ID:"></asp:Label>&nbsp;&nbsp;
                <asp:TextBox id="IDArg" runat="server"></asp:TextBox>&nbsp;&nbsp;
                <asp:Button id="Fetch" runat="server" Text="Fetch" OnClick="Fetch_Click" />
                <asp:Label id="MessageLabel" runat="server"></asp:Label>--%>

            </div>
        </div>
        <div class="col-md-6">
            <asp:Label runat="server" Text="Category ID:"></asp:Label>&nbsp;&nbsp;
            <asp:Label id="ID" runat="server" ></asp:Label>
            <br />
            <asp:Label runat="server" Text="Category Name:"></asp:Label>&nbsp;&nbsp;
            <asp:Label id="Name" runat="server" ></asp:Label>
        </div>
    </div>
</asp:Content>
