using System;
using System.Windows.Forms;
using ULMSWinFormsApp.Models;

namespace ULMSWinFormsApp.Forms
{
    public partial class FrmStudentRegistration : Form
    {
        public FrmStudentRegistration()
        {
            InitializeComponent();
        }

        private void btnSaveStudent_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtStudentId.Text))
                {
                    MessageBox.Show("Please enter Student ID");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Please enter Full Name");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Please enter Email");
                    return;
                }

                if (!int.TryParse(txtAge.Text, out int age))
                {
                    MessageBox.Show("Please enter a valid Age");
                    return;
                }

                if (age < 16 || age > 100)
                {
                    MessageBox.Show("Age must be between 16 and 100");
                    return;
                }

                if (cmbProgramme.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a Programme");
                    return;
                }

                Student student = new Student
                {
                    StudentId = txtStudentId.Text,
                    FullName = txtFullName.Text,
                    Email = txtEmail.Text,
                    Age = age,
                    Programme = cmbProgramme.Text
                };

                txtStudentOutput.Text = "Student saved successfully!" + Environment.NewLine +
                    "Student ID: " + student.StudentId + Environment.NewLine +
                    "Full Name: " + student.FullName + Environment.NewLine +
                    "Email: " + student.Email + Environment.NewLine +
                    "Age: " + student.Age + Environment.NewLine +
                    "Programme: " + student.Programme;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnClearStudent_Click(object sender, EventArgs e)
        {
            txtStudentId.Clear();
            txtFullName.Clear();
            txtEmail.Clear();
            txtAge.Clear();
            cmbProgramme.SelectedIndex = -1;
            txtStudentOutput.Clear();
        }

        private void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}