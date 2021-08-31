using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITB1704Application.Model
{
    public class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId {get;set;}
        public int ProductId { get; set; }
        public int TransactionNo { get; set; }
        public DateTime TransactionTime { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
