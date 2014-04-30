
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub btnLoginFocus1_Click(sender As Object, e As EventArgs) Handles btnLoginFocus1.Click, btnLoginFocus2.Click, btnLoginFocus3.Click

        'doesn't quite work but who cares
        Dim mpContentPlaceHolder As ContentPlaceHolder
        Dim mpTextBox As TextBox
        mpContentPlaceHolder = _
            CType(Master.FindControl("ContentPlaceHolder1"),  _
            ContentPlaceHolder)
        If Not mpContentPlaceHolder Is Nothing Then
            mpTextBox = CType(mpContentPlaceHolder. _
                FindControl("txtUsername"), TextBox)
            If Not mpTextBox Is Nothing Then
                mpTextBox.Focus()
                lblMessage.Text = "hi"
            End If
        End If

    End Sub
End Class
