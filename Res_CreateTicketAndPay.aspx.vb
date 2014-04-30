Option Strict On

Partial Class _Default
    Inherits System.Web.UI.Page
    Dim Calculate As New ClassCalculate

   
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        ElseIf Session("UserType").ToString = "Crew" Then
            Response.Redirect("Emp_EmployeeDashboard.aspx")
        End If

        If Session("RunningSubtotal") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        Else
            lblSubTotal.Text = "Your subtotal is " & FormatCurrency(Session("RunningSubtotal").ToString)
        End If
    End Sub


    Protected Sub btnCalculateTotal_Click(sender As Object, e As EventArgs) Handles btnCalculateTotal.Click
        Dim decTotal As Decimal
        decTotal = Calculate.CalculateTotal(CDec(Session("RunningSubtotal")))

        lblTotal.Text = "Your total is " & decTotal.ToString("c2") & ". Please click Pay to pay."

        btnCalculateTotal.Visible = False

        lblTotal.Visible = True

        btnPay.Visible = True

    End Sub

    Protected Sub btnPay_Click(sender As Object, e As EventArgs) Handles btnPay.Click

        Session.Remove("RunningSubtotal")
        Response.Redirect("Cust_ViewAndOrConfirmReservation.aspx")


    End Sub
End Class
