Public Class ClienteLiquidacion
    Inherits System.Windows.Forms.Form

    Public _Acepta As Integer

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents lbMunicipio As System.Windows.Forms.Label
    Friend WithEvents lbCP As System.Windows.Forms.Label
    Friend WithEvents lbColonia As System.Windows.Forms.Label
    Friend WithEvents lbEntreCalle1 As System.Windows.Forms.Label
    Friend WithEvents lbEntrecalle2 As System.Windows.Forms.Label
    Friend WithEvents lbExterior As System.Windows.Forms.Label
    Friend WithEvents lbInterior As System.Windows.Forms.Label
    Friend WithEvents lbCalle As System.Windows.Forms.Label
    Friend WithEvents lbNombre As System.Windows.Forms.Label
    Friend WithEvents lbContrato As System.Windows.Forms.Label
    Friend WithEvents lbCelula As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lbRuta As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lbMunicipio = New System.Windows.Forms.Label()
        Me.lbCP = New System.Windows.Forms.Label()
        Me.lbColonia = New System.Windows.Forms.Label()
        Me.lbEntreCalle1 = New System.Windows.Forms.Label()
        Me.lbEntrecalle2 = New System.Windows.Forms.Label()
        Me.lbExterior = New System.Windows.Forms.Label()
        Me.lbInterior = New System.Windows.Forms.Label()
        Me.lbCalle = New System.Windows.Forms.Label()
        Me.lbNombre = New System.Windows.Forms.Label()
        Me.lbContrato = New System.Windows.Forms.Label()
        Me.lbCelula = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbRuta = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(452, 48)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 46
        Me.btnCancelar.Text = "Cancelar"
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(452, 16)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 45
        Me.btnAceptar.Text = "Aceptar"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(248, 104)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 14)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "# Interior :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(207, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(42, 14)
        Me.Label12.TabIndex = 48
        Me.Label12.Text = "Celula :"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(152, 184)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(58, 14)
        Me.Label30.TabIndex = 68
        Me.Label30.Text = "Municipio :"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(44, 184)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(28, 14)
        Me.Label28.TabIndex = 65
        Me.Label28.Text = "CP : "
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(20, 160)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(52, 14)
        Me.Label31.TabIndex = 64
        Me.Label31.Text = "Colonia : "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 14)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "Entre calles :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 14)
        Me.Label5.TabIndex = 56
        Me.Label5.Text = "# Exterior :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(28, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 14)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Calle :"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(20, 48)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(51, 14)
        Me.label1.TabIndex = 52
        Me.label1.Text = "Nombre :"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(12, 16)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(58, 14)
        Me.Label27.TabIndex = 47
        Me.Label27.Text = "Contrato : "
        '
        'lbMunicipio
        '
        Me.lbMunicipio.BackColor = System.Drawing.SystemColors.Control
        Me.lbMunicipio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbMunicipio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbMunicipio.Location = New System.Drawing.Point(208, 176)
        Me.lbMunicipio.Name = "lbMunicipio"
        Me.lbMunicipio.Size = New System.Drawing.Size(232, 21)
        Me.lbMunicipio.TabIndex = 67
        Me.lbMunicipio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbCP
        '
        Me.lbCP.BackColor = System.Drawing.SystemColors.Control
        Me.lbCP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbCP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCP.Location = New System.Drawing.Point(72, 176)
        Me.lbCP.Name = "lbCP"
        Me.lbCP.Size = New System.Drawing.Size(64, 21)
        Me.lbCP.TabIndex = 66
        Me.lbCP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbColonia
        '
        Me.lbColonia.BackColor = System.Drawing.SystemColors.Control
        Me.lbColonia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbColonia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbColonia.ForeColor = System.Drawing.Color.Blue
        Me.lbColonia.Location = New System.Drawing.Point(72, 152)
        Me.lbColonia.Name = "lbColonia"
        Me.lbColonia.Size = New System.Drawing.Size(368, 21)
        Me.lbColonia.TabIndex = 63
        Me.lbColonia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbEntreCalle1
        '
        Me.lbEntreCalle1.BackColor = System.Drawing.SystemColors.Control
        Me.lbEntreCalle1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbEntreCalle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbEntreCalle1.Location = New System.Drawing.Point(72, 128)
        Me.lbEntreCalle1.Name = "lbEntreCalle1"
        Me.lbEntreCalle1.Size = New System.Drawing.Size(176, 21)
        Me.lbEntreCalle1.TabIndex = 61
        Me.lbEntreCalle1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbEntrecalle2
        '
        Me.lbEntrecalle2.BackColor = System.Drawing.SystemColors.Control
        Me.lbEntrecalle2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbEntrecalle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbEntrecalle2.Location = New System.Drawing.Point(256, 128)
        Me.lbEntrecalle2.Name = "lbEntrecalle2"
        Me.lbEntrecalle2.Size = New System.Drawing.Size(184, 21)
        Me.lbEntrecalle2.TabIndex = 62
        Me.lbEntrecalle2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbExterior
        '
        Me.lbExterior.BackColor = System.Drawing.SystemColors.Control
        Me.lbExterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbExterior.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbExterior.Location = New System.Drawing.Point(72, 104)
        Me.lbExterior.Name = "lbExterior"
        Me.lbExterior.Size = New System.Drawing.Size(128, 21)
        Me.lbExterior.TabIndex = 58
        Me.lbExterior.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbInterior
        '
        Me.lbInterior.BackColor = System.Drawing.SystemColors.Control
        Me.lbInterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbInterior.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbInterior.Location = New System.Drawing.Point(312, 104)
        Me.lbInterior.Name = "lbInterior"
        Me.lbInterior.Size = New System.Drawing.Size(128, 21)
        Me.lbInterior.TabIndex = 59
        Me.lbInterior.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbCalle
        '
        Me.lbCalle.BackColor = System.Drawing.SystemColors.Control
        Me.lbCalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbCalle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCalle.ForeColor = System.Drawing.Color.Blue
        Me.lbCalle.Location = New System.Drawing.Point(72, 80)
        Me.lbCalle.Name = "lbCalle"
        Me.lbCalle.Size = New System.Drawing.Size(368, 21)
        Me.lbCalle.TabIndex = 55
        Me.lbCalle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbNombre
        '
        Me.lbNombre.BackColor = System.Drawing.SystemColors.Control
        Me.lbNombre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbNombre.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNombre.ForeColor = System.Drawing.Color.Blue
        Me.lbNombre.Location = New System.Drawing.Point(72, 48)
        Me.lbNombre.Name = "lbNombre"
        Me.lbNombre.Size = New System.Drawing.Size(368, 21)
        Me.lbNombre.TabIndex = 53
        Me.lbNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbContrato
        '
        Me.lbContrato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbContrato.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbContrato.Location = New System.Drawing.Point(72, 16)
        Me.lbContrato.Name = "lbContrato"
        Me.lbContrato.Size = New System.Drawing.Size(120, 24)
        Me.lbContrato.TabIndex = 49
        Me.lbContrato.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbCelula
        '
        Me.lbCelula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbCelula.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCelula.ForeColor = System.Drawing.Color.Red
        Me.lbCelula.Location = New System.Drawing.Point(255, 16)
        Me.lbCelula.Name = "lbCelula"
        Me.lbCelula.Size = New System.Drawing.Size(52, 23)
        Me.lbCelula.TabIndex = 51
        Me.lbCelula.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(324, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 14)
        Me.Label4.TabIndex = 69
        Me.Label4.Text = "Ruta :"
        '
        'lbRuta
        '
        Me.lbRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbRuta.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbRuta.ForeColor = System.Drawing.Color.Red
        Me.lbRuta.Location = New System.Drawing.Point(364, 16)
        Me.lbRuta.Name = "lbRuta"
        Me.lbRuta.Size = New System.Drawing.Size(72, 23)
        Me.lbRuta.TabIndex = 70
        Me.lbRuta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ClienteLiquidacion
        '
        Me.AcceptButton = Me.btnAceptar
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(544, 206)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbMunicipio, Me.lbEntreCalle1, Me.Label4, Me.lbRuta, Me.btnCancelar, Me.btnAceptar, Me.Label6, Me.Label12, Me.Label30, Me.Label28, Me.Label31, Me.Label3, Me.Label5, Me.Label2, Me.label1, Me.Label27, Me.lbCP, Me.lbColonia, Me.lbEntrecalle2, Me.lbExterior, Me.lbInterior, Me.lbCalle, Me.lbNombre, Me.lbContrato, Me.lbCelula})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ClienteLiquidacion"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Datos del cliente"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub Entrada(ByVal Contrato As Integer, ByVal sqlconnection As SqlClient.SqlConnection)






        Dim cmdCliente As New SqlClient.SqlCommand()
        cmdCliente.Connection = sqlconnection
        cmdCliente.CommandText = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                 " Select Cliente, Celula, Ruta, Nombre, CalleNombre, NumExterior, IsNull(NumInterior,'') as NumInterior, IsNull(Entrecalle1Nombre,'') as Entrecalle1Nombre, " & _
                                 " IsNull(Entrecalle2Nombre,'') as Entrecalle2Nombre, ColoniaNombre, IsNull(CP,'') as CP, MunicipioNombre " & _
                                 "  from vwDatosCliente where Cliente=@Cliente " & _
                                 " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "

        cmdCliente.CommandType = CommandType.Text
        cmdCliente.Parameters.Add("@Cliente", SqlDbType.Int).Value = Contrato
        cmdCliente.Connection = sqlconnection

            
        Dim rdrLiquidacion As SqlClient.SqlDataReader
        rdrLiquidacion = cmdCliente.ExecuteReader
        rdrLiquidacion.Read()
        lbContrato.Text = CType(rdrLiquidacion("Cliente"), String)
        lbCelula.Text = CType(rdrLiquidacion("Celula"), String)
        lbRuta.Text = CType(rdrLiquidacion("Ruta"), String)
        lbNombre.Text = CType(rdrLiquidacion("Nombre"), String)
        lbCalle.Text = CType(rdrLiquidacion("CalleNombre"), String)
        lbExterior.Text = CType(rdrLiquidacion("NumExterior"), String)
        lbInterior.Text = CType(rdrLiquidacion("NumInterior"), String)
        lbEntreCalle1.Text = CType(rdrLiquidacion("EntreCalle1Nombre"), String)
        lbEntrecalle2.Text = CType(rdrLiquidacion("EntreCalle2Nombre"), String)
        lbColonia.Text = CType(rdrLiquidacion("ColoniaNombre"), String)
        lbCP.Text = CType(rdrLiquidacion("CP"), String)
        lbMunicipio.Text = CType(rdrLiquidacion("MunicipioNombre"), String)

        rdrLiquidacion.Close()
        cmdCliente.Dispose()

        _Acepta = -1
        Me.ShowDialog()

    End Sub


    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        _Acepta = -1
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        _Acepta = 1
        'Error al 
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class
