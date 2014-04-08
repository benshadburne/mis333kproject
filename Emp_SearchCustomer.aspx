<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_SearchCustomer.aspx.vb" Inherits="Emp_SearchCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header">
       Search Customer
   </div>
    <div id="middle">
    <asp:Label ID="Label1" runat="server" padding="3px" Text="Select a Filter:"></asp:Label>
        <br />
        <asp:RadioButtonList ID="radSort" runat="server" AutoPostBack="True">
            <asp:ListItem Selected="True" Value="0">Lastname</asp:ListItem>
            <asp:ListItem Value="1">Advantage Number</asp:ListItem>
        </asp:RadioButtonList>
        <br />

        <br />
        <br />

        </div>
    <asp:GridView ID="gvCustomers" runat="server">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
    </asp:GridView>


</asp:Content>

