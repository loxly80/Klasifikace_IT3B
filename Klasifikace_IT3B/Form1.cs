using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klasifikace_IT3B
{
    public partial class Form1 : Form
    {
        List<Student> students;
        SqlRepository sqlRepository = new SqlRepository();

        public Form1()
        {
            InitializeComponent();
            students = sqlRepository.GetStudents();
            RefreshGUI();
        }

        private void RefreshGUI()
        {
            lvData.Items.Clear();
            foreach (Student student in students)
            {
                ListViewItem listViewItem = new ListViewItem(new string[] {
                    student.Lastname + " " + student.Firstname,
                    student.Birthday.ToString("dd.MM.yyyy"),
                    string.Join(", ", student.Grades.FindAll(grade => grade.Subject.ShortName.ToLower() == "aj")),
                    string.Join(", ", student.Grades.FindAll(grade => grade.Subject.ShortName.ToLower() == "prg")),
                    string.Join(", ", student.Grades.FindAll(grade => grade.Subject.ShortName.ToLower() == "m")),
                    string.Join(", ", student.Grades.FindAll(grade => grade.Subject.ShortName.ToLower() == "gm"))
                });
                lvData.Items.Add(listViewItem);
            }
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            frmStudent frmStudent = new frmStudent();
            frmStudent.StartPosition = FormStartPosition.CenterParent;
            frmStudent.ShowDialog();
        }

        private void lvData_DoubleClick(object sender, EventArgs e)
        {
            frmStudent frmStudent = new frmStudent();
            frmStudent.StartPosition = FormStartPosition.CenterParent;
            frmStudent.ShowDialog(students[lvData.SelectedIndices[0]]);
        }
    }
}
