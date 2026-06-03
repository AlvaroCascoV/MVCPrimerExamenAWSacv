using Microsoft.AspNetCore.Mvc;
using MVCPrimerExamenAWSacv.Models;
using MVCPrimerExamenAWSacv.Repositories;
using MVCPrimerExamenAWSacv.Services;

namespace MVCPrimerExamenAWSacv.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repo;
        private ServiceStorageS3 service;

        public ZapatillasController(RepositoryZapatillas repo, ServiceStorageS3 service)
        {
            this.repo = repo;
            this.service = service;
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
        public async Task<IActionResult> Create(Zapatilla zapas, IFormFile imagenArchivo)
        {
            int codigo = 0;

            using (Stream stream = imagenArchivo.OpenReadStream())
            {
                codigo = await this.service.UploadFileAsync
                (imagenArchivo.FileName, stream);
            }

            
            await this.repo.InsertZapatillaAsync(zapas.Nombre, zapas.Descripcion, imagenArchivo.FileName);
            return RedirectToAction("Index");
        }
    }
}
