using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //SQL connection
using System.Threading;

namespace AarsprojektSweetHome
{
    public partial class Form2 : Form
    {
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

        

        // Open a connection to the SQL Server database at a server:
        SqlConnection conn = new SqlConnection("User id =sweethomedb1; " + // \\ to get \
                                 "Password=Sn8E~2XHa9_9; " +
                                 //"Persist Security Info=False;" +
                                 //"Integrated Security=SSPI;" +
                                 "Database=sweethomedb1;" +
                                 "Server=den1.mssql6.gear.host;" + // not localhost
                                 "Connect Timeout=45");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        DataTable dt;



        public Form2()
        {

            InitializeComponent();
            disp_data();

            conn.Open();
            SqlCommand sc = new SqlCommand("select BrugerID,Navn from ejendomsmæglere", conn);
            
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt2 = new DataTable();

            dt2.Columns.Add("customerid", typeof(string));
            dt2.Columns.Add("contactname", typeof(string));
            dt2.Load(reader);

            comboBox1.ValueMember = "BrugerID";
            comboBox1.DisplayMember = "Navn";
            comboBox1.DataSource = dt2;


            conn.Close();




            conn.Open(); //Åbner forbindelse til databasen
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Huse WHERE Hid>0";
            cmd.ExecuteNonQuery();



            DataTable dt5 = new DataTable(); // Datagridview top venstre fordelingsvindue
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt5);
            dataGridView3.DataSource = dt5;




            double[] fordelerarray = new double[30];











        }




        private void Crud_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sweethomedb1DataSet3.Huse' table. You can move, or remove it, as needed.
            //this.huseTableAdapter.Fill(this.sweethomedb1DataSet3.Huse);
            // TODO: This line of code loads data into the 'sweethomedb1DataSet2.Boliger2' table. You can move, or remove it, as needed.
            //this.boliger2TableAdapter.Fill(this.sweethomedb1DataSet2.Boliger2);

            //timer interval
            t.Interval = 1000;  //in milliseconds

            t.Tick += new EventHandler(this.t_Tick);

            //start timer when form loads
            t.Start();  //this will use t_Tick() method
        }

        //timer eventhandler
        private void t_Tick(object sender, EventArgs e)
        {

            Thread Ur = new Thread(Traad);
            Ur.Start();



            //Ur.Join();
           
            //update label
            label1.Text = output2;
            label19.Text = output1;

            
        }

        public static void Traad()
        {
            //get current time
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int hh2 = DateTime.Now.Hour - 1;
            //time
            string time = "";
            string time2 = "";

            //KBH time
            if (hh < 10)
            {
                time += "0" + hh;
            }
            else
            {
                time += hh;
            }
            time += ":";

            if (mm < 10)
            {
                time += "0" + mm;
            }
            else
            {
                time += mm;
            }




            // London time
            if (hh2 < 10)
            {
                time2 += "0" + hh2;
            }
            else
            {
                time2 += hh2;
            }
            time2 += ":";

            if (mm < 10)
            {
                time2 += "0" + mm;
            }
            else
            {
                time2 += mm;
            }

            var Field1 = new Form2();
            Field1.Output1 = time;

            var Field2 = new Form2();
            Field2.Output2 = time2;

            

            


        }
        //field for kbh
        public static string output1;

        public string Output1 { get => output1; set => output1 = value; }

        //field for lon
        public static string output2;

        public string Output2 { get => output2; set => output2 = value; }


        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                String sqlString = "SET TRANSACTION ISOLATION LEVEL REPEATABLE READ";
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();
               
                sqlString = "BEGIN TRANSACTION";
                cmd = new SqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();
                
                cmd = new SqlCommand("INSERT INTO Huse VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + textBox12.Text + "','" + textBox13.Text + "','" + textBox14.Text + "','" + textBox15.Text + "','" + textBox16.Text + "','" + textBox24.Text + "','" + 0 + "','" + 0 + "','" + null + "','" + null + "')", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Oprettelse gennemført");
                disp_data();
                ClearData();

                sqlString = "COMMIT"; // or ROLLBACK
                cmd = new SqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();

                try
                {
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        
            else
            {
                MessageBox.Show("Indtast venligst boligdata.");
            }
            
            
            
        }

        public void disp_data() //Opdatere DataGridView
        {
            
            adapt = new SqlDataAdapter("SELECT * FROM huse", conn);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
                       
            dataGridView2.DataSource = dt;

        }



        //Clear Data  
        private void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            textBox18.Text = "Hus ID";
            textBox24.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                conn.Open(); //Åbner forbindelse til databasen
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Huse WHERE Adresse = '" + textBox18.Text + " ' ";
                cmd.ExecuteNonQuery();
                conn.Close();
                
                MessageBox.Show("Sletning gennemført");
            }
            else
            {
                MessageBox.Show("Indtast venligst i adressefeltet");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                conn.Open(); //Åbner forbindelse til databasen
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Huse SET Adresse = '" + textBox1.Text + "', Postnr = '" + textBox2.Text + "', Grundareal = '" + textBox3.Text + "', Kælderareal = '" + textBox4.Text + "', Boligareal = '" + textBox5.Text + "', GarageCarport = '" + textBox6.Text + "', Kvmpris = '" + textBox7.Text + "', Antalsoveværelser = '" + textBox8.Text + "', Antalrum = '" + textBox9.Text + "', Ejendomstype = '" + textBox10.Text + "', Byggeår = '" + textBox11.Text + "', Kontantpris = '" + textBox12.Text + "', Ejerudgiftprmd = '" + textBox13.Text + "', Udbetaling = '" + textBox14.Text + "', Prisudvikling = '" + textBox15.Text + "', Energimærke = '" + textBox16.Text + "', Sagsnr = '" + textBox24.Text + "' WHERE Hid = '" + textBox18.Text + "'";
                cmd.ExecuteNonQuery();
                conn.Close();
                disp_data();
                ClearData();
                MessageBox.Show("Opdatering af gennemført");
            }
            else
            {
                MessageBox.Show("Indtast venligst bolig data");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox17.Text != "")
            {
                conn.Open(); //Åbner forbindelse til databasen

                SqlCommand cmd = conn.CreateCommand();

                adapt = new SqlDataAdapter("SELECT * FROM huse", conn);
                dt = new DataTable();
                adapt.Fill(dt);
                dataGridView1.DataSource = dt;
                             

                DataView DV = new DataView(dt);

                DV.RowFilter = string.Format("Adresse LIKE '%{0}%'", textBox17.Text);

                dataGridView1.DataSource = DV;
                conn.Close();

                //textBox14.Text = "";
            }
            else
            {
                MessageBox.Show("Indtast venligst en adresse");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            disp_data();
            ClearData();
            MessageBox.Show("Data cleared");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox22.Text != "")
            {
                conn.Open(); //Åbner forbindelse til databasen

                SqlCommand cmd = conn.CreateCommand();

                adapt = new SqlDataAdapter("SELECT * FROM huse", conn);
                dt = new DataTable();
                adapt.Fill(dt);
                dataGridView2.DataSource = dt;


                DataView DV = new DataView(dt);

                DV.RowFilter = string.Format("Adresse LIKE '%{0}%'", textBox22.Text);

                dataGridView2.DataSource = DV;
                conn.Close();
            }
            else
            {
                MessageBox.Show("Indtast venligst en adresse");
            }
        }

        private void button8_Click(object sender, EventArgs e) //Opret salg
        {
            
            if (textBox19.Text != "" && textBox23.Text != "BoligID")
            {

                string s = "";
                if (comboBox1.SelectedIndex >= 0)
                    s = comboBox1.Items[comboBox1.SelectedIndex].ToString();

                
                conn.Open(); //Åbner forbindelse til databasen
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Huse SET Salgsstatus = '" + 1 + "', salgsprisen = '" + textBox19.Text + "', SolgtAfMedarbejder = '" + comboBox1.SelectedValue.ToString() + "' , DatoForSalg = '" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "' WHERE Hid = '" + textBox23.Text + "'";
                cmd.ExecuteNonQuery();
                conn.Close();
                disp_data();
                ClearData();
                MessageBox.Show("Opdatering af gennemført");

               

            }

            else
            {
                MessageBox.Show("Indtast venligst boligdata.");
            }


        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (true)
            {

            }
            else
            {

            }
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = comboBox1.SelectedValue.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            conn.Open(); //Åbner forbindelse til databasen
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Huse SET Salgsstatus = '" + 0 + "', salgsprisen = '" + 0 + "', SolgtAfMedarbejder = '" + null + "' , DatoForSalg = '" + null + "' WHERE Hid = '" + textBox23.Text + "'";
            cmd.ExecuteNonQuery();
            conn.Close();
            disp_data();
            ClearData();
            MessageBox.Show("Opdatering af gennemført");
        }


        #region Statestik Beregner

        private void btnBeregnStat_Click(object sender, EventArgs e)
        {

        }

        #endregion






        #region Fordelervindue

        private void button12_Click(object sender, EventArgs e)
        {

        }


        #endregion





    }
}
