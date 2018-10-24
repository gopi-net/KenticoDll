using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Components.StaffDirectory.Services
{
    /// <summary>
    /// Staff Directory service interface
    /// </summary>
   public interface IStaffDirectoryService
    {
       /// <summary>
       /// Search Physician by parameter where condition
       /// </summary>
       /// <param name="wherecondition">where condtion</param>
       /// <returns>Physician record in dataset</returns>
       DataSet GetPhysicianByCriteria(string wherecondition);

       /// <summary>
       /// Accept Item id and result result in Dataset format
       /// </summary>
       /// <param name="itemId">interger item id</param>
       /// <returns>physician records in dataset format</returns>
       DataSet GetPhysicianByItemId(int itemId);
    }
}
