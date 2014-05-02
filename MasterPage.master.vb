Option Strict On
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Dim Valid As New ClassValidate
    Dim DBCustomer As New DBCustomersClone
    Dim DBEmployee As New DBEmployee
    Dim DBDate As New DBdate

    'If Session("UserType") = "Crew" Then
    'Response.Redirect("Homepage.aspx")
    'Elseif
    'Session("UserType") = "Employee" Then
    'Response.Redirect("Homepage.aspx")
    'Else
    'let them load the page
    'End if

    'Session("UserType") holds either "Customer" , "Manager" , "Agent" (for gate agent), and "Crew" for crew
    'SEssion("UserID") hold the advantage number of the EmpID depending on the employee type. 
    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim strUser As String
        Dim strPassword As String

        strUser = txtUsername.Text
        strPassword = txtPassword.Text

        lblMessage.Text = ""

        If Valid.CheckIntegerWithSubstring(strUser) = False Then
            'they didn't enter a number
            lblMessage.Text = "Please enter an integer to log in."
            Exit Sub
        End If

        If Len(strUser) = 3 Then
            'this is an employee
            If DBEmployee.CheckEmpExists(strUser) = False Then
                lblMessage.Text = "That employee ID does not exist."
                Exit Sub
            Else
                'check password
                If DBEmployee.CheckPassword(strUser, strPassword) = True Then
                    'log them in
                    Session.Add("UserID", strUser)
                    'check to see if they are a manager or employee
                    Session.Add("UserType", DBEmployee.CheckEmployeeType(strUser))
                    ToggleLoginButton()
                    Session.Add("JustLogged", "Yes")
                    Response.Redirect("Emp_EmployeeDashboard.aspx")
                Else
                    lblMessage.Text = "Username password combination is incorrect."
                    Exit Sub
                End If

            End If

        ElseIf Len(strUser) = 4 Then
            'this is a customer
            If DBCustomer.CheckCustomerExists(strUser) = False Then
                'no customer with that advantage number
                lblMessage.Text = "That advantage number does not exist or has been inactivated."
                Exit Sub
            Else
                'check password
                If DBCustomer.CheckPassword(strUser, strPassword) = True Then
                    Session.Add("UserID", strUser)
                    Session.Add("UserType", "Customer")
                    lblMessage.Text = "Login Successful"
                    ToggleLoginButton()
                    Session.Add("JustLogged", "Yes")
                    Response.Redirect("Cust_CustomerDashboard.aspx")
                Else
                    lblMessage.Text = "Login Failure."
                    Exit Sub
                End If
            End If
        Else
            lblMessage.Text = "log in is three digits long for an employee, four digits long for a customer."
            Exit Sub
        End If

        txtPassword.Text = ""
        txtUsername.Text = ""

    End Sub

    Public Sub ToggleLoginButton()
        btnLogin.Visible = Not btnLogin.Visible
        btnLogout.Visible = Not btnLogout.Visible
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim strDate As String
        Dim strTime As String

        strDate = DBDate.GetCurrentDate()

        strDate = DBDate.ConvertToVBDate(strDate).ToShortDateString
        strTime = DBDate.getcurrentTime()

        'check time length
        If Len(strTime) = 4 Then
            lblTime.Text = "The date is " & strDate & " and the time is " & strTime.Substring(0, 2) & ":" & strTime.Substring(2, 2) & "."
        Else
            lblTime.Text = "The date is " & strDate & " and the time is " & strTime.Substring(0, 1) & ":" & strTime.Substring(1, 2) & "."
        End If

        If Session("UserID") Is Nothing Then
            'no one is logged in
            btnLogout.Visible = False
            pnlID.Visible = True
            pnlPassword.Visible = True

        Else
            btnLogin.Visible = False
            pnlID.Visible = False
            pnlPassword.Visible = False
            btnCreateProfile.Visible = False
        End If

    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        lblMessage.Text = ""
        Session.RemoveAll()
        Response.Redirect("HomePage.aspx")
    End Sub


    Protected Sub btnCreateProfile_Click(sender As Object, e As EventArgs) Handles btnCreateProfile.Click
        Response.Redirect("Cust_AddNewCustomer.aspx")
    End Sub
   


End Class

