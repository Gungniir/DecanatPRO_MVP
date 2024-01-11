using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Models;

namespace Shared
{
    public interface IStudentsView
    {
        event EventHandler<int> EventStudentDelete;
        event EventHandler EventStudentsFill;

        void AddStudent(Student student);
        void RemoveStudent(int id);
    }
}
