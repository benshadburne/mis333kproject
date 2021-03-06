﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Res_SelectCustomer.aspx.vb" Inherits="Res_SelectCustomer" %>

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
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <br />
        <asp:TextBox ID="txtSearch" runat="server" Width ="100px"></asp:TextBox>

        <br />
        <br />

        <asp:Button ID="btnSearch" runat="server" Text="Search" />

        <br />
        <br />
          <asp:Button ID="btnSearchAll" runat="server" Text="Search All" />
        <br />
        <br />
        <asp:Button ID="btnAddCustomer" runat="server" Text="Add Customer" />
        <br />
        <br />
        <br />
        <br />
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

    
    <br />
    <asp:GridView ID="gvCustomers" runat="server">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <div>
    <div class="center-block" style="width:100%">
        <asp:Label CssClass="h5" ID="Label8" runat="server" Text="Need to cancel this reservation?"></asp:Label>
       </div>
        <ul>
            <asp:Button ID="btnCancel" runat="server" Text="Cancel Reservation" />
            </ul> 
            </div>

</asp:Content>

