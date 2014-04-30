Option Strict On
Imports System.Data
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
        'NEED TO CHECK IF GATE AGENTS CAN ADD CITIES
        If Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        ElseIf Session("UserType").ToString <> "Manager" Then
            Response.Redirect("Emp_EmployeeDashboard.aspx")
        End If

        'Make sure pressing buttons does not keep reloading the page
        If IsPostBack = False Then
            'Load up the grid view
            TicketsDB.GetGrossRevenueAndSeatCount()

            LoadGridView()

            LoadDDLDeparture()

            LoadDDLEnd()
        End If
    End Sub


    Public Sub LoadGridView()
        Dim decRevenue As Decimal = 0
        Dim intSeats As Integer = 0
        gvReports.DataSource = TicketsDB.MyView

        gvReports.DataBind()

        lblRevenue.Text = ""
        lblSeats.Text = ""

        If gvReports.Rows.Count = 0 Then
            lblMessage.Text = "Your search returned no records"
            Exit Sub
        End If

        For i = 0 To gvReports.Rows.Count - 1
            Dim datDate As Date
            Dim strDate As String
            datDate = CDate((gvReports.Rows(i).Cells(2).Text))
            strDate = datDate.ToShortDateString

            gvReports.Rows(i).Cells(2).Text = strDate
        Next

        If radRevenueSeatCount.SelectedIndex = 0 Then
            For i = 0 To gvReports.Rows.Count - 1
                intSeats += CInt(gvReports.Rows(i).Cells(5).Text)
            Next

            lblSeats.Text = "Total Seats: " & intSeats.ToString

        Else
            For i = 0 To gvReports.Rows.Count - 1
                decRevenue += CDec(gvReports.Rows(i).Cells(5).Text)
            Next

            lblRevenue.Text = "Total Revenue: " & decRevenue.ToString("c2")

            If radRevenueSeatCount.SelectedIndex = 2 Then
                For i = 0 To gvReports.Rows.Count - 1
                    intSeats += CInt(gvReports.Rows(i).Cells(6).Text)
                Next

                lblSeats.Text = "Total Seats: " & intSeats.ToString
            End If
        End If
    End Sub


    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'Clear the error message
        lblMessage.Text = ""

        'Declare the strFilterStatement
        Dim strFilterStatement1 As String
        Dim strFilterStatement2 As String
        Dim strFilterStatement3 As String
        Dim strFilterStatementOverall As String

        'Check to see which is selected of the reports - just revenue, just seats, or both
        TicketsDB.FilterClass(radClass.SelectedIndex, radRevenueSeatCount.SelectedIndex)

        'Prepare the check date function
        ''Validate that the lower date is less than the upper date
        If calUpperDate.SelectedDate <> Nothing And valid.CheckLowerDateLessThanGreaterDate(calLowerDate.SelectedDate, calUpperDate.SelectedDate) = False Then
            lblMessage.Text = "The Lower Date must be earlier than the Upper Date."
            Exit Sub
        End If

        'Filter by date range or single date. Check to make sure that there are dates there first
        If calLowerDate.SelectedDate <> Nothing Or calUpperDate.SelectedDate <> Nothing Then
            'If there is a date selected, filter
            strFilterStatement1 = TicketsDB.RevenueSeatFilterByDate(calLowerDate.SelectedDate, calUpperDate.SelectedDate)
        End If

        'Add whatever the result is of the End City for the filter statement
        strFilterStatement2 = TicketsDB.FilterEndCity(ddlCityOrRouteEnd.SelectedValue)

        'Add whatever the result is of the Departure city for the filter statement
        strFilterStatement3 = TicketsDB.FilterDepartureCity(ddlCityOrRouteDepart.SelectedValue)

        'If nothing is selected for date and ALL is selected on both ddls, return Nothing for the row filter
        If strFilterStatement1 = Nothing And strFilterStatement2 = Nothing And strFilterStatement3 = Nothing Then
            strFilterStatementOverall = Nothing

            'If there is no date and Departure city, just make strFilterStatementOverall = to End City
        ElseIf strFilterStatement1 = Nothing And strFilterStatement3 = Nothing Then
            strFilterStatementOverall = strFilterStatement2

            'If there is no date and End city, just make strFilterStatementOverall = to Departure City
        ElseIf strFilterStatement1 = Nothing And strFilterStatement2 = Nothing Then
            strFilterStatementOverall = strFilterStatement3

            'If there is no Departure Date and End city, just make strFilterStatementOverall = to Date
        ElseIf strFilterStatement2 = Nothing And strFilterStatement3 = Nothing Then
            strFilterStatementOverall = strFilterStatement1

            'If there is no date, make strFilterStatementOverall = to Departure City and End City
        ElseIf strFilterStatement1 = Nothing Then
            strFilterStatementOverall = strFilterStatement2 + " and " + strFilterStatement3

            'If there is no departure city, make strFilterStatementOverall = to Date and End City
        ElseIf strFilterStatement3 = Nothing Then
            strFilterStatementOverall = strFilterStatement1 + " and " + strFilterStatement2

            'If there is no end city, make strFilterStatementOverall = to Date and Departure City
        ElseIf strFilterStatement2 = Nothing Then
            strFilterStatementOverall = strFilterStatement1 + " and " + strFilterStatement3

            'If everything equals something, then make it equal to date and Departure City and End City
        Else
            strFilterStatementOverall = strFilterStatement1 + " and " + strFilterStatement2 + " and " + strFilterStatement3
        End If

        'Filter the statement
        TicketsDB.RowFilter(strFilterStatementOverall)

        'Bind the data
        LoadGridView()
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

        LoadGridView()

        'Load the DDLs
        LoadDDLDeparture()
        LoadDDLEnd()
    End Sub
End Class
