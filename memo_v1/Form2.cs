using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memo_v1
{
    public partial class Form2 : Form
    {
        //private Control _F1 = new Control(); //宣告Control用以接收Form1本體
        public Form2() //接收form1
        {
            InitializeComponent();
            textBox1.Select();
            //this.button1.Click += new System.EventHandler(this.button1_Click);
        }
        //public Form2(string strTextMsg) //接收form1Msg
        //{
        //    InitializeComponent();
        //    textBox1.Select();
        //    //this.button1.Click += new System.EventHandler(this.button1_Click);
        //}
        public string Msg;

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            Msg = textBox1.Text;
            //f1.tempName = Msg;
            //MessageBox.Show(Msg);
            textBox1.Text = "";
            this.Hide();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if(keyData == Keys.Enter && !this.button1.Focused)
            {
                button1_Click(null, null);
                DialogResult = DialogResult.OK;
            }
            if (keyData == Keys.Escape && !this.button2.Focused)
            {
                button2_Click(null, null);
                DialogResult = DialogResult.Cancel;
            }
            return base.ProcessDialogKey(keyData);
        }

        public string GetMsg()
        {
            return Msg;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*選取Cancel不做任何事*/
            this.Hide();
        }
    }
}
