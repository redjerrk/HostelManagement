﻿namespace Web.MVC.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
        public int? HostelId { get; set; }
    }
}
