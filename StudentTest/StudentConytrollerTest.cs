

namespace StudentTest
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    using StudentsApp;

    [TestClass]
    public class StudentConytrollerTest
    {
        private IRepository repositorio;
        private StudentController studentController;
        
        [TestInitialize]
        public void Init()
        { 
            this.repositorio = new StudentRepository();
            this.studentController = new StudentController(this.repositorio);
        }

        [TestMethod]        
        public void TestMethodList()
        {
            int expected = 4;
            var lista = this.studentController.List().ToList();
            Assert.AreEqual<int>(expected, lista.Count, "List Fail");
        }

        [TestMethod]
        public void TestMethodSearch()
        {
            int expected = 2;
            var parameter = 'G';
            var value = "M";
            var lista = this.studentController.Search(parameter,value).ToList();
            Assert.AreEqual<int>(expected, lista.Count, "Search Fail");
        }

        [TestMethod]
        public void TestMethodCreate()
        {
            var student = new Student() { StudentType = TypeStudent.University, Name = "Jhon Rambo", Gender = 'M' };
            var result = this.studentController.Create(student);
            Assert.IsTrue(result, "Create Fail");
        }

        [TestCleanup]
        public void Finish()
        {
            this.repositorio = null;
            this.studentController = null;
        }
    }
}
