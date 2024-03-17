﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace pharma_quick_test2.Models
{
    public partial class MedicationCategory
    {
        public MedicationCategory()
        {
            Medications = new HashSet<Medication>();
        }

        [Key]
        [Column("CategoryID")]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string CategoryName { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Medication> Medications { get; set; }
    }
}