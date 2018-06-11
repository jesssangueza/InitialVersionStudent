
namespace StudentsApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface IRepository
    /// </summary>
    public interface IRepository
    {
        bool Create(Student student);

        bool Delete(int studentId);

        bool Update(Student student);

        IEnumerable<Student> List();

        IEnumerable<Student> Search(char parameter, string value);

        Task<int> ReadStudentsAsync();
    }
}
