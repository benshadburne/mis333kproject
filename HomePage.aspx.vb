
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub btnLoginFocus1_Click(sender As Object, e As EventArgs) Handles btnLoginFocus1.Click, btnLoginFocus2.Click, btnLoginFocus3.Click

        Dim txtUsername As TextBox = CType(Master.FindControl("txtUsername"), TextBox)

        txtUsername.Text = ""
        txtUsername.Focus()
    End Sub
End Class
