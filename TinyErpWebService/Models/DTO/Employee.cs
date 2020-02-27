using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyErpWebService.Models.DTO
{
    public enum RoleType
    {
        Listener,
        Administrator
    }

    [Table("t_employee")]
    public class Employee : User
    {
        [Range(0, byte.MaxValue)]
        public RoleType Role { get; set; }
    }
}
