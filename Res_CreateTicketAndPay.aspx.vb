Option Strict On

Partial Class _Default
    Inherits System.Web.UI.Page
    Dim Calculate As New ClassCalculate

   
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("RunningSubtotal") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        Else
            lblSubTotal.Text = "Your subtotal is " & FormatCurrency(Session("RunningSubtotal").ToString)
        End If
    End Sub


    Protected Sub btnCalculateTotal_Click(sender As Object, e As EventArgs) Handles btnCalculateTotal.Click
        Dim decTotal As Decimal
        decTotal = Calculate.CalculateTotal(CDec(Session("RunningSubtotal")))

        lblTotal.Text = "Your total is $" & Session("RunningSubtotal").ToString & ". Please click Pay to pay."

        btnPay.Visible = True

    End Sub

    Protected Sub btnPay_Click(sender As Object, e As EventArgs) Handles btnPay.Click

        Response.Redirect("Cust_ViewAndOrConfirmReservation.aspx")


    End Sub
End Class
