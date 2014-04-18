<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cust_CreateReservationAndSelectFlight.aspx.vb" Inherits="Cust_CreateReservationAndSelectFlight" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div id ="middle">
            <br />
            <asp:RadioButtonList ID="rblTrip" runat="server">
                <asp:ListItem Value="One Way">One Way</asp:ListItem>
                <asp:ListItem Value="Round Trip">Round Trip</asp:ListItem>
                <asp:ListItem Value="Multiple City">Multiple City</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <br />
            <asp:Button CssClass="btn" ID="btnFinalLeg" runat="server" Text="Add as Final Leg" style="height: 26px" visible="false"/>
            <br />
            <br />
            <br />
            <asp:Button ID="btnAddJourney" runat="server" Text="Add this leg" />
            <br />
            <br />
            <br />
            <br />
            <asp:LinkButton ID="lnkHome" runat="server" PostBackUrl="~/index.aspx" CausesValidation="False">Home</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lnkShowAll" runat="server" PostBackUrl="~/ShowAll.aspx" CausesValidation="False">Show All Customers</asp:LinkButton>
        </div>
   
        <div id ="content">
            <div id ="content-labels">
                <asp:Label ID="lblAdult" runat="server" Text="# of People age 13+"></asp:Label>
            </div>
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
            <br />
             <div id ="Div1">
            <div id ="Div2">
                <asp:Label ID="lblChildren" runat="server" Text="# of People age 3-12"></asp:Label>
            </div>
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

                 <br /> 

                 <div id ="Div4">
            <div id ="Div5">
                <asp:Label ID="lblBabies" runat="server" Text="# of People age 2 and under"></asp:Label>
            </div>
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
            <br />
            <br />

            <div id="ddl">
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
            <br />
                <br />
            <br />
                

            </div>
            </div>
            </asp:Content>

            