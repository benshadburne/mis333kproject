
Option Strict On
Partial Class Res_Pay
    Inherits System.Web.UI.Page
    'declare databases
    Dim DBSeats As New DBSeats
    Dim DBTickets As New DBTickets
    Dim Calculate As New ClassCalculate
    Dim DBJourney As New DBjourneyclone
    Dim DBDate As New DBdate

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Session("UserSeat") = ""
        'write some code to pull up the advantage number we need to use to select the seats. 

        'check session reservationID if it's empty

        'If Session("ReservationID") Is Nothing Then
        '    Response.Redirect("HomePage.aspx")
        'End If

        'check to see if there is a running price subtotal on the page
        If Session("RunningSubtotal") Is Nothing Then
            'there isn't a running price subtotal, create one
            Session.Add("RunningSubtotal", 0)
        End If

        If IsPostBack = False Then
            DBTickets.GetTicketsInReservation(Session("ReservationID").ToString)
            Session("TicketCount") = DBTickets.MyDataSetOne.Tables("tblTickets").Rows.Count - 1
            LoadDDLs()
            ddlJourneyID.SelectedIndex = 0
            ddlAdvantageNum.SelectedIndex = 0
            If Session("TicketRecord") Is Nothing Then
                Session("TicketRecord") = 0

            End If
        End If
        Session("Infant") = ""
        Session("InfantID") = ""

        Session("ActiveUser") = ddlAdvantageNum.SelectedValue

        'check customer login

        ''check session login if it's empty 
        'strAdvantageNum = Session("Login").ToString
        'If strAdvantageNum = "" then
        '   Response.Redirect("HomePage.aspx")
        'End If


        'next, need to load all tickets dataset
        'load ddls and calendar date first time

        LoadTickets()

        SortandBind()

        'bind seats and seats w/advantage numbers
        BindSeats()
        'check seats to initialize them
        CheckSeats()


        'lblMessage.Text = Calculate.ConvertToVBDate("2014-04-16").ToShortDateString
    End Sub

    Public Sub LoadDDLs()
        'bind ddl for journeys
        ddlJourneyID.DataSource = DBTickets.MyView
        ddlJourneyID.DataValueField = "JourneyID"
        ddlJourneyID.DataBind()

        DBTickets.GetAdvantageNumbersUsingSP(ddlJourneyID.SelectedValue, Session("ReservationID").ToString)
        'bind ddl for advantage numbers
        ddlAdvantageNum.DataSource = DBTickets.MyViewAdvantageNumbers
        ddlAdvantageNum.DataValueField = "AdvantageNumber"
        ddlAdvantageNum.DataBind()

    End Sub

    Public Sub LoadTickets()
        DBTickets.GetALLTicketsUsingSP()
        DBTickets.GetALLOthersTicketsUsingSP()
        DBTickets.FilterYou(Session("ReservationID").ToString, Session("ActiveUser").ToString)
        DBTickets.FilterOthers(Session("ReservationID").ToString, Session("ActiveUser").ToString)
        DBTickets.GetTicketsInReservation(Session("ReservationID").ToString)


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

    End Sub

    Protected Sub ddlJourneyID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJourneyID.SelectedIndexChanged
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
        Dim intBaseFare As Integer
        Dim decAgeDiscount As Decimal
        Dim decFirstClassPremium As Decimal = 0
        Dim decInternetDiscount As Decimal = 0
        Dim decTwoWeekDiscount As Decimal
        Dim decSubtotal As Decimal
        Dim datReservation As Date
        Dim datToday As Date


        If rblPayment.SelectedValue = "Miles" Then
          
            strMiles = DBTickets.GetMileage(gvYourReservation.Columns(3).ToString)
            lblMiles.Text = "You currently have " & strMiles & " miles in your account."
            If FirstClassSelected() = True Then
                lblCost.Text = "The ticket costs 2000 miles."
            Else
                lblCost.Text = "The ticket costs 1000 miles."
            End If

        Else
            'run all the cost calculations
            intAge = CInt(gvYourReservation.Columns(6).ToString)
            intBaseFare = CInt((gvOtherReservation.Columns(11).ToString))
            decAgeDiscount = Calculate.CalculateAgeDiscount(intAge, intBaseFare)
            If FirstClassSelected() = True Then
                decFirstClassPremium = Calculate.CalculateFirstClass(intBaseFare)
            End If


            datReservation = CDate(DBJourney.GetDateByJourney(gvYourReservation.Columns(3).ToString, gvYourReservation.Columns(0).ToString))
            datToday = DBDate.GetCurrentDate

            'use if statement to see if we should apply an internet discount
            'If Session("login") = "customer" Then
            '    'internet reservation
            '    decInternetDiscount = Calculate.CalculateInternetPurchaseDiscount(intBaseFare)
            'Else
            '    decInternetDiscount = 0
            'End If

            decTwoWeekDiscount = Calculate.CalculateDateDiscount(intBaseFare, datReservation, datToday)

            decSubtotal = Calculate.CalculateSubTotalDiscount(intBaseFare, decFirstClassPremium, decAgeDiscount, decTwoWeekDiscount, decInternetDiscount)

            Session.Add("Subtotal", decSubtotal)

            lblCost.Text = "This ticket will cost you $" & decSubtotal.ToString("n2") & "."

        End If

        btnPay.Visible = True


    End Sub

    Protected Sub btnPay_Click(sender As Object, e As EventArgs) Handles btnPay.Click
        Dim strMiles As String
        Dim intMiles As Integer

        strMiles = DBTickets.GetMileage(gvYourReservation.Columns(3).ToString)
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
                    'must make sure we put them in a first class seat somehow...
                    'send payment information to database (miles and moneys)
                    Session("RunningSubtotal") = CDec(Session("RunningSubtotal")) + CDec(Session("Subtotal"))
                    Session.Remove("Subtotal")
                End If
            Else
                'do nothing
            End If
        Else
            'they are paying by miles
            If FirstClassSelected() = True Then
                'check to see if they have over 2000 miles
                If intMiles >= 2000 Then
                    intMiles -= 2000
                    'send new mileage to the database
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
                Else
                    'not enough miles
                    lblMessage.Text = "You don't have enough miles to pay for your economy class ticket. Please pay with money."
                    rblPayment.SelectedIndex = 0
                    rblPayment.Enabled = False
                    Exit Sub
                End If
            End If
        End If


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

    Public Sub GoToNextCustomer()
        'write some code so that we will select the next customer
    End Sub

    Protected Sub ddlAdvantageNum_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAdvantageNum.SelectedIndexChanged

        Session("ActiveUser") = ddlAdvantageNum.SelectedValue
        LoadTickets()
        SortandBind()
    End Sub

    Protected Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        rblPayment.Visible = True
    End Sub
End Class
