<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cust_CreateReservationAndSelectFlight.aspx.vb" Inherits="Cust_CreateReservationAndSelectFlight" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <div style="text-align:center">
    <h1>Create Reservation</h1>
    </div>
    
        <asp:Panel ID="pnlTripType" runat="server">
            <h2>Select Trip Type</h2>
            
            <asp:RadioButtonList ID="rblTrip" runat="server">
                <asp:ListItem Value="One Way">One Way</asp:ListItem>
                <asp:ListItem Value="Round Trip">Round Trip</asp:ListItem>
                <asp:ListItem Value="Multiple City">Multiple City</asp:ListItem>
            </asp:RadioButtonList>
            </asp:Panel>
        
    <asp:Panel CssClass="panel" ID="pnlPeopleType" runat="server">
       <h2>Select Number on Reservation</h2>
           <div class="pull-left"  style="word-spacing: 0px; width:150px">
<asp:Label CssClass="h6" ID="lblAdult" runat="server" Text="# of People age 13+"></asp:Label>
                    <div id ="content-textboxes">
                                <asp:DropDownList ID="ddlAdult" runat="server">
                                    <asp:ListItem>0</asp:ListItem>
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
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                </asp:DropDownList>
                </div>
                </div>
                       
                <div class="pull-left"  style="word-spacing: 0px; width:150px">
                     <asp:Label ID="lblChildren" runat="server" Text="# of People age 3-12"></asp:Label>
            
            <div id ="Div3">
                                <asp:DropDownList ID="ddlChildren" runat="server">
                                    <asp:ListItem>0</asp:ListItem>
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
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                </asp:DropDownList>
                </div>
            </div>
    <div class="pull-left"  style="word-spacing: 0px; width:250px">
                                 <asp:Label ID="lblBabies" runat="server" Text="# of People age 2 and under"></asp:Label>
            <div id ="Div6">
                                <asp:DropDownList ID="ddlBabies" runat="server">
                                    <asp:ListItem>0</asp:ListItem>
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
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                </asp:DropDownList>
                </div>
            </div>
            </asp:Panel>
            
                <asp:Panel CssClass="panel" ID="pnlCities" runat="server" Width="100%">
                    <br />
                    <h2>Select Your Citites</h2>
                                <div id="ddl-left">
                <asp:DropDownList ID="ddlDepartureCity" runat="server"></asp:DropDownList>
                <asp:SqlDataSource ID="AirportCodes" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_20142_Team06ConnectionString %>" SelectCommand="SELECT [CityName], [AirportCode] FROM [tblAirport]"></asp:SqlDataSource>
            </div>
            <div id="ddl-middle">
                <asp:Label ID="lblDivider" runat="server" Text="To"></asp:Label>
            </div>
            <div id="ddl-right">
                <asp:DropDownList ID="ddlArrivalCity" runat="server"></asp:DropDownList>
                <asp:SqlDataSource ID="ArrivalDDL" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_20142_Team06ConnectionString2 %>" SelectCommand="SELECT [AirportCode], [CityName] FROM [tblAirportClone]"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
            </div>
                </asp:Panel>
            <br />

                <div style="text-align:center; word-spacing:30px">
                <asp:Button ID="btnAddJourney" cssclass="btn btn-primary" runat="server" Text="Add this leg" />
                    <asp:Button CssClass="btn btn-primary" ID="btnFinalLeg" runat="server" Text="Add Last Leg" visible="false"/>
                <br />
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </div>
             
            </asp:Content>

            