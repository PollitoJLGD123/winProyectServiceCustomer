namespace winProyectService
{
    partial class frmCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.btnEnviarMensaje = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboClientes = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSeleccionarImagen = new System.Windows.Forms.Button();
            this.txtRuta = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.barraProgreso = new System.Windows.Forms.ProgressBar();
            this.lblBytesEnvio = new System.Windows.Forms.Label();
            this.checkEnviado = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnEnviarImagen = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRutaEnviada = new System.Windows.Forms.RichTextBox();
            this.txtConversacion = new System.Windows.Forms.RichTextBox();
            this.lblLenght = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.txtPuerto = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtDireccion);
            this.panel1.Controls.Add(this.txtPuerto);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtConversacion);
            this.panel1.Controls.Add(this.lblLenght);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txtRutaEnviada);
            this.panel1.Controls.Add(this.btnEnviarImagen);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtRuta);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnSeleccionarImagen);
            this.panel1.Controls.Add(this.comboClientes);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnEnviarMensaje);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtMensaje);
            this.panel1.Controls.Add(this.lblCustomer);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1039, 732);
            this.panel1.TabIndex = 0;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe Script", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.Location = new System.Drawing.Point(484, 25);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(187, 55);
            this.lblCustomer.TabIndex = 1;
            this.lblCustomer.Text = "Customer";
            // 
            // btnEnviarMensaje
            // 
            this.btnEnviarMensaje.Font = new System.Drawing.Font("MV Boli", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarMensaje.Location = new System.Drawing.Point(46, 264);
            this.btnEnviarMensaje.Name = "btnEnviarMensaje";
            this.btnEnviarMensaje.Size = new System.Drawing.Size(234, 36);
            this.btnEnviarMensaje.TabIndex = 10;
            this.btnEnviarMensaje.Text = "Enviar Mensaje";
            this.btnEnviarMensaje.UseVisualStyleBackColor = true;
            this.btnEnviarMensaje.Click += new System.EventHandler(this.btnEnviarMensaje_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Script MT Bold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 34);
            this.label2.TabIndex = 9;
            this.label2.Text = "Mensaje a Enviar:";
            // 
            // txtMensaje
            // 
            this.txtMensaje.Location = new System.Drawing.Point(46, 160);
            this.txtMensaje.Multiline = true;
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.Size = new System.Drawing.Size(276, 79);
            this.txtMensaje.TabIndex = 8;
            this.txtMensaje.TextChanged += new System.EventHandler(this.txtMensaje_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Script MT Bold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(420, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 28);
            this.label6.TabIndex = 12;
            this.label6.Text = "Cliente a Enviar";
            // 
            // comboClientes
            // 
            this.comboClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboClientes.FormattingEnabled = true;
            this.comboClientes.Location = new System.Drawing.Point(425, 160);
            this.comboClientes.Name = "comboClientes";
            this.comboClientes.Size = new System.Drawing.Size(195, 24);
            this.comboClientes.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Script MT Bold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(726, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(261, 28);
            this.label5.TabIndex = 28;
            this.label5.Text = "Selecciona Archivo a Enviar";
            // 
            // btnSeleccionarImagen
            // 
            this.btnSeleccionarImagen.Font = new System.Drawing.Font("Dubai", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionarImagen.Location = new System.Drawing.Point(737, 184);
            this.btnSeleccionarImagen.Name = "btnSeleccionarImagen";
            this.btnSeleccionarImagen.Size = new System.Drawing.Size(226, 43);
            this.btnSeleccionarImagen.TabIndex = 27;
            this.btnSeleccionarImagen.Text = "Seleccionar Archivo";
            this.btnSeleccionarImagen.UseVisualStyleBackColor = true;
            // 
            // txtRuta
            // 
            this.txtRuta.Enabled = false;
            this.txtRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRuta.Location = new System.Drawing.Point(737, 326);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(270, 87);
            this.txtRuta.TabIndex = 30;
            this.txtRuta.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Script MT Bold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(732, 272);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 28);
            this.label3.TabIndex = 29;
            this.label3.Text = "Ruta Seleccionada:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.barraProgreso);
            this.groupBox1.Controls.Add(this.lblBytesEnvio);
            this.groupBox1.Controls.Add(this.checkEnviado);
            this.groupBox1.Location = new System.Drawing.Point(713, 549);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 112);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            // 
            // barraProgreso
            // 
            this.barraProgreso.Location = new System.Drawing.Point(18, 33);
            this.barraProgreso.Name = "barraProgreso";
            this.barraProgreso.Size = new System.Drawing.Size(226, 23);
            this.barraProgreso.TabIndex = 1;
            // 
            // lblBytesEnvio
            // 
            this.lblBytesEnvio.AutoSize = true;
            this.lblBytesEnvio.Location = new System.Drawing.Point(15, 78);
            this.lblBytesEnvio.Name = "lblBytesEnvio";
            this.lblBytesEnvio.Size = new System.Drawing.Size(104, 16);
            this.lblBytesEnvio.TabIndex = 23;
            this.lblBytesEnvio.Text = "Bytes Enviados:";
            // 
            // checkEnviado
            // 
            this.checkEnviado.AutoCheck = false;
            this.checkEnviado.BackColor = System.Drawing.Color.White;
            this.checkEnviado.Font = new System.Drawing.Font("Lucida Handwriting", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkEnviado.ForeColor = System.Drawing.Color.Red;
            this.checkEnviado.Location = new System.Drawing.Point(264, 33);
            this.checkEnviado.Name = "checkEnviado";
            this.checkEnviado.Size = new System.Drawing.Size(39, 23);
            this.checkEnviado.TabIndex = 8;
            this.checkEnviado.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Script MT Bold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(708, 505);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(255, 28);
            this.label4.TabIndex = 31;
            this.label4.Text = "Estado de Envio de Archivo";
            // 
            // btnEnviarImagen
            // 
            this.btnEnviarImagen.Font = new System.Drawing.Font("Dubai", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarImagen.Location = new System.Drawing.Point(777, 435);
            this.btnEnviarImagen.Name = "btnEnviarImagen";
            this.btnEnviarImagen.Size = new System.Drawing.Size(180, 44);
            this.btnEnviarImagen.TabIndex = 33;
            this.btnEnviarImagen.Text = "Enviar Archivo";
            this.btnEnviarImagen.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Script MT Bold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(385, 505);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(189, 28);
            this.label14.TabIndex = 39;
            this.label14.Text = "Archivos Recibidos:";
            // 
            // txtRutaEnviada
            // 
            this.txtRutaEnviada.Enabled = false;
            this.txtRutaEnviada.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRutaEnviada.Location = new System.Drawing.Point(390, 549);
            this.txtRutaEnviada.Name = "txtRutaEnviada";
            this.txtRutaEnviada.Size = new System.Drawing.Size(270, 84);
            this.txtRutaEnviada.TabIndex = 38;
            this.txtRutaEnviada.Text = "";
            // 
            // txtConversacion
            // 
            this.txtConversacion.Enabled = false;
            this.txtConversacion.Location = new System.Drawing.Point(46, 427);
            this.txtConversacion.Name = "txtConversacion";
            this.txtConversacion.ReadOnly = true;
            this.txtConversacion.Size = new System.Drawing.Size(276, 228);
            this.txtConversacion.TabIndex = 43;
            this.txtConversacion.Text = "";
            // 
            // lblLenght
            // 
            this.lblLenght.AutoSize = true;
            this.lblLenght.Font = new System.Drawing.Font("Gabriola", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLenght.Location = new System.Drawing.Point(124, 321);
            this.lblLenght.Name = "lblLenght";
            this.lblLenght.Size = new System.Drawing.Size(38, 42);
            this.lblLenght.TabIndex = 42;
            this.lblLenght.Text = "00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Gabriola", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(53, 326);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 37);
            this.label9.TabIndex = 41;
            this.label9.Text = "Lenght:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Script MT Bold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(40, 379);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(164, 34);
            this.label7.TabIndex = 40;
            this.label7.Text = "Conversación:";
            // 
            // label8
            // 
            this.label8.Image = global::winProyectService.Properties.Resources.service;
            this.label8.Location = new System.Drawing.Point(437, 211);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(196, 189);
            this.label8.TabIndex = 44;
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(534, 446);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.ReadOnly = true;
            this.txtDireccion.Size = new System.Drawing.Size(146, 22);
            this.txtDireccion.TabIndex = 48;
            // 
            // txtPuerto
            // 
            this.txtPuerto.Location = new System.Drawing.Point(494, 414);
            this.txtPuerto.Name = "txtPuerto";
            this.txtPuerto.ReadOnly = true;
            this.txtPuerto.Size = new System.Drawing.Size(121, 22);
            this.txtPuerto.TabIndex = 47;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Bell MT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(412, 449);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 19);
            this.label11.TabIndex = 46;
            this.label11.Text = "DIRECCIÓN IP:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bell MT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(415, 416);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 19);
            this.label10.TabIndex = 45;
            this.label10.Text = "PUERTO:";
            // 
            // frmCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1037, 730);
            this.Controls.Add(this.panel1);
            this.Name = "frmCliente";
            this.Text = "frmCliente";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCliente_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Button btnEnviarMensaje;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMensaje;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboClientes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSeleccionarImagen;
        private System.Windows.Forms.RichTextBox txtRuta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar barraProgreso;
        private System.Windows.Forms.Label lblBytesEnvio;
        private System.Windows.Forms.CheckBox checkEnviado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnEnviarImagen;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RichTextBox txtRutaEnviada;
        private System.Windows.Forms.RichTextBox txtConversacion;
        private System.Windows.Forms.Label lblLenght;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtPuerto;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
    }
}