﻿Option Strict On
'author: Jace Barton
'date: 4/17/14
'purpose: add a flight to the flight database table
Partial Class Emp_AddFlight
    Inherits System.Web.UI.Page

    'create instance of flight class ***ASK BEN WHY NAMED DBFLIGHTSCLONE
    Dim FObject As New DBFlightsClone

    'create instance of Airport class ***ASK AARYAMAN WHY NAMED DBAIRPORTCLONE
    Dim AObject As New DBairportclone

    'create instance of validation class
    Dim VObject As New ClassValidate

    'create instance of calculation class
    Dim CObject As New ClassCalculate

    'create instance of mileage class
    Dim MObject As New DBMileage

    Dim strDepartureTime As String
    Dim strArrivalTime As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'check for session variables
        If Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        ElseIf Session("UserType").ToString = "Manager" Then
            'everything's good, they're logged in as a manager
        Else
            Response.Redirect("HomePage.aspx")
        End If

        'if first time loading page, load drop down lists
        If IsPostBack = False Then
            'get all airports
            AObject.GetALLairportcloneUsingSP()
            'set datasource to all airports
            LoadDDLs()
            'set selected index

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
            aryParamValuesMileage.Add(ddlDepartureCity.SelectedValue)
            aryParamValuesMileage.Add(ddlArrivalCity.SelectedValue)

            'find duration of flight
            MObject.FindDuration(aryParamNamesMileage, aryParamValuesMileage)
            intDuration = CInt(MObject.MyDataSet.Tables("tblMileageClone").Rows(0).Item("FlightTime"))

            'calculate the arrival time that needs to appear
            strArrivalTime = CObject.CalculateArrivalTime(intDepartureTime, intDuration)

            'put it in label
            lblArrivalTime.Text = strArrivalTime

        End If
    End Sub

    Private Sub LoadDDLs()

        AObject.GetALLairportcloneUsingSP()

        ddlDepartureCity.DataSource = AObject.MyDataSet.Tables("tblAirportClone")
        'set datafield showing to city names
        ddlDepartureCity.DataTextField = "CityName"
        'set datafield value associated with what's showing to 3 letter airport code
        ddlDepartureCity.DataValueField = "AirportCode"
        'bind ddl
        ddlDepartureCity.DataBind()


        ddlArrivalCity.DataSource = AObject.MyDataSet.Tables("tblAirportClone")
        'set datafield showing to city names
        ddlArrivalCity.DataTextField = "CityName"
        'set datafield value associated with what's showing to 3 letter airport code
        ddlArrivalCity.DataValueField = "AirportCode"
        'bind ddl
        ddlArrivalCity.DataBind()

        ddlDepartureCity.SelectedIndex = 0

        'do same as above, but for arrival city
        'set datasource to all airports
        'set selected index
        ddlArrivalCity.SelectedIndex = 1


    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'build variable for flight number
        Dim intFlightNumber As Integer

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

        Try
            intFlightNumber = CInt(txtFlightNumber.Text)
        Catch ex As Exception
            lblMessage.Text = "Please enter a positive integer for flight number!"
            Exit Sub
        End Try

        'validate flight number is integer 
        If VObject.CheckIntegerWithSubstring(txtFlightNumber.Text) = False Then
            lblMessage.Text = "Please enter a positive integer for flight number!"
            Exit Sub
        End If

        If intFlightNumber < 0 Then
            lblMessage.Text = "Please enter a positive integer for flight number!"
            Exit Sub
        End If

        'validate that flight number is unique ***THIS NEEDS TO BE MADE OO COMPLIANT
        FObject.RunSPwithOneParam("usp_FlightClone_Get_One", "@FlightNumber", txtFlightNumber.Text)
        If FObject.MyDataSet.Tables("tblFlightsClone").Rows.Count <> 0 Then
            lblMessage.Text = "Please enter a unique flight number!"
            Exit Sub
        End If

        If ddlArrivalCity.SelectedIndex = ddlDepartureCity.SelectedIndex Then
            lblMessage.Text = "Please choose different cities for departure and arrival!"
            Exit Sub
        End If

        'If VObject.CheckIntegerWithSubstring(txtFlightNumber.Text) = False Then
        '    lblMessage.Text = "Please enter an integer for base fare!"
        '    Exit Sub
        'End If

        'validate that a day of the week is checked 
        If intCount = 0 Then
            lblMessage.Text = "Plase select at least one day of the week!"
            Exit Sub
        End If


        'validate that base fare is integer
        If VObject.CheckIntegerWithSubstring(txtBaseFare.Text) = False Then
            lblMessage.Text = "Please enter an integer for base fare!"
            Exit Sub
        End If

        'create array for parameter names
        Dim aryParamNames As New ArrayList

        'fill array for param names
        aryParamNames.Add("@FlightNumber")
        aryParamNames.Add("@DepartureCity")
        aryParamNames.Add("@EndCity")
        aryParamNames.Add("@DepartureTime")
        aryParamNames.Add("@ArrivalTime")
        aryParamNames.Add("@BaseFare")
        aryParamNames.Add("@Active") 'will default to yes
        aryParamNames.Add("@Monday")
        aryParamNames.Add("@Tuesday")
        aryParamNames.Add("@Wednesday")
        aryParamNames.Add("@Thursday")
        aryParamNames.Add("@Friday")
        aryParamNames.Add("@Saturday")
        aryParamNames.Add("@Sunday")

        'create array for parameter values
        Dim aryParamValues As New ArrayList

        'fill missing variables
        strDepartureTime = ddlDepartureTimeHour.SelectedItem.Text & ddlDepartureTimeMinutes.SelectedItem.Text
        strArrivalTime = lblArrivalTime.Text

        'fill that array
        aryParamValues.Add(txtFlightNumber.Text)
        aryParamValues.Add(ddlDepartureCity.SelectedValue.ToString)
        aryParamValues.Add(ddlArrivalCity.SelectedValue.ToString)
        aryParamValues.Add(strDepartureTime)
        aryParamValues.Add(strArrivalTime)
        aryParamValues.Add(txtBaseFare.Text)
        aryParamValues.Add("Y")
        aryParamValues.Add(Days(0))
        aryParamValues.Add(Days(1))
        aryParamValues.Add(Days(2))
        aryParamValues.Add(Days(3))
        aryParamValues.Add(Days(4))
        aryParamValues.Add(Days(5))
        aryParamValues.Add(Days(6))

        'run stored procedure to add flight
        FObject.AddFlight(aryParamNames, aryParamValues)

        'show success message to user
        lblMessage.Text = "You have successfully added flight #" & txtFlightNumber.Text

        ClearTextboxes()

        LoadDDLs()

    End Sub

    Private Sub ClearTextboxes()
        txtBaseFare.Text = ""
        txtFlightNumber.Text = ""
        cblDaysToFly.ClearSelection()
    End Sub


    Protected Sub ddlDepartureTimeMinutes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepartureTimeMinutes.SelectedIndexChanged, ddlDepartureTimeHour.SelectedIndexChanged
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
        aryParamValuesMileage.Add(ddlDepartureCity.SelectedValue)
        aryParamValuesMileage.Add(ddlArrivalCity.SelectedValue)

        'find duration of flight
        MObject.FindDuration(aryParamNamesMileage, aryParamValuesMileage)
        intDuration = CInt(MObject.MyDataSet.Tables("tblMileageClone").Rows(0).Item(0))

        'calculate the arrival time that needs to appear
        strArrivalTime = CObject.CalculateArrivalTime(intDepartureTime, intDuration)

        'put it in label
        lblArrivalTime.Text = strArrivalTime

    End Sub


    Protected Sub ddlDepartureCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepartureCity.SelectedIndexChanged, ddlArrivalCity.SelectedIndexChanged
        'check if selected indexes match
        If ddlArrivalCity.SelectedIndex = ddlDepartureCity.SelectedIndex Then
            'disable everything else, and give message that they can't select same city
            txtFlightNumber.ReadOnly = True
            ddlDepartureTimeHour.Enabled = False
            ddlDepartureTimeMinutes.Enabled = False
            txtBaseFare.ReadOnly = True
            cblDaysToFly.Enabled = False
            lblMessage.Text = "Please select different departure and arrival cities!"
        Else
            'they don't equal eachother, and everything should be active
            txtFlightNumber.ReadOnly = False
            ddlDepartureTimeHour.Enabled = True
            ddlDepartureTimeMinutes.Enabled = True
            txtBaseFare.ReadOnly = False
            cblDaysToFly.Enabled = True
            lblMessage.Text = ""
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
            aryParamValuesMileage.Add(ddlDepartureCity.SelectedValue)
            aryParamValuesMileage.Add(ddlArrivalCity.SelectedValue)

            'find duration of flight
            MObject.FindDuration(aryParamNamesMileage, aryParamValuesMileage)
            intDuration = CInt(MObject.MyDataSet.Tables("tblMileageClone").Rows(0).Item(0))

            'calculate the arrival time that needs to appear
            strArrivalTime = CObject.CalculateArrivalTime(intDepartureTime, intDuration)

            'put it in label
            lblArrivalTime.Text = strArrivalTime
        End If
    End Sub
End Class
