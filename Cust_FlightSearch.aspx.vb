﻿Option Strict On
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim DBFlightSearch As New DBFlightSearch
    Dim DBAddJourney As New AddJourneyClass
    Dim DBReservations As New DBReservations
    Dim DBSeats As New DBJourneySeats

    Dim intJourneyID As Integer
    Dim intJourneyNumber As Integer
    Dim strJourneyNumber As String
    Dim strDate As String
    Dim strTempAirport As String


    'Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
    '    Dim strDay As String
    '    Dim strDate As String

    '    'populate those variables
    '    strDay = WeekdayName(Weekday(calFlightSearch.SelectedDate))
    '    strDate = calFlightSearch.SelectedDate.ToString

    '    'add journeys for those days if they aren't added already
    '    DBAddJourney.AddJourney(strDay, strDate)

    '    'show flights available
    '    ShowAll()
    '    SortandBind()

    '    MakeIndirectInvisible()
    'End Sub

    Private Sub MakeIndirectInvisible()
        gvIndirectFinish.Visible = False
        lblIndirectFinish.Visible = False
        lblIndirectFinishC.Visible = False
        lblCountFinish.Visible = False
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' lblMessage.Text = ddlTimeOfDay.SelectedValue

        'make sure there is an arrival, departure ciy
        If Session("StartAirport") Is Nothing Then
            Response.Redirect("Cust_CreateReservationAndSelectFlight.aspx")
            Exit Sub
        Else
            lblDeparture.Text = Session("StartAirport").ToString
        End If

        If Session("EndAirport") Is Nothing Then
            Response.Redirect("Cust_CreateReservationAndSelectFlight.aspx")
            Exit Sub
        Else
            lblArrival.Text = Session("EndAirport").ToString
        End If

        If Session("JourneyNumber") Is Nothing Then
            Response.Redirect("Cust_CreateReservationAndSelectFlight.aspx")
        End If


        'make sure a date is selected before they search, fill the dataview
        If IsPostBack = False Then
            calFlightSearch.SelectedDate = Now()
            DBAddJourney.AddJourney(WeekdayName(Weekday(calFlightSearch.SelectedDate)), calFlightSearch.SelectedDate.ToString)

        End If
        ShowAll()
        SortandBind()

    End Sub

    Public Sub ShowAll()
        DBFlightSearch.GetALLFlightSearchUsingSP()
        DBFlightSearch.GetALLIndirectStartUsingSP()
        DBFlightSearch.GetALLIndirectFinishUsingSP()
    End Sub

    Public Sub SortandBind()
        'Author: AaryamanSinghal
        'Purpose: sort the data and bind it 
        'Arguments:  na
        'Return: sorted and binded data
        'Date: 03/18/2014

        DBFlightSearch.DoSort()

        'this filters by start airport, end airport, date, and earliest start time
        DBFlightSearch.SearchDirect(Session("StartAirport").ToString, Session("EndAirport").ToString, DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString))
        DBFlightSearch.SearchIndirectStart(Session("StartAirport").ToString, Session("EndAirport").ToString, DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString))

        'bind all data
        gvDirectFlights.DataSource = DBFlightSearch.MyView
        gvDirectFlights.DataBind()
        gvIndirectStart.DataSource = DBFlightSearch.MyViewStart
        gvIndirectStart.DataBind()
        gvIndirectFinish.DataSource = DBFlightSearch.MyViewFinish
        gvIndirectFinish.DataBind()

        ' show record count
        lblCountDirect.Text = CStr(DBFlightSearch.lblCount)
        lblCountIndirect.Text = CStr(DBFlightSearch.lblCountStart)
        lblCountFinish.Text = CStr(DBFlightSearch.lblCountFinish)

    End Sub

    Protected Sub calFlightSearch_SelectionChanged(sender As Object, e As EventArgs) Handles calFlightSearch.SelectionChanged
        'define variables for day and date
        Dim strDay As String
        Dim strDate As String

        'populate those variables
        strDay = WeekdayName(Weekday(calFlightSearch.SelectedDate))
        strDate = DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString)

        'add journeys for those days if they aren't added already
        DBAddJourney.AddJourney(strDay, strDate)

        'the only criteria we need to search on is date and time
        'converts to only the date calFlightSearch.SelectedDate.ToShortDateString

        ShowAll()
        SortandBind()
        MakeIndirectInvisible()

    End Sub

    Protected Sub gvDirectFlights_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDirectFlights.SelectedIndexChanged


        'add this flight to the reservation table
        intJourneyID = CInt(gvDirectFlights.Rows(gvDirectFlights.SelectedIndex).Cells(1).Text())

        'use another variable to hold the session variable. Otherwise it gives me option strict on problems
        intJourneyNumber = CInt(Session("JourneyNumber"))

        'add one to the journey number
        intJourneyNumber += 1

        'get the right string for the column name in the DB
        strJourneyNumber = DBReservations.ConvertJourneyNumberToString(intJourneyNumber)

        'if this is the first journey they are adding, use add first Journey Function
        If intJourneyNumber = 1 Then

            strDate = gvDirectFlights.Rows(gvDirectFlights.SelectedIndex).Cells(2).Text

            'add the first record of the reservation
            DBReservations.AddFirstReservationJourney("usp_ReservationsClone_Add_Journey", strJourneyNumber, intJourneyID, strDate)

            'retrieve the reservationID for the reservation we just added to. Store it in a new session variable
            Session.Add("ReservationID", DBReservations.GetNewestReservationID())

            'if this is a one way ticket, have them go book their seats
            If Session("TripType").ToString = "One Way" Then
                'remove session variables
                RemoveSessionVariablesAndRedirect()
                Exit Sub
            End If

            'update the session variable
            Session("JourneyNumber") = intJourneyNumber

            'if this is a round trip, send back the other 
            If Session("TripType").ToString = "Round Trip" Then
                'switch begin, end airport
                FirstHalfRoundTrip()
            End If

        Else
            'add a later journey
            DBReservations.AddLaterReservationJourneys("usp_ReservationsClone_Add_Later_Journeys", strJourneyNumber, intJourneyID, CInt(Session("ReservationID")))

            If Session("TripType").ToString = "Round Trip" Then
                'remove session variables
                RemoveSessionVariablesAndRedirect()
            End If

            'update this session variable if this is a multiple city trip
            Session("JourneyNumber") = intJourneyNumber

        End If

        'if the code gets this far, the trip is a multiple city trip

        If Session("IsFinal") Is Nothing Then
            'dont do anything
        Else
            'remove session variables
            'redirect to customer Reservation page
            Session.Remove("IsFinal")
            RemoveSessionVariablesAndRedirect()
        End If

        'mark the airport they must now leave from
        Session("StartAirport") = Session("EndAirport")

        Response.Redirect("Cust_CreateReservationAndSelectFlight.aspx")


        'direct the user back to the previous page
        'figure out what session variables I need to pass to the reservation page -- the airport the user ended at, flight date and arrive time, 

    End Sub

    Private Sub RemoveSessionVariablesAndRedirect()
        'remove session variables
        Session.Remove("StartAirport")
        Session.Remove("EndAirport")
        Session.Remove("JourneyNumber")
        Session.Remove("TripType")


        Response.Redirect("Res_SelectCustomer.aspx")

    End Sub

    Protected Sub gvIndirectStart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvIndirectStart.SelectedIndexChanged
        'need to do searchbtn b/c otherwise other gv's get reset

        DBFlightSearch.SearchIndirectFinish(gvIndirectStart.Rows(gvIndirectStart.SelectedIndex).Cells(6).Text, lblArrival.Text, _
        DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString), _
        gvIndirectStart.Rows(gvIndirectStart.SelectedIndex).Cells(4).Text)

        'check if there's anything in second gv
        If CInt(DBFlightSearch.lblCountFinish) = 0 Then
            lblMessage.Text = "No second leg results. Please choose a direct flight or different indirect flight"
            Exit Sub
        End If

        SortandBind()

        'highlight the selected row
        gvIndirectStart.SelectedRow.Style.Add("background-color", "#ffcccc")

        'and make the count equal to the count
        lblCountFinish.Text = DBFlightSearch.lblCountFinish.ToString

        gvIndirectFinish.Visible = True
        lblIndirectFinishC.Visible = True
        lblIndirectFinish.Visible = True
        lblCountFinish.Visible = True

    End Sub

    Protected Sub gvIndirectFinish_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvIndirectFinish.SelectedIndexChanged
        'This code binds all the gridviews. 
        'Without this code, they change on button click and the selected index has diff info in it
        'add this flight to the reservation table
        DBFlightSearch.SearchIndirectFinish(gvIndirectStart.Rows(gvIndirectStart.SelectedIndex).Cells(6).Text, lblArrival.Text, _
        DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString), _
        gvIndirectStart.Rows(gvIndirectStart.SelectedIndex).Cells(4).Text)
        SortandBind()

        'use another variable to hold the session variable. Otherwise it gives me option strict on problems
        intJourneyNumber = CInt(Session("JourneyNumber"))

        'add one to the journey number
        intJourneyNumber += 1

        'get the right string for the column name in the DB
        strJourneyNumber = DBReservations.ConvertJourneyNumberToString(intJourneyNumber)

        If intJourneyNumber = 1 Then
            'get the date to put in as start reservation date
            strDate = DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString)

            'get Journey ID for first flight
            intJourneyID = CInt(gvIndirectStart.Rows(gvIndirectStart.SelectedIndex).Cells(1).Text)

            'add the first record of the reservation
            DBReservations.AddFirstReservationJourney("usp_ReservationsClone_Add_Journey", strJourneyNumber, intJourneyID, strDate)

            'update the session variable
            intJourneyNumber += 1

            'retrieve the reservationID for the reservation we just added to. Store it in a new session variable
            Session.Add("ReservationID", DBReservations.GetNewestReservationID())

            'now the Journey ID is the one from the finish gridview
            intJourneyID = CInt(gvIndirectFinish.Rows(gvIndirectFinish.SelectedIndex).Cells(1).Text)
            strJourneyNumber = DBReservations.ConvertJourneyNumberToString(intJourneyNumber)

            'add the second leg of the journey
            DBReservations.AddLaterReservationJourneys("usp_ReservationsClone_Add_Later_Journeys", strJourneyNumber, intJourneyID, CInt(Session("ReservationID")))

            'if this is a one way ticket, have them go book their seats
            If Session("TripType").ToString = "One Way" Then
                'remove session variables and redirect
                RemoveSessionVariablesAndRedirect()
                Exit Sub
            End If

            'if this is a round trip, send back the other 
            If Session("TripType").ToString = "Round Trip" Then

                'get correct values for journey number
                Session("JourneyNumber") = intJourneyNumber

                'switch begin, end airport, reload page
                FirstHalfRoundTrip()
                Exit Sub
            End If

        Else
            'add a later journey -- update info needed
            intJourneyID = CInt(gvIndirectStart.Rows(gvIndirectStart.SelectedIndex).Cells(1).Text)

            DBReservations.AddLaterReservationJourneys("usp_ReservationsClone_Add_Later_Journeys", strJourneyNumber, intJourneyID, CInt(Session("ReservationID")))

            intJourneyID = CInt(gvIndirectFinish.Rows(gvIndirectFinish.SelectedIndex).Cells(1).Text)
            intJourneyNumber += 1
            strJourneyNumber = DBReservations.ConvertJourneyNumberToString(intJourneyID)

            DBReservations.AddLaterReservationJourneys("usp_ReservationsClone_Add_Later_Journeys", strJourneyNumber, intJourneyID, CInt(Session("ReservationID")))


            If Session("TripType").ToString = "Round Trip" Then
                'remove session variables
                RemoveSessionVariablesAndRedirect()
                Exit Sub
            End If

            'update this session variable if this is a multiple city trip
            Session("JourneyNumber") = intJourneyNumber

        End If

        If Session("IsFinal") Is Nothing Then
            'dont do anything
        Else
            'remove session variables
            'redirect to customer Reservation page
            Session.Remove("IsFinal")
            RemoveSessionVariablesAndRedirect()
        End If

        'mark the airport they must now leave from for next leg
        Session("StartAirport") = Session("EndAirport")

        'redirect them to selection
        Response.Redirect("Cust_CreateReservationAndSelectFlight.aspx")
    End Sub

    Private Sub FirstHalfRoundTrip()
        strTempAirport = Session("EndAirport").ToString
        Session("EndAirport") = Session("StartAirport")
        Session("StartAirport") = strTempAirport
        'reload the page and exit sub
        ShowAll()
        lblReturn.Visible = True
        Response.Redirect("Cust_FlightSearch.aspx")
    End Sub

End Class
