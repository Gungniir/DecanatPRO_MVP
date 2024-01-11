using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ModelLayer.Controllers;
using ModelLayer.Models;
using Ninject;
using Presenter;
using Shared;
using ZedGraph;
using ListView = System.Windows.Forms.ListView;

namespace DecanatPRO_MVP
{
    public partial class FromMain : Form, IStudentsView
    {
        private readonly IKernel _kernel;
        private StudentsViewPresenter _presenter;

        public event EventHandler<int> EventStudentDelete = delegate { };
        public event EventHandler EventStudentsFill = delegate { };
        public void AddStudent(Student student)
        {
            ListViewItem item = new ListViewItem(student.Name);
            item.SubItems.Add(student.Speciality);
            item.SubItems.Add(student.Group);
            item.SubItems.Add(student.Id.ToString());

            listViewStudents.Items.Add(item);

            ReloadZedGraph();
        }

        public void RemoveStudent(int id)
        {
            int index = -1;

            for (var i = 0; i < listViewStudents.Items.Count; i++)
            {
                var listViewItem = listViewStudents.Items[i];

                if (listViewItem.SubItems[3].Text != id.ToString()) continue;

                index = i;
                break;
            }

            if (index == -1)
            {
                return;
            }

            listViewStudents.Items.RemoveAt(index);
            ReloadZedGraph();
        }

        public FromMain(IKernel kernel)
        {
            _kernel = kernel;
            _presenter = new StudentsViewPresenter(this, kernel.Get<IStudentsController>());

            InitializeComponent();
            InitStudentsLists();
            ReloadZedGraph();
        }

        private void InitStudentsLists()
        {
            listViewStudents.Clear();
            
            ColumnHeader headerName = new ColumnHeader();
            headerName.Text = "Имя";
            headerName.Width = 120;
            listViewStudents.Columns.Add(headerName);
            ColumnHeader headerSpec = new ColumnHeader();
            headerSpec.Text = "Специальность";
            headerSpec.Width = 120;
            listViewStudents.Columns.Add(headerSpec);
            ColumnHeader headerGroup = new ColumnHeader();
            headerGroup.Text = "Группа";
            headerGroup.Width = 60;
            listViewStudents.Columns.Add(headerGroup);

            listViewStudents.Items.Clear();

            List<Student> students = _presenter.GetStudents();

            foreach (Student student in students)
            {
                ListViewItem item = new ListViewItem(student.Name);
                item.SubItems.Add(student.Speciality);
                item.SubItems.Add(student.Group);
                item.SubItems.Add(student.Id.ToString());

                listViewStudents.Items.Add(item);
            }
        }

        private void ReloadZedGraph()
        {
            var groups = _presenter.GetGroups();

            if (!groups.Any())
            {
                return;
            }

            GraphPane pane = zedGraph.GraphPane;

            pane.CurveList.Clear();

            string[] names = groups.Select(group => group.Key).ToArray();
            double[] values = groups.Select(group => Convert.ToDouble(group.Count())).ToArray();

            BarItem curve = pane.AddBar("Гистограмма", null, values, Color.Blue);

            // Настроим ось X так, чтобы она отображала текстовые данные
            pane.XAxis.Type = AxisType.Text;

            // Уставим для оси наши подписи
            pane.XAxis.Scale.TextLabels = names;

            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            zedGraph.AxisChange();

            // Обновляем график
            zedGraph.Invalidate();
        }

        private void buttonFill_Click(object sender, EventArgs e)
        {
            EventStudentsFill.Invoke(this, null);
        }

        private void buttonAddStudent_Click(object sender, EventArgs e)
        {
            FormAddStudent formAddStudent = new FormAddStudent(_kernel);

            formAddStudent.Show();
            Hide();
            formAddStudent.Closed += (o, args) => Show();
        }

        private void listViewStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDelete.Enabled = listViewStudents.SelectedItems.Count > 0;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selected = listViewStudents.SelectedItems;

            foreach (ListViewItem item in selected)
            {
                EventStudentDelete.Invoke(this, Convert.ToInt32(item.SubItems[3].Text));
            }

            buttonDelete.Enabled = false;
        }
    }
}
