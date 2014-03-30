<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_ModifyCity.aspx.vb" Inherits="Emp_ModifyCity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <link href="bootstrap-3.1.1-dist/css/bootstrap.css" rel="stylesheet" />
    <div class ="label-default">

        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
<br />
        <br />

        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
        <br />


        </div>

    <div class ="input-group-lg">
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br /> 
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br /> 
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br /> 
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <br /> 
        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnModify" runat="server" Text="Modify" />
        <br /> 
        </div>


</asp:Content>

