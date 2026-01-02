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
    public partial class frmapplication : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-VRDI6ID\SQLEXPRESS;Initial Catalog=Skill International;Integrated Security=True");
        public frmapplication()
        {
            InitializeComponent();
        }
        string gender;

        public void popregno()
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select Reg_No from RegisterForm", con);
            adapter.Fill(dt);

            cmbregno.DataSource = dt;
            //textBox1.DataBindings.ToString();
            cmbregno.DisplayMember = "Reg_No";
            cmbregno.ValueMember = "Reg_No";
            con.Close();



        }

        public void popctrl(int RegNo)
        {
            DataTable dt=new DataTable();
            con.Open();
            SqlCommand cmd=new SqlCommand("Select * From RegisterForm where Reg_No=@Reg_No",con);

            cmd.Parameters.AddWithValue("@Reg_No", RegNo);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            con.Close();

            txtfirstname.Text = dt.Rows[0]["First_Name"].ToString();
            txtlastname.Text = dt.Rows[0]["Last_Name"].ToString();
            txtemail.Text = dt.Rows[0]["Email"].ToString(); 
            txtaddress.Text = dt.Rows[0]["Address"].ToString();
            txtmobile.Text = dt.Rows[0]["Mobile_No"].ToString();
            txthome.Text = dt.Rows[0]["Home_No"].ToString();
            txtparentname.Text = dt.Rows[0]["Parent_Name"].ToString();
            txtnic.Text = dt.Rows[0]["NIC"].ToString();
            txtcontactno.Text = dt.Rows[0]["Contact_No"].ToString();
            dtpdob.Text = dt.Rows[0]["Date_of_Birth"].ToString();
            gender = dt.Rows[0]["Gender"].ToString();
            if (gender == "Male")
            {
                radmale.Checked = true;
            }
            else
            {
                radfemale.Checked=true;
            }







        }

        public void clear()
        {
            cmbregno.Text = "";
            txtfirstname.Clear();
            txtlastname.Clear();
            dtpdob.Text = "";
            radfemale.Checked = false;
            radmale.Checked = false;
            txtaddress.Clear();
            txtemail.Clear();
            txtmobile.Clear();
            txthome.Clear();
            txtparentname.Clear();
            txtnic.Clear();
            txtcontactno.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string commandstring = @"INSERT INTO [dbo].[RegisterForm]
           ([Reg_No]
           ,[First_Name]
           ,[Last_Name]
           ,[Date_of_Birth]
           ,[Gender]
           ,[Address]
           ,[Email]
           ,[Mobile_No]
           ,[Home_No]
           ,[Parent_Name]
           ,[NIC]
           ,[Contact_No])
     VALUES

     (@Reg_No,@First_Name,@Last_Name,@Date_of_Birth,@Gender,@Address,@Email,@Mobile_No,@Home_No,@Parent_Name,@NIC,@Contact_No)";

                SqlCommand cmd = new SqlCommand(commandstring, con);

                cmd.Parameters.AddWithValue("@Reg_No", cmbregno.Text);
                cmd.Parameters.AddWithValue("@First_Name", txtfirstname.Text);
                cmd.Parameters.AddWithValue("@Last_Name", txtlastname.Text);
                cmd.Parameters.AddWithValue("@Date_of_Birth", dtpdob.Text);
                cmd.Parameters.AddWithValue("@Address", txtaddress.Text);
                cmd.Parameters.AddWithValue("@Email", txtemail.Text);
                cmd.Parameters.AddWithValue("@Mobile_No", txtmobile.Text);
                cmd.Parameters.AddWithValue("@Home_No", txthome.Text);
                cmd.Parameters.AddWithValue("@Parent_Name", txtparentname.Text);
                cmd.Parameters.AddWithValue("@NIC", txtnic.Text);
                cmd.Parameters.AddWithValue("@Contact_No", txtcontactno.Text);

                if (radmale.Checked == true)
                {
                    gender = "Male";
                }
                else if (radfemale.Checked == true)
                {
                    gender = "Female";
                }

                cmd.Parameters.AddWithValue("@Gender", gender);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Added Successfully", "REGISTER..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.clear();
            }
            //end of the try
            catch
            {
                MessageBox.Show("Invalid Registration Number", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbregno.ForeColor = Color.Red;
            }

            }

        private void button3_Click(object sender, EventArgs e)
        {
            this.clear();
        }

        private void frmapplication_Load(object sender, EventArgs e)
        {
            popregno();
            int regno = Convert.ToInt32(cmbregno.SelectedValue);
            popctrl(regno);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbregno_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectedValue = cmbregno.SelectedValue.ToString();
            int Reg_No;
            if (int.TryParse(SelectedValue, out Reg_No))
            {
                Reg_No = Convert.ToInt32(SelectedValue);
                popctrl(Reg_No);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
                string commandstring = @"UPDATE [dbo].[RegisterForm]
   SET [Reg_No] = @Reg_No
      ,[First_Name] = @First_Name
      ,[Last_Name] = @Last_Name
      ,[Date_of_Birth] = @Date_of_Birth
      ,[Gender] = @Gender
      ,[Address] = @Address
      ,[Email] = @Email
      ,[Mobile_No] = @Mobile_No
      ,[Home_No] = @Home_No
      ,[Parent_Name] = @Parent_Name
      ,[NIC] = @NIC
      ,[Contact_No] = @Contact_No
 WHERE Reg_No = @Reg_No";

            SqlCommand cmd = new SqlCommand(commandstring, con);
            //Set sql Parameters with values
            cmd.Parameters.AddWithValue("@First_Name", txtfirstname.Text);
            cmd.Parameters.AddWithValue("@Last_Name", txtlastname.Text);
            cmd.Parameters.AddWithValue("@Date_of_Birth",dtpdob.Text);
            cmd.Parameters.AddWithValue("@Address", txtaddress.Text);
            cmd.Parameters.AddWithValue("@Email", txtemail.Text);
            cmd.Parameters.AddWithValue("@Mobile_No", txtmobile.Text);
            cmd.Parameters.AddWithValue("@Home_No", txthome.Text);
            cmd.Parameters.AddWithValue("@Parent_Name", txthome.Text);
            cmd.Parameters.AddWithValue("@NIC", txtnic.Text);
            cmd.Parameters.AddWithValue("@Reg_No", cmbregno.Text);
            cmd.Parameters.AddWithValue("@Contact_No", txtcontactno.Text);
            if (radmale.Checked == true)
            {
                gender = "Male";
            }
            else if (radfemale.Checked == true)
            {
                gender = "Female";
            }
            cmd.Parameters.AddWithValue("Gender", gender);
            
            
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Update Successfully", "Register Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.clear();
            
     
            



        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are You Sure you want to delese this Record..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {

                string commandstring = @"DELETE from RegisterForm where Reg_No=@Reg_No";
                SqlCommand cmd = new SqlCommand(commandstring, con);

                cmd.Parameters.AddWithValue("Reg_No", Convert.ToInt32(cmbregno.Text));
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted Successfully", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            MDIParent1 mdi = new MDIParent1();
            mdi.Hide();
            frmlogin log = new frmlogin();
            log.Show();

            
        }
            
        
                

        
    }
}
