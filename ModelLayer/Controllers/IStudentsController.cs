using ModelLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ModelLayer.Controllers;

public interface IStudentsController
{
    public event EventHandler<Student> EventStudentAdded;
    public event EventHandler<int> EventStudentRemoved;

    public void AddStudent(string name, string speciality, string group);

    public bool DeleteStudent(int id);

    public void FillStudents();

    public List<Student> ShowTable();

    public IEnumerable<IGrouping<string, Student>> ShowGist();
}