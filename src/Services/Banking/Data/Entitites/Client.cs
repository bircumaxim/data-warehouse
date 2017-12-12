﻿using System;

namespace Banking.Data.Entitites
{
   public class Client : IEntity
   {
      public Guid Id { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Cnp { get; set; }

      public Client()
      {
         
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