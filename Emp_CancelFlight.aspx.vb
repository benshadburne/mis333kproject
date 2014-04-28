Option Strict On
Partial Class Emp_CancelFlight
    Inherits System.Web.UI.Page

    'create instance of flight database class
    Dim FObject As New DBFlightsClone

    'create instance of mileage database class
    Dim MObject As New DBMileage

    'create instance of calculation database class
    Dim CObject As New ClassCalculate


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'if first time loading page, set datasource of ddlFlights
        If IsPostBack = False Then
            'get all flights into database
            FObject.GetALLFlightsCloneUsingSP()
            ddlFlights.DataSource = FObject.MyDataSet.Tables("tblFlightClone")
            ddlFlights.DataTextField = "FlightNumber"
            ddlFlights.DataBind()
            ddlFlights.SelectedIndex = 0
            LoadInformationFromFlight(ddlFlights.SelectedIndex)
        End If

    End Sub

    'sub will load all needed information into rest of fields once user enters flight number
    Public Sub LoadInformationFromFlight(intIndex As Integer)
        'set variables for hours and minutes 
        Dim strDepartureTimeRaw As String
        Dim strDepartureTimeHour As String
        Dim strDepartureTimeMinute As String
        Dim intDepartureTime As Integer
        Dim strArrivalTime As String

        FObject.GetALLFlightsCloneUsingSP()
        'load departure city
        txtDepartureCity.Text = FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("DepartureCity").ToString
        'load arrival city
        txtArrivalCity.Text = FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("EndCity").ToString
        'load departure time
        strDepartureTimeRaw = FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("DepartureTime").ToString
        If strDepartureTimeRaw.Length = 3 Then
            strDepartureTimeHour = strDepartureTimeRaw.Substring(0, 1)
            strDepartureTimeMinute = strDepartureTimeRaw.Substring(1, 2)
        ElseIf strDepartureTimeRaw.Length = 4 Then
            strDepartureTimeHour = strDepartureTimeRaw.Substring(0, 2)
            strDepartureTimeMinute = strDepartureTimeRaw.Substring(2, 2)
        Else
            lblMessage.Text = "Something has gone horribly wrong."
            Exit Sub
        End If

        ddlDepartureTimeHour.SelectedValue = strDepartureTimeHour
        ddlDepartureTimeMinutes.SelectedValue = strDepartureTimeMinute
        'load arrival time
        intDepartureTime = CInt(FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("DepartureTime"))
        'create variable for duration of flight
        Dim intDuration As Integer

        'find duration
        Dim aryParamNamesMileage As New ArrayList
        Dim aryParamValuesMileage As New ArrayList

        'add param names to array list
        aryParamNamesMileage.Add("@StartAirport")
        aryParamNamesMileage.Add("@EndAirport")

        'add param values to array list
        aryParamValuesMileage.Add(txtDepartureCity.Text)
        aryParamValuesMileage.Add(txtArrivalCity.Text)

        'find duration of flight
        MObject.FindDuration(aryParamNamesMileage, aryParamValuesMileage)
        intDuration = CInt(MObject.MyDataSet.Tables("tblMileageClone").Rows(0).Item(0))

        'calculate the arrival time that needs to appear
        strArrivalTime = CObject.CalculateArrivalTime(intDepartureTime, intDuration)
        'create session variable for arrival time
        Session("ArrivalTime") = strArrivalTime

        'put it in label
        lblArrivalTime.Text = strArrivalTime

        'load base fare
        txtBaseFare.Text = FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("BaseFare").ToString
        'load days flown
        'load Monday
        If FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("Monday").ToString = "Y" Then
            cblDaysToFly.Items(0).Selected = True
        End If

        'load Tuesday
        If FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("Tuesday").ToString = "Y" Then
            cblDaysToFly.Items(1).Selected = True
        End If

        'load Wednesday
        If FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("Wednesday").ToString = "Y" Then
            cblDaysToFly.Items(2).Selected = True
        End If

        'load Thursday
        If FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("Thursday").ToString = "Y" Then
            cblDaysToFly.Items(3).Selected = True
        End If

        'load Friday
        If FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("Friday").ToString = "Y" Then
            cblDaysToFly.Items(4).Selected = True
        End If

        'load Saturday
        If FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("Saturday").ToString = "Y" Then
            cblDaysToFly.Items(5).Selected = True
        End If

        'load Sunday 
        If FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("Sunday").ToString = "Y" Then
            cblDaysToFly.Items(6).Selected = True
        End If

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        EnterConfirmAbortMode()
    End Sub

    Protected Sub btnAbort_Click(sender As Object, e As EventArgs) Handles btnAbort.Click
        ProtectedMode()
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click


        lblMessage.Text = "Flight #" + ddlFlights.SelectedItem.ToString + " has been inactivated. All journeys scheduled have been cancelled."
        ProtectedMode()
    End Sub

    Public Sub EnterConfirmAbortMode()
        btnCancel.Visible = False
        btnAbort.Visible = True
        btnConfirm.Visible = True
        ddlFlights.Enabled = False

    End Sub

    Public Sub ProtectedMode()
        btnCancel.Visible = True
        btnAbort.Visible = False
        btnConfirm.Visible = False
        ddlFlights.Enabled = True
    End Sub

    Protected Sub ddlFlights_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFlights.SelectedIndexChanged
        LoadInformationFromFlight(ddlFlights.SelectedIndex)
    End Sub
End Class
