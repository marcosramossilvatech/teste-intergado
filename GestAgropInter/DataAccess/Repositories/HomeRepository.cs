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

            List<Animal> animais = new List<Animal>();

            animais = _context.Animal.ToList();

            List<Fazenda> fazendas = new List<Fazenda>();
            fazendas = _context.Fazenda.ToList();

            var retorno = (from f in fazendas
                           from a in animais.Where(x => x.FazendaID == f.Id).DefaultIfEmpty()
                           group f by f.Id into g
                           select new Home
                           {
                               Id = g.Key,
                               Titulo = animais.Where(x=> x.FazendaID == g.Key).Count().ToString(),
                               PrimeiroSubtitulo = animais.Where(x => x.FazendaID == g.Key && x.Sexo.StartsWith("F")).Count().ToString(),
                               SegundoSubtitulo = animais.Where(x => x.FazendaID == g.Key && x.Sexo.StartsWith("M")).Count().ToString(),
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
