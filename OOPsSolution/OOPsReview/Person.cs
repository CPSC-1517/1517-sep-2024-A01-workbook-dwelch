﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public class Person
    {
        private string _FirstName;
        private string _LastName;
        public string FirstName 
        {
            get { return _FirstName; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("FirstName", "First Name cannot be empty or blank");
                _FirstName = value.Trim();
            }
        }
        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("LastName", "Last Name cannot be empty or blank");
                _LastName = value.Trim();
            }
        }
        public ResidentAddress Address { get; set; }
        public List<Employment> EmploymentPositions { get; set; } = new List<Employment>();

        public string FullName { get { return LastName + ", " + FirstName; } }
        public Person()
        {
            FirstName = "Unknown";
            LastName = "Unknown";
           
        }
        public Person(string firstname, string lastname, ResidentAddress address, List<Employment> employmentpositions)
        {
          
            FirstName = firstname;
            LastName = lastname;
            Address = address;
            if (employmentpositions != null)
            {
                EmploymentPositions = employmentpositions;
               
            }
            
        }

        public void ChangeFullName(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }

        public void AddEmployment(Employment position)
        {
            if (position == null)
                throw new ArgumentNullException("Employment Position", "Position data is required");
            EmploymentPositions.Add(position);
        }
    }
}
