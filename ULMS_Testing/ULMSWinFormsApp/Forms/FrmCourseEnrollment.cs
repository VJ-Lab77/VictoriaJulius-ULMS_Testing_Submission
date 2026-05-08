using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ULMSWinFormsApp.Models;

namespace ULMSWinFormsApp.Forms
{
    public partial class FrmCourseEnrollment : Form
    {
        //  Tracking any existing enrollments to prevent duplicates
        private readonly List<string> _enrolledCourses = new List<string>();

        public FrmCourseEnrollment()
        {
            InitializeComponent();
        }

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            string studentId = txtEnrollStudentId.Text.Trim();
            string studentName = txtEnrollStudentName.Text.Trim();
            string courseName = cmbCourse.Text.Trim();
            string semester = cmbSemester.Text.Trim();

            //  Validate Student ID is not empty
            if (string.IsNullOrWhiteSpace(studentId))
            {
                MessageBox.Show("Please provide a Student ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEnrollStudentId.Focus();
                return;
            }

            // Validate Student Name is not empty
            if (string.IsNullOrWhiteSpace(studentName))
            {
                MessageBox.Show("Please provide a Student Name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEnrollStudentName.Focus();
                return;
            }

            //  Validate a Course  been selected
            if (string.IsNullOrWhiteSpace(courseName))
            {
                MessageBox.Show("Please select a Course.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCourse.Focus();
                return;
            }

            //  Validate semester  been selected
            if (string.IsNullOrWhiteSpace(semester))
            {
                MessageBox.Show("Please select a Semester.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSemester.Focus();
                return;
            }

            //  Check for duplicate enrollment using Student ID + Course combination
            string enrollmentKey = $"{studentId}_{courseName}".ToLower();

            if (_enrolledCourses.Contains(enrollmentKey))
            {
                MessageBox.Show(
                    $"Student '{studentId}' is already enrolled in '{courseName}'.",
                    "Duplicate Enrollment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // All validations passed 
            _enrolledCourses.Add(enrollmentKey);

            Enrollment enrollment = new Enrollment
            {
                StudentId = studentId,
                StudentName = studentName,
                CourseName = courseName,
                Semester = semester
            };

            txtEnrollmentOutput.Text =
                "Enrollment completed successfully!" + Environment.NewLine +
                "Student ID: " + enrollment.StudentId + Environment.NewLine +
                "Student Name: " + enrollment.StudentName + Environment.NewLine +
                "Course: " + enrollment.CourseName + Environment.NewLine +
                "Semester: " + enrollment.Semester;
        }

        private void btnClearEnrollment_Click(object sender, EventArgs e)
        {
            txtEnrollStudentId.Clear();
            txtEnrollStudentName.Clear();
            cmbCourse.SelectedIndex = -1;
            cmbSemester.SelectedIndex = -1;
            txtEnrollmentOutput.Clear();
            txtEnrollStudentId.Focus();
        }

        private void btnBackEnrollment_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}