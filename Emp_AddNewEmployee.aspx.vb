Option Strict On
Partial Class Emp_AddNewEmployee
    Inherits System.Web.UI.Page

    'create instance of employee database class
    Dim EObject As New DBEmployee

    'create instance of validation class
    Dim VObject As New ClassValidate

    'create instance of zip class
    Dim ZObject As New DBZip

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        'check phone number
        If VObject.CheckIntegerWithSubstring(txtPhoneNumber.Text) = False Then
            lblMessage.Text = "Please enter a valid 10 digit phone number with no formatting!"
            Exit Sub
        End If

        'check if phone number is 10 digits long
        If VObject.CheckLength(txtPhoneNumber.Text, 10) = False Then
            lblMessage.Text = "Please enter a valid 10 digit phone number with no formatting!"
            Exit Sub
        End If

        'check social security number
        If VObject.CheckIntegerWithSubstring(txtSSN.Text) = False Then
            lblMessage.Text = "Please enter a valid 9 digit social security number with no formatting!"
            Exit Sub
        End If

        'check if SSN is 9 digits long
        If VObject.CheckLength(txtPhoneNumber.Text, 9) = False Then
            lblMessage.Text = "Please enter a valid 9 digit social security number with no formatting!"
            Exit Sub
        End If

        'check if zip is in the zip table. this calls Validation class. Validation class calls Zip database class. Might be able to consolidate.
        If VObject.CheckZip(txtZip.Text) = False Then
            'Zip not found
            lblMessage.Text = "Please enter a valid US Zip code!"
            Exit Sub
        End If



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
