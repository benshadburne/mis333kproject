
Partial Class Emp_AddCity
    Inherits System.Web.UI.Page
    Dim DBAirport As New DBairportclone
    Dim Validations As New ClassValidate

    'define a counter variable
    Dim i As Integer

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

        'THE CITY NAME CURRENTLY DOES NOT ALLOW CITIES WITH A SPACE IN THEM TO PASS VALIDATION
        If Validations.CheckCity(txtCity.Text) = False Then
            lblMessage.Text = "City must begin with a capital letter and only contain letters and spaces"
            Exit Sub
        End If

        'add record
        DBAirport.AddAirport("usp_AirportClone_Add_New", txtAirport.Text.ToUpper, txtCity.Text)

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
        'define a variable into which to add the flight time
        Dim strFlightTime As String

        'run a validation to make sure that milage is an integer
        If Validations.CheckIntegerWithSubstring(txtMileage.Text) = False Then
            lblAirportMessage.Text = "The mileage must be an integer"
            Exit Sub
        End If

        'NEED TO ADD A DROP DOWN AND USE IT TO FILL FLIGHT TIME
        strFlightTime = ddlHours.SelectedValue & ddlMinutes.SelectedValue

        'add mileage and flight time going to and from the new airport to the old one
        DBAirport.AddMileageAndFlightTime("usp_MilageClone_Add_New", Session("NewAirport"), Session("Airport"), CInt(txtMileage.Text), CInt(strFlightTime))
        DBAirport.AddMileageAndFlightTime("usp_MilageClone_Add_New", Session("Airport"), Session("NewAirport"), CInt(txtMileage.Text), CInt(strFlightTime))

        'Get the records for the all airports (this is necessary to run the loop for the UpdateSessionVariable sub) 
        DBAirport.GetALLairportcloneUsingSP()

        'add the next airport the session variable
        UpdateSessionVariable(Session("Airport"))

        'clear form
        lblAirportMessage.Text = ""
        txtMileage.Text = ""

        'return DDL to normal
        ddlHours.SelectedIndex = 0
        ddlMinutes.SelectedIndex = 0

    End Sub
End Class
