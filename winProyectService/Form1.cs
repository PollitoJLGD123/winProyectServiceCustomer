﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winProyectService
{
    public partial class Form1 : Form
    {

        IPHostEntry ipInfoHost;
        IPAddress ipDireccion;
        IPEndPoint PuntoFinal;

        bool isListening = false;

        public Socket SocketEscucha;

        public Thread hiloEscuchar;

        private ConcurrentDictionary<string, Socket> listaClientes = new ConcurrentDictionary<string, Socket>();

        public Form1()
        {
            InitializeComponent();

            comboDirecciones.Items.Clear();
            ipInfoHost = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in ipInfoHost.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    comboDirecciones.Items.Add(ip.ToString());
                }
            }

            comboDirecciones.SelectedIndex = 0;
            comboPuertos.SelectedIndex = 0;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            try
            {

                int puerto = int.Parse(comboPuertos.SelectedItem.ToString());
                string ip = comboDirecciones.SelectedItem.ToString();

                if (!isListening)
                {
                    empezarServidor(puerto, ip);
                }
                else
                {
                    pararServidor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void empezarServidor(int puerto, string ip)
        {
            try
            {

                ipDireccion = IPAddress.Parse(ip);
                PuntoFinal = new IPEndPoint(ipDireccion, puerto);

                SocketEscucha = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                SocketEscucha.Bind(PuntoFinal);

                SocketEscucha.Listen(5);


                isListening = true;
                lblEstado.Text = "ON";
                lblEstado.ForeColor = Color.Green;
                checkEstado.Checked = true;
                checkEstado.ForeColor = Color.Orange;
                barraEstado.Value = 100;
                txtDireccion.Text = ip;
                btnIniciar.Text = "Detener Servidor";
                txtPuerto.Text = puerto.ToString();

                hiloEscuchar = new Thread(new ThreadStart(Escuchar));
                hiloEscuchar.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void pararServidor()
        {
            isListening = false;

            SocketEscucha?.Close(); 

            hiloEscuchar?.Join();

            lblEstado.Text = "OFF";
            lblEstado.ForeColor = Color.Red;
            checkEstado.Checked = false;
            checkEstado.ForeColor = Color.Red;
            barraEstado.Value = 0;

            foreach (var client in listaClientes.Values)
            {
                client.Close();
            }
            listaClientes.Clear();

            btnIniciar.Text = "Iniciar Servidor";

            txtDireccion.Text = "";
            txtPuerto.Text = "";
            UpdateUI("Servidor detenido");
        }


        private void Escuchar()
        {
            while (isListening)
            {
                try
                {

                    Socket socketHijo = SocketEscucha.Accept(); //

                    Console.WriteLine("Cliente conectado" + socketHijo.ToString());

                    if (socketHijo != null) { 
                        Thread clientThread = new Thread(envioMensaje);
                        clientThread.Start(socketHijo);
                    }

                }
                catch (SocketException)
                {
                    // El servidor se ha detenido
                    break;
                }
            }
        }


        private void envioMensaje(object socketHijo)
        {
            Socket cliente_socket = (Socket)socketHijo;

            string clientId = Guid.NewGuid().ToString();

            listaClientes.TryAdd(clientId, cliente_socket);

            if (!listaClientes.IsEmpty)
            {
                foreach (var client in listaClientes.Values) // que lo haga automatica sin necesidad de
                {
                    enviarClientes(client);
                }
            }

            byte[] buffer = new byte[1024];
            try
            {

                while (true)
                {
                    int bytesRead = cliente_socket.Receive(buffer);

                    Console.WriteLine("bytesRead: " + bytesRead);

                    if (bytesRead == 0) break; //cliente se conecta y no envia nada

                    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    
                    procesarMensaje(clientId, dataReceived);
                }
            }
            catch (Exception ex)
            {
                UpdateUI($"Error en la comunicación con el cliente: {ex.Message}");
            }
        }

        private void procesarMensaje(string id_envia, string message)
        {
            string[] parts = message.Split(new[] { ':' }, 2);
            if (parts.Length == 2)
            {
                string id_recibe = parts[0];
                string message_real = parts[1];

                if (listaClientes.TryGetValue(id_recibe, out Socket socket_recibe))
                {
                    string mensaje_total = $"{id_envia}:{message_real}";
                    byte[] msgBuffer = Encoding.UTF8.GetBytes(mensaje_total);
                    socket_recibe.Send(msgBuffer);
                }
                else
                {
                    UpdateUI($"El cliente {id_recibe} no está conectado");
                }
            }
        }

        private void enviarClientes(Socket cliente_socket)
        {
            string clientes_enlazados = "CLIENTS:" + string.Join(",", listaClientes.Keys);
            byte[] listBuffer = Encoding.UTF8.GetBytes(clientes_enlazados);
            cliente_socket.Send(listBuffer);
        }

        private void reenviarClientes()
        {
            foreach (var client in listaClientes.Values)
            {
                enviarClientes(client);
            }
        }


        private void UpdateUI(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateUI), message);
                return;
            }
           
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            try
            {
                int puerto = int.Parse(comboPuertos.SelectedItem.ToString());
                string ip = comboDirecciones.SelectedItem.ToString();

                frmCliente accion = new frmCliente();

                accion.Conectar(puerto, ip);

                accion.Visible = true;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
