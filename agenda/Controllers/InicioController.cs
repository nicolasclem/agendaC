using agenda.Data;
using agenda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace agenda.Controllers
{
    public class InicioController : Controller
    {
        
        private readonly ApplicationDbContext context;

        public InicioController(ApplicationDbContext context)
        {
            
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View( await context.Contactos.ToListAsync());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Crear(Contacto contacto)
        {
            if(ModelState.IsValid)
            {

                var nuevoContacto = new Contacto();
                nuevoContacto.Nombre  = contacto.Nombre;
                nuevoContacto.Celular = contacto.Celular;
                nuevoContacto.Email = contacto.Email;
                nuevoContacto.FechaCreacion = DateTime.Now;

                
                context.Contactos.Add(nuevoContacto);
                await context.SaveChangesAsync();   
                return RedirectToAction("Index");   
            }

            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if(id== null)
            {
                return NotFound();  
            }
            var contacto = context.Contactos.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
           
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Editar(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                context.Contactos.Update(contacto);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }
            var contacto = context.Contactos.Find(id);
            if(contacto == null) 
            {
                NotFound();
            }
            return View(contacto);
        }
        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contacto = context.Contactos.Find(id);
            if (contacto == null)
            {
                NotFound();
            }
            return View(contacto);
        }


        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarContacto(int? id)
        {
            var contacto = await context.Contactos.FindAsync(id);
            if(contacto == null) 
            {
                return View();
            }
            //Borrado
            context.Contactos.Remove(contacto);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");   
        }
        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}