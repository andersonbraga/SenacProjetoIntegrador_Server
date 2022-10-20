using Dapper;
using System.Data;

namespace Controle_Estoque.Data
{
    public class ProdutoService : IProdutoService
    {
        private readonly IDapperDal _dapperDal;
        public ProdutoService(IDapperDal dapperDal)
        {
            this._dapperDal = dapperDal;
        }

        public Task<int> Create(Produto produto)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("nome", produto.nome, DbType.String);
            dbPara.Add("descricao", produto.descricao, DbType.String);
            dbPara.Add("imagem", produto.imagem, DbType.String);
            dbPara.Add("preco", produto.preco, DbType.Decimal);
            dbPara.Add("estoque", produto.estoque, DbType.Int32);

            var produtoId = Task.FromResult(_dapperDal.Insert<int>("[dbo].[SP_Novo_Produto]",
                dbPara,
                commandType: CommandType.StoredProcedure));

            return produtoId;
        }
        public Task<Produto> GetById(int id)
        {
#pragma warning disable CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
            var produto = Task.FromResult(_dapperDal.Get<Produto>($"select * from [Produtos] where ProdutoId = {id}", null,
                    commandType: CommandType.Text));
#pragma warning restore CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
            return produto;
        }
        public Task<int> Delete(int id)
        {
#pragma warning disable CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
            var deleteProduto = Task.FromResult(_dapperDal.Execute($"Delete [Produtos] where ProdutoId = {id}", null,
                    commandType: CommandType.Text));
#pragma warning restore CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
            return deleteProduto;
        }
        public Task<List<Produto>> ListAll()
        {
#pragma warning disable CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
            var produtos = Task.FromResult(_dapperDal.GetAll<Produto>("select * from [Produtos]", null, commandType: CommandType.Text));
#pragma warning restore CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
            return produtos;
        }
        public Task<int> Update(Produto produto)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("ProdutoId", produto.id_produto);
            dbPara.Add("Nome", produto.nome, DbType.String);
            dbPara.Add("Descricao", produto.descricao, DbType.String);
            dbPara.Add("Imagem", produto.imagem, DbType.String);
            dbPara.Add("Preco", produto.preco, DbType.Decimal);
            dbPara.Add("Estoque", produto.estoque, DbType.Int32);

            var updateProduto = Task.FromResult(_dapperDal.Update<int>("[dbo].[SP_Atualiza_Produto]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateProduto;
        }
    }
}
