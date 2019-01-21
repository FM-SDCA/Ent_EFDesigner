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
    public partial class MainForm : Form
    {
        private Point mousePoint;
        int[] dtNum = new int[] { 1000, 1002, 1004, 1006, 1008, 1010, 1012, 1014 };
        string ipadd = "192.168.11.1";
        int port = 9023;
        MewtocolLib.FP7 fp7;

        public MainForm()
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
            int[] vals = new int[dtNum.Length];
            for(int i = 0; i < dtNum.Length; i++)
            { 
                vals[i] = fp7.GetDT(dtNum[i]);
            }
            AddDataToDatabase(vals);
        }

        private void AddDataToDatabase(int[] _value)
        {
            using (var table = new fp7dataEntities())
            {
                var nmodel = new ex1table()
                { 
                    //id = new Func<int>(() =>
                    //{
                    //    //foreach (var en in ent.ex1table) num = (int)en.id;
                    //    //return num + 1;
                    //})(),
                    val1 = (short)_value[0],
                    val2 = (short)_value[1],
                    val3 = (short)_value[2],
                    val4 = (short)_value[3],
                    val5 = (short)_value[4],
                    val6 = (short)_value[5],
                    val7 = (short)_value[6],
                    val8 = (short)_value[7],
                    time = DateTime.Now
                };

                table.ex1table.Add(nmodel);
                table.SaveChanges();

                foreach (var record in table.ex1table)
                {
                    Console.Write("id: {0}, vals: {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8} time: {9}", 
                        record.id, record.val1, record.val2, record.val3, record.val4, record.val5
                        , record.val6, record.val7, record.val8, record.time);
                    Console.WriteLine();
                }
            }
        }

        private void GetDataFromDataBase()
        {
            using (var db = new fp7dataEntities())
            {
                
                var t1 = int.Parse(textBox1.Text);
                var t2 = int.Parse(textBox2.Text);
                var query = from x in db.ex1table
                            where x.id >= t1
                               && x.id < t1 + t2
                            select x;
                var dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("val1");
                dt.Columns.Add("val2");
                dt.Columns.Add("val3");
                dt.Columns.Add("val4");
                dt.Columns.Add("val5");
                dt.Columns.Add("val6");
                dt.Columns.Add("val7");
                dt.Columns.Add("val8");
                dt.Columns.Add("time");
                foreach (var q in query)
                {
                    var row = dt.NewRow();
                    row["id"] = q.id;
                    row["val1"] = q.val1;
                    row["val2"] = q.val2;
                    row["val3"] = q.val3;
                    row["val4"] = q.val4;
                    row["val5"] = q.val5;
                    row["val6"] = q.val6;
                    row["val7"] = q.val7;
                    row["val8"] = q.val8;
                    row["time"] = q.time;
                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
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

        private void button5_Click(object sender, EventArgs e)
        {
            var ex2 = new Ex2Form();
            ex2.Visible = true;
            ex2.FormClosed += new FormClosedEventHandler(EX2Form_FormClosed);
        }

        private void EX2Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ex2Form ex2 = (Ex2Form)sender;

            // 閉じた時の動作
        }

        // Add
    }
}
