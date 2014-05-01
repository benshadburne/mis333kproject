Option Strict On
Partial Class Emp_ReactivateJourney
    Inherits System.Web.UI.Page

    Dim JObject As New DBjourneyclone
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        JObject.GetAllInactiveJourneys()
        gvJourneys.DataSource = JObject.MyDataSet.Tables("tblJourneyClone")
        gvJourneys.DataBind()
    End Sub
End Class
