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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            using (var ent = new ex1Entities1())
            {
                int num = 0;
                var nmodel = new extable()
                {
                id = new Func<int>(() =>
                    {
                        foreach (var en in ent.extable) num = en.id;
                        return num + 1;
                    })(),
                    name = "num" + num + 1,
                    value = 3152,
                    time = DateTime.Now
                };

                ent.extable.Add(nmodel);
                ent.SaveChanges();

                foreach (var en in ent.extable)
                {
                    Console.WriteLine("id:" + en.id);
                    Console.WriteLine("name:" + en.name);
                    Console.WriteLine();
                }
            }

        }

        // Show
        private void button1_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            dataGridView1.DataSource = new ex1Entities1().extable;
        }

        // Add
    }
}
