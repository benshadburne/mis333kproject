Option Strict On
Partial Class Emp_AddNewEmployee
    Inherits System.Web.UI.Page

    'create instance of employee database class
    Dim EObject As New DBEmployee

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        'create array list for parameter names
        Dim aryParamNames As New ArrayList

        'add parameter names to array list
        aryParamNames.Add("@LastName")
        aryParamNames.Add("@FirstName")
        aryParamNames.Add("@MI")
        aryParamNames.Add("@PSW")
        aryParamNames.Add("@SSN")
        aryParamNames.Add("@EmpType")
        aryParamNames.Add("@Address")
        aryParamNames.Add("@Zip")
        aryParamNames.Add("@Phone")

        'create array list for parameter values
        Dim aryParamValues As New ArrayList

        'add parameter values to array list
        aryParamValues.Add(txtLastName.Text)
        aryParamValues.Add(txtFirstName.Text)
        aryParamValues.Add(txtMI.Text)
        aryParamValues.Add(txtPassword.Text)
        aryParamValues.Add(txtSSN.Text)
        aryParamValues.Add(txtEmpType.Text)
        aryParamValues.Add(txtAddress.Text)
        aryParamValues.Add(txtZip.Text)
        aryParamValues.Add(txtPhoneNumber.Text)

        EObject.AddEmployee("tblEmployeesClone", aryParamNames, aryParamValues)

        Label11.Text = "You have added the record!"

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ''create variables for biggest empID and new empID
        'Dim intMaxEmpID As Integer
        'Dim intNewEmpID As Integer

        ''find biggest current empid
        'intMaxEmpID = EObject.FindMaxEmpID()

        ''set variable to one more than current biggest empID
        'intNewEmpID = intMaxEmpID + 1

        ''output new empID
        'txtEmpID.Text = intNewEmpID.ToString
    End Sub
End Class
