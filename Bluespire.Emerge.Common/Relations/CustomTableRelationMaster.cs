using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Relations
{
    /// <summary>
    /// Class representation of custom table "CustomTableRelationMaster"
    /// </summary>
    public class CustomTableRelationMaster
    {

        /// <summary>
        /// get set relation name
        /// </summary>
        public string RelationName { get; set; }

        /// <summary>
        /// get set Primary Table value
        /// </summary>
        public string PrimaryTableName { get; set; }

        /// <summary>
        /// get set primary table primary column name
        /// </summary>
        public string PrimaryPkColumnName { get; set; }


        /// <summary>
        /// get set primary table display column name
        /// </summary>
        public string PrimaryDisplayColumnName { get; set; }

        /// <summary>
        /// get set foreign table name
        /// </summary>
        public string ForeignTableName { get; set; }


        /// <summary>
        /// get set foreign table column name
        /// </summary>
        public string ForeignTableColumnName { get; set; }

        
        /// <summary>
        /// get set skip validation.
        /// skip grid view delete or deactivate relational data validation
        /// </summary>
        public bool? SkipValidation { get; set; }
    }
}
