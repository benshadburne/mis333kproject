﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cust_ReservationDetails.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <br />
    <br />

   

        <asp:Label CssClass="h1" ID="Label1" runat="server" Text="Reservation ID: "></asp:Label>
        <asp:Label CssClass="h1" ID="lblReservationID" runat="server" Text=""></asp:Label>
       
        <br />
        <br />
        <div class="pull-left" style="word-spacing:10px; min-width: 300px; width: 100%">
        <asp:Button CssClass="btn btn-primary" ID="btnHideTickets" runat="server" Text="Show Tickets" />
        <asp:Button CssClass="btn btn-primary" ID="btnHideSeats" runat="server" Text="Show Seats" />
            <asp:Button CssClass="btn btn-primary" ID="btnHideDates" runat="server" Text="Show Dates" />
        <br />
             <div class="pull-left"  style="word-spacing: 0px; width:150px">
        <asp:Label CssClass="h6" ID="Label5" runat="server" Text="Choose the Flight/Date:"></asp:Label>
        <br />
        <asp:DropDownList CssClass="dropdown" ID="ddlJourneyID" runat="server" AutoPostBack="True"></asp:DropDownList>
            </div>
        <div class="pull-left" style="word-spacing: 0px; width:200px" >
             <asp:Label CssClass="h6" ID="Label8" runat="server" Text="Choose the Advantage Number:"></asp:Label>
        <br />
        <asp:DropDownList CssClass="dropdown" ID="ddlAdvantageNum" runat="server" AutoPostBack="True"></asp:DropDownList>

        </div>
            <div style="word-spacing:0px">
        <asp:Label CssClass="h6" ID="lblMessage" runat="server" Text=""></asp:Label>
                </div>
        </div>
        <br />
        <br />
        <br />
        <asp:Panel CssClass="panel" ID="pnlTickets" runat="server">
            <br />
        <h2>View Reservation Tickets</h2>
            <br />
        <asp:Label CssClass="h3" ID="Label2" runat="server" Text="Your Tickets:"></asp:Label>
        <asp:GridView class="table" ID="gvYourReservation" runat="server"></asp:GridView>
        <br />
         <asp:Label CssClass="h3" ID="Label3" runat="server" Text="Other Tickets:"></asp:Label>
        <asp:GridView class="table" ID="gvOtherReservation" runat="server"></asp:GridView>
                <br />
        <br />
            </asp:Panel>
        
                <br />
        <asp:Panel CssClass="panel" ID="pnlSeats" runat="server" Width="100%">
        
        <h2>Modify Seats</h2>
   <br />
            
        <div style="width: 100%; text-align: center" class="pull-left">
            
            <asp:Label CssClass="h6" ID="Label6" runat="server" Text="Front of Plane" ></asp:Label>
       </div>
                     
          <div class="pull-left" style="text-align:center; width: 449px">
              <div class="pull-right" style="width:200px">
              <asp:Button class="btn-seat" ID="btn1A" runat="server" Text="1A" />
              <br />
              <asp:Button class="btn-seat" ID="btn2A" runat="server" Text="2A" />
              <br />
              <asp:Button class="btn-seat" ID="btn3A" runat="server" Text="3A" />
              <asp:Button class="btn-seat" ID="btn3B" runat="server" Text="3B" />
              <br />
              <asp:Button class="btn-seat" ID="btn4A" runat="server" Text="4A" />
              <asp:Button class="btn-seat" ID="btn4B" runat="server" Text="4B" />
              <br />
              <asp:Button class="btn-seat" ID="btn5A" runat="server" Text="5A" />
              <asp:Button class="btn-seat" ID="btn5B" runat="server" Text="5B" />
                <br />
                 </div>
              </div>


      <div class="pull-right" style="text-align: center; width: 449px">
          <div class="pull-left" style="width:200px">
          <asp:Button class="btn-seat" ID="btn1B" runat="server" Text="1B" />
              <br />
              <asp:Button class="btn-seat" ID="btn2B" runat="server" Text="2B" />
              <br />
              <asp:Button class="btn-seat" ID="btn3C" runat="server" Text="3C" />
              <asp:Button class="btn-seat" ID="btn3D" runat="server" Text="3D" />
              <br />
              <asp:Button class="btn-seat" ID="btn4C" runat="server" Text="4C" />
              <asp:Button class="btn-seat" ID="btn4D" runat="server" Text="4D" />
              <br />
              <asp:Button class="btn-seat" ID="btn5C" runat="server" Text="5C" />
              <asp:Button class="btn-seat" ID="btn5D" runat="server" Text="5D" />
                <br />
              </div>
          </div>
      
            <div style="width: 100%; text-align: center" class="pull-left">
            <asp:Label CssClass="h6" ID="Label7" runat="server" Text="Rear of Plane" ></asp:Label>
       </div>

            <div class="center-block">
                <h6>Legend:</h6> 
               <div class="pull-left" style="padding-right:20px">
                   <asp:Label CssClass="h6" ID="Label9" runat="server" Text="Active Seat: "></asp:Label><asp:Button CssClass="btn-seat btn-sm" ID="Button1" runat="server" Text="XX" BackColor="Green" />
               </div>
                <div class="pull-left" style="padding-right:20px">
                   <asp:Label CssClass="h6" ID="Label10" runat="server" Text="On Reservation: "></asp:Label><asp:Button CssClass="btn-seat btn-sm" ID="Button2" runat="server" Text="XX" BackColor="Blue" />
               </div>
                <div class="pull-left" style="padding-right:20px">
                   <asp:Label CssClass="h6" ID="Label11" runat="server" Text="Not on Reservation: "></asp:Label><asp:Button CssClass="btn-seat btn-sm" ID="Button3" runat="server" Text="XX" BackColor="Coral" />
               </div>
                <div class="pull-left" style="padding-right:20px">
                   <asp:Label CssClass="h6" ID="Label12" runat="server" Text="Empty Seat: "></asp:Label><asp:Button CssClass="btn-seat btn-sm" ID="Button4" runat="server" Text="XX" BackColor="LightGrey" />
               </div>
                <div class="pull-left" style="padding-right:20px">
                   <asp:Label CssClass="h6" ID="Label13" runat="server" Text="Infant in Seat: "></asp:Label><asp:Button CssClass="btn-seat btn-sm" ID="Button5" runat="server" Text="XXi" BackColor="Green" />
               </div>
                <div class="pull-left">
                   <asp:Label CssClass="h6" ID="Label14" runat="server" Text="Infant is user: "></asp:Label><asp:Button CssClass="btn-seat btn-sm" ID="Button6" runat="server" Text="XXi*" BackColor="Green" />
               </div> 
            </div>

      </asp:Panel>
        <div class="pull-left" style="width:100%">
        <asp:Panel CssClass="panel" ID="pnlDates" runat="server" Width="100%" Height="300px">
            
            <h2>Modify Journey Date</h2>
            <br />
         
            <div class="pull-left">
            <asp:Calendar ID="calFlightDate" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px">
                <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                <WeekendDayStyle BackColor="#CCCCFF" />
            </asp:Calendar>
            </div>

            <div class="pull-left" style="width:60%; min-width: 500px; text-align: center">
                
                <br />
                 <asp:TextBox CssClass="text-center" ID="txtAvailable" runat="server" ReadOnly="true" Text=""></asp:TextBox>
                <br />
                <asp:Label CssClass="label" ID="Label4" runat="server" Text="(Making a change will require you to pick new seats and pay a $50 fee)" ForeColor="Gray" Font-Size="XX-Small"></asp:Label>
                <br />
                <br />
                <asp:Button CssClass="btn-seat" ID="btnReservationChange" runat="server" Text="Make Reservation Change" />
            </div>

        </asp:Panel>
        </div>
        
</asp:Content>

