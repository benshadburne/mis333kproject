Option Strict On
Partial Class Emp_ModifyFlight
    Inherits System.Web.UI.Page

    'create instance of flight database class
    Dim FObject As New DBFlightsClone

    'create instance of mileage database class
    Dim MObject As New DBMileage

    'create instance of calculation database class
    Dim CObject As New ClassCalculate

    'create instance of validaiton class
    Dim VObject As New ClassValidate

    'create instance of journey class
    Dim JObject As New DBjourneyclone

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

    Protected Sub ddlFlights_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFlights.SelectedIndexChanged
        LoadInformationFromFlight(ddlFlights.SelectedIndex)
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
        LoadInformationFromFlight(ddlFlights.SelectedIndex)
        'show user success message
        lblMessage.Text = "Changes canceled."
        DisableThings()
        btnModify.Visible = True
        btnAccept.Visible = False
        btnCancel.Visible = False
    End Sub

    Protected Sub ddlDepartureTimeHour_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepartureTimeHour.SelectedIndexChanged, ddlDepartureTimeMinutes.SelectedIndexChanged
        'declarations
        Dim strDepartureTime As String
        Dim strArrivalTime As String

        'make it equal to hour and minute on ddls for departure time
        strDepartureTime = ddlDepartureTimeHour.SelectedItem.Text & ddlDepartureTimeMinutes.SelectedItem.Text
        'make intDepartureTime and intarrival time variables
        Dim intDepartureTime As Integer

        'check to see if valid integer
        intDepartureTime = VObject.CheckInteger(strDepartureTime)

        If intDepartureTime = -1 Then 'something went wrong
            Exit Sub
        End If

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

        'put it in label
        lblArrivalTime.Text = strArrivalTime


    End Sub

    Protected Sub btnModify_Click(sender As Object, e As EventArgs) Handles btnModify.Click
        EnableThings()
        btnModify.Visible = False
        btnAccept.Visible = True
        btnCancel.Visible = True
    End Sub

    Public Sub EnableThings()
        ddlFlights.Enabled = True
        ddlDepartureTimeHour.Enabled = True
        ddlDepartureTimeMinutes.Enabled = True
        txtBaseFare.Enabled = True
        cblDaysToFly.Enabled = True
    End Sub

    Public Sub DisableThings()
        ddlFlights.Enabled = False
        ddlDepartureTimeHour.Enabled = False
        ddlDepartureTimeMinutes.Enabled = False
        txtBaseFare.Enabled = False
        cblDaysToFly.Enabled = False
    End Sub

    Protected Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        Dim intBaseFare As Integer
        Dim strDepartureTime As String

        strDepartureTime = ddlDepartureTimeHour.SelectedValue.ToString + ddlDepartureTimeMinutes.SelectedValue.ToString

        'validate base fare
        intBaseFare = VObject.CheckInteger(txtBaseFare.Text)
        If intBaseFare = -1 Then
            lblMessage.Text = "Please enter a positive integer for base fare!"
            Exit Sub
        End If


        'create array list for parameter names
        Dim aryParamNames As New ArrayList

        'add parameter names to array list
        aryParamNames.Add("@FlightNumber")
        aryParamNames.Add("@DepartureCity")
        aryParamNames.Add("@EndCity")
        aryParamNames.Add("@DepartureTime")
        aryParamNames.Add("@ArrivalTime")
        aryParamNames.Add("@BaseFare")
        aryParamNames.Add("@Monday")
        aryParamNames.Add("@Tuesday")
        aryParamNames.Add("@Wednesday")
        aryParamNames.Add("@Thursday")
        aryParamNames.Add("@Friday")
        aryParamNames.Add("@Saturday")
        aryParamNames.Add("@Sunday")

        'create array list for parameter values
        Dim aryParamValues As New ArrayList
        'build variable for checking if a day of the week is selected
        Dim intCount As Integer
        'build array for days of week selected
        Dim Days(6) As String

        'check selected index of check list
        For i = 0 To cblDaysToFly.Items.Count - 1
            'check if item is selected. if yes, make Days(i) = "y", else, = "n"
            If cblDaysToFly.Items(i).Selected = True Then
                Days(i) = "Y"
                intCount += 1
            Else
                Days(i) = "N"
            End If

        Next

        If intCount = 0 Then
            lblMessage.Text = "Please select at least one day to fly!"
            Exit Sub
        End If

        'add parameter values to array list
        aryParamValues.Add(ddlFlights.SelectedValue.ToString)
        aryParamValues.Add(txtDepartureCity.Text)
        aryParamValues.Add(txtArrivalCity.Text)
        aryParamValues.Add(strDepartureTime)
        aryParamValues.Add(lblArrivalTime.Text)
        aryParamValues.Add(txtBaseFare.Text)
        aryParamValues.Add(Days(0))
        aryParamValues.Add(Days(1))
        aryParamValues.Add(Days(2))
        aryParamValues.Add(Days(3))
        aryParamValues.Add(Days(4))
        aryParamValues.Add(Days(5))
        aryParamValues.Add(Days(6))

        'call stored procedure to modify flight
        FObject.ModifyFlight(aryParamNames, aryParamValues)

        'show message of success
        lblMessage.Text = "Your flight has been modified."

        'exit modify mode
        DisableThings()
        btnModify.Visible = True
        btnCancel.Visible = False
        btnAccept.Visible = False

        'call sub to change status of flights that conflict with new flight, and cancel stuff
        FObject.GetALLFlightsCloneUsingSP()
        lblMessage.Text = FObject.MyDataSet.Tables("tblFlightClone").Rows(ddlFlights.SelectedIndex).Item("Tuesday").ToString
    End Sub

    Public Sub FlightModificationCheck()
        'dim Days array
        Dim Days(6) As String
        Dim intIndex As Integer
        intIndex = ddlFlights.SelectedIndex
        Dim intCount As Integer

        'find out what days the flight now flies
        FObject.GetALLFlightsCloneUsingSP()

        If FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("Monday").ToString = "Y" Then
            intCount += 1
            Days(0) = "Monday"
        Else
            Days(0) = "NULL"
        End If

        If FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("Tuesday").ToString = "Y" Then
            intCount += 1
            Days(1) = "Tuesday"
        Else
            Days(1) = "NULL"
        End If

        If FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("Wednesday").ToString = "Y" Then
            intCount += 1
            Days(2) = "Wednesday"
        Else
            Days(2) = "NULL"
        End If

        If FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("Thursday").ToString = "Y" Then
            intCount += 1
            Days(3) = "Thursday"
        Else
            Days(3) = "NULL"
        End If

        If FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("Friday").ToString = "Y" Then
            intCount += 1
            Days(4) = "Friday"
        Else
            Days(4) = "NULL"
        End If

        If FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("Saturday").ToString = "Y" Then
            intCount += 1
            Days(5) = "Saturday"
        Else
            intCount += 1
            Days(5) = "NULL"
        End If

        If FObject.MyDataSet.Tables("tblFlightClone").Rows(intIndex).Item("Sunday").ToString = "Y" Then
            intCount += 1
            Days(6) = "Sunday"
        Else
            Days(6) = "NULL"
        End If

        Dim GoodDays(intCount - 1) As String

        For i = 0 To 6
            If Days(i) <> "NULL" Then
                GoodDays(i) = Days(i)
            End If
        Next

        'can I just check what's checked on page, and see if it's a yes in database?  
        'No, because that would necessitate remembering what WAS checked, and I don't want to use 7 session variables
        'so see what's not checked, then run query on database to see if there's a day present that isn't checked
        'have 7 places where flight can be cancelled, one for each of the 7 days

    End Sub
End Class
