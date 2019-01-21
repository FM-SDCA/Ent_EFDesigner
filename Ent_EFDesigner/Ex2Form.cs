using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ent_EFDesigner
{
    public partial class Ex2Form : Form
    {
        public Ex2Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                using (var ctx = new SampleModel1())
                {
                    var table = new ex2table()
                    {
                        Stock = int.Parse(textBox1.Text),
                        PartNumber = textBox2.Text,
                        Name = textBox3.Text,
                        Spec = textBox4.Text,
                        Maker = textBox5.Text,
                        Distributor = textBox6.Text,
                        EolInfo = textBox7.Text,
                        ReplacementPart = textBox8.Text
                    };
                    ctx.ex2table.Add(table);
                    ctx.SaveChanges();
                }
                //結果を設定する
                this.DialogResult = DialogResult.OK;
                //閉じる
                this.Close();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //結果を設定する
            this.DialogResult = DialogResult.OK;
            //閉じる
            this.Close();
        }
    }
}
