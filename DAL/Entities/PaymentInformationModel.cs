using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class PaymentInformationModel
    {
        public decimal Amount { get; set; }
        public string OrderType { get; set; } = "billpayment";
        public string OrderDescription { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
