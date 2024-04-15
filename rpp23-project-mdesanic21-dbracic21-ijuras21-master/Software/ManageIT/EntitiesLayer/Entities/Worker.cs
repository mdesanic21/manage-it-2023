namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Worker")]
    public partial class Worker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Worker()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Receipts = new HashSet<Receipt>();
            WorkOrders = new HashSet<WorkOrder>();
            Worker1 = new HashSet<Worker>();
            Workers = new HashSet<Worker>();
        }

        [Key]
        public int ID_worker { get; set; }

        [StringLength(40)]
        public string FirstName { get; set; }

        [StringLength(60)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [Required]
        [StringLength(60)]
        public string Email { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        public int? Id_type { get; set; }

        [StringLength(20)]
        public string UserName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Receipt> Receipts { get; set; }

        public virtual WorkerType WorkerType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Worker> Worker1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Worker> Workers { get; set; }
    }
}
