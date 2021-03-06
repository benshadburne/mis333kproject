﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_ModifyEmployeeManager.aspx.vb" Inherits="Emp_ModifyEmployeeManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div id ="left">
        <div class ="label-primary">
            
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" /><br /><br />
        </div><asp:Label ID="lblMessage" runat="server" Text="Welcome to the Modify Employee Page!"></asp:Label>
        <div class ="btn">
            <asp:Button ID="btnModify" runat="server" Text="Modify" /><br /><br />
            <asp:Button ID="btnAccept" runat="server" Text="Accept" Visible="False" /><br /><br />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="False" CausesValidation="False" />
        </div>

    </div>

    <div id ="middle">

<asp:Label ID="Label2" runat="server" Text="Last Name"></asp:Label><br />
            <asp:Label ID="Label3" runat="server" Text="First Name"></asp:Label><br />
            <asp:Label ID="Label4" runat="server" Text="MI"></asp:Label><br />
            <asp:Label ID="Label5" runat="server" Text="Password"></asp:Label><br />
            <asp:Label ID="Label6" runat="server" Text="Social Security Number"></asp:Label><br />
            <asp:Label ID="Label7" runat="server" Text="Employee Type"></asp:Label><br />
            <asp:Label ID="Label8" runat="server" Text="Address"></asp:Label><br />
        <asp:Label ID="Label9" runat="server" Text="Zip"></asp:Label><br />
        <asp:Label ID="Label10" runat="server" Text="Phone"></asp:Label><br />
        <asp:Label ID="Label1" runat="server" Text="Active"></asp:Label><br />
        
        </div>
        <div id ="right">
            <div class ="input-group-lg">
                <asp:TextBox ID="txtLastName" runat="server" ReadOnly="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Please enter a last name!" ControlToValidate="txtLastName" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtFirstName" runat="server" ReadOnly="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="Please enter a first name!" ControlToValidate="txtFirstName" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtMI" runat="server" MaxLength="1" ReadOnly="True"></asp:TextBox><br />
                <asp:TextBox ID="txtPassword" runat="server" ReadOnly="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please enter a password!" ControlToValidate="txtPassword" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtSSN" runat="server" MaxLength="9" ReadOnly="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSSN" runat="server" ErrorMessage="Please enter a Social Security Number!" ControlToValidate="txtLastName" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtEmpType" runat="server" ReadOnly="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmpType" runat="server" ErrorMessage="Please enter an employee type!" ControlToValidate="txtEmpType" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtAddress" runat="server" ReadOnly="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="Please enter an address!" ControlToValidate="txtAddress" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtZip" runat="server" ReadOnly="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvZip" runat="server" ErrorMessage="Please enter a zip code!" ControlToValidate="txtZip" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="10" ReadOnly="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" ErrorMessage="Please enter a phone number!" ControlToValidate="txtPhoneNumber" Text="*"></asp:RequiredFieldValidator><br />
                <asp:DropDownList ID="ddlActive" runat="server" Enabled="False">
                    <asp:ListItem>Y</asp:ListItem>
                    <asp:ListItem>N</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        
    
</asp:Content>
