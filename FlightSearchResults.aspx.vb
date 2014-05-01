
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim DBFlights As New DBFlightsClone
    Dim DBJourney As New DBjourneyclone
    Dim AddJourneyClass As New AddJourneyClass
    Dim DBJourneySeats As New DBJourneySeats
    Dim DBDate As New DBdate

    Dim DBFlightSearch As New DBFlightSearch

   
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim strDate As String

        strDate = DBDate.GetCurrentDate()

        strDate = DBDate.ConvertToVBDate(strDate).ToString

        'makes sure selected date for first time is today
        If IsPostBack = False Then
            calFlightSearch.SelectedDate = Date.Parse(strDate).AddDays(1)
        End If
        'need to add flights before datasets are loaded
        'define variables
        AddJourneys()

        ShowAll()
        SortandBind()

        'lblMessage.Text = DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString)


    End Sub

    Public Sub AddJourneys()
        Dim strDay As String
        Dim strDate As String
        'put the name of the day of the week into strDay
        strDay = WeekdayName(Weekday(calFlightSearch.SelectedDate))

        strDate = (DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString))

        'adds flights to the date, ensures we have flights to show
        AddJourneyClass.AddJourney(strDay, strDate)
    End Sub

    Public Sub ShowAll()

        DBFlightSearch.GetAllJourneysSeatsEmpty(1)

    End Sub

    Public Sub SortandBind()
        'Author: Ben Shadburne
        'Purpose: sort the data and bind it 
        'Arguments:  na
        'Return: sorted and binded data
        'Date: 03/18/2014

        'sort 
        DBFlightSearch.DoSort()

        'bind all data
        gvDirectFlights.DataSource = DBFlightSearch.MyView
        gvDirectFlights.DataBind()
        gvIndirectStart.DataSource = DBFlightSearch.MyViewStart
        gvIndirectStart.DataBind()
        gvIndirectFinish.DataSource = DBFlightSearch.MyViewFinish
        gvIndirectFinish.DataBind()

        FormatDate(gvDirectFlights, 1)
        FormatDate(gvIndirectStart, 2)
        FormatDate(gvIndirectFinish, 1)

        ' show record count
        lblCountDirect.Text = CStr(DBFlightSearch.lblCount)
        lblCountIndirect.Text = CStr(DBFlightSearch.lblCountStart)
        lblCountFinish.Text = CStr(DBFlightSearch.lblCountFinish)
    End Sub

    Private Sub FormatDate(gvGridview As GridView, intColumn As Integer)
        For i = 0 To gvGridview.Rows.Count - 1
            Dim datDate As Date
            Dim strDate As String
            datDate = CDate((gvGridview.Rows(i).Cells(intColumn).Text))
            strDate = datDate.ToShortDateString

            gvGridview.Rows(i).Cells(intColumn).Text = strDate
        Next
    End Sub


    Protected Sub gvDirectFlights_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDirectFlights.SelectedIndexChanged

        ''sets flight choice session variable to selected index
        'Session("FlightChoice") = DBFlightSearch.MyView.Table().Rows(gvDirectFlights.SelectedIndex)

        'lblMessage.Text = DBFlightSearch.MyView.Table().Rows(gvDirectFlights.SelectedIndex).ToString


    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        lblMessage.Text = ""
        AddJourneys()
        SearchBtn()

        SortandBind()

        'also gotta make second leg stuff invisible
        gvIndirectFinish.Visible = False
        lblIndirectFinishC.Visible = False
        lblIndirectFinish.Visible = False
        lblCountFinish.Visible = False

    End Sub

    Public Sub SearchBtn()
        DBFlightSearch.SearchDirect(ddlDepart.SelectedValue, ddlArrival.SelectedValue, DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString))
        DBFlightSearch.SearchIndirectStart(ddlDepart.SelectedValue, ddlArrival.SelectedValue, DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString))

        gvIndirectStart.Visible = True
        lblIndirectStart.Visible = True
        lblIndirectStartC.Visible = True
        lblCountIndirect.Visible = True

    End Sub

    Protected Sub gvIndirectStart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvIndirectStart.SelectedIndexChanged

        'need to do searchbtn b/c otherwise other gv's get reset
        SearchBtn()
        SortandBind()
        DBFlightSearch.SearchIndirectFinish(gvIndirectStart.Rows(gvIndirectStart.SelectedIndex).Cells(6).Text, ddlArrival.SelectedValue, _
        DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString), _
        gvIndirectStart.Rows(gvIndirectStart.SelectedIndex).Cells(7).Text)

        SortandBind()
        'check if there's anything in second gv
        If CInt(DBFlightSearch.lblCountFinish) = 0 Then
            lblMessage.Text = "No second leg results"
            Exit Sub
        End If



        'and make the count equal to the count
        lblCountFinish.Text = DBFlightSearch.lblCountFinish.ToString

        gvIndirectFinish.Visible = True
        lblIndirectFinishC.Visible = True
        lblIndirectFinish.Visible = True
        lblCountFinish.Visible = True


    End Sub

    Protected Sub gvIndirectFinish_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvIndirectFinish.SelectedIndexChanged

        'on flightsearch this does nothing, on emp and cust it does something

    End Sub

End Class
