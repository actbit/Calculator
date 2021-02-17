using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    class Node
    {
        public string Data;
        public virtual decimal Calc()
        {
            if(decimal.TryParse(Data,out decimal O))
            {
                return O;
            }
            else
            {
                return 0.0m;
            }
        }
    }
    class TempNode:Node
    {

        public List<Node> Datas = new List<Node>();
        public override decimal Calc()
        {
            decimal d = 1.0m;
            for (int i = 0; i < Datas.Count; i++)
            {
                if ((Datas[i].Data == "+" || Datas[i].Data == "*" || Datas[i].Data == "÷" || Datas[i].Data == "×"))
                {
                    MessageBox.Show("正しくありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0.0m;    
                }
                else
                {
                    d *= Datas[i].Calc();
                }
            }
            return d;
        }
    }
}
