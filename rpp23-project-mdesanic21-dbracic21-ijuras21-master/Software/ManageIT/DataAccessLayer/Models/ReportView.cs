using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    /// <remarks>
    /// Matej Desanić
    /// </remarks>
    // Used only for storing and showing specific list item data in the report.
    public class ReportView
    {
        public string WorkType { get; set; }
        public string Worker { get; set; }
        public string Client { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }
    }
}
