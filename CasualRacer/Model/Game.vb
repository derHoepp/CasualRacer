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
        UpdatePlayer(TotalTime, ElapsedTime, mPlayer1)
    End Sub

    Public Sub UpdatePlayer(ByRef TotalTime As TimeSpan, ByRef ElapsedTime As TimeSpan, thePlayer As Player)
        'Hier wird das Spiel regelmäßig geupdated

        'Lenkung
        If thePlayer.WheelLeft Then
            thePlayer.Direction = thePlayer.Direction - ElapsedTime.TotalSeconds * 100
        End If
        If thePlayer.WheelRight Then
            thePlayer.Direction += ElapsedTime.TotalSeconds * 100
        End If

        'Beschleunigung und Verzögerung



        Infotext = CInt(thePlayer.Position.X) & " / " & CInt(thePlayer.Position.Y)
        'Infotext = TileX & " / " & TileY

        'TODO Tile under Player kontrollieren
        Dim TargetSpeed As Double

        TargetSpeed = 0
        If thePlayer.Accelerate Then
            TargetSpeed += 150
        End If
        If thePlayer.Brake Then
            TargetSpeed -= 50
        End If


        TargetSpeed = TargetSpeed * mTrack.getSpeedModificatorByPosition(thePlayer.Position)
        If TargetSpeed > thePlayer.Velocity Then
            thePlayer.Velocity = Math.Min(TargetSpeed, thePlayer.Velocity + (ElapsedTime.TotalSeconds * 60)) '60 - Beschleunigung
        Else
            thePlayer.Velocity = Math.Max(TargetSpeed, thePlayer.Velocity - (ElapsedTime.TotalSeconds * 100)) '100 = Verzögerung
        End If
        'Sinus und Cosinus arbeiten nicht im Bogenmaß

        Dim DirectionBogenmass As Double = thePlayer.Direction * Math.PI / 180
        'Positionsveränderung berechnen
        Dim PositionChange As Vector = New Vector(Math.Cos(DirectionBogenmass) * thePlayer.Velocity * ElapsedTime.TotalSeconds,
                                           Math.Sin(DirectionBogenmass) * thePlayer.Velocity * ElapsedTime.TotalSeconds)
        thePlayer.Position += PositionChange
    End Sub
End Class
