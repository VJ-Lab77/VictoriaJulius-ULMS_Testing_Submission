using System;
using System.Text;
using System.Windows.Forms;

namespace ULMSWinFormsApp.Forms
{
    public partial class FrmReports : Form
    {
        public FrmReports()
        {
            InitializeComponent();
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            string reportType = cmbReportType.Text;
            string studentId = txtReportStudentId.Text.Trim();

            // Validate Student ID is not empty
            if (string.IsNullOrWhiteSpace(studentId))
            {
                MessageBox.Show("Please enter a Student ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReportStudentId.Focus();
                return;
            }

            // Report type 
            if (string.IsNullOrWhiteSpace(reportType))
            {
                MessageBox.Show("Please select a Report Type.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbReportType.Focus();
                return;
            }

            //  Removed Thread.Sleep(4000) 
            StringBuilder report = new StringBuilder();
            report.AppendLine("===== ULMS REPORT =====");
            report.AppendLine("Report Type: " + reportType);
            report.AppendLine("Student ID Filter: " + studentId);
            report.AppendLine("Generated On: " + DateTime.Now);
            report.AppendLine();

            if (reportType == "Student Summary Report")
            {
                
                report.AppendLine("Student ID: " + studentId);
                report.AppendLine("Programme: Software Engineering");
                report.AppendLine("Status: Active");
            }
            else if (reportType == "Marks Report")
            {
                
                if (!TryGetMark("Subject 1", out double subject1)) return;
                if (!TryGetMark("Subject 2", out double subject2)) return;
                if (!TryGetMark("Subject 3", out double subject3)) return;

                double average = (subject1 + subject2 + subject3) / 3;

                report.AppendLine("Student ID: " + studentId);
                report.AppendLine("Subject 1: " + subject1);
                report.AppendLine("Subject 2: " + subject2);
                report.AppendLine("Subject 3: " + subject3);
                report.AppendLine("Average: " + average.ToString("F2"));
                report.AppendLine("Result: " + (average >= 50 ? "PASS" : "FAIL"));
            }
            else if (reportType == "Enrollment Report")
            {
                report.AppendLine("Student ID: " + studentId);
                report.AppendLine("Course 1: Programming 1");
                report.AppendLine("Course 2: Database Systems");
                report.AppendLine("Semester: Semester 1");
            }
            else
            {
                report.AppendLine("No data found for this report type.");
            }

            txtReportOutput.Text = report.ToString();
        }

        //  Helper to safely collect and validate a mark from the user at runtime
        private bool TryGetMark(string subjectLabel, out double mark)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                $"Enter mark for {subjectLabel} (0 - 100):",
                "Enter Mark");

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show($"Mark for {subjectLabel} cannot be empty.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mark = 0;
                return false;
            }

            if (!double.TryParse(input, out mark) || mark < 0 || mark > 100)
            {
                MessageBox.Show($"{subjectLabel} must be a number between 0 and 100.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnClearReport_Click(object sender, EventArgs e)
        {
            cmbReportType.SelectedIndex = -1;
            txtReportStudentId.Clear();
            txtReportOutput.Clear();
            txtReportStudentId.Focus();
        }

        private void btnBackReport_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}