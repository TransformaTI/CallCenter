Imports System.Data

Public Class frmSeleccionAutotanque
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "
    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents lstVwAutotanques As System.Windows.Forms.ListView
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSeleccionAutotanque))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.DataGrid1 = New System.Windows.Forms.DataGrid()
        Me.lstVwAutotanques = New System.Windows.Forms.ListView()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'DataGrid1
        '
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(196, 52)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.TabIndex = 0
        '
        'lstVwAutotanques
        '
        Me.lstVwAutotanques.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstVwAutotanques.FullRowSelect = True
        Me.lstVwAutotanques.HideSelection = False
        Me.lstVwAutotanques.MultiSelect = False
        Me.lstVwAutotanques.Name = "lstVwAutotanques"
        Me.lstVwAutotanques.Size = New System.Drawing.Size(367, 216)
        Me.lstVwAutotanques.TabIndex = 0
        '
        'frmSeleccionAutotanque
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(367, 216)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lstVwAutotanques})
        Me.Name = "frmSeleccionAutotanque"
        Me.Text = "Selección de autotanque"
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public listaAutotanqes As DataTable

    Public Sub New(ByVal ListaAutotanques As DataTable)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

        listaAutotanqes = ListaAutotanques

        Dim i As DataRow
        lstVwAutotanques.Clear()        
        lstVwAutotanques.BeginUpdate()
        lstVwAutotanques.LargeImageList = ImageList1
        lstVwAutotanques.SmallImageList = ImageList1()
        
        For Each i In listaAutotanqes.Rows
            If i(1) Is DBNull.Value Then
                Dim item As New ListViewItem()
                item.ImageIndex = 0
                item.SubItems.Add(CType(i(0), String))
                item.SubItems.Add("NULL")
                lstVwAutotanques.Items.Add(item)
            Else
                Dim item As New ListViewItem()
                item.ImageIndex = 0
                item.SubItems.Add(CType(i(0), String))
                item.SubItems.Add(CType(i(1), String))
                lstVwAutotanques.Items.Add(item)
            End If
        Next
        lstVwAutotanques.Columns.Add("Autotanque", 70, HorizontalAlignment.Left)
        lstVwAutotanques.Columns.Add("NumeroAutotanque", 150, HorizontalAlignment.Left)
        lstVwAutotanques.Columns.Add("Boletin", 150, HorizontalAlignment.Left)
        lstVwAutotanques.View = View.Details
        lstVwAutotanques.EndUpdate()
    End Sub

    Private Sub lstVwAutotanques_DoubleClick(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles lstVwAutotanques.DoubleClick
        Dim seleccion As String
        Dim item As ListViewItem
        item = lstVwAutotanques.SelectedItems(0)
        seleccion = item.SubItems(1).Text
        Me.Tag() = seleccion
        Me.Close()
    End Sub
End Class
