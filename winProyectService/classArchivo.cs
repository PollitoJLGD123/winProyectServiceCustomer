using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace winTwoPlays
{
    public class classArchivo
    {
        public classArchivo(string Nombre, byte[] bytes, int Avance, int orden)
        {
            this.bytes = bytes;
            this.Avance = Avance;
            this.Nombre = Nombre;
            //this.Id = id;
            Orden = orden;
        }
        public string Nombre { get; set; }
        public byte[] bytes { get; set; }
        public int Avance { get; set; }
        public FileStream FlujoArchivoRecibir { get; set; }
        public BinaryWriter EscribiendoArchivo { get; set; }
        //public int Id { get; set; }

        public int Orden { get; set; }

        public void iniciarFlujo()
        {
            FlujoArchivoRecibir = new FileStream(Nombre, FileMode.Create, FileAccess.Write);
            EscribiendoArchivo = new BinaryWriter(FlujoArchivoRecibir);
        }
    }
}
