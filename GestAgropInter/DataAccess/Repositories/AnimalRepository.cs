using GestAgropInter.Models;
using GestAgropInter.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestAgropInter.DataAccess.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private AppDbContext _animalContext;
        public AnimalRepository(AppDbContext animalContext)
        {
            _animalContext = animalContext;
        }
        public void AddAnimal(Animal animal)
        {
            _animalContext.Add(animal);
            _animalContext.SaveChanges();
        }

        public void DeleteAnimal(int? id)
        {
            var faze = _animalContext.Animal.Find(id);
            if (faze != null)
            {
                _animalContext.Remove(faze);
                _animalContext.SaveChanges();
            }
        }

        public IEnumerable<Animal> GetAllAnimal()
        {
            var retorno = _animalContext.Animal.ToList();
            return retorno;
        }

        public Animal GetAnimal(int? id)
        {
            var animal = _animalContext.Animal.Find(id);
            return animal;
        }

        public Animal GetAnimal(string? tag)
        {
            var animal = _animalContext.Animal.FirstOrDefault(x=> x.Tag == tag);
            return animal;
        }
        public void UpdateAnimal(Animal animal)
        {
            _animalContext.Update(animal);
            _animalContext.SaveChanges();
        }
    }
}
