using GestAgropInter.Models;
using GestAgropInter.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GestAgropInter.Controllers
{
    public class AnimalController : Controller
    {
        private readonly IAnimalRepository _animal;
        private readonly IFazendaRepository _fazenda;

        Animal ani = new Animal();
        public AnimalController(IAnimalRepository animal, IFazendaRepository fazenda)
        {
            _animal = animal;
            _fazenda = fazenda;
        }

        public IActionResult Index()
        {
            var animais = _animal.GetAllAnimal();

            return View(animais);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Animal animal = _animal.GetAnimal(id);

            if (animal == null)
            {
                return NotFound();
            }
            animal.FazendaList = _fazenda.GetAllFazenda();
            return View(animal);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ani = new Animal();
            ani.FazendaList = _fazenda.GetAllFazenda();
            return View(ani);
        }
        [HttpGet]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCadastro([Bind] Animal animal)
        {
        

            if (ModelState.IsValid)
            {
                if (animal.Id != null)
                {
                    animal.DataAlteracao = DateTime.Now;
                    _animal.UpdateAnimal(animal);
                }
                else
                    _animal.AddAnimal(animal);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Animal animal = _animal.GetAnimal(id);
           

            if (animal == null)
            {
                return NotFound();
            }
            animal.FazendaList = _fazenda.GetAllFazenda();
            return View(animal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _animal.UpdateAnimal(animal);
                return RedirectToAction("Index");
            }
            return View(animal);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Animal animal = _animal.GetAnimal(id);

            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            _animal.DeleteAnimal(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult VerificaTag(string tag)
        {

            if (tag == null)
            {
                return NotFound();
            }

            Animal animal = _animal.GetAnimal(tag);
            string mensagem = "yes";
            if (animal == null)
                mensagem = "not";
            return Ok(new
            {
                mensagem = mensagem
            });
        }

        [HttpPost]
        public IActionResult SalvarListaAnimais([FromBody] List<Animal> animais)
        {
            foreach (var animal in animais)
            {
                
                if (animal.Tag == "" || animal.Tag.Length < 15)
                    return Ok(new { erro = "true", message = "A tag informada é invalida!"});
                Animal exisAnimal = _animal.GetAnimal(animal.Tag);
                if (exisAnimal != null)
                    return Ok(new { erro = "true", message = "Já existe uma animal cadastrado com esse tag" });
            }
            try
            {
                _animal.AddAnimais(animais);
                return Ok(new { erro = "false", message = "Cadastro realizado com sucesso!!!" });
            }
            catch (Exception)
            {
                return BadRequest("Aconteceu algum erro!");
                throw;
            }
            
           
        }

    }
}