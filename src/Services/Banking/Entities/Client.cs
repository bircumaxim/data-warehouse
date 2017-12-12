using System.Collections.Generic;

namespace Banking.Entities
{
    public class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cnp { get; set; }
        public JobStatus JobStatus { get; set; }
        public int Gender { get; set; }
        public List<Payment> Payments { get; set; }
    }
}