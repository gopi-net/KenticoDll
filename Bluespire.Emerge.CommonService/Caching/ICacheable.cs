using System.Data;

namespace Bluespire.Emerge.CommonService.Caching
{
    public interface ICacheable
    {
        string Key{get; set;}
        DataSet GetData();
    }
}
