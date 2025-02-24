using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using winTwoPlays;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.Collections.Concurrent;
using System.Xml.Serialization;

namespace winProyectService
{
    public partial class frmCliente : Form
    {

        IPHostEntry ipInfoHost;
        IPAddress ipDireccion;
        IPEndPoint PuntoRemoto;
        Socket SocketCliente;

        private string clientId;

        private Thread hiloRecibir;
        private Thread enviandoArch;

        private Dictionary<string, string> clientes_conectados = new Dictionary<string, string>();

        private classArchivo archivoEnviar;
        private classArchivo archivoRecibir;

        int number = 0;

        int aea = 0;

        byte[] bufferRecibir;

        public frmCliente()
        {
            InitializeComponent();
        }

        public void Conectar(int puerto, string ip)
        {
            try
            {
                ipDireccion = IPAddress.Parse(ip);
                ipInfoHost = Dns.GetHostEntry(ipDireccion);
                PuntoRemoto = new IPEndPoint(ipDireccion, puerto);

                SocketCliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                SocketCliente.Connect(PuntoRemoto);

                hiloRecibir = new Thread(recibirMensajes);
                hiloRecibir.Start();

                MessageBox.Show("Conectado al servidor");

                txtPuerto.Text = puerto.ToString();
                txtDireccion.Text = ip;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEnviarMensaje_Click(object sender, EventArgs e)
        {
            try
            {
                // MENSAJE M
                // INFO I
                // ASIGNARID ID N
                // ARCHIVO A

                // M - po12 - infoooooooooooooooooooooooooooooo 

                if (txtMensaje.Text.Length <= 0 || txtMensaje.Text.Equals(""))
                {
                    MessageBox.Show("Ingresa texto válido");
                }
                else
                {
                    if (SocketCliente != null && SocketCliente.Connected && comboClientes.SelectedItem != null)
                    {
                        byte[] buferMensaje = Enumerable.Repeat((byte)'@', 1024).ToArray();

                        string recipientId = ((KeyValuePair<string, string>)comboClientes.SelectedItem).Key;
                        string texto = txtMensaje.Text.Trim();
                        int size = texto.Length;
                        string fullMessage = $"M:{recipientId}:{size.ToString("D3")}:{texto}";

                        Console.WriteLine("Mensaje a enviar: " + fullMessage);

                        byte[] msgBuffer = Encoding.UTF8.GetBytes(fullMessage);

                        Array.Copy(msgBuffer, 0, buferMensaje, 0, msgBuffer.Length);

                        SocketCliente.Send(buferMensaje);

                        Console.WriteLine("Se envio");

                        UpdateUI($"Yo: {texto} (para {comboClientes.Text})");
                        txtMensaje.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("No estás conectado o no has seleccionado un destinatario");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void recibirMensajes()
        {
            try
            {
                bufferRecibir = new byte[1024];
                while (SocketCliente.Connected)
                {
                    int bytesRead = SocketCliente.Receive(bufferRecibir); 
                    if (bytesRead >= 1024)
                    {
                        string tipo_mensaje = Encoding.UTF8.GetString(bufferRecibir, 0, 1);

                        //"ACK"

                        switch (tipo_mensaje)
                        {
                            case "C":
                                procesarClientes(bytesRead);
                                break;
                            case "N":
                                procesarID(bytesRead);
                                break;
                            case "M":
                                procesarMensaje(bytesRead);
                                break;
                            case "I":
                                procesarInformacion();
                                break;
                            case "A":
                                procesarArchivo();
                                break;
                            default:
                                MessageBox.Show("Mensaje no reconocido");
                                break;
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode != SocketError.Interrupted)
                {
                    MessageBox.Show($"Error en la comunicación con el servidor: {ex.Message}");
                }
                else
                {
                    Console.WriteLine("aca es el error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}");
            }
        }

        private void procesarClientes(int cantidad)
        {
            string message = Encoding.UTF8.GetString(bufferRecibir, 0, cantidad);
            UpdateClientList(message.Substring(2));
            //Console.WriteLine("Clientes conectados: " + message.Substring(2));
            //C:jdh1,oii1,ddd1@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        }

        private void procesarID(int cantidad)
        {
            string message = Encoding.UTF8.GetString(bufferRecibir, 0, cantidad);
            UpdateName(message.Substring(2, 4));
        }

        private void procesarMensaje(int cantidad)
        {
            string message = Encoding.UTF8.GetString(bufferRecibir, 0, cantidad);
            
            string emisor = message.Substring(2, 4);

            //M:0123:004:Hola@@@@@@@@@@@@@@@@@@@@@

            int size_leer = Convert.ToInt32(Encoding.UTF8.GetString(bufferRecibir,7,3));

            string message_real = message.Substring(11,size_leer);

            UpdateUI($"{clientes_conectados[emisor]}: {message_real}");
        }

        private void procesarInformacion()
        {
            //string info_total = $"I:001J:0000002222:006:AE.TXT:3";

            string emisor = Encoding.UTF8.GetString(bufferRecibir, 2, 4);
            int size_archivo = Convert.ToInt32(Encoding.UTF8.GetString(bufferRecibir, 7, 10));
            int size_nombre = Convert.ToInt32(Encoding.UTF8.GetString(bufferRecibir, 18, 3));

            string nombre = Encoding.UTF8.GetString(bufferRecibir, 22, size_nombre);

            string ruta_temp = $"E:/Probando/Recibir/{nombre}";

            if (File.Exists(ruta_temp))
            {
                File.Delete(ruta_temp); // Evitamos problemas de sobreescritura 
            }

            archivoRecibir = new classArchivo(ruta_temp, new byte[size_archivo], 0);

            archivoRecibir.iniciarFlujo();
        }

        private void procesarArchivo()
        {
            try
            {
                //A:aea1:0000222:

                int contador = Convert.ToInt32(Encoding.UTF8.GetString(bufferRecibir, 7, 7));

                int bytesRestantes = archivoRecibir.bytes.Length - archivoRecibir.Avance;
                int tamañoPaquete = bufferRecibir.Length - 15;

                string sendId = Encoding.UTF8.GetString(bufferRecibir, 2, 4);

                int bytesAEscribir = Math.Min(bytesRestantes, tamañoPaquete);

                if (bytesAEscribir > 0)  
                {
                    archivoRecibir.EscribiendoArchivo.Write(bufferRecibir, 15, bytesAEscribir);
                    archivoRecibir.Avance += bytesAEscribir;

                    if(bytesAEscribir >= 1009) // 
                    {
                        Console.WriteLine("Trama que se recibe aca en el cliente (5 primeros): " + contador + " Mensaje:" +   ASCIIEncoding.UTF8.GetString(bufferRecibir, 0, 20));
                        Console.WriteLine("Trama que se recibe aca en el cliente (5 ultimos): " + contador + " Mensaje:" +  ASCIIEncoding.UTF8.GetString(bufferRecibir, 1004, 20));
                    }
                    else
                    {
                        Console.WriteLine("Trama ultimaaaa que se recibe: " + ASCIIEncoding.UTF8.GetString(bufferRecibir));                   
                    }

                }

                UpdateRecibir((archivoRecibir.Avance / (float)archivoRecibir.bytes.Length) * 100, archivoRecibir.Avance, archivoRecibir.bytes.Length);

                if (archivoRecibir.Avance >= archivoRecibir.bytes.Length)
                {
                    archivoRecibir.EscribiendoArchivo.Flush();
                    archivoRecibir.EscribiendoArchivo.Close();
                    archivoRecibir.FlujoArchivoRecibir.Close();

                    UpdateUI($"Cliente {Encoding.UTF8.GetString(bufferRecibir, 2, 4)} envió un archivo exitosamente.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en procesarArchivo: {ex.Message}");
            }
        }

        private void UpdateRecibir(float cantidad, float bytes_actuales, float total)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<float, float, float>(UpdateRecibir), cantidad, bytes_actuales, total);
                return;
            }
            barraRecibir1.Value = (int)Math.Ceiling(cantidad);
            lblBytesConstruccion1.Text = $"Bytes de Construccion: {bytes_actuales.ToString()}/{total.ToString()}";
            if (bytes_actuales == total)
            {
                checkRecibir1.Checked = true;
            }
            else
            {
                checkRecibir1.Checked = false;
            }
        }

        private void UpdateClientList(string clientListString)
        {
            //Console.WriteLine("Clientes conectados: " + message.Substring(2));
            //C:jdh1,oii1,ddd1@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

            string[] clientIds = clientListString.Split(',');
            clientes_conectados.Clear();
            foreach (string id in clientIds)
            {
                if (id != clientId)
                {
                    clientes_conectados.Add(id.Substring(0, 4), $"Cliente {id.Substring(0, 4)}");
                }
                else
                {
                    clientId = id.Substring(0, 4);
                }
            }
            UpdateClientList();
        }

        private void UpdateClientList()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateClientList));
                return;
            }
            comboClientes.DataSource = new BindingSource(clientes_conectados, null);
            comboClientes.DisplayMember = "Value";
            comboClientes.ValueMember = "Key";

            if (comboClientes.Items.Count > 0)
            {
                comboClientes.SelectedIndex = 0;
            }
        }

        private void UpdateUI(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateUI), message);
                return;
            }
            txtConversacion.Text = txtConversacion.Text + "\n" + message;
        }

        private void UpdateName(string id)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateName), id);
                return;
            }
            lblCustomer.Text = "Cliente " + id;
        }

        private void txtMensaje_TextChanged(object sender, EventArgs e)
        {
            int lenghtText = txtMensaje.TextLength;
            lblLenght.Text = string.Format("{0:00}", lenghtText);
        }

        private void DisconnectFromServer()
        {
            if (SocketCliente != null && SocketCliente.Connected)
            {
                SocketCliente.Shutdown(SocketShutdown.Both);
                SocketCliente.Close();
            }
            if (hiloRecibir != null)
            {
                hiloRecibir.Join();
            }
            clientes_conectados.Clear();
            UpdateClientList();
        }

        private void frmCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DisconnectFromServer();
            }
            catch(Exception ex)
            {
                MessageBox.Show(this,ex.Message);
            }
        }

        private void btnSeleccionarImagen_Click(object sender, EventArgs e)
        {
            try
            {
                fileDialog.Filter = "Todos los archivos (*.*)|*.*";
                fileDialog.Title = "Seleccionar un archivo";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    String rutaArchivo = fileDialog.FileName;

                    txtRuta.Text = "\n" + rutaArchivo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEnviarImagen_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRuta.Text.Equals(""))
                {
                    MessageBox.Show("Elige un archivo primero");
                }
                else
                {
                    string rutita = txtRuta.Text.Trim();

                    string recipientId = ((KeyValuePair<string, string>)comboClientes.SelectedItem).Key;
                    byte[] bytesImagen = File.ReadAllBytes(rutita);

                    archivoEnviar = new classArchivo(rutita, bytesImagen, 0);

                    enviarInformacion(recipientId);

                    enviandoArch = new Thread( () =>enviarArchivo(recipientId));
                    enviandoArch.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al enviar: " + ex.Message);
            }
        }

        private void enviarInformacion(string recipientId)
        {
            byte[] bufferInformacionEnviar = Enumerable.Repeat((byte)'@', 1024).ToArray();

            string size_archivo = archivoEnviar.bytes.Length.ToString("D10");

            string nombre = Path.GetFileName(archivoEnviar.Nombre);

            string name_real = nombre.Split('.')[0];
            string extension = nombre.Split('.')[1];

            nombre = $"{name_real}{number}.{extension}";

            string size_nombre = nombre.Length.ToString("D3");

            string info_total = $"I:{recipientId}:{size_archivo}:{size_nombre}:{nombre}";

            byte[] bufferInfo = Encoding.UTF8.GetBytes(info_total);

            Array.Copy(bufferInfo, 0, bufferInformacionEnviar, 0, bufferInfo.Length);

            number++;

            SocketCliente.Send(bufferInformacionEnviar);
        }

        private void enviarArchivo(string recipientId)
        {
            try
            {
                int contador = 0;

                int tamaño_imagen = archivoEnviar.bytes.Length;

                int cantidad_exacta = 1009 * ((int)(tamaño_imagen / 1009));

                for (int i = 0; i < tamaño_imagen; i += 1009)
                {
                    byte[] cabeza = Encoding.UTF8.GetBytes($"A:{recipientId}:{contador.ToString("D7")}:");
                    int size = Math.Min(1009, tamaño_imagen - i);
                    byte[] tramaEnviar = Enumerable.Repeat((byte)'@', 1024).ToArray();

                    archivoEnviar.Avance += size;

                    Array.Copy(cabeza, 0, tramaEnviar, 0, 15);
                    Array.Copy(archivoEnviar.bytes, i, tramaEnviar, 15, size);

                    Console.WriteLine("Trama que se envia al cliente del archivo(5 primeros): " + contador + " Mensaje:" + ASCIIEncoding.UTF8.GetString(tramaEnviar, 0, 20));
                    Console.WriteLine("Trama que se envia al cliente del archivo(5 ultimos): " + contador + " Mensaje:" +  ASCIIEncoding.UTF8.GetString(tramaEnviar, 1004, 20));

                    SocketCliente.Send(tramaEnviar);

                    UpdateEnvio(((float)i / (float)cantidad_exacta) * 100, archivoEnviar.Avance, tamaño_imagen);
                    contador++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar archivo: " + ex.Message);
            }
        }

        private void UpdateEnvio(float cantidad,float bytes_actuales, float total)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<float,float,float>(UpdateEnvio),cantidad,bytes_actuales,total);
                return;
            }

            barraProgreso1.Value = (int)Math.Ceiling(cantidad);
            lblBytesEnvio1.Text = $"Bytes Enviados: {bytes_actuales.ToString()}/{total.ToString()}";

            if (bytes_actuales == total)
            {
                checkEnviado1.Checked = true;
            }
            else
            {
                checkEnviado1.Checked = false;
            }
        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
