Option Strict On

Partial Class _Default
    Inherits System.Web.UI.Page

   
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("RunningSubtotal") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        Else
            lblSubTotal.Text = "Your subtotal is " & FormatCurrency(Session("RunningSubtotal").ToString)
        End If
    End Sub
End Class
