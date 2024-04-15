namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class OrderDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderDetail()
        {
            WorkOrders = new HashSet<WorkOrder>();
        }

        [Key]
        public int Id_Order_Details { get; set; }

        public int? ID_Work_Type { get; set; }

        [StringLength(80)]
        public string Location { get; set; }

        public TimeSpan? Duration { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public int? ID_Client { get; set; }

        public int? ID_Worker { get; set; }

        public virtual Client Client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }

        public virtual Worker Worker { get; set; }

        public virtual WorkType WorkType { get; set; }
    }
}
