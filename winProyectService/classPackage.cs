using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winProyectService
{
    class classPackage
    {
        public byte[] Paquete { get; set; }
        public int Orden { get; set; }

        public classPackage(byte[] paquete, int orden)
        {
            Paquete = paquete;
            Orden = orden;
        }
    }
}
