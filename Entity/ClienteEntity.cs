using apiSorteio.Data;
using apiSorteio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiSorteio.Entity
{
    public class ClienteEntity
    {
        private readonly ApiContext _context;
        Random sort = new Random();
        Sorteio nSorteado = new Sorteio();

        public ClienteEntity(ApiContext context)
        {
            _context = context;
        }
     
        public async Task<string> AddCliente(Cliente cliente)
        {
            Sorteador(cliente);
            CriaTxt(cliente);

            Cliente checkEmail = _context.Clientes.Where(x => x.ClienteEmail == cliente.ClienteEmail).AsNoTracking().FirstOrDefault();

            if (checkEmail != null)
            {
                bool checkDados = Equals(checkEmail.ClienteNome, cliente.ClienteNome)
                == Equals(checkEmail.ClienteCPF, cliente.ClienteCPF) == Equals(checkEmail.ClienteTelefone, cliente.ClienteTelefone);

                if (checkDados)
                {
                    _context.Clientes.Add(cliente);
                    await _context.SaveChangesAsync();

                    return "OK";
                }
                else
                {
                    return "Error";
                }

            }
            else
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                return "OK";
            }
        }
        public int Sorteador(Cliente cliente)
        {
            nSorteado.NumeroSorteado = sort.Next(0, 99999);

            Cliente CheckNumeros = _context.Clientes.Where(x => x.ClienteNumSorteado == nSorteado.NumeroSorteado).AsNoTracking().FirstOrDefault();

            while (CheckNumeros != null)
            {
                nSorteado.NumeroSorteado = sort.Next(0, 99999);
                CheckNumeros = _context.Clientes.Where(x => x.ClienteNumSorteado == nSorteado.NumeroSorteado).AsNoTracking().FirstOrDefault();
            }

            return cliente.ClienteNumSorteado = nSorteado.NumeroSorteado; 
        }
        public void CriaTxt(Cliente cliente)
        {
            //cria arquivo txt com dados do cliente + nº sorteio
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, "DadosSorteio.txt")))
            {
                outputFile.WriteLine("Nome: " + cliente.ClienteNome);
                outputFile.WriteLine("CPF: " + cliente.ClienteCPF);
                outputFile.WriteLine("Email: " + cliente.ClienteEmail);
                outputFile.WriteLine("Telefone: " + cliente.ClienteTelefone);
                outputFile.WriteLine("Número do sorteio: " + cliente.ClienteNumSorteado);
            }

        }
    }
}
