using Microsoft.EntityFrameworkCore;

namespace Banking.Data.Repository
{
    public class PaymentRepoistory : Repository<Entitites.Payment>, IPaymentRepository
    {
        public PaymentRepoistory(DbContext context) : base(context)
        {
        }
    }
}