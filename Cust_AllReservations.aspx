﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cust_AllReservations.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <br />

    <div class="center-block">

        <asp:Label CssClass="h1" ID="Label1" runat="server" Text="All Reservations"></asp:Label>

        <br />
        <br />
        
       
       
        
        <div class="pull-right">
            <asp:Label CssClass="label" ID="Label7" runat="server" Text="Count:"></asp:Label></div>
         <br />
         <asp:GridView class="table" ID="gvAllReservations" runat="server" AutoGenerateSelectButton="True"></asp:GridView>
           


    </div>
    
</asp:Content>

