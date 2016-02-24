Imports System.Windows.Threading

Public Class GameControl
    Friend mGame As New Game
    'Private mTimer As DispatcherTimer = New DispatcherTimer() No longer Needed
    Private mTotalWatch As Stopwatch = New Stopwatch()
    Private mElapsedWatch As Stopwatch = New Stopwatch()
    Private mWindow As MainWindow

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        DataContext = mGame

        'Previously the Trigger was a timer. Now it is the Rendering event of the Gamecontrol
        'With mTimer
        '    .Interval = TimeSpan.FromMilliseconds(40)
        '    .IsEnabled = True
        'End With

        'AddHandler mTimer.Tick, AddressOf Timer_Tick

        'Handler fürs Rendering-Event einbauen
        AddHandler CompositionTarget.Rendering, AddressOf OnRendering

        'Handler für die KeyDown und Keyup-Events abfangen
        Try
            mWindow = System.Windows.Application.Current.MainWindow
            AddHandler mWindow.KeyDown, AddressOf MainWindow_KeyDown
            AddHandler mWindow.KeyUp, AddressOf MainWindow_KeyUp
        Catch ex As Exception
            'DO nothing, adding the handler only works if called within running application.
        End Try

        mTotalWatch.Start()
        mElapsedWatch.Start()
    End Sub

    'No longer in use, trigger now with onRendering
    'Private Sub Timer_Tick(sender As Object, e As EventArgs)
    '    Dim elapsed As TimeSpan = mElapsedWatch.Elapsed
    '    mElapsedWatch.Restart()
    '    mGame.Update(mTotalWatch.Elapsed, elapsed)

    'End Sub

    Private Sub OnRendering(sender As Object, e As EventArgs)
        Dim elapsed As TimeSpan = mElapsedWatch.Elapsed
        mElapsedWatch.Restart()
        mGame.Update(mTotalWatch.Elapsed, elapsed)
    End Sub

    Private Sub MainWindow_KeyDown(sender As Object, e As KeyEventArgs)
        Select Case e.Key
            Case Key.Up
                mGame.Player1.Accelerate = True
            Case Key.Down
                mGame.Player1.Brake = True
            Case Key.Left
                mGame.Player1.WheelLeft = True
            Case Key.Right
                mGame.Player1.WheelRight = True

        End Select
    End Sub

    Private Sub MainWindow_KeyUp(sender As Object, e As KeyEventArgs)
        Select Case e.Key
            Case Key.Up
                mGame.Player1.Accelerate = False
            Case Key.Down
                mGame.Player1.Brake = False
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

        'Dim dirtBrush As Brush = New SolidColorBrush(Color.FromArgb(255, 127, 51, 0))
        'Dim sandBrush As Brush = New SolidColorBrush(Color.FromArgb(255, 255, 226, 147))
        'Dim grasBrush As Brush = New SolidColorBrush(Color.FromArgb(255, 76, 255, 0))
        'Dim roadBrush As Brush = New SolidColorBrush(Color.FromArgb(255, 128, 128, 128))
        Dim testBrush As Brush = New SolidColorBrush(Color.FromArgb(0, 200, 200, 200))

        Dim path As String = System.IO.Path.Combine(Environment.CurrentDirectory, "Assets", "Sprites", "Roads")


        Dim DirtImageSource As ImageSource = New BitmapImage(New Uri(path & "\Dirt_Center.png"))
        Dim SandImageSource As ImageSource = New BitmapImage(New Uri(path & "\Sand_Center.png"))
        Dim RoadImageSource As ImageSource = New BitmapImage(New Uri(path & "\Road_Center.png"))
        Dim GrasImageSource As ImageSource = New BitmapImage(New Uri(path & "\Gras_Center.png"))

        Dim dirtBrush As ImageBrush = New ImageBrush(DirtImageSource)
        Dim sandBrush As ImageBrush = New ImageBrush(SandImageSource)
        Dim grasBrush As ImageBrush = New ImageBrush(GrasImageSource)
        Dim RoadBrush As ImageBrush = New ImageBrush(RoadImageSource)




        Dim tmpPen As Pen = New Pen(testBrush, 2)
        Dim PaintingBrush As Brush

        Dim countXTiles As Long = UBound(theTrack.Tiles, 1) + 1
        Dim countYTiles As Long = UBound(theTrack.Tiles, 2) + 1


        tileWidth = (Me.RenderSize.Width / countXTiles)
        tileHeight = (Me.RenderSize.Height / countYTiles)
        'dirtBrush.TileMode = TileMode.Tile

        'dirtBrush.Viewbox = New Rect(0, 0, tileWidth, tileHeight)

        theTrack.TileSize = New TileSize(tileWidth, tileHeight)

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
                    Case Else
                        PaintingBrush = testBrush
                End Select


                drawingContext.DrawRectangle(PaintingBrush, tmpPen, New Rect(x * theTrack.TileSize.Width,
                                                                             y * theTrack.TileSize.Height,
                                                                             theTrack.TileSize.Width,
                                                                             theTrack.TileSize.Height))
            Next y
        Next x

    End Sub

    Private Sub GameControl_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded

        RemoveHandler CompositionTarget.Rendering, AddressOf OnRendering

        Try 'Try-Block benötigt, da der Compiler sonst nicht funktioniert.
            RemoveHandler mWindow.KeyDown, AddressOf MainWindow_KeyDown
            RemoveHandler mWindow.KeyUp, AddressOf MainWindow_KeyUp
        Catch ex As Exception
            'No exception needed
        End Try
    End Sub
End Class
