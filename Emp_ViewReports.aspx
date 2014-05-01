<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_ViewReports.aspx.vb" Inherits="Emp_ViewReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class ="header" style="text-align: center;">
       <h1>View Reports</h1>
   </div>
    <div class="center-block" style="float:left; width: 20%">
        <asp:Button class="btn" ID="btnSearch" runat="server" Text="Search" />
        <br />
        <br />
        <asp:Button class="btn" ID="btnRestartSearch" runat="server" Text="Restart Search" />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <br />
       <asp:Label ID="Label3" runat="server" Text="Search By:" CssClass="label"></asp:Label>
       <asp:RadioButtonList ID="radRevenueSeatCount" runat="server" CssClass="radio">
           <asp:ListItem Value="0">Seats</asp:ListItem>
           <asp:ListItem Value="1">Revenue</asp:ListItem>
           <asp:ListItem Value="2" Selected="True">Both</asp:ListItem>
       </asp:RadioButtonList>
       <br />

        <asp:RadioButtonList ID="rblShowRevenueFromModifications" runat="server" CssClass="radio">
           <asp:ListItem Value="0" Selected ="True">Include Revenue From Modifications</asp:ListItem>
           <asp:ListItem Value="1">Don't Include Revenue From Modifications</asp:ListItem>
       </asp:RadioButtonList>
       <br />

       <asp:Label ID="Label4" runat="server" Text="Filter by:" CssClass="label"></asp:Label>
       <asp:RadioButtonList ID="radClass" runat="server" CssClass="radio">
           <asp:ListItem Value="0">Economy</asp:ListItem>
           <asp:ListItem Value="1">1st Class</asp:ListItem>
       </asp:RadioButtonList>
       <br />
        <asp:Label ID="Label5" CssClass="label" runat="server" Text="Departure City:"></asp:Label>
        <br />
       <asp:DropDownList class="dropdown" ID="ddlCityOrRouteDepart" runat="server">
            <asp:ListItem>DepartureCities</asp:ListItem>
           
        </asp:DropDownList>
       <br />
        <br />
        <asp:Label CssClass="label" ID="Label6" runat="server" Text="End City:"></asp:Label>
        <br />
        <asp:DropDownList Class="dropdown" ID="ddlCityOrRouteEnd" runat="server"></asp:DropDownList>
        
        
       <br />
        <br />
       <asp:Label ID="Label1" runat="server" Text="Lower Date:" CssClass="label"></asp:Label>
    <asp:Calendar ID="calLowerDate" runat="server" ></asp:Calendar>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Upper date:" CssClass="label"></asp:Label>

    <asp:Calendar ID="calUpperDate" runat="server"></asp:Calendar>

       </div>

    <div class ="center-block" style="float: left; width: 70%;"> 

        <asp:GridView ID="gvReports" runat="server">
           
        </asp:GridView>
        <br />
        <asp:Label ID="Label7" runat="server" Text="Totals:" Font-Bold="true" padding-right ="300px"></asp:Label>

    <asp:Label ID="lblRevenue" runat="server" Text="" Font-Bold="true" padding-right="30px"></asp:Label>
        <asp:Label ID="lblModificationRevenue" runat="server" Text="" Font-Bold="true" padding-right="30px"></asp:Label>
        <asp:Label ID="lblSeats" runat="server" Text="" Font-Bold="true"></asp:Label>
    </div>

    </asp:Content>

