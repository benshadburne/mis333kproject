Option Strict On
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim DBJourney As New DBjourneyclone
    Dim DBFlightSearch As New DBFlightSearch
    Dim DBCrew As New ClassCrewScheduling
    Dim DBDate As New DBdate
    Dim DBAddJourney As New AddJourneyClass



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'NEED TO CHECK IF GATE AGENTS CAN SCHEDULE CREWS
        If Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        End If
        If Session("UserType").ToString <> "Manager" Then
            'they don't belong on this page
            If Session("UserType").ToString = "Customer" Then
                Response.Redirect("Cust_CustomerDashboard.aspx")
            Else
                Response.Redirect("Emp_EmployeeDashboard.aspx")
            End If
        Else
        End If

        If IsPostBack = False Then
            calFlightSearch.SelectedDate = CDate(DBDate.GetCurrentDate)
            LoadFlightGridView()
        End If

    End Sub

    Public Sub LoadFlightGridView()
        'bind ddl for journeys
        DBJourney.GetJourneysForCrewByDate(DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString))
        gvJourneys.DataSource = DBJourney.MyView
        gvJourneys.DataBind()
        FormatDate(gvJourneys, 3)
    End Sub

    Protected Sub calFlightSearch_SelectionChanged(sender As Object, e As EventArgs) Handles calFlightSearch.SelectionChanged
        'define variables for day and date
        Dim strDay As String
        Dim strDate As String

        lblMessage.Text = ""

        If calFlightSearch.SelectedDate < CDate(DBDate.ConvertToVBDate(DBDate.GetCurrentDate)) Then
            lblMessage.Text = "You cannot schedule crew for past flights"
            calFlightSearch.SelectedDate = CDate(DBDate.ConvertToVBDate(DBDate.GetCurrentDate))
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

    Private Sub FormatDate(gvGridview As GridView, intColumn As Integer)
        For i = 0 To gvGridview.Rows.Count - 1
            Dim datDate As Date
            Dim strDate As String
            datDate = CDate((gvGridview.Rows(i).Cells(intColumn).Text))
            strDate = datDate.ToShortDateString

            gvGridview.Rows(i).Cells(intColumn).Text = strDate
        Next
    End Sub

    Public Sub ShowDDLs()
        ddlCabins.Visible = True
        ddlCoCaptains.Visible = True
        ddlCaptains.Visible = True
    End Sub

    Protected Sub gvJourneys_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvJourneys.SelectedIndexChanged
        lblMessage.Text = ""
        gvJourneys.SelectedRow.Style.Add("background-color", "#ffcccc")
        LoadDDLs()
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim bolAdded As Boolean = False
        lblMessage.Text = ""
        'check to make sure the DDL Value isn't blank
        'check to make sure a Captain was added

        If ddlCaptains.SelectedIndex = 0 Then
            'don't do anything
            lblMessage.Text = "If no crew members are available for scheduling, hire some employees."
        Else
            DBJourney.AddCaptain(ddlCaptains.SelectedValue.ToString, gvJourneys.SelectedRow.Cells(1).Text)
            bolAdded = True
        End If

        If ddlCoCaptains.SelectedIndex = 0 Then
            lblMessage.Text = "If no crew members are available for scheduling, hire some employees."
        Else
            DBJourney.AddCoCaptain(ddlCoCaptains.SelectedValue.ToString, gvJourneys.SelectedRow.Cells(1).Text)
            bolAdded = True
        End If

        If ddlCabins.SelectedIndex = 0 Then
            lblMessage.Text = "If no crew members are available for scheduling, hire some employees."
        Else
            DBJourney.AddCabin(ddlCabins.SelectedValue.ToString, gvJourneys.SelectedRow.Cells(1).Text)
            bolAdded = True
        End If

        LoadFlightGridView()
        LoadDDLs()
        If bolAdded = True Then
            lblMessage.Text = "You successfully scheduled a crew."
        End If

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
        ddlCaptains.Items.Insert(0, "None")

        ddlCoCaptains.DataSource = aryCoCaptains
        ddlCoCaptains.DataBind()
        ddlCoCaptains.Items.Insert(0, "None")

        ddlCabins.DataSource = aryCabin
        ddlCabins.DataBind()
        ddlCabins.Items.Insert(0, "None")
        ShowDDLs()
    End Sub

    Protected Sub btnAddEmployee_Click(sender As Object, e As EventArgs) Handles btnAddEmployee.Click
        Response.Redirect("Emp_AddNewEmployee.aspx")
    End Sub
End Class
