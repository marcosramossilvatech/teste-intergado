namespace GestAgropInter.Models.Interfaces
{
    public interface IFazendaRepository
    {
        IEnumerable<Fazenda> GetAllFazenda();
        void AddFazenda(Fazenda fazenda);
        void UpdateFazenda(Fazenda fazenda);
        Fazenda GetFazenda(int? id);

        Fazenda GetFazenda(string nome);
        void DeleteFazenda(int? id);
    }
}
