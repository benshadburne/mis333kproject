Option Strict On
'author: Jace Barton
'purpose: select an employee to modify, using a grid view
Partial Class Emp_SelectEmployeeToModify
    Inherits System.Web.UI.Page

    'create instance of employee DB
    Dim EObject As New DBEmployee

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'check for session variables
        If Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        ElseIf Session("UserType").ToString = "Manager" Then
            'everything's good, they're logged in as a manager
        Else
            Response.Redirect("HomePage.aspx")
        End If

        'get all employees into a dataset
        EObject.GetALLEmployeeUsingSP()

        'set source of grid view to dataview and bind
        gvEmployees.DataSource = EObject.MyView
        gvEmployees.DataBind()

    End Sub

    Protected Sub gvEmployees_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvEmployees.SelectedIndexChanged
        'set recordID for next page
        Session("RecordID") = gvEmployees.SelectedRow.Cells(1).Text

        'fire up next form
        Response.Redirect("Emp_ModifyEmployeeManager.aspx")
    End Sub
End Class
