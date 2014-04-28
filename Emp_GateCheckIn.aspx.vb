
Partial Class Emp_GateCheckIn
    Inherits System.Web.UI.Page
    Dim DBJourney As New DBjourneyclone
    Dim DBCustomer As New DBCustomersClone
    Dim DBDate As New DBdate
    Dim DBMilage As New DBMilage
    Dim DBTickets As New DBTickets
    Dim DBCrew As New ClassCrewScheduling



    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJourneys.SelectedIndexChanged
        lblMessage.Text = ""

        LoadCustomerGridView(ddlJourneys.SelectedValue.ToString)

    End Sub

    Public Sub LoadCustomerGridView(strJourneyID As String)
        'run stored procedure to get all customers and their miles and stuff
        DBCustomer.GetCustomersByJourney(strJourneyID)

        gvCustomers.DataSource = DBCustomer.MyDataset
        gvCustomers.DataBind()

        If gvCustomers.Rows.Count = 0 Then
            'throw them an error
            lblMessage.Text = "There are no customers on this flight. Please try another."
        Else
            gvCustomers.Visible = True
        End If

        gvCustomers.Visible = True
    End Sub

    Public Sub LoadCrewGridView(strJourneyID As String)
        DBCrew.GetCrewByJourney(strJourneyID)

        gvCrew.DataSource = DBCrew.MyView
        gvCrew.DataBind()

        If gvCrew.Rows.Count = 0 Then
            'throw them an error
            lblMessage.Text = "There are no crew members sheduled for this flight. Please schedule crew members"
        Else
            gvCrew.Visible = True
            btnDeparted.Visible = True
        End If




    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then
            LoadDDL()
            ddlJourneys.SelectedIndex = 0
            LoadCustomerGridView(ddlJourneys.SelectedValue.ToString)
        End If
    End Sub

    Public Sub LoadDDL()
        'bind ddl for journeys
        DBJourney.GetJourneysByDate(DBDate.GetCurrentDate())
        ddlJourneys.DataSource = DBJourney.MyView
        ddlJourneys.DataTextField = "FlightNumber"
        ddlJourneys.DataValueField = "JourneyID"
        ddlJourneys.DataBind()
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        'check to see if any customers showed up for the flight
        Dim chkPassengers As CheckBox
        Dim bolOnFlight As Boolean = False

        'loop through each record of gridview
        For i = 0 To gvCustomers.Rows.Count - 1
            'select a row
            gvCustomers.SelectedIndex = i
            chkPassengers = gvCustomers.SelectedRow.FindControl("chkPassengers")
            'check to see if the customer is there
            If chkPassengers.Checked = True Then
                'this passenger is on the flight
                bolOnFlight = True
            End If
        Next

        'if no one is on the flight
        If bolOnFlight = False Then
            'throw them an error
            lblMessage.Text = "We don't want to fly a plane with no customers on it."
            Exit Sub
        End If

        LoadCrewGridView(ddlJourneys.SelectedValue.ToString)

        If gvCrew.Rows.Count <> 0 Then
            btnConfirm.Visible = False
            ddlJourneys.Enabled = False
            gvCustomers.Enabled = False
        End If



    End Sub

    Protected Sub btnDeparted_Click(sender As Object, e As EventArgs) Handles btnDeparted.Click
        Dim chkPassengers As CheckBox
        Dim chkCrew As CheckBox
        Dim strMiles As String
        Dim intMiles As Integer
        Dim BolCrew As Boolean = True

        For i = 0 To gvCrew.Rows.Count - 1
            gvCrew.SelectedIndex = i
            chkCrew = gvCrew.SelectedRow.FindControl("chkCrew")

            'if any of the crew isn't present
            If chkCrew.Checked = False Then
                'make the boolean false
                BolCrew = False
            End If
        Next

        If BolCrew = False Then
            'journey can't take off
            lblMessage.Text = "This flight can't take off without all of its crew members."
            Exit Sub
        End If

        strMiles = DBMilage.GetMilesForFlight(ddlJourneys.SelectedItem.ToString)

        For i = 0 To gvCustomers.Rows.Count - 1
            gvCustomers.SelectedIndex = i
            chkPassengers = gvCustomers.SelectedRow.FindControl("chkPassengers")

            If chkPassengers.Checked = True Then
                'run if statement to see if they paid my miles or price
                If gvCustomers.SelectedRow.Cells(9).Text = "0" Then
                    'they have not paid any real money for their ticket
                    'they do not get miles
                Else
                    'give them miles
                    intMiles = CInt(gvCustomers.SelectedRow.Cells(5).Text)
                    intMiles += CInt(strMiles)
                    DBCustomer.UpdateMiles(intMiles.ToString, gvCustomers.SelectedRow.Cells(1).Text)
                    'run code to indicate they were on the flight
                    DBTickets.MarkOnFlight(gvCustomers.SelectedRow.Cells(7).Text)
                End If
            Else
                'don't add miles
                'send them an email -- Get Jace's help
            End If

        Next

        'mark journey as departed
        'this is commented out since we haven't done crew scheduling yet
        DBJourney.MarkJourneyDeparted(ddlJourneys.SelectedValue.ToString)

        'load the ddl
        LoadDDL()
        LoadCustomerGridView(ddlJourneys.SelectedValue.ToString)
        'fix what the user can touch on the form
        btnConfirm.Visible = True
        btnDeparted.Visible = False
        ddlJourneys.Enabled = True
        gvCustomers.Enabled = True
        gvCrew.Visible = False

        'SHOW A FLIGHT MANIFEST
    End Sub
End Class
