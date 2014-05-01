<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="HomePage.aspx.vb" Inherits="_Default" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="center-block" style="width:900px">
        <div style="align-content:center; text-align:center; width:100%;padding-top:5px;padding-bottom:20px">
        <h1>Welcome to Penguin United Airlines</h1>
        </div>
    <div id="myCarousel" class="carousel slide" data-ride="carousel">
      <!-- Indicators -->
      <ol class="carousel-indicators">
        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
        <li data-target="#myCarousel" data-slide-to="1"></li>
        <li data-target="#myCarousel" data-slide-to="2"></li>
          <li data-target="#myCarousel" data-slide-to="3"></li>
      </ol>
      <div class="carousel-inner">
        <div class="item active">
            <img src="Z_penguin%20airline.png" />
          <div class="container">
            <div class="carousel-caption">
             <p>
                 <asp:Button CssClass="btn-primary " ID="btn1" runat="server" Text="Log in Now" /></p>
            </div>
          </div>
        </div>
        <div class="item">
            <img src="Z_penguin%20airplane.png" />
          <div class="container">
            <div class="carousel-caption">
             <p>
                 <asp:Button CssClass="btn-primary " ID="btn2" runat="server" Text="Log in Now" /></p>
            </div>
          </div>
        </div>
        
        <div class="item">
            <img src="Z_flyingpenguin.jpg" />
          <div class="container">
            <div class="carousel-caption">
             <p>
                 <asp:Button CssClass="btn-primary " ID="btn3" runat="server" Text="Log in Now" /></p>
            </div>
          </div>
        </div>
        <div class="item">
            <img src="Z_Penguin%20image.jpg" />
          <div class="container">
            <div class="carousel-caption">
             <p>
                 <asp:Button CssClass="btn-primary " ID="btn4" runat="server" Text="Log in Now" /></p>
            </div>
          </div>
        </div>
      </div>
      <a class="left carousel-control" href="#myCarousel" data-slide="prev"><span class="glyphicon glyphicon-chevron-left"></span></a>
      <a class="right carousel-control" href="#myCarousel" data-slide="next"><span class="glyphicon glyphicon-chevron-right"></span></a>
    </div><!-- /.carousel -->
        </div>
</asp:Content>

