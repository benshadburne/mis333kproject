﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_Cancel_Journey.aspx.vb" Inherits="Emp_Cancel_Journey" %>

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
                 <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                 <WeekendDayStyle BackColor="#CCCCFF" />
             </asp:Calendar>
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <br />
    <br />



    <br />
    <br />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel Selected Journey" /><br />
    <asp:Button ID="btnAbort" runat="server" Text="Abort Cancellation" Visible="False" /><br />
    <asp:Button ID="btnConfirm" runat="server" Text="Confirm Cancelation" Visible="False" />



</asp:Content>