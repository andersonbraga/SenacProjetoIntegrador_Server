
namespace Controle_Estoque.Data

{
    public interface IProdutoService
    {
        Task<int> Create(Produto produto);
        Task<int> Delete(int id);  
        Task<int>Update (Produto produto);
        Task<Produto> GetById(int id);
        Task<List<Produto>> ListAll();
    }
}