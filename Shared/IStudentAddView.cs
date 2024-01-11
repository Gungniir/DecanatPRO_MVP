using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Models;

namespace Shared
{
    public interface IStudentAddView
    {
        event EventHandler<AddStudentArgs> EventStudentAdd;
    }

    public struct AddStudentArgs
    {
        public string Name { get; set; }
        public string Speciality { get; set; }
        public string Group { get; set; }
    }
}
