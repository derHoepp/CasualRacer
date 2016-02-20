Friend Class Game
    'Public Players As List(Of Player)
    Private mPlayer1 As Player

    Private mTrack As Track


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


    Public Sub New()
        'Players = New List(Of Player)
        mPlayer1 = New Player()
        Track = New Track(1, 1).Load("Tracks\Track 01.txt")

    End Sub

    Public Sub Update(ByRef TotalTime As TimeSpan, ByRef ElapsedTime As TimeSpan)
        'Hier wird das Spiel regelmäßig geupdated

        'Lenkung
        If mPlayer1.WheelLeft Then
            mPlayer1.Direction -= ElapsedTime.TotalSeconds * 100
        End If
        If mPlayer1.WheelRight Then
            mPlayer1.Direction += ElapsedTime.TotalSeconds * 100
        End If

        'Beschleunigung und Verzögerung



    End Sub
End Class
