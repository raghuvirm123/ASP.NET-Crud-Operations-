﻿namespace ASPNETCRUDO.Models
{
    public class UpadateEmployeeViewModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public long Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Department { get; set; }
    }
}
