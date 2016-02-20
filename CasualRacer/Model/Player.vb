Imports System.ComponentModel

Friend Class Player
    Implements INotifyPropertyChanged

    Private Const TURN_TIME_P_SECOND As Long = 100
    'Private Members for Properties

    Private mDirection As Double
    Private mPosition As Vector
    Private mAcceleration As Boolean
    Private mWheelLeft As Boolean
    Private mWheelRight As Boolean

    Public Sub New()
        mPosition = New Vector(0, 0)
        mDirection = 0
    End Sub


    Public Property Position() As Vector
        Get
            Return mPosition
        End Get
        Set(ByVal value As Vector)
            If mPosition <> value Then
                mPosition = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Position)))
            End If
        End Set
    End Property

    Public Property Direction() As Double
        Get
            Return mDirection
        End Get
        Set(ByVal value As Double)
            If mDirection <> value Then
                mDirection = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Direction)))
            End If
        End Set
    End Property


    Public Property Acceleration() As Boolean
        Get
            Return mAcceleration
        End Get
        Set(ByVal value As Boolean)
            mAcceleration = value
        End Set
    End Property
    'wheel left
    'wheel right


    Public Property WheelLeft() As Boolean
        Get
            Return mWheelLeft
        End Get
        Set(ByVal value As Boolean)
            mWheelLeft = value
        End Set
    End Property


    Public Property WheelRight() As Boolean
        Get
            Return mWheelRight
        End Get
        Set(ByVal value As Boolean)
            mWheelRight = value
        End Set
    End Property


    Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged



    Public Sub Update(ByRef TotalTime As TimeSpan, ByRef ElapsedTime As TimeSpan)
        If mWheelLeft Then
            Me.Direction -= ElapsedTime.TotalSeconds * TURN_TIME_P_SECOND
        End If
        If mWheelRight Then
            Me.Direction += ElapsedTime.TotalSeconds * TURN_TIME_P_SECOND
        End If

    End Sub

End Class
