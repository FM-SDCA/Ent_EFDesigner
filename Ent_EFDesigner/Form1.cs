using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Ent_EFDesigner
{
    public partial class Form1 : Form
    {
        private Point mousePoint;
        int[] dtNum = new int[] { 1000/*, 1002, 1004, 1006 */};
        string ipadd = "192.168.11.1";
        int port = 9023;
        MewtocolLib.FP7 fp7;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

            // フォームの角を丸くする
            int radius = 20;
            int diameter = radius * 2;
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();

            gp.AddPie(0, 0, diameter, diameter, 180, 90);
            gp.AddPie(this.Width - diameter, 0, diameter, diameter, 270, 90);
            gp.AddPie(0, this.Height - diameter, diameter, diameter, 90, 90);
            gp.AddPie(this.Width - diameter, this.Height - diameter, diameter, diameter, 0, 90);
            gp.AddRectangle(new Rectangle(radius, 0, this.Width - diameter, this.Height));
            gp.AddRectangle(new Rectangle(0, radius, radius, this.Height - diameter));
            gp.AddRectangle(new Rectangle(this.Width - radius, radius, radius, this.Height - diameter));

            this.Region = new Region(gp);

            fp7 = new MewtocolLib.FP7(dtNum, ipadd, port);

            timer1.Interval = 10000;
            timer1.Start();
        }

        // Show
        private void button1_Click(object sender, EventArgs e)
        {
            GetDataFromDataBase();
        }

        // Add
        private void button2_Click(object sender, EventArgs e)
        {
            foreach(int a in dtNum)
            {
                AddDataToDatabase(DateTime.Today.ToString(), fp7.GetDT(a));
            }
        }

        private void AddDataToDatabase(string name, int value)
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
        
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //位置を記憶する
                mousePoint = new Point(e.X, e.Y);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        private void GetDataFromDataBase()
        {
            using (var db = new ex1Entities1())
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
                var t1 = int.Parse(textBox1.Text);
                var t2 = int.Parse(textBox2.Text);
                var query = from x in db.extable
                            where x.id >= t1
                               && x.id < t1 + t2
                            select x;
                var dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("name");
                dt.Columns.Add("value");
                dt.Columns.Add("time");
                foreach (var q in query)
                {
                    Console.WriteLine(q.id + ", " + q.name + ", " + q.value + ", " + q.time);
                    var row = dt.NewRow();
                    row["id"] = q.id;
                    row["name"] = q.name;
                    row["value"] = q.value;
                    row["time"] = q.time;
                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
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

        // timer control

        int time = 0;
        int time_up = 0;
        const int time_min = 10;

        // start / reset
        private void button3_Click(object sender, EventArgs e)
        {
            if(!timer1.Enabled) timer1.Start();
            time_up = int.Parse(textBox3.Text) < time_min ? time_min : int.Parse(textBox3.Text);
            time = time_up;
        }

        // stop
        private void button4_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled) timer1.Stop();
            else
            {
                time_up = int.Parse(textBox3.Text) < time_min ? time_min : int.Parse(textBox3.Text);
                time = time_up;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time--;
            if(time <= 0)
            {

            }
        }

        // Add
    }
}
