using System;
using System.Collections.Generic;
using System.Text;
//using AspNetCoreHero.Abstractions;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace Domain.Entities
{
    public class Employee : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
