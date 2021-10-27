using CDR.DataRecipient.SDK.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDR.DataRecipient.Repository.SQL
{
    public class SqlRegistrationsRepository : IRegistrationsRepository
    {
        protected readonly IConfiguration _config;

        public SqlDataAccess _sqlDataAccess { get; }

        public SqlRegistrationsRepository(IConfiguration config) 
        {
            _config = config;
            _sqlDataAccess = new SqlDataAccess(_config);
        }

        public async Task<Registration> GetRegistration(string clientId)
        {                        
            var registration = await _sqlDataAccess.GetRegistration(clientId);
            return registration;            
        }

        public async Task<IEnumerable<Registration>> GetRegistrations()
        {            
            var registration = await _sqlDataAccess.GetRegistrations();
            return registration;
        }

        public async Task DeleteRegistration(string clientId)
        {                        
            var registration = await GetRegistration(clientId);

            //Delete existing data. 
            if (!string.IsNullOrEmpty(registration?.ClientId))
            {
                await _sqlDataAccess.DeleteRegistration(clientId);
            }
        }

        public async Task PersistRegistration(Registration _registration)
        {            
            var registration = await GetRegistration(_registration.ClientId);

            if (string.IsNullOrEmpty(registration?.ClientId))
            {
                await _sqlDataAccess.InsertRegistration(_registration);
            }
            return;
        }

        public async Task UpdateRegistration(Registration registration)
        {            
            var _registration = await GetRegistration(registration.ClientId);

            //Update existing data. 
            if (!string.IsNullOrEmpty(_registration?.ClientId))
            {
                await _sqlDataAccess.UpdateRegistration(registration);
            }
        }
    }
}
