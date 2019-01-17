using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MewToCol
{
    class FP7
    {
        int[] getDTNum;

        public FP7(int[] dtnums)
        {
            getDTNum = dtnums;
        }
        public void addDTListener(int dtnum)
        {
            int[] ovw = new int[getDTNum.Length + 1];
            foreach(int i in getDTNum) ovw[i] = getDTNum[i];
            ovw[getDTNum.Length] = dtnum;
        }
    }
}
