namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("WorkOrder")]
    public partial class WorkOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkOrder()
        {
            Receipts = new HashSet<Receipt>();
        }

        [Key]
        public int ID_Work_Order { get; set; }

        public bool IsFinished { get; set; }

        public DateTime DateCreated { get; set; }

        public int ID_Worker { get; set; }

        public int Id_Order_Details { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Receipt> Receipts { get; set; }

        public virtual Worker Worker { get; set; }
    }
}
