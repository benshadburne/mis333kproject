
Option Strict On
Partial Class Journey_AddNew

    Inherits System.Web.UI.Page
    Dim DBFlights As New DBFlightsClone
    Dim DBJourney As New DBjourneyclone
    Dim AddFlightClass As New AddFlightClass

    Private Sub LoadDDL()
        DBFlights.GetALLFlightsCloneUsingSP()

        ddlFlights.DataSource = DBFlights.MyDataSet

        ddlFlights.DataTextField = "FlightNumber"
        ddlFlights.DataBind()

    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'define variables
        Dim strDay As String
        Dim strDate As String
        'put the name of the day of the week into strDay
        strDay = WeekdayName(Weekday(calDate.SelectedDate))

        strDate = calDate.SelectedDate.ToString

        AddFlightClass.AddFlight(strDay, strDate)

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then
            LoadDDL()
        End If
    End Sub

End Class
