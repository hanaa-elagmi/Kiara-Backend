using e_commerce.Context;
using e_commerce.Entities;
using e_commerce.Interfaces;

namespace e_commerce.Repositories
{
    public class PaymentRepo:Ipayment
    {
        private readonly StoreContext db;

        public PaymentRepo( StoreContext db)
        {
            this.db = db;
        }

        public void Payment(Payment payment)
        {
            db.Add(payment);
            db.SaveChanges();
        }

        
    }
}
