using ModelLayer.Controllers;
using Shared;

namespace Presenter
{
    public class StudentAddViewPresenter
    {
        private readonly IStudentAddView _view;
        private readonly IStudentsController _controller;

        public StudentAddViewPresenter(IStudentAddView view, IStudentsController controller)
        {
            this._view = view;
            this._controller = controller;

            this._view.EventStudentAdd += ViewOnEventStudentAdd;
        }

        private void ViewOnEventStudentAdd(object sender, AddStudentArgs e)
        {
            _controller.AddStudent(e.Name, e.Speciality, e.Group);
        }
    }
}