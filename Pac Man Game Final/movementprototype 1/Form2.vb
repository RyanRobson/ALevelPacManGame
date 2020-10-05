Public Class PacManStartUpForm
    Private Sub PacManStartUpForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode 'e.keycode detects for any keypresses made by the user
            Case Is = Keys.Enter ' this checks for the Enter key being pressed
                Form1.Visible = True 'makers thie form become visible and useable by the user
                Me.Close() 'make this form close
            Case Is = Keys.Escape ' if the Escape key is pressed
                Me.Close()        ' The project is closed
                Form1.Close()
            Case Is = Keys.H  'if the H key is pressed
                Me.Close()    'The currwent form is closed
                HighScoreListForm.Visible = True  ' and the new form is made visible 

            Case Is = Keys.F1    ' if the F1 button is pressed
                FRMHelp.Visible = True  ' then the help form is made visible

        End Select
    End Sub

  
    Private Sub PacManStartUpForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

   
End Class