using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyErpWebService.Models.DTO
{
    [Table("t_item")]
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? FinishDate { get; set; }
    }
}
