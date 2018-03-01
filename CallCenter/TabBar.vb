Public Class TabBar
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.Dock = DockStyle.Top
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
    Friend WithEvents ttBotones As System.Windows.Forms.ToolTip
    Friend WithEvents pnlMover As System.Windows.Forms.Panel
    Friend WithEvents btnIzquierda As System.Windows.Forms.Button
    Friend WithEvents btnDerecha As System.Windows.Forms.Button
    Friend WithEvents imgBotones As System.Windows.Forms.ImageList
    Friend WithEvents btnFijar As System.Windows.Forms.Button
    Friend WithEvents pnlBotones As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TabBar))
        Me.ttBotones = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlMover = New System.Windows.Forms.Panel()
        Me.btnDerecha = New System.Windows.Forms.Button()
        Me.imgBotones = New System.Windows.Forms.ImageList(Me.components)
        Me.btnIzquierda = New System.Windows.Forms.Button()
        Me.btnFijar = New System.Windows.Forms.Button()
        Me.pnlBotones = New System.Windows.Forms.Panel()
        Me.pnlMover.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMover
        '
        Me.pnlMover.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnDerecha, Me.btnIzquierda})
        Me.pnlMover.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlMover.Location = New System.Drawing.Point(566, 0)
        Me.pnlMover.Name = "pnlMover"
        Me.pnlMover.Size = New System.Drawing.Size(50, 23)
        Me.pnlMover.TabIndex = 1
        Me.pnlMover.Visible = False
        '
        'btnDerecha
        '
        Me.btnDerecha.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDerecha.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDerecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDerecha.Image = CType(resources.GetObject("btnDerecha.Image"), System.Drawing.Bitmap)
        Me.btnDerecha.ImageIndex = 3
        Me.btnDerecha.ImageList = Me.imgBotones
        Me.btnDerecha.Location = New System.Drawing.Point(24, 0)
        Me.btnDerecha.Name = "btnDerecha"
        Me.btnDerecha.Size = New System.Drawing.Size(25, 23)
        Me.btnDerecha.TabIndex = 1
        '
        'imgBotones
        '
        Me.imgBotones.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.imgBotones.ImageSize = New System.Drawing.Size(16, 16)
        Me.imgBotones.ImageStream = CType(resources.GetObject("imgBotones.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgBotones.TransparentColor = System.Drawing.Color.Transparent
        '
        'btnIzquierda
        '
        Me.btnIzquierda.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnIzquierda.Enabled = False
        Me.btnIzquierda.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnIzquierda.Image = CType(resources.GetObject("btnIzquierda.Image"), System.Drawing.Bitmap)
        Me.btnIzquierda.ImageIndex = 0
        Me.btnIzquierda.ImageList = Me.imgBotones
        Me.btnIzquierda.Name = "btnIzquierda"
        Me.btnIzquierda.Size = New System.Drawing.Size(24, 23)
        Me.btnIzquierda.TabIndex = 0
        '
        'btnFijar
        '
        Me.btnFijar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnFijar.Image = CType(resources.GetObject("btnFijar.Image"), System.Drawing.Bitmap)
        Me.btnFijar.ImageIndex = 5
        Me.btnFijar.ImageList = Me.imgBotones
        Me.btnFijar.Name = "btnFijar"
        Me.btnFijar.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnFijar.Size = New System.Drawing.Size(20, 23)
        Me.btnFijar.TabIndex = 2
        Me.btnFijar.Visible = False
        '
        'pnlBotones
        '
        Me.pnlBotones.Location = New System.Drawing.Point(20, 0)
        Me.pnlBotones.Name = "pnlBotones"
        Me.pnlBotones.Size = New System.Drawing.Size(548, 23)
        Me.pnlBotones.TabIndex = 3
        '
        'TabBar
        '
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.pnlMover, Me.btnFijar, Me.pnlBotones})
        Me.Name = "TabBar"
        Me.Size = New System.Drawing.Size(616, 23)
        Me.pnlMover.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Friend WithEvents MyParent As Form

    Private _UnfocusedColor As Color = Me.BackColor
    Private _FocusedColor As Color = Color.LemonChiffon
    Private _BotonActivo As Control
    Private _AutoHide As Boolean = False


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
    Property AutoHide() As Boolean
        Get
            Return _AutoHide
        End Get
        Set(ByVal Value As Boolean)
            _AutoHide = Value
            btnFijar.Visible = Value
        End Set
    End Property

    Private Sub TabBar_ParentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ParentChanged
        MyParent = Me.ParentForm
    End Sub
    Private Sub Paret_MdiChildActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyParent.MdiChildActivate
        Me.BringToFront()
        If Not MyParent.ActiveMdiChild Is Nothing Then
            Dim Hndl As String = MyParent.ActiveMdiChild.Handle.ToString
            Dim ctrl As Control
            Dim Found As Boolean
            Dim btn As TabButton
            For Each ctrl In pnlBotones.Controls
                ctrl.BackColor = _UnfocusedColor
                If CType(ctrl, TabButton).HandledFormID = Hndl Then
                    Found = True
                    ctrl.BackColor = _FocusedColor
                    _BotonActivo = ctrl
                End If
            Next
            If Not Found Then
                btn = CreaBoton(MyParent.ActiveMdiChild.Text, Hndl)
                btn.HandledForm = MyParent.ActiveMdiChild
                pnlBotones.Controls.Add(btn)
                btn.BringToFront()
                btn.BackColor = _FocusedColor
                If 150 * pnlBotones.Controls.Count - pnlBotones.Left > Me.Width Then
                    pnlBotones.Left -= 150
                End If
            End If
        Else
            _BotonActivo = Nothing
        End If
        MakeVisible()
    End Sub
    Private Function CreaBoton(ByVal Texto As String, ByVal Handle As String) As TabButton
        Dim btn As New TabButton()
        btn.Text = Texto
        btn.HandledFormID = Handle
        btn.FocusedColor = _FocusedColor
        btn.UnfocusedColor = _UnfocusedColor
        AddHandler btn.Closed, AddressOf QuitaBoton
        AddHandler btn.TextChanged, AddressOf Boton_TextChanged
        AddHandler btn.MouseEnter, AddressOf TabBar_MouseEnter
        AddHandler btn.MouseLeave, AddressOf TabBar_MouseLeave
        pnlBotones.Width = 150 * (pnlBotones.Controls.Count + 1)
        ttBotones.SetToolTip(btn, Texto)
        Return btn
    End Function
    Private Sub QuitaBoton(ByVal sender As Object, ByVal e As System.EventArgs)
        pnlBotones.Width = 150 * (pnlBotones.Controls.Count - 1)
        If pnlBotones.Left < 0 Then
            pnlBotones.Left += 150
        End If
        MakeVisible()
    End Sub
    Private Sub Boton_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ttBotones.SetToolTip(CType(sender, Control), CType(sender, Control).Text)
    End Sub
    Private Sub On_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize, pnlBotones.Resize
        Dim nl As Integer
        pnlMover.Visible = pnlBotones.Width > Me.Width
        ActivaBotones()
        If pnlBotones.Controls.Count > 0 Then
            nl = -150 * (pnlBotones.Controls.Count - CInt(Me.Width / 150))
            pnlBotones.Left = CInt(IIf(nl < 0, nl, 0))
            MakeVisible()
        Else
            _BotonActivo = Nothing
        End If
    End Sub
    Private Sub pnlBotones_Move(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ActivaBotones()
    End Sub
    Private Sub btnIzquierda_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIzquierda.Click
        If pnlBotones.Left < 0 Then
            pnlBotones.Left += 150
        End If
    End Sub
    Private Sub btnDerecha_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDerecha.Click
        If pnlBotones.Left + pnlBotones.Width > Me.Width Then
            pnlBotones.Left -= 150
        End If
    End Sub
    Private Sub ActivaBotones()
        btnIzquierda.Enabled = pnlBotones.Left < 0
        btnDerecha.Enabled = pnlBotones.Width + pnlBotones.Left > Me.Width
        btnIzquierda.ImageIndex = CInt(IIf(btnIzquierda.Enabled, 1, 0))
        btnDerecha.ImageIndex = CInt(IIf(btnDerecha.Enabled, 3, 2))
    End Sub
    Private Sub MakeVisible()
        Dim nl As Integer
        If Not _BotonActivo Is Nothing AndAlso (_BotonActivo.Left + pnlBotones.Left > Me.Width OrElse _BotonActivo.Left + pnlBotones.Left < 0) Then
            nl = 150 * (pnlBotones.Controls.IndexOf(_BotonActivo) - CInt(Me.Width / 150))
            pnlBotones.Left = CInt(IIf(nl < 0, nl, 0))
        End If
    End Sub

    Private Sub TabBar_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.MouseLeave, btnDerecha.MouseLeave, btnIzquierda.MouseLeave, btnFijar.MouseLeave, pnlMover.MouseLeave, pnlBotones.MouseLeave
        If _AutoHide AndAlso CursorFuera() Then
            Me.Height = 5
        End If
    End Sub

    Private Sub TabBar_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.MouseEnter, btnDerecha.MouseEnter, btnIzquierda.MouseEnter, btnFijar.MouseEnter, pnlMover.MouseEnter, pnlBotones.MouseEnter
        If _AutoHide Then
            Me.Height = 23
        End If
    End Sub

    Private Function CursorFuera() As Boolean
        Dim x, y, MeX, MeY As Integer
        x = Me.PointToClient(Cursor.Position).X
        y = Me.PointToClient(Cursor.Position).Y
        MeX = (Me.Location).X
        MeY = (Me.Location).Y
        Return Not (x > MeX And x < MeX + Me.Width And y > MeY And y < MeY + Me.Height)
    End Function

    Private Sub btnFijar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFijar.Click
        If btnFijar.ImageIndex = 5 Then
            btnFijar.ImageIndex = 4
            Me.Width = 23
            _AutoHide = False
        Else
            btnFijar.ImageIndex = 5
            _AutoHide = True
        End If
    End Sub
End Class


