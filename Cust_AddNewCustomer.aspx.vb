Option Strict On
'Author: Dennis Phelan
'Purpose: Make sure Save and Clear buttons know what to do to Add a Customer
'Date Created: April 11, 2014
'Date Last Modified: April 11, 2014

Partial Class Cust_AddNewCustomer
    Inherits System.Web.UI.Page

    'Declare instances of classes
    Dim CustDB As New DBCustomersClone
    Dim valid As New ClassValidate
    Dim ZipDB As New DBZip

    'Public sub using an SP for adding a new customer
    Private Sub AddNewCustomerUsingSP()

        'Declare the names of the arrays and the values of the arrays
        Dim aryNames As New ArrayList
        Dim aryValues As New ArrayList

        aryNames.Add("@Password")
        aryNames.Add("@LastName")
        aryNames.Add("@FirstName")
        aryNames.Add("@MI")
        aryNames.Add("@Address")
        aryNames.Add("@Zip")
        aryNames.Add("@Phone")
        aryNames.Add("@Email")

        aryValues.Add(txtPassword.Text)
        aryValues.Add(txtLName.Text)
        aryValues.Add(txtFName.Text)
        aryValues.Add(txtMI.Text)
        aryValues.Add(txtAddress.Text)
        aryValues.Add(txtZip.Text)
        aryValues.Add(txtPhone.Text)
        aryValues.Add(txtEmail.Text)

        'Call the SP to insert the record
        CustDB.UseSPforInsertOrUpdateQuery("usp_CustomersClone_Add_New", aryNames, aryValues)
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        'Here's where I will put all of my validations
        'Validate Phone
        If valid.CheckPhone(txtPhone.Text) = False Then
            lblErrorMessage.Text = "Phone number needs to be 10 digits."
            Exit Sub
        End If

        'Validate the Zip by making sure it is zip code that exists
        If valid.CheckZip(txtZip.Text) = False Then
            lblErrorMessage.Text = "Zip code does not exist. Please try again."
            Exit Sub
        End If

        'Validate the state to where if it is a US Zip code, then it must match the two letter state associated with it
        If valid.CheckUSState(txtState.Text, txtZip.Text) = False Then
            lblErrorMessage.Text = "The US state does not match US two letter state that corresponds with the zip code you entered. Please try again."
            Exit Sub
        End If

        'Try adding a new customer using an SP
        AddNewCustomerUsingSP()

        'Outputs
        lblSuccessMessage.Text = "Profile successfully added."

    End Sub
End Class
