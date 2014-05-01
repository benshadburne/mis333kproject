Option Strict On
Partial Class Emp_ReactivateJourney
    Inherits System.Web.UI.Page

    Dim JObject As New DBjourneyclone
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        JObject.GetAllInactiveJourneys()
        gvJourneys.DataSource = JObject.MyDataSet.Tables("tblJourneyClone")
        gvJourneys.DataBind()
    End Sub

    Protected Sub gvJourneys_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvJourneys.SelectedIndexChanged
        lblMessage.Text = "You have selected the following record to modify:"
        lblID.Text = gvJourneys.SelectedRow.Cells(1).Text
    End Sub
End Class
