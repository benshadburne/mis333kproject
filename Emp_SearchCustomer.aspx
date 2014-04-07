﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_SearchCustomer.aspx.vb" Inherits="Emp_SearchCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header">
       Search Customer
   </div>
    <div id="middle">
    <asp:Label ID="Label1" runat="server" padding="3px" Text="Select a Filter:"></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server" padding ="3px"></asp:DropDownList>
        <br />
        <br />

    <asp:TextBox ID="txtFilter" runat="server"></asp:TextBox>
        <br />
        <br />

        <asp:Button ID="btnFilter" runat="server" text="Filter"></asp:Button>
    <br />

        </div>
    <asp:GridView ID="gvCustomers" runat="server" AllowSorting="True" AutoGenerateColumns="False">
    </asp:GridView>


    <asp:SqlDataSource ID="CustomersDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_20142_Team06ConnectionString %>" SelectCommand="SELECT * FROM [tblCustomersClone]"></asp:SqlDataSource>


</asp:Content>

