
namespace StudentsApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Student Controlle Class.
    /// </summary>
    public class StudentController
    {
        /// <summary>
        /// Repository instance.
        /// </summary>
        private IRepository repository;

        /// <summary>
        /// Class Constructor.
        /// </summary>
        /// <param name="repository">
        /// Repository instance.
        /// </param>
        public StudentController(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Method to create a new student
        /// </summary>
        /// <param name="student">
        /// Student object.
        /// </param>
        /// <returns>
        /// True if insertion was true.
        /// </returns>
        public bool Create(Student student)
        {
            return this.repository.Create(student);           
        }

        /// <summary>
        /// Method to update a student
        /// </summary>
        /// <param name="student">
        /// Student object.
        /// </param>
        /// <returns>
        /// True if updating was true.
        /// </returns>
        public bool Update(Student student)
        {
            return this.repository.Update(student);
        }

        /// <summary>
        /// Method to update a student
        /// </summary>
        /// <param name="studentId">
        /// Student ID.
        /// </param>
        /// <returns>
        /// True if updating was true.
        /// </returns>
        public bool Delete(int studentId)
        {
            return this.repository.Delete(studentId);
        }

        /// <summary>
        /// Method tha list all students.
        /// </summary>
        /// <returns>
        /// List of students.
        /// </returns>
        public IEnumerable<Student> List()
        {
            return this.repository.List();
        }

        /// <summary>
        /// Method that look for specific students.
        /// </summary>
        /// <param name="parameter">Parameter to search</param>
        /// <param name="value">Value to search.</param>
        /// <returns>
        ///  List of students.
        /// </returns>
        public IEnumerable<Student> Search(char parameter, string value)
        {
            return this.repository.Search(parameter, value);
        }

        /// <summary>
        /// Method that Reads all Students.
        /// </summary>
        /// <returns>
        /// Count of Students.
        /// </returns>
        public async Task<int> ReadStudentsAsync()
        {
            return await this.repository.ReadStudentsAsync();
        }
    }
}
