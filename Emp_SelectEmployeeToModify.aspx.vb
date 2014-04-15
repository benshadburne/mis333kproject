Option Strict On
'author: Jace Barton
'purpose: select an employee to modify, using a grid view
Partial Class Emp_SelectEmployeeToModify
    Inherits System.Web.UI.Page

    'create instance of employee DB
    Dim EObject As New DBEmployee

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
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
        Response.Redirect("Emp_ModifyEmployee.aspx")
    End Sub
End Class
