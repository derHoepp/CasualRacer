Class GamePage
    'Private mGameControl As New GameControl

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.


    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        NavigationService.GoBack()
    End Sub
End Class
