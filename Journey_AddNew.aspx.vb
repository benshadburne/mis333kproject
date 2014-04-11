
Partial Class Journey_AddNew

    Inherits System.Web.UI.Page
    Dim DBFlights As New DBFlightsClone
    Dim DBJourney As New DBjourneyclone

    Private Sub LoadDDL()
        DBFlights.GetALLFlightsCloneUsingSP()

        ddlFlights.DataSource = DBFlights.MyDataSet

        ddlFlights.DataTextField = "FlightNumber"
        ddlFlights.DataBind()

    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'define variables
        Dim intRow As Integer
        Dim strDay As String

        'intRow keeps track of selected value of ddl
        intRow = ddlFlights.SelectedIndex

        'put the name of the day of the week into strDay
        strDay = WeekdayName(Weekday(calDate.SelectedDate))
        'the textbox shows the day in strDay -- I just did this to double check that it was working properly
        TextBox1.Text = strDay

        'This fills the DBFlights.MyDataSet and DBFlights.MyView with the Flights the Journey table should have in it for the given date
        DBFlights.CheckFlightsNeededForSpecificDate("usp_FlightClone_Need_Journeys", "@DayOfWeek", strDay)

        'this fills the DB Journey Dataset and DB Journey Dataview with journeys that have already been created for the specific day
        DBJourney.GetJourneysByDate("usp_JourneyClone_Choose_Active_By_Day", "tblJourneyClone", strDay, calDate.SelectedDate)

        'Run code to make sure we don't add duplicate journeys. Then add required journeys. 
        DBJourney.CheckWhichJourneysToAdd(DBJourney.MyDataSet, DBFlights.MyDataSet, calDate.SelectedDate)

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then
            LoadDDL()
        End If
    End Sub

End Class
