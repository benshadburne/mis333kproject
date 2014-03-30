<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_ModifyEmployee.aspx.vb" Inherits="Emp_ModifyEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <link href="StyleSheetforASP2.css" rel="stylesheet" />
    <link href="bootstrap-3.1.1-dist/css/bootstrap-theme.css" rel="stylesheet" type="text/css" />
<link href="bootstrap-3.1.1-dist/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
<link href="bootstrap-3.1.1-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
<link href="bootstrap-3.1.1-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />

    <div id ="left">
        <div class ="label-primary">
            <asp:Label ID="lblMessage" runat="server" Text="Welcome to the Add New Employee Page!"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" /><br /><br />
        </div>
        <div class ="btn">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" /><br /><br />
        </div>

        <asp:LinkButton ID="lbHome" runat="server" CausesValidation="False">Return to Employee Dashboard</asp:LinkButton>
    </div>

    <div id ="middle">

        <div class ="label-default">
            <asp:Label ID="Label1" runat="server" Text="EmpID"></asp:Label><br />
            <asp:Label ID="Label2" runat="server" Text="Last Name"></asp:Label><br />
            <asp:Label ID="Label3" runat="server" Text="First Name"></asp:Label><br />
            <asp:Label ID="Label4" runat="server" Text="Password"></asp:Label><br />
            <asp:Label ID="Label5" runat="server" Text="Employee Type"></asp:Label><br />
            <asp:Label ID="Label6" runat="server" Text="Social Security Number"></asp:Label><br />
            <asp:Label ID="Label7" runat="server" Text="Phone Number"></asp:Label><br />
        </div>
        </div>
        <div id ="right">
            <div class ="input-group-lg">
                <asp:TextBox ID="txtEmpID" runat="server"></asp:TextBox><br />
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Please enter a last name!" ControlToValidate="txtLastName" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="Please enter a first name!" ControlToValidate="txtFirstName" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please enter a password!" ControlToValidate="txtPassword" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtEmpType" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmpType" runat="server" ErrorMessage="Please enter an employee type!" ControlToValidate="txtEmpType" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtSSN" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSSN" runat="server" ErrorMessage="Please enter a Social Security Number!" ControlToValidate="txtLastName" Text="*"></asp:RequiredFieldValidator><br />
                <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" ErrorMessage="Please enter a phone number!" ControlToValidate="txtPhoneNumber" Text="*"></asp:RequiredFieldValidator><br />
            </div>
        </div>
        
    
</asp:Content>
