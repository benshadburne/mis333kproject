﻿Option Strict On
Partial Class Emp_ReactivateJourney
    Inherits System.Web.UI.Page

    Dim JObject As New DBjourneyclone

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        JObject.GetAllInactiveJourneys()
        gvJourneys.DataSource = JObject.MyDataSet.Tables("tblJourneyClone")
        gvJourneys.DataBind()
    End Sub

    Protected Sub gvJourneys_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvJourneys.SelectedIndexChanged
        lblMessage.Text = "You have selected the following journey number to modify:"
        lblID.Text = gvJourneys.SelectedRow.Cells(1).Text
    End Sub


    Protected Sub btnReactivate_Click(sender As Object, e As EventArgs) Handles btnReactivate.Click
        btnReactivate.Visible = False
        btnAbort.Visible = True
        btnAccept.Visible = True
        gvJourneys.Enabled = False
    End Sub


    Protected Sub btnAbort_Click(sender As Object, e As EventArgs) Handles btnAbort.Click
        btnReactivate.Visible = True
        btnAbort.Visible = False
        btnAccept.Visible = False
        gvJourneys.Enabled = True
    End Sub

    Protected Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        JObject.ActivateJourneyByJourneyID(lblID.Text)

        lblMessage.Text = "You have successfully re-activated the following journey number:"

        btnReactivate.Visible = True
        btnAbort.Visible = False
        btnAccept.Visible = False
        gvJourneys.Enabled = False
    End Sub
End Class
