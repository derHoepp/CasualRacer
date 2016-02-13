Imports System.Windows.Threading

Public Class GameControl
    Friend mGame As New Game
    Private mTimer As DispatcherTimer = New DispatcherTimer()
    Private mTotalWatch As Stopwatch = New Stopwatch()
    Private mElapsedWatch As Stopwatch = New Stopwatch()
    Private mWindow As MainWindow

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        DataContext = mGame
        With mTimer
            .Interval = TimeSpan.FromMilliseconds(40)
            .IsEnabled = True
        End With
        Try
            mWindow = System.Windows.Application.Current.MainWindow
            AddHandler mWindow.KeyDown, AddressOf MainWindow_KeyDown
            AddHandler mWindow.KeyUp, AddressOf MainWindow_KeyUp
        Catch ex As Exception
            'DO nothing, adding the handler only works if called within running application.
        End Try
        AddHandler mTimer.Tick, AddressOf Timer_Tick
        mTotalWatch.Start()
        mElapsedWatch.Start()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        Dim elapsed As TimeSpan = mElapsedWatch.Elapsed
        mElapsedWatch.Restart()
        mGame.Update(mTotalWatch.Elapsed, elapsed)

    End Sub

    Private Sub MainWindow_KeyDown(sender As Object, e As KeyEventArgs)
        Select Case e.Key
            Case Key.Up
                mGame.Player1.Acceleration = True
            Case Key.Left
                mGame.Player1.WheelLeft = True
            Case Key.Right
                mGame.Player1.WheelRight = True

        End Select
    End Sub

    Private Sub MainWindow_KeyUp(sender As Object, e As KeyEventArgs)
        Select Case e.Key
            Case Key.Up
                mGame.Player1.Acceleration = False
            Case Key.Left
                mGame.Player1.WheelLeft = False
            Case Key.Right
                mGame.Player1.WheelRight = False

        End Select
    End Sub
End Class
