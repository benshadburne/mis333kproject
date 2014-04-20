<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cust_FlightSearch.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        

    <div class="center-block" style="width: 90%; padding-top: 20px;">
        
        <asp:Panel ID="Panel1" runat="server" width="100%">
        <div class="pull-left" style="width: 90px"> <asp:Button class="btn" ID="btnSearch" runat="server" Text="Search" />

       </div>
          
           <div class="pull-left" style="width: 40px; margin-left: 5px">
                 <asp:Button class="btn" ID="btnShowAll" runat="server" Text="Show All" />
                  
        </div>
            </asp:Panel>
    </div>

    <br />
    <br />
    <div class="center-block" style="width: 90%; padding-top: 20px; height:240px; min-width: 600px">

     
         <div class="pull-left" style="width: 37%; min-width:40px">
        <asp:Label class="label" ID="Label3" runat="server" Text="Date" ForeColor="Black" Font-Size="Small"></asp:Label>
            <br/> 
              <br/> 
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
             <asp:Label ID="lblFilter" runat="server" Text="lblFilter"></asp:Label>
        </div>
           
        <div class="pull-left" style="width: 20%; min-width:40px; height: 67px;">
          
        <br />
             <asp:Label class="label" ID="lblReturn" runat="server" Text="Select Return Ticket" ForeColor="Black" Font-Size="Small" Visible ="false"></asp:Label>
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
             <asp:Label class="label" ID="Label4" runat="server" Text="Departing City" ForeColor="Black" Font-Size="Small"></asp:Label>
            <br/> 
              <br/> 
              <asp:Label ID="lblDeparture" runat="server"></asp:Label>
        </div>

        <div class="pull-left" style="width: 30%; min-width:40px; height: 67px;">
             <asp:Label class="label" ID="Label5" runat="server" Text="Arrival City" ForeColor="Black" Font-Size="Small"></asp:Label>
            <br/> 
              <br />
             <asp:Label ID="lblArrival" runat="server"></asp:Label>
              <br/> 
        </div>

            </div>
            
    <br/> 
        
   
    <div class="center-block" style="width:90%; min-height:100px;">
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label CssClass="label" ID="Label6" runat="server" Text="Direct Flights"></asp:Label>
        <div class="pull-right">
             <asp:Label CssClass="label" ID="Label1" runat="server" Text="Count: "></asp:Label>
            <asp:Label CssClass="label" ID="lblCountDirect" runat="server" Text=""></asp:Label></div>
         <br />
         <asp:GridView class="table" ID="gvDirectFlights" runat="server" AutoGenerateSelectButton="True">
        </asp:GridView>
       
        
              
    <br />
    <br />
    
     <asp:Label CssClass="label" ID="lblIndirectStart" runat="server" Text="Indirect Flights: First Leg" Visible="true"></asp:Label>
        &nbsp;<div class="pull-right" id="StartCount">
            <asp:Label CssClass="label" ID="lblIndirectStartC" runat="server" Text="Count: " Visible="true"></asp:Label>
            <asp:Label CssClass="label" ID="lblCountIndirect" runat="server" Text="" Visible="true"></asp:Label></div>
         <br />

         <asp:GridView class="table" ID="gvIndirectStart" runat="server" Visible="true" AutoGenerateSelectButton="True"></asp:GridView>
       <br /><asp:Label CssClass="label" ID="lblIndirectFinish" runat="server" Text="Indirect Flights: Second Leg" Visible="false"></asp:Label>
        <div class="pull-right">

            <asp:Label CssClass="label" ID="lblIndirectFinishC" runat="server" Text="Count: " Visible="false"></asp:Label>

            <asp:Label CssClass="label" ID="lblCountFinish" runat="server" Text="" Visible="false"> </asp:Label>

        </div>
        
         <asp:GridView class="table" ID="gvIndirectFinish" runat="server" Visible="false" AutoGenerateSelectButton="True"></asp:GridView>
    
        
            
         </div>       
     
</asp:Content>

