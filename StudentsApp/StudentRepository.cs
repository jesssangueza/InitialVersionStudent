

namespace StudentsApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Clase Student Repository That implements IRepository
    /// </summary>
    public class StudentRepository: IRepository
    {
        private List<Student> lista;
        
        private string path = "..\\..\\AppData\\students.csv";               

        /// <summary>
        /// Class constructor.
        /// </summary>
        public StudentRepository()
        {
            this.lista = new List<Student>();
        }

        /// <summary>
        /// Class constructor to be called from Test Project.
        /// </summary>
        public StudentRepository(bool test)
        {
            this.lista = new List<Student>();
            this.path = "..\\..\\..\\StudentsApp\\AppData\\students.csv";           
        }
        
        /// <summary>
        /// Method that read lines from the file;
        /// </summary>
        public Task<int> ReadStudentsAsync()
        {            
            try
            {
                this.lista.Clear();
                return Task.Run(() =>
                {
                    var lines = File.ReadLines(path).Select(l => l.Split(','));
                    
                    foreach (var line in lines)
                    {
                        this.lista.Add(
                            new Student()
                            {
                                StudentId = Convert.ToInt32(line[0]),
                                StudentType = (TypeStudent)Enum.Parse(typeof(TypeStudent), line[1].ToString()),
                                Name = line[2],
                                Gender = Convert.ToChar(line[3]),
                                TimeStamp = line[4],
                            });
                    }

                    return this.lista.Count;
                });
                
            }
            catch (Exception ex)
            {
                return Task.Run(() => { return 0; });
            }
        }

        /// <summary>
        /// Method that list all students.
        /// </summary>
        /// <returns>
        /// List of students.
        /// </returns>
        public IEnumerable<Student> List()
        {
            ReadStudentsAsync().Wait();
            return this.lista;
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
            try 
            {
                student.TimeStamp = GetTimeStamp();               
                student.StudentId = this.GetLastId(); // Get the last ID from the csv file.

                var newLine = new StringBuilder();
                newLine.Append(student.StudentId.ToString());
                newLine.Append(",");
                newLine.Append(student.StudentType.ToString());
                newLine.Append(",");
                newLine.Append(student.Name);
                newLine.Append(",");
                newLine.Append(student.Gender.ToString());
                newLine.Append(",");
                newLine.AppendLine(student.TimeStamp);

                File.AppendAllText(path, newLine.ToString());                
                return true;
            }
            catch(Exception ex) 
            {
                return false;
            }         
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
            try
            {
                var studentTpUpdate = this.lista.FirstOrDefault(s => s.StudentId == student.StudentId);
                studentTpUpdate.Name = student.Name;
                studentTpUpdate.Gender = student.Gender;
                studentTpUpdate.StudentType = student.StudentType;
                studentTpUpdate.TimeStamp = GetTimeStamp();

                return this.SaveFile();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Method to delete a student
        /// </summary>
        /// <param name="studentId">
        /// Student object.
        /// </param>
        /// <returns>
        /// True if deleting was succesfull.
        /// </returns>
        public bool Delete(int studentId)
        {
            try
            {
                this.lista.Remove(this.lista.First(s=> s.StudentId == studentId));
                return this.SaveFile();
            }
            catch (Exception ex)
            {
                return false;
            }
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
            ReadStudentsAsync().Wait();
            switch (parameter)
            {
                case 'T':
                    var auxType = (TypeStudent) Enum.Parse(typeof(TypeStudent), value);
                    return this.lista.Where(s => s.StudentType == auxType).OrderByDescending(s => s.TimeStamp);
                    break;
                case 'N':
                    return this.lista.Where(s => s.Name == value).OrderBy(s=> s.Name);
                    break;
                case 'G':
                    var auxGender = Convert.ToChar(value);
                    return this.lista.Where(s => s.Gender == auxGender).OrderByDescending(s=>s.TimeStamp);;
                    break;
                default: 
                    return new List<Student>();
                    break;
            }            
        }

        /// <summary>
        /// Method that get the next Student Id.
        /// </summary>
        /// <returns>
        /// Next Student ID.
        /// </returns>
        private int GetLastId()
        {
            var lastId = File.ReadLines(path).Last().Split(',')[0];
            return Convert.ToInt32(lastId) + 1;

        }

        /// <summary>
        /// Method that convert the List<Student> into List<string>.
        /// </summary>
        /// <returns>
        /// List<string>
        /// </returns>
        private List<string> GetStrings()
        {
            var strings = new List<string>();
            foreach (var student in this.lista)
            {
                var newLine = new StringBuilder();
                newLine.Append(student.StudentId.ToString());
                newLine.Append(",");
                newLine.Append(student.StudentType.ToString());
                newLine.Append(",");
                newLine.Append(student.Name);
                newLine.Append(",");
                newLine.Append(student.Gender.ToString());
                newLine.Append(",");
                newLine.Append(student.TimeStamp);
                strings.Add(newLine.ToString());
            }
            return strings;
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
            cadenaTimeStamp.Append(DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month : DateTime.Now.Month.ToString());
            cadenaTimeStamp.Append(DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day : DateTime.Now.Day.ToString());
            cadenaTimeStamp.Append(DateTime.Now.Hour.ToString());
            cadenaTimeStamp.Append(DateTime.Now.Minute.ToString());
            cadenaTimeStamp.Append(DateTime.Now.Second.ToString());
            return cadenaTimeStamp.ToString();
        }

        /// <summary>
        /// Method that save the list into the file.
        /// </summary>
        /// <returns>
        /// True if the save was succesfull.
        /// </returns>
        public bool SaveFile()
        {
            try
            {
                File.WriteAllLines(path, GetStrings());                

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }            
        }
    }
}
