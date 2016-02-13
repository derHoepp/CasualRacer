Friend Class Game
    'Public Players As List(Of Player)
    Private mPlayer1 As Player

    'Es muss sich um eine Property handeln, damit diese in WPF ins Binding übernommen werden kann
    Public Property Player1() As Player
        Get
            Return mPlayer1
        End Get
        Set(ByVal value As Player)
            mPlayer1 = value
        End Set
    End Property
    Public Sub New()
        'Players = New List(Of Player)
        mPlayer1 = New Player()
    End Sub

    Public Sub Update(ByRef TotalTime As TimeSpan, ByRef ElapsedTime As TimeSpan)
        'Hier wird das Spiel regelmäßig geupdated
        mPlayer1.Update(TotalTime, ElapsedTime)
    End Sub
End Class
