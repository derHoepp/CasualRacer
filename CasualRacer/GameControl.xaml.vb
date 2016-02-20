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

    Protected Overrides Sub OnRender(drawingContext As DrawingContext)
        MyBase.OnRender(drawingContext)
        Dim theTrack As Track = (DataContext).Track
        Dim x, y As Long
        Dim tileWidth As Long
        Dim tileHeight As Long

        Dim dirtBrush As Brush = New SolidColorBrush(Color.FromArgb(255, 127, 51, 0))
        Dim sandBrush As Brush = New SolidColorBrush(Color.FromArgb(255, 255, 226, 147))
        Dim grasBrush As Brush = New SolidColorBrush(Color.FromArgb(255, 76, 255, 0))
        Dim roadBrush As Brush = New SolidColorBrush(Color.FromArgb(255, 128, 128, 128))
        Dim testBrush As Brush = New SolidColorBrush(Color.FromArgb(0, 200, 200, 200))
        Dim tmpPen As Pen = New Pen(testBrush, 2)
        Dim PaintingBrush As Brush



        'drawingContext.DrawRectangle(dirtBrush, Nothing, New Rect(10, 10, 100, 150))
        'drawingContext.DrawRectangle(sandBrush, Nothing, New Rect(20, 20, 100, 150))
        'drawingContext.DrawRectangle(grasBrush, Nothing, New Rect(30, 30, 100, 150))
        'drawingContext.DrawRectangle(roadBrush, Nothing, New Rect(40, 40, 100, 150))
        Dim countXTiles As Long = UBound(theTrack.Tiles, 1) + 1
        Dim countYTiles As Long = UBound(theTrack.Tiles, 2) + 1
        tileWidth = (Me.RenderSize.Width / countXTiles)
        tileHeight = (Me.RenderSize.Height / countYTiles)
        For x = LBound(theTrack.Tiles(), 1) To UBound(theTrack.Tiles(), 1)
            For y = LBound(theTrack.Tiles(), 2) To UBound(theTrack.Tiles(), 2)
                Select Case theTrack.Tiles(x, y)

                    Case TrackTile.Dirt
                        PaintingBrush = dirtBrush
                    Case TrackTile.Sand
                        PaintingBrush = sandBrush
                    Case TrackTile.Gras
                        PaintingBrush = grasBrush
                    Case TrackTile.Road
                        PaintingBrush = roadBrush
                End Select

                'Top = x * tilehight
                'Left = y * tilewidth
                'Width =
                'Height 

                'tileWidth = GameControl.WidthProperty / UBound(theTrack.Tiles, 2)
                drawingContext.DrawRectangle(PaintingBrush, tmpPen, New Rect(x * tileWidth, y * tileHeight, tileWidth, tileHeight))
            Next y
        Next x

    End Sub
End Class
