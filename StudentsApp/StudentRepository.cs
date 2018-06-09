

namespace StudentsApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Clase Student Repository That implements IRepository
    /// </summary>
    public class StudentRepository: IRepository
    {
        private static List<Student> lista = new List<Student> { 
            new Student() { StudentType = TypeStudent.Kinder, Name = "Kevin", Gender = 'M', TimeStamp = GetTimeStamp() },
            new Student() { StudentType = TypeStudent.University, Name = "Jose", Gender = 'M', TimeStamp = GetTimeStamp() },
            new Student() { StudentType = TypeStudent.Elementary, Name = "Maria", Gender = 'F', TimeStamp = GetTimeStamp() },
            new Student() { StudentType = TypeStudent.High, Name = "Monica", Gender = 'F', TimeStamp = GetTimeStamp() }
        };

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
            student.TimeStamp = GetTimeStamp();
            lista.Add(student);
            return true;
        }

        /// <summary>
        /// Method tha list all students.
        /// </summary>
        /// <returns>
        /// List of students.
        /// </returns>
        public IEnumerable<Student> List() 
        {
            return lista;
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
            switch (parameter)
            {
                case 'T':
                    var auxType = (TypeStudent) Enum.Parse(typeof(TypeStudent), value);
                    return lista.Where(s => s.StudentType == auxType);
                    break;
                case 'N':
                    return lista.Where(s => s.Name == value);
                    break;
                case 'G':
                    var auxGender = Convert.ToChar(value);
                    return lista.Where(s => s.Gender == auxGender);
                    break;
                default: 
                    return new List<Student>();
                    break;
            }            
        }

        /// <summary>
        /// Method that obtain a timestamp value.
        /// </summary>
        /// <returns>
        /// TimeStamp value.
        /// </returns>
        private static string GetTimeStamp()
        {
            var cadenaTimeStamp = new StringBuilder();
            cadenaTimeStamp.Append(DateTime.Now.Year.ToString());
            cadenaTimeStamp.Append(DateTime.Now.Month.ToString());
            cadenaTimeStamp.Append(DateTime.Now.Day.ToString());
            cadenaTimeStamp.Append(DateTime.Now.Hour.ToString());
            cadenaTimeStamp.Append(DateTime.Now.Minute.ToString());
            cadenaTimeStamp.Append(DateTime.Now.Second.ToString());
            return cadenaTimeStamp.ToString();
        }
    }
}
