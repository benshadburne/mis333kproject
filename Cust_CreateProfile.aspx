<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cust_CreateProfile.aspx.vb" Inherits="CreateCustomerProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="StyleSheetforASP2.css" rel="stylesheet" />
    <link href="bootstrap-3.1.1-dist/css/bootstrap-theme.css" rel="stylesheet" type="text/css" />
<link href="bootstrap-3.1.1-dist/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
<link href="bootstrap-3.1.1-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
<link href="bootstrap-3.1.1-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <div id="left">

        <br />
        <br />
        <asp:LinkButton ID="lnkLogin" runat="server" PostBackUrl="login.aspx">Return to Login</asp:LinkButton>

        <br />
        <br />
        <asp:ValidationSummary ID="vsErrors" runat="server" />
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="lblMessage" runat="server"></asp:Label>

    </div>

    <div id ="middle">

        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="*First Name:"></asp:Label>

    

        <br />
        <asp:Label ID="Label2" runat="server" Text="*Last Name:"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Text="*Password:"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" Text="*Confirm Password:"></asp:Label>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Address:"></asp:Label>
        <br />
        <asp:Label ID="Label8" runat="server" Text="City:"></asp:Label>

    

        <br />
            <asp:Label ID="Label6" runat="server" Text="State:"></asp:Label><br />
        <asp:Label ID="Label9" runat="server" Text="*Phone:"></asp:Label>
        <br />
        <asp:Label ID="Label7" runat="server" Text="*Email:"></asp:Label>

    

    </div>

    <div id="right">

        <br />
        <br />
        <br />
        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First Name is required field.">*</asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfdLastname" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last Name is a required field">*</asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfdPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required field">*</asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="txtConfirmPassword" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfdConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm Password is a required field">*</asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfdPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone is requried field">*</asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>

        <asp:RequiredFieldValidator ID="rfdEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required field">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is not in proper email format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Button" />

        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="RecordID" HeaderText="RecordID" InsertVisible="False" ReadOnly="True" SortExpression="RecordID" />
                <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
                <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                <asp:BoundField DataField="MI" HeaderText="MI" SortExpression="MI" />
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
                <asp:BoundField DataField="ZipCode" HeaderText="ZipCode" SortExpression="ZipCode" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                <asp:BoundField DataField="EmailAddr" HeaderText="EmailAddr" SortExpression="EmailAddr" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_msbcd323ConnectionString %>" SelectCommand="SELECT * FROM [tblCustomers]"></asp:SqlDataSource>

    </div>
</asp:Content>

