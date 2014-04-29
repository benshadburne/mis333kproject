
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim DBJourney As New DBjourneyclone
    Dim DBFlightSearch As New DBFlightSearch
    Dim DBCrew As New ClassCrewScheduling
    Dim DBDate As New DBdate
    Dim DBAddJourney As New AddJourneyClass



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
       
        If IsPostBack = False Then
            calFlightSearch.SelectedDate = DBDate.GetCurrentDate
            LoadFlightGridView()
        End If
    End Sub

    Public Sub LoadFlightGridView()
        'bind ddl for journeys
        DBJourney.GetJourneysForCrewByDate(DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString))
        gvJourneys.DataSource = DBJourney.MyView
        gvJourneys.DataBind()
    End Sub

    Protected Sub calFlightSearch_SelectionChanged(sender As Object, e As EventArgs) Handles calFlightSearch.SelectionChanged
        'define variables for day and date
        Dim strDay As String
        Dim strDate As String

        lblMessage.Text = ""

        If calFlightSearch.SelectedDate < DBDate.GetCurrentDate Then
            lblMessage.Text = "You cannot schedule crew for past flights"
            calFlightSearch.SelectedDate = DBDate.GetCurrentDate
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


    Protected Sub gvJourneys_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvJourneys.SelectedIndexChanged
        lblMessage.Text = ""
        gvJourneys.SelectedRow.Style.Add("background-color", "#ffcccc")
        LoadDDLs()
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        lblMessage.Text = ""
        'check to make sure the DDL Value isn't blank
        If ddlCaptains.SelectedValue = "" Then
            lblMessage.Text = "You don't have a captain available at this time. Consider hiring one."
            LoadFlightGridView()
            LoadDDLs()
            Exit Sub
        End If
        If ddlCoCaptains.SelectedValue = "" Then
            lblMessage.Text = "You don't have a cocaptain available at this time. Consider hiring one."
            LoadFlightGridView()
            LoadDDLs()
            Exit Sub
        End If
        If ddlCabins.SelectedValue = "" Then
            lblMessage.Text = "You don't have a cabin member available at this time. Consider hiring one."
            LoadFlightGridView()
            LoadDDLs()
            Exit Sub
        End If

        DBCrew.AddCrew(ddlCaptains.SelectedValue.ToString, ddlCoCaptains.SelectedValue.ToString, ddlCabins.SelectedValue.ToString, gvJourneys.SelectedRow.Cells(1).Text)
        LoadFlightGridView()
        LoadDDLs()

        lblMessage.Text = "You successfully scheduled a crew."
    End Sub

    Public Sub LoadDDLs()
        Dim strDate As String
        Dim aryCaptains As New ArrayList
        Dim aryCoCaptains As New ArrayList
        Dim aryCabin As New ArrayList


        strDate = DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString)
        aryCaptains = DBCrew.FindAvailableCaptains(strDate, CInt(gvJourneys.SelectedRow.Cells(4).Text), CInt(gvJourneys.SelectedRow.Cells(10).Text))
        aryCoCaptains = DBCrew.FindAvailableCoCaptains(strDate, CInt(gvJourneys.SelectedRow.Cells(4).Text), CInt(gvJourneys.SelectedRow.Cells(10).Text))
        aryCabin = DBCrew.FindAvailableCabin(strDate, CInt(gvJourneys.SelectedRow.Cells(4).Text), CInt(gvJourneys.SelectedRow.Cells(10).Text))

        ddlCaptains.DataSource = aryCaptains
        ddlCaptains.DataBind()

        ddlCoCaptains.DataSource = aryCoCaptains
        ddlCoCaptains.DataBind()

        ddlCabins.DataSource = aryCabin
        ddlCabins.DataBind()
    End Sub
End Class
