﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LibraryForYou.Models
{
    public class BorrowHistory
    {
        public int BorrowHistoryId { get; set; }

        [Required]
        [Display(Name = "Book")]
        public int BookId { get; set; }

        public Book Book { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Display(Name = "Borrow Date")]
        public DateTime BorrowDate { get; set; }

        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }
    }
}
