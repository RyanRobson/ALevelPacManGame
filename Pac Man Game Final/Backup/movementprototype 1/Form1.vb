Public Class Form1

    'Private constants 
    Private Const Player_Width = 35
    Private Const Player_Hight = 35
    'interger Variable lists 
    Dim intVelocityx As Integer
    Dim intvelocityY As Integer
    Dim pbPacman As New PictureBox
    Dim intResetX As Integer
    Dim IntResetY As Integer
    Dim intAlignedLeft As Integer
    Dim intAlignedRight As Integer
    Dim intAlignedUp As Integer
    Dim intAlignedDown As Integer
    Dim intPlayerLives As Integer = 3
    Dim intTimePlayed As Integer
    Dim maze As System.Drawing.Bitmap




    Private Sub Search_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        ' maneges the players keystrokes 
        Select Case e.KeyCode
            Case Is = Keys.Up
                intAlignedUp = pbPacman.Location.X Mod 35
                If intAlignedUp <= 17 Then
                    intVelocityx = 0
                    intvelocityY = -7
                    intResetX = ((pbPacman.Location.X / 35) - Int(pbPacman.Location.X / 35)) * 35
                    ' calculates the distance from the last X coordinate square
                    pbPacman.SetBounds(pbPacman.Location.X - intResetX, pbPacman.Location.Y, 35, 35)
                    ' moves the player to the last x coordinate grid.
                    pbPacman.Image = PictureBox2.Image
                End If
            Case Is = Keys.Down
                intAlignedDown = pbPacman.Location.X Mod 35
                If intAlignedDown <= 17 Then
                    intVelocityx = 0
                    intvelocityY = 7
                    intResetX = ((pbPacman.Location.X / 35) - Int(pbPacman.Location.X / 35)) * 35
                    ' calculates the distance from the last X coordinate square
                    pbPacman.SetBounds(pbPacman.Location.X - intResetX, pbPacman.Location.Y, 35, 35)
                    ' moves the player to the last x coordinate grid.
                    pbPacman.Image = PictureBox4.Image
                End If
            Case Is = Keys.Left
                intAlignedLeft = pbPacman.Location.Y Mod 35
                If intAlignedLeft <= 17 Then
                    intvelocityY = 0
                    intVelocityx = -7
                    IntResetY = ((pbPacman.Location.Y / 35) - Int(pbPacman.Location.Y / 35)) * 35
                    ' calculates the distance from the last y coordinate square
                    pbPacman.SetBounds(pbPacman.Location.X, pbPacman.Location.Y - IntResetY, 35, 35)
                    ' moves the player to the last y coordinate grid.
                    pbPacman.Image = PictureBox3.Image
                End If
            Case Is = Keys.Right
                intAlignedRight = pbPacman.Location.Y Mod 35
                If intAlignedRight <= 17 Then
                    intvelocityY = 0
                    intVelocityx = 7
                    IntResetY = ((pbPacman.Location.Y / 35) - Int(pbPacman.Location.Y / 35)) * 35
                    ' calculates the distance from the last y coordinate square
                    pbPacman.SetBounds(pbPacman.Location.X, pbPacman.Location.Y - IntResetY, 35, 35)
                    ' moves the player to the last y coordinate grid.
                    pbPacman.Image = PictureBox5.Image
                End If
            Case Is = Keys.Space
                intVelocityx = 0
                intvelocityY = 0
            Case Is = Keys.Escape
                Me.Close()

        End Select
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TMRVelocity.Start()
        TMRTimeCounter.Start()
        pbPacman = PictureBox1
        PictureBox2.Visible = False
        PictureBox3.Visible = False
        PictureBox4.Visible = False
        PictureBox5.Visible = False
        PictureBox7.Visible = False
        PictureBox8.Visible = False
        PictureBox9.Visible = False
        PictureBox10.Visible = False
        LBlTimePlayed.Visible = True
        Panel2.Visible = True
        Panel1.Height = Me.ClientSize.Height
        Panel1.Width = Me.ClientSize.Width
        Panel2.Height = Me.ClientSize.Height
        Panel2.Width = Me.ClientSize.Width
        Panel3.Height = Me.ClientSize.Height
        Panel3.Width = Me.ClientSize.Width
        Panel1.Top = 0
        Panel1.Left = 0
        Panel2.Top = 0
        Panel2.Left = 0
        Panel3.Top = 0
        Panel3.Left = 0
        maze = My.Resources.Maze()
        For Index = 0 To 149


            Index = Index + 1
        Next

    End Sub

    Private Sub TMRVelocity_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TMRVelocity.Tick
        pbPacman.Left = pbPacman.Left + intVelocityx
        pbPacman.Top = pbPacman.Top + intvelocityY
        If pbPacman.Left < 0 Then
            pbPacman.Left = Me.ClientSize.Width
        End If
        If pbPacman.Left > Me.ClientSize.Width Then
            pbPacman.Left = 0
        End If
        If pbPacman.Top <= 0 Then
            intvelocityY = 0
        End If
        If pbPacman.Top > Me.ClientSize.Height Then
            pbPacman.Top = 0
        End If
    End Sub

    Private Sub Panel2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel2.Click
        pbPacman = PictureBox6
        Panel2.Visible = False
        Panel1.Visible = True
        intVelocityx = 0
        intvelocityY = 0
    End Sub

    Private Sub Panel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel1.Click
        pbPacman = PictureBox1
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = True
        intVelocityx = 0
        intvelocityY = 0
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint
        For gridx = 0 To 50
            For gridy = 0 To 50
                e.Graphics.DrawRectangle(Pens.White, 35 * gridx, 35 * gridy, 35, 35)
            Next
        Next

    End Sub

    Private Sub Panel1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
        For gridx = 0 To 50
            For gridy = 0 To 50
                e.Graphics.DrawRectangle(Pens.Black, 35 * gridx, 35 * gridy, 35, 35)
            Next
        Next
    End Sub

    Private Sub Panel3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel3.Click
        Panel2.Visible = True
        Panel3.Visible = False
        Panel1.Visible = False
    End Sub

    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint
        For gridx = 0 To 50
            For gridy = 0 To 50
                e.Graphics.DrawRectangle(Pens.Black, 35 * gridx, 35 * gridy, 35, 35)

            Next
        Next
    End Sub

    Private Sub TMRTimeCounter_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TMRTimeCounter.Tick
        If intVelocityx = 0 And intvelocityY = 0 Then
        Else
            intTimePlayed = intTimePlayed + 1
        End If
        LBlTimePlayed.Text = Int(intTimePlayed / 10)
    End Sub


    Private Function makeX(ByVal input As Integer)
        Return input * 35
    End Function
    Private Function makeY(ByVal input As Integer)
        Return input * 35
    End Function

    Private Sub PBInky_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBInky.Click

    End Sub
End Class
