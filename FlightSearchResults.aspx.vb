
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim DBFlights As New DBFlightsClone
    Dim DBJourney As New DBjourneyclone
    Dim AddJourneyClass As New AddJourneyClass

    Dim DBFlightSearch As New DBFlightSearch

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'lblMessage.Text = calFlightSearch.SelectedDate.ToString

        'make sure a date is selected before they search
        calFlightSearch.SelectedDate = Now()


        'Dim strLogin As String
        'strLogin = Session("Login").ToString
        ''checks login session variable 
        ''if empty, neither select nor edit show up
        ''if it's a customer id then select shows up
        'If strLogin = "???????????" Then
        '    gvDirectFlights.AutoGenerateSelectButton = True
        '    gvIndirectFlights.AutoGenerateSelectButton = True
        'End If
        ''if it's an employee id then select and edit show up
        'If strLogin = "??????Employee" Then
        '    gvDirectFlights.AutoGenerateSelectButton = True
        '    gvDirectFlights.AutoGenerateEditButton = True
        '    gvIndirectFlights.AutoGenerateSelectButton = True
        '    gvIndirectFlights.AutoGenerateEditButton = True
        'End If


        ShowAll()

    End Sub

    Public Sub ShowAll()
        DBFlightSearch.GetALLFlightSearchUsingSP()

        SortandBind()
    End Sub

    Public Sub SortandBind()
        'Author: Ben Shadburne
        'Purpose: sort the data and bind it 
        'Arguments:  na
        'Return: sorted and binded data
        'Date: 03/18/2014

        'sort using radio
        'DB.DoSort(radSort.SelectedIndex)

        gvDirectFlights.DataSource = DBFlightSearch.MyView
        gvDirectFlights.DataBind()

        ' show record count
        lblCountDirect.Text = "Count: " & CStr(DBFlightSearch.lblCount)
    End Sub


    Protected Sub gvDirectFlights_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDirectFlights.SelectedIndexChanged

        'sets flight choice session variable to selected index
        Session("FlightChoice") = DBFlightSearch.MyView.Table().Rows(gvDirectFlights.SelectedIndex)

        lblMessage.Text = DBFlightSearch.MyView.Table().Rows(gvDirectFlights.SelectedIndex).ToString



    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click



        'define variables
        Dim strDay As String
        Dim strDate As String
        'put the name of the day of the week into strDay
        strDay = WeekdayName(Weekday(calFlightSearch.SelectedDate))

        strDate = calFlightSearch.SelectedDate.ToString

        'adds flights to the date, ensures we have flights to show
        AddJourneyClass.AddJourney(strDay, strDate)

        'first filter by time


    End Sub

    Protected Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        'just reloads dataset and binds it
        ShowAll()
    End Sub


End Class
