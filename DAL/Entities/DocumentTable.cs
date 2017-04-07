using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("document_table")]
    public class DocumentTable
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string F001 { get; set; }
        public string F002 { get; set; }
        public string F003 { get; set; }
        public string F004 { get; set; }
        public string F005 { get; set; }
        public string F006 { get; set; }
        public string F007 { get; set; }
        public string F008 { get; set; }
        public string F009 { get; set; }
        public string F010 { get; set; }
        public virtual Document Document { get; set; }
    }
}
