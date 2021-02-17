using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    class OperatorNode:Node
    {
        public OperatorsType ThisOperatorsType = OperatorsType.None;
        public Node RightSide;
        public Node LeftSide;
        public override decimal Calc()
        {
            decimal R = RightSide.Calc();
            decimal L = LeftSide.Calc();
            if(OperatorsType.Addition == ThisOperatorsType)
            {
                return L + R;
            }
            else if(OperatorsType.Division == ThisOperatorsType)
            {
                return L / R;
            }
            else if (OperatorsType.Subtraction== ThisOperatorsType)
            {
                return L - R;
            }
            else if (OperatorsType.Multiplication== ThisOperatorsType)
            {
               return L*R;
            }
            else
            {
                MessageBox.Show("エラー");
                return 0;
            }

        }
    }
    enum OperatorsType
    {
        None,
        Multiplication,
        Addition,
        Subtraction,
        Division,
        Percent
    }
}
