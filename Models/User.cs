using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberQRCodeScannerPOC.Models
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }

        public string? QRCodePath { get; set; }
    }
}
