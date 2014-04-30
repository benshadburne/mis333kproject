<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cust_ModifyProfile.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="header" style="text-align: center;">

        <h1>
            Modify Customer Page
        </h1>

    </div>

    <br />
    <br />

    <div class ="center-block">
    <div class ="pull-left" style="width:20%;">
            <br />
            <asp:Button class="btn" ID="btnModify" runat="server" Text="Modify Customer" />
        <br />
            <br />
        <asp:Button class="btn" ID="btnAbort" runat="server" Text="Abort Modification" />
            <br />
        <br />
        <asp:Button class="btn" ID="btnSave" runat="server" Text="Save Modification" />
            <br />
            <br />
            <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
        <br />
            <asp:Label ID="lblSuccessMessage" runat="server" Text=""></asp:Label>
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
            <br />
            <br />
            <br />
        </div>
   
        <div class ="center-block" style="width: 70%;">
            <div id ="pull-right" style="width: 15%; float:left; text-align: right; line-height: 200%">
                <asp:Label ID="lblFName" runat="server" Text="First Name:"></asp:Label>
                <br />
                <asp:Label ID="lblMI" runat="server" Text="Middle Initial:"></asp:Label>
                <br />
                <asp:Label ID="lblLName" runat="server" Text="Last Name:"></asp:Label>
                <br />
                <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                <br />
                <asp:Label ID="lblAddress" runat="server" Text="Address:"></asp:Label>
                <br />
                <asp:Label ID="lblZip" runat="server" Text="Zip Code:"></asp:Label>
                <br />
                <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                <br />
                <asp:Label ID="lblPhone" runat="server" Text="Phone:"></asp:Label>
                <br />
                <asp:Label ID="lblMiles" runat="server" Text="Miles"></asp:Label>
            </div>
            <div id ="pull-left" style="width: 70%; float: left; line-height: 160%">
                <asp:TextBox ID="txtFName" class="text-left" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFName" ErrorMessage="First name required." ForeColor="Red">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtMI" runat="server"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtLName" class="text-left" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLName" ErrorMessage="Last name required." ForeColor="Red">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password required." ForeColor="Red">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtZip" runat="server"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email address required." ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email address needs to be formatted correctly." ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                <br />
                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone number required." ForeColor="Red">*</asp:RequiredFieldValidator><br />
                
                <asp:TextBox ID="txtMiles" runat="server"></asp:TextBox>
                <br />
            </div>
            </div>
        </div>
</asp:Content>
