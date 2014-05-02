
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim DBEmployee As New DBEmployee
    Dim DBCrew As New ClassCrewScheduling
    Dim DBDate As New DBdate
    Dim DBFlightSearch As New DBFlightSearch

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("UserType") Is Nothing Or Session("UserType").ToString = "Customer" Then
            Response.Redirect("HomePage.aspx")
        End If

        If Session("UserType").ToString = "Agent" Then
            Response.Redirect("Emp_EmployeeDashboard.aspx")

        Else
            If IsPostBack = False Then
                calFlightSearch.SelectedDate = CDate(DBDate.ConvertToVBDate(DBDate.GetCurrentDate))

                If Session("UserType").ToString = "Manager" Then
                    'let them see the schedule for a crew member
                    LoadDDL()
                    ddlCrew.Visible = True
                    LoadScheduleGridView()
                Else
                    'load the page normally
                    LoadScheduleGridView()

                End If
            End If
        End If

    End Sub

    Public Sub LoadScheduleGridView()
        Dim intEmpType As Integer
        Dim strSQLDate As String

        If Session("UserType") = "Crew" Then
            DBEmployee.FindActiveEmpID(Session("UserID").ToString)
        Else
            DBEmployee.FindActiveEmpID(ddlCrew.SelectedValue.ToString)
        End If

        intEmpType = CInt(DBEmployee.dsEmployees.Tables("tblEmployeesClone").Rows(0).Item("EmpType"))

        strSQLDate = DBFlightSearch.AlterDate(calFlightSearch.SelectedDate.ToShortDateString)
        If Session("UserType").ToString = "Crew" Then

            DBCrew.GetSchedule(intEmpType, Session("UserID").ToString, strSQLDate)
        Else
            DBCrew.GetSchedule(intEmpType, DBEmployee.dsEmployees.Tables("tblEmployeesClone").Rows(0).Item("EmpID").ToString, strSQLDate)
        End If


        gvSchedule.DataSource = DBCrew.MyDataSetCaptain.Tables("tblSchedule")
        gvSchedule.DataBind()

        If gvSchedule.Rows.Count = 0 Then
            If Session("UserType").ToString = "Crew" Then
                lblMessage.Text = "You have the day off!"
            Else
                'manager is logged in 
                lblMessage.Text = "This crew member has the day off."
            End If
        End If

    End Sub

    Public Sub LoadDDL()
        DBCrew.GetCrewNames()

        ddlCrew.DataSource = DBCrew.MyDataSetCoCaptain.Tables("tblCoCaptain")
        ddlCrew.DataTextField = "Full Name"
        ddlCrew.DataValueField = "EmpID"
        ddlCrew.DataBind()

    End Sub

    Protected Sub calFlightSearch_SelectionChanged(sender As Object, e As EventArgs) Handles calFlightSearch.SelectionChanged
        LoadScheduleGridView()
    End Sub

    Protected Sub ddlCrew_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrew.SelectedIndexChanged
        LoadScheduleGridView()
    End Sub
End Class
