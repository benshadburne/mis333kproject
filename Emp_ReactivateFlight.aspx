<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_ReactivateFlight.aspx.vb" Inherits="Emp_ReactivateFlight" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="header" style="text-align: center;">

        <h1>
            Re-Activate a Journey
        </h1>

    </div>

    <br />
    <br />

    <div class ="center-block">
    <div class ="pull-left" style="width:20%;">
           
        <asp:Button ID="btnReactivate" runat="server" Text="Reactivate" /><br />
        <asp:Button ID="btnAbort" runat="server" Text="Abort" Visible="False" /><br />
        <asp:Button ID="btnAccept" runat="server" Text="Accept" Visible="False" /><br />
        <asp:Label ID="lblMessage" runat="server" Text="You currently do not have a selected journey."></asp:Label><br />
        <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
    </div>
   
        <div class ="center-block" style="width: 70%;">
            <div id ="pull-right" style="width: 15%; float:left; text-align: right; line-height: 200%">
               
                
            </div>
            <div id ="pull-left" style="width: 70%; float: left; line-height: 160%">
                
                <asp:GridView ID="gvFlights" runat="server">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
                
            </div>
            </div>
        </div>
</asp:Content>
