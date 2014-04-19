'Author: Dennis Phelan
'Purpose: Make sure Save and Clear buttons know what to do to Add a Customer
'Date Created: April 11, 2014
'Date Last Modified: April 11, 2014

Partial Class Cust_AddNewCustomer
    Inherits System.Web.UI.Page

    'Declare instances of classes
    Dim CustDB As New DBCustomersClone
    Dim valid As New ClassValidate

    'Public sub using an SP for adding a new customer
    Private Sub AddNewCustomerUsingSP()

        'Declare the names of the arrays and the values of the arrays
        Dim aryNames As New ArrayList
        Dim aryValues As New ArrayList

        aryNames.Add("@Username")
        aryNames.Add("@Password")
        aryNames.Add("@LastName")
        aryNames.Add("@FirstName")
        aryNames.Add("@MI")
        aryNames.Add("@Address")
        aryNames.Add("@City")
        aryNames.Add("@State")
        aryNames.Add("@Zip")
        aryNames.Add("@Phone")
        aryNames.Add("@Email")

        aryValues.Add(txtUsername.Text)
        aryValues.Add(txtPassword.Text)
        aryValues.Add(txtLName.Text)
        aryValues.Add(txtFName.Text)
        aryValues.Add(txtMI.Text)
        aryValues.Add(txtAddress.Text)
        aryValues.Add(txtCity.Text)
        aryValues.Add(txtState.Text)
        aryValues.Add(txtZip.Text)
        aryValues.Add(txtPhone.Text)
        aryValues.Add(txtEmail.Text)

        'Call the SP to insert the record
        CustDB.UseSPforInsertOrUpdateQuery("usp_Customers_Add_New", aryNames, aryValues)
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

       'Try adding a new customer using an SP
        AddNewCustomerUsingSP()

        'Outputs
        lblSuccessMessage.Text = "Profile successfully added."

    End Sub
End Class
