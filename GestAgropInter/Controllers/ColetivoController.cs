using GestAgropInter.Models;
using GestAgropInter.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data.OleDb;
using System.Data;
using System.Diagnostics;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace GestAgropInter.Controllers
{
    public class ColetivoController : Controller
    {
        private readonly IAnimalRepository _animal;
        private readonly IFazendaRepository _fazenda;
        private IHostingEnvironment Environment;
        private IConfiguration Configuration;
        Animal ani = new Animal();

        public ColetivoController(IAnimalRepository animal, IFazendaRepository fazenda, IHostingEnvironment _environment, IConfiguration _configuration)
        {
            _animal = animal;
            _fazenda = fazenda;
            Environment = _environment;
            Configuration = _configuration;
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
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
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

                string conString = this.Configuration.GetConnectionString("ExcelConString");
                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }
                List<Animal> listaAnimais = new List<Animal>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    if(dataRow["Fazenda"].ToString() != "")
                    {
                        Fazenda faze = _fazenda.GetFazenda(dataRow["Fazenda"].ToString());
                        if (faze == null)
                        {
                            faze = new Fazenda(dataRow["Fazenda"].ToString(), "Cadastrado pelo coletivo");
                            _fazenda.AddFazenda(faze);
                        }
                        Animal animal = new Animal(dataRow["Tag"].ToString(), dataRow["Sexo"].ToString(), (int)faze.Id);
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