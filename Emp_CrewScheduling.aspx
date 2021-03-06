﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_CrewScheduling.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="gvJourneys" runat="server">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
     <asp:Calendar ID="calFlightSearch" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px" AutoPostBack ="true">
                 <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                 <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                 <OtherMonthDayStyle ForeColor="#999999" />
                 <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                 <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                 <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                 <TodayDayStyle BackColor="White" />
                 <WeekendDayStyle BackColor="#CCCCFF" />
             </asp:Calendar>
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Captains "></asp:Label> <asp:Label ID="Label2" runat="server" Text="Copilots "></asp:Label><asp:Label ID="Label3" runat="server" Text="Cabin"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlCaptains" runat="server" padding ="3px" visible ="false">

    </asp:DropDownList>
    <asp:DropDownList ID="ddlCoCaptains" runat="server" padding ="3px" visible="false"></asp:DropDownList>
    <asp:DropDownList ID="ddlCabins" runat="server" padding ="3px" visible="false"></asp:DropDownList>



    



    <br />
    <br />
    <asp:Button ID="btnConfirm" runat="server" Text="Confirm Crew" />  <asp:Button ID="btnAddEmployee" runat="server" Text="Add Employee" /> <asp:Button ID="btnGateCheckin" runat="server" Text="Gate Checkin" />







</asp:Content>

