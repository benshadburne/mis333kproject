<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cust_CreateReservationAndSelectFlight.aspx.vb" Inherits="Cust_CreateReservationAndSelectFlight" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div id ="sidebar">
            <br />
            <br />
            <br />
            <asp:Button CssClass="btn" ID="btnCreateReservation" runat="server" Text="Create Reservation" style="height: 26px" />
            <br />
            <br />
            <br />
            <asp:Button ID="btnAddJourney" runat="server" Text="Add this leg" />
            <br />
            <br />
            <br />
            <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" />
            <br />
            <br />
            <asp:LinkButton ID="lnkHome" runat="server" PostBackUrl="~/index.aspx" CausesValidation="False">Home</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lnkShowAll" runat="server" PostBackUrl="~/ShowAll.aspx" CausesValidation="False">Show All Customers</asp:LinkButton>
        </div>
   
        <div id ="content">
            <div id ="content-labels">
                <asp:Label ID="lblPeopleCount" runat="server" Text="# of People?:"></asp:Label>
            </div>
            <div id ="content-textboxes">
                <asp:TextBox CssClass="text-left" ID="txtPeopleCount" runat="server"></asp:TextBox>
                
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

            <asp:Panel ID="Panel1" runat="server">
            <br />
            <br />
            <br />
            <br />
            <br />

            <div id="ddl">
            <div id="ddl-left">
                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="AirportCodes"></asp:DropDownList>
            </div>
            <div id="ddl-middle">
                <asp:Label ID="Label1" runat="server" Text="To"></asp:Label>
            </div>
            <div id="ddl-right">
                <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="AirportCodes"></asp:DropDownList>
            </div>
            </asp:Panel>

            <asp:Panel ID="Panel2" runat="server">
            <br />
            <br />
            <br />
            <br />
            <br />

            <div id="ddl">
            <div id="ddl-left">
                <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="AirportCodes"></asp:DropDownList>
            </div>
            <div id="ddl-middle">
                <asp:Label ID="Label2" runat="server" Text="To"></asp:Label>
            </div>
            <div id="ddl-right">
                <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="AirportCodes"></asp:DropDownList>
            </div>
            </asp:Panel>

            <asp:Panel ID="Panel3" runat="server">
            <br />
            <br />
            <br />
            <br />
            <br />

            <div id="ddl">
            <div id="ddl-left">
                <asp:DropDownList ID="DropDownList5" runat="server" DataSourceID="AirportCodes"></asp:DropDownList>
            </div>
            <div id="ddl-middle">
                <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
            </div>
            <div id="ddl-right">
                <asp:DropDownList ID="DropDownList6" runat="server" DataSourceID="AirportCodes"></asp:DropDownList>
            </div>
            </asp:Panel>

        </div>
        
</asp:Content>

