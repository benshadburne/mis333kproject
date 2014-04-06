<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_ModifyCity.aspx.vb" Inherits="Emp_ModifyCity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div id="header">
       Modify City
   </div>
    <div class ="label-default" id="middle">

       <asp:Label ID="Label1" runat="server" Text="City name:"></asp:Label>
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

    <div class ="input-group-lg" id="right">
        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ErrorMessage="The city name is a required field." Text="*" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
        <br /> 
        <asp:TextBox ID="txtAirport" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAirport" runat="server" ErrorMessage="The airport code must be 3 letters." Text="*" ControlToValidate="txtAirport"></asp:RequiredFieldValidator>
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

