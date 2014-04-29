<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Res_SelectCustomer.aspx.vb" Inherits="Res_SelectCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header">
        Select customer to create a ticket
   </div>
    <asp:Panel ID="pnlDoSearch" runat="server"> 
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

        <br />
        <br />
        <asp:Button ID="btnAddCustomer" runat="server" Text="Add Customer" />
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Made a mistake? Cancel your Reservation by clicking below"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel Reservation" />
        </div>

        </asp:Panel>

    <asp:Panel ID="pnlAddAge" runat="server" Visible="false"> 
    <div id="Div1">
    <asp:Label ID="lblAge" runat="server" padding="3px" Text="Please enter your age"></asp:Label>

        <br />
      
        <asp:TextBox ID="txtAge" runat="server"></asp:TextBox>

        <br />

        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" />
        
        <br />
        <asp:Label ID="lblAgeMessage" runat="server" Text=""></asp:Label>

        </div>

        </asp:Panel>

    <asp:Label ID="lblMessage" runat="server"></asp:Label>
    <br />
    <asp:GridView ID="gvCustomers" runat="server">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
    </asp:GridView>

</asp:Content>

