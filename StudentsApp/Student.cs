

namespace StudentsApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    /// <summary>
    /// Student Class.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Student Type [Kinde|Elementary|High|University]
        /// </summary>
        public TypeStudent StudentType { get; set; }

        /// <summary>
        /// Student's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Student's gender.
        /// </summary>
        public char Gender { get; set; }

        /// <summary>
        /// TimeStamp of the record.
        /// </summary>
        public string TimeStamp { get; set; }
    }
}
