using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Models
{
    public class InstrumentoContext: DbContext //this is the database context
    {
        public InstrumentoContext(DbContextOptions<InstrumentoContext> options): base(options)
        {
        }

        public DbSet<Instrumento> Instrumentos { get; set; }
    }
}
