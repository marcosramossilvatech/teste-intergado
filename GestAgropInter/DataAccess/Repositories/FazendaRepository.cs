using GestAgropInter.Models;
using GestAgropInter.Models.Interfaces;

namespace GestAgropInter.DataAccess.Repositories
{
    public class FazendaRepository : IFazendaRepository
    {
        private AppDbContext _fazendaContext;

        public FazendaRepository(AppDbContext fazendaContext)
        {
            _fazendaContext = fazendaContext;
        }
        public void AddFazenda(Fazenda fazenda)
        {
            _fazendaContext.Add(fazenda);
            _fazendaContext.SaveChanges();
        }

        public void DeleteFazenda(int? id)
        {
            var faze = _fazendaContext.Fazenda.Find(id);
            if(faze != null)
            {
                _fazendaContext.Remove(faze);
                _fazendaContext.SaveChanges();
            }
              
        }

        public IEnumerable<Fazenda> GetAllFazenda()
        {
            return _fazendaContext.Fazenda.ToList();
        }

        public Fazenda GetFazenda(int? id)
        {
            var fazenda =  _fazendaContext.Fazenda.Find(id);
            return fazenda;
        }

        public void UpdateFazenda(Fazenda fazenda)
        {
            _fazendaContext.Update(fazenda);
            _fazendaContext.SaveChanges();
        }
    }
}
