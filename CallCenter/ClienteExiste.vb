Public Class ClienteExiste
    Inherits System.Windows.Forms.Form

    Public _Acepta As Integer

    Public Sub Entrada(ByVal Contrato As Integer)

        Try
            SqlConnection.Close()
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        _Acepta = -1
        LlenarDatos(Contrato)
        Me.ShowDialog()
    End Sub

    Private Function ValidaMascara(ByVal Longitud As Integer) As String
        Dim Mascara As String
        Dim i As Int16

        Mascara = ""

        If Longitud <= 2 Then
            Mascara = "############"
        Else
            For i = 1 To CType(Longitud, Short)
                Select Case i
                    Case 1, 2, 4, 6, 8, 10, 12
                        Mascara = Mascara + "#"
                    Case 3, 5, 7, 9, 11, 13
                        Mascara = Mascara + "-#"
                End Select
            Next
        End If

        Return (Mascara)
    End Function


    Private Sub LlenarDatos(ByVal Contrato As Integer)

        DsClienteEncontrado.Cliente.Clear()
        cmdCCliente.Parameters("@Cliente").Value = Contrato
        daCliente.Fill(DsClienteEncontrado, "Cliente")

        lbContrato.Text = DsClienteEncontrado.Cliente(0).Cliente
        'If Not IsDBNull(DsClienteEncontrado.Cliente(0).DigitoVerificador) Then
        '    lbDigito.Text = CType(DsClienteEncontrado.Cliente(0).DigitoVerificador, String)
        'End If

        lbCelula.Text = DsClienteEncontrado.Cliente(0).Siglas
        lbNombre.Text = DsClienteEncontrado.Cliente(0).Nombre
        lbCalle.Text = DsClienteEncontrado.Cliente(0).DesCalle
        If DsClienteEncontrado.Cliente(0).StatusCalle = "VERIFICADO" Then
            lbCalle.ForeColor = System.Drawing.SystemColors.WindowText
        Else
            lbCalle.ForeColor = Color.Red
        End If

        lbEntreCalle1.Text = DsClienteEncontrado.Cliente(0).DesEntreCalle1
        If DsClienteEncontrado.Cliente(0).StatusCalle1 = "VERIFICADO" Then
            lbEntreCalle1.ForeColor = System.Drawing.SystemColors.WindowText
        Else
            lbEntreCalle1.ForeColor = Color.Red
        End If

        lbEntrecalle2.Text = DsClienteEncontrado.Cliente(0).DesEntreCalle2
        If DsClienteEncontrado.Cliente(0).StatusCalle2 = "VERIFICADO" Then
            lbEntrecalle2.ForeColor = System.Drawing.SystemColors.WindowText
        Else
            lbEntrecalle2.ForeColor = Color.Red
        End If

        lbExterior.Text = CType(DsClienteEncontrado.Cliente(0).NumExterior, String)
        lbInterior.Text = DsClienteEncontrado.Cliente(0).NumInterior

        lbColonia.Text = DsClienteEncontrado.Cliente(0).DesColonia
        If DsClienteEncontrado.Cliente(0).StatusColonia = "VERIFICADO" Then
            lbColonia.ForeColor = System.Drawing.SystemColors.WindowText
        Else
            lbColonia.ForeColor = Color.Red
        End If

        lbCP.Text = DsClienteEncontrado.Cliente(0).CP

        If Len(RTrim(LTrim(DsClienteEncontrado.Cliente(0).DesMunicipio))) >= 34 Then
            lbMunicipio.Text = DsClienteEncontrado.Cliente(0).DesMunicipio.Substring(0, 34)
        Else
            lbMunicipio.Text = DsClienteEncontrado.Cliente(0).DesMunicipio.Substring(0, Len(DsClienteEncontrado.Cliente(0).DesMunicipio))
        End If

        lbTipoCliente.Text = DsClienteEncontrado.Cliente(0).DesTipoCliente
        lbClasificacion.Text = DsClienteEncontrado.Cliente(0).DesClasificacion
        lbRamoCliente.Text = DsClienteEncontrado.Cliente(0).DesRamo
        lbFormaPago.Text = DsClienteEncontrado.Cliente(0).DesFormaPago
        If Len(RTrim(LTrim(DsClienteEncontrado.Cliente(0).DesTipoCredito))) >= 16 Then
            lbTipoCredito.Text = DsClienteEncontrado.Cliente(0).DesTipoCredito.Substring(0, 16)
        Else
            lbTipoCredito.Text = DsClienteEncontrado.Cliente(0).DesTipoCredito.Substring(0, Len(DsClienteEncontrado.Cliente(0).DesTipoCredito))
        End If
        lbDiasCredito.Text = CType(DsClienteEncontrado.Cliente(0).DiasCredito, String)

        'Modificacion para quitar lo del cliente telefono de la liquidación. 
        'MAHV. 16/11/2004.

        'Dim rdrTelefonos As SqlClient.SqlDataReader
        'Dim cmdClienteTelefono As New SqlClient.SqlCommand()
        'Dim itelCasa As Integer
        'Dim itelOficina As Integer
        'cmdClienteTelefono.Connection = SqlConnection
        'cmdClienteTelefono.CommandTimeout = 30
        'cmdClienteTelefono.CommandText = "select Telefono, TipoTelefono, Extensiones from ClienteTelefono where Cliente=@Cliente Order by TipoTelefono"
        'cmdClienteTelefono.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(Contrato, Integer)
        'rdrTelefonos = cmdClienteTelefono.ExecuteReader

        'itelCasa = 0
        'itelOficina = 0

        'While rdrTelefonos.Read

        'If CType(rdrTelefonos("TipoTelefono"), Integer) = 1 And itelCasa = 1 Then
        'lbTelCasa2.Text = CType(rdrTelefonos("Telefono"), String) + " Ext. " + CType(rdrTelefonos("Extensiones"), String)
        'End If

        'If CType(rdrTelefonos("TipoTelefono"), Integer) = 1 And itelCasa = 0 Then
        'lbTelCasa1.Text = CType(rdrTelefonos("Telefono"), String) + " Ext. " + CType(rdrTelefonos("Extensiones"), String)
        'itelCasa = itelCasa + 1
        'End If

        'If CType(rdrTelefonos("TipoTelefono"), Integer) = 2 And itelOficina = 1 Then
        'lbTelOficina2.Text = CType(rdrTelefonos("Telefono"), String) + " Ext. " + CType(rdrTelefonos("Extensiones"), String)
        'End If

        'If CType(rdrTelefonos("TipoTelefono"), Integer) = 2 And itelOficina = 0 Then
        'lbTelOficina1.Text = CType(rdrTelefonos("Telefono"), String) + " Ext. " + CType(rdrTelefonos("Extensiones"), String)
        'itelOficina = itelOficina + 1
        'End If


        'If CType(rdrTelefonos("TipoTelefono"), Integer) = 3 Then
        'lbTelCelular.Text = CType(rdrTelefonos("Telefono"), String)
        'End If

        'If CType(rdrTelefonos("TipoTelefono"), Integer) = 4 Then
        'lbTelRecados.Text = CType(rdrTelefonos("Telefono"), String) + " Ext. " + CType(rdrTelefonos("Extensiones"), String)
        'End If


        'rdrTelefonos.Read()
        'End While
        'rdrTelefonos.Close()
        'cmdClienteTelefono.Dispose()

        SqlConnection.Close()
    End Sub



#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

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
    Friend WithEvents lbContrato As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents lbCelula As System.Windows.Forms.Label
    Friend WithEvents lbDigito As System.Windows.Forms.Label
    Friend WithEvents lbNombre As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents lbCalle As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbExterior As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lbInterior As System.Windows.Forms.Label
    Friend WithEvents lbEntreCalle1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbEntrecalle2 As System.Windows.Forms.Label
    Friend WithEvents lbColonia As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents lbMunicipio As System.Windows.Forms.Label
    Friend WithEvents lbCP As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents gbTelefonos As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lbTelCasa1 As System.Windows.Forms.Label
    Friend WithEvents lbTelCasa2 As System.Windows.Forms.Label
    Friend WithEvents lbTelOficina1 As System.Windows.Forms.Label
    Friend WithEvents lbTelOficina2 As System.Windows.Forms.Label
    Friend WithEvents lbTelCelular As System.Windows.Forms.Label
    Friend WithEvents lbTelRecados As System.Windows.Forms.Label
    Friend WithEvents lbDiasCredito As System.Windows.Forms.Label
    Friend WithEvents lbTipoCredito As System.Windows.Forms.Label
    Friend WithEvents lbFormaPago As System.Windows.Forms.Label
    Friend WithEvents lbRamoCliente As System.Windows.Forms.Label
    Friend WithEvents lbClasificacion As System.Windows.Forms.Label
    Friend WithEvents lbTipoCliente As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents daCliente As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdCCliente As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents DsClienteEncontrado As Sigamet.dsClienteEncontrado
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lbContrato = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lbCelula = New System.Windows.Forms.Label()
        Me.lbDigito = New System.Windows.Forms.Label()
        Me.lbNombre = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.lbCalle = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbExterior = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbInterior = New System.Windows.Forms.Label()
        Me.lbEntreCalle1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbEntrecalle2 = New System.Windows.Forms.Label()
        Me.lbColonia = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.lbMunicipio = New System.Windows.Forms.Label()
        Me.lbCP = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.gbTelefonos = New System.Windows.Forms.GroupBox()
        Me.lbTelCasa1 = New System.Windows.Forms.Label()
        Me.lbTelCasa2 = New System.Windows.Forms.Label()
        Me.lbTelOficina1 = New System.Windows.Forms.Label()
        Me.lbTelOficina2 = New System.Windows.Forms.Label()
        Me.lbTelCelular = New System.Windows.Forms.Label()
        Me.lbTelRecados = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lbDiasCredito = New System.Windows.Forms.Label()
        Me.lbTipoCredito = New System.Windows.Forms.Label()
        Me.lbFormaPago = New System.Windows.Forms.Label()
        Me.lbRamoCliente = New System.Windows.Forms.Label()
        Me.lbClasificacion = New System.Windows.Forms.Label()
        Me.lbTipoCliente = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.daCliente = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdCCliente = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.DsClienteEncontrado = New Sigamet.dsClienteEncontrado()
        Me.gbTelefonos.SuspendLayout()
        CType(Me.DsClienteEncontrado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbContrato
        '
        Me.lbContrato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbContrato.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbContrato.Location = New System.Drawing.Point(80, 16)
        Me.lbContrato.Name = "lbContrato"
        Me.lbContrato.Size = New System.Drawing.Size(120, 23)
        Me.lbContrato.TabIndex = 10
        Me.lbContrato.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(268, 18)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(42, 14)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "Celula :"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(23, 18)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(58, 14)
        Me.Label27.TabIndex = 8
        Me.Label27.Text = "Contrato : "
        '
        'lbCelula
        '
        Me.lbCelula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbCelula.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCelula.Location = New System.Drawing.Point(312, 16)
        Me.lbCelula.Name = "lbCelula"
        Me.lbCelula.Size = New System.Drawing.Size(136, 23)
        Me.lbCelula.TabIndex = 12
        Me.lbCelula.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbDigito
        '
        Me.lbDigito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbDigito.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbDigito.ForeColor = System.Drawing.Color.Red
        Me.lbDigito.Location = New System.Drawing.Point(216, 16)
        Me.lbDigito.Name = "lbDigito"
        Me.lbDigito.Size = New System.Drawing.Size(32, 23)
        Me.lbDigito.TabIndex = 11
        Me.lbDigito.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbNombre
        '
        Me.lbNombre.BackColor = System.Drawing.SystemColors.Control
        Me.lbNombre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbNombre.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNombre.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lbNombre.Location = New System.Drawing.Point(80, 48)
        Me.lbNombre.Name = "lbNombre"
        Me.lbNombre.Size = New System.Drawing.Size(368, 21)
        Me.lbNombre.TabIndex = 14
        Me.lbNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(29, 48)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(51, 14)
        Me.label1.TabIndex = 13
        Me.label1.Text = "Nombre :"
        '
        'lbCalle
        '
        Me.lbCalle.BackColor = System.Drawing.SystemColors.Control
        Me.lbCalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbCalle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCalle.Location = New System.Drawing.Point(80, 80)
        Me.lbCalle.Name = "lbCalle"
        Me.lbCalle.Size = New System.Drawing.Size(368, 21)
        Me.lbCalle.TabIndex = 16
        Me.lbCalle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(43, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 14)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Calle :"
        '
        'lbExterior
        '
        Me.lbExterior.BackColor = System.Drawing.SystemColors.Control
        Me.lbExterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbExterior.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbExterior.Location = New System.Drawing.Point(80, 104)
        Me.lbExterior.Name = "lbExterior"
        Me.lbExterior.Size = New System.Drawing.Size(128, 21)
        Me.lbExterior.TabIndex = 19
        Me.lbExterior.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(256, 104)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 14)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "# Interior :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(17, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 14)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "# Exterior :"
        '
        'lbInterior
        '
        Me.lbInterior.BackColor = System.Drawing.SystemColors.Control
        Me.lbInterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbInterior.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbInterior.Location = New System.Drawing.Point(320, 104)
        Me.lbInterior.Name = "lbInterior"
        Me.lbInterior.Size = New System.Drawing.Size(128, 21)
        Me.lbInterior.TabIndex = 20
        Me.lbInterior.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbEntreCalle1
        '
        Me.lbEntreCalle1.BackColor = System.Drawing.SystemColors.Control
        Me.lbEntreCalle1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbEntreCalle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbEntreCalle1.Location = New System.Drawing.Point(80, 128)
        Me.lbEntreCalle1.Name = "lbEntreCalle1"
        Me.lbEntreCalle1.Size = New System.Drawing.Size(176, 21)
        Me.lbEntreCalle1.TabIndex = 22
        Me.lbEntreCalle1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 134)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 14)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Entre calles :"
        '
        'lbEntrecalle2
        '
        Me.lbEntrecalle2.BackColor = System.Drawing.SystemColors.Control
        Me.lbEntrecalle2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbEntrecalle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbEntrecalle2.Location = New System.Drawing.Point(264, 128)
        Me.lbEntrecalle2.Name = "lbEntrecalle2"
        Me.lbEntrecalle2.Size = New System.Drawing.Size(184, 21)
        Me.lbEntrecalle2.TabIndex = 23
        Me.lbEntrecalle2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbColonia
        '
        Me.lbColonia.BackColor = System.Drawing.SystemColors.Control
        Me.lbColonia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbColonia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbColonia.Location = New System.Drawing.Point(80, 152)
        Me.lbColonia.Name = "lbColonia"
        Me.lbColonia.Size = New System.Drawing.Size(368, 21)
        Me.lbColonia.TabIndex = 39
        Me.lbColonia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(32, 158)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(52, 14)
        Me.Label31.TabIndex = 40
        Me.Label31.Text = "Colonia : "
        '
        'lbMunicipio
        '
        Me.lbMunicipio.BackColor = System.Drawing.SystemColors.Control
        Me.lbMunicipio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbMunicipio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbMunicipio.Location = New System.Drawing.Point(216, 176)
        Me.lbMunicipio.Name = "lbMunicipio"
        Me.lbMunicipio.Size = New System.Drawing.Size(232, 21)
        Me.lbMunicipio.TabIndex = 43
        Me.lbMunicipio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbCP
        '
        Me.lbCP.BackColor = System.Drawing.SystemColors.Control
        Me.lbCP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbCP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCP.Location = New System.Drawing.Point(80, 176)
        Me.lbCP.Name = "lbCP"
        Me.lbCP.Size = New System.Drawing.Size(64, 21)
        Me.lbCP.TabIndex = 42
        Me.lbCP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(53, 180)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(28, 14)
        Me.Label28.TabIndex = 41
        Me.Label28.Text = "CP : "
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(159, 180)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(58, 14)
        Me.Label30.TabIndex = 44
        Me.Label30.Text = "Municipio :"
        '
        'gbTelefonos
        '
        Me.gbTelefonos.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbTelCasa1, Me.lbTelCasa2, Me.lbTelOficina1, Me.lbTelOficina2, Me.lbTelCelular, Me.lbTelRecados, Me.Label10, Me.Label8, Me.Label7, Me.Label9})
        Me.gbTelefonos.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTelefonos.Location = New System.Drawing.Point(8, 200)
        Me.gbTelefonos.Name = "gbTelefonos"
        Me.gbTelefonos.Size = New System.Drawing.Size(232, 181)
        Me.gbTelefonos.TabIndex = 45
        Me.gbTelefonos.TabStop = False
        Me.gbTelefonos.Text = "Telefonos"
        '
        'lbTelCasa1
        '
        Me.lbTelCasa1.BackColor = System.Drawing.SystemColors.Control
        Me.lbTelCasa1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbTelCasa1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTelCasa1.Location = New System.Drawing.Point(72, 16)
        Me.lbTelCasa1.Name = "lbTelCasa1"
        Me.lbTelCasa1.Size = New System.Drawing.Size(152, 21)
        Me.lbTelCasa1.TabIndex = 13
        Me.lbTelCasa1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbTelCasa2
        '
        Me.lbTelCasa2.BackColor = System.Drawing.SystemColors.Control
        Me.lbTelCasa2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbTelCasa2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTelCasa2.Location = New System.Drawing.Point(72, 40)
        Me.lbTelCasa2.Name = "lbTelCasa2"
        Me.lbTelCasa2.Size = New System.Drawing.Size(152, 21)
        Me.lbTelCasa2.TabIndex = 14
        Me.lbTelCasa2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbTelOficina1
        '
        Me.lbTelOficina1.BackColor = System.Drawing.SystemColors.Control
        Me.lbTelOficina1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbTelOficina1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTelOficina1.Location = New System.Drawing.Point(72, 70)
        Me.lbTelOficina1.Name = "lbTelOficina1"
        Me.lbTelOficina1.Size = New System.Drawing.Size(152, 21)
        Me.lbTelOficina1.TabIndex = 15
        Me.lbTelOficina1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbTelOficina2
        '
        Me.lbTelOficina2.BackColor = System.Drawing.SystemColors.Control
        Me.lbTelOficina2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbTelOficina2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTelOficina2.Location = New System.Drawing.Point(72, 93)
        Me.lbTelOficina2.Name = "lbTelOficina2"
        Me.lbTelOficina2.Size = New System.Drawing.Size(152, 21)
        Me.lbTelOficina2.TabIndex = 16
        Me.lbTelOficina2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbTelCelular
        '
        Me.lbTelCelular.BackColor = System.Drawing.SystemColors.Control
        Me.lbTelCelular.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbTelCelular.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTelCelular.Location = New System.Drawing.Point(73, 125)
        Me.lbTelCelular.Name = "lbTelCelular"
        Me.lbTelCelular.Size = New System.Drawing.Size(152, 21)
        Me.lbTelCelular.TabIndex = 17
        Me.lbTelCelular.Tag = ""
        Me.lbTelCelular.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbTelRecados
        '
        Me.lbTelRecados.BackColor = System.Drawing.SystemColors.Control
        Me.lbTelRecados.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbTelRecados.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTelRecados.Location = New System.Drawing.Point(72, 152)
        Me.lbTelRecados.Name = "lbTelRecados"
        Me.lbTelRecados.Size = New System.Drawing.Size(152, 21)
        Me.lbTelRecados.TabIndex = 18
        Me.lbTelRecados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(29, 126)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(46, 14)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "Celular :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(27, 70)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 14)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Oficina :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(36, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 14)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Casa :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(22, 156)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 14)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Recados :"
        '
        'lbDiasCredito
        '
        Me.lbDiasCredito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbDiasCredito.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbDiasCredito.Location = New System.Drawing.Point(328, 344)
        Me.lbDiasCredito.Name = "lbDiasCredito"
        Me.lbDiasCredito.Size = New System.Drawing.Size(120, 24)
        Me.lbDiasCredito.TabIndex = 57
        Me.lbDiasCredito.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbTipoCredito
        '
        Me.lbTipoCredito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbTipoCredito.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTipoCredito.Location = New System.Drawing.Point(328, 316)
        Me.lbTipoCredito.Name = "lbTipoCredito"
        Me.lbTipoCredito.Size = New System.Drawing.Size(120, 24)
        Me.lbTipoCredito.TabIndex = 56
        Me.lbTipoCredito.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbFormaPago
        '
        Me.lbFormaPago.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbFormaPago.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbFormaPago.Location = New System.Drawing.Point(328, 288)
        Me.lbFormaPago.Name = "lbFormaPago"
        Me.lbFormaPago.Size = New System.Drawing.Size(120, 24)
        Me.lbFormaPago.TabIndex = 55
        Me.lbFormaPago.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbRamoCliente
        '
        Me.lbRamoCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbRamoCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbRamoCliente.Location = New System.Drawing.Point(328, 259)
        Me.lbRamoCliente.Name = "lbRamoCliente"
        Me.lbRamoCliente.Size = New System.Drawing.Size(120, 24)
        Me.lbRamoCliente.TabIndex = 54
        Me.lbRamoCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbClasificacion
        '
        Me.lbClasificacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbClasificacion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbClasificacion.Location = New System.Drawing.Point(328, 232)
        Me.lbClasificacion.Name = "lbClasificacion"
        Me.lbClasificacion.Size = New System.Drawing.Size(120, 24)
        Me.lbClasificacion.TabIndex = 53
        Me.lbClasificacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbTipoCliente
        '
        Me.lbTipoCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbTipoCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTipoCliente.Location = New System.Drawing.Point(328, 208)
        Me.lbTipoCliente.Name = "lbTipoCliente"
        Me.lbTipoCliente.Size = New System.Drawing.Size(120, 24)
        Me.lbTipoCliente.TabIndex = 52
        Me.lbTipoCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(246, 350)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(86, 14)
        Me.Label25.TabIndex = 51
        Me.Label25.Text = "Dias de credito :"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(245, 320)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(87, 14)
        Me.Label24.TabIndex = 50
        Me.Label24.Text = "Tipo de credito :"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(245, 293)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(87, 14)
        Me.Label23.TabIndex = 49
        Me.Label23.Text = "Forma de pago :"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(259, 237)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(72, 14)
        Me.Label21.TabIndex = 48
        Me.Label21.Text = "Clasificación :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(248, 208)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(85, 14)
        Me.Label16.TabIndex = 47
        Me.Label16.Text = "Tipo de cliente :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(256, 262)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(76, 14)
        Me.Label15.TabIndex = 46
        Me.Label15.Text = "Ramo cliente :"
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(464, 16)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 0
        Me.btnAceptar.Text = "Aceptar"
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(464, 48)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 1
        Me.btnCancelar.Text = "Cancelar"
        '
        'daCliente
        '
        Me.daCliente.SelectCommand = Me.cmdCCliente
        '
        'cmdCCliente
        '
        Me.cmdCCliente.CommandText = "SELECT CONVERT(VarChar(10), C.Cliente) AS Cliente, C.Celula, CE.Siglas, C.DigitoV" & _
        "erificador, C.Nombre, C.Calle, ISNULL(CA.Nombre, '') AS DesCalle, C.EntreCalle1," & _
        " ISNULL(CA1.Nombre, '') AS DesEntreCalle1, C.EntreCalle2, ISNULL(CA2.Nombre, '')" & _
        " AS DesEntreCalle2, ISNULL(C.NumExterior, 0) AS NumExterior, ISNULL(C.NumInterio" & _
        "r, '') AS NumInterior, C.Ruta, ISNULL(R.Descripcion, '') AS DesRuta, ISNULL(C.Ra" & _
        "moCliente, 6) AS RamoCliente, ISNULL(RC.Descripcion, '') AS DesRamo, ISNULL(C.St" & _
        "atus, 'INACTIVO') AS Status, C.ClasificacionCliente, ISNULL(CC.Descripcion, '') " & _
        "AS DesClasificacion, C.TipoCliente, ISNULL(TC.Descripcion, '') AS DesTipoCliente" & _
        ", C.FormaPago, ISNULL(FP.Descripcion, '') AS DesFormaPago, TCR.TipoCredito, ISNU" & _
        "LL(TCR.Descripcion, '') AS DesTipoCredito, ISNULL(C.DiasCredito, 0) AS DiasCredi" & _
        "to, ISNULL(C.FAlta, GETDATE()) AS FAlta, C.FActualizacion, ISNULL(C.StatusCalida" & _
        "d, 'NUEVO') AS StatusCalidad, C.Colonia, ISNULL(CO.Nombre, '') AS DesColonia, CO" & _
        ".Municipio, ISNULL(CO.CP, '') AS CP, M.Estado, ISNULL(M.Nombre, '') + ' (' + ISN" & _
        "ULL(E.Abreviatura, '') + ')' AS DesMunicipio, ISNULL(CA.StatusCalidad, 'VERIFICA" & _
        "DO') AS StatusCalle, ISNULL(CA1.StatusCalidad, 'VERIFICADO') AS StatusCalle1, IS" & _
        "NULL(CA2.StatusCalidad, 'VERIFICADO') AS StatusCalle2, ISNULL(CO.StatusCalidad, " & _
        "'VERIFICADO') AS StatusColonia, CASE C.TipoCredito WHEN 1 THEN 'N' ELSE 'S' END " & _
        "AS Modificable FROM Cliente C INNER JOIN Celula CE ON C.Celula = CE.Celula INNER" & _
        " JOIN Calle CA ON C.Calle = CA.Calle LEFT OUTER JOIN Calle CA1 ON C.EntreCalle1 " & _
        "= CA1.Calle LEFT OUTER JOIN Calle CA2 ON C.EntreCalle2 = CA2.Calle LEFT OUTER JO" & _
        "IN Ruta R ON C.Ruta = R.Ruta LEFT OUTER JOIN RamoCliente RC ON RC.RamoCliente = " & _
        "C.RamoCliente LEFT OUTER JOIN ClasificacionCliente CC ON CC.ClasificacionCliente" & _
        " = C.ClasificacionCliente LEFT OUTER JOIN TipoCliente TC ON TC.TipoCliente = C.T" & _
        "ipoCliente LEFT OUTER JOIN FormaPago FP ON FP.FormaPago = C.FormaPago LEFT OUTER" & _
        " JOIN TipoCredito TCR ON TCR.TipoCredito = C.TipoCredito LEFT OUTER JOIN Colonia" & _
        " CO ON CO.Colonia = C.Colonia LEFT OUTER JOIN Municipio M ON CO.Municipio = M.Mu" & _
        "nicipio LEFT OUTER JOIN Estado E ON M.Estado = E.Estado AND E.Status = 1 WHERE (" & _
        "C.Cliente = @Cliente)"
        Me.cmdCCliente.Connection = Me.SqlConnection
        Me.cmdCCliente.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Cliente", System.Data.SqlDbType.Int, 4, "Cliente"))
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=ERPMETRO;initial catalog=sigamet;persist security info=False;user id=" & _
        "SA;workstation id=DESARROLLO-4;packet size=4096"
        '
        'DsClienteEncontrado
        '
        Me.DsClienteEncontrado.DataSetName = "dsClienteEncontrado"
        Me.DsClienteEncontrado.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.DsClienteEncontrado.Namespace = "http://www.tempuri.org/dsClienteEncontrado.xsd"
        '
        'ClienteExiste
        '
        Me.AcceptButton = Me.btnAceptar
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(546, 384)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnCancelar, Me.btnAceptar, Me.lbDiasCredito, Me.lbTipoCredito, Me.lbFormaPago, Me.lbRamoCliente, Me.lbClasificacion, Me.lbTipoCliente, Me.Label25, Me.Label24, Me.Label23, Me.Label21, Me.Label16, Me.Label15, Me.Label6, Me.Label12, Me.Label30, Me.gbTelefonos, Me.lbMunicipio, Me.lbCP, Me.lbColonia, Me.lbEntreCalle1, Me.lbEntrecalle2, Me.lbExterior, Me.lbInterior, Me.lbCalle, Me.lbNombre, Me.lbContrato, Me.lbCelula, Me.lbDigito, Me.Label28, Me.Label31, Me.Label3, Me.Label5, Me.Label2, Me.label1, Me.Label27})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ClienteExiste"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Datos encontrados"
        Me.gbTelefonos.ResumeLayout(False)
        CType(Me.DsClienteEncontrado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        _Acepta = -1
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        _Acepta = 1
        Me.Close()
    End Sub

    Private Sub ClienteExiste_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        SqlConnection.Close()
    End Sub
End Class
