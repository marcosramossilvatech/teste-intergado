using GestAgropInter.Models;
using GestAgropInter.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestAgropInter.DataAccess.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private AppDbContext _context;
        public AnimalRepository(AppDbContext animalContext)
        {
            _context = animalContext;
        }

        public void AddAnimais(List<Animal> animais)
        {
            _context.AddRange(animais);
            _context.SaveChanges();
        }

        public void AddAnimal(Animal animal)
        {
            _context.Add(animal);
            _context.SaveChanges();
        }

        public void DeleteAnimal(int? id)
        {
            var faze = _context.Animal.Find(id);
            if (faze != null)
            {
                _context.Remove(faze);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Animal> GetAllAnimal()
        {

            var retorno = (from a in _context.Animal
                             from f in _context.Fazenda.Where(x=>x.Id == a.FazendaID).DefaultIfEmpty()
                             select new Animal
                             {
                                 Id = a.Id,
                                 Tag = a.Tag,
                                 Sexo = a.Sexo,
                                 FazendaID = a.FazendaID,
                                 Fazenda = f
                             }).ToList();
            return retorno;
        }

        public Animal GetAnimal(int? id)
        {
            var animal = (from a in _context.Animal
                             from f in _context.Fazenda.Where(x => x.Id == a.FazendaID).DefaultIfEmpty()
                             where a.Id== id
                             select new Animal
                             {
                                 Id = a.Id,
                                 Tag = a.Tag,
                                 Sexo = a.Sexo,
                                 FazendaID = a.FazendaID,
                                 Fazenda = f
                             }).FirstOrDefault();

            return animal;
        }

        public Animal GetAnimal(string? tag)
        {
            var animal = _context.Animal.FirstOrDefault(x=> x.Tag == tag);
            return animal;
        }
        public void UpdateAnimal(Animal animal)
        {
            _context.Update(animal);
            _context.SaveChanges();
        }
    }
}
