﻿namespace winProyectService
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
            this.txtRutaEnviada = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.barraRecibir = new System.Windows.Forms.ProgressBar();
            this.checkRecibir = new System.Windows.Forms.CheckBox();
            this.lblBytesConstruccion = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.txtPuerto = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtConversacion = new System.Windows.Forms.RichTextBox();
            this.lblLenght = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnEnviarImagen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.barraProgreso = new System.Windows.Forms.ProgressBar();
            this.lblBytesEnvio = new System.Windows.Forms.Label();
            this.checkEnviado = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSeleccionarImagen = new System.Windows.Forms.Button();
            this.comboClientes = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnEnviarMensaje = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtRutaEnviada);
            this.panel1.Controls.Add(this.groupBox3);
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
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(806, 595);
            this.panel1.TabIndex = 0;
            // 
            // txtRutaEnviada
            // 
            this.txtRutaEnviada.Enabled = false;
            this.txtRutaEnviada.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRutaEnviada.Location = new System.Drawing.Point(297, 524);
            this.txtRutaEnviada.Margin = new System.Windows.Forms.Padding(2);
            this.txtRutaEnviada.Name = "txtRutaEnviada";
            this.txtRutaEnviada.Size = new System.Drawing.Size(240, 51);
            this.txtRutaEnviada.TabIndex = 50;
            this.txtRutaEnviada.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.barraRecibir);
            this.groupBox3.Controls.Add(this.checkRecibir);
            this.groupBox3.Controls.Add(this.lblBytesConstruccion);
            this.groupBox3.Location = new System.Drawing.Point(297, 418);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(238, 91);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            // 
            // barraRecibir
            // 
            this.barraRecibir.Location = new System.Drawing.Point(17, 22);
            this.barraRecibir.Margin = new System.Windows.Forms.Padding(2);
            this.barraRecibir.Name = "barraRecibir";
            this.barraRecibir.Size = new System.Drawing.Size(170, 19);
            this.barraRecibir.TabIndex = 24;
            // 
            // checkRecibir
            // 
            this.checkRecibir.AutoCheck = false;
            this.checkRecibir.AutoSize = true;
            this.checkRecibir.BackColor = System.Drawing.Color.White;
            this.checkRecibir.Font = new System.Drawing.Font("Lucida Handwriting", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkRecibir.ForeColor = System.Drawing.Color.Red;
            this.checkRecibir.Location = new System.Drawing.Point(200, 24);
            this.checkRecibir.Margin = new System.Windows.Forms.Padding(2);
            this.checkRecibir.Name = "checkRecibir";
            this.checkRecibir.Size = new System.Drawing.Size(15, 14);
            this.checkRecibir.TabIndex = 26;
            this.checkRecibir.UseVisualStyleBackColor = false;
            // 
            // lblBytesConstruccion
            // 
            this.lblBytesConstruccion.AutoSize = true;
            this.lblBytesConstruccion.Location = new System.Drawing.Point(15, 57);
            this.lblBytesConstruccion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBytesConstruccion.Name = "lblBytesConstruccion";
            this.lblBytesConstruccion.Size = new System.Drawing.Size(94, 13);
            this.lblBytesConstruccion.TabIndex = 25;
            this.lblBytesConstruccion.Text = "Bytes Construidos:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(404, 344);
            this.txtDireccion.Margin = new System.Windows.Forms.Padding(2);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.ReadOnly = true;
            this.txtDireccion.Size = new System.Drawing.Size(110, 20);
            this.txtDireccion.TabIndex = 48;
            // 
            // txtPuerto
            // 
            this.txtPuerto.Location = new System.Drawing.Point(374, 318);
            this.txtPuerto.Margin = new System.Windows.Forms.Padding(2);
            this.txtPuerto.Name = "txtPuerto";
            this.txtPuerto.ReadOnly = true;
            this.txtPuerto.Size = new System.Drawing.Size(92, 20);
            this.txtPuerto.TabIndex = 47;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Bell MT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(313, 347);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 15);
            this.label11.TabIndex = 46;
            this.label11.Text = "DIRECCIÓN IP:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bell MT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(315, 320);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 15);
            this.label10.TabIndex = 45;
            this.label10.Text = "PUERTO:";
            // 
            // label8
            // 
            this.label8.Image = global::winProyectService.Properties.Resources.service;
            this.label8.Location = new System.Drawing.Point(328, 162);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(147, 154);
            this.label8.TabIndex = 44;
            // 
            // txtConversacion
            // 
            this.txtConversacion.Location = new System.Drawing.Point(34, 347);
            this.txtConversacion.Margin = new System.Windows.Forms.Padding(2);
            this.txtConversacion.Name = "txtConversacion";
            this.txtConversacion.ReadOnly = true;
            this.txtConversacion.Size = new System.Drawing.Size(208, 186);
            this.txtConversacion.TabIndex = 43;
            this.txtConversacion.Text = "";
            // 
            // lblLenght
            // 
            this.lblLenght.AutoSize = true;
            this.lblLenght.Font = new System.Drawing.Font("Gabriola", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLenght.Location = new System.Drawing.Point(93, 261);
            this.lblLenght.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLenght.Name = "lblLenght";
            this.lblLenght.Size = new System.Drawing.Size(33, 35);
            this.lblLenght.TabIndex = 42;
            this.lblLenght.Text = "00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Gabriola", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(40, 265);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 29);
            this.label9.TabIndex = 41;
            this.label9.Text = "Lenght:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Script MT Bold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(30, 308);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 27);
            this.label7.TabIndex = 40;
            this.label7.Text = "Conversación:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Script MT Bold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(293, 382);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(187, 23);
            this.label14.TabIndex = 39;
            this.label14.Text = "Estado Archivo Recibir:";
            // 
            // btnEnviarImagen
            // 
            this.btnEnviarImagen.Font = new System.Drawing.Font("Dubai", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarImagen.Location = new System.Drawing.Point(605, 353);
            this.btnEnviarImagen.Margin = new System.Windows.Forms.Padding(2);
            this.btnEnviarImagen.Name = "btnEnviarImagen";
            this.btnEnviarImagen.Size = new System.Drawing.Size(135, 36);
            this.btnEnviarImagen.TabIndex = 33;
            this.btnEnviarImagen.Text = "Enviar Archivo";
            this.btnEnviarImagen.UseVisualStyleBackColor = true;
            this.btnEnviarImagen.Click += new System.EventHandler(this.btnEnviarImagen_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.barraProgreso);
            this.groupBox1.Controls.Add(this.lblBytesEnvio);
            this.groupBox1.Controls.Add(this.checkEnviado);
            this.groupBox1.Location = new System.Drawing.Point(563, 446);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(232, 91);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            // 
            // barraProgreso
            // 
            this.barraProgreso.Location = new System.Drawing.Point(14, 27);
            this.barraProgreso.Margin = new System.Windows.Forms.Padding(2);
            this.barraProgreso.Name = "barraProgreso";
            this.barraProgreso.Size = new System.Drawing.Size(170, 19);
            this.barraProgreso.TabIndex = 1;
            // 
            // lblBytesEnvio
            // 
            this.lblBytesEnvio.AutoSize = true;
            this.lblBytesEnvio.Location = new System.Drawing.Point(11, 63);
            this.lblBytesEnvio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBytesEnvio.Name = "lblBytesEnvio";
            this.lblBytesEnvio.Size = new System.Drawing.Size(83, 13);
            this.lblBytesEnvio.TabIndex = 23;
            this.lblBytesEnvio.Text = "Bytes Enviados:";
            // 
            // checkEnviado
            // 
            this.checkEnviado.AutoCheck = false;
            this.checkEnviado.BackColor = System.Drawing.Color.White;
            this.checkEnviado.Font = new System.Drawing.Font("Lucida Handwriting", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkEnviado.ForeColor = System.Drawing.Color.Red;
            this.checkEnviado.Location = new System.Drawing.Point(198, 27);
            this.checkEnviado.Margin = new System.Windows.Forms.Padding(2);
            this.checkEnviado.Name = "checkEnviado";
            this.checkEnviado.Size = new System.Drawing.Size(29, 19);
            this.checkEnviado.TabIndex = 8;
            this.checkEnviado.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Script MT Bold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(564, 410);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 23);
            this.label4.TabIndex = 31;
            this.label4.Text = "Estado de Envio de Archivo";
            // 
            // txtRuta
            // 
            this.txtRuta.Enabled = false;
            this.txtRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRuta.Location = new System.Drawing.Point(570, 266);
            this.txtRuta.Margin = new System.Windows.Forms.Padding(2);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(204, 71);
            this.txtRuta.TabIndex = 30;
            this.txtRuta.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Script MT Bold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(566, 221);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 23);
            this.label3.TabIndex = 29;
            this.label3.Text = "Ruta Seleccionada:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Script MT Bold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(560, 102);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(218, 23);
            this.label5.TabIndex = 28;
            this.label5.Text = "Selecciona Archivo a Enviar";
            // 
            // btnSeleccionarImagen
            // 
            this.btnSeleccionarImagen.Font = new System.Drawing.Font("Dubai", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionarImagen.Location = new System.Drawing.Point(577, 141);
            this.btnSeleccionarImagen.Margin = new System.Windows.Forms.Padding(2);
            this.btnSeleccionarImagen.Name = "btnSeleccionarImagen";
            this.btnSeleccionarImagen.Size = new System.Drawing.Size(170, 35);
            this.btnSeleccionarImagen.TabIndex = 27;
            this.btnSeleccionarImagen.Text = "Seleccionar Archivo";
            this.btnSeleccionarImagen.UseVisualStyleBackColor = true;
            this.btnSeleccionarImagen.Click += new System.EventHandler(this.btnSeleccionarImagen_Click);
            // 
            // comboClientes
            // 
            this.comboClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboClientes.FormattingEnabled = true;
            this.comboClientes.Location = new System.Drawing.Point(330, 130);
            this.comboClientes.Margin = new System.Windows.Forms.Padding(2);
            this.comboClientes.Name = "comboClientes";
            this.comboClientes.Size = new System.Drawing.Size(147, 21);
            this.comboClientes.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Script MT Bold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(333, 90);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 23);
            this.label6.TabIndex = 12;
            this.label6.Text = "Cliente a Enviar";
            // 
            // btnEnviarMensaje
            // 
            this.btnEnviarMensaje.Font = new System.Drawing.Font("MV Boli", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarMensaje.Location = new System.Drawing.Point(34, 214);
            this.btnEnviarMensaje.Margin = new System.Windows.Forms.Padding(2);
            this.btnEnviarMensaje.Name = "btnEnviarMensaje";
            this.btnEnviarMensaje.Size = new System.Drawing.Size(176, 29);
            this.btnEnviarMensaje.TabIndex = 10;
            this.btnEnviarMensaje.Text = "Enviar Mensaje";
            this.btnEnviarMensaje.UseVisualStyleBackColor = true;
            this.btnEnviarMensaje.Click += new System.EventHandler(this.btnEnviarMensaje_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Script MT Bold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 27);
            this.label2.TabIndex = 9;
            this.label2.Text = "Mensaje a Enviar:";
            // 
            // txtMensaje
            // 
            this.txtMensaje.Location = new System.Drawing.Point(34, 130);
            this.txtMensaje.Margin = new System.Windows.Forms.Padding(2);
            this.txtMensaje.Multiline = true;
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.Size = new System.Drawing.Size(208, 65);
            this.txtMensaje.TabIndex = 8;
            this.txtMensaje.TextChanged += new System.EventHandler(this.txtMensaje_TextChanged);
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe Script", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.Location = new System.Drawing.Point(277, 19);
            this.lblCustomer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(154, 44);
            this.lblCustomer.TabIndex = 1;
            this.lblCustomer.Text = "Customer";
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            // 
            // frmCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(804, 593);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmCliente";
            this.Text = "frmCliente";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCliente_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.RichTextBox txtConversacion;
        private System.Windows.Forms.Label lblLenght;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtPuerto;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ProgressBar barraRecibir;
        private System.Windows.Forms.CheckBox checkRecibir;
        private System.Windows.Forms.Label lblBytesConstruccion;
        private System.Windows.Forms.RichTextBox txtRutaEnviada;
        private System.Windows.Forms.OpenFileDialog fileDialog;
    }
}