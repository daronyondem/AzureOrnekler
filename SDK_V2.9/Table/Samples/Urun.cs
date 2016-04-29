using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples
{
    public class Urun : TableEntity
    {
        public string Adi { get; set; }
        public string Aciklama { get; set; }
    }
}
