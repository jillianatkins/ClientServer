<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="01SingleRecordQuery.aspx.cs" Inherits="WebApp.Pages._01SingleRecordQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Simple Query by Primary Key (Ex05)</h1>
    

    <div class="row">
        <div class="col-md-6">
            <div class="offset-1">
                <span id="labelIDArg">Enter a Category ID: </span> />
                <input type="text" name="IDArg" id="IDArg" />
                <input type="submit" id="buttonFetch" name="buttonFetch" runat="server" onserverclick="Fetch_Click" />    
                <br /><br />
                <span id="MessageLabel"></span> />
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
