Option Strict On
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim DBFlightSearch As New DBFlightSearch
    Dim DBAddJourney As New AddJourneyClass

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'show flights available
        ShowAll()
    End Sub

    Protected Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' lblMessage.Text = ddlTimeOfDay.SelectedValue

        'make sure a date is selected before they search
        If IsPostBack = False Then
            calFlightSearch.SelectedDate = Now()
        End If


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


        ShowAll()
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



        gvDirectFlights.DataSource = DBFlightSearch.MyView
        gvDirectFlights.DataBind()

        'filter using start and end airport -- will need to put this in a class
        DBFlightSearch.SearchByAirports(Session("StartAirport").ToString, Session("EndAirport").ToString)
        ' show record count
        lblCountDirect.Text = "Count: " & CStr(DBFlightSearch.lblCount)
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
        DBFlightSearch.SearchByDate(calFlightSearch.SelectedDate.ToShortDateString)
        DBFlightSearch.SearchTime(ddlTimeOfDay.SelectedValue)

    End Sub
End Class
