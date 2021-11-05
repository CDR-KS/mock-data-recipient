using CDR.DataRecipient.SDK.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDR.DataRecipient.Repository.SQL
{
    public class SqlDataHoldersRepository : IDataHoldersRepository
    {
        protected readonly IConfiguration _config;

        public SqlDataAccess _sqlDataAccess { get; }

        public SqlDataHoldersRepository(IConfiguration config)
        {
            _config = config;
            _sqlDataAccess = new SqlDataAccess(_config);
        }

        public async Task<DataHolderBrand> GetDataHolderBrand(string brandId)
        {            
            var dataHolderBrand = await _sqlDataAccess.GetDataHolderBrand(brandId);            
            return dataHolderBrand;
        }

        public async Task<IEnumerable<DataHolderBrand>> GetDataHolderBrands()
        {            
            var dataHolderBrands = await _sqlDataAccess.GetDataHolderBrands();
            return dataHolderBrands;
        }

        public async Task PersistDataHolderBrands(IEnumerable<DataHolderBrand> dataHolderBrands)
        {            
            //Delete existing data first then add all data
            await _sqlDataAccess.PersistDataHolderBrands(dataHolderBrands);
        }
    }
}
