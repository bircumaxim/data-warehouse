using Banking.Data.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Banking.Data.Repository
{
    public class ClientRepository : Repository<Client> , IClientRepository
    {
        public ClientRepository(DbContext context) : base(context)
        {
        }
    }
}