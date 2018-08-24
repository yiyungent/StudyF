using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public partial class RankingList
    {
        [Key]
        public int RID { get; set; }

        [Required]
        public DateTime SendTime { get; set; }

        [Required, MaxLength(20)]
        public string SenderIP { get; set; }

        [MaxLength(100)]
        public string SendMessage { get; set; }
    }

}
