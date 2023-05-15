using ControleDeDividas.Core.Data;
using ControleDeDividas.Domain.Interfaces;
using ControleDeDividas.Domain.Models;
using ControleDeDividas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControleDeDividas.Infra.Data.Repository
{
    public class DividaRepository : IDividaRepository
    {
        private readonly SystemContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public DividaRepository(SystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Adicionar(Divida divida)
        {
            _context.Dividas.Add(divida);
        }

        public void Atualizar(Divida divida)
        {
            _context.Dividas.Update(divida);
        }

        public async Task<Divida> ObterPorId(Guid id)
        {
            return await _context.Dividas
                .Include(d => d.Parcelas)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id.Equals(id));
        }

        public async Task<Divida> ObterPorCpf(string cpf)
        {
            return await _context.Dividas
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.CpfDoDevedor.Equals(cpf));
        }

        public async Task<List<Divida>> ObterListagem()
        {
            return await _context.Dividas
                .Include(d => d.Parcelas)
                .AsNoTracking()
                .ToListAsync();
        }

        public void Remover(Divida divida)
        {
            _context.Dividas.Remove(divida);
        }

        public void RemoverParcelas(ICollection<Parcela> parcelas)
        {
            _context.Parcelas.RemoveRange(parcelas);
        }

        public void AdicionarParcelas(ICollection<Parcela> parcelas)
        {
            _context.Parcelas.AddRange(parcelas);
        }
    }
}
