<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="StyleSheetforASP2.css" rel="stylesheet" />
    <div id="left">
        <br />
        <asp:Label ID="Label1" runat="server" Text="Username:"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Password:"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="Login" />
        <br />
        <br />
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <br />
        <br />
        <asp:LinkButton ID="lnk" runat="server">New Customer</asp:LinkButton>
    </div>
</asp:Content>

