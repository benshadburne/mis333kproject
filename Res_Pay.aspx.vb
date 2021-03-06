﻿
Option Strict On
Partial Class Res_Pay
    Inherits System.Web.UI.Page
    'declare databases
    Dim DBSeats As New DBSeats
    Dim DBTickets As New DBTickets
    Dim Calculate As New ClassCalculate
    Dim DBJourney As New DBjourneyclone
    Dim DBDate As New DBdate
    Dim DBCustomer As New DBCustomersClone
    Dim DBCancel As New CancelReservation
    Dim DBEmployee As New DBEmployee

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        ElseIf Session("UserType").ToString = "Crew" Then
            Response.Redirect("Emp_EmployeeDashboard.aspx")
        End If

        If Session("ReservationID") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        Else
            lblReservationID.Text = Session("ReservationID").ToString
        End If

        Session("UserSeat") = ""
        'write some code to pull up the advantage number we need to use to select the seats. 

        'check session reservationID if it's empty

        'If Session("ReservationID") Is Nothing Then
        '    Response.Redirect("HomePage.aspx")
        'End If
        'check to see if there is a running price subtotal on the page

        If IsPostBack = False Then
            DBTickets.GetTicketsInReservationForPricing(Session("ReservationID").ToString)
            Session("TicketCount") = DBTickets.MyDataSetOne.Tables("tblTickets").Rows.Count - 1
            If Session("TicketRecord") Is Nothing Then
                Session("TicketRecord") = 0

            End If
            pnlLogin.Visible = False
        End If
        Session("Infant") = ""
        Session("InfantID") = ""

        'check customer login

        LoadTickets()

        SortandBind()

        'next, need to load all tickets dataset
        If IsPostBack = False Then
            'load ddls and calendar date first time
            LoadDDLs()
        End If

        'bind seats and seats w/advantage numbers
        BindSeats()
        'check seats to initialize them
        CheckSeats()
    End Sub

    Public Sub LoadDDLs()
        'bind ddl for journeys
        ddlJourneyID.DataSource = DBTickets.MyView
        ddlJourneyID.DataValueField = "JourneyID"
        ddlJourneyID.DataBind()

        'lblJourneys.Text = "You are choosing payment for Flight Number " & DBJourney.MyDataSet.Tables("tblJourneys").Rows(0).Item("FlightNumber").ToString & _
        '    "that flies on " & DBJourney.MyDataSet.Tables("tblJourneys").Rows(0).Item("FlightDate").ToString


        DBTickets.GetAdvantageNumbersUsingSP(ddlJourneyID.SelectedValue, Session("ReservationID").ToString)
        'bind ddl for advantage numbers
        ddlAdvantageNum.DataSource = DBTickets.MyViewAdvantageNumbers
        ddlAdvantageNum.DataValueField = "AdvantageNumber"
        ddlAdvantageNum.DataBind()

        'DBCustomer.GetCustomerByAdvantageNumber(ddlAdvantageNum.SelectedValue)
        'lblActive.Text = "Select seat for " & DBCustomer.MyDataset.Tables("tblCustomersClone").Rows(0).Item("FirstName").ToString & " " & _
        'DBCustomer.MyDataset.Tables("tblCustomersClone").Rows(0).Item("LastName").ToString & " with phone Number " & DBCustomer.MyDataset.Tables("tblCustomersClone").Rows(0).Item("Phone").ToString

    End Sub

    Public Sub LoadTickets()
        DBTickets.GetALLTicketsUsingSP()
        DBTickets.GetALLOthersTicketsUsingSP()
        DBTickets.FilterYou(Session("ReservationID").ToString, (Session("ActiveUser").ToString))
        DBTickets.FilterOthers(Session("ReservationID").ToString, Session("ActiveUser").ToString)
        DBTickets.GetTicketsInReservationForPricing(Session("ReservationID").ToString)
        DBTickets.FilterReservationByPaid()

    End Sub

    Public Sub BindSeats()
        DBSeats.GetALLSeatsUsingSP()
        DBSeats.FilterJourneyID(ddlJourneyID.SelectedValue)
        DBSeats.GetALLSeatsAdvantageUsingSP(ddlJourneyID.SelectedValue)
    End Sub

    Public Sub CheckSeats()

        'button variable makes it so that I can give the work 'button' methods like .backcolor for each button in the arlSeats array
        Dim button As Button
        Dim arlSeats As New ArrayList

        'adding all buttons to the arraylist
        arlSeats.Add(btn1A)
        arlSeats.Add(btn1B)
        arlSeats.Add(btn2A)
        arlSeats.Add(btn2B)
        arlSeats.Add(btn3A)
        arlSeats.Add(btn3B)
        arlSeats.Add(btn3C)
        arlSeats.Add(btn3D)
        arlSeats.Add(btn4A)
        arlSeats.Add(btn4B)
        arlSeats.Add(btn4C)
        arlSeats.Add(btn4D)
        arlSeats.Add(btn5A)
        arlSeats.Add(btn5B)
        arlSeats.Add(btn5C)
        arlSeats.Add(btn5D)

        'first we should revert all the colors to lightgray so the tests work, and text to normal seat texts
        For i = 0 To 15
            button = CType(arlSeats(i), Button)
            button.BackColor = Drawing.Color.LightGray
            button.Text = button.Text.Substring(0, 2)
        Next

        'basically it does a test in each step
        For i = 0 To DBSeats.lblCountAdvantage - 1
            'so basically, there can be more than 16 entries in the myviewadvantage dataset, so we have to loop to find which seat is being chosen
            For j = 0 To 15
                button = CType(arlSeats(j), Button)
                If DBSeats.MyViewAdvantage.Table().Rows(i).Item("Seat").ToString = button.Text.Substring(0, 2) Then
                    'if the seat of this person matches the one in the array list, then we know we have the right one
                    Exit For
                End If
            Next

            'baby test, will put B if there's a 2 for status
            If CInt(DBSeats.MyViewAdvantage.Table().Rows(i).Item("Status")) = 2 Then
                If Len(button.Text) = 2 Then
                    button.Text += "i"
                ElseIf Len(button.Text) = 3 And button.BackColor = Drawing.Color.Green Then
                    'parent is the active user, if the infant was, then it would be blue
                    'setting the session infant to the infants advantage for later use
                    Session("InfantID") = DBSeats.MyViewAdvantage.Table().Rows(i).Item("AdvantageNumber").ToString
                End If
            Else
                button.Text = button.Text.Substring(0, 2)
            End If
            'second test is if the seat is empty, if yes then it makes the color lightgray
            If CInt(DBSeats.MyViewAdvantage.Table().Rows(i).Item("Status")) = 0 Then
                button.BackColor = Drawing.Color.LightGray
                'second test checks if the advantage dataset advantage number (should be user's advantage number)
            ElseIf ConvertInteger(DBSeats.MyViewAdvantage.Table().Rows(i).Item("AdvantageNumber").ToString) = CInt(ddlAdvantageNum.SelectedValue) Then
                button.BackColor = Drawing.Color.Green
                Session("UserSeat") = DBSeats.MyViewAdvantage.Table().Rows(i).Item("Seat").ToString
                'if the user is an infant then we make the B into B*
                If ConvertInteger(DBSeats.MyViewAdvantage.Table().Rows(i).Item("Age").ToString) < 3 Then
                    Session("Infant") = "Yes"
                    If Len(button.Text) < 4 Then
                        button.Text += "*"
                    End If
                End If
                'third test is whether the 'Others' dataset is empty (basically if there are other nonreservation people on the plane
            ElseIf ConvertInteger(DBSeats.MyViewAdvantage.Table().Rows(i).Item("ReservationID").ToString) <> CInt(Session("ReservationID")) Then
                'it has to loop through all the people in the dataset and check whether their advantage number is the one in the seat we are checking (i)
                button.BackColor = Drawing.Color.Coral
                'this is to prevent babies from changing their paren'ts color if it isn't blue
            ElseIf (button.BackColor <> Drawing.Color.Green And button.BackColor <> Drawing.Color.Coral) Or (button.BackColor = Drawing.Color.Green And Session("Infant").ToString = "Yes") Then
                'if this runs, the seat is filled by someone who is on the reservation but not the user
                button.BackColor = Drawing.Color.Blue
            End If

        Next

    End Sub

    Public Sub SortandBind()
        'Author: Ben Shadburne
        'Purpose: sort the data and bind it 
        'Arguments:  na
        'Return: sorted and binded data
        'Date: 03/18/2014

        'sort 
        'DBTickets.DoSort()

        ''bind all data
        gvYourReservation.DataSource = DBTickets.MyView
        gvYourReservation.DataBind()
        gvOtherReservation.DataSource = DBTickets.MyViewOthers
        gvOtherReservation.DataBind()
        gvTickets.DataSource = DBTickets.MyViewOne
        gvTickets.DataBind()

        If gvTickets.Rows.Count = 0 Then
            'have them pay or something
            Response.Redirect("Res_CreateTicketAndPay.aspx")

        End If

        HideGridViewColumn(gvTickets, 5)
        HideGridViewColumn(gvTickets, 7)
        HideGridViewColumn(gvTickets, 9)
        HideGridViewColumn(gvTickets, 10)
    End Sub

    Private Sub HideGridViewColumn(gvName As GridView, intColumn As Integer)
        gvName.HeaderRow.Cells(intColumn).Visible = False
        For Each gvr As GridViewRow In gvName.Rows
            gvr.Cells(intColumn).Visible = False
        Next
    End Sub

    Protected Sub ddlJourneyID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJourneyID.SelectedIndexChanged
        DBJourney.GetOneJourney(CInt(ddlJourneyID.SelectedValue))
        'lblJourneys.Text = "You are choosing payment for Flight Number " & DBJourney.MyDataSet.Tables("tblJourneys").Rows(0).Item("FlightNumber").ToString & _
        '    "that flies on " & DBJourney.MyDataSet.Tables("tblJourneys").Rows(0).Item("FlightDate").ToString
        CheckSeats()
    End Sub

    'every time a button is clicked this happens 
    Protected Sub btn1A_Click(sender As Object, e As EventArgs) Handles btn1A.Click, btn1B.Click, _
        btn2A.Click, btn2B.Click

        lblMessage.Text = ""

        Dim button As Button
        button = CType(sender, Button)
        Dim strNewSeat As String
        strNewSeat = button.ID.Substring(3, 2)

        If button.BackColor = Drawing.Color.Coral Then
            'seat taken by someone not on reservation
            lblMessage.Text = "Seat taken by someone not on reservation"
            Exit Sub
        End If

        If button.BackColor = Drawing.Color.Blue And Session("Infant").ToString = "Yes" Then
            'this is a loggin in infant, change in the database
            DBSeats.BluePress(Session("UserSeat").ToString, strNewSeat, ddlAdvantageNum.SelectedValue.ToString, ddlJourneyID.SelectedValue)
            ResetAll()
            'gotta change previous seat to blue

            Exit Sub
        End If

        If button.BackColor = Drawing.Color.Blue Then
            Exit Sub
        End If

        'if seat is empty then stuff happens
        If button.BackColor = Drawing.Color.LightGray Then

            'if person is a baby, they can't choose lightgrey
            If Session("Infant").ToString <> "" Then
                lblMessage.Text = "Infants can't sit alone"
                Exit Sub
            End If

            'this runs code to make sure database is updated
            'gotta check if there's a baby
            If Session("InfantID").ToString <> "" Then
                'person has a baby in their lap, run database changes for that
                DBSeats.GreyPressBaby(Session("UserSeat").ToString, strNewSeat, ddlAdvantageNum.SelectedValue.ToString, ddlJourneyID.SelectedValue, Session("InfantID").ToString)
            Else
                'person doesn't have baby
                DBSeats.GreyPress(Session("UserSeat").ToString, strNewSeat, ddlAdvantageNum.SelectedValue.ToString, ddlJourneyID.SelectedValue)
            End If

        End If

        ResetAll()

        rblPayment.SelectedIndex = -1

    End Sub

    Public Sub ResetAll()
        'binds the seats, then checks them again, then changes the gridviews
        BindSeats()
        CheckSeats()
        LoadTickets()
        SortandBind()
    End Sub

    Public Function ConvertInteger(strIn As String) As Integer
        Dim intFill As Integer
        Try
            intFill = CInt(strIn)
        Catch ex As Exception
            Return 3
        End Try
        Return CInt(strIn)
    End Function

    Protected Sub RadioButtonList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblPayment.SelectedIndexChanged
        Dim strMiles As String
        Dim intAge As Integer
        Dim decBaseFare As Decimal
        Dim strAge As String
        Dim strBaseFare As String
        Dim decAgeDiscount As Decimal
        Dim decFirstClassPremium As Decimal = 0
        Dim decInternetDiscount As Decimal = 0
        Dim decTwoWeekDiscount As Decimal
        Dim decDiscount As Decimal
        Dim decSubtotal As Decimal

        MakeAllInvisible()
        rblPayment.Visible = True

        If rblPayment.SelectedValue = "Miles" Then

            strMiles = DBTickets.GetMileage(gvTickets.SelectedRow.Cells(3).Text)
            lblMiles.Text = "You currently have " & strMiles & " miles in your account."
            If FirstClassSelected() = True Then
                lblCost.Text = "The ticket costs 2000 miles."
            Else
                lblCost.Text = "The ticket costs 1000 miles."
            End If

        Else
            'run all the cost calculations
            strAge = gvTickets.SelectedRow.Cells(6).Text
            intAge = CInt(strAge)
            strBaseFare = (gvTickets.SelectedRow.Cells(11).Text)
            decBaseFare = CDec(strBaseFare)
            If FirstClassSelected() = True Then
                decFirstClassPremium = Calculate.CalculateFirstClass(decBaseFare)
                decBaseFare += decFirstClassPremium
            End If
            decAgeDiscount = Calculate.CalculateAgeDiscount(intAge, decBaseFare)

            'use if statement to see if we should apply an internet discount
            If Session("UserType").ToString = "Customer" Then
                'internet reservation
                decInternetDiscount = Calculate.CalculateInternetPurchaseDiscount(decBaseFare)
            Else
                'this is over the phone, an employee is logged in
                btnOverride.Visible = True
                txtOverride.Visible = True
                decInternetDiscount = 0
            End If

            If CBool(Session("TwoWeek")) = True Then
                'apply internet discount
                decTwoWeekDiscount = Calculate.CalculateDateDiscount(decBaseFare)
            Else
                decTwoWeekDiscount = 0
            End If

            decDiscount = Calculate.CalculateSubTotalDiscount(decBaseFare, decFirstClassPremium, decAgeDiscount, decTwoWeekDiscount, decInternetDiscount)

            decSubtotal = decBaseFare - decDiscount

            lblPrice.Text = decSubtotal.ToString("n2")

            lblMiles.Visible = True
            lblMiles.Text = ""

            lblCost.Visible = True
            lblCost.Text = "This ticket will cost you " & FormatCurrency(decSubtotal.ToString, 2) & " plus 7.75 % tax (calculated on your total)."

        End If

        btnPay.Visible = True


    End Sub

    Protected Sub btnPay_Click(sender As Object, e As EventArgs) Handles btnPay.Click
        Dim strMiles As String
        Dim intMiles As Integer

        MakeAllInvisible()

        strMiles = DBTickets.GetMileage(gvTickets.SelectedRow.Cells(3).Text)
        intMiles = CInt(strMiles)

        'send information to the DB
        'check if we are paying by money
        If rblPayment.SelectedIndex = 1 Then
            'check to see if the customer chose a first class ticket
            If FirstClassSelected() = False Then
                'they didn't choose a first class ticket
                If FirstClassAvailable() = True And intMiles >= 500 Then
                    lblUpgrade.Visible = True
                    btnYes.Visible = True
                    btnNo.Visible = True
                    'exit sub so that they must click yes/no
                    Exit Sub
                    'must make sure we put them in a first class seat somehow...
                    'send payment information to database (miles and moneys)
                End If
            Else
                'do nothing
            End If
            PriceAdd()
        Else
            'they are paying by miles
            If FirstClassSelected() = True Then
                'check to see if they have over 2000 miles
                If intMiles >= 2000 Then
                    intMiles -= 2000
                    'send new mileage to customer record to the database
                    DBCustomer.UpdateMiles(intMiles.ToString, gvTickets.SelectedRow.Cells(3).Text)
                    DBTickets.AddTicketMiles("2000", gvTickets.SelectedRow.Cells(1).Text)
                Else
                    'not enough miles
                    lblMessage.Text = "You don't have enough miles to pay for your first class ticket. Try paying with money"
                    Exit Sub
                End If
            Else
                'first class not selected
                If intMiles >= 1000 Then
                    intMiles -= 1000
                    'send new mileage to the database
                    DBCustomer.UpdateMiles(intMiles.ToString, gvTickets.SelectedRow.Cells(3).Text)
                    DBTickets.AddTicketMiles("1000", gvTickets.SelectedRow.Cells(1).Text)
                Else
                    'not enough miles
                    lblMessage.Text = "You don't have enough miles to pay for your economy class ticket. Please pay with money."
                    rblPayment.SelectedIndex = 0
                    Exit Sub
                End If
            End If
        End If

        ResetAll()

    End Sub

    Public Function FirstClassAvailable() As Boolean
        Dim Button As Button
        Dim arlSeats As New ArrayList

        'adding all buttons to the arraylist
        arlSeats.Add(btn1A)
        arlSeats.Add(btn1B)
        arlSeats.Add(btn2A)
        arlSeats.Add(btn2B)

        For i = 0 To 3
            Button = CType((arlSeats(i)), Button)
            If Button.BackColor = Drawing.Color.LightGray Then
                Return True
            End If
        Next

        Return False

    End Function

    Public Function FirstClassSelected() As Boolean
        Dim Button As Button
        Dim arlSeats As New ArrayList

        'adding all buttons to the arraylist
        arlSeats.Add(btn1A)
        arlSeats.Add(btn1B)
        arlSeats.Add(btn2A)
        arlSeats.Add(btn2B)

        For i = 0 To 3
            Button = CType((arlSeats(i)), Button)
            If Button.BackColor = Drawing.Color.Green Then
                Return True
            End If
        Next

        Return False

    End Function

    Protected Sub ddlAdvantageNum_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAdvantageNum.SelectedIndexChanged
        'NEED TO PUT BABY INTO SESSION VARIABLE HERE IF THEY SELECTED A BABY??
        Session("ActiveUser") = ddlAdvantageNum.SelectedValue
        'DBCustomer.GetCustomerByAdvantageNumber(ddlAdvantageNum.SelectedValue)
        'lblActive.Text = "Select seat for " & DBCustomer.MyDataset.Tables("tblCustomersClone").Rows(0).Item("FirstName").ToString & " " & _
        'DBCustomer.MyDataset.Tables("tblCustomersClone").Rows(0).Item("LastName").ToString & "Phone Number" & DBCustomer.MyDataset.Tables("tblCustomersClone").Rows(0).Item("Phone").ToString
        LoadTickets()
        SortandBind()
    End Sub

    Protected Sub gvTickets_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvTickets.SelectedIndexChanged

        ddlJourneyID.SelectedValue = gvTickets.SelectedRow.Cells(4).Text
        ddlAdvantageNum.SelectedValue = gvTickets.SelectedRow.Cells(3).Text
        ResetAll()
        ddlAdvantageNum.Enabled = False
        ddlJourneyID.Enabled = False

        MakeAllInvisible()
        rblPayment.Visible = True
        lblPay.Visible = True


        rblPayment.SelectedIndex = -1

        rblPayment.Enabled = True

        gvTickets.SelectedRow.Style.Add("background-color", "#ffcccc")

        DBCustomer.FindCustomersByAdvantageNumber(CInt(gvTickets.SelectedRow.Cells(3).Text))

        DBJourney.GetOneJourney(CInt(gvTickets.SelectedRow.Cells(4).Text))

        lblActive.Text = "Pay for ticket " & DBCustomer.MyDataset.Tables("tblCustomersClone").Rows(0).Item("FirstName").ToString & " " & _
        DBCustomer.MyDataset.Tables("tblCustomersClone").Rows(0).Item("LastName").ToString & " with phone Number " & DBCustomer.MyDataset.Tables("tblCustomersClone").Rows(0).Item("Phone").ToString

        lblJourneys.Text = "You are choosing payment for Flight Number " & DBJourney.MyDataSet.Tables("tblJourneys").Rows(0).Item("FlightNumber").ToString & _
          " that flies on " & DBJourney.MyDataSet.Tables("tblJourneys").Rows(0).Item("FlightDate").ToString
    End Sub

    Protected Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        MakeAllInvisible()
        'tell them to select their first class ticket
        lblMessage.Text = "Please select your first class ticket."

        'show them the panel with the seats
        pnlSeats.Visible = True

        btnConfirm.Visible = True

    End Sub

    Protected Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        PriceAdd()
        MakeAllInvisible()
        ResetAll()
    End Sub

    Private Sub PriceAdd()
        Dim decSubtotal As Decimal
        decSubtotal = CDec(lblPrice.Text)
        Session("RunningSubtotal") = CDec(Session("RunningSubtotal")) + decSubtotal

        'update paid on DB
        DBTickets.AddTicketPrices(decSubtotal.ToString("n2"), gvTickets.SelectedRow.Cells(1).Text)
        lblPrice.Text = ""
        lblSubtotal.Text = "Your tickets currently cost " & FormatCurrency(Session("RunningSubtotal").ToString, 2) & "."
        lblActive.Text = ""
        lblJourneys.Text = ""
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim strMiles As String
        Dim intMiles As Integer

        strMiles = DBTickets.GetMileage(gvTickets.SelectedRow.Cells(3).Text)
        intMiles = CInt(strMiles)

        If FirstClassSelected() = False Then
            'error message
            lblMessage.Text = "Please click on a first class seat"
            Exit Sub
        Else
            'pay for their ticket
            DBTickets.AddTicketPricesAndMiles(lblPrice.Text.ToString, "500", gvTickets.SelectedRow.Cells(1).Text)
            'remove miles from their account
            intMiles -= 500
            DBCustomer.UpdateMiles(intMiles.ToString, gvTickets.SelectedRow.Cells(3).Text)
            Session("RunningSubtotal") = CDec(Session("RunningSubtotal")) + CDec(lblPrice.Text)
            lblPrice.Text = ""
        End If

        MakeAllInvisible()

        ResetAll()

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'this code is necessary to cancel the reservation in the database
        DBCancel.CancelReservation(Session("ReservationID").ToString)
        DBCancel.ChangeSeatStatus(Session("ReservationID").ToString)
        DBCancel.ReturnMilesAndDeactivateTicket(Session("ReservationID").ToString)

        'FIGURE OUT WHAT OTHER SESSION VARIABLES I NEED TO REMOVE
        Session.Remove("ReservationID")

        'redirect the customer
        Response.Redirect("Cust_CreateReservationAndSelectFlight.aspx")
    End Sub

    Protected Sub btnOverride_Click(sender As Object, e As EventArgs) Handles btnOverride.Click
        MakeAllInvisible()

        Dim decSubtotal As Decimal
        Try
            decSubtotal = CDec(txtOverride.Text)
        Catch ex As Exception
            lblMessage.Text = "You did not enter a decimal price."
            rblPayment.Visible = True
            rblPayment.SelectedIndex = -1
            txtOverride.Text = ""
            Exit Sub
        End Try

        If decSubtotal < 0 Then
            lblMessage.Text = "You did not enter a positive decimal price."
            rblPayment.Visible = True
            rblPayment.SelectedIndex = -1
            txtOverride.Text = ""
            Exit Sub
        End If

        MakeAllInvisible()

        lblPrice.Text = decSubtotal.ToString("n2")

        ResetAll()
        pnlLogin.Visible = True
        txtOverride.Visible = True
        txtOverride.Enabled = False

        Exit Sub

    End Sub

    Public Sub MakeAllInvisible()
        btnPay.Visible = False
        btnOverride.Visible = False
        btnYes.Visible = False
        btnNo.Visible = False
        btnConfirm.Visible = False

        lblMiles.Text = ""
        lblCost.Text = ""
        lblUpgrade.Visible = False
        lblSubtotal.Text = ""
        lblMessage.Text = ""

        txtOverride.Visible = False
        txtOverride.Enabled = True

        rblPayment.Visible = False

        pnlSeats.Visible = False

        pnlLogin.Visible = False

    End Sub

    Protected Sub btnOverridePrice_Click(sender As Object, e As EventArgs) Handles btnOverridePrice.Click
        MakeAllInvisible()
        Dim intLogin As Integer
        Try
            intLogin = CInt(txtUsername.Text)
        Catch ex As Exception
            lblMessage.Text = "You must login with an integer value"
            pnlLogin.Visible = True
            Exit Sub
        End Try

        If Len(txtUsername.Text) <> 3 Then
            lblMessage.Text = "An employee login is 3 digits long."
            pnlLogin.Visible = True
            Exit Sub

        End If

        If DBEmployee.CheckEmpExists(txtUsername.Text) = False Then
            lblMessage.Text = "That employee ID does not exist. Override Failed."
            'test fails
            rblPayment.SelectedIndex = -1
            rblPayment.Visible = True
            Exit Sub

        Else
            'check password
            If DBEmployee.CheckPassword(txtUsername.Text, txtPassword.Text) = True Then
                'the code passes
                Dim decSubtotal As Decimal
                Try
                    decSubtotal = CDec(lblPrice.Text)
                Catch ex As Exception
                    lblMessage.Text = "Please enter a positive decimal price."
                    Exit Sub
                End Try

                If decSubtotal < 0 Then
                    lblMessage.Text = "Please enter a positive decimal price."
                    Exit Sub
                End If

                MakeAllInvisible()

                txtOverride.Text = ""

                PriceAdd()

                ResetAll()

            Else
                'login failed
                lblMessage.Text = "Username and password combination was incorrect. Override failed."
                rblPayment.Visible = True
                rblPayment.SelectedIndex = -1
            End If
        End If

        'code ran all the way
        txtOverride.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        MakeAllInvisible()
        txtUsername.Text = ""
        rblPayment.Visible = True
        rblPayment.SelectedIndex = -1
    End Sub
End Class
