using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CURD_WORK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=./;Initial Catalog=AHSAN;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentRecord();
        }

        private void GetStudentRecord()
        {
            
            SqlCommand cmd = new SqlCommand("select * from curd_work",con);
            DataTable dt = new DataTable();

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt;

        }

        private void btninsert_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                SqlCommand cmd = new SqlCommand("insert into curd_work values (@name,@age,@gender)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name",txtname.Text);
                cmd.Parameters.AddWithValue("@age", txtage.Text);
                cmd.Parameters.AddWithValue("@gender", txtgender.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Insertd", "Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStudentRecord();
                reset();
            }
        }

        private bool isValid()
        {
            if (txtname.Text==string.Empty)
            {
                MessageBox.Show("Insert Name","Fill Student Name",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reset();

        }

        private void reset()
        {
            txtname.Clear();
            txtid.Clear();
            txtage.Clear();
            txtname.Focus();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtage.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtgender.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text)>0)
            {
                SqlCommand cmd = new SqlCommand("update curd_work set Name=@name,Age=@age,Gender=@gender where ID=@id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtid.Text));
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@age", Convert.ToInt32(txtage.Text));
                cmd.Parameters.AddWithValue("@gender", txtgender.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Updated", "Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStudentRecord();
                reset();
            }
            else
            {
                MessageBox.Show("Select data ", "Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) > 0)
            {
                SqlCommand cmd = new SqlCommand("delete from curd_work where ID=@id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtid.Text));
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@age", Convert.ToInt32(txtage.Text));
                cmd.Parameters.AddWithValue("@gender", txtgender.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("DELETED", "Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStudentRecord();
                reset();
            }
            else
            {
                MessageBox.Show("Select data ", "Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
