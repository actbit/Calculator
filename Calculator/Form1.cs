using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void numButton__Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            string tempt = ((Control)sender).Text;
            string t=textBox1.Text;
            int s = textBox1.SelectionStart;
            bool add = false;
            if (tempt!="."||(tempt == "."&&NumPCheck(t,s)==false))
            {
                if (s==textBox1.Text.Length)
                {
                    t+= tempt;
                    add = true;
                }
                else
                {
                    t=t.Insert(s, tempt);

                    add = true;
                }
            }
            textBox1.Text = t;
            if (add)
            {
                textBox1.SelectionStart = s + 1;
            }


        }
        bool NumPCheck(string s,int NowSelect)
        {
            if(NowSelect != 0)
            {
                NowSelect--;
            }
            
            int sN = NowSelect;
            int ss = 0;
            while (true)
            {
                if(NowSelect == 0)
                {
                    break;
                }
                else if (CheckOperator(s[NowSelect]))
                {
                    ss = NowSelect;
                    break;
                }

                NowSelect--;
            }
            for(int i = ss; i < s.Length; i++)
            {
                if (s[i] == '.')
                {
                    return true;
                }
                else if (CheckOperator(s[i]))
                {
                    return false;
                }
                
            }
            return false; 
        }
        private void buttonPM_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            int index = 0;
            for(int i = 0; i < text.Length; i++)
            {
                if(CheckOperator(text[i]))
                {
                    index = i;
                }
            }

            if (text[index] == '-')
            {
                text = text.Remove(index, 1);
                if (index != 0)
                {

                    text = text.Insert(index, "+");
                }

            }
            else if (text[index] == '+')
            {
                text = text.Remove(index, 1);
                text = text.Insert(index, "-");



            }
            else
            {
                text=text.Insert(index, "-");
            }
            textBox1.Text = text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            
            int s= textBox1.SelectionStart;
            if (s != 0)
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart - 1, 1);
                textBox1.SelectionStart = s - 1;
                
            }
            textBox1.Focus();
        }

        private void Operator_Click(object sender, EventArgs e)
        {
            string add = ((Control)sender).Text;

            Operator(add);
        }

        void Operator(string add)
        {
            int s = textBox1.SelectionStart;
            if(s == 0)
            {
                return;
            }
            if (s > textBox1.Text.Length)
            {
                textBox1.Text += add;
            }
            else
            {
                s--;
                char c = textBox1.Text[s];
                string text = textBox1.Text;
                if (CheckOperator(c))
                {
                    if (CheckOperator(char.Parse(add)))
                    {
                        if(((c == '÷'||c=='×')&&(add =="+"||add == "-"))==false)
                        {
                            text = text.Remove(s, 1);
                            
                            if (textBox1.Text[s-1]=='÷'|| textBox1.Text[s - 1] == '×')
                            {
                                s--;
                                text = text.Remove(s, 1);
                            }
                            
                        }
                        else
                        {
                            s++;
                        }
                        
                        text = text.Insert(s, add);
                    }

                }
                else
                {
                    s++;
                    text = text.Insert(s, add);
                }

                textBox1.Text = text;
            }
            textBox1.SelectionStart = s + 1;
            textBox1.Focus();
        }
        bool CheckOperator(char C)
        {
            return C == '+' || C == '-' || C == '×' || C == '÷';
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Focus();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            if (textBox1.SelectionStart != 0)
            {
                textBox1.SelectionStart = textBox1.SelectionStart - 1;
            }
            
        }

        private void button22_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            if (textBox1.SelectionStart != textBox1.Text.Length)
            {
                textBox1.SelectionStart = textBox1.SelectionStart +1;
            }
        }
        Node Result = new Node();

        void Calculation()
        {
            string T= textBox1.Text;
            List<string> tempTs = new List<string>();
            int index = 0;
            for(int i = 0; i < T.Length; i++)
            {
                if (CheckOperator(T[i])||T[i]=='%'|| T[i] == '('|| T[i] == ')')
                {
                    if(i-index != 0) 
                    {
                        tempTs.Add(T.Substring(index, i - index));

                    }
                    tempTs.Add(T[i].ToString());
                    index = i+1;
                }
            }
            if(index != T.Length)
            {
                tempTs.Add(T.Substring(index, T.Length - index));
            }
            index = 0;
            List<string> ss = new List<string>();
            percentCheck(tempTs);
            TempNode tn=brackets(ChengeNode(tempTs));
            multiplication(tn);
            addition(tn);
            decimal d=Calc(tn);
            textBox1.Text = decimal.Round(d, 2, MidpointRounding.AwayFromZero).ToString();

        }

        void BarketsChenge(TempNode tempNode)
        {

        }
        decimal Calc(TempNode tempNode)
        {
            return tempNode.Calc();
            
        }

        TempNode ChengeNode(List<string> strings)
        {
            TempNode tempNode = new TempNode();
            for(int i = 0; i < strings.Count; i++)
            {
                tempNode.Datas.Add(new Node() { Data=strings[i] });
            }
            return tempNode;
        }
        TempNode brackets(TempNode node)
        {
            
            int sindex = -1;
            for(int i = 0; i < node.Datas.Count; i++)
            {
                if(node.Datas[i].Data == "(")
                {
                    sindex = i;
                }
                else if (node.Datas[i].Data == ")")
                {
                    if (sindex != -1)
                    {
                        TempNode tempNode = new TempNode();
                        for (int ii = sindex + 1; ii < i; ii++)
                        {
                            tempNode.Datas.Add(node.Datas[ii]);

                        }
                        node.Datas.RemoveRange(sindex, tempNode.Datas.Count + 2);

                        node.Datas.Insert(sindex, tempNode);
                        i = sindex - 1;
                        sindex = -1; 
                        if (BracketsContains(node))
                        {
                            brackets(node);

                        }
                    }


                }
                else
                {

                }
            }
            if (BracketsContains(node)==false)
            {
                if(node.Datas[node.Datas.Count-1].Data == ")")
                {
                    node.Datas.RemoveAt(node.Datas.Count-1);
                }

            }

            return node;
        }

        bool BracketsContains(TempNode node)
        {
            bool Next = false;
            for (int i = 0; i < node.Datas.Count; i++)
            {
                if (node.Datas[i].Data == "(")
                {
                    Next = true;
                    break;
                }
            }
            return Next;
        }

        void multiplication(Node node)
        {
            if (node is TempNode)
            {
                TempNode tempNode = (TempNode)node;
                for (int i = 0; i < tempNode.Datas.Count; i++)
                {
                    if (tempNode.Datas[i] is TempNode)
                    {
                        multiplication((TempNode)tempNode.Datas[i]);
                    }
                    if (tempNode.Datas[i].Data == "×")
                    {
                        Woodification(tempNode, ref i, OperatorsType.Multiplication);

                    }
                    else if (tempNode.Datas[i].Data == "÷")
                    {

                        Woodification(tempNode, ref i, OperatorsType.Division);

                    }
                }
            }

        }

        void Woodification(TempNode node,ref int i,OperatorsType operatorsType)
        {
            if (node.Datas.Count > i+1)
            {
                OperatorNode operatorNode = new OperatorNode();
                operatorNode.ThisOperatorsType = operatorsType;
                if (node.Datas[i + 1].Data == "-" || node.Datas[i + 1].Data == "+")
                {
                    node.Datas[i + 1].Data = node.Datas[i + 1].Data + node.Datas[i + 2].Data;
                    node.Datas.RemoveAt(i + 2);
                }

                operatorNode.LeftSide = node.Datas[i - 1];
                operatorNode.RightSide = node.Datas[i + 1];
                for (int j = i - 1; j <= i + 1; j++)
                {
                    node.Datas.RemoveAt(i - 1);
                }
                node.Datas.Insert(i - 1, operatorNode);
                i--;
            }
            else
            {
                node.Datas.RemoveAt(i);
                i--;
            }

        }
        void addition(Node node)
        {
            
            if(node is TempNode)
            {
                TempNode tempNode = (TempNode)node;
                for (int i = 0; i < tempNode.Datas.Count; i++)
                {
                    if (tempNode.Datas[i] is TempNode|| tempNode.Datas[i] is OperatorNode)
                    {
                        addition(tempNode.Datas[i]);
                    }
                    else if (tempNode.Datas[i] is OperatorNode)
                    {
                        addition(((OperatorNode)tempNode.Datas[i]).LeftSide);
                    }
                    if (tempNode.Datas[i].Data == "+")
                    {
                        Woodification(tempNode, ref i, OperatorsType.Addition);

                    }
                    else if (tempNode.Datas[i].Data == "-")
                    {
                        Woodification(tempNode, ref i, OperatorsType.Subtraction);
                    }
                }
            }

            

        }

        void percentCheck(List<string> Strings)
        {
            for (int i = 0; i < Strings.Count; i++)
            {
                if (Strings[i] == "%"&&(i-1>0&&decimal.TryParse(Strings[i - 1], out decimal ii)))
                {
                    ii = ii / 100m;
                    Strings[i - 1] = ii.ToString();
                    Strings.RemoveAt(i);
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            
            string tempt = c.ToString();
            if(int.TryParse(tempt,out int o)||c=='.'|| c == '('|| c == ')')
            {
                string t = textBox1.Text;
                int s = textBox1.SelectionStart;
                bool add = false;
                if (tempt != "." || (tempt == "." && NumPCheck(t, s) == false))
                {
                    if (s == textBox1.Text.Length)
                    {
                        t += tempt;
                        add = true;
                    }
                    else
                    {
                        t = t.Insert(s, tempt);

                        add = true;
                    }
                }
                textBox1.Text = t;
                if (add)
                {
                    textBox1.SelectionStart = s + 1;
                }
                
            }
            else if(c == '\b')
            {
                int s = textBox1.SelectionStart;
                if (s != 0)
                {
                    textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart - 1, 1);
                    textBox1.SelectionStart = s - 1;

                }
            }
            else if(c =='*'|| c == '+' || c == '-' || c == '/')
            {
                if(tempt == "*")
                {
                    tempt = "×";
                }
                else if(tempt == "/")
                {
                    tempt = "÷";
                }
                Operator(tempt);
            }
            else if (c == '%')
            {
                percentCheck();
            }
            else if(c == '\r')
            {
                Calculation();
                textBox1.Focus();
                textBox1.SelectionStart = textBox1.Text.Length;
            }
            textBox1.Focus();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            percentCheck();
        }
        void percentCheck()
        {
            int s = textBox1.SelectionStart - 1;
            if (s==-1)
            {
                return;
            }
            string Text = textBox1.Text;
            char t = textBox1.Text[s];
            if (t == '%' || CheckOperator(t) == false)
            {
                Text = Text.Insert(s + 1, "%");
            }
            textBox1.Text = Text;
            textBox1.SelectionStart = s + 2;
            textBox1.Focus();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Calculation();
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length ;
        }
    }
}
