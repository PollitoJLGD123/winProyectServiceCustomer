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

        private Dictionary<string, string> clientes_conectados = new Dictionary<string, string>();

        private ManualResetEvent enviarInformacionCompleta = new ManualResetEvent(false);

        classArchivo archivoRecibir;

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
                        string fullMessage = $"{recipientId}:MENSAJE:{texto}";
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
                byte[] buffer = new byte[1024];
                while (SocketCliente.Connected)
                {
                    int bytesRead = SocketCliente.Receive(buffer);
                    if (bytesRead > 0)
                    {
                        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        procesarMensaje(receivedMessage);
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

        private void procesarMensaje(string mensaje)
        {
            if (mensaje.StartsWith("CLIENTS:"))
            {
                UpdateClientList(mensaje.Substring(8));
            }
            else
            {
                if (mensaje.StartsWith("ID:"))
                {
                    UpdateName(mensaje.Substring(3));
                }
                else
                {
                    string sendId = mensaje.Substring(0,4); // 0aea:

                    string tipo = mensaje.Substring(5);

                    if (tipo.StartsWith("MENSAJE:"))
                    {
                        string actualMessage = mensaje.Substring(13);
                        UpdateUI($"{clientes_conectados[sendId]}: {actualMessage}");
                    }
                    else
                    {
                        if(tipo.StartsWith("INFO:"))
                        {
                            //string info_total = $"{recipientId}:INFO:{size_archivo}:{size_nombre}:{nombre}";

                            int size_archivo = int.Parse(mensaje.Substring(10, 10));

                            int size_nombre = int.Parse(mensaje.Substring(21, 3));

                            string nombre = mensaje.Substring(25, size_nombre);

                            string ruta_temp = $"E:/Probando/Recibir/{nombre}";

                            if (File.Exists(ruta_temp))
                            {
                                File.Delete(ruta_temp); // Evitamos problemas de sobreescritura 
                            }

                            archivoRecibir = new classArchivo(ruta_temp, new byte[size_archivo], 0, 0);

                            archivoRecibir.iniciarFlujo();
                        }
                        else
                        {
                            if (tipo.StartsWith("ARCHIVO:"))
                            {
                                byte[] bytes = Encoding.UTF8.GetBytes(tipo.Substring(8));

                                int bytesRestantes = archivoRecibir.bytes.Length - archivoRecibir.Avance;

                                if (bytesRestantes > 1011)
                                {
                                    archivoRecibir.EscribiendoArchivo.Write(bytes, 0, 1011);
                                    archivoRecibir.Avance += 1011;
                                    UpdateRecibir((archivoRecibir.Avance / (float)archivoRecibir.bytes.Length) * 100, archivoRecibir.Avance, archivoRecibir.bytes.Length);
                                }
                                else
                                {
                                    archivoRecibir.EscribiendoArchivo.Write(bytes, 0, bytesRestantes);
                                    archivoRecibir.Avance += bytesRestantes;
                                    UpdateRecibir((archivoRecibir.Avance / (float)archivoRecibir.bytes.Length) * 100, archivoRecibir.Avance, archivoRecibir.bytes.Length);
                                    archivoRecibir.EscribiendoArchivo.Close();
                                    archivoRecibir.FlujoArchivoRecibir.Close();
                                    UpdateUI($"Cliente {sendId} envio un archivo: {archivoRecibir.Nombre}");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Mensaje no reconocido");
                            }
                        }
                    }
                    
                }
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

                    classArchivo archivoEnviar = new classArchivo(rutita, bytesImagen,0,1);

                    enviarInformacion(archivoEnviar);

                    enviarArchivo(archivoEnviar);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al enviar: " + ex.Message);
            }
        }

        private void enviarInformacion(classArchivo archivo)
        {
            string recipientId = ((KeyValuePair<string, string>)comboClientes.SelectedItem).Key;

            string size_archivo = archivo.bytes.Length.ToString("D10");

            string nombre = Path.GetFileName(archivo.Nombre);

            string size_nombre = nombre.Length.ToString("D3");

            string info_total = $"{recipientId}:INFO:{size_archivo}:{size_nombre}:{nombre}";

            byte[] bufferInfo = Encoding.UTF8.GetBytes(info_total);

            SocketCliente.Send(bufferInfo);
        }

        private void enviarArchivo(classArchivo archivo)
        {
            try
            {
                string recipientId = ((KeyValuePair<string, string>)comboClientes.SelectedItem).Key;

                byte[] cabeza = ASCIIEncoding.UTF8.GetBytes($"{recipientId}:ARCHIVO:");

                int tamaño_imagen = archivo.bytes.Length;

                int cantidad_exacta = 1011 * ((int)(tamaño_imagen / 1011)); //

                for (int i = 0; i < tamaño_imagen; i += 1011) //0, 
                {
                    int size = Math.Min(1011, archivo.bytes.Length - i); //

                    byte[] tramaEnviar = Enumerable.Repeat((byte)'@', 1011).ToArray();//Rellena todo el arreglo con @

                    archivo.Avance += size;

                    Array.Copy(cabeza, 0, tramaEnviar, 0, 13);
                    Array.Copy(archivo.bytes, i, tramaEnviar, 13, size);

                    SocketCliente.Send(tramaEnviar);


                    if (i == 0)
                    {
                        if (tamaño_imagen < 1011)
                        {
                            UpdateEnvio(100, archivo.Avance, tamaño_imagen);
                        }
                        else
                        {
                            UpdateEnvio(0, 0, tamaño_imagen);  
                        }

                    }
                    else
                    {
                        UpdateEnvio(((float)i / (float)cantidad_exacta) * 100, archivo.Avance, tamaño_imagen); 
                    }

                }

            }
            catch(Exception ex)
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
