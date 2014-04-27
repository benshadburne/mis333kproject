﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Res_Pay.aspx.vb" Inherits="Res_Pay" %>

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
        <asp:GridView class="table" ID="gvYourReservation" runat="server" Visible="false"></asp:GridView>
        <asp:GridView class="table" ID="gvTickets" runat="server">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        
        <br />
         <asp:Label CssClass="h3" ID="Label3" runat="server" Text="Other Tickets:"></asp:Label>
        <asp:GridView class="table" ID="gvOtherReservation" runat="server" Visible="false"></asp:GridView>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
   <br />
        <div style="width:100%">
        <div class="pull-left">
        <asp:Label CssClass="label" ID="Label5" runat="server" Text="Choose the JourneyID:"></asp:Label>
        <br />
        <asp:DropDownList CssClass="dropdown" ID="ddlJourneyID" runat="server" AutoPostBack="True"></asp:DropDownList>
        </div>
                  
        <div class="pull-left" style="word-spacing: 0px" >
             <asp:Label CssClass="label" ID="Label9" runat="server" Text="Choose the Advantage Number:"></asp:Label>
        <br />
        <asp:DropDownList CssClass="dropdown" ID="ddlAdvantageNum" runat="server" AutoPostBack="True"></asp:DropDownList>

        
            </div>
        </div>
        <br />
        <div class="center-block" style="text-align:center ">
         
            <asp:Label CssClass="label" ID="Label6" runat="server" Text="Front of Plane" ></asp:Label>
     </div>
      
        <div class="pull-left" style="text-align:right; width: 200px">
               
                 
          
              <asp:Label ID="lblPay" runat="server" Text="Pay with:" Visible ="false"></asp:Label>
            <br />
              <asp:RadioButtonList ID="rblPayment" runat="server" AutoPostBack="True" Visible="false">
                  <asp:ListItem>Miles</asp:ListItem>
                  <asp:ListItem>Money</asp:ListItem>
              </asp:RadioButtonList>
               
            <asp:Label ID="lblMiles" runat="server" Text=""></asp:Label>
              <br />
              <br />
            <asp:Label ID="lblCost" runat="server" Text=""></asp:Label>
              <br />
              <br />
            <asp:Label ID="lblSubtotal" runat="server" Text=""></asp:Label>
            <br />
            <br />
                           
                 
              <asp:Button ID="btnPay" runat="server" Text="Pay" Visible="false"/>
              
              <br />
            <br />
            <asp:Label ID="lblUpgrade" runat="server" Text="Would you like to upgrade for 500 miles?" Visible="false"></asp:Label>

                                       
                 
              <asp:Button ID="btnYes" runat="server" Text="Yes" visible="false"/>
              
                                         
                 
              <asp:Button ID="btnNo" runat="server" Text="No" visible="false"/>
              
              

               
                 
              </div> 

        <asp:Panel runat="server" ID="pnlSeats" Visible ="false">
        
         <div class="pull-left" style="text-align:center; width: 225px">
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


      <div class="pull-left" style="text-align: center; width: 225px">
                     
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

            </asp:Panel>

           
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
