using Microsoft.AspNetCore.Mvc;
using MVCPrimerExamenAWSacv.Models;
using MVCPrimerExamenAWSacv.Repositories;

namespace MVCPrimerExamenAWSacv.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repo;

        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Zapatilla> zapas = await this.repo.GetZapatillasAsync();
            return View(zapas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Zapatilla zapas)
        {
            await this.repo.InsertZapatillaAsync(zapas.Nombre, zapas.Descripcion, zapas.Imagen);
            return RedirectToAction("Index");
        }
    }
}
