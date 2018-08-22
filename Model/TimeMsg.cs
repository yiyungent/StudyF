using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class TimeMsg
    {
        [Key, Required]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Message { get; set; }

        [Required]
        public DateTime SendTime { get; set; }

        [Required]
        public string SenderIP { get; set; }
    }
}
