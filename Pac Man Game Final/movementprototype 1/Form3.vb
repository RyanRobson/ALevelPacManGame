Public Class FRMHelp

    Private Sub FRMHelp_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Is = Keys.Escape
                Me.Close()
            Case Is = Keys.Enter
                Me.Close()
                PacManStartUpForm.Visible = True


        End Select
    End Sub

    Private Sub FRMHelp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub
End Class