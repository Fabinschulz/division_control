using DivisionControl.Core.Data;
using DivisionControl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivisionControl.Domain.Interfaces
{
    public interface IDividaRepository : IRepository<Divida>
    {
        void Adicionar(Divida divida);
        void Atualizar(Divida divida);
        void Remover(Divida divida);
        void RemoverParcelas(ICollection<Parcela> parcelas);
        void AdicionarParcelas(ICollection<Parcela> parcelas);
        Task<Divida> ObterPorId(Guid id);
        Task<Divida> ObterPorCpf(string cpf);
        Task<List<Divida>> ObterListagem();
    }
}
