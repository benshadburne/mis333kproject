
Partial Class Emp_AddCity
    Inherits System.Web.UI.Page
    Dim DBAirport As New DBairportclone

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'define a counter variable
        Dim i As Integer
        'define a variable to check if the airport code is unique

        'get all airports with a stored procedure
        DBAirport.GetALLairportcloneUsingSP()

        i = DBAirport.MyView.Count - 1

        'SHOULD THIS LOOP BE IN A CLASS? A DB CLASS??
        'loop through airports to make sure the airport we are using is unique. 
        'loop through i record
        For j = 0 To i
            'if the airport code for this table record is the same as the one entered...
            If DBAirport.MyDataSet.Tables("tblAirportClone").Rows(j).Item("AirportCode") = txtAirport.Text Then
                'show message and exit sub
                lblMessage.Text = "That airport code is already in use. Please use another three letter combination."
                Exit Sub
            End If
            'the record didn't match the one in that row of the database, go to next record
        Next

        'add record
        DBAirport.AddAirport("usp_Airport_Add_New", txtAirport.Text, txtCity.Text)

        'change which panel is visible
        pnlAddAirport.Visible = False
        pnlAddInfo.Visible = True

        'use a loop to add session for each current airport to the page.
        For j = 0 To i
            Session.Add("Airport" + j.ToString, DBAirport.MyDataSet.Tables("tblAirportClone").Rows(j).Item("AirportCode"))
        Next

        'FIND OUT IF THERE IS A WAY TO FORCE THE USER TO GO THROUGH ALL OF THE AIRPORTS WITHOUT CLOSING THE PAGE OR GOING SOMEWHERE ELSE
        'allow the user to add the info for the first airport. 
        'remove the session variable for the first airport. 
        'add flight information 
        'reload the page and go to the next session variable

    End Sub
End Class
