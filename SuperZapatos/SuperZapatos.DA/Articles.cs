namespace SuperZapatos.DA
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Articles
    {
        public int Id { get; set; }

        [Required]
        [StringLength(28)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Price { get; set; }

        public int Total_in_shelf { get; set; }

        public int Total_in_vault { get; set; }

        public int Store_Id { get; set; }

        public virtual Store Store { get; set; }
    }
}
