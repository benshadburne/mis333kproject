﻿Option Strict On
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim DBSeats As New DBSeats
    Dim DBTickets As New DBTickets
    Dim DBJourneySeats As New DBJourneySeats
    Dim DBFlightSearch As New DBFlightSearch
    Dim AddJourneyClass As New AddJourneyClass
    Dim DBReservations As New DBReservations
    Dim DBDate As New DBdate
    Dim mAdvantageNumber As Integer

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load, calFlightDate.SelectionChanged

        'THIS PAGE IS CRASHING BECAUSE THERE IS NO INITIALIZED VALUE FOR Session("ReservationID"). 
        'MAKE THEM CHOOSE A RESERVATION WHICH HAS ACTIVE TICKETS AND TICKETS WHICH AREN'T FLOWN YET
        If IsPostBack = False Then
            If Session("FromReservation") Is Nothing Then
                Response.Redirect("Cust_AllReservations.aspx")
            ElseIf Session("FromReservation").ToString = "Yes1" Then
                lblMessage.Text = "Modification made!"
                Session.Remove("FromReservation")
            Else
                Session.Remove("FromReservation")
            End If
        End If
        'check if login session is empty
        If Session("UserID") Is Nothing Or Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        End If

        If Session("UserType").ToString <> "Customer" Then
            Response.Redirect("HomePage.aspx")
        End If

        If Session("ReservationID").ToString Is Nothing Then
            Response.Redirect("Cust_AllReservations.aspx")
        End If

        Session("Infant") = ""
        Session("InfantID") = ""
        Session("UserSeat") = ""


        If IsPostBack = False Then
            Session("ActiveUser") = Session("UserID").ToString
        End If

        Try
            'next, need to load all tickets dataset
            LoadTickets()
        Catch ex As Exception
            Session("NoJourneys") = "Yes"
            Response.Redirect("AllReservations.aspx")
        End Try


        SortandBind()

        If IsPostBack = False Then
            LoadDDLs()
            calFlightDate.SelectedDate = Now()
        End If

        'bind seats and seats w/advantage numbers
        BindSeats()
        'check seats to initialize them
        CheckSeats()

        'finally add journeys based on calendar
        AddJourneys()

        'fill available textbox
        FillAvailable()

    End Sub

    Public Sub FillAvailable()

        'sets buttons to disabled and availabe to unavailable if journey is before this date
        'first get date of selected journey into date format
        Dim strDate As String
        Dim datCurrentDate As Date
        strDate = (ddlJourneyID.SelectedItem.ToString).Substring(5, Len(ddlJourneyID.SelectedItem.ToString) - 5)
        datCurrentDate = CDate(DBDate.GetCurrentDate())

        If Date.Parse(strDate) < datCurrentDate Then
            'the journey took off before the currect datetime in database, set buttons to disable and unavailable
            btn1A.Enabled = False
            btn1B.Enabled = False
            btn2A.Enabled = False
            btn2B.Enabled = False
            btn3A.Enabled = False
            btn3B.Enabled = False
            btn3C.Enabled = False
            btn3D.Enabled = False
            btn4A.Enabled = False
            btn4B.Enabled = False
            btn4C.Enabled = False
            btn4D.Enabled = False
            btn5A.Enabled = False
            btn5B.Enabled = False
            btn5C.Enabled = False
            btn5D.Enabled = False

            txtAvailable.Text = "Unavailable"
            Exit Sub
        End If

        'can't change to an earlier date
        If calFlightDate.SelectedDate < datCurrentDate Then
            txtAvailable.Text = "Unavailable"
            Exit Sub
        End If

        'These are to test whether the flight is available on selected day, will catch as exception if not
        Try
            DBTickets.GetFlightNumber(Session("ReservationID").ToString, ddlJourneyID.SelectedValue)
            DBJourneySeats.GetJourneyIDUsingSP(DBTickets.MyViewFlight.Table.Rows(0).Item(0).ToString, DBFlightSearch.AlterDate(calFlightDate.SelectedDate.ToShortDateString))
            'if it gets past these, then the journey exists on the selected day
        Catch ex As Exception
            txtAvailable.Text = "Unavailable"
            Exit Sub
        End Try

        'check how many empty seats there are
        DBSeats.GetALLSeatsUsingSP()
        DBSeats.FilterJourneyEmpty(ddlJourneyID.SelectedValue)

        'if number of empty seats is less than number of advantage numbers on reservation then
        If DBSeats.lblCount < DBTickets.lblCountAdvantage Then
            'number of empty seats is lower than number of advantage numbers
            txtAvailable.Text = "Unavailable"
        Else
            txtAvailable.Text = "Available"
        End If

    End Sub

    Public Sub LoadDDLs()
        'bind ddl for journeys
        ddlJourneyID.DataSource = DBTickets.MyView
        ddlJourneyID.DataTextField = "FlightAndDate"
        ddlJourneyID.DataValueField = "JourneyID"
        ddlJourneyID.DataBind()

        DBTickets.GetActiveAdvantageNumbersUsingSP(ddlJourneyID.SelectedValue, Session("ReservationID").ToString)
        'bind ddl for advantage numbers
        ddlAdvantageNum.DataSource = DBTickets.MyViewAdvantageNumbers
        ddlAdvantageNum.DataValueField = "AdvantageNumber"
        ddlAdvantageNum.DataBind()

    End Sub

    Public Sub LoadTickets()
        DBTickets.GetALLTicketsWithDateUsingSP()
        DBTickets.GetALLOthersTicketsWithDateUsingSP()
        DBTickets.FilterYou(Session("ReservationID").ToString, Session("ActiveUser").ToString)
        DBTickets.FilterOthers(Session("ReservationID").ToString, Session("ActiveUser").ToString)

    End Sub

    Public Sub BindSeats()
        DBSeats.GetALLSeatsUsingSP()
        DBSeats.FilterJourneyID(ddlJourneyID.SelectedValue)
        DBSeats.GetALLSeatsAdvantageUsingSP(ddlJourneyID.SelectedValue)
    End Sub

    Public Sub CheckSeats()

        'need to enable all seats
        btn1A.Enabled = True
        btn1B.Enabled = True
        btn2A.Enabled = True
        btn2B.Enabled = True
        btn3A.Enabled = True
        btn3B.Enabled = True
        btn3C.Enabled = True
        btn3D.Enabled = True
        btn4A.Enabled = True
        btn4B.Enabled = True
        btn4C.Enabled = True
        btn4D.Enabled = True
        btn5A.Enabled = True
        btn5B.Enabled = True
        btn5C.Enabled = True
        btn5D.Enabled = True

        'button variable makes it so that I can give the work 'button' methods like .backcolor for each button in the arlSeats array
        Dim button As Button
        Dim arlSeats As New ArrayList
        Dim intSeat As Integer

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

        'sets session("infant") to yes when the infant isn't in a seat but the user is an infant
        'Try
        If CInt(gvYourReservation.Rows(0).Cells(5).Text) < 3 Then
            Session("Infant") = "Yes"
        End If
        'Catch ex As Exception

        'End Try


        'disable seats so that user cannot switch between seat classes
        For i = 0 To 15
            button = CType(arlSeats(i), Button)
            If button.BackColor = Drawing.Color.Green Then
                'sets the seat integer to reference of i, 0-3 is first class 4-15 is economy
                intSeat = i
                Exit For
            End If
        Next
        Select Case intSeat
            Case 0 To 3
                'disable economy buttons since user is in first class
                btn3A.Enabled = False
                btn3B.Enabled = False
                btn3C.Enabled = False
                btn3D.Enabled = False
                btn4A.Enabled = False
                btn4B.Enabled = False
                btn4C.Enabled = False
                btn4D.Enabled = False
                btn5A.Enabled = False
                btn5B.Enabled = False
                btn5C.Enabled = False
                btn5D.Enabled = False
            Case 4 To 15
                'disable first class
                btn1A.Enabled = False
                btn1B.Enabled = False
                btn2A.Enabled = False
                btn2B.Enabled = False
            Case Else
                lblMessage.Text = "Please choose a different user, error in button load"
        End Select


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

    End Sub

    Protected Sub ddlJourneyID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJourneyID.SelectedIndexChanged
        CheckSeats()
    End Sub

    'every time a button is clicked this happens 
    Protected Sub btn1A_Click(sender As Object, e As EventArgs) Handles btn1A.Click, btn1B.Click, _
        btn2A.Click, btn2B.Click, btn3A.Click, btn3B.Click, btn3C.Click, btn3D.Click, btn4A.Click, btn4B.Click, _
        btn4C.Click, btn4D.Click, btn5A.Click, btn5B.Click, btn5C.Click, btn5D.Click

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
            'is there a baby already in the lap?
            If button.Text.Length >= 3 Then
                'baby already in that lap
                lblMessage.Text = "A baby already occupys that lap"
                Exit Sub
            End If

            'this is a loggin in infant, change in the database
            DBSeats.BluePress(Session("UserSeat").ToString, strNewSeat, ddlAdvantageNum.SelectedValue.ToString, ddlJourneyID.SelectedValue)
            ResetAll()

            Exit Sub
        End If

        If button.BackColor = Drawing.Color.Blue Then
            lblMessage.Text = "Seat occupied by someone on reservation"
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

    Protected Sub btnHide_Click(sender As Object, e As EventArgs) Handles btnHideSeats.Click
        pnlSeats.Visible = Not pnlSeats.Visible
    End Sub

    Protected Sub btnHideTickets_Click(sender As Object, e As EventArgs) Handles btnHideTickets.Click
        pnlTickets.Visible = Not pnlTickets.Visible
    End Sub

    Protected Sub btnHideDates_Click(sender As Object, e As EventArgs) Handles btnHideDates.Click
        pnlDates.Visible = Not pnlDates.Visible
    End Sub

    Protected Sub btnReservationChange_Click(sender As Object, e As EventArgs) Handles btnReservationChange.Click

        lblMessage.Text = ""

        'will only run if textbox says available
        If txtAvailable.Text <> "Available" Then
            lblMessage.Text = "Journey is either full or unavailable for selected date"
            Exit Sub
        End If

        'first we gotta set all existing seats to 0 and remove their seats from their tickets
        DBSeats.FilterReservationID(Session("ReservationID").ToString)
        For i = 0 To DBSeats.lblCountAdvantage - 1
            'makes journeyseatbridge value = 0
            DBSeats.UpdateJourneySeatBridge(DBSeats.MyViewAdvantage.Table().Rows(i).Item("Seat").ToString, 0, ddlJourneyID.SelectedValue.ToString)
            'makes ticket seats blank
            DBTickets.ModifyTicketSeat(ddlJourneyID.SelectedValue, Session("ReservationID").ToString)
        Next

        'need to find flightnumber for the journeyid selected
        DBTickets.GetFlightNumber(Session("ReservationID").ToString, ddlJourneyID.SelectedValue)

        'next we find the new journeyID and modify the tickets with it
        DBJourneySeats.GetJourneyIDUsingSP(DBTickets.MyViewFlight.Table.Rows(0).Item(0).ToString, DBFlightSearch.AlterDate(calFlightDate.SelectedDate.ToShortDateString))

        Try
            'run code to update tickets with new journeyID
            DBTickets.ModifyTicketJourneyID(DBJourneySeats.MyViewSeats.Table().Rows(0).Item("JourneyID").ToString, ddlJourneyID.SelectedValue, Session("ReservationID").ToString)
        Catch ex As Exception
            lblMessage.Text = "Error in modifying reservation, make sure date is after the current date and that it is active"
            Exit Sub
        End Try

        'charge them $50, idk?!?!?!??
        DBReservations.AddFee(Session("ReservationID").ToString)


        Session("FromReservation") = "Yes1"
        Response.Redirect("Cust_ReservationDetails.aspx")
        ''also load ddls so that they represent new journeyID, and fill available so that it responds to new ddls
        'LoadDDLs()
        'FillAvailable()
    End Sub

    Public Sub AddJourneys()
        Dim strDay As String
        Dim strDate As String
        'put the name of the day of the week into strDay
        strDay = WeekdayName(Weekday(calFlightDate.SelectedDate))

        strDate = (DBFlightSearch.AlterDate(calFlightDate.SelectedDate.ToShortDateString))

        'adds flights to the date, ensures we have flights to show
        AddJourneyClass.AddJourney(strDay, strDate)

    End Sub

    Protected Sub ddlAdvantageNum_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAdvantageNum.SelectedIndexChanged

        Session("ActiveUser") = ddlAdvantageNum.SelectedValue
        LoadTickets()
        SortandBind()
    End Sub
End Class
