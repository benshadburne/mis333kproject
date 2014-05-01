<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cust_AddNewCustomer.aspx.vb" Inherits="Cust_AddNewCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="header" style="text-align: center;">

        <h1>
            Add New Customer Page
        </h1>

    </div>

    <br />
    <br />

    <div class ="center-block">
        <div style="text-align:center; word-spacing:30px;padding-bottom:20px">
            <asp:Button class="btn btn-primary" ID="btnSave" runat="server" Text="Save" />
           
            <asp:Button class="btn btn-primary" ID="btnClear" runat="server" Text="Clear" CausesValidation="False" />
            
        <asp:Button class="btn btn-primary" ID="btnAddFamilyMember" runat="server" Text="Add Family Member" CausesValidation="False" />
       

        </div>
   
        <div class ="center-block" style="width: 70%;">
            <div id ="pull-right" style="width: 15%; float:left; text-align: right; line-height: 28px">
                <asp:Label CssClass="h6" ID="lblFName" runat="server" Text="First Name:"></asp:Label>
                <br />
                <asp:Label CssClass="h6" ID="lblMI" runat="server" Text="Middle Initial:"></asp:Label>
                <br />
                <asp:Label CssClass="h6" ID="lblLName" runat="server" Text="Last Name:"></asp:Label>
                <br />
                <asp:Label CssClass="h6" ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                <br />
                <asp:Label CssClass="h6" ID="lblAddress" runat="server" Text="Address:"></asp:Label>
                <br />
                <asp:Label class="h6" ID="lblCity" runat="server" Text="City:"></asp:Label>
                <br />
                <asp:Label CssClass="h6" ID="lblState" runat="server" Text="State:"></asp:Label>
                <br />
                <asp:Label class="h6" ID="lblZip" runat="server" Text="Zip Code:"></asp:Label>
                <br />
                <asp:Label class="h6" ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                <br />
                <asp:Label class="h6" ID="lblPhone" runat="server" Text="Phone:"></asp:Label>
            </div>
            <div id ="center-block" style="text-align:center">
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
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtZip" runat="server"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email address required." ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email address needs to be formatted correctly." ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                <br />
                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone number required." ForeColor="Red">*</asp:RequiredFieldValidator>
                <br />
            </div>
    <div class ="center-block">
           
            
            <asp:Label CssClass="h6" ID="lblErrorMessage" runat="server" Text=""></asp:Label>
            <asp:Label CssClass="h6" ID="lblSuccessMessage" runat="server" Text=""></asp:Label>
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
            <br />
        </div>
   
            </div>
        </div>
</asp:Content>
