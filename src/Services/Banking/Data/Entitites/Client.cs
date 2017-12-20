using System;
using System.Collections.Generic;

namespace Banking.Data.Entitites
{
   public class Client : IEntity
   {
      public Guid Id { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string ImageUri { get; set; }
      public string Cnp { get; set; }
      public ICollection<Payment> Payments { get; set; }

      public Client()
      {         
         Payments = new List<Payment>();
      }
      
      public Client(Guid id, string firstName, string lastName, string cnp)
      {
         Id = id;
         FirstName = firstName;
         LastName = lastName;
         Cnp = cnp;
      }
   }
}