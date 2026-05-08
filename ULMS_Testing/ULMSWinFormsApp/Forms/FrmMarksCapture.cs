using System;
using System.Windows.Forms;
using ULMSWinFormsApp.Models;

namespace ULMSWinFormsApp.Forms
{
    public partial class FrmMarksCapture : Form
    {
        public FrmMarksCapture()
        {
            InitializeComponent();
        }

        private void btnCalculateResults_Click(object sender, EventArgs e)
        {
            // Validate Student ID and Name are not empty
            if (string.IsNullOrWhiteSpace(txtMarkStudentId.Text))
            {
                MessageBox.Show("Please provide Student ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMarkStudentId.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMarkStudentName.Text))
            {
                MessageBox.Show("Please provide Student Name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMarkStudentName.Focus();
                return;
            }

            //  Use TryParse instead of Convert.ToDouble to handle empty fields and non-numeric input safely
            if (!TryParseMarks(txtSubject1.Text, "Subject 1", out double subject1)) return;
            if (!TryParseMarks(txtSubject2.Text, "Subject 2", out double subject2)) return;
            if (!TryParseMarks(txtSubject3.Text, "Subject 3", out double subject3)) return;

            //  Validate marks are between 0 and 100
            if (!ValidateRange(subject1, "Subject 1")) return;
            if (!ValidateRange(subject2, "Subject 2")) return;
            if (!ValidateRange(subject3, "Subject 3")) return;

            MarkRecord record = new MarkRecord();
            record.StudentId = txtMarkStudentId.Text.Trim();
            record.StudentName = txtMarkStudentName.Text.Trim();
            record.Subject1 = subject1;
            record.Subject2 = subject2;
            record.Subject3 = subject3;

            //  Added parentheses so all three subjects are summed before dividing
            record.Average = (record.Subject1 + record.Subject2 + record.Subject3) / 3;

            record.ResultStatus = record.Average >= 50 ? "PASS" : "FAIL";

            txtMarksOutput.Text =
                "Marks processed successfully!" + Environment.NewLine +
                "Student ID: " + record.StudentId + Environment.NewLine +
                "Student Name: " + record.StudentName + Environment.NewLine +
                "Subject 1: " + record.Subject1 + Environment.NewLine +
                "Subject 2: " + record.Subject2 + Environment.NewLine +
                "Subject 3: " + record.Subject3 + Environment.NewLine +
                "Average: " + record.Average.ToString("F2") + Environment.NewLine +
                "Final Result: " + record.ResultStatus;
        }

        // Safe parsing of marks with user-friendly error messages
        private bool TryParseMarks(string input, string fieldName, out double result)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show($"Please add a mark for {fieldName}.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                result = 0;
                return false;
            }

            if (!double.TryParse(input, out result))
            {
                MessageBox.Show($"{fieldName} must be a valid number! ", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        //  Ensure mark is within 0–100 range
        private bool ValidateRange(double mark, string fieldName)
        {
            if (mark < 0 || mark > 100)
            {
                MessageBox.Show($"{fieldName} must be between 0 and 100.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnClearMarks_Click(object sender, EventArgs e)
        {
            txtMarkStudentId.Clear();
            txtMarkStudentName.Clear();
            txtSubject1.Clear();
            txtSubject2.Clear();
            txtSubject3.Clear();
            txtMarksOutput.Clear();
            txtMarkStudentId.Focus();
        }

        private void btnBackMarks_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}