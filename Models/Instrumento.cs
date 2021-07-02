using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Models
{
    public class Instrumento
    {
        //prop tab para crear propiedades
        //Ctrl+R Ctrl+R para refactorizar
        //Para poder crear migraciones
        //dotnet tool install --global dotnet-ef 
        //dotnet-ef migrations add InitrialCreate
        //dotnet ef database update

        public long Id { get; set; }
        public string instrumento { get; set; }
        public string  marca { get; set; }
        public string modelo { get; set; }
        public string imagen { get; set; }
        public double precio { get; set; }
        public string costoEnvio { get; set; }
        public int cantidadVendida { get; set; }
        public string descripcion { get; set; }


    }
}
