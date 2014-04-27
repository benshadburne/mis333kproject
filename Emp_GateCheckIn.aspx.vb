
Partial Class Emp_GateCheckIn
    Inherits System.Web.UI.Page
    Dim DBJourney As New DBjourneyclone
    Dim DBCustomer As New DBCustomersClone
    Dim DBDate As New DBdate
    Dim DBMilage As New DBMilage
    Dim DBTickets As New DBTickets



    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJourneys.SelectedIndexChanged
        'run stored procedure to get all customers and their miles and stuff
        DBCustomer.GetCustomersByJourney(ddlJourneys.SelectedValue.ToString)

        gvCustomers.DataSource = DBCustomer.MyDataset
        gvCustomers.DataBind()

        gvCustomers.Visible = True

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then
            LoadDDL()
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
        Dim CheckBox As CheckBox
        Dim strMiles As String
        Dim intMiles As Integer

        strMiles = DBMilage.GetMilesForFlight(ddlJourneys.SelectedItem.ToString)

        For i = 0 To gvCustomers.Rows.Count - 1
            gvCustomers.SelectedIndex = i
            CheckBox = gvCustomers.SelectedRow.FindControl("chkOnFlight")

            If CheckBox.Checked = True Then
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
                'run code to say they weren't on the flight
            End If

        Next

        ''mark journey as departed
        ''this is commented out since we haven't done crew scheduling yet
        'DBJourney.MarkJourneyDeparted(gvCustomers.SelectedValue.ToString)
    End Sub
End Class
