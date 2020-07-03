<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectCRUD.aspx.cs" Inherits="WebApp.Exercises.ProjectCRUD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-4"></div>
        <div class="col-8 text-left">
            <h3>Program Maintenance Page</h3>
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                <asp:Label ID="Label1" runat="server" Text="Program ID"
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
                  <asp:Label ID="Label2" runat="server" Text="Program Name"
                     AssociatedControlID="Name"></asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="Name" runat="server"></asp:TextBox>
        </div>
    </div>
    
    <div class="row">
        <div class="col-4 text-right">
                  <asp:Label ID="Label3" runat="server" Text="Diploma Name"
                     AssociatedControlID="Diploma"></asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="Diploma" runat="server"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <div class="col-4 text-right">
                <asp:Label ID="Label4" runat="server" Text="School"
                     AssociatedControlID="SchoolsList">
                </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:DropDownList ID="SchoolsList" runat="server" Width="300px" >
                </asp:DropDownList> 
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                  <asp:Label ID="Label5" runat="server" Text="Tuition"
                     AssociatedControlID="Tuition">
                  </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="Tuition" runat="server"> 
                </asp:TextBox>
        </div>
    </div>

        <div class="row">
        <div class="col-4 text-right">
                  <asp:Label ID="Label6" runat="server" Text="International Tuition"
                     AssociatedControlID="IntTuition">
                  </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="IntTuition" runat="server"> 
                </asp:TextBox>
        </div>
    </div>

    <br />
    <div class="row">
        <div class="col-4"></div>
        <div class="col-8 text-left">
            <asp:Button ID="BackButton" runat="server" Text="Back" OnClick="Back_Click" />&nbsp;
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
