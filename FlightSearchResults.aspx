<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="FlightSearchResults.aspx.vb" Inherits="_Default" %>

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
             <asp:Calendar ID="calFlightSearch" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px">
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
           
        <div class="pull-left" style="width: 20%; min-width:40px; height: 67px;">
        <asp:Label class="label" ID="Label2" runat="server" Text="Time" ForeColor="Black" Font-Bold="true" Font-Size="small"></asp:Label>
          
        <br />
    <br />
   

  
        <asp:DropDownList class="dropdown" ID="ddlTimeOfDay" runat="server">
            <asp:ListItem>200</asp:ListItem>
            <asp:ListItem>100</asp:ListItem>
            <asp:ListItem>400</asp:ListItem>
            <asp:ListItem>300</asp:ListItem>
            <asp:ListItem>500</asp:ListItem>
            <asp:ListItem>600</asp:ListItem>
            <asp:ListItem>700</asp:ListItem>
            <asp:ListItem>800</asp:ListItem>
            <asp:ListItem>900</asp:ListItem>
            <asp:ListItem>1000</asp:ListItem>
            <asp:ListItem>1100</asp:ListItem>
            <asp:ListItem>1200</asp:ListItem>
            <asp:ListItem>1300</asp:ListItem>
            <asp:ListItem>1400</asp:ListItem>
            <asp:ListItem>1500</asp:ListItem>
            <asp:ListItem>1600</asp:ListItem>
            <asp:ListItem>1700</asp:ListItem>
            <asp:ListItem>1800</asp:ListItem>
            <asp:ListItem>1900</asp:ListItem>
            <asp:ListItem>2000</asp:ListItem>
            <asp:ListItem>2100</asp:ListItem>
            <asp:ListItem>2200</asp:ListItem>
            <asp:ListItem>2300</asp:ListItem>
            <asp:ListItem>2400</asp:ListItem>
        </asp:DropDownList>

<%--       
        <asp:Label class="label" ID="Label1" runat="server" Text="  :  " ForeColor="Black" ></asp:Label>
       

 
        <asp:DropDownList class="dropdown" ID="ddlFifteenInterval" runat="server">
            <asp:ListItem>00</asp:ListItem>
             <asp:ListItem>15</asp:ListItem>
            <asp:ListItem>30</asp:ListItem>
            <asp:ListItem>45</asp:ListItem>
                    </asp:DropDownList>--%>

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
              <asp:DropDownList class="dropdown" ID="ddlDepart" runat="server" DataSourceID="AirportCode" DataTextField="AirportCode" DataValueField="AirportCode"></asp:DropDownList>
             <asp:SqlDataSource ID="AirportCode" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_20142_Team06ConnectionString2 %>" SelectCommand="SELECT [AirportCode] FROM [tblAirport]"></asp:SqlDataSource>
        </div>

        <div class="pull-left" style="width: 30%; min-width:40px; height: 67px;">
             <asp:Label class="label" ID="Label5" runat="server" Text="Arrival City" ForeColor="Black" Font-Size="Small"></asp:Label>
            <br/> 
              <br/> 
            <asp:DropDownList class="dropdown" ID="ddlArrival" runat="server" DataSourceID="AirportCode" DataTextField="AirportCode" DataValueField="AirportCode"></asp:DropDownList>
             <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_20142_Team06ConnectionString2 %>" SelectCommand="SELECT [AirportCode] FROM [tblAirport]"></asp:SqlDataSource>
        </div>

            </div>
            
    <br/> 
        
   
    <div class="center-block" style="width:90%; min-height:100px;">
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label CssClass="label" ID="Label6" runat="server" Text="Direct Flights"></asp:Label>
        <div class="pull-right">
            <asp:Label CssClass="label" ID="lblCountDirect" runat="server" Text="Count: "></asp:Label></div>
         <br />
         <asp:GridView class="table" ID="gvDirectFlights" runat="server" AutoGenerateSelectButton="True">
        </asp:GridView>
       
        
              
    <br />
    <br />
    
     <asp:Label CssClass="label" ID="lblIndirect" runat="server" Text="Indirect Flights" Visible="false"></asp:Label>
        <div class="pull-right">
            <asp:Label CssClass="label" ID="lblCountIndirect" runat="server" Text="Count:" Visible="false"></asp:Label></div>
         <br />
         <asp:GridView class="table" ID="gvIndirectStart" runat="server" Visible="false" AutoGenerateSelectButton="True"></asp:GridView>
       
         <br />
         <asp:GridView class="table" ID="gvIndirectFinish" runat="server" Visible="false" AutoGenerateSelectButton="True"></asp:GridView>
       
         </div>      
     
</asp:Content>

