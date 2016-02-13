Public Class MainWindow
    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        NavigationFrame.Navigate(New StartPage())
    End Sub
End Class
