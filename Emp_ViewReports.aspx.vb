Option Strict On
Partial Class Emp_ViewReports
    Inherits System.Web.UI.Page

    'Declare an instance of Tickets DB
    Dim TicketsDB As New DBTickets
    Dim valid As New ClassValidate

    'Create a sub for loading the DDL
    Private Sub LoadDDLDeparture()
        'Purpose: Load the DDL
        'Inputs: None
        'Returns: Nothing; loads the ddl
        'Author: Dennis Phelan
        'Date Created: April 29, 2014
        'Date Last Modified: April 29, 2014

        'Get all of the city names
        TicketsDB.GetAllCities()

        'Load the DDL
        ddlCityOrRouteDepart.DataSource = TicketsDB.MyView.Table
        ddlCityOrRouteDepart.DataTextField = "AirportCode"
        ddlCityOrRouteDepart.DataValueField = "AirportCode"
        ddlCityOrRouteDepart.DataBind()
        ddlCityOrRouteDepart.Items.Insert(0, "ALL")
    End Sub

    Private Sub LoadDDLEnd()
        'Purpose: Load the DDL
        'Inputs: None
        'Returns: Nothing; loads the ddl
        'Author: Dennis Phelan
        'Date Created: April 29, 2014
        'Date Last Modified: April 29, 2014

        'Get all of the city names
        TicketsDB.GetAllCities()

        'Load the DDL
        ddlCityOrRouteEnd.DataSource = TicketsDB.MyView.Table
        ddlCityOrRouteEnd.DataTextField = "AirportCode"
        ddlCityOrRouteEnd.DataValueField = "AirportCode"
        ddlCityOrRouteEnd.DataBind()
        ddlCityOrRouteEnd.Items.Insert(0, "ALL")
    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Make sure pressing buttons does not keep reloading the page
        If IsPostBack = False Then
            'Load up the grid view
            TicketsDB.GetGrossRevenueAndSeatCount()

            gvReports.DataSource = TicketsDB.MyView

            gvReports.DataBind()

            LoadDDLDeparture()

            LoadDDLEnd()
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'Clear the error message
        lblMessage.Text = ""

        'Check to see which is selected of the reports - just revenue, just seats, or both
        TicketsDB.FilterClass(radClass.SelectedIndex, radRevenueSeatCount.SelectedIndex)

        'Prepare the check date function
        ''Validate that the lower date is less than the upper date
        'If calUpperDate.SelectedDate <> Nothing And valid.CheckLowerDateLessThanGreaterDate(calLowerDate.SelectedDate, calUpperDate.SelectedDate) = False Then
        '    lblMessage.Text = "The Lower Date must be earlier than the Upper Date."
        '    Exit Sub
        'End If

        ''Filter by date range or single date. Check to make sure that there are dates there first
        'If calLowerDate.SelectedDate <> Nothing Or calUpperDate.SelectedDate <> Nothing Then
        '    'If there is a date selected, filter
        '    TicketsDB.RevenueSeatFilterByDate(calLowerDate.SelectedDate, calUpperDate.SelectedDate)
        'End If


        'Filter by DepartureCity
        TicketsDB.FilterDepartureCity(ddlCityOrRouteDepart.SelectedValue)

        'Filter by EndCity
        TicketsDB.FilterEndCity(ddlCityOrRouteEnd.SelectedValue)

        gvReports.DataSource = TicketsDB.MyView

        gvReports.DataBind()
    End Sub

    Protected Sub btnRestartSearch_Click(sender As Object, e As EventArgs) Handles btnRestartSearch.Click
        'Clear the message
        lblMessage.Text = ""

        'Put the search back on both
        radRevenueSeatCount.SelectedIndex = 2

        'Clear the economy/first class
        radClass.SelectedIndex = -1

        'Clear the selected dates
        calLowerDate.SelectedDate = Nothing
        calUpperDate.SelectedDate = Nothing

        'Load up the grid view
        TicketsDB.GetGrossRevenueAndSeatCount()

        gvReports.DataSource = TicketsDB.MyView

        gvReports.DataBind()
    End Sub
End Class
