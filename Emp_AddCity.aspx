<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_AddCity.aspx.vb" Inherits="Emp_AddCity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
        <asp:Button ID="btnAdd" runat="server" Text="Add" />
        <br /> 
        </div>



    
</asp:Content>

