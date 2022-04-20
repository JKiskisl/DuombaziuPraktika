using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        #region "sqlcon&panels"
        List<Panel> panels = new List<Panel>();
        int index;


        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\local; Initial Catalog = Praktinis; Integrated security=True;");
        SqlCommand cmd;
        SqlDataAdapter adapt;

        int ID = 0;



        public Form1()
        {
            InitializeComponent();
            panels.Add(panel1);
            panels.Add(panel2);
            panels.Add(panel3);
            panels.Add(panel4);
            panels.Add(panel5);
            panels.Add(panel6);
            panels.Add(panel7);
            panels.Add(panel8);
            panels.Add(panel9);
            panels.Add(panel10);
            panels.Add(panel11);
            panels.Add(panel12);
            panels.Add(panel13);
            panels[index].BringToFront();

        }
        #endregion

        #region "Login"
        private void button2_Click(object sender, EventArgs e)
        {
            //Students
            SqlConnection scn = new SqlConnection();
            scn.ConnectionString = @"Data Source=(localdb)\local; Initial Catalog = Praktinis; Integrated security=True;";
            SqlCommand scmd = new SqlCommand("select count (*) as cnt from tblStudents where Name=@usr and LastName=@pwd", scn);
            SqlCommand scmdAdmin = new SqlCommand("SELECT count (*) as cnt from tblAdmin where Name=@usr and LastName=@pwd", scn);
            SqlCommand scmdLecturer = new SqlCommand("Select count (*) as cnt from tblLecturer where Name=@usr and LastName=@pwd", scn);

            scmd.Parameters.Clear();
            scmd.Parameters.AddWithValue("@usr", textBox1.Text);
            scmd.Parameters.AddWithValue("@pwd", textBox2.Text);

            scmdAdmin.Parameters.Clear();
            scmdAdmin.Parameters.AddWithValue("@usr", textBox1.Text);
            scmdAdmin.Parameters.AddWithValue("@pwd", textBox2.Text);

            scmdLecturer.Parameters.Clear();
            scmdLecturer.Parameters.AddWithValue("@usr", textBox1.Text);
            scmdLecturer.Parameters.AddWithValue("@pwd", textBox2.Text);

            scn.Open();

            if (scmd.ExecuteScalar().ToString() == "1")
            {
                MessageBox.Show("YOU ARE GRANTED WITH ACCESS");
                panels[1].BringToFront();
            }
            if (scmdAdmin.ExecuteScalar().ToString() == "1")
            {
                MessageBox.Show("YOU ARE GRANTED WITH ACCESS");
                panels[2].BringToFront();

            }
            if (scmdLecturer.ExecuteScalar().ToString() == "1")
            {
                MessageBox.Show("YOU ARE GRANTED WITH ACCESS");
                panels[9].BringToFront();
            }
            scn.Close();
            //admins
        }
        #endregion

        #region "Student view"
        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void GetStudentData()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\local; Initial Catalog = Praktinis; Integrated security=True;";
            conn.Open();
            string select = "select * from tblStudents where Name LIKE '" + textBox1.Text + "'";
           

            SqlCommand cmd = new SqlCommand(select, conn);
            DataTable dataTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        public void GetStudentGrades()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\local; Initial Catalog = Praktinis; Integrated security=True;";
            conn.Open();
            string select = "SELECT Name, SubjectName, Grade FROM tblGrades JOIN tblStudents " +
                "ON tblGrades.StudentID = tblStudents.StudentID JOIN tblSubjects " +
                "ON tblGrades.SubjectID = tblSubjects.SubjectID WHERE tblStudents.StudentID" +
                " = (SELECT StudentID FROM tblStudents Where Name = '"+textBox1.Text+"')";
            //string select = "Select tblStudents.Name, tblLecturer.Name, tblSubjects.SubjectName, tblGrades.Grade FROM tblGrades INNER JOIN tblStudents ON tblStudents.Name = tblStudents.ID where (SELECT StudentID FROM tblStudents Where Name = '" + textBox1.Text + "')";
            //string select = "SELECT tblSubjects.SubjectName AS Subject, tblGroups.GroupName AS grupe, tblLecturer.Name AS Lecturer, tblGrades.Grade as grades";

            SqlCommand cmd = new SqlCommand(select, conn);
            DataTable dataTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(dataTable);
            dataGridView2.DataSource = dataTable;
        }

       
        private void Button4_Click(object sender, EventArgs e)
        {
            GetStudentData();
            GetStudentGrades();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            panels[0].BringToFront();
        }

        #endregion

        #region "Admin view"
        #region "Edit and view all students"
        private void button12_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\local; Initial Catalog = Praktinis; Integrated security=True;";
            conn.Open();
            string select = "select * from tblStudents";


            SqlCommand cmd = new SqlCommand(select, conn);
            DataTable dataTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(dataTable);
            dataGridView5.DataSource = dataTable;
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Insert into students new student
        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox32.Text != "" && textBox33.Text != "" && textBox34.Text != "")
            {
                cmd = new SqlCommand("insert into tblStudents(StudentID,GroupID,Name,LastName,MiestoIDD,SaliesID,KalbosID) values(@studentid,@groupid,@name,@lastname,@coun,@lang,@city)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@studentid", Convert.ToInt32(textBox6.Text));
                cmd.Parameters.AddWithValue("@name", textBox3.Text);
                cmd.Parameters.AddWithValue("@lastname", textBox4.Text);
                cmd.Parameters.AddWithValue("@groupid", Convert.ToInt32(textBox5.Text));
                cmd.Parameters.AddWithValue("@lang", Convert.ToInt32(textBox32.Text));
                cmd.Parameters.AddWithValue("@city", Convert.ToInt32(textBox33.Text));
                cmd.Parameters.AddWithValue("@coun", Convert.ToInt32(textBox34.Text));
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            {
                cmd = new SqlCommand("delete FROM tblStudents WHERE StudentID = '"+textBox6.Text+"'", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textBox6.Text));
                cmd.Parameters.AddWithValue("@groupid", Convert.ToInt32(textBox5.Text));
                cmd.Parameters.AddWithValue("@name", textBox3.Text);
                cmd.Parameters.AddWithValue("@lastname", textBox4.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from tblStudents", con);
            adapt.Fill(dt);
            dataGridView5.DataSource = dt;
            con.Close();
        }
        private void ClearData()
        {
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            ID = 0;
        }
        #endregion

        #region "Edit and view lecturers"
        private void button17_Click(object sender, EventArgs e)
        {
            if (textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "" && textBox10.Text != "")
            {
                cmd = new SqlCommand("insert into tblLecturer(LecturerID,SubjectID,Name,LastName) values(@lecid,@subid,@name,@lastname)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@lecid", Convert.ToInt32(textBox7.Text));
                cmd.Parameters.AddWithValue("@subid", Convert.ToInt32(textBox8.Text));
                cmd.Parameters.AddWithValue("@name", textBox9.Text);
                cmd.Parameters.AddWithValue("@lastname", textBox10.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\local; Initial Catalog = Praktinis; Integrated security=True;";
            conn.Open();
            string select = "select * from tblLecturer";


            SqlCommand cmd = new SqlCommand(select, conn);
            DataTable dataTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(dataTable);
            dataGridView6.DataSource = dataTable;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "" && textBox10.Text != "")
            {
                cmd = new SqlCommand("delete FROM tblLecturer WHERE LecturerID = '" + textBox7.Text + "'", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textBox7.Text));
                cmd.Parameters.AddWithValue("@groupid", Convert.ToInt32(textBox8.Text));
                cmd.Parameters.AddWithValue("@name", textBox9.Text);
                cmd.Parameters.AddWithValue("@lastname", textBox10.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }
        #endregion

        #region "Edit and view all groups"
        private void button20_Click(object sender, EventArgs e)
        {
            if (textBox11.Text != "" && textBox12.Text != "" && textBox13.Text != "")
            {
                cmd = new SqlCommand("insert into tblGroups(GroupID,GroupName,FullProgramName) values(@groupid,@name,@full)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@groupid", Convert.ToInt32(textBox11.Text));
                cmd.Parameters.AddWithValue("@name", textBox12.Text);
                cmd.Parameters.AddWithValue("@full", textBox13.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\local; Initial Catalog = Praktinis; Integrated security=True;";
            conn.Open();
            string select = "select * from tblGroups";


            SqlCommand cmd = new SqlCommand(select, conn);
            DataTable dataTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(dataTable);
            dataGridView4.DataSource = dataTable;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (textBox11.Text != "" && textBox12.Text != "" && textBox13.Text != "")
            {
                cmd = new SqlCommand("delete FROM tblGroups WHERE GroupID = '" + textBox11.Text + "'", con);
                con.Open();
                cmd.Parameters.AddWithValue("@groupid", Convert.ToInt32(textBox11.Text));
                cmd.Parameters.AddWithValue("@name", textBox12.Text);
                cmd.Parameters.AddWithValue("@full", textBox13.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            panels[2].BringToFront();
        }
        #endregion

        #region "Edit and view all subjects"
        private void button27_Click(object sender, EventArgs e)
        {
            if (textBox14.Text != "" && textBox15.Text != "" && textBox16.Text != "")
            {
                cmd = new SqlCommand("insert into tblSubjects(SubjectID,GroupID,SubjectName) values(@subid,@groupid,@name)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@subid", Convert.ToInt32(textBox14.Text));
                cmd.Parameters.AddWithValue("@groupid", Convert.ToInt32(textBox15.Text));
                cmd.Parameters.AddWithValue("@name", textBox16.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\local; Initial Catalog = Praktinis; Integrated security=True;";
            conn.Open();
            string select = "select * from tblSubjects";


            SqlCommand cmd = new SqlCommand(select, conn);
            DataTable dataTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(dataTable);
            dataGridView3.DataSource = dataTable;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (textBox14.Text != "" && textBox15.Text != "" && textBox16.Text != "")
            {
                cmd = new SqlCommand("delete FROM tblSubjects WHERE SubjectID = '" + textBox14.Text + "'", con);
                con.Open();
                cmd.Parameters.AddWithValue("@subid", Convert.ToInt32(textBox14.Text));
                cmd.Parameters.AddWithValue("@groupid", Convert.ToInt32(textBox15.Text));
                cmd.Parameters.AddWithValue("@name", textBox16.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            panels[2].BringToFront();
        }
        #endregion

        #region "Edit and view all admins"
        private void button31_Click(object sender, EventArgs e)
        {
            if (textBox17.Text != "" && textBox18.Text != "" && textBox19.Text != "")
            {
                cmd = new SqlCommand("insert into tblAdmin(adminID,Name,LastName) values(@admid,@name,@lastname)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@admid", Convert.ToInt32(textBox17.Text));
                cmd.Parameters.AddWithValue("@name", textBox18.Text);
                cmd.Parameters.AddWithValue("@lastname", textBox19.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\local; Initial Catalog = Praktinis; Integrated security=True;";
            conn.Open();
            string select = "select * from tblAdmin";


            SqlCommand cmd = new SqlCommand(select, conn);
            DataTable dataTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(dataTable);
            dataGridView7.DataSource = dataTable;
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (textBox17.Text != "" && textBox18.Text != "" && textBox19.Text != "")
            {
                cmd = new SqlCommand("delete FROM tblAdmin WHERE adminID = '" + textBox17.Text + "'", con);
                con.Open();
                cmd.Parameters.AddWithValue("@admid", Convert.ToInt32(textBox17.Text));
                cmd.Parameters.AddWithValue("@name", textBox18.Text);
                cmd.Parameters.AddWithValue("@lastname", textBox19.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        #endregion

        #region "Edit and view all grades"
        private void button35_Click(object sender, EventArgs e)
        {
            if (textBox20.Text != "" && textBox21.Text != "" && textBox22.Text != "" && textBox23.Text != "" && textBox24.Text != "" && textBox25.Text != "")
            {
                cmd = new SqlCommand("insert into tblGrades(GradeID,SubjectID,GroupID,StudentID,LecturerID,Grade) values(@gid,@sid,@grpid,@studid,@lid,@grade)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@gid", Convert.ToInt32(textBox20.Text));
                cmd.Parameters.AddWithValue("@sid", Convert.ToInt32(textBox21.Text));
                cmd.Parameters.AddWithValue("@grpid", Convert.ToInt32(textBox22.Text));
                cmd.Parameters.AddWithValue("@studid", Convert.ToInt32(textBox23.Text));
                cmd.Parameters.AddWithValue("@lid", Convert.ToInt32(textBox24.Text));
                cmd.Parameters.AddWithValue("@grade", Convert.ToInt32(textBox25.Text));
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\local; Initial Catalog = Praktinis; Integrated security=True;";
            conn.Open();
            string select = "select * from tblGrades";


            SqlCommand cmd = new SqlCommand(select, conn);
            DataTable dataTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(dataTable);
            dataGridView8.DataSource = dataTable;
        }

        private void button37_Click(object sender, EventArgs e)
        {
            if (textBox20.Text != "" && textBox21.Text != "" && textBox22.Text != "" && textBox23.Text != "" && textBox24.Text != "" && textBox25.Text != "")
            {
                cmd = new SqlCommand("delete FROM tblGrades WHERE GradeID = '" + textBox20.Text + "'", con);
                con.Open();
                cmd.Parameters.AddWithValue("@gid", Convert.ToInt32(textBox20.Text));
                cmd.Parameters.AddWithValue("@sid", Convert.ToInt32(textBox21.Text));
                cmd.Parameters.AddWithValue("@grpid", Convert.ToInt32(textBox22.Text));
                cmd.Parameters.AddWithValue("@studid", Convert.ToInt32(textBox23.Text));
                cmd.Parameters.AddWithValue("@lid", Convert.ToInt32(textBox24.Text));
                cmd.Parameters.AddWithValue("@grade", Convert.ToInt32(textBox25.Text));
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }
        #endregion

        #region "View and edit all languages
        private void button42_Click(object sender, EventArgs e)
        {
            panels[10].BringToFront();
        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void button48_Click(object sender, EventArgs e)
        {
            panels[2].BringToFront();
        }




        private void button47_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\local; Initial Catalog = Praktinis; Integrated security=True;";
            conn.Open();
            string select = "select * from Kalba";


            SqlCommand cmd = new SqlCommand(select, conn);
            DataTable dataTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(dataTable);
            dataGridView10.DataSource = dataTable;
        }

        private void button46_Click(object sender, EventArgs e)
        {
            if (textBox35.Text != "" && textBox36.Text != "")
            {
                cmd = new SqlCommand("delete FROM Kalba WHERE KalbosID = '" + textBox35.Text + "'", con);
                con.Open();
                cmd.Parameters.AddWithValue("@groupid", Convert.ToInt32(textBox35.Text));
                cmd.Parameters.AddWithValue("@name", textBox12.Text);
                cmd.Parameters.AddWithValue("@full", textBox13.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {

        }

        private void button45_Click(object sender, EventArgs e)
        {
            if (textBox35.Text != "" && textBox36.Text != "")
            {
                cmd = new SqlCommand("insert into Kalba(KalbosID,Kalbospavadinimas) values(@Id,@name)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox35.Text));
                cmd.Parameters.AddWithValue("@name", textBox36.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }
        #endregion

        #region "View and edit all cities"

        private void button43_Click(object sender, EventArgs e)
        {
            panels[11].BringToFront();
        }

        private void button52_Click(object sender, EventArgs e)
        {
            panels[2].BringToFront();
        }

        private void button51_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\local; Initial Catalog = Praktinis; Integrated security=True;";
            conn.Open();
            string select = "select * from Miestas";


            SqlCommand cmd = new SqlCommand(select, conn);
            DataTable dataTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(dataTable);
            dataGridView11.DataSource = dataTable;
        }

        private void button50_Click(object sender, EventArgs e)
        {
            if (textBox37.Text != "" && textBox38.Text != "")
            {
                cmd = new SqlCommand("delete FROM Miestas WHERE MiestoID = '" + textBox37.Text + "'", con);
                con.Open();
                cmd.Parameters.AddWithValue("@groupid", Convert.ToInt32(textBox37.Text));
                cmd.Parameters.AddWithValue("@name", textBox12.Text);
                cmd.Parameters.AddWithValue("@full", textBox13.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            if (textBox37.Text != "" && textBox38.Text != "")
            {
                cmd = new SqlCommand("insert into Miestas(MiestoID,MiestoPavadinimas) values(@Id,@name)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox37.Text));
                cmd.Parameters.AddWithValue("@name", textBox38.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }



        #endregion

        #region "View and edit all countries"
        private void button44_Click(object sender, EventArgs e)
        {
            panels[12].BringToFront();
        }

        private void button56_Click(object sender, EventArgs e)
        {
            panels[2].BringToFront();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\local; Initial Catalog = Praktinis; Integrated security=True;";
            conn.Open();
            string select = "select * from Salis";


            SqlCommand cmd = new SqlCommand(select, conn);
            DataTable dataTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(dataTable);
            dataGridView12.DataSource = dataTable;
        }

        private void button54_Click(object sender, EventArgs e)
        {
            if (textBox39.Text != "" && textBox40.Text != "")
            {
                cmd = new SqlCommand("delete FROM Salis WHERE SaliesID = '" + textBox39.Text + "'", con);
                con.Open();
                cmd.Parameters.AddWithValue("@groupid", Convert.ToInt32(textBox39.Text));
                cmd.Parameters.AddWithValue("@name", textBox12.Text);
                cmd.Parameters.AddWithValue("@full", textBox13.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void button53_Click(object sender, EventArgs e)
        {
            if (textBox39.Text != "" && textBox40.Text != "")
            {
                cmd = new SqlCommand("insert into Salis(SaliesID,SaliesPavadinimas) values(@Id,@name)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox39.Text));
                cmd.Parameters.AddWithValue("@name", textBox40.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        #endregion

        #endregion

        #region "Lecturer view"
        private void button39_Click(object sender, EventArgs e)
        {
            if (textBox28.Text != "" && textBox26.Text != "" && textBox29.Text != "" && textBox30.Text != "" && textBox27.Text != "" && textBox31.Text != "")
            {
                cmd = new SqlCommand("insert into tblGrades(GradeID,SubjectID,GroupID,StudentID,LecturerID,Grade) values(@gid,@sid,@grpid,@studid,@lid,@grade)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@gid", Convert.ToInt32(textBox28.Text));
                cmd.Parameters.AddWithValue("@sid", Convert.ToInt32(textBox26.Text));
                cmd.Parameters.AddWithValue("@grpid", Convert.ToInt32(textBox29.Text));
                cmd.Parameters.AddWithValue("@studid", Convert.ToInt32(textBox30.Text));
                cmd.Parameters.AddWithValue("@lid", Convert.ToInt32(textBox27.Text));
                cmd.Parameters.AddWithValue("@grade", Convert.ToInt32(textBox31.Text));
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\local; Initial Catalog = Praktinis; Integrated security=True;";
            conn.Open();
            string select = "Select tblSubjects.SubjectID, SubjectName ,tblGroups.GroupID, GroupName, tblStudents.StudentID, Name, LastName, Grade, tblGrades.GradeID " +
                "FROM tblGrades JOIN tblSubjects ON tblGrades.SubjectID = tblSubjects.SubjectID " +
                "JOIN tblGroups ON tblGrades.GroupID = tblGroups.GroupID JOIN tblStudents ON tblGrades.StudentID = tblStudents.StudentID " +
                "WHERE LecturerID = (SELECT LecturerID FROM tblLecturer Where Name = '"+textBox1.Text+ "')";




            SqlCommand cmd = new SqlCommand(select, conn);
            DataTable dataTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(dataTable);
            dataGridView9.DataSource = dataTable;
        }

        private void button41_Click(object sender, EventArgs e)
        {
            if (textBox28.Text != "" && textBox26.Text != "" && textBox29.Text != "" && textBox30.Text != "" && textBox27.Text != "" && textBox31.Text != "")
            {
                cmd = new SqlCommand("delete FROM tblGrades WHERE GradeID = '" + textBox28.Text + "'", con);
                con.Open();
                cmd.Parameters.AddWithValue("@gid", Convert.ToInt32(textBox28.Text));
                cmd.Parameters.AddWithValue("@sid", Convert.ToInt32(textBox26.Text));
                cmd.Parameters.AddWithValue("@grpid", Convert.ToInt32(textBox29.Text));
                cmd.Parameters.AddWithValue("@studid", Convert.ToInt32(textBox30.Text));
                cmd.Parameters.AddWithValue("@lid", Convert.ToInt32(textBox27.Text));
                cmd.Parameters.AddWithValue("@grade", Convert.ToInt32(textBox31.Text));
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }
        #endregion

        #region "Generated"
        private void button25_Click(object sender, EventArgs e)
        {
            panels[8].BringToFront();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            panels[2].BringToFront();
        }

        private void dataGridView8_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panels[0].BringToFront();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            panels[0].BringToFront();
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            panels[4].BringToFront();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panels[5].BringToFront();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            panels[2].BringToFront();
        }


        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            panels[3].BringToFront();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            panels[2].BringToFront();
        }


        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button23_Click(object sender, EventArgs e)
        {
            panels[6].BringToFront();
        }


        private void button24_Click(object sender, EventArgs e)
        {
            panels[7].BringToFront();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            panels[2].BringToFront();
        }

        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void button38_Click(object sender, EventArgs e)
        {
            panels[0].BringToFront();
        }

        private void dataGridView9_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            //subjectid
        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {
            //gradeid
        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {
            //groupid
        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            //studid
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            //lectid
        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            //grade
        }


        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }
        #endregion

        #region "gen2"
        //MiestasSalisKalba Admin view
        private void textBox32_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox34_TextChanged(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }
        #endregion


    }
}
