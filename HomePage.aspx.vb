
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click
        Dim txtUsername As TextBox = CType(Master.FindControl("txtUsername"), TextBox)

        txtUsername.Text = ""
        txtUsername.Focus()
    End Sub
End Class
