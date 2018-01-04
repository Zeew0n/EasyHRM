namespace BeeHRM.ApplicationService.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DocumentCategory")]
    public class DocumentCategoryDTO
    {
        [Key]
        public int CatId { get; set; }

        [Required]
        [StringLength(100)]
        public string CatTitle { get; set; }
    }
}
