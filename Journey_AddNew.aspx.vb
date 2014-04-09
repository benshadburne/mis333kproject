
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
        Dim intRow As Integer

        intRow = ddlFlights.SelectedIndex

        Dim aryParamNames As New ArrayList

        aryParamNames.Add("@FlightNumber")
        aryParamNames.Add("@FlightDate")
        aryParamNames.Add("@DepartureTime")

        DBFlights.GetALLFlightsCloneUsingSP()


        Dim aryParamValues As New ArrayList

        aryParamValues.Add(CInt(DBFlights.MyDataSet.Tables("tblFlightClone").Rows(intRow).Item("FlightNumber")))
        aryParamValues.Add(calDate.SelectedDate)
        aryParamValues.Add(CInt(DBFlights.MyDataSet.Tables("tblFlightClone").Rows(intRow).Item("DepartureTime")))



        DBJourney.AddNewJourney("usp_JourneyClone_Add_New", aryParamNames, aryParamValues)

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then
            LoadDDL()
        End If
    End Sub
End Class
