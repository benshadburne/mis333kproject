<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="FlightSearchResults.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    

    <div class="center-block" style="width: 79%; padding-top: 20px;">
        
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
    <div class="center-block" style="width: 79%; padding-top: 20px; height:100px; min-width: 600px">

     
         <div class="pull-left" style="width: 20%; min-width:40px">
        <asp:Label class="label" ID="Label3" runat="server" Text="Day of Week" ForeColor="Black" Font-Size="Small"></asp:Label>
            <br/> 
              <br/> 
             <asp:DropDownList class="dropdown" ID="DropDownList1" runat="server">
                 <asp:ListItem Selected="True">Monday</asp:ListItem>
                 <asp:ListItem>Tuesday</asp:ListItem>
                 <asp:ListItem>Wednesday</asp:ListItem>
                 <asp:ListItem>Thursday</asp:ListItem>
                 <asp:ListItem>Friday</asp:ListItem>
                 <asp:ListItem>Saturday</asp:ListItem>
                 <asp:ListItem>Sunday</asp:ListItem>
             </asp:DropDownList>
        </div>
           
        <div class="pull-left" style="width: 20%; min-width:40px; height: 67px;">
        <asp:Label class="label" ID="Label2" runat="server" Text="Time" ForeColor="Black" Font-Bold="true" Font-Size="small"></asp:Label>
          
        <br />
    <br />
   

  
        <asp:DropDownList class="dropdown" ID="ddlTimeOfDay" runat="server">
            <asp:ListItem>12</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
        </asp:DropDownList>

       
        <asp:Label class="label" ID="Label1" runat="server" Text="  :  " ForeColor="Black" ></asp:Label>
       

 
        <asp:DropDownList class="dropdown" ID="ddlFifteenInterval" runat="server">
            <asp:ListItem>00</asp:ListItem>
             <asp:ListItem>15</asp:ListItem>
            <asp:ListItem>30</asp:ListItem>
            <asp:ListItem>45</asp:ListItem>
                    </asp:DropDownList>

            </div>
   <div class="pull-left" style="width: 12%; min-width:40px; min-height: 67px; margin-top:30px;">
        <asp:RadioButtonList class="radio-inline" ID="RadioButtonList1" runat="server">
            <asp:ListItem Selected="True">AM</asp:ListItem>
            <asp:ListItem>PM</asp:ListItem>
        </asp:RadioButtonList>
       </div>
 

        <div class="pull-left" style="width: 20%; min-width:40px; height: 67px;">
             <asp:Label class="label" ID="Label4" runat="server" Text="Departing City" ForeColor="Black" Font-Size="Small"></asp:Label>
            <br/> 
              <br/> 
              <asp:DropDownList class="dropdown" ID="ddlDepart" runat="server" DataSourceID="AirportCode" DataTextField="AirportCode" DataValueField="AirportCode"></asp:DropDownList>
             <asp:SqlDataSource ID="AirportCode" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_20142_Team06ConnectionString2 %>" SelectCommand="SELECT [AirportCode] FROM [tblAirport]"></asp:SqlDataSource>
        </div>

        <div class="pull-left" style="width: 20%; min-width:40px; height: 67px;">
             <asp:Label class="label" ID="Label5" runat="server" Text="Arrival City" ForeColor="Black" Font-Size="Small"></asp:Label>
            <br/> 
              <br/> 
            <asp:DropDownList class="dropdown" ID="ddlArrival" runat="server" DataSourceID="AirportCode" DataTextField="AirportCode" DataValueField="AirportCode"></asp:DropDownList>
             <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_20142_Team06ConnectionString2 %>" SelectCommand="SELECT [AirportCode] FROM [tblAirport]"></asp:SqlDataSource>
        </div>

            </div>
            

        
   
    <div class="center-block" style="width:79%;">
       
         </div>
                    

</asp:Content>

