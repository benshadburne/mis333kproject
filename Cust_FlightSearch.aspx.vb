Option Strict On
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim DBFlightSearch As New DBFlightSearch
    Dim DBAddJourney As New AddJourneyClass
    Dim DBReservations As New DBReservations
    Dim DBSeats As New DBJourneySeats


    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'show flights available
        ShowAll()
    End Sub

    Protected Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click

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
            ShowAll()
        End If


    End Sub

    Public Sub ShowAll()
        DBFlightSearch.GetALLFlightSearchUsingSP()

        SortandBind()
    End Sub

    Public Sub SortandBind()
        'Author: AaryamanSinghal
        'Purpose: sort the data and bind it 
        'Arguments:  na
        'Return: sorted and binded data
        'Date: 03/18/2014

        Dim strFilterStatement As String

        'this filters by start airport, end airport, date, and earliest start time
        strFilterStatement = DBFlightSearch.FilterAll(Session("StartAirport").ToString, Session("EndAirport").ToString, calFlightSearch.SelectedDate.ToShortDateString, 1)
        DBFlightSearch.MyView.RowFilter = strFilterStatement
        DBFlightSearch.DoSort()
        gvDirectFlights.DataSource = DBFlightSearch.MyView
        gvDirectFlights.DataBind()

        ' show record count
        lblCountDirect.Text = "Count: " & CStr(DBFlightSearch.lblCount)
        lblFilter.Text = strFilterStatement

    End Sub

    Protected Sub calFlightSearch_SelectionChanged(sender As Object, e As EventArgs) Handles calFlightSearch.SelectionChanged
        'define variables for day and date
        Dim strDay As String
        Dim strDate As String

        'populate those variables
        strDay = WeekdayName(Weekday(calFlightSearch.SelectedDate))
        strDate = calFlightSearch.SelectedDate.ToString

        'add journeys for those days if they aren't added already
        DBAddJourney.AddJourney(strDay, strDate)

        'the only criteria we need to search on is date and time
        'converts to only the date calFlightSearch.SelectedDate.ToShortDateString

        ShowAll()

    End Sub

    Protected Sub gvDirectFlights_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDirectFlights.SelectedIndexChanged
        Dim intJourneyID As Integer
        Dim intJourneyNumber As Integer
        Dim strJourneyNumber As String
        Dim strDate As String
        Dim strTempAirport As String

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

            strDate = DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString)

            'add the first record of the reservation
            DBReservations.AddFirstReservationJourney("usp_ReservationsClone_Add_Journey", strJourneyNumber, intJourneyID, strDate)

            'update the session variable
            Session("JourneyNumber") = intJourneyNumber

            'retrieve the reservationID for the reservation we just added to. Store it in a new session variable
            Session.Add("ReservationID", DBReservations.GetNewestReservationID())

            'if this is a one way ticket, have them go book their seats
            If Session("TripType").ToString = "One Way" Then
                'remove session variables
                RemoveSessionVariablesAndRedirect()
                Exit Sub
            End If

            'if this is a round trip, send back the other 
            If Session("TripType").ToString = "Round Trip" Then
                'switch begin, end airport
                strTempAirport = Session("EndAirport").ToString
                Session("EndAirport") = Session("StartAirport")
                Session("StartAirport") = strTempAirport
                'reload the page and exit sub
                ShowAll()
                lblReturn.Visible = True
                Exit Sub
            End If

        Else
            'add a later journey
            DBReservations.AddLaterReservationJourneys("usp_ReservationsClone_Add_Later_Journeys", strJourneyNumber, intJourneyID, CInt(Session("ReservationID")))

            If Session("TripType").ToString = "Round Trip" Then
                'remove session variables
                RemoveSessionVariablesAndRedirect()
                Exit Sub
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

        Response.Redirect("Cust_SelectSeats.aspx")
    End Sub
End Class
