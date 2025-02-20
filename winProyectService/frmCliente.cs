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

namespace winProyectService
{
    public partial class frmCliente: Form
    {

        IPHostEntry ipInfoHost;
        IPAddress ipDireccion;
        IPEndPoint PuntoRemoto;
        Socket SocketCliente;

        private string clientId;

        private Thread hiloRecibir;
        private Thread enviandoArch;

        private Dictionary<string, string> clientes_conectados = new Dictionary<string, string>();

        private ManualResetEvent enviarInformacionCompleta = new ManualResetEvent(false);

        classArchivo archivoRecibir;

        classArchivo archivoEnviar;

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
                        string recipientId = ((KeyValuePair<string, string>)comboClientes.SelectedItem).Key;
                        string texto = txtMensaje.Text.Trim();
                        string fullMessage = $"M:{recipientId}:{texto}";
                        byte[] msgBuffer = Encoding.UTF8.GetBytes(fullMessage);
                        SocketCliente.Send(msgBuffer);
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
                int i = 0;
                while (SocketCliente.Connected)
                {
                    int bytesRead = SocketCliente.Receive(bufferRecibir);
                    if (bytesRead > 0)
                    {
                        string tipo_mensaje = Encoding.UTF8.GetString(bufferRecibir, 0, 1);
                        
                        switch (tipo_mensaje)
                        {
                            case "C":
                                Console.WriteLine("Bytes recibidos en el Cliente CLIENTS: " + bytesRead);
                                procesarClientes(bytesRead);
                                break;
                            case "N":
                                Console.WriteLine("Bytes recibidos en el Cliente ID: " + bytesRead);
                                procesarID(bytesRead);
                                break;
                            case "M":
                                Console.WriteLine("Bytes recibidos en el Cliente MENSAJE: " + bytesRead);
                                procesarMensaje(bytesRead);
                                break;
                            case "I":
                                Console.WriteLine("Bytes recibidos en el Cliente INFO: " + bytesRead);
                                procesarInformacion();
                                break;
                            case "A":
                                Console.WriteLine("Bytes recibidos en el Cliente ARCHIVO: " + bytesRead);
                                Console.WriteLine("VEz Recibida en cliente" + i);
                                i++;
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
            string message_real = message.Substring(7);

            UpdateUI($"{clientes_conectados[emisor]}: {message_real}");
        }

        private void procesarInformacion()
        {
            //string info_total = $"I:001J:0000002222:006:AE.TXT";

            if (bufferRecibir.Length < 22)
            {
                Console.WriteLine("Error: Buffer demasiado pequeño para procesar la información.");
                return;
            }

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

            Console.WriteLine("Tamaño del archivo: " + archivoRecibir.bytes.Length);
        }

        private void procesarArchivo()
        {
            try
            {
                int bytesRestantes = archivoRecibir.bytes.Length - archivoRecibir.Avance;
                int tamañoPaquete = bufferRecibir.Length - 7; 

                int bytesAEscribir = Math.Min(bytesRestantes, tamañoPaquete);

                archivoRecibir.EscribiendoArchivo.Write(bufferRecibir, 7, bytesAEscribir);
                archivoRecibir.Avance += bytesAEscribir;

                UpdateRecibir((archivoRecibir.Avance / (float)archivoRecibir.bytes.Length) * 100, archivoRecibir.Avance, archivoRecibir.bytes.Length);

                if (archivoRecibir.Avance >= archivoRecibir.bytes.Length)
                {
                    archivoRecibir.EscribiendoArchivo.Close();
                    archivoRecibir.FlujoArchivoRecibir.Close();
                    UpdateUI($"Cliente {archivoRecibir.Nombre} envió un archivo exitosamente.");
                    Console.WriteLine("Archivo recibido correctamente y cerrado.");
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

            barraRecibir.Value = (int)Math.Ceiling(cantidad);
            lblBytesConstruccion.Text = $"Bytes de Construccion: {bytes_actuales.ToString()}/{total.ToString()}";
            if (bytes_actuales == total)
            {
                checkRecibir.Checked = true;
            }
            else
            {
                checkRecibir.Checked = false;
            }
        }

        private void UpdateClientList(string clientListString)
        {
            string[] clientIds = clientListString.Split(',');
            clientes_conectados.Clear();
            foreach (string id in clientIds)
            {
                if (id != clientId)
                {
                    clientes_conectados.Add(id, $"Cliente {id.Substring(0, 4)}");
                }
                else
                {
                    clientId = id;
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
                Invoke(new Action<string>(UpdateUI), id);
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
                    byte[] bytesImagen = File.ReadAllBytes(rutita);

                    archivoEnviar = new classArchivo(rutita, bytesImagen, 0);

                    string recipientId = ((KeyValuePair<string, string>)comboClientes.SelectedItem).Key;

                    enviarInformacion();

                    enviandoArch = new Thread(() => enviarArchivo(recipientId));
                    enviandoArch.Start();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al enviar: " + ex.Message);
            }
        }

        private void enviarInformacion()
        {
            string recipientId = ((KeyValuePair<string, string>)comboClientes.SelectedItem).Key;

            string size_archivo = archivoEnviar.bytes.Length.ToString("D10");

            string nombre = Path.GetFileName(archivoEnviar.Nombre);

            string size_nombre = nombre.Length.ToString("D3");

            string info_total = $"I:{recipientId}:{size_archivo}:{size_nombre}:{nombre}";

            Console.WriteLine(info_total);

            byte[] bufferInfo = Encoding.UTF8.GetBytes(info_total);

            SocketCliente.Send(bufferInfo);
        }

        private void enviarArchivo(string recipientId)
        {
            try
            {
                byte[] cabeza = Encoding.UTF8.GetBytes($"A:{recipientId}:"); // A:aea1:
                int tamaño_imagen = archivoEnviar.bytes.Length;

                for (int i = 0; i < tamaño_imagen; i += 1017)
                {
                    int size = Math.Min(1017, tamaño_imagen - i);
                    byte[] tramaEnviar = new byte[7 + size];

                    archivoEnviar.Avance += size;

                    Array.Copy(cabeza, 0, tramaEnviar, 0, 7);
                    Array.Copy(archivoEnviar.bytes, i, tramaEnviar, 7, size);

                    int totalEnviado = 0;
                    while (totalEnviado < tramaEnviar.Length)
                    {
                        int enviado = SocketCliente.Send(tramaEnviar, totalEnviado, tramaEnviar.Length - totalEnviado, SocketFlags.None);
                        if (enviado == 0) throw new Exception("Conexión cerrada durante el envío");
                        totalEnviado += enviado;
                    }

                    float progreso = ((float)archivoEnviar.Avance / tamaño_imagen) * 100;
                    UpdateEnvio(progreso, archivoEnviar.Avance, tamaño_imagen);
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

            barraProgreso.Value = (int)Math.Ceiling(cantidad);
            lblBytesEnvio.Text = $"Bytes Enviados: {bytes_actuales.ToString()}/{total.ToString()}";

            if (bytes_actuales == total)
            {
                checkEnviado.Checked = true;
            }
            else
            {
                checkEnviado.Checked = false;
            }

        }
    }
}
