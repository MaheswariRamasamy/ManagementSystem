﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Models.ViewModel
{
    public class AddDeviceVM
    {
       
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
