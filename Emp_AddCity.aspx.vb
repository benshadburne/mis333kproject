
Partial Class Emp_AddCity
    Inherits System.Web.UI.Page
    Dim DBAirport As New DBairportclone
    Dim Validations As New ClassValidate
    Dim DBZip As New DBZip

    'define a counter variable
    Dim i As Integer

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'NEED TO CHECK IF GATE AGENTS CAN ADD CITIES
        If Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        ElseIf Session("UserType").ToString = "Crew" Or Session("UserType").ToString = "Agent" Then
            Response.Redirect("Emp_EmployeeDashboard.aspx")
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'Run a validation to make sure airport code is the right length
        If Validations.CheckAirportCode(txtAirport.Text) = False Then
            lblMessage.Text = "The AirportCode must be three letters."
            Exit Sub
        End If

        'MAKE SURE AIRPORT CODE IS UNIQUE

        'get all airports with a stored procedure
        DBAirport.GetALLairportcloneUsingSP()

        i = DBAirport.MyView.Count - 1

        'SHOULD THIS LOOP BE IN A CLASS? A DB CLASS??
        'loop through airports to make sure the airport we are using is unique. 
        'loop through i record
        For j = 0 To i
            'if the airport code for this table record is the same as the one entered...
            If DBAirport.MyDataSet.Tables("tblAirportClone").Rows(j).Item("AirportCode") = txtAirport.Text.ToUpper Then
                'show message and exit sub
                lblMessage.Text = "That airport code is already in use. Please use another three letter combination."
                Exit Sub
            End If
            'the record didn't match the one in that row of the database, go to next record
        Next

        'validate city name
        If Validations.CheckCity(txtCity.Text) = False Then
            lblMessage.Text = "City must begin with a capital letter and only contain letters and spaces"
            Exit Sub
        End If

        'check city unique 
        If DBAirport.CheckUniqueCity(txtCity.Text) = False Then
            lblMessage.Text = "That city already has an airport."
            Exit Sub
        End If

        'check state
        If Len(txtState.Text) <> 2 Then
            lblMessage.Text = "Please enter a two letter US State abbreviation."
            Exit Sub
        Else
            If Validations.CheckLetterWithSubstring(txtState.Text) = False Then
                lblMessage.Text = "Please enter letters for the state abbreviation."
                Exit Sub
            End If
        End If

        'add record
        DBAirport.AddAirport("usp_AirportClone_Add_New", txtAirport.Text.ToUpper, txtCity.Text, txtState.Text)

        'create session variable to track new airport through while we add flight time and mileage to other airports
        Session.Add("NewAirport", txtAirport.Text.ToUpper)

        'change which panel is visible
        pnlAddAirport.Visible = False
        pnlAddInfo.Visible = True

        'Add a session variable for the first airport
        Session.Add("Airport", DBAirport.MyDataSet.Tables("tblAirportClone").Rows(0).Item("AirportCode"))

        'FIND OUT IF THERE IS A WAY TO FORCE THE USER TO GO THROUGH ALL OF THE AIRPORTS WITHOUT CLOSING THE PAGE OR GOING SOMEWHERE ELSE
        'tell the user to add the info for the first airport. 
        lblAirport.Text = "Add the mileage and flight time from " & Session("NewAirport") & " to " & Session("Airport")

    End Sub

    
    Private Sub UpdateSessionVariable(strAirportCode As String)

        'set i equal to the row count minus two (When this sub is called) the new airport has been added as the last record
        'we don't want to loop through the record we just added
        i = DBAirport.MyDataSet.Tables("tblAirportClone").Rows.Count - 2

        For j = 0 To i
            'find the correct row in the dataset 
            If DBAirport.MyDataSet.Tables("tblAirportClone").Rows(j).Item("AirportCode") = strAirportCode Then

                'if this is the last airport we need to add information for,
                If j = i Then
                    'remove both session variables
                    Session.Remove("Airport")
                    Session.Remove("NewAirport")
                    'direct the manager to another page
                    'ADD CODE HERE
                    Response.Redirect("Emp_AddCity.aspx")
                    Exit For
                Else
                    'we need to add information for more airports. Update the session variable to equal the next airport code
                    Session("Airport") = DBAirport.MyDataSet.Tables("tblAirportClone").Rows(j + 1).Item("AirportCode")
                    'update the label
                    lblAirport.Text = "Add the information about mileage and flight time from " & Session("NewAirport") & " to " & Session("Airport")
                    'exit the loop
                    Exit For
                End If
            End If

        Next

    End Sub

    Protected Sub btnAddInfo_Click(sender As Object, e As EventArgs) Handles btnAddInfo.Click
        'clear form
        lblAirportMessage.Text = ""
        lblMessage.Text = ""

        'define a variable into which to add the flight time
        Dim strFlightTime As String

        'run a validation to make sure that milage is an integer
        If Validations.CheckIntegerWithSubstring(txtMileage.Text) = False Then
            lblAirportMessage.Text = "The mileage must be an integer"
            Exit Sub
        End If

        If ddlHours.SelectedIndex = 0 And ddlMinutes.SelectedIndex = 0 Then
            lblAirportMessage.Text = "Planes do not teleport people instantaneously. Add the flight time."
            Exit Sub
        End If

        'NEED TO ADD A DROP DOWN AND USE IT TO FILL FLIGHT TIME
        strFlightTime = ddlHours.SelectedValue & ddlMinutes.SelectedValue

        'add mileage and flight time going to and from the new airport to the old one
        DBAirport.AddMileageAndFlightTime("usp_MilageClone_Add_New", Session("NewAirport"), Session("Airport"), CInt(txtMileage.Text), CInt(strFlightTime))
        DBAirport.AddMileageAndFlightTime("usp_MilageClone_Add_New", Session("Airport"), Session("NewAirport"), CInt(txtMileage.Text), CInt(strFlightTime))

        'Get the records for the all airports (this is necessary to run the loop for the UpdateSessionVariable sub) 
        DBAirport.GetALLairportcloneUsingSP()

        lblMessage.Text = "You successfully added flight time Mileages to " & Session("Airport").ToString

        'add the next airport the session variable
        UpdateSessionVariable(Session("Airport"))

        'return DDL to normal
        ddlHours.SelectedIndex = 0
        ddlMinutes.SelectedIndex = 0

        txtMileage.Text = ""

    End Sub


End Class
