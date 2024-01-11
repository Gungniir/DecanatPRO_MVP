using ModelLayer.Models;
using ModelLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelLayer.Controllers
{
    internal class StudentsController : IStudentsController
    {
        private readonly IRepository<Student> _studentRepository;
        public event EventHandler<Student> EventStudentAdded = delegate { };
        public event EventHandler<int> EventStudentRemoved = delegate { };

        public StudentsController(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void AddStudent(string name, string speciality, string group)
        {
            var student = _studentRepository.Create(new Student
            {
                Name = name,
                Speciality = speciality,
                Group = group,
            });

            EventStudentAdded.Invoke(this, student);
        }

        public bool DeleteStudent(int id)
        {
            var result =_studentRepository.Delete(id);

            if (result)
            {
                EventStudentRemoved.Invoke(this, id);
            }

            return result;
        }

        public void FillStudents()
        {
            int baseSeed = DateTime.Now.Millisecond;

            for (int i = 0; i < 8; i++)
            {
                Student student = new Student();
                student.FillRandom(baseSeed * (100 + i));

                student = _studentRepository.Create(student);

                EventStudentAdded.Invoke(this, student);
            }
        }

        public List<Student> ShowTable()
        {
            return _studentRepository.Read();
        }

        public IEnumerable<IGrouping<string, Student>> ShowGist()
        {
            return _studentRepository.Read().GroupBy(student => student.Speciality);
        }
    }
}
