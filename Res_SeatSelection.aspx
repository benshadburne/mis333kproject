﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Res_SeatSelection.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    <br />
    <br />

    <div class="center-block">

        <asp:Label CssClass="h1" ID="Label1" runat="server" Text="Reservation ID: "></asp:Label>
        <asp:Label CssClass="h1" ID="lblReservationID" runat="server" Text=""></asp:Label>
       
        <br />
        <br />
        <br />
        <asp:Label CssClass="h3" ID="Label2" runat="server" Text="Your Tickets:"></asp:Label>
        <asp:GridView class="table" ID="gvYourReservation" runat="server"></asp:GridView>
        <br />
         <asp:Label CssClass="h3" ID="Label3" runat="server" Text="Other Tickets:"></asp:Label>
        <asp:GridView class="table" ID="gvOtherReservation" runat="server"></asp:GridView>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
   <br />
        <asp:Label CssClass="label" ID="Label5" runat="server" Text="Choose the JourneyID:"></asp:Label>
        <br />
        <asp:DropDownList CssClass="dropdown" ID="ddlJourneyID" runat="server"></asp:DropDownList>
        <br />
        <br />
        <div class="center-block" style="text-align:center ">
         
            <asp:Label CssClass="label" ID="Label6" runat="server" Text="Front of Plane" ></asp:Label>
     </div>
      
          <div class="pull-left" style="margin-left:25%; text-align:center">
                <asp:Button class="btn-seat" ID="btn1A" runat="server" Text="" />
              <br />
              <asp:Button class="btn-seat" ID="btn2A" runat="server" Text="" />
              <br />
              <asp:Button class="btn-seat" ID="btn3A" runat="server" Text="" />
              <asp:Button class="btn-seat" ID="btn3B" runat="server" Text="" />
              <br />
              <asp:Button class="btn-seat" ID="btn4A" runat="server" Text="" />
              <asp:Button class="btn-seat" ID="btn4B" runat="server" Text="" />
              <br />
              <asp:Button class="btn-seat" ID="btn5A" runat="server" Text="" />
              <asp:Button class="btn-seat" ID="btn5B" runat="server" Text="" />
                <br />
                 
              </div>


      <div class="pull-left" style="text-align: center">
                     
              <asp:Button class="btn-seat" ID="btn1B" runat="server" Text="" />
              <br />
              <asp:Button class="btn-seat" ID="btn2B" runat="server" Text="" />
              <br />
              <asp:Button class="btn-seat" ID="btn3C" runat="server" Text="" />
              <asp:Button class="btn-seat" ID="btn3D" runat="server" Text="" />
              <br />
              <asp:Button class="btn-seat" ID="btn4C" runat="server" Text="" />
              <asp:Button class="btn-seat" ID="btn4D" runat="server" Text="" />
              <br />
              <asp:Button class="btn-seat" ID="btn5C" runat="server" Text="" />
              <asp:Button class="btn-seat" ID="btn5D" runat="server" Text="" />
                <br />
          </div>
        </div>
       <div />
      
        
            <div style="width: 100%; text-align: center" class="pull-left">
            
            <asp:Label CssClass="label" ID="Label7" runat="server" Text="Rear of Plane" ></asp:Label>
       
        <br />
        <br />
            <div class="center-block" style="width:100%">
        <asp:Label CssClass="h5" ID="Label4" runat="server" Text="Need to make changes?"></asp:Label>
       </div>
        <ul>
         <li><a href="Cust_ModifyReservation.aspx">Modify Reservation</a></li>
            </ul> 
            </div>
               

</asp:Content>
