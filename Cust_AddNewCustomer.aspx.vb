'Author: Dennis Phelan
'Purpose: Make sure Save and Clear buttons know what to do to Add a Customer
'Date Created: April 11, 2014
'Date Last Modified: April 11, 2014

Partial Class Cust_AddNewCustomer
    Inherits System.Web.UI.Page

    'Declare instances of classes
    Dim CustDB As New DBCustomersClone
    Dim valid As New ClassValidate

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

    End Sub
End Class
