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
            <asp:Label ID="Label15" runat="server" Text="To pay, please select a ticket and then indicate if you would prefer to pay with miles or money"></asp:Label>
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
              <asp:Button ID="btnOverride" runat="server" Text="Override Price" visible="false"/>
              
            <asp:TextBox ID="txtOverride" runat="server" Text ="" Visible ="false"></asp:TextBox>
                 
            <br />
            <asp:Label ID="lblUpgrade" runat="server" Text="Would you like to upgrade for 500 miles?" Visible="false"></asp:Label>

                                       
                 
              <asp:Button ID="btnYes" runat="server" Text="Yes" visible="false"/>
              
              <asp:Button ID="btnNo" runat="server" Text="No" visible="false"/>
            <br /> 
            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" visible="false"/>
              
              

               
                 
              </div> 

        <asp:Panel runat="server" ID="pnlSeats" Visible="False">
            <div class="center-block">
                <h6>Legend:</h6> 
               <div class="pull-left" style="padding-right:20px">
                   <asp:Label CssClass="h6" ID="Label4" runat="server" Text="Active Seat"></asp:Label><asp:Button CssClass="btn-sm" ID="Button1" runat="server" Text="XX" BackColor="Green" />
               </div>
                <div class="pull-left" style="padding-right:20px">
                   <asp:Label CssClass="h6" ID="Label10" runat="server" Text="On Reservation"></asp:Label><asp:Button CssClass="btn-sm" ID="Button2" runat="server" Text="XX" BackColor="Blue" />
               </div>
                <div class="pull-left" style="padding-right:20px">
                   <asp:Label CssClass="h6" ID="Label11" runat="server" Text="Not on Reservation"></asp:Label><asp:Button CssClass="btn-sm" ID="Button3" runat="server" Text="XX" BackColor="Coral" />
               </div>
                <div class="pull-left" style="padding-right:20px">
                   <asp:Label CssClass="h6" ID="Label12" runat="server" Text="Empty Seat"></asp:Label><asp:Button CssClass="btn-sm" ID="Button4" runat="server" Text="XX" BackColor="LightGrey" />
               </div>
                <div class="pull-left" style="padding-right:20px">
                   <asp:Label CssClass="h6" ID="Label13" runat="server" Text="Infant in Seat"></asp:Label><asp:Button CssClass="btn-sm" ID="Button5" runat="server" Text="XXi" BackColor="LightGrey" />
               </div>
                <div class="pull-left">
                   <asp:Label CssClass="h6" ID="Label14" runat="server" Text="Infant is user"></asp:Label><asp:Button CssClass="btn-sm" ID="Button6" runat="server" Text="XXi*" BackColor="Green" />
               </div> 
            </div>
        
         <div class="pull-left" style="text-align:center; width: 225px">
                <asp:Button class="btn-seat" ID="btn1A" runat="server" Text="1A" />
              <br />
              <asp:Button class="btn-seat" ID="btn2A" runat="server" Text="2A" />
              <br />
              <asp:Button class="btn-seat" ID="btn3A" runat="server" Text="3A" Enabled ="false"/>
              <asp:Button class="btn-seat" ID="btn3B" runat="server" Text="3B" Enabled="false" />
              <br />
              <asp:Button class="btn-seat" ID="btn4A" runat="server" Text="4A" Enabled ="false"/>
              <asp:Button class="btn-seat" ID="btn4B" runat="server" Text="4B" Enabled ="false"/>
              <br />
              <asp:Button class="btn-seat" ID="btn5A" runat="server" Text="5A" Enabled ="false" />
              <asp:Button class="btn-seat" ID="btn5B" runat="server" Text="5B" Enabled ="false"/>
                <br />
                 
              </div>


      <div class="pull-left" style="text-align: center; width: 225px">
                     
              <asp:Button class="btn-seat" ID="btn1B" runat="server" Text="1B" />
              <br />
              <asp:Button class="btn-seat" ID="btn2B" runat="server" Text="2B" />
              <br />
              <asp:Button class="btn-seat" ID="btn3C" runat="server" Text="3C" Enabled ="false"/>
              <asp:Button class="btn-seat" ID="btn3D" runat="server" Text="3D" Enabled ="false"/>
              <br />
              <asp:Button class="btn-seat" ID="btn4C" runat="server" Text="4C" Enabled ="false"/>
              <asp:Button class="btn-seat" ID="btn4D" runat="server" Text="4D" Enabled ="false"/>
              <br />
              <asp:Button class="btn-seat" ID="btn5C" runat="server" Text="5C" Enabled ="false"/>
              <asp:Button class="btn-seat" ID="btn5D" runat="server" Text="5D" Enabled ="false"/>
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
      
       </div>
        <ul>
         
            </ul> 

                 <div class="center-block" style="width:100%">
        <asp:Label CssClass="h5" ID="Label8" runat="server" Text="Need to cancel this reservation?"></asp:Label>
       </div>
        <ul>
            <asp:Button ID="btnCancel" runat="server" Text="Cancel Reservation" />
            </ul> 
            </div>
</asp:Content>

