Option Strict On
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("UserType").ToString <> "Customer" Then
            Response.Redirect("HomePage.aspx")
        End If

        If Session("JustLogged") Is Nothing Then
        Else
            Dim lblMessage As Label = CType(Master.FindControl("lblMessage"), Label)
            lblMessage.Text = "Log in successful"
            Session("JustLogged") = Nothing
        End If

    End Sub
End Class
