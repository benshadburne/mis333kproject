<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cust_AddNewCustomer.aspx.vb" Inherits="Cust_AddNewCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class ="pull-left" style="width:20%;">
            <br />
            <br />
            <br />
            <asp:Button class="btn" ID="btnSave" runat="server" Text="Save" />
            <br />
            <br />
            <asp:Button class="btn" ID="btnClear" runat="server" Text="Clear" CausesValidation="False" />
            <br />
            <br />
            <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblSuccessMessage" runat="server" Text=""></asp:Label>
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
            <br />
            <asp:LinkButton ID="lnkHome" class="btn-link" runat="server" PostBackUrl="~/index.aspx" CausesValidation="False">Home</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lnkShowAll" CssClass="btn-link" runat="server" PostBackUrl="~/ShowAll.aspx" CausesValidation="False">Show All Customers</asp:LinkButton>
        </div>
   
        <div class ="content">
            <div id ="content-labels">
                <asp:Label ID="lblLName" runat="server" Text="Last Name:"></asp:Label>
                <br />
                <asp:Label ID="lblFName" runat="server" Text="First Name:"></asp:Label>
                <br />
                <asp:Label ID="lblMI" runat="server" Text="Middle Initial:"></asp:Label>
                <br />
                <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label>
                <br />
                <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                <br />
                <asp:Label ID="lblAddress" runat="server" Text="Address:"></asp:Label>
                <br />
                <asp:Label ID="lblCity" runat="server" Text="City:"></asp:Label>
                <br />
                <asp:Label ID="lblState" runat="server" Text="State:"></asp:Label>
                <br />
                <asp:Label ID="lblZip" runat="server" Text="Zip Code:"></asp:Label>
                <br />
                <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                <br />
                <asp:Label ID="lblPhone" runat="server" Text="Phone number:"></asp:Label>
            </div>
            <div id ="content-textboxes">
                <asp:TextBox ID="txtLName" class="text-left" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLName" ErrorMessage="Last name required." ForeColor="Red">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtFName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFName" ErrorMessage="First name required." ForeColor="Red">*</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtMI" runat="server"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username required." ForeColor="Red">*</asp:RequiredFieldValidator>
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
            </div>
            </div>
</asp:Content>
