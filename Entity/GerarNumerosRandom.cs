using apiSorteio.Data;
using apiSorteio.Models;
using Microsoft.EntityFrameworkCore;

namespace apiSorteio.Entity
{
    public class GerarNumerosRandom
    {
        private readonly ApiContext _context;

        public GerarNumerosRandom()
        {
        }

        public GerarNumerosRandom(ApiContext context)
        {
            _context = context;
        }

        public int Sorteador(Cliente cliente)
        {
            Random sort = new Random();
            int numeroSorteado = sort.Next(0, 99999);

            Cliente CheckNumeros = _context.Clientes.Where(x => x.ClienteNumSorteado == numeroSorteado).AsNoTracking().FirstOrDefault();

            while (CheckNumeros != null)
            {
                numeroSorteado = sort.Next(0, 99999);
                CheckNumeros = _context.Clientes.Where(x => x.ClienteNumSorteado == numeroSorteado).AsNoTracking().FirstOrDefault();
            }

            return cliente.ClienteNumSorteado = numeroSorteado;
        }
    }
}
