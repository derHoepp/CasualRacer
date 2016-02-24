Imports System.ComponentModel

Friend Class Game
    Implements INotifyPropertyChanged
    'Public Players As List(Of Player)
    Private mPlayer1 As Player

    Private mTrack As Track
    Private mInfotext As String
    Public Property Infotext() As String
        Get
            Return mInfotext
        End Get
        Set(ByVal value As String)
            If mInfotext <> value Then
                mInfotext = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Infotext)))
            End If
        End Set
    End Property

    'Es muss sich um eine Property handeln, damit diese in WPF ins Binding übernommen werden kann
    Public Property Player1() As Player
        Get
            Return mPlayer1
        End Get
        Set(ByVal value As Player)
            mPlayer1 = value
        End Set
    End Property

    Public Property Track() As Track
        Get
            Return mTrack
        End Get
        Set(ByVal value As Track)
            mTrack = value
        End Set
    End Property
    Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New()
        'Players = New List(Of Player)
        mPlayer1 = New Player()
        Track = New Track(1, 1).Load("Tracks\Track 01.txt")
        Infotext = "leer"
    End Sub

    Public Sub Update(ByRef TotalTime As TimeSpan, ByRef ElapsedTime As TimeSpan)
        'Hier wird das Spiel regelmäßig geupdated

        'Lenkung
        If mPlayer1.WheelLeft Then
            mPlayer1.Direction = mPlayer1.Direction - ElapsedTime.TotalSeconds * 100
        End If
        If mPlayer1.WheelRight Then
            mPlayer1.Direction += ElapsedTime.TotalSeconds * 100
        End If

        'Beschleunigung und Verzögerung



        Dim TileX As Integer = CInt((mPlayer1.Position.X - 10) / mTrack.TileSize.Width)
        Dim TileY As Integer = CInt((mPlayer1.Position.Y - 10) / mTrack.TileSize.Height)

        TileX = Math.Min(Math.Max(0, TileX), UBound(mTrack.Tiles, 1))
        TileY = Math.Min(Math.Max(0, TileY), UBound(mTrack.Tiles, 2))
        Dim TileUnderPlayer As TrackTile = mTrack.Tiles(TileX, TileY)
        Infotext = TileX & " / " & TileY
        Dim maxSpeed As Double
        'TODO Tile under Player kontrollieren
        Dim TargetSpeed As Double

        TargetSpeed = 0
        If mPlayer1.Accelerate Then
            TargetSpeed += 100
        End If
        If mPlayer1.Brake Then
            TargetSpeed -= 50
        End If

        'TargetSpeed = Math.Min(TargetSpeed, 500)
        Select Case TileUnderPlayer
            Case TrackTile.Dirt
                maxSpeed = TargetSpeed * 0.2
            Case TrackTile.Sand
                maxSpeed = TargetSpeed * 0.4
            Case TrackTile.Gras
                maxSpeed = TargetSpeed * 0.6
            Case Else
                maxSpeed = TargetSpeed
        End Select
        If maxSpeed < 0 Then
            mPlayer1.Velocity = Math.Min(maxSpeed, mPlayer1.Velocity + (ElapsedTime.TotalSeconds * 60))
        Else
            mPlayer1.Velocity = Math.Max(maxSpeed, mPlayer1.Velocity - (ElapsedTime.TotalSeconds * 90))
        End If
        'Sinus und Cosinus arbeiten nicht im Bogenmaß

        Dim DirectionBogenmass As Double = Player1.Direction * Math.PI / 180
        'Positionsveränderung berechnen
        Dim PositionChange As Vector = New Vector(Math.Cos(DirectionBogenmass) * Player1.Velocity * ElapsedTime.TotalSeconds,
                                           Math.Sin(DirectionBogenmass) * Player1.Velocity * ElapsedTime.TotalSeconds)
        Player1.Position += PositionChange
    End Sub
End Class
