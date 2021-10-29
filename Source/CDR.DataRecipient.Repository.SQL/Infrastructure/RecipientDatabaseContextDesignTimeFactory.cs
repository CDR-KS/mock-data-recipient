using CDR.DataRecipient.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace CDR.DataRecipient.Repository.SQL.Infrastructure
{
    public class RecipientDatabaseContextDesignTimeFactory : IDesignTimeDbContextFactory<RecipientDatabaseContext>
    {
        public RecipientDatabaseContextDesignTimeFactory()
        {
            // A parameter-less constructor is required by the EF Core CLI tools.
        }

        public RecipientDatabaseContext CreateDbContext(string[] args)
        {
			// Copy the connection string into here to create the Initial Migration using EFCore tools
			// eg. Server=(localdb)\\MSSQLLocalDB;Database=cdr-mdr;Integrated Security=true
			// From Package Manager Console > Select Repository as the start up project
			// Delete any existing migrations and ContextModelSnapshots
			// execute Add-Migration "Init"
			// database will be created and seeded when solution is started
			var connectionString = "";
			if (string.IsNullOrEmpty(connectionString))
			{
				throw new InvalidOperationException("The connection string was not set in the CreateDbContext() method.");
			}
			var options = new DbContextOptionsBuilder<RecipientDatabaseContext>().UseSqlServer(connectionString).Options;
			return new RecipientDatabaseContext(options);
		}
	}
}