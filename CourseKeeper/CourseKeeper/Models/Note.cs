using System;
using System.Collections.Generic;
using SQLite;

namespace CourseKeeper.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string NoteText { get; set; }
        public int CourseID { get; set; }
    }
}