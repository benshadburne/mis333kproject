
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim DBFlights As New DBFlightsClone
    Dim DBJourney As New DBjourneyclone
    Dim AddJourneyClass As New AddJourneyClass
    Dim DBJourneySeats As New DBJourneySeats

    Dim DBFlightSearch As New DBFlightSearch

   
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim h As String
        h = calFlightSearch.SelectedDate.ToShortDateString
        h = DBFlightSearch.AlterDate(h)
        lblMessage.Text = h

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

        lblMessage.Text = ""

        'define variables
        Dim strDay As String
        Dim strDate As String
        'put the name of the day of the week into strDay
        strDay = WeekdayName(Weekday(calFlightSearch.SelectedDate))

        strDate = DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToString)

        'adds flights to the date, ensures we have flights to show
        AddJourneyClass.AddJourney(strDay, strDate)

        'filter info for regular search A -> B
        DBFlightSearch.FilterRegular(ddlDepart.SelectedValue, ddlArrival.SelectedValue, strDate)

        SortandBind()

        'make sure regular count isn't 0
        If DBFlightSearch.lblCount = 0 Then
            lblMessage.Text = "No Journeys found, please try again with different criteria"
            Exit Sub
        End If

        'run info for indirect search A -> C -> B
        DBFlightSearch.FilterStart(ddlDepart.SelectedValue, ddlArrival.SelectedValue, strDate)

        'make visible the indirect stuff
        gvIndirectStart.Visible = True
        lblIndirect.Visible = True
        lblCountDirect.Visible = True

    End Sub

    Protected Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        'just reloads dataset and binds it
        ShowAll()
    End Sub

    Public Function CreateSeatsArray() As ArrayList

        Dim arlSeats As New ArrayList
        Dim i As Integer

        For i = 1 To 5

            arlSeats.Add(i & "A")
            arlSeats.Add(i & "B")
            If i > 2 Then
                arlSeats.Add(i & "C")
                arlSeats.Add(i & "D")
            End If

        Next

        Return arlSeats
    End Function

    Protected Sub gvIndirectStart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvIndirectStart.SelectedIndexChanged

        'search db for journeys on same day, starting from the ending location of this one, and start after this one finishes

        'make gvIndirectFinish visible
        gvIndirectFinish.Visible = True

    End Sub

    Protected Sub gvIndirectFinish_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvIndirectFinish.SelectedIndexChanged

        'on flightsearch this does nothing, on emp and cust it does something

    End Sub
End Class
