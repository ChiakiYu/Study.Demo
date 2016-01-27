using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDemo.Models
{
    public class DatatablesQuery
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DTColumn[] Columns { get; set; }

        public DTOrder[] Order { get; set; }

        public DTSearch Search { get; set; }

        public string OrderBy
        {
            get
            {
                return Columns != null && Order != null && Order.Length > 0
                    ? Columns[Order[0].Column].Data
                    : string.Empty;
            }
        }

        public DTOrderDir OrderDir
        {
            get
            {
                return Order != null && Order.Length > 0
                    ? Order[0].Dir
                    : DTOrderDir.DESC;
            }
        }
    }

    public class DTOrder
    {
        /// <summary>
        /// Column to which ordering should be applied.
        /// This is an index reference to the columns array of information that is also submitted to the server.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Ordering direction for this column.
        /// It will be dt-string asc or dt-string desc to indicate ascending ordering or descending ordering, respectively.
        /// </summary>
        public DTOrderDir Dir { get; set; }
    }

    public enum DTOrderDir
    {
        ASC,
        DESC
    }

    public class DTColumn
    {
        /// <summary>
        /// Column's data source, as defined by columns.data.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Column's name, as defined by columns.name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Flag to indicate if this column is searchable (true) or not (false). This is controlled by columns.searchable.
        /// </summary>
        public bool Searchable { get; set; }

        /// <summary>
        /// Flag to indicate if this column is orderable (true) or not (false). This is controlled by columns.orderable.
        /// </summary>
        public bool Orderable { get; set; }

        ///// <summary>
        ///// Specific search value.
        ///// </summary>
        //public DTSearch Search { get; set; }
    }

    public class DTSearch
    {
        /// <summary>
        /// Global search value. To be applied to all columns which have searchable as true.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// true if the global filter should be treated as a regular expression for advanced searching, false otherwise.
        /// Note that normally server-side processing scripts will not perform regular expression searching for performance reasons on large data sets, but it is technically possible and at the discretion of your script.
        /// </summary>
        public bool Regex { get; set; }
    }
}