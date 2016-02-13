Imports System.Windows.Media.Animation

Public Class MainWindow
    Private inNavigation As Boolean = False
    Private navArgs As NavigatingCancelEventArgs

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        NavigationFrame.Navigate(New StartPage())
    End Sub

    Private Sub NavigationFrame_Navigating(sender As Object, e As NavigatingCancelEventArgs)
        If Not inNavigation Then
            navArgs = e
            e.Cancel = True

            Dim myAnimation As DoubleAnimation = New DoubleAnimation
            myAnimation.From = 1.0F
            myAnimation.To = 0F
            myAnimation.Duration = New Duration(TimeSpan.FromMilliseconds(200))
            AddHandler myAnimation.Completed, AddressOf myAnimation_Completed
            NavigationFrame.BeginAnimation(OpacityProperty, myAnimation)
            inNavigation = True
        End If

    End Sub

    Private Sub myAnimation_Completed(sender As Object, e As EventArgs)


        Select Case navArgs.NavigationMode
            Case NavigationMode.[New]
                If IsNothing(navArgs.Uri) Then
                    NavigationFrame.Navigate(navArgs.Content)
                Else
                    NavigationFrame.Navigate(navArgs.Uri)
                End If
            Case NavigationMode.Back
                NavigationFrame.GoBack()
            Case NavigationMode.Forward
                NavigationFrame.GoForward()
            Case NavigationMode.Refresh
                NavigationFrame.Refresh()
        End Select
        inNavigation = False
        Dim myAnimation As DoubleAnimation = New DoubleAnimation
        myAnimation.From = 0F
        myAnimation.To = 1.0F
        myAnimation.Duration = New Duration(TimeSpan.FromMilliseconds(200))
        NavigationFrame.BeginAnimation(OpacityProperty, myAnimation)

    End Sub
End Class
