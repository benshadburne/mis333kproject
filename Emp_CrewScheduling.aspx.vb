
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim DBJourney As New DBjourneyclone
    Dim DBFlightSearch As New DBFlightSearch




    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then
            LoadDDL()
        End If
    End Sub

    Public Sub LoadDDL()
        'bind ddl for journeys
        DBJourney.GetJourneysByDate(DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString))
        ddlJourneys.DataSource = DBJourney.MyView
        ddlJourneys.DataTextField = "FlightNumber"
        ddlJourneys.DataValueField = "JourneyID"
        ddlJourneys.DataBind()
    End Sub

    Protected Sub calFlightSearch_SelectionChanged(sender As Object, e As EventArgs) Handles calFlightSearch.SelectionChanged
        LoadDDL()
    End Sub
End Class
