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
    public partial class frmStudent : Form
    {
        private bool isEdit = false;
        private Student student;
        public frmStudent()
        {
            InitializeComponent();
        }

        public new DialogResult ShowDialog()
        {
            isEdit = false;
            return base.ShowDialog();
        }

        public DialogResult ShowDialog(Student student)
        {
            isEdit = true;
            this.student = student;
            return ShowDialog();
        }
    }
}
