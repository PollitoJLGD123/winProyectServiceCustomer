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

        classArchivo[] archivosRecibir;

        classArchivo[] archivosEnviar;

        int number = 0;
        int contador = 0;

        byte[] bufferRecibir;

        private Queue<classPackage> colaArchivos = new Queue<classPackage>();
        private bool recibiendoArchivo = false;
       
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

                SocketCliente.ReceiveBufferSize = 8192; 
                SocketCliente.SendBufferSize = 8192;

                SocketCliente.Connect(PuntoRemoto);

                archivosEnviar = new classArchivo[5];

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
                archivosRecibir = new classArchivo[5];
                while (SocketCliente.Connected)
                {
                    int bytesRead = SocketCliente.Receive(bufferRecibir);
                    if (bytesRead > 0)
                    {
                        string tipo_mensaje = Encoding.UTF8.GetString(bufferRecibir, 0, 1);
                        
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
                                int orden = Convert.ToInt32(Encoding.UTF8.GetString(bufferRecibir, 7, 1));

                                lock (colaArchivos)
                                {
                                    colaArchivos.Enqueue(new classPackage(bufferRecibir.Take(bytesRead).ToArray(), orden));
                                }
                                if (!recibiendoArchivo)
                                {
                                    Thread hiloArchivo = new Thread(procesarArchivo);
                                    hiloArchivo.Start();
                                }
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
            //string info_total = $"I:001J:0000002222:006:AE.TXT:3";

            string emisor = Encoding.UTF8.GetString(bufferRecibir, 2, 4);
            int size_archivo = Convert.ToInt32(Encoding.UTF8.GetString(bufferRecibir, 7, 10));
            int size_nombre = Convert.ToInt32(Encoding.UTF8.GetString(bufferRecibir, 18, 3));

            string nombre = Encoding.UTF8.GetString(bufferRecibir, 22, size_nombre);

            int orden = Convert.ToInt32(Encoding.UTF8.GetString(bufferRecibir, 23 + size_nombre, 1));

            string ruta_temp = $"E:/Probando/Recibir/{nombre}";

            if (File.Exists(ruta_temp))
            {
                File.Delete(ruta_temp); // Evitamos problemas de sobreescritura 
            }

            archivosRecibir[orden] = new classArchivo(ruta_temp, new byte[size_archivo], 0, orden);

            archivosRecibir[orden].iniciarFlujo();

            Console.WriteLine("Tamaño del archivo: " + archivosRecibir[orden].bytes.Length);
        }

        private void procesarArchivo()
        {
            recibiendoArchivo = true;

            try
            {
                while (colaArchivos.Count > 0 || archivosRecibir.Any(a => a != null && a.Avance < a.bytes.Length))
                {
                    classPackage paqueteConOrden;
                    lock (colaArchivos)
                    {
                        if (colaArchivos.Count == 0)
                            continue;
                        paqueteConOrden = colaArchivos.Dequeue();
                    }

                    byte[] paquete = paqueteConOrden.Paquete;
                    int orden = paqueteConOrden.Orden;

                    int bytesRestantes = archivosRecibir[orden].bytes.Length - archivosRecibir[orden].Avance;
                    int tamañoPaquete = paquete.Length - 9;

                    int bytesAEscribir = Math.Min(bytesRestantes, tamañoPaquete);

                    if (bytesAEscribir > 0)
                    {
                        archivosRecibir[orden].EscribiendoArchivo.Write(paquete, 9, bytesAEscribir);
                        archivosRecibir[orden].Avance += bytesAEscribir;
                    }

                    UpdateRecibir((archivosRecibir[orden].Avance / (float)archivosRecibir[orden].bytes.Length) * 100, archivosRecibir[orden].Avance, archivosRecibir[orden].bytes.Length);

                    if (archivosRecibir[orden].Avance >= archivosRecibir[orden].bytes.Length)
                    {
                        archivosRecibir[orden].EscribiendoArchivo.Flush();
                        archivosRecibir[orden].EscribiendoArchivo.Close();
                        archivosRecibir[orden].FlujoArchivoRecibir.Close();
                        UpdateUI($"Cliente {Encoding.UTF8.GetString(paquete, 2, 4)} envió un archivo exitosamente.");
                        Console.WriteLine("Archivo recibido correctamente");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en procesarArchivo: {ex.Message}");
            }
            finally
            {
                recibiendoArchivo = false;
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

                    if(comboClientes.SelectedItem == null)
        {
                        MessageBox.Show("Selecciona un cliente primero");
                        return;
                    }

                    string recipientId = ((KeyValuePair<string, string>)comboClientes.SelectedItem).Key;

                    archivosEnviar[contador] = new classArchivo(rutita, bytesImagen, 0, contador);

                    enviarInformacion(recipientId);

                    Console.WriteLine(contador);

                    enviandoArch = new Thread(() => enviarArchivo(recipientId, contador));

                    enviandoArch.Start();

                    contador++;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al enviar: " + ex.Message);
            }
        }

        private void enviarInformacion(string recipientId)
        {

            string size_archivo = archivosEnviar[contador].bytes.Length.ToString("D10");

            string nombre = Path.GetFileName(archivosEnviar[contador].Nombre);

            string name_real = nombre.Split('.')[0];
            string extension = nombre.Split('.')[1];

            nombre = $"{name_real}{number}.{extension}";

            string size_nombre = nombre.Length.ToString("D3");

            string info_total = $"I:{recipientId}:{size_archivo}:{size_nombre}:{nombre}:{contador}";

            Console.WriteLine(info_total);

            byte[] bufferInfo = Encoding.UTF8.GetBytes(info_total);

            number++;

            SocketCliente.Send(bufferInfo);
        }

        private void enviarArchivo(string recipientId,int conta)
        {
            try
            {

                int contador1 = conta - 1;

                byte[] cabeza = Encoding.UTF8.GetBytes($"A:{recipientId}:{contador1}:"); // A:aea1:1:
                int tamaño_imagen = archivosEnviar[contador1].bytes.Length;

                int cantidad_exacta = 1015 * ((int)(tamaño_imagen / 1015));

                for (int i = 0; i < tamaño_imagen; i += 1015)
                {
                    int size = Math.Min(1015, tamaño_imagen - i);
                    byte[] tramaEnviar = new byte[9 + size];

                    archivosEnviar[contador1].Avance += size;

                    Array.Copy(cabeza, 0, tramaEnviar, 0, 9);
                    Array.Copy(archivosEnviar[contador1].bytes, i, tramaEnviar, 9, size);

                    int totalEnviado = 0;
                    while (totalEnviado < tramaEnviar.Length)
                    {
                        int enviado = SocketCliente.Send(tramaEnviar, totalEnviado, tramaEnviar.Length - totalEnviado, SocketFlags.None);
                        if (enviado == 0) throw new Exception("Conexión cerrada durante el envío");
                        totalEnviado += enviado;
                    }

                    if (i == 0)
                    {
                        if (tamaño_imagen < 1015)
                        {
                            UpdateEnvio(100, archivosEnviar[contador1].Avance, tamaño_imagen);
                        }
                        else
                        {
                            UpdateEnvio(0, 0, tamaño_imagen);
                        }

                    }
                    else
                    {
                        UpdateEnvio(((float)i / (float)cantidad_exacta) * 100, archivosEnviar[contador1].Avance, tamaño_imagen);
                    }
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
