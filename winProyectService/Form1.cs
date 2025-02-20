using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Policy;
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
                MessageBox.Show(this, ex.Message);
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

                    Socket socketHijo = SocketEscucha.Accept();

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

            string clientId = Guid.NewGuid().ToString().Substring(0,4);

            enviarID(clientId, cliente_socket);

            listaClientes.TryAdd(clientId, cliente_socket);

            reenviarClientes(true, Color.Green);

            byte[] buffer = new byte[1024];
            try
            {
                int i = 0;
                while (true)
                {
                    int bytesRead = cliente_socket.Receive(buffer); // solo devuelve cuando se desconecta el cliente

                    Console.WriteLine("bytesRead: " + bytesRead);

                    if (bytesRead == 0) break; //cliente se conecta y no envia nada

                    string id_recibe = Encoding.ASCII.GetString(buffer, 2, 4);

                    if (listaClientes.TryGetValue(id_recibe, out Socket socket_recibe))
                    {
                        byte[] nombreEnvia = Encoding.UTF8.GetBytes(clientId);

                        Array.Copy(nombreEnvia, 0, buffer, 2, 4);

                        Console.WriteLine("Enviando mensaje a: " + id_recibe + " Y Se envio correctamente VEZ: " + i);
                        socket_recibe.Send(buffer, bytesRead, SocketFlags.None);
                        i++;
                    }
                    else
                    {
                        UpdateUI($"El cliente {id_recibe} no está conectado");
                    }

                }
            }
            catch (Exception ex)
            {
                UpdateUI($"Error en la comunicación con el cliente: {ex.Message}");
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                listaClientes.TryRemove(clientId, out _);
                
                cliente_socket.Close();

                Console.WriteLine("Se elimino al cliente");
                reenviarClientes(false, Color.Red);
            }
        }

        private void enviarID(string id, Socket cliente_socket)
        {
            byte[] idBuffer = Encoding.UTF8.GetBytes("N:" + id);
            cliente_socket.Send(idBuffer);
        }

        private void enviarClientes(Socket cliente_socket)
        {
            string clientes_enlazados = "C:" + string.Join(",", listaClientes.Keys);
            byte[] listBuffer = Encoding.UTF8.GetBytes(clientes_enlazados);
            cliente_socket.Send(listBuffer);
        }

        private void reenviarClientes(bool estado, Color color)
        {
            int i = 1;
            foreach (var kvp in listaClientes)
            {
                string idCliente = kvp.Key;
                var client = kvp.Value;

                enviarClientes(client);
                UpdateChecker(i, true, idCliente, Color.Green);
                i++;
            }

            for (int j = i; j <= 5; j++)
            {
                UpdateChecker(j, false, "", Color.Red);
            }
        }
        private void UpdateChecker(int number, bool prendido, string name, Color color)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int, bool, string, Color>(UpdateChecker), number, prendido, name, color);
                return;
            }

            CheckBox[] checkBoxes = { checkConectado1, checkConectado2, checkConectado3, checkConectado4, checkConectado5 };
            Label[] labels = { lblNombre1, lblNombre2, lblNombre3, lblNombre4, lblNombre5 };

            if (number >= 1 && number <= checkBoxes.Length)
            {
                checkBoxes[number - 1].Checked = prendido;
                checkBoxes[number - 1].ForeColor = color;

                checkBoxes[number - 1].Text = prendido == true ? "Connect ✓ ✖" : "Disconnect ✓ ✖";
                labels[number - 1].Text = $"Name: {name}";
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
                MessageBox.Show(this, ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
