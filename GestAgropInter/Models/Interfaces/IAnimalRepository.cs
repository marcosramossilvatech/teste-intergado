namespace GestAgropInter.Models.Interfaces
{
    public interface IAnimalRepository
    {
        IEnumerable<Animal> GetAllAnimal();
        void AddAnimal(Animal Animal);
        void AddAnimais(List<Animal> Animais);
        void UpdateAnimal(Animal Animal);
        Animal GetAnimal(int? id);
        Animal GetAnimal(string tag);
        void DeleteAnimal(int? id);
    }
}
