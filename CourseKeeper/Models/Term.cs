using System;
using System.Collections.Generic;
using SQLite;

namespace CourseKeeper.Models
{
    public class Term
    {
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}