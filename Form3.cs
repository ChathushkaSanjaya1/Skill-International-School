using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Skills_International_School
{
    public partial class frmch : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7VOPEDF\SQLEXPRESS;Initial Catalog=Skill International;Integrated Security=True");
        public frmch()
        {
            InitializeComponent();
        }
        int reg, cls, house;
        string hcolor;

        public void popreg()
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select RegNo from Class_House", con);
            adapter.Fill(dt);

            cmbreg.DataSource = dt;
            //textBox1.DataBindings.ToString();
            cmbreg.DisplayMember = "RegNo";
            cmbreg.ValueMember = "RegNo";
            con.Close();



        }
        public void popctrl(int RegNo)
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * From Class_House where RegNo=@RegNo", con);

            cmd.Parameters.AddWithValue("@RegNo", RegNo);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            con.Close();

            txtclass.Text = dt.Rows[0]["Class_Name"].ToString();
            txthouse.Text = dt.Rows[0]["House_Name"].ToString();
            txtteacher.Text = dt.Rows[0]["Teacher"].ToString();

            hcolor = dt.Rows[0]["House_Color"].ToString();
            if (hcolor == "RED")
            {
                txtcolor.BackColor = Color.Red;
            }
            else if (hcolor == "BLUE")
            {

                txtcolor.BackColor = Color.Blue;
            }
            else if (hcolor == "GREEN")
            {

                txtcolor.BackColor = Color.Green;
            }
            else if (hcolor == "PINK")
            {

                txtcolor.BackColor = Color.Pink;
            }
        }
        public void clear()
        {

            cmbreg.Text="";
            txtclass.Clear();
            txthouse.Clear();
            txtteacher.Clear();
            txtcolor.BackColor = Color.White;
        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            reg = int.Parse(cmbreg.Text);

            cls = reg % 5;
            house = reg % 4;

            if (cls == 1)
            {
                txtclass.Text = "A";
            }
            else if (cls == 2)
            {
                txtclass.Text = "B";
            }
            else if (cls == 3)
            {
                txtclass.Text = "C";
            }
            else if (cls == 4)
            {
                txtclass.Text = "D";
            }
            else
            {
                txtclass.Text = "E";
            }

            if (house == 1)
            {
                txthouse.Text = "ALPHA";
                txtcolor.BackColor = Color.Red;
                txtteacher.Text = "R.S.RATHNAYAKE";
            }
            else if (house == 2)
            {
                txthouse.Text = "BETA";
                txtcolor.BackColor = Color.Blue;
                txtteacher.Text = "K.S.P.DHARMARATNE";
            }
            else if (house == 3)
            {
                txthouse.Text = "LEO";
                txtcolor.BackColor = Color.Green;
                txtteacher.Text = "V.H.EKANAYAKE";
            }
            else
            {
                txthouse.Text = "OMEGA";
                txtcolor.BackColor = Color.Pink;
                txtteacher.Text = "A.R.H.ABEYKOON";
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            this.clear();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            string commandstring = @"INSERT INTO [dbo].[Class_House]
           ([RegNo]
           ,[Class_Name]
           ,[House_Name]
           ,[House_Color]
           ,[Teacher])
     VALUES
         (@RegNo,@Class_Name,@House_Name,@House_Color,@Teacher)";

            SqlCommand cmd = new SqlCommand(commandstring, con);

            cmd.Parameters.AddWithValue("@RegNo", cmbreg.Text);
            cmd.Parameters.AddWithValue("@Class_Name", txtclass.Text);
            cmd.Parameters.AddWithValue("@House_Name", txthouse.Text);
            cmd.Parameters.AddWithValue("@Teacher", txtteacher.Text);
            if (txtcolor.BackColor == Color.Red)
            {
                hcolor = "RED";
            }
            else if (txtcolor.BackColor == Color.Blue)
            {
                hcolor = "BLUE";
            }
            else if (txtcolor.BackColor == Color.Green)
            {
                hcolor = "GREEN";
            }
            else if (txtcolor.BackColor == Color.Pink)
            {
                hcolor = "PINK";
            }
                
            cmd.Parameters.AddWithValue("@House_Color", hcolor);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Saved Successfully", "SAVE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.clear();
            
            
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            string commandstring = @"UPDATE [dbo].[Class_House]
   SET [RegNo] =@RegNo
      ,[Class_Name] =@Class_Name
      ,[House_Name] =@House_Name
      ,[House_Color] =@House_Color
      ,[Teacher] =@Teacher
 WHERE RegNo = @RegNo";

            SqlCommand cmd = new SqlCommand(commandstring, con);
            //Set sql Parameters with values
            cmd.Parameters.AddWithValue("@RegNo", cmbreg.Text);
            cmd.Parameters.AddWithValue("@Class_Name", txtclass.Text);
            cmd.Parameters.AddWithValue("@House_Name", txthouse.Text);
            cmd.Parameters.AddWithValue("@Teacher", txtteacher.Text);
            if (txtcolor.BackColor == Color.Red)
            {
                hcolor = "RED";
            }
            else if (txtcolor.BackColor == Color.Blue)
            {
                hcolor = "BLUE";
            }
            else if (txtcolor.BackColor == Color.Green)
            {
                hcolor = "GREEN";
            }
            else if (txtcolor.BackColor == Color.Pink)
            {
                hcolor = "PINK";
            }

            cmd.Parameters.AddWithValue("@House_Color", hcolor);

            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Update Successfully", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.clear();
        }

        private void cmbreg_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectedValue = cmbreg.SelectedValue.ToString();
            int RegNo;
            if (int.TryParse(SelectedValue, out RegNo))
            {
                RegNo = Convert.ToInt32(SelectedValue);
                popctrl(RegNo);
            }
        }

        private void frmch_Load(object sender, EventArgs e)
        {
            popreg();
            int RegNo = Convert.ToInt32(cmbreg.SelectedValue);
            popctrl(RegNo);
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are You Sure you want to delese this Record..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {

                string commandstring = @"DELETE from Class_House where RegNo=@RegNo";
                SqlCommand cmd = new SqlCommand(commandstring, con);

                cmd.Parameters.AddWithValue("RegNo", Convert.ToInt32(cmbreg.Text));
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted Successfully", "DELETE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.clear();
                con.Close();
            }
            else
            {
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmlogin log = new frmlogin();
            log.Show();
        }
    }
}
