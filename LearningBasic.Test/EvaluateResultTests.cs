﻿namespace LearningBasic.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EvaluateResultTests : BaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EvaluateResult_WithNullMessage_ThrowsArgumentNullException()
        {
            var result = new EvaluateResult((string)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EvaluateResult_WithNullObject_ThrowsArgumentNullException()
        {
            var result = new EvaluateResult((object)null);
        }

        [TestMethod]
        public void EvaluateResult_WithMessage_StoresMessage()
        {
            var result = new EvaluateResult("message");

            Assert.AreEqual("message", result.Message);
        }

        [TestMethod]
        public void EvaluateResult_WithMessage_SetsHasMessage()
        {
            var result = new EvaluateResult("message");

            Assert.IsTrue(result.HasMessage);
        }

        [TestMethod]
        public void EvaluateResult_WithFormat_StoresFormattedMessage()
        {
            var result = new EvaluateResult("{0}", "format");

            Assert.AreEqual("format", result.Message);
        }

        [TestMethod]
        public void HasMessage_OfEmptyEvaluateResult_IsFalse()
        {
            Assert.IsFalse(EvaluateResult.None.HasMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Message_OfEmptyEvaluateResult_ThrowsInvalidOperationException()
        {
            var message = EvaluateResult.None.Message;
        }
    }
}
