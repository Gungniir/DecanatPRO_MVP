using System;
using System.Collections.Generic;
using System.Linq;
using ModelLayer.Controllers;
using ModelLayer.Models;
using Shared;

namespace Presenter
{
    public class StudentsViewPresenter
    {
        private readonly IStudentsView _view;
        private readonly IStudentsController _controller;

        public StudentsViewPresenter(IStudentsView view, IStudentsController controller)
        {
            this._view = view;
            this._controller = controller;

            this._view.EventStudentDelete += ViewOnEventStudentDelete;
            this._view.EventStudentsFill += ViewOnEventStudentsFill;
            this._controller.EventStudentAdded += ControllerOnEventStudentAdded;
            this._controller.EventStudentRemoved += ControllerOnEventStudentRemoved;
        }

        public List<Student> GetStudents()
        {
            return _controller.ShowTable();
        }

        public IEnumerable<IGrouping<string, Student>> GetGroups()
        {
            return _controller.ShowGist();
        }

        private void ViewOnEventStudentsFill(object sender, EventArgs e)
        {
            _controller.FillStudents();
        }

        private void ViewOnEventStudentDelete(object sender, int e)
        {
            _controller.DeleteStudent(e);
        }

        private void ControllerOnEventStudentRemoved(object sender, int id)
        {
            _view.RemoveStudent(id);
        }

        private void ControllerOnEventStudentAdded(object sender, Student e)
        {
            _view.AddStudent(e);
        }
    }
}
