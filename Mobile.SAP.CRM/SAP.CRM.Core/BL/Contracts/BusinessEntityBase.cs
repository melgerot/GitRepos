using System;
using SAP.CRM.Core.SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SAP.CRM.Core.BL.Contracts
{
    /// <summary>
    /// Business entity base class. Provides the ID property.
    /// </summary>
    public abstract class BusinessEntityBase : IBusinessEntity
    {
        public BusinessEntityBase()
        {
        }

        /// <summary>
        /// Gets or sets the Database ID.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
       
    }
}
