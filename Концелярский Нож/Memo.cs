using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Концелярский_Нож
{
    public class Memo
    {
        public Memo()
        {
            this.MemoText = string.Empty;
            this.MemoDate = DateTime.Now;
        }

        public string MemoText { get; set; }

        public DateTime MemoDate { get; set; }

    }
}
