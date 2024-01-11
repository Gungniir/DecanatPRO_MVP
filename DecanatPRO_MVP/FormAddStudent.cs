using System;
using System.Windows.Forms;
using ModelLayer.Controllers;
using Ninject;
using Presenter;
using Shared;

namespace DecanatPRO_MVP
{
    public partial class FormAddStudent : Form, IStudentAddView
    {
        private StudentAddViewPresenter _presenter;

        public event EventHandler<AddStudentArgs> EventStudentAdd = delegate { };

        public FormAddStudent(IKernel kernel)
        {
            _presenter = new StudentAddViewPresenter(this, kernel.Get<IStudentsController>());

            InitializeComponent();
        }

        private void ValidateInput()
        {
            if (textBoxName.Text == "" || textBoxGroup.Text == "" || textBoxSpec.Text == "")
            {
                buttonAdd.Enabled = false;
            }

            buttonAdd.Enabled = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void textBoxSpec_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void textBoxGroup_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var args = new AddStudentArgs
            {
                Group = textBoxGroup.Text,
                Name = textBoxName.Text,
                Speciality = textBoxName.Text
            };

            EventStudentAdd.Invoke(this, args);
            Close();
        }
    }
}