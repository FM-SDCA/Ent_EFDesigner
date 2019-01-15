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
            using(var db = new ex1Entities1())
            {
                var list = db.extable.ToList();

                this.comboBox1.DisplayMember = "name";
                this.comboBox1.ValueMember = "id";
                this.comboBox1.DataSource = list;

                //var list2 = ex1Entities1.Set<extable>().Select(item => new extable
                //{
                //    id = item.id,
                //    name = item.name,
                //    value = item.value,
                //    time = item.time
                //})
                //    .ToList();

                //IQueryable<extable> query = db.extable
                //    .Select(x => new extable
                //    {
                //        id = x.id,
                //        name = x.name,
                //        value = x.value,
                //        time = x.time
                //    });

                var query = from x in db.extable
                            where x.id > 0
                            select x;

                foreach(var q in query)
                {
                    Console.WriteLine(q.id + ", " + q.name + ", " + q.value + ", " + q.time);
                }
                    
            }
            /*
            using(var db = new ex1Entities1())
            {
                var list = db.extable
                    .Where(item => item.id == 1)
                    .Select(item => new extable
                    {
                        id = item.id,
                        name = item.name,
                        value = item.value,
                        time = item.time
                    })
                    .ToList();
                this.dataGridView1.DataSource = list;
            }
            */
        }

        // Add
    }
}
