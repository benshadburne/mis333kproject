
Partial Class Res_CreateCustProfile
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
        'Clear the message
        lblErrorMessage.Text = ""
        lblSuccessMessage.Text = ""

        'Check for First Name, Last Name, and Phone duplicates on one add
        If CustDB.GetFirstName(txtFName.Text) = True And CustDB.GetLastName(txtLName.Text) = True And CustDB.GetPhoneNumber(txtPhone.Text) = True Then
            lblErrorMessage.Text = "Duplicate First Name, Last Name, and Phone Number. Please try again."
            Exit Sub
        End If


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

        'Create the values for the session variables
        Session("Address") = txtAddress.Text
        Session("City") = txtCity.Text
        Session("State") = txtState.Text
        Session("Zip") = txtZip.Text
        Session("LastName") = txtLName.Text
        Session("Phone") = txtPhone.Text

        ClearAll()

        CustDB.GetNewestCustomer()
        Session("NewCustomer") = CustDB.MyDataset.Tables("tblCustomersClone").Rows(0).Item("AdvantageNumber").ToString

        Response.Redirect("Res_SelectCustomer.aspx")


        'go back to reservation select customer page


        

    End Sub

    Protected Sub btnAddFamilyMember_Click(sender As Object, e As EventArgs) Handles btnAddFamilyMember.Click
        'Fill the text boxes with the session variable values
        txtAddress.Text = CStr(Session("Address"))
        txtCity.Text = CStr(Session("City"))
        txtState.Text = CStr(Session("State"))
        txtZip.Text = CStr(Session("Zip"))
        txtLName.Text = CStr(Session("LastName"))
        txtPhone.Text = CStr(Session("Phone"))

        btnAddFamilyMember.Visible = False

        Session.Remove("Address")
        Session.Remove("City")
        Session.Remove("State")
        Session.Remove("Zip")
        Session.Remove("LastName")
        Session.Remove("Phone")

    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'Clear the message
        ClearAll()
    End Sub

    Public Sub ClearAll()
        'Clear the message
        lblErrorMessage.Text = ""
        lblSuccessMessage.Text = ""

        txtFName.Text = ""
        txtMI.Text = ""
        txtLName.Text = ""
        txtPassword.Text = ""
        txtAddress.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtZip.Text = ""
        txtEmail.Text = ""
        txtPhone.Text = ""

        btnAddFamilyMember.Visible = True
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        ElseIf Session("UserType").ToString = "Crew" Then
            Response.Redirect("Emp_EmployeeDashboard.aspx")
        End If

        If Session("Zip") Is Nothing Then
            'don't do anything
        Else
            'let them use session variables to fill some textboxes
            btnAddFamilyMember.Visible = True
        End If
    End Sub
End Class
