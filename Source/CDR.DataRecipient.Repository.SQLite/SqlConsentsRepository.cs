using CDR.DataRecipient.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDR.DataRecipient.Repository.SQL
{
    public class SqlConsentsRepository : IConsentsRepository
    {
        protected readonly IConfiguration _config;

        public SqlDataAccess _sqlDataAccess { get; }

        public SqlConsentsRepository(IConfiguration config)
        {
            _config = config;
            _sqlDataAccess = new SqlDataAccess(_config);
        }
        public async Task<ConsentArrangement> GetConsent(string cdrArrangementId)
        {            
            var dataHolderBrand = await _sqlDataAccess.GetConsent(cdrArrangementId);
            return dataHolderBrand;
        }

        public async Task<IEnumerable<ConsentArrangement>> GetConsents()
        {                        
            var cdrArrangements = await _sqlDataAccess.GetConsents();
            return cdrArrangements.OrderByDescending(x => x.CreatedOn);
        }

        public async Task PersistConsent(ConsentArrangement consentArrangement)
        {                                   
            var existingArrangement = await GetConsent(consentArrangement.CdrArrangementId);
            if (existingArrangement == null)
            {
                
                await _sqlDataAccess.InsertCdrArrangement(consentArrangement);
                return;
            }
            
            await _sqlDataAccess.UpdateCdrArrangement(consentArrangement);            
        }

        public async Task UpdateTokens(string cdrArrangementId, string idToken, string accessToken, string refreshToken)
        {                        
            var consent = await GetConsent(cdrArrangementId);
            consent.IdToken = idToken;
            consent.AccessToken = accessToken;
            consent.RefreshToken = refreshToken;
            
            await _sqlDataAccess.UpdateCdrArrangement(consent);
        }

        public async Task DeleteConsent(string cdrArrangementId)
        {                        
            var consent = await GetConsent(cdrArrangementId);

            if (!string.IsNullOrEmpty(consent?.CdrArrangementId))
            {
                await _sqlDataAccess.DeleteConsent(cdrArrangementId);
            }            
        }

        public async Task<bool> RevokeConsent(string cdrArrangementId, string dataHolderBrandId)
        {                        
            var consent = await GetConsent(cdrArrangementId);
            
            if (!string.IsNullOrEmpty(consent?.CdrArrangementId) && 
                string.Equals(consent?.DataHolderBrandId, dataHolderBrandId, StringComparison.OrdinalIgnoreCase))
            {
                await _sqlDataAccess.DeleteConsent(cdrArrangementId);
                return true;
            }
            return false;
        }        
    }
}
