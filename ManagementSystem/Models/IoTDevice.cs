using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Models
{
    public class IoTDevice
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description{ get; set; }
        public int IsDeviceOn { get; set; }


        //Foreign key
        public string UserId { get; set; }
        //Nav prop
        public User User { get; set; }


    }
}
