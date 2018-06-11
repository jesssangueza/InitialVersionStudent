

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
        public async void Init()
        { 
            this.repositorio = new StudentRepository(test:true);
            this.studentController = new StudentController(this.repositorio);           
        }

        [TestMethod]        
        public void TestMethodList()
        {
            int expected = 5;
            var lista = this.studentController.List().ToList();
            Assert.AreEqual(expected, lista.Count, "List Fail");
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

        [TestMethod]
        public void TestMethodUpdate()
        {
            var student = new Student() { StudentType = TypeStudent.University, Name = "Leia", Gender = 'F' };
            var result = this.studentController.Update(student);
            Assert.IsTrue(result, "Update Fail");
        }

        [TestMethod]
        public void TestMethodDelete()
        {            
            var result = this.studentController.Delete(2);
            Assert.IsTrue(result, "Delete Fail");
        }

        [TestCleanup]
        public void Finish()
        {
            this.repositorio = null;
            this.studentController = null;
        }
    }
}
