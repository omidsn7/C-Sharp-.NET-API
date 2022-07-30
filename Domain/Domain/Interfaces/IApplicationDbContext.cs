using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Domain.Interfaces
{
    public interface IApplicationDbContext
    {
        public IDbConnection Connection { get; }
        DatabaseFacade Database { get; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Department { get; set; }

        Task<int> SaveChangeAsync(CancellationToken cancellationToken);
    }
}
