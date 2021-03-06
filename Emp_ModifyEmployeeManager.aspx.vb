﻿Option Strict On
Partial Class Emp_ModifyEmployeeManager
    Inherits System.Web.UI.Page

    'create instance of employee database class
    Dim EObject As New DBEmployee

    'create instance of validation class
    Dim VObject As New ClassValidate

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'create variable for EmpID
        Dim strEmpID As String

        'check for session variables
        If Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        ElseIf Session("UserType").ToString = "Manager" Then
            'they're logged in as a manager, and have selected an employee to modify
            If Session("RecordID") Is Nothing Then ' they haven't passed in a record ID from select page
                Response.Redirect("emp_SelectEmployeeToModify.aspx")
            Else
                strEmpID = Session("RecordID").ToString
            End If
        Else
            'they are a customer trying to access this page
            Response.Redirect("HomePage.aspx")
        End If



        'set varaible to session variable
        'strEmpID = (Session("RecordID")).ToString

        'if first time loading, check to see if we have a record ID
        If IsPostBack = False Then

            'get record from last page or current empID into dataset
            EObject.FindEmpID(strEmpID)
            FillTextboxes()



        End If

    End Sub

    Private Sub FillTextboxes()
        'purpose: fill textboxes with information of selected user index, in this case always 0
        'arguments: none
        'returns: nothing

        'declare variable to find out if employee is active or not
        Dim strYorN As String

        If Session("UserType").ToString = "Manager" Then
            EObject.FindEmpID(Session("RecordID").ToString)
        Else
            EObject.FindEmpID(Session("UserID").ToString)
        End If



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

        strYorN = EObject.dsEmployees.Tables("tblEmployeesClone").Rows(intIndex).Item("Active").ToString

        If strYorN.ToUpper = "Y" Then
            ddlActive.SelectedIndex = 0
        Else
            ddlActive.SelectedIndex = 1
        End If
    End Sub

    Protected Sub btnModify_Click(sender As Object, e As EventArgs) Handles btnModify.Click
        'hide modify button
        btnModify.Visible = False

        'show accept and cancel buttons
        btnAccept.Visible = True
        btnCancel.Visible = True

        'make textboxes editable
        txtLastName.ReadOnly = False
        txtFirstName.ReadOnly = False
        txtMI.ReadOnly = False
        txtPassword.ReadOnly = False
        txtSSN.ReadOnly = False
        txtAddress.ReadOnly = False
        txtEmpType.ReadOnly = False
        txtZip.ReadOnly = False
        txtPhoneNumber.ReadOnly = False
        ddlActive.Enabled = True
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'get record id that they wanted, and refill the info from the dataset
        EObject.FindEmpID(Session("UserID").ToString)
        FillTextboxes()

        'hide cancel and accept buttons
        btnAccept.Visible = False
        btnCancel.Visible = False

        'show modify button
        btnModify.Visible = True

        'make textboxes uneditable
        txtLastName.ReadOnly = True
        txtFirstName.ReadOnly = True
        txtMI.ReadOnly = True
        txtPassword.ReadOnly = True
        txtSSN.ReadOnly = True
        txtAddress.ReadOnly = True
        txtEmpType.ReadOnly = True
        txtZip.ReadOnly = True
        txtPhoneNumber.ReadOnly = True
        ddlActive.Enabled = False
    End Sub

    Protected Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        Dim strEmpID As String
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
        If VObject.CheckLength(txtSSN.Text, 9) = False Then
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
        aryParamNames.Add("@EmpID")
        aryParamNames.Add("@active")

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
        If Session("UserType").ToString = "Manager" Then
            aryParamValues.Add(Session("RecordID").ToString)
        Else
            aryParamValues.Add(Session("UserID").ToString)
        End If
        If ddlActive.SelectedIndex = 0 Then
            aryParamValues.Add("Y")
        Else
            aryParamValues.Add("N")
        End If


        'call stored procedure to modify current employee
        EObject.ModifyEmployee(aryParamNames, aryParamValues)

        'show success message
        lblMessage.Text = "You have successfully updated the record for the shown employee. You will now be redirected to the select employee to modify page."

        'hide cancel and accept buttons
        btnAccept.Visible = False
        btnCancel.Visible = False

        strEmpID = Session("RecordID").ToString
        EObject.FindEmpID(strEmpID)
        FillTextboxes()

        'redirect to Select employee to modify 
        Response.AddHeader("Refresh", "5; URL=Emp_SelectEmployeeToModify.aspx")
    End Sub
End Class

