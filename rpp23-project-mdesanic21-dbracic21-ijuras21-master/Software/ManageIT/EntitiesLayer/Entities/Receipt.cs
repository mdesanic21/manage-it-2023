namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Receipt")]
    public partial class Receipt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_receipt { get; set; }

        public int? ID_Worker { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public int? ID_Work_Order { get; set; }

        public int? OIB { get; set; }

        public int? Canceled { get; set; }

        [Column(TypeName = "text")]
        public string Additional_info { get; set; }

        public virtual Worker Worker { get; set; }

        public virtual WorkOrder WorkOrder { get; set; }
    }
}
