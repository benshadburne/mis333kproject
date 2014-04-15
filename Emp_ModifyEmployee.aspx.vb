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

        If IsPostBack = False Then
            'get record from last page into dataset
            EObject.FindEmpID(Session("RecordID").ToString)
        End If
    End Sub

    Private Sub FillTextboxes()
        'purpose: fill textboxes with information of selected user index, in this case always 0
        'arguments: none
        'returns: nothing

        'we're only going to want the first row because there will only be one customer in dataset based on selected customer
        Dim intIndex As Integer = 0

        'fill textboxes with the information of the customer currently selected
        txtLName.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("LastName").ToString
        txtFName.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("FirstName").ToString
        txtMI.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("MI").ToString
        txtPassword.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("Password").ToString
        txtUsername.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("Username").ToString
        txtAddress.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("Address").ToString
        txtCity.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("City").ToString
        txtState.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("State").ToString
        txtZip.Text = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("ZipCode").ToString
        'format the phone number
        txtPhone.Text = FObject.PhoneNumber(EObject.dsEmployees.Tables("tblCustomersClone").Rows(intIndex).Item("Phone").ToString)
        txtEmail.Text = EObject.dsEmployees.Tables("tblCustomersClone").Rows(intIndex).Item("EmailAddr").ToString
    End Sub
End Class
