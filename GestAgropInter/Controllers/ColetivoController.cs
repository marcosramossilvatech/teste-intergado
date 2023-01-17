using GestAgropInter.Models;
using GestAgropInter.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data.OleDb;
using System.Data;
using System.Diagnostics;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Http;
using System.Formats.Asn1;
using System.Globalization;


namespace GestAgropInter.Controllers
{
    public class ColetivoController : Controller
    {
        private readonly IAnimalRepository _animal;
        private readonly IFazendaRepository _fazenda;
        private IHostingEnvironment Environment;
        private IConfiguration Configuration;
        Animal ani = new Animal();
        IWebHostEnvironment _appEnvironment;

        public ColetivoController(IAnimalRepository animal, IFazendaRepository fazenda, IHostingEnvironment _environment, IConfiguration _configuration, IWebHostEnvironment env)
        {
            _animal = animal;
            _fazenda = fazenda;
            Environment = _environment;
            Configuration = _configuration;
            _appEnvironment = env;
        }

        public IActionResult Index()
        {
            ViewBag.Fazenda = _fazenda.GetAllFazenda();
            return View();
        }
                

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult ImportarArquivo(IFormFile postedFile)
        {
            if (postedFile != null)
            {


                string path = Path.Combine(_appEnvironment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                string csvData = System.IO.File.ReadAllText(filePath);
                DataTable dt = new DataTable();
                bool firstRow = true;
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            if (firstRow)
                            {
                                foreach (string cell in row.Split(';'))
                                {
                                    dt.Columns.Add(cell.Trim());
                                }
                                firstRow = false;
                            }
                            else
                            {
                                dt.Rows.Add();
                                int i = 0;
                                foreach (string cell in row.Split(';'))
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = cell.Trim();
                                    i++;
                                }
                            }
                        }
                    }
                }
                List<Animal> listaAnimais = new List<Animal>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    if (dataRow["Fazenda"].ToString() != "")
                    {
                        Fazenda faze = _fazenda.GetFazenda(dataRow["Fazenda"].ToString());
                        if (faze == null)
                        {
                            faze = new Fazenda(dataRow["Fazenda"].ToString(), "Cadastrado pelo coletivo");
                            _fazenda.AddFazenda(faze);
                        }
                        string sexoAjustado = dataRow["Sexo"].ToString().StartsWith("F") ? "Fêmea" : dataRow["Sexo"].ToString();
                        Animal animal = new Animal(dataRow["Tag"].ToString(), sexoAjustado, (int)faze.Id);
                        listaAnimais.Add(animal);
                    }

                }

                if (listaAnimais.Count() > 0)
                    _animal.AddAnimais(listaAnimais);
            }
            return RedirectToAction("Index","Animal");
        }
    }
}