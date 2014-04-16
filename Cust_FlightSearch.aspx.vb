﻿Option Strict On
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
        strFilterStatement = DBFlightSearch.FilterAll(Session("StartAirport").ToString, Session("EndAirport").ToString, calFlightSearch.SelectedDate.ToShortDateString)
        DBFlightSearch.MyView.RowFilter = strFilterStatement
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
End Class
