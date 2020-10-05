Imports System.Drawing.Drawing2D
Imports System.Threading

'      Pac-Man Game
'Ryan Robson 
'A2 Computing Project 
'Last updated 15/03/11


Public Class Form1

    'Private global constants within the form 
    Private Const Player_Width = 20
    Private Const Player_Hight = 20

    'Global interger Variable lists 
    Dim intVelocityx As Integer
    Dim intvelocityY As Integer
    Dim intResetX As Integer
    Dim IntResetY As Integer
    Dim intAlignedLeft As Integer
    Dim intAlignedRight As Integer
    Dim intAlignedUp As Integer
    Dim intAlignedDown As Integer
    Dim intPlayerLives As Integer = 3
    Dim intTimePlayed As Integer
    Dim pacLocX As Integer
    Dim PacLocY As Integer
    Dim InterSectIndex As Integer = 1
    Dim intghost1VelocityX As Integer
    Dim intghost1Velocityy As Integer
    Dim intGhost2VelocityX As Integer
    Dim intghost2VelocityY As Integer
    Dim intghost3velocityX As Integer
    Dim intghost3velocityy As Integer
    Dim intghost4VelocityX As Integer
    Dim intghost4velocityy As Integer
    Dim GhostSpeed As Integer
    Dim GhostSize As Integer
    Dim PowerPellettime As Integer
    Dim IntPlayerScore As Integer

    'Boolean variable lists
    Dim KeysUp As Boolean
    Dim KeysDown As Boolean
    Dim KeysLeft As Boolean
    Dim KeysRight As Boolean
    Dim PacMoving As Boolean
    Dim powerpelletActive As Boolean

    'Picture Box Arrays and variables list
    Dim Wall(46) As PictureBox
    Dim Junction(34) As PictureBox
    Dim Ghosts(4) As PictureBox
    Dim pbPacman As New PictureBox
    Dim PacPellet(266) As PictureBox
    Dim powerpellet(4) As PictureBox

    'Declaring the background image as the map
    Private map As System.Drawing.Bitmap

    Public Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        'starts all of the necessery times when form 1 is loaded
        TMRGhost1.Start() ' |
        TMRGhost2.Start() ' |
        TMRGhost3.Start() ' --- these timers handle the movement of the ghosts  
        TMRGhost4.Start() ' |
        TMRHitghost.Start()  'this timer detects a collision between pacman and Ghosts
        TMRTimeCounter.Start() ' This timer counts the time the player has played the game for
        TMRPlayerScore.Start() ' This timer handles the counting of the players score, including the collision between pacman and Pac Dots
        TMRPowerPelletCollision.Start() ' This timer handles the collision between pacman and powerpellets

        'calling the declaratrion the arrays of picture boxes
        Call WallDeclaration()              '|
        Call VariableInitialisation()       '|
        Call GhostArrayDeclaration()        '--- these subroutines declare all of the picture boxes on the form into arrays.  
        Call PacDotDeclaration()            '|
        Call PowerPelletinitialisation()    '|

        'Declaring the maze image from my resources folder, as the background image. 
        map = My.Resources.Maze1() ' this image is used as the maze image on the background

        TXTScore.Focus() 'Sets the focus of the form on the textbox, which displays the score, and handles the KeyDown event for moving the player. 

    End Sub


    Private Sub TMRVelocity_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TMRVelocity.Tick
        'This timer handles the players velocity, and inturn, the movement of the player. 

        Call WallCollision() 'this calls the subrouting which detects colisions between the player and a wall.


        pbPacman.Left = pbPacman.Left + intVelocityx 'Moves Pacman left or right, depending on its X velocity
        pbPacman.Top = pbPacman.Top + intvelocityY 'Moves Pacman up or down, depending on its Y velocity
        'confines the player withint he bounds of the form,stoping it moveing off the top of the form, 
        'and when it leaves from one side, it will emerge on the otherside, used to make the warp tunnels

        If pbPacman.Left < 0 Then                     '|
            pbPacman.Left = Me.ClientSize.Width       '|
        End If                                        '|
        If pbPacman.Left > Me.ClientSize.Width Then   '|
            pbPacman.Left = 0                         '|
        End If                                        '--} this keeps pacman within the bounds of the form, not allowing it to move off the top of the form,
        If pbPacman.Top <= 0 Then                     '--} but allowing to move off the left and right, to make the warp tunnels
            intvelocityY = 0                          '|
        End If                                        '|
        If pbPacman.Top > Me.ClientSize.Height Then   '|
            pbPacman.Top = 0                          '|
        End If                                        '|

        Call WallCollision() 'Calls the subroutine which detects colisions between the player and a wall.
    End Sub

    Private Sub TMRTimeCounter_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TMRTimeCounter.Tick
        'This timer counts the game time that the player has played for, and sets it to a variable, intTimePlayed. 
        If intVelocityx = 0 And intvelocityY = 0 Then  '|
        Else                                           '-- This increases the vlauue of the timer if the player is moving, 
            intTimePlayed = intTimePlayed + 1          '-- and stops increasing it when the player is not moving
        End If                                         '|
        'Sets a label to show the value of the varable storing the game time. 
        LBlTimePlayed.Text = Int(intTimePlayed / 10) ' Changes the value of the lable which displays the timer

    End Sub

    Private Sub WallCollision()
        ' This handles the collisions between the player and the walls of the maze 
        For index = 1 To 46    'Loops through all of the pictureboxes which make the maze
            If pbPacman.Bounds.IntersectsWith(Wall(index).Bounds) Then ' chacks for a collision with each wall 
                'this checks for a collision with PacMan moving up and down
                If intvelocityY = 2 Then  'if PacMan if moving down when there is a collison
                    intvelocityY = 0      ' then PacMan stops moving down
                    pbPacman.SetBounds(pbPacman.Location.X, pbPacman.Location.Y - 2, Player_Hight, Player_Width) 'Pacman is moved 2 pixals backwards to avoid getting stuck in a wall
                ElseIf intvelocityY = -2 Then 'If pacman is moving up when there is a collison
                    intvelocityY = 0          'then pacman stops moving
                    pbPacman.SetBounds(pbPacman.Location.X, pbPacman.Location.Y + 2, Player_Hight, Player_Width) 'Pacman is moved 2 pixels backwards to aviod getting stuck in a wall
                End If
                'This checks for a collision with PacMan moving left and right
                If intVelocityx = 2 Then ' if pacman is moving right
                    intVelocityx = 0     ' PacMan stops moving 
                    pbPacman.SetBounds(pbPacman.Location.X - 2, pbPacman.Location.Y, Player_Hight, Player_Width) 'PacMan is moved 2 Pixels backwards to aviod getting stuck in a wall
                ElseIf intVelocityx = -2 Then  'if PacMan is moving left
                    intVelocityx = 0           ' PacMan stops moving
                    pbPacman.SetBounds(pbPacman.Location.X + 2, pbPacman.Location.Y, Player_Hight, Player_Width) 'PacMan is moved 2 pixels backwards to aviod getting stuck in a wall
                End If
            End If
        Next
    End Sub
    Private Sub VariableInitialisation()
        'this subroutine initialises all of the neccessery variables and is called on form load. 
        pacLocX = 210   ' Sets the X locations of PacMan to 210
        PacLocY = 321   ' Sets the Y location of PacMan  to 321
        TMRVelocity.Start() 'starts the timer which handles the velocity of PacMan
        TMRTimeCounter.Start() 'starts the timer which handles the time counter
        pbPacman = PictureBox1 ' initialised the varible, PBPacman to pictuerebox 1 
        PictureBox2.Visible = False  '|
        PictureBox3.Visible = False  '|
        PictureBox4.Visible = False  '|
        PictureBox5.Visible = False  '-- makes all of these pictureboxes, which contain the differnt images of pacman, invisible. 
        PictureBox7.Visible = False  '|
        PictureBox8.Visible = False  '|
        PictureBox9.Visible = False  '|
        PictureBox10.Visible = False '|
        LBlTimePlayed.Visible = True
        powerpelletActive = False
        pacLocX = pbPacman.Left ' changes the varible PacLocX to the location of pacmans X co-ordinate
        PacLocY = pbPacman.Top  ' changes the variable PacLocY to the location of PacMans Y Coordinate 
        GhostSpeed = 2          ' the speed in which the ghosts move 
        intghost1VelocityX = GhostSpeed   '|
        intghost1Velocityy = 0            '|
        intGhost2VelocityX = -GhostSpeed  '|
        intghost2VelocityY = 0            '|
        intghost3velocityX = 0            '-- initialises the starting velocities of the ghosts, so that they move in different directions
        intghost3velocityy = GhostSpeed   '|
        intghost4VelocityX = 0            '|
        intghost4velocityy = GhostSpeed   '|
        PowerPellettime = 0 'The time remaing for powerpellets
        GhostSize = 20      'The size of the ghosts pictureboxes
    End Sub

    Private Sub TMRGhost1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TMRGhost1.Tick
        'this timer handles the movement and wall collision for the first ghost, PBBlinkyy
        Dim ii As Integer
        Static direction As Integer = 0
        Dim random As New Random()  ' generates a random number, to determin which direction the ghost moves
        PBBlinkyy.Left = PBBlinkyy.Left + intghost1VelocityX ' moves the picture box left and right, dependingon the bvalue of the velocity
        PBBlinkyy.Top = PBBlinkyy.Top + intghost1Velocityy   ' moves the picturebox up and down, depending on the value of the velocity
        For i = 1 To 46  ' checks on every picturebox making the walls,
            direction = random.Next(0, 2) 'sets direction to a random bnumber between 0 and 2
            'This uses a random number to deecide on which direction to move after a collision is detected. 
            If PBBlinkyy.Bounds.IntersectsWith(Wall(i).Bounds) Then 'checks for a collsion with each section of the walls
                If (intghost1VelocityX <> 0) Then ' if as the ghost is moving
                    If direction = 0 Then 'and if direction = 0 
                        intghost1VelocityX = -intghost1VelocityX ' then the ghosts goes ion the reverse direction is was moving
                    Else
                        PBBlinkyy.SetBounds(PBBlinkyy.Location.X - intghost1VelocityX, PBBlinkyy.Location.Y + 2, GhostSize, GhostSize) 'else the ghost moves upwards 
                        intghost1VelocityX = 0 ' and stops moving on the X coordinate 
                        intghost1Velocityy = GhostSpeed ' and moved up or down.
                        For ii = 1 To 46 ' checks on each picturebox which makes up the walls
                            If PBBlinkyy.Bounds.IntersectsWith(Wall(ii).Bounds) Then ' if there is a collsion between the ghost and the wall
                                intghost1Velocityy = -intghost1Velocityy ' Then the ghosts Y velocity is reversed, and it moves in the oposite direction
                                PBBlinkyy.SetBounds(PBBlinkyy.Location.X, PBBlinkyy.Location.Y - 2, GhostSize, GhostSize) ' The ghost is moved upwards, to stop getting stuck in a wall
                                ii = 46 ' end the loop if there is a collison
                            End If
                        Next
                        i = 46 ' ends the loop if there is a collison
                    End If
                Else
                    If direction = 0 Then ' if direction = 0
                        intghost1Velocityy = -intghost1Velocityy ' the ghosts Y velocity is reveresed, so that it moves in the oposite direction
                    Else
                        PBBlinkyy.SetBounds(PBBlinkyy.Location.X + 2, PBBlinkyy.Location.Y - intghost1Velocityy, GhostSize, GhostSize) ' moves the ghosts away from the walls, 
                        intghost1VelocityX = GhostSpeed ' sets the ghosts X velocity to the value of ghostspeed
                        intghost1Velocityy = 0          ' stops the ghost moving in the Y coordinate 
                        For ii = 1 To 46 ' checks for every picturbox making the walls 
                            If PBBlinkyy.Bounds.IntersectsWith(Wall(ii).Bounds) Then ' if there is a collision 
                                intghost1VelocityX = -intghost1VelocityX ' then the ghosts X velocity is reversed, so it moves in theoposite direction
                                PBBlinkyy.SetBounds(PBBlinkyy.Location.X - 2, PBBlinkyy.Location.Y, GhostSize, GhostSize) ' The ghost is moved to the left, so it does not get stuck in a wall
                                ii = 46 ' end the loop, if there is a collision
                            End If
                        Next
                        i = 46 ' ends the loop if there is a collision
                    End If
                End If
            End If
        Next

        If PBBlinkyy.Left < 0 Then                      '|
            PBBlinkyy.Left = Me.ClientSize.Width        '|
        End If                                          '|
        If PBBlinkyy.Left > Me.ClientSize.Width Then    '|
            PBBlinkyy.Left = 0                          '|
        End If                                          '-- this stops the ghosts from moving off the top of the form, but allows it to 
        If PBBlinkyy.Top <= 0 Then                      '-- move off the left and right, to use the warp tunnels
            intghost1Velocityy = 0                      '|
        End If                                          '|
        If PBBlinkyy.Top > Me.ClientSize.Height Then    '|
            PBBlinkyy.Top = 0                           '|
        End If                                          '|
    End Sub

    Private Sub TMRGhost2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TMRGhost2.Tick

        PBPinkyy.Left = PBPinkyy.Left + intGhost2VelocityX ' moves PacMan left and right, depending on the value of intGhost2VelocityX 
        PBPinkyy.Top = PBPinkyy.Top + intghost2VelocityY   ' moves PacMan up and down, depending on the value of intGhost2VelocityY

        Dim ii As Integer
        Static direction As Integer = 0
        Dim random As New Random() ' sets random as a random number
        For i = 1 To 46 'checks all of the pictureboxes which make the walls
            direction = random.Next(0, 2) ' sets direction as a random number between 0 and 2
            If PBPinkyy.Bounds.IntersectsWith(Wall(i).Bounds) Then ' if there is a collision between the ghost and a wall
                If (intGhost2VelocityX <> 0) Then ' and if ghost is moving in the X coordinate
                    If direction = 0 Then ' and if direction = 0
                        intGhost2VelocityX = -intGhost2VelocityX ' the ghosts X veloctiy is reversed, so that the ghsot moves in the oposite direction
                    Else
                        PBPinkyy.SetBounds(PBPinkyy.Location.X - intGhost2VelocityX, PBPinkyy.Location.Y + 2, GhostSize, GhostSize) ' the ghost moves 2 pixels to the left, to stop getting stuck in a wall
                        intGhost2VelocityX = 0 ' the ghosts X velocity is set to 0, to stop the ghost moving that way
                        intghost2VelocityY = GhostSpeed  ' the ghosts Y velocity is set to 0, to start it moving up or down
                        For ii = 1 To 46 ' checks for each picture box that makes the walls 
                            If PBPinkyy.Bounds.IntersectsWith(Wall(ii).Bounds) Then ' if there is a collsion between the ghost and a wall section
                                intghost2VelocityY = -intghost2VelocityY ' Then the ghosts Y velocity is reveres, so it moves inthe oposide direction
                                PBPinkyy.SetBounds(PBPinkyy.Location.X, PBPinkyy.Location.Y - 2, GhostSize, GhostSize) ' the ghost is moved upwards, so that it doesnt get stuck in a wall
                                ii = 46 ' ends the loop if there is a collision
                            End If
                        Next
                        i = 46 ' ends the loop if there is a colllision
                    End If

                Else
                    If direction = 0 Then  ' if direction = 0 then 
                        intghost2VelocityY = -intghost2VelocityY 'the ghosts Y velocity is reversed, so that the ghsot moves in the oposide direction
                    Else
                        PBPinkyy.SetBounds(PBPinkyy.Location.X + 2, PBPinkyy.Location.Y - intghost2VelocityY, GhostSize, GhostSize) ' moves the ghost to the left and upwards, so that it does not get stuck in a wall
                        intGhost2VelocityX = GhostSpeed  ' sets the X velocity to 2, so that the ghsot moves accross the X coordinate
                        intghost2VelocityY = 0 ' sets the Y velocty to  0 so that it does not move allong the Y coordinate.
                        For ii = 1 To 46 ' checks for each picture box which makes the walls
                            If PBPinkyy.Bounds.IntersectsWith(Wall(ii).Bounds) Then ' if there is a collision
                                intGhost2VelocityX = -intGhost2VelocityX ' the ghosts X velocity is reversed, fo that it moved in the oposite direction
                                PBPinkyy.SetBounds(PBPinkyy.Location.X - 2, PBPinkyy.Location.Y, GhostSize, GhostSize) ' The ghost is moved to the left, so that it does not get stuck in a wall
                                ii = 46 ' ends the loop if there is a collision
                            End If
                        Next
                        i = 46 ' ends the loop if there is a collision
                    End If
                End If
            End If
        Next

        If PBPinkyy.Left < 0 Then                     '|
            PBPinkyy.Left = Me.ClientSize.Width       '|
        End If                                        '|
        If PBPinkyy.Left > Me.ClientSize.Width Then   '|
            PBBlinkyy.Left = 0                        '|
        End If                                        '-- this stops the ghost moving off the top and bottom of the form but allows it to 
        If PBPinkyy.Top <= 0 Then                     '-- move off the left and right, to use the warp tunnels 
            intghost2VelocityY = 0                    '|
        End If                                        '|
        If PBPinkyy.Top > Me.ClientSize.Height Then   '|
            PBPinkyy.Top = 0                          '|
        End If                                        '|
    End Sub

    Private Sub TMRHitghost_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TMRHitghost.Tick
        Static rnd As Integer      '|
        Dim random As New Random   '--  generates a new random number, used for the re-spawning of the ghosts after they have been eaten. 
        rnd = random.Next(1, 266)  '|

        For i = 1 To 4 ' checks for all 4 of the ghosts
            If pbPacman.Bounds.IntersectsWith(Ghosts(i).Bounds) Then ' if there is a collision between PacMan and a ghost
                If powerpelletActive = True Then 'checks for if the player had an active powerpellet
                    Ghosts(i).Location = PacPellet(rnd).Location ' the ghost is remved, and places it on the location of a random pac dot, to remove the ghost spawn glitch
                    IntPlayerScore = IntPlayerScore + 100 ' increses the players score by 100
                    TXTScore.Text = IntPlayerScore ' this textbox displays the value of the players score
                Else
                    Call Deathsequence() ' this handles the death of pacman
                End If
            End If
        Next
        LBLPlayerLives.Text = intPlayerLives 'this label shows the number of lives the plpayer has
    End Sub

    Private Sub Deathsequence()
        pbPacman.SetBounds(210, 321, Player_Hight, Player_Width) ' sets the location of pacman to its original position
        intPlayerLives = intPlayerLives - 1 'removes one life from the players life counter
        intVelocityx = 0 ' sets the X velocity to 0, so PacMan is not moving
        intvelocityY = 0 ' sets the Y velocity to 0, so PacMan is not moving

        If intPlayerLives = 0 Then ' if the player has no lives left
            intVelocityx = 0        '|
            intvelocityY = 0        '|
            intghost1VelocityX = 0  '|
            intghost1Velocityy = 0  '|
            intGhost2VelocityX = 0  '-- stops all of the ghosts from moving f the player has no lives left
            intghost2VelocityY = 0  '|
            intghost3velocityX = 0  '|
            intghost3velocityy = 0  '|
            intghost4VelocityX = 0  '|
            intghost4velocityy = 0  '|
            MsgBox("Game Over, You scored " & IntPlayerScore & " points") ' shows the players score when the game is over
            Me.Close() ' closes the form when the game is over
        End If
    End Sub


    Private Sub WallDeclaration()
        ' Declaring my array of picture boxes to the pictureboxes containing each section of wall
        Wall(1) = PBWall1
        Wall(2) = PBWall2
        Wall(3) = PBWall3
        Wall(4) = PBWall4
        Wall(5) = PBWall5
        Wall(6) = PBWall6
        Wall(7) = PBWall7
        Wall(8) = PBWall8
        Wall(9) = PBWall9
        Wall(10) = PBWall10
        Wall(11) = PBWall11
        Wall(12) = PBWall12
        Wall(13) = PBWall13
        Wall(14) = PBWall14
        Wall(15) = PBWall15
        Wall(16) = PBWall16
        Wall(17) = PBWall17
        Wall(18) = PBWall18
        Wall(19) = PBWall19
        Wall(20) = PBWall20
        Wall(21) = PBWall21
        Wall(22) = PBWall22
        Wall(23) = PBWall23
        Wall(24) = PBWall24
        Wall(25) = PBWall25
        Wall(26) = PBWall26
        Wall(27) = PBWall27
        Wall(28) = PBWall28
        Wall(29) = PBWall29
        Wall(30) = PBWall30
        Wall(31) = PBWall31
        Wall(32) = PBWall32
        Wall(33) = PBWall33
        Wall(34) = PBWall34
        Wall(35) = PBWall35
        Wall(36) = PBWall36
        Wall(37) = PBWall37
        Wall(38) = PBWall38
        Wall(39) = PBWall39
        Wall(40) = PBWall40
        Wall(41) = PBWall41
        Wall(42) = PBWall42
        Wall(43) = PBWall43
        Wall(44) = PBWall44
        Wall(45) = PBWall45
        Wall(46) = PBWall46
    End Sub
    Private Sub GhostArrayDeclaration()
        Ghosts(1) = PBBlinkyy '| 
        Ghosts(2) = PBPinkyy  '-- declared each ghost as an element in an array
        Ghosts(3) = PBInkyy   '|
        Ghosts(4) = PBClydee  '|
    End Sub

    Private Sub TMRGhost3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TMRGhost3.Tick
        PBInkyy.Left = PBInkyy.Left + intghost3velocityX
        PBInkyy.Top = PBInkyy.Top + intghost3velocityy

        Dim ii As Integer
        Static direction As Integer = 0
        Dim random As New Random()

        For i = 1 To 46
            direction = random.Next(0, 2)

            If PBInkyy.Bounds.IntersectsWith(Wall(i).Bounds) Then
                If (intghost3velocityX <> 0) Then
                    If direction = 0 Then
                        intghost3velocityX = -intghost3velocityX
                    Else
                        PBInkyy.SetBounds(PBInkyy.Location.X - intghost3velocityX, PBInkyy.Location.Y + 2, GhostSize, GhostSize)
                        intghost3velocityX = 0
                        intghost3velocityy = GhostSpeed
                        For ii = 1 To 46
                            If PBInkyy.Bounds.IntersectsWith(Wall(ii).Bounds) Then
                                intghost3velocityy = -intghost3velocityy
                                PBInkyy.SetBounds(PBInkyy.Location.X, PBInkyy.Location.Y - 2, GhostSize, GhostSize)
                                ii = 46
                            End If
                        Next
                        i = 46
                    End If
                   
                Else

                    If direction = 0 Then
                        intghost3velocityy = -intghost3velocityy
                    Else
                        PBInkyy.SetBounds(PBInkyy.Location.X + 2, PBInkyy.Location.Y - intghost3velocityy, GhostSize, GhostSize)
                        intghost3velocityX = GhostSpeed
                        intghost3velocityy = 0
                        For ii = 1 To 46
                            If PBInkyy.Bounds.IntersectsWith(Wall(ii).Bounds) Then
                                intghost3velocityX = -intghost3velocityX
                                PBInkyy.SetBounds(PBInkyy.Location.X - 2, PBInkyy.Location.Y, GhostSize, GhostSize)
                                ii = 46
                            End If
                        Next
                        i = 46
                    End If
                End If
            End If
        Next

    End Sub

    Private Sub TMRGhost4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TMRGhost4.Tick
        PBClydee.Left = PBClydee.Left + intghost4VelocityX
        PBClydee.Top = PBClydee.Top + intghost4velocityy

        Dim ii As Integer
        Static direction As Integer = 0
        Dim random As New Random()

        For i = 1 To 46
            direction = random.Next(0, 2)

            If PBClydee.Bounds.IntersectsWith(Wall(i).Bounds) Then
                If (intghost4VelocityX <> 0) Then
                    If direction = 0 Then
                        intghost4VelocityX = -intghost4VelocityX
                    Else
                        PBClydee.SetBounds(PBClydee.Location.X - intghost4VelocityX, PBClydee.Location.Y + 2, GhostSize, GhostSize)
                        intghost4VelocityX = 0
                        intghost4velocityy = GhostSpeed
                        For ii = 1 To 46
                            If PBClydee.Bounds.IntersectsWith(Wall(ii).Bounds) Then
                                intghost4velocityy = -intghost4velocityy
                                PBClydee.SetBounds(PBClydee.Location.X, PBClydee.Location.Y - 2, GhostSize, GhostSize)
                                ii = 46
                            End If
                        Next
                        i = 46
                    End If
                   
                Else

                    If direction = 0 Then
                        intghost4velocityy = -intghost4velocityy
                    Else
                        PBClydee.SetBounds(PBClydee.Location.X + 2, PBClydee.Location.Y - intghost4velocityy, GhostSize, GhostSize)
                        intghost4VelocityX = GhostSpeed
                        intghost4velocityy = 0
                        For ii = 1 To 46
                            If PBClydee.Bounds.IntersectsWith(Wall(ii).Bounds) Then
                                intghost4VelocityX = -intghost4VelocityX
                                PBClydee.SetBounds(PBClydee.Location.X - 2, PBClydee.Location.Y, GhostSize, GhostSize)
                                ii = 46
                            End If
                        Next
                        i = 46
                    End If
                End If
            End If
        Next
    End Sub


    Private Sub TMRPlayerScore_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TMRPlayerScore.Tick
        Dim PacDotVisible As Boolean = False
        For i = 1 To 266 ' checks for all of the pacdots 
            If pbPacman.Bounds.IntersectsWith(PacPellet(i).Bounds) Then ' if there is a collision between pacman and a pacpellet
                If PacPellet(i).Visible = True Then ' if the pacpelet had not already been eaten 
                    IntPlayerScore = IntPlayerScore + 10 ' the players score increases by 10
                    PacPellet(i).Visible = False ' and maked the pacpellet invisilbe, so it can not be eaten again
                Else
                End If
                TXTScore.Text = IntPlayerScore ' changes the value of the textbox storing the score
            End If

            If PacPellet(i).Visible = True Then   '|
                PacDotVisible = True              '|
            End If                                '|
        Next                                      '|
        If PacDotVisible = False Then             '|
            For i = 1 To 266                      '-- checks if all of the pac dots have been collected, and if they have
                PacPellet(i).Visible = True       '-- sets the PacDotvisible to true, and makes them all visible again.
            Next                                  '|
            For i = 1 To 4                        '|
                powerpellet(i).Visible = True     '|
            Next                                  '|
            GhostSpeed = GhostSpeed * 1.5 ' increses the ghost s speed by 25% when the player colects all of the pacdots 
        End If
    End Sub
    Private Sub PowerPelletinitialisation()
        powerpellet(1) = Powerpellet1 '|
        powerpellet(2) = PowerPellet2 '-- declares each powerpellet into an array 
        powerpellet(3) = PowerPellet3 '|
        powerpellet(4) = PowerPellet4 '|


    End Sub

    Private Sub PacDotDeclaration()
        ' Declares each pocdot as an element in an array
        PacPellet(1) = PacDot1
        PacPellet(2) = PacDot2
        PacPellet(3) = PacDot3
        PacPellet(4) = PacDot4
        PacPellet(5) = PacDot5
        PacPellet(6) = PacDot6
        PacPellet(7) = PacDot7
        PacPellet(8) = PacDot8
        PacPellet(9) = PacDot9
        PacPellet(10) = PacDot10
        PacPellet(11) = PacDot11
        PacPellet(12) = Pacdot12
        PacPellet(13) = PacDot13
        PacPellet(14) = Pacdot14
        PacPellet(15) = Pacdot15
        PacPellet(16) = PacDot16
        PacPellet(17) = pacdot17
        PacPellet(18) = pacdot18
        PacPellet(19) = Pacdot19
        PacPellet(20) = Pacdot20
        PacPellet(21) = PacDot21
        PacPellet(22) = PacDot22
        PacPellet(23) = PacDot23
        PacPellet(24) = PacDot24
        PacPellet(25) = PacDot25
        PacPellet(26) = PacDot26
        PacPellet(27) = Pacdot27
        PacPellet(28) = PacDot28
        PacPellet(29) = PacDot29
        PacPellet(30) = Pacdot30
        PacPellet(31) = Pacdot31
        PacPellet(32) = Pacdot32
        PacPellet(33) = PacDot33
        PacPellet(34) = Pacdot34
        PacPellet(35) = PacDot35
        PacPellet(36) = PacDot36
        PacPellet(37) = PacDot37
        PacPellet(38) = PacDot38
        PacPellet(39) = PacDot39
        PacPellet(40) = PacDot40
        PacPellet(41) = PacDot41
        PacPellet(42) = PacDot42
        PacPellet(43) = PacDot43
        PacPellet(44) = PacDot44
        PacPellet(45) = PacDot45
        PacPellet(46) = PacDot46
        PacPellet(47) = Pacdot47
        PacPellet(48) = PacDot48
        PacPellet(49) = PacDot49
        PacPellet(50) = PacDot50
        PacPellet(51) = PacDot51
        PacPellet(52) = Pacdot52
        PacPellet(53) = PacDot53
        PacPellet(54) = PacDot54
        PacPellet(55) = PacDot55
        PacPellet(56) = PacDot56
        PacPellet(57) = PacDot57
        PacPellet(58) = PacDot58
        PacPellet(59) = PacDot59
        PacPellet(60) = PacDot60
        PacPellet(61) = PacDot61
        PacPellet(62) = PacDot62
        PacPellet(63) = PacDot63
        PacPellet(64) = PacDot64
        PacPellet(65) = PacDot65
        PacPellet(66) = PAcDot66
        PacPellet(67) = PAcDot67
        PacPellet(68) = PacDot68
        PacPellet(69) = Pacdot69
        PacPellet(70) = Pacdot70
        PacPellet(71) = PacDot71
        PacPellet(72) = PacDot72
        PacPellet(73) = PacDot73
        PacPellet(74) = PacDot74
        PacPellet(75) = PacDot75
        PacPellet(76) = Pacdot76
        PacPellet(77) = PacDot77
        PacPellet(78) = PacDot78
        PacPellet(79) = PacDot79
        PacPellet(80) = PacDot80
        PacPellet(81) = PAcDot81
        PacPellet(82) = PacDot82
        PacPellet(83) = PacDot83
        PacPellet(84) = PacDot84
        PacPellet(85) = PAcDot85
        PacPellet(86) = PacDot86
        PacPellet(87) = PacDot87
        PacPellet(88) = PAcDot88
        PacPellet(89) = PacDot89
        PacPellet(90) = PAcDot90
        PacPellet(91) = PacDot91
        PacPellet(92) = PAcDot92
        PacPellet(93) = PAcDot93
        PacPellet(94) = PAcDot94
        PacPellet(95) = PacDot95
        PacPellet(96) = PAcdot96
        PacPellet(97) = Pacdot97
        PacPellet(98) = PacDot98
        PacPellet(99) = PAcDot99
        PacPellet(100) = PacDot100
        PacPellet(101) = PacDot101
        PacPellet(102) = PacDot102
        PacPellet(103) = PacDot103
        PacPellet(104) = PacDot104
        PacPellet(105) = PacDot105
        PacPellet(106) = PAcDot106
        PacPellet(107) = PacDot107
        PacPellet(108) = PAcDot108
        PacPellet(109) = PacDot109
        PacPellet(110) = PacDot110
        PacPellet(111) = PacDot111
        PacPellet(112) = PacDot112
        PacPellet(113) = PacDot113
        PacPellet(114) = PacDot114
        PacPellet(115) = PacDot115
        PacPellet(116) = PacDot116
        PacPellet(117) = PacDot117
        PacPellet(118) = PacDot118
        PacPellet(119) = PacDot119
        PacPellet(120) = PacDot120
        PacPellet(121) = PacDot121
        PacPellet(122) = PacDot122
        PacPellet(123) = PacDot123
        PacPellet(124) = PAcDot124
        PacPellet(125) = PACDot125
        PacPellet(126) = PacDot126
        PacPellet(127) = PaCDot127
        PacPellet(128) = PAcDot128
        PacPellet(129) = PacDot129
        PacPellet(130) = PacDot130
        PacPellet(131) = PacDot131
        PacPellet(132) = PacDot132
        PacPellet(131) = PacDot131
        PacPellet(132) = PacDot132
        PacPellet(133) = PacDot133
        PacPellet(134) = PacDot134
        PacPellet(135) = PacDot135
        PacPellet(136) = PacDot136
        PacPellet(137) = PacDot137
        PacPellet(138) = PaCDot138
        PacPellet(139) = PacDot139
        PacPellet(140) = Pacdot140
        PacPellet(141) = PacDot141
        PacPellet(142) = PAcDot142
        PacPellet(143) = PacDot143
        PacPellet(144) = PacDot144
        PacPellet(145) = PacDot145
        PacPellet(146) = PacDot146
        PacPellet(147) = PacDot147
        PacPellet(148) = Pacdot148
        PacPellet(149) = Pacdot149
        PacPellet(150) = PacDot150
        PacPellet(151) = PacDot151
        PacPellet(152) = PacDot152
        PacPellet(153) = Pacdot153
        PacPellet(154) = PacDot154
        PacPellet(155) = pacdot155
        PacPellet(156) = Pacdot156
        PacPellet(157) = PacDot157
        PacPellet(158) = PacDot158
        PacPellet(159) = Pacdot159
        PacPellet(160) = Pacdot160
        PacPellet(161) = PacDot161
        PacPellet(162) = PacDot162
        PacPellet(163) = PacDot163
        PacPellet(164) = PacDot164
        PacPellet(165) = Pacdot165
        PacPellet(166) = PacDot166
        PacPellet(167) = PacDot167
        PacPellet(168) = PacDot168
        PacPellet(169) = Pacdot169
        PacPellet(170) = PacDot170
        PacPellet(171) = PacDot171
        PacPellet(172) = PacDot172
        PacPellet(173) = PacDot173
        PacPellet(174) = PacDot174
        PacPellet(175) = PacDot175
        PacPellet(176) = PacDot176
        PacPellet(177) = PacDot177
        PacPellet(178) = PacDot178
        PacPellet(179) = PacDot179
        PacPellet(180) = PacDot180
        PacPellet(181) = Pacdot181
        PacPellet(182) = PacDot182
        PacPellet(183) = PAcDot183
        PacPellet(184) = PacDot184
        PacPellet(185) = PacDot185
        PacPellet(186) = PacDot186
        PacPellet(187) = PacDot187
        PacPellet(188) = PacDot188
        PacPellet(189) = PacDot189
        PacPellet(190) = PacDot190
        PacPellet(191) = PacDot191
        PacPellet(192) = Pacdot192
        PacPellet(193) = PacDot193
        PacPellet(194) = Pacdot194
        PacPellet(195) = PacDot195
        PacPellet(196) = PacDot196
        PacPellet(197) = Pacdot197
        PacPellet(198) = PacDot198
        PacPellet(199) = PacDot199
        PacPellet(200) = PacDot200
        PacPellet(201) = PacDot201
        PacPellet(202) = PacDot202
        PacPellet(203) = Pacdot203
        PacPellet(204) = PacDot204
        PacPellet(205) = Pacdot205
        PacPellet(206) = PacDot206
        PacPellet(207) = PacDot207
        PacPellet(208) = PacDot208
        PacPellet(209) = Pacdot209
        PacPellet(210) = Pacdot210
        PacPellet(211) = PacDot211
        PacPellet(212) = PAcdot212
        PacPellet(213) = Pacdot213
        PacPellet(214) = Pacdot214
        PacPellet(215) = PAcdot215
        PacPellet(216) = PacDot216
        PacPellet(217) = PacDot217
        PacPellet(218) = Pacdot218
        PacPellet(219) = PacDot219
        PacPellet(220) = Pacdot220
        PacPellet(221) = PacDot221
        PacPellet(222) = PacDot222
        PacPellet(223) = Pacdot223
        PacPellet(224) = Pacdot224
        PacPellet(225) = PacDot225
        PacPellet(226) = PacDot226
        PacPellet(227) = PacDot227
        PacPellet(228) = Pacdot228
        PacPellet(229) = Pacdot229
        PacPellet(230) = Pacdot230
        PacPellet(231) = PacDot231
        PacPellet(232) = PAcdot232
        PacPellet(233) = Pacdot233
        PacPellet(234) = PacDot234
        PacPellet(235) = PacDot235
        PacPellet(236) = PacDot236
        PacPellet(237) = PAcDot237
        PacPellet(238) = PaCDot238
        PacPellet(239) = PacDot239
        PacPellet(240) = PacDot240
        PacPellet(241) = PAcdot241
        PacPellet(242) = PacDot242
        PacPellet(243) = Pacdot243
        PacPellet(244) = PaCDot244
        PacPellet(245) = Pacdot245
        PacPellet(246) = PacDot246
        PacPellet(247) = PacDot247
        PacPellet(248) = PacDot248
        PacPellet(249) = Pacdot249
        PacPellet(250) = Pacdot250
        PacPellet(251) = PacDot251
        PacPellet(252) = Pacdot252
        PacPellet(253) = Pacdot253
        PacPellet(254) = PacDot254
        PacPellet(255) = PacDot255
        PacPellet(256) = Pacdot256
        PacPellet(257) = PacDot257
        PacPellet(258) = Pacdot258
        PacPellet(259) = PacDot259
        PacPellet(260) = PacDot260
        PacPellet(261) = PacDot261
        PacPellet(262) = PacDot262
        PacPellet(263) = PacDot263
        PacPellet(264) = Pacdot264
        PacPellet(265) = PacDot265
        PacPellet(266) = Pacdot266
    End Sub

    Private Sub PowerPelletCollision_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TMRPowerPelletCollision.Tick
        For i = 1 To 4 ' checks for each powerpellet
            If pbPacman.Bounds.IntersectsWith(powerpellet(i).Bounds) Then ' if there is a collkison between PacMan and a powerpellet
                If powerpellet(i).Visible = True Then ' if the powerpellet has not been collected
                    powerpellet(i).Visible = False ' the powerpellet turns invisible
                    powerpelletActive = True ' changes the value of the variable Powerpelletactive to true
                    PowerPellettime = 150 ' sets the value of powerpellettime to 150
                    TMRPowerPelletTime.Start() 'starts the timer which handles the time remain of rthe power pellets
                End If
            End If
        Next
        Label2.Text = PowerPellettime / 10 ' changes the value shown in the lable displaying the time remaining
    End Sub

    Private Sub TMRPowerPelletTime_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TMRPowerPelletTime.Tick
        'This timer handles the amount of time that power pellets stay active for, and changes the images of the ghosts when it is active. 
        If PowerPellettime = 0 Then 'if a powerpellet had run out

            powerpelletActive = False ' powerpellet active is turned on
            ' and changes the images of the frightened ghosts back to their oricional colours from am image in rousources
            Ghosts(1).Image = My.Resources.PacMan_Blinky
            Ghosts(2).Image = My.Resources.PacMan_Inky
            Ghosts(3).Image = My.Resources.PAcMan_Pinky
            Ghosts(4).Image = My.Resources.PacMan_Clyde

        ElseIf PowerPellettime > 0 Then                       '|
            PowerPellettime = PowerPellettime - 1             '|
            For ii = 1 To 4                                   '-- this checks for an active powerpellet, and if there is one, it reduces the 
                Ghosts(ii).Image = PBGhostFrightened.Image    '-- counter by 1, and changes the image of the ghosts
            Next                                              '| 
        End If
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TXTScore.KeyDown
        ' manages the players keystrokes for the movement of the player. 
        'containded within a text box as when it was maneged on the form, there was lag, due to each keypress re-rendering the whole form
        'where it only re-renders the text box 
        Select Case e.KeyCode 'e.Keycode detects for any key presses made on the keyboard. 
            Case Is = Keys.Up 'detects the Up key on the keyboard
                'Moves the player up 2 pixels, and changes the image acordingly, when the Up key is pressed. 
                KeysUp = True
                KeysDown = False
                KeysLeft = False
                KeysRight = False
                intVelocityx = 0 ' changes the players X velocity, so that pacman stopes moving in the direction it was moving
                intvelocityY = -2 ' changes the players Y Velocity so that pacMan starts moving upwards, and continues to do so, without repreatedly pressing the button 
                pbPacman.Image = PictureBox2.Image 'changes the image so that it faces the direction PacMan is moving
                PacMoving = True

            Case Is = Keys.Down  ' Detects the Down key on the keyboard
                'Moves the player Down 2 pixels, and changes the image acordingly, when the down key is pressed.
                KeysUp = False
                KeysDown = True
                KeysLeft = False
                KeysRight = False
                intVelocityx = 0 ' changes the players X velocity, so that pacman stopes moving in the direction it was moving
                intvelocityY = 2 ' changes the players Y Velocity so that pacMan starts moving downwards, and continues to do so, without repreatedly pressing the button
                pbPacman.Image = PictureBox4.Image 'changes the image so that it faces the direction PacMan is moving
                PacMoving = True

            Case Is = Keys.Left  ' detects the Left key on the keyboard
                'Moves the player left 2 pixels, and changes the image acordingly, when the left key is pressed.
                KeysUp = False
                KeysDown = False
                KeysLeft = True
                KeysRight = False
                intvelocityY = 0 ' changes the players X velocity, so that pacman stopes moving in the direction it was moving
                intVelocityx = -2 ' changes the players Y Velocity so that pacMan starts moving left, and continues to do so, without repreatedly pressing the button
                pbPacman.Image = PictureBox3.Image 'changes the image so that it faces the direction PacMan is moving
                PacMoving = True

            Case Is = Keys.Right 'detects the Right key on the keyboard
                'Moves the player right 2 pixels, and changes the image acordingly, when the right key is pressed.
                KeysUp = False
                KeysDown = False
                KeysLeft = False
                KeysRight = True
                intvelocityY = 0 ' changes the players X velocity, so that pacman stopes moving in the direction it was moving
                intVelocityx = 2 ' changes the players Y Velocity so that pacMan starts moving right, and continues to do so, without repreatedly pressing the button
                pbPacman.Image = PictureBox5.Image 'changes the image so that it faces the direction PacMan is moving
                PacMoving = True

            Case Is = Keys.Space ' detects the spacebar key ont he keyboard
                'stops the player from moving when the space bar it pressed.
                intVelocityx = 0
                intvelocityY = 0
                PacMoving = False

            Case Is = Keys.Escape
                'Closes the whole programme when the escape key is pressed. 
                Me.Close()
                PacManStartUpForm.Close()
        End Select
    End Sub

    Private Sub TXTScore_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTScore.TextChanged
        TXTScore.Text = IntPlayerScore ' changes the score outputted in the textbox as the player earns more
    End Sub


    'Private Sub TMRNextLevel_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TMRNextLevel.Tick
    '    intghost1VelocityX = intghost1VelocityX * 2
    '    intghost1Velocityy = intghost1Velocityy * 2
    '    intGhost2VelocityX = intGhost2VelocityX * 2
    '    intghost2VelocityY = intghost2VelocityY * 2
    '    intghost3velocityX = intghost3velocityX * 2
    '    intghost3velocityy = intghost3velocityy * 2
    '    intghost4VelocityX = intghost4VelocityX * 2
    '    intghost4velocityy = intghost4velocityy * 2
    'End Sub
End Class


