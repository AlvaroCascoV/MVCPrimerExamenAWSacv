using Microsoft.EntityFrameworkCore;
using MVCPrimerExamenAWSacv.Models;

namespace MVCPrimerExamenAWSacv.Data
{
    public class ZapatillasContext : DbContext  
    {
        public ZapatillasContext(DbContextOptions<ZapatillasContext> options) : base(options)
        {
        }
        public DbSet<Zapatilla> Zapatillas { get; set; }
    }
}
