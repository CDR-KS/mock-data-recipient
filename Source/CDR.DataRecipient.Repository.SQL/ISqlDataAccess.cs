using CDR.DataRecipient.SDK.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDR.DataRecipient.Repository.SQL
{
    public interface ISqlDataAccess
    {
        bool SqlCreateDatabase();        
        bool RecreateDatabaseWithForTests();
        Task DeleteCdrArrangementData();
        Task DeleteRegistrationData();        
        Task<DataHolderBrand> GetDataHolderBrand(string brandId);
        Task<IEnumerable<DataHolderBrand>> GetDataHolderBrands();
        Task PersistDataHolderBrands(IEnumerable<DataHolderBrand> dataHolderBrands);
    }
}