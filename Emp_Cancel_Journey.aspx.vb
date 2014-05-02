Option Strict On
Partial Class Emp_Cancel_Journey
    Inherits System.Web.UI.Page

    Dim DBJourney As New DBjourneyclone
    Dim DBFlightSearch As New DBFlightSearch
    Dim DBCrew As New ClassCrewScheduling
    Dim DBDate As New DBdate
    Dim DBAddJourney As New AddJourneyClass
    Dim FObject As New DBFlightsClone
    Dim CaObject As New CancelFlight

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'check for session variables
        If Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        ElseIf Session("UserType").ToString = "Manager" Then
            'everything's good, they're logged in as a manager
        Else
            Response.Redirect("HomePage.aspx")
        End If

        If IsPostBack = False Then
            calFlightSearch.SelectedDate = DBDate.GetCurrentDateReturnDate
            LoadFlightGridView()

        End If

        LoadFlightGridView()
    End Sub

    Public Sub LoadFlightGridView()
        'bind ddl for journeys
        DBJourney.GetJourneysForCrewByDate(DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString))
        gvJourneys.DataSource = DBJourney.MyView
        gvJourneys.DataBind()
        FormatDate(gvJourneys, 3)
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


    Protected Sub calFlightSearch_SelectionChanged(sender As Object, e As EventArgs) Handles calFlightSearch.SelectionChanged
        'define variables for day and date
        Dim strDay As String
        Dim strDate As String

        lblMessage.Text = ""

        If calFlightSearch.SelectedDate < DBDate.GetCurrentDateReturnDate Then
            lblMessage.Text = "You cannot cancel a past flight"
            calFlightSearch.SelectedDate = DBDate.GetCurrentDateReturnDate
            LoadFlightGridView()
            Exit Sub
        End If
        'populate those variables
        strDay = WeekdayName(Weekday(calFlightSearch.SelectedDate))
        strDate = DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString)

        'add journeys for those days if they aren't added already
        DBAddJourney.AddJourney(strDay, strDate)

        LoadFlightGridView()

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        EnterAbortConfirmMode()
    End Sub

    Protected Sub gvJourneys_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvJourneys.SelectedIndexChanged
        Session("RecordID") = gvJourneys.SelectedRow.Cells(1).Text
        lblMessage.Text = "Your currently selected record ID is " & Session("RecordID").ToString
    End Sub

    Public Sub EnterAbortConfirmMode()
        btnCancel.Visible = False
        btnAbort.Visible = True
        btnConfirm.Visible = True
        calFlightSearch.Enabled = False
        gvJourneys.Enabled = False
    End Sub

    Public Sub NormalMode()
        btnCancel.Visible = True
        btnAbort.Visible = False
        btnConfirm.Visible = False
        calFlightSearch.Enabled = True
        gvJourneys.Enabled = True
    End Sub

    Protected Sub btnAbort_Click(sender As Object, e As EventArgs) Handles btnAbort.Click
        NormalMode()
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If Session("RecordID") Is Nothing Then
            lblMessage.Text = "Please select a journey to cancel!"
            Exit Sub
        End If
        Try
            CaObject.InactivateSpecificJourney(Session("RecordID").ToString)
        Catch ex As Exception
            lblMessage.Text = "Something went wrong when trying to cancel the journey." & ex.Message
            Exit Sub
        End Try

        lblMessage.Text = "You have inactivated Journey #" & Session("RecordID").ToString
        NormalMode()
        LoadFlightGridView()
    End Sub


End Class
