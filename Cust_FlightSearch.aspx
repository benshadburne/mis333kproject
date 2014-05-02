<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cust_FlightSearch.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        

    <div class="center-block" style="width: 90%; padding-top: 20px;">
        
        <asp:Panel ID="Panel1" runat="server" width="100%">
        
          
            </asp:Panel>
    </div>

    <br />
    <br />
    <div class="center-block" style="width: 90%; padding-top: 20px; height:240px; min-width: 600px">

     
         <div class="pull-left" style="width: 37%; min-width:40px">
        <asp:Label class="h2" ID="Label3" runat="server" Text="Choose Date" ForeColor="Black"></asp:Label>
            <br/> 
              <br/> 
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
             
        </div>
           
        <div class="pull-left" style="width: 20%; min-width:40px; height: 67px;">
          
        <br />
             <asp:Label class="h5" ID="lblReturn" runat="server" Text="Select Return Ticket" ForeColor="Black" Visible ="false"></asp:Label>
    <br />
   

  
<%--<asp:RadioButtonList class="radio-inline" ID="RadioButtonList1" runat="server">
            <asp:ListItem>AM</asp:ListItem>
            <asp:ListItem>PM</asp:ListItem>
        </asp:RadioButtonList>--%>

            </div>
   <div class="pull-left" style="width: 30%; min-width:40px; height: 67px; margin-top:30px;">
        <%--<asp:RadioButtonList class="radio-inline" ID="RadioButtonList1" runat="server">
            <asp:ListItem>AM</asp:ListItem>
            <asp:ListItem>PM</asp:ListItem>
        </asp:RadioButtonList>--%>
       </div>
 

        <div class="pull-left" style="width: 30%; min-width:40px; height: 67px;">
             <asp:Label class="h6" ID="Label4" runat="server" Text="Departing City" ForeColor="Black"></asp:Label>
            <br/> 
              <br/> 
              <asp:Label ID="lblDeparture" runat="server"></asp:Label>
        </div>

        <div class="pull-left" style="width: 30%; min-width:40px; height: 67px;">
             <asp:Label class="h6" ID="Label5" runat="server" Text="Arrival City" ForeColor="Black"></asp:Label>
            <br/> 
              <br />
             <asp:Label ID="lblArrival" runat="server"></asp:Label>
              <br/> 
        </div>
        <div class="pull-left" style="width: 30%; min-width:40px; height: 67px;">
            <br/> 
              <br/> 
        </div>
            </div>
            
    <br/> 
        
   <div  style="text-align:center">
       <asp:Button class="btn btn-primary" ID="btnSearch" runat="server" Text="Search" />
   </div>
    <div class="center-block" style="width:90%; min-height:100px;">
        <asp:Label Class="h6" ID="lblMessage" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label CssClass="h2" ID="Label6" runat="server" Text="Direct Flights"></asp:Label>
        <div class="pull-right">
             <asp:Label CssClass="h6" ID="Label1" runat="server" Text="Count: "></asp:Label>
            <asp:Label CssClass="h6" ID="lblCountDirect" runat="server" Text=""></asp:Label></div>
         <br />
         <asp:GridView class="table" ID="gvDirectFlights" runat="server" AutoGenerateSelectButton="True">
        </asp:GridView>
       
        
              
    <br />
    <br />
    
     <asp:Label CssClass="h2" ID="lblIndirectStart" runat="server" Text="Indirect Flights: First Leg" Visible="true"></asp:Label>
        &nbsp;<div class="pull-right" id="StartCount">
            <asp:Label CssClass="h6" ID="lblIndirectStartC" runat="server" Text="Count: " Visible="true"></asp:Label>
            <asp:Label CssClass="h6" ID="lblCountIndirect" runat="server" Text="" Visible="true"></asp:Label></div>
         <br />

         <asp:GridView class="table" ID="gvIndirectStart" runat="server" Visible="true" AutoGenerateSelectButton="True"></asp:GridView>
       <br /><asp:Label CssClass="h2" ID="lblIndirectFinish" runat="server" Text="Indirect Flights: Second Leg" Visible="false"></asp:Label>
        <div class="pull-right">

            <asp:Label CssClass="h6" ID="lblIndirectFinishC" runat="server" Text="Count: " Visible="false"></asp:Label>

            <asp:Label CssClass="h6" ID="lblCountFinish" runat="server" Text="" Visible="false"> </asp:Label>

        </div>
        
         <asp:GridView class="table" ID="gvIndirectFinish" runat="server" Visible="false" AutoGenerateSelectButton="True"></asp:GridView>
    
        
            
         <br />
        <br />
    
        
            
         </div> 
    <div style="text-align:center">      
    <asp:Label CssClass="h6" ID="lblBack" runat="server" Text="Can't find a flight?"></asp:Label>
    <br />

    <asp:Button CssClass="btn btn-primary" ID="btnBack" runat="server" Text="Choose Different Route" />
    <br />
    <br />

    <asp:Label CssClass="h6" ID="Label2" runat="server" Text="Made a mistake? Cancel your reservation by clicking below"></asp:Label>
    <br />

    <asp:Button CssClass="btn btn-danger" ID="btnCancel" runat="server" Text="Cancel Reservation" />
        </div>
</asp:Content>

