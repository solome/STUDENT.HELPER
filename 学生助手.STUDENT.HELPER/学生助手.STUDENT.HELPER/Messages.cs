using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace 学生助手.STUDENT.HELPER
{
    public class Messages
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(30)]
        public string Week { get; set; }
        [MaxLength(30)]
        public string Title { get; set; }
        [MaxLength(30)]
        public string Subtitle { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(30)]
        public string Teacher { get; set; }
        [MaxLength(30)]
        public string TextBook { get; set; }
        [MaxLength(30)]
        public string Start { get; set; }
        [MaxLength(30)]
        public string End { get; set; }
    }
}
