Public Class Track
    Private mTrackTile(,) As TrackTile

    Public Sub New(width As Long, height As Long)
        ReDim mTrackTile(width, height)
    End Sub

    Public Property Tiles As TrackTile(,)
        Get
            Return mTrackTile
        End Get
        Set(ByVal value As TrackTile(,))
            mTrackTile = value
        End Set
    End Property
End Class

Public Enum TrackTile
    Dirt
    Sand
    Gras
    Road
End Enum