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

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'if first time loading page, load drop down lists
        If IsPostBack = False Then
            'get all airports
            AObject.GetALLairportcloneUsingSP()
            'set datasource to all airports
            ddlDepartureCity.DataSource = AObject.MyDataSet.Tables("tblAirportClone")
            'set datafield showing to city names
            ddlDepartureCity.DataTextField = "CityName"
            'set datafield value associated with what's showing to 3 letter airport code
            ddlDepartureCity.DataValueField = "AirportCode"
            'bind ddl
            ddlDepartureCity.DataBind()

            'do same as above, but for arrival city
            'set datasource to all airports
            ddlArrivalCity.DataSource = AObject.MyDataSet.Tables("tblAirportClone")
            'set datafield showing to city names
            ddlArrivalCity.DataTextField = "CityName"
            'set datafield value associated with what's showing to 3 letter airport code
            ddlArrivalCity.DataValueField = "AirportCode"
            'bind ddl
            ddlArrivalCity.DataBind()

        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'build array for days of week selected
        Dim Days(6) As String

        'check selected index of check list
        For i = 0 To cblDaysToFly.Items.Count - 1
            'check if item is selected. if yes, make Days(i) = "y", else, = "n"
            If cblDaysToFly.Items(i).Selected = True Then
                Days(i) = "Y"
            Else
                Days(i) = "N"
            End If

        Next
        'build departure time and arrival time
        Dim strDepartureTime As String
        Dim strArrivalTime As String

        'make it equal to hour and minute on ddls for departure time
        strDepartureTime = ddlDepartureTimeHour.SelectedItem.Text & ddlDepartureTimeMinutes.SelectedItem.Text
        strArrivalTime = ddlArrivalTimeHour.SelectedItem.Text & ddlArrivalTimeMinutes.SelectedItem.Text


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

    End Sub
End Class