using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Концелярский_Нож
{
    
    public class MemoFiles
    {
        List<Memo> list = new List<Memo>();

        public List<Memo> Memos
        {
            get => list;
            set => this.list = value;
        }

        public DateTime[] GetDates
        {
            get
            {
                var res = from el in this.list select el.MemoDate;
                return res.ToArray();
            }
        }
    }
}
