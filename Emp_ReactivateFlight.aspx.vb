Option Strict On
Partial Class Emp_ReactivateFlight
    Inherits System.Web.UI.Page

    Dim FObject As New DBFlightsClone

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        FObject.GetAllInactive()
        gvFlights.DataSource = FObject.MyDataSet.Tables("tblFlightClone")
        gvFlights.DataBind()
    End Sub

    Protected Sub gvJourneys_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvFlights.SelectedIndexChanged
        lblMessage.Text = "You have selected the following flight number to modify:"
        lblID.Text = gvFlights.SelectedRow.Cells(1).Text
    End Sub


    Protected Sub btnReactivate_Click(sender As Object, e As EventArgs) Handles btnReactivate.Click
        btnReactivate.Visible = False
        btnAbort.Visible = True
        btnAccept.Visible = True
        gvFlights.Enabled = False
    End Sub


    Protected Sub btnAbort_Click(sender As Object, e As EventArgs) Handles btnAbort.Click
        btnReactivate.Visible = True
        btnAbort.Visible = False
        btnAccept.Visible = False
        gvFlights.Enabled = True
    End Sub

    Protected Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        If lblID.Text = "" Then
            lblMessage.Text = "Please select a flight to activate!"
            btnReactivate.Visible = True
            btnAbort.Visible = False
            btnAccept.Visible = False
            gvFlights.Enabled = True
            Exit Sub
        End If
        FObject.MakeFlightActive(lblID.Text)

        lblMessage.Text = "You have successfully re-activated the following flight number:"

        btnReactivate.Visible = True
        btnAbort.Visible = False
        btnAccept.Visible = False
        gvFlights.Enabled = True
    End Sub

End Class
