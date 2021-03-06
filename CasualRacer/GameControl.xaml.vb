﻿Imports System.Windows.Threading

Public Class GameControl
    Friend mGame As New Game
    'Private mTimer As DispatcherTimer = New DispatcherTimer() No longer Needed
    Private mTotalWatch As Stopwatch = New Stopwatch()
    Private mElapsedWatch As Stopwatch = New Stopwatch()
    Private mWindow As MainWindow

    Private dirtBrush As ImageBrush
    Private sandBrush As ImageBrush
    Private grasBrush As ImageBrush
    Private roadBrush As ImageBrush
    Private tilesBrush As ImageBrush
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
        Dim path As String = System.IO.Path.Combine(Environment.CurrentDirectory, "Assets", "Sprites", "Roads")


        Dim DirtImageSource As ImageSource = New BitmapImage(New Uri(path & "\Dirt_Center.png"))
        Dim SandImageSource As ImageSource = New BitmapImage(New Uri(path & "\Sand_Center.png"))
        Dim RoadImageSource As ImageSource = New BitmapImage(New Uri(path & "\Road_Center.png"))
        Dim GrasImageSource As ImageSource = New BitmapImage(New Uri(path & "\Gras_Center.png"))
        Dim TilesImageSource As ImageSource = New BitmapImage(New Uri(path & "\tiles.png"))

        dirtBrush = New ImageBrush(DirtImageSource)
        sandBrush = New ImageBrush(SandImageSource)
        grasBrush = New ImageBrush(GrasImageSource)
        roadBrush = New ImageBrush(RoadImageSource)
        tilesBrush = New ImageBrush(TilesImageSource)

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

        Dim fallBackBrush As Brush = New SolidColorBrush(Color.FromArgb(0, 200, 200, 200))


        Dim tmpPen As Pen = New Pen(fallBackBrush, 2)
        Dim PaintingBrush As Brush

        Dim countXTiles As Long = UBound(theTrack.Tiles, 1) + 1
        Dim countYTiles As Long = UBound(theTrack.Tiles, 2) + 1




        tileWidth = (Me.RenderSize.Width / countXTiles)
        tileHeight = (Me.RenderSize.Height / countYTiles)

        theTrack.TileSize = New TileSize(tileWidth, tileHeight)

        tilesBrush.TileMode = TileMode.Tile
        'tilesBrush.Stretch
        tilesBrush.Viewport = New Rect(0, 0, 1 / theTrack.Tiles.GetLength(0), 1 / theTrack.Tiles.GetLength(1))
        tilesBrush.ViewportUnits = BrushMappingMode.RelativeToBoundingBox
        tilesBrush.Viewbox = New Rect(1820, 0, 128, 128) 'Rect(1299, 649, 128, 128)
        tilesBrush.ViewboxUnits = BrushMappingMode.Absolute



        drawingContext.DrawRectangle(tilesBrush, tmpPen, New Rect(0, 0, theTrack.Width, theTrack.Height))

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
                        PaintingBrush = fallBackBrush
                End Select

                'drawingContext.DrawRectangle(PaintingBrush, tmpPen, New Rect(x * theTrack.TileSize.Width,
                '                                                             y * theTrack.TileSize.Height,
                '                                                             theTrack.TileSize.Width,
                '                                                             theTrack.TileSize.Height))
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
