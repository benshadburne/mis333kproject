﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_AddNewEmployee.aspx.vb" Inherits="Emp_AddNewEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="center-align" style="padding-bottom:20px">
        <h1>Add New Employee</h1>
        </div>

    <div style="line-height:28px">
    <asp:Panel CssClass="panel" ID="Panel1" Width="900px" runat="server">
        <div class="pull-left" style="text-align:right;width:350px; padding-right:5px">
 <asp:Label ID="Label2" runat="server" Text="Last Name"></asp:Label><br />
            <asp:Label ID="Label3" runat="server" Text="First Name:"></asp:Label><br />
            <asp:Label ID="Label4" runat="server" Text="MI:"></asp:Label><br />
            <asp:Label ID="Label5" runat="server" Text="Password:"></asp:Label><br />
            <asp:Label ID="Label6" runat="server" Text="Social Security Number:"></asp:Label><br />
            <asp:Label ID="Label7" runat="server" Text="Employee Type:"></asp:Label><br />
            <asp:Label ID="Label8" runat="server" Text="Address:"></asp:Label><br />
            <asp:Label ID="Label9" runat="server" Text="Zip:"></asp:Label><br />
            <asp:Label ID="Label10" runat="server" Text="Phone:"></asp:Label><br />
        </div>
        <div class="pull-left" style="text-align:left;width:540px">
              <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Please enter a last name!" ControlToValidate="txtLastName" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="Please enter a first name!" ControlToValidate="txtFirstName" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtMI" runat="server" MaxLength="1"></asp:TextBox><br />
                <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please enter a password!" ControlToValidate="txtPassword" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtSSN" runat="server" MaxLength="9"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSSN" runat="server" ErrorMessage="Please enter a Social Security Number!" ControlToValidate="txtLastName" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtEmpType" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmpType" runat="server" ErrorMessage="Please enter an employee type!" ControlToValidate="txtEmpType" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="Please enter an address!" ControlToValidate="txtAddress" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtZip" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvZip" runat="server" ErrorMessage="Please enter a zip code!" ControlToValidate="txtZip" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" ErrorMessage="Please enter a phone number!" ControlToValidate="txtPhoneNumber" Text="*"></asp:RequiredFieldValidator><br />
            </div>
    </asp:Panel></div>
             
    <div class="center-align">
        <asp:Button CssClass="btn btn-primary" ID="btnSubmit" runat="server" Text="Submit" /><br />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <asp:Label CssClass="h6" ID="lblMessage" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>

