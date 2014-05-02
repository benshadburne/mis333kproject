Option Strict On
Partial Class _Default
    Inherits System.Web.UI.Page

    'Declare instances of classes
    Dim CustDB As New DBCustomersClone
    Dim valid As New ClassValidate

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim intAdvantageNumber As Integer

        'check for session variables
        If Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        Else
            If Session("UserType").ToString = "Customer" Then
                intAdvantageNumber = CInt(Session("UserID"))
            ElseIf Session("UserType").ToString = "Manager" Or Session("UserType").ToString = "Agent" Then
                If Session("AdvantageNumber_Selected_By_Manager") Is Nothing Then
                    Response.Redirect("Emp_Select_Cust_To_Modify.aspx")
                Else
                    intAdvantageNumber = CInt(Session("AdvantageNumber_Selected_By_Manager"))
                End If
            Else
                    Response.Redirect("HomePage.aspx")
            End If
        End If

        If Session("UserType").ToString = "Customer" Then
            ddlActive.Visible = True
            lblActive.Visible = True
            txtMiles.ReadOnly = True
            lblMiles.Visible = True
        End If



        'Dim strAdvantageNumber As String

        'if logged in as employee then



        'elseif logged in as customer
        'strAdvantageNumber = Session("AdvantageNumberOfCustomer")
        'else
        'response.redirect(HomePage)

        If IsPostBack = False Then
            LoadTextboxes(intAdvantageNumber)
            MakeThingsInactive()
        End If


    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        'Dim strAdvantageNumber As Stringa
        'strAdvantageNumber = Session(
        'Clear the message
        lblErrorMessage.Text = ""
        lblSuccessMessage.Text = ""

        'Check for First Name, Last Name, and Phone duplicates on one add
        If txtFName.Text = Session("CheckFirstName").ToString And txtLName.Text = Session("CheckLastName").ToString And txtPhone.Text = Session("CheckPhone").ToString Then
            'it's fine, they're modifying their own record
        Else
            'if it doesn't match what we initially loaded into the textbox, but these three fields match something found in the database then
            CustDB.FindIfCustomerExists(txtFName.Text, txtLName.Text, txtPhone.Text)
            If CustDB.MyDataset.Tables("tblCustomersClone").Rows.Count <> 0 Then
                'someone else has this combination of fname, lname, phone
                lblErrorMessage.Text = "This customer already exists. Please change either First name, last name, or phone number!"
                Exit Sub
            End If
        End If

        If valid.CheckIntegerWithSubstring(txtMiles.Text) = False Then
            lblErrorMessage.Text = "Please enter a positive integer (or 0) for miles!"
            Exit Sub
        End If

        If CInt(txtMiles.Text) < 0 Then
            lblErrorMessage.Text = "Please enter a positive integer (or 0) for miles!"
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

        'Try modifying customer using an SP
        Dim strCustomerID As String

        If Session("UserType").ToString = "Customer" Then
            strCustomerID = Session("UserID").ToString
        Else
            strCustomerID = Session("AdvantageNumber_Selected_By_Manager").ToString
        End If

        If ddlActive.SelectedIndex = 1 Then ' customer needs to be made inactive, and records need to reflect that
            CustDB.InactivateTicketsOfACustomer(strCustomerID)
        End If

        'modify the record
        CustDB.ModifyEmployeeRecord(strCustomerID, txtPassword.Text, txtLName.Text, txtFName.Text, txtMI.Text, txtAddress.Text, txtZip.Text, txtPhone.Text, txtEmail.Text, txtMiles.Text, ddlActive.SelectedValue.ToString)

        'Outputs
        'customer has chosen to inactivate own profile
        If ddlActive.SelectedIndex = 1 And Session("UserType").ToString = "Customer" Then
            lblSuccessMessage.Text = "You have inactivated your profile. You will now be logged out. You must call a Penguin Air representative if you wish to reactivate your profile."
            Session.Clear()
            Response.AddHeader("Refresh", "10; URL=HomePage.aspx")
        End If

        'employee has inactivated customer
        If ddlActive.SelectedIndex = 1 And Session("UserType").ToString <> "Customer" Then
            lblSuccessMessage.Text = "You have inactivated the selected customer. You will now be redirected to the page to select a customer to modify."
            Response.AddHeader("Refresh", "10, URL=Emp_Select_Cust_To_Modify.aspx")
        End If

        'customer or employee has modified record but kept record active
        If ddlActive.SelectedIndex = 0 Then
            lblSuccessMessage.Text = "You have successfully modified the above user's record."
        End If
        MakeThingsInactive()
    End Sub

    Public Sub LoadTextboxes(intAdvantageNumber As Integer)

        Dim strYorN As String
        CustDB.FindCustomersByAdvantageNumber(intAdvantageNumber)

        If CustDB.MyDataset.Tables("tblCustomersClone").Rows.Count = 0 Then
            lblErrorMessage.Text = "We couldn't find a customer with this advantage number!"
            Exit Sub
        End If

        Dim intIndex As Integer = 0 'will always only be one record with advantage number
        txtAddress.Text = CustDB.MyDataset.Tables("tblCustomersClone").Rows(0).Item("Address").ToString
        txtEmail.Text = CustDB.MyDataset.Tables("tblCustomersClone").Rows(0).Item("Email").ToString
        txtFName.Text = CustDB.MyDataset.Tables("tblCustomersClone").Rows(0).Item("FirstName").ToString
        txtLName.Text = CustDB.MyDataset.Tables("tblCustomersClone").Rows(0).Item("LastName").ToString
        txtMI.Text = CustDB.MyDataset.Tables("tblCustomersClone").Rows(0).Item("MI").ToString
        txtPassword.Text = CustDB.MyDataset.Tables("tblCustomersClone").Rows(0).Item("Password").ToString
        txtPhone.Text = CustDB.MyDataset.Tables("tblCustomersClone").Rows(0).Item("Phone").ToString
        txtZip.Text = CustDB.MyDataset.Tables("tblCustomersClone").Rows(0).Item("Zip").ToString
        txtMiles.Text = CustDB.MyDataset.Tables("tblCustomersClone").Rows(0).Item("Miles").ToString
        strYorN = CustDB.MyDataset.Tables("tblCustomersClone").Rows(0).Item("Active").ToString
        If strYorN.ToUpper = "Y" Then
            ddlActive.SelectedIndex = 0
        Else
            ddlActive.SelectedIndex = 1
        End If

        Session("CheckFirstName") = txtFName.Text
        Session("CheckLastName") = txtLName.Text
        Session("CheckPhone") = txtPhone.Text

    End Sub

    Public Sub MakeThingsInactive()
        txtAddress.Enabled = False
        txtEmail.Enabled = False
        txtFName.Enabled = False
        txtLName.Enabled = False
        txtMI.Enabled = False
        txtMiles.Enabled = False
        txtPassword.Enabled = False
        txtPhone.Enabled = False
        txtZip.Enabled = False
        btnModify.Visible = True
        btnAbort.Visible = False
        btnSave.Visible = False
        ddlActive.Enabled = False
    End Sub

    Public Sub MakeThingsActive()
        txtAddress.Enabled = True
        txtEmail.Enabled = True
        txtFName.Enabled = True
        txtLName.Enabled = True
        txtMI.Enabled = True
        txtMiles.Enabled = True
        txtPassword.Enabled = True
        txtPhone.Enabled = True
        txtZip.Enabled = True
        btnModify.Visible = False
        btnAbort.Visible = True
        btnSave.Visible = True
        ddlActive.Enabled = True
    End Sub

    Protected Sub btnModify_Click(sender As Object, e As EventArgs) Handles btnModify.Click
        MakeThingsActive()
    End Sub


    Protected Sub btnAbort_Click(sender As Object, e As EventArgs) Handles btnAbort.Click
        LoadTextboxes(CInt(Session("AdvantageNumber_Selected_By_Manager")))
        MakeThingsInactive()
    End Sub
End Class
