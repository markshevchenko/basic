namespace LearningBasic.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProgramResultTests
    {
        [TestMethod]
        public void CreateBroken_WhenCalled_SetsIsBroke()
        {
            var result = ProgramResult.CreateBroken();

            Assert.IsTrue(result.IsBroken);
        }

        [TestMethod]
        public void CreateBroken_WhenCalled_ResetsIsCompletedAndIsAborted()
        {
            var result = ProgramResult.CreateBroken();

            Assert.IsFalse(result.IsCompleted || result.IsAborted);
        }

        [TestMethod]
        public void CreateBroken_WhenCalled_SetsExceptionToNull()
        {
            var result = ProgramResult.CreateBroken();

            Assert.IsNull(result.Exception);
        }

        [TestMethod]
        public void CreateCompleted_WhenCalled_SetsIsCompleted()
        {
            var result = ProgramResult.CreateCompleted();

            Assert.IsTrue(result.IsCompleted);
        }

        [TestMethod]
        public void CreateCompleted_WhenCalled_ResetsIsBrokenAndIsAborted()
        {
            var result = ProgramResult.CreateCompleted();

            Assert.IsFalse(result.IsBroken || result.IsAborted);
        }

        [TestMethod]
        public void CreateCompleted_WhenCalled_SetsExceptionToNull()
        {
            var result = ProgramResult.CreateCompleted();

            Assert.IsNull(result.Exception);
        }

        [TestMethod]
        public void CreateAborted_WhenCalled_SetsIsAborted()
        {
            var result = ProgramResult.CreateAborted(new Exception());

            Assert.IsTrue(result.IsAborted);
        }

        [TestMethod]
        public void CreateAborted_WhenCalled_ResetsIsBrokenAndIsCompleted()
        {
            var result = ProgramResult.CreateAborted(new Exception());

            Assert.IsFalse(result.IsBroken || result.IsCompleted);
        }

        [TestMethod]
        public void CreateAborted_WhenCalled_StoresException()
        {
            var result = ProgramResult.CreateAborted(new Exception("the program was aborted"));

            Assert.AreEqual("the program was aborted", result.Exception.Message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAborted_WithNullException_ThrowsArgumentNullException()
        {
            var result = ProgramResult.CreateAborted(null);
        }
    }
}
