<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Res_SelectCustomer.aspx.vb" Inherits="Res_SelectCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header">
        Select customer to create a ticket
   </div>
    <div id="middle">
    <asp:Label ID="Label1" runat="server" padding="3px" Text="Select a Filter:"></asp:Label>

        <asp:RadioButtonList ID="rblSearchBy" runat="server" AutoPostBack="True">
            <asp:ListItem Selected="True" Value="0">Lastname</asp:ListItem>
            <asp:ListItem Value="1">Advantage Number (must be exact)</asp:ListItem>
        </asp:RadioButtonList>

        <br />
        <asp:RadioButtonList ID="rblSearchType" runat="server" >
            <asp:ListItem Value="0">Partial</asp:ListItem>
            <asp:ListItem Value="1" Selected="True">Keyword</asp:ListItem>
        </asp:RadioButtonList>
        <br />

        <br />
        

        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Button ID="btnSearch" runat="server" Text="Search" />

        </div>
    <asp:GridView ID="gvCustomers" runat="server">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
    </asp:GridView>

</asp:Content>

