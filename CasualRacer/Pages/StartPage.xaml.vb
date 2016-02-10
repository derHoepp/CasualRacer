Class StartPage
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        NavigationService.Navigate(New GamePage())
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        NavigationService.Navigate(New OptionPage())
    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        Application.Current.MainWindow.Close()
    End Sub
End Class
