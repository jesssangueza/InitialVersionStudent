

namespace StudentsApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class Program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Instance of Student Repository.
        /// </summary>
        private static IRepository repository = new StudentRepository();

        /// <summary>
        /// Instance of Student Controller.
        /// </summary>
        private static StudentController controlador = new StudentController(repository);

        /// <summary>
        /// Main Method.
        /// </summary>
        /// <param name="args">Arguments</param>
        static void Main(string[] args)
        {
            Initialization();                       
        }

        /// <summary>
        /// According to the instruction.
        /// "Read students at system startup from a CSV input file"
        /// </summary>
        static public async void Initialization()
        {
             var totalStudents = controlador.ReadStudentsAsync();
             DisplayMainMenu();
             await totalStudents;
        }

        /// <summary>
        /// Method that display the main menu.
        /// </summary>
        public static void DisplayMainMenu()
        {
            Console.WriteLine("##########################");
            Console.WriteLine("###### STUDENTS APP ######");

            Console.WriteLine("Options:");
            Console.WriteLine("L : List all students");
            Console.WriteLine("N : Create new student");
            Console.WriteLine("U : Update student");
            Console.WriteLine("D : Delete student");
            Console.WriteLine("S : Searh students");
            Console.WriteLine("Q : Exit");
            EjectAction(Console.ReadKey());
        }

        /// <summary>
        /// Method that eject actions accroding to the option selected.
        /// </summary>
        /// <param name="option">
        /// Option selected.
        /// </param>
        public static void EjectAction(ConsoleKeyInfo option)
        {
            switch (option.KeyChar)
            {
                #region List
                case 'L':
                    ShowList(controlador.List());
                    
                    break;
                #endregion

                #region New
                case 'N':
                    var newStudent = new Student();
                    Console.WriteLine("\nEnter the StudentType: [1 - Kinder | 2 - Elementary | 3 - High | 4 - University]");
                    newStudent.StudentType = (TypeStudent) Convert.ToInt16(Console.ReadKey().KeyChar.ToString());
                    Console.WriteLine("\nEnter the Name:");
                    newStudent.Name = Console.ReadLine();
                    Console.WriteLine("\nEnter the Gender: [M - Male | F - Female]");
                    newStudent.Gender = Console.ReadKey().KeyChar;
                    if (controlador.Create(newStudent))
                    {
                        Console.WriteLine("\nStudent Added");
                    }
                    else
                    {
                        Console.WriteLine("\nError!! Something happen try Again");
                    }

                    break;
                #endregion

                #region Update
                case 'U':
                    var updateStudent = new Student();
                    Console.WriteLine("\nEnter the Student ID you want to update:");
                    updateStudent.StudentId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\nEnter the new StudentType: [1 - Kinder | 2 - Elementary | 3 - High | 4 - University]");
                    updateStudent.StudentType = (TypeStudent)Convert.ToInt16(Console.ReadKey().KeyChar.ToString());
                    Console.WriteLine("\nEnter the new Name:");
                    updateStudent.Name = Console.ReadLine();
                    Console.WriteLine("\nEnter the new Gender: [M - Male | F - Female]");
                    updateStudent.Gender = Console.ReadKey().KeyChar;
                    if (controlador.Update(updateStudent))
                    {
                        Console.WriteLine("\nStudent updated");
                    }
                    else
                    {
                        Console.WriteLine("\nError!! Something happen try Again");
                    }

                    break;
                #endregion

                #region Delete
                case 'D':                    
                    Console.WriteLine("\nEnter the Student ID you want to delete:");
                    var studentId = Convert.ToInt32(Console.ReadLine());
                    if (controlador.Delete(studentId))
                    {
                        Console.WriteLine("\nStudent deleted");
                    }
                    else
                    {
                        Console.WriteLine("\nError!! Something happen try Again");
                    }

                    break;
                #endregion

                #region Search
                case 'S':
                    Console.WriteLine("\nSearch For: [T - Type | N - Name | G - Gender] ");
                    var parameterofSearch = Console.ReadKey().KeyChar;
                    switch (parameterofSearch)
                    {
                        case 'T':
                            Console.WriteLine("\nEnter the StudentType: [1 - Kinder | 2 - Elementary | 3 - High | 4 - University] ");
                            break;
                        case 'N':
                            Console.WriteLine("\nEnter the Name ");
                            break;
                        case 'G':
                            Console.WriteLine("\nEnter the Gender: [M - Male | F - Female] ");
                            break;

                    }
                    
                    var value = Console.ReadLine();
                    ShowList(controlador.Search(parameterofSearch, value));                    
                    break;
                #endregion

                #region Exit
                case 'Q':
                    Environment.Exit(0);
                    break;
                #endregion
                
            }

            DisplayMainMenu();
        }

        /// <summary>
        /// Method thah display the List of students
        /// </summary>
        /// <param name="list">
        /// Lis of students.
        /// </param>
        public static void ShowList(IEnumerable<Student> list)
        {
            
                    Console.WriteLine("\nID | Type | Name  | Gender    | TimeStamp");
                    Console.WriteLine("======================================");
                    var cadena = new StringBuilder();
                    foreach (var s in list)
                    {
                        cadena.Append(s.StudentId.ToString());
                        cadena.Append(" | ");
                        cadena.Append(s.StudentType.ToString());
                        cadena.Append(" | ");
                        cadena.Append(s.Name);
                        cadena.Append(" | ");
                        cadena.Append(s.Gender.ToString());
                        cadena.Append(" | ");
                        cadena.Append(s.TimeStamp);
                        Console.WriteLine(cadena.ToString());                        
                        cadena.Clear();
                    }

                    Console.WriteLine();
        }
    }
}
