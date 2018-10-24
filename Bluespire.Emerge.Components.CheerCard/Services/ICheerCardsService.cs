using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Components.CheerCard.Services
{
    /// <summary>
    /// Cheer card interface.
    /// </summary>
    public interface ICheerCardsService
    {
         DataSet GetCheerCardImagesByCriteria(string WhereCondition);
         DataSet GetCheerCardRequestItemByID(int ID);
         DataSet GetCheerCardCategoriesByCriteria(string WhereCondition);
         DataSet GetCheerCardMessageConfigurationsByCriteria(string WhereCondition);
         string CreateAndSaveCheerCardAttachmentImage(Dictionary<string, object> FormParameters, string selectedCardGuid, CheerCardAttachementImageConfigurationInfo AttachmentImageConfigurations, string EmptyStringPlaceHolder);
         string ReplacePlaceHolders(string sourceString, DataTable dt, string emptyStringPlaceHolder);
         string ReplacePlaceHolders(string sourceString, Dictionary<string, object> parameters, string emptyStringPlaceHolder);
         int SaveCheerCardRequest(Dictionary<string, object> FormParameters);
         string GetCheerCardPreviewHtml(Dictionary<string, object> FormParameters, Constants.Environments Environment, string selectedImageGuid,string EmptyStringPlaceHolder, bool isCalledFromCmsDesk);
         void DeleteImage(string imagePath);
    }
}
