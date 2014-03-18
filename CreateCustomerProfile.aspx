<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="CreateCustomerProfile.aspx.vb" Inherits="CreateCustomerProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="StyleSheetforASP2.css" rel="stylesheet" />
    <div id="left">

        <br />
        <br />
        <asp:LinkButton ID="lnkLogin" runat="server" PostBackUrl="login.aspx">Return to Login</asp:LinkButton>

    </div>

    <div id ="middle">

        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="First Name:"></asp:Label>

    

        <br />
        <asp:Label ID="Label2" runat="server" Text="Last Name:"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Password:"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Confirm Password:"></asp:Label>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Address:"></asp:Label>
        <br />
        <asp:Label ID="Label8" runat="server" Text="City:"></asp:Label>

    

        <br />
        State:<br />
        <asp:Label ID="Label9" runat="server" Text="Phone:"></asp:Label>
        <br />
        <asp:Label ID="Label7" runat="server" Text="Email:"></asp:Label>

    

    </div>

    <div id="right">

        <br />
        <br />
        <br />
        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtConfirmPassword" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>

    </div>
</asp:Content>

