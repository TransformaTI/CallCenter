Public Class TabButton
    Inherits System.Windows.Forms.Button

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'TabButton
        '
        Me.Dock = System.Windows.Forms.DockStyle.Left
        Me.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Size = New System.Drawing.Size(150, 23)

    End Sub

#End Region

    Friend WithEvents _HandledForm As Form
    Private _HandledFormID As String
    Private _UnfocusedColor As Color = Me.BackColor
    Private _FocusedColor As Color = Color.LemonChiffon

    Property HandledForm() As Form
        Get
            Return _HandledForm
        End Get
        Set(ByVal Value As Form)
            _HandledForm = Value
        End Set
    End Property
    Property HandledFormID() As String
        Get
            Return _HandledFormID
        End Get
        Set(ByVal Value As String)
            _HandledFormID = Value
        End Set
    End Property
    Property UnfocusedColor() As Color
        Get
            Return _UnfocusedColor
        End Get
        Set(ByVal Value As Color)
            _UnfocusedColor = Value
        End Set
    End Property
    Property FocusedColor() As Color
        Get
            Return _FocusedColor
        End Get
        Set(ByVal Value As Color)
            _FocusedColor = Value
        End Set
    End Property

    Public Event Closed(ByVal sender As Object, ByVal e As System.EventArgs)


    Private Sub TabButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Click
        ActivaVentana()
    End Sub
    Private Sub _HandledForm_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _HandledForm.Closed
        RaiseEvent Closed(Me, Nothing)
        Me.Dispose()
    End Sub
    Public Sub ActivaVentana()
        If Not _HandledForm Is Nothing Then
            _HandledForm.Show()
            _HandledForm.Activate()
            Me.BackColor = _FocusedColor
        End If
    End Sub

    Private Sub _HandledForm_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _HandledForm.TextChanged
        Me.Text = _HandledForm.Text

    End Sub
End Class
