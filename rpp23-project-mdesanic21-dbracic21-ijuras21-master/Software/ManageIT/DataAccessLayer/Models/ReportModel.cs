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
    // Needed a way to connect all the data which should be shown in the report. We decided not to create table Report because we thought it would be redundant to have the same data in the PDF file and the database. The model is used to extract data and furthermore to acces data for the ReportView.
    public class ReportModel
    {
        public List<WorkOrder> workOrderReport { get; set; }
        public DateTime startDate { get; set; }
        public DateTime finishDate { get; set; }
        public Worker creatorWorker { get; set; }
        public int ID_Report { get; set; }
    }
}
