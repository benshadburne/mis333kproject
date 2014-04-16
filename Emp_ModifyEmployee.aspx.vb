'author: Jace Barton
'purpose: Modify the selected employee
Option Strict On
Partial Class Emp_ModifyEmployee
    Inherits System.Web.UI.Page

    'create instance of employee database class
    Dim EObject As New DBEmployee

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'create variable for EmpID
        Dim intEmpID As Integer

        'set varaible to session variable
        intEmpID = CInt(Session("RecordID"))

        'if first time loading, check to see if we have a record ID
        If IsPostBack = False Then
            If Session("RecordID") Is Nothing Then 'we don't have a record ID
                Response.Redirect("emp_SelectEmployeeToModify.aspx")
            Else
                'get record from last page into dataset
                EObject.FindEmpID(Session("RecordID").ToString)
                FillTextboxes()
            End If

            
        End If
    End Sub

    Private Sub FillTextboxes()
        'purpose: fill textboxes with information of selected user index, in this case always 0
        'arguments: none
        'returns: nothing

        'we're only going to want the first row because there will only be one customer in dataset based on selected customer
        Dim intIndex As Integer = 0

        'fill textboxes with the information of the customer currently selected
        txtLastName.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("LastName").ToString
        txtFirstName.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("FirstName").ToString
        txtMI.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("MI").ToString
        txtPassword.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("PSW").ToString
        txtSSN.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("SSN").ToString
        txtAddress.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("Address").ToString
        txtEmpType.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("EmpType").ToString
        txtZip.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("Zip").ToString
        txtPhoneNumber.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("Phone").ToString

    End Sub
End Class
