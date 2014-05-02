<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_AddCity.aspx.vb" Inherits="Emp_AddCity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <div class="center-align" style="padding-bottom:20px">
    <h1>Add Airport</h1>
       </div>
    
   <div style="line-height:28px">
        <asp:Panel CssClass="panel"  ID="pnlAddAirport" runat="server">
        <div class="pull-left" style="width:200px; text-align:right; padding-right:5px">
        <asp:Label CssClass="h6" ID="Label1" runat="server" Text="City name:"></asp:Label>
        <br />
        <asp:Label CssClass="h6" ID="Label2" runat="server" Text="Airport Code:"></asp:Label>
            <br />
            <asp:Label CssClass="h6" ID="Label7" runat="server" Text="State:"></asp:Label>
        </div>
        <div class="pull-left" style="width:220px; text-align:left">
            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ErrorMessage="The city name is a required field." Text="*" ControlToValidate="txtCity" ForeColor="Red"></asp:RequiredFieldValidator>
        <br /> 
        <asp:TextBox ID="txtAirport" runat="server" MaxLength="3"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAirport" runat="server" ErrorMessage="The airport code must be 3 letters." Text="*" ControlToValidate="txtAirport" ForeColor="Red"></asp:RequiredFieldValidator>
        <br /> 
        <asp:TextBox ID="txtState" runat="server" MaxLength="2"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvState" runat="server" ErrorMessage="Enter a 2 letter US State Abbreviation" Text="*" ControlToValidate="txtState" ForeColor="Red"></asp:RequiredFieldValidator>
        <br /> 
        </div>
            <asp:Label CssClass="h6" ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
   <br />
        <asp:Button CssClass="btn btn-primary" ID="btnAdd" runat="server" Text="Add" />
    </asp:Panel>
        </div>
       
    <div style="line-height:28px">
        <asp:Panel CssClass="panel"  ID="pnlAddInfo" visible="false" runat="server">
        <div class="pull-left" style="width:200px; text-align:right; padding-right:5px">
            <asp:Label CssClass="h6" ID="Label3" runat="server" Text="Mileage:"></asp:Label>
        <br />
            <br />
            <asp:Label CssClass="h6" ID="Label4" runat="server" Text="Flight Time:"></asp:Label>
            <br />
            <asp:Label CssClass="h6" ID="lblAirport" runat="server" Text=""></asp:Label>
        </div>
        <div class="pull-left" style="width:220px; text-align:left">
            <asp:TextBox ID="txtMileage" runat="server" MaxLength="5"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMileage" ErrorMessage="The city name is a required field." ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
        <br /> 
            <asp:Label CssClass="h6" ID="Label5" runat="server" Text="Hours"></asp:Label>
            <asp:Label CssClass="h6" ID="Label6" runat="server" Text="Minutes"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlHours" runat="server">
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
            </asp:DropDownList>
            <asp:DropDownList ID="ddlMinutes" runat="server">
                <asp:ListItem Value="00">0</asp:ListItem>
                <asp:ListItem Value="01">1</asp:ListItem>
                <asp:ListItem Value="02">2</asp:ListItem>
                <asp:ListItem Value="03">3</asp:ListItem>
                <asp:ListItem Value="04">4</asp:ListItem>
                <asp:ListItem Value="05">5</asp:ListItem>
                <asp:ListItem Value="06">6</asp:ListItem>
                <asp:ListItem Value="07">7</asp:ListItem>
                <asp:ListItem Value="08">8</asp:ListItem>
                <asp:ListItem Value="09">9</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
                <asp:ListItem>13</asp:ListItem>
                <asp:ListItem>14</asp:ListItem>
                <asp:ListItem>15</asp:ListItem>
                <asp:ListItem>16</asp:ListItem>
                <asp:ListItem>17</asp:ListItem>
                <asp:ListItem>18</asp:ListItem>
                <asp:ListItem>19</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>21</asp:ListItem>
                <asp:ListItem>22</asp:ListItem>
                <asp:ListItem>23</asp:ListItem>
                <asp:ListItem>24</asp:ListItem>
                <asp:ListItem>25</asp:ListItem>
                <asp:ListItem>26</asp:ListItem>
                <asp:ListItem>27</asp:ListItem>
                <asp:ListItem>28</asp:ListItem>
                <asp:ListItem>29</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>31</asp:ListItem>
                <asp:ListItem>32</asp:ListItem>
                <asp:ListItem>33</asp:ListItem>
                <asp:ListItem>34</asp:ListItem>
                <asp:ListItem>35</asp:ListItem>
                <asp:ListItem>36</asp:ListItem>
                <asp:ListItem>37</asp:ListItem>
                <asp:ListItem>38</asp:ListItem>
                <asp:ListItem>39</asp:ListItem>
                <asp:ListItem>40</asp:ListItem>
                <asp:ListItem>41</asp:ListItem>
                <asp:ListItem>42</asp:ListItem>
                <asp:ListItem>43</asp:ListItem>
                <asp:ListItem>44</asp:ListItem>
                <asp:ListItem>45</asp:ListItem>
                <asp:ListItem>46</asp:ListItem>
                <asp:ListItem>47</asp:ListItem>
                <asp:ListItem>48</asp:ListItem>
                <asp:ListItem>49</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>51</asp:ListItem>
                <asp:ListItem>52</asp:ListItem>
                <asp:ListItem>53</asp:ListItem>
                <asp:ListItem>54</asp:ListItem>
                <asp:ListItem>55</asp:ListItem>
                <asp:ListItem>56</asp:ListItem>
                <asp:ListItem>57</asp:ListItem>
                <asp:ListItem>59</asp:ListItem>
                <asp:ListItem>59</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Label CssClass="h6" ID="lblAirportMessage" runat="server" Forecolor="red" Text=""></asp:Label>
        <br /> 
        </div>
            <asp:Label CssClass="h6" ID="Label9" runat="server" Text="" ForeColor="Red"></asp:Label>
   <br />
            <asp:Button class="btn btn-primary" ID="btnAddInfo" runat="server" Text="Add Info" />
</asp:Panel>
        </div>


    
</asp:Content>

