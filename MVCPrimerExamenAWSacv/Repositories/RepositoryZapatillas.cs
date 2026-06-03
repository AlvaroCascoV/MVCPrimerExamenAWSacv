using Microsoft.EntityFrameworkCore;
using MVCPrimerExamenAWSacv.Data;
using MVCPrimerExamenAWSacv.Models;

namespace MVCPrimerExamenAWSacv.Repositories
{
    public class RepositoryZapatillas
    {
        private ZapatillasContext context;

        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }

        public async Task<List<Zapatilla>> GetZapatillasAsync()
        {
            List<Zapatilla> zapatillas = await this.context.Zapatillas.ToListAsync();
            return zapatillas;
        }

        public async Task<Zapatilla> FindZapatillaAsync(int id)
        {
            Zapatilla zapatilla = await this.context.Zapatillas.FirstOrDefaultAsync(z => z.IdProducto == id);
            return zapatilla;
        }

        private async Task<int> GetMaxIdAsync()
        {
            int maxId = 0;
            maxId = await this.context.Zapatillas.MaxAsync(z => z.IdProducto);
            return maxId;
        }

        public async Task InsertZapatillaAsync(string nombre, string descripcion, string imagen)
        {
            int maxId = await GetMaxIdAsync();
            int newId = maxId + 1;

            Zapatilla zapatilla = new Zapatilla
            {
                IdProducto = newId,
                Nombre = nombre,
                Descripcion = descripcion,
                Imagen = imagen
            };
            this.context.Zapatillas.Add(zapatilla);
            await this.context.SaveChangesAsync();
        }
    }
}
