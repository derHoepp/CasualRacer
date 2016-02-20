Class TestPage
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        NavigationService.GoBack()
    End Sub

    Private Sub Button1_Click(sender As Object, e As RoutedEventArgs)
        Dim t As New Track(3, 5)
        Dim ttt(0 To 3, 0 To 5) As TrackTile
        Dim i As Long
        Dim j As Long

        For i = 0 To 3
            For j = 0 To 5
                ttt(i, j) = i
            Next j
        Next i
        t.Tiles = ttt
        MessageBox.Show("Tracktile 2,3 ist: " & t.Tiles(2, 3))
    End Sub
End Class
