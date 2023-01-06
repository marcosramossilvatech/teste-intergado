using GestAgropInter.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GestAgropInter.DataAccess.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private AppDbContext _context;
        public HomeRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Home> GetDados()
        {

            List<Animal> dados = new List<Animal>();
            dados.AddRange(new[] {
                new Animal(1, "123456789123456", "Masculino", 1,null),
                new Animal(2, "123456789123457", "Masculino", 1,null),
                new Animal(3, "123456789123458", "Feminino", 1,null),
                new Animal(4, "123456789123459", "Feminino", 1,null),
                new Animal(5, "123456789123451", "Feminino", 1,null),
                new Animal(6, "123456789123452", "Masculino", 2,null),
                new Animal(7, "123456789123453", "Masculino", 2,null),
                new Animal(8, "123456789123454", "Feminino", 2,null),
                new Animal(9, "123456789123455", "Feminino", 2,null),
                new Animal(10, "123456789123441", "Feminino", 2,null)

            });

            List<Fazenda> fazendas = new List<Fazenda>();
            fazendas.AddRange(new[] {
                new Fazenda(1, "Fazenda 1"),
                new Fazenda(2, "Fazenda 2")
            });

            var retorno = (from f in fazendas
                           from a in dados.Where(x => x.FazendaID == f.Id).DefaultIfEmpty()
                           group f by f.Id into g
                           select new Home
                           {
                               Id = g.Key,
                               Titulo = "Total de animais " + dados.Where(x=> x.FazendaID == g.Key).Count().ToString(),
                               PrimeiroSubtitulo = dados.Where(x => x.FazendaID == g.Key && x.Sexo.StartsWith("F")).Count().ToString(),
                               SegundoSubtitulo =  dados.Where(x => x.FazendaID == g.Key && x.Sexo.StartsWith("M")).Count().ToString(),
                               Rodape = fazendas.Where(x=> x.Id == g.Key).FirstOrDefault().NomeFazenda,
                               ClasseCorFundo = "small-box bg-"+ RetornaCor(g.Key)
                           }).ToList();

            return retorno;
        }

        private string RetornaCor(int? key)
        {
            string retorno = "";

            int? valor = key;
            if(valor> 4)
                valor = valor % 2 == 2 ? 2 : 4;

            switch (valor)
            {
                case 1:
                    return  "info";
                    break;
                case 2:
                    return  "success";
                    break;
                case 3:
                    return "warning";
                    break;
                case 4:
                    return  "danger";
                    break;
                default:
                    return  "info";
                    break;
            }
        }
    }
}
