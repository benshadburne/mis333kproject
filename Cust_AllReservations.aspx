<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cust_AllReservations.aspx.vb" Inherits="_Default" %>

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
            <asp:Label CssClass="label" ID="lblCountReservations" runat="server" Text="Count:"></asp:Label></div>
         <br />
         <asp:Label CssClass="h6" ID="lblMessage" runat="server" Text=""></asp:Label>
         <asp:GridView class="table" ID="gvAllReservations" runat="server" AutoGenerateSelectButton="True">
        </asp:GridView>
           
        <div style="text-align:center">
        <asp:Button CssClass="btn btn-primary" ID="btnCustomerDash" runat="server" Text="Customer Dashboard" />
            </div>
    </div>
    
</asp:Content>

