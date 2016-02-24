Imports System.IO

Public Class Track
    Private mTrackTile(,) As TrackTile
    Private mTileSize As TileSize

    Public Property TileSize() As TileSize
        Get
            Return mTileSize
        End Get
        Set(ByVal value As TileSize)
            mTileSize = value
        End Set
    End Property

    Public Sub New(width As Long, height As Long)
        ReDim mTrackTile(width, height)
        mTileSize = New TileSize(20, 20)
    End Sub

    Public Property Tiles As TrackTile(,)
        Get
            Return mTrackTile
        End Get
        Set(ByVal value As TrackTile(,))
            mTrackTile = value
        End Set
    End Property
    Public Function Load(FileName As String) As Track

        Dim lines As List(Of String) = New List(Of String)

        Using myStream As Stream = File.OpenRead(FileName)
            Using myStreamReader As New StreamReader(myStream)
                While Not myStreamReader.EndOfStream
                    lines.Add(myStreamReader.ReadLine)
                End While
            End Using
        End Using

        'TODO: Fehlerquellen beseitigen und behandeln
        Dim tmpTrack As Track = New Track(lines(0).Length - 1, lines.Count - 1)
        Dim i As Long
        Dim j As Long

        For i = 0 To lines.Count - 1
            For j = 0 To lines(i).Length - 1
                tmpTrack.Tiles(j, i) = Val(Mid(lines(i), j + 1, 1))
            Next j
        Next i

        Return tmpTrack
    End Function

End Class

Public Enum TrackTile
    Dirt = 0
    Sand = 1
    Gras = 2
    Road = 3
End Enum
Public Class TileSize
    Public Width As Double
    Public Height As Double
    Public Sub New(Width As Double, Height As Double)
        Me.Width = Width
        Me.Height = Height
    End Sub
End Class