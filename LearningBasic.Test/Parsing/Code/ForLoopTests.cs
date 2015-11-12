namespace LearningBasic.Parsing.Code
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ForLoopTests : BaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ForLoop_WithNullVariable_ThrowsArguementNullException()
        {
            Expression variable = null;
            Expression from = Expression.Constant(0);
            Expression to = Expression.Constant(2);
            Expression step = Expression.Constant(1);

            var forLoop = new ForLoop(variable, from, to, step);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ForLoop_WithNullFrom_ThrowsArguementNullException()
        {
            var i = new WriteableExpressionVariable(0);
            Expression variable = i.expression;
            Expression from = null;
            Expression to = Expression.Constant(2);
            Expression step = Expression.Constant(1);

            var forLoop = new ForLoop(variable, from, to, step);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ForLoop_WithNullTo_ThrowsArguementNullException()
        {
            var i = new WriteableExpressionVariable(0);
            Expression variable = i.expression;
            Expression from = Expression.Constant(0);
            Expression to = null;
            Expression step = Expression.Constant(1);

            var forLoop = new ForLoop(variable, from, to, step);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ForLoop_WithNullStep_ThrowsArguementNullException()
        {
            var i = new WriteableExpressionVariable(0);
            Expression variable = i.expression;
            Expression from = Expression.Constant(0);
            Expression to = Expression.Constant(2);
            Expression step = null;

            var forLoop = new ForLoop(variable, from, to, step);
        }

        [TestMethod]
        public void ForLoop_WithoutStep_SetsStepTo1()
        {
            var i = new WriteableExpressionVariable(0);
            Expression variable = i.expression;
            Expression from = Expression.Constant(0);
            Expression to = Expression.Constant(2);

            var forLoop = new ForLoop(variable, from, to);
            var actual = (forLoop.Step as ConstantExpression).Value;

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void IsOver_AtStart_IsFalse()
        {
            var i = new WriteableExpressionVariable(0);
            Expression variable = i.expression;
            Expression from = Expression.Constant(0);
            Expression to = Expression.Constant(2);

            var forLoop = new ForLoop(variable, from, to);

            Assert.IsFalse(forLoop.IsOver);
        }

        [TestMethod]
        public void IsOver_AtMiddle_IsFalse()
        {
            var i = new WriteableExpressionVariable(0);
            Expression variable = i.expression;
            Expression from = Expression.Constant(0);
            Expression to = Expression.Constant(2);

            var forLoop = new ForLoop(variable, from, to);
            forLoop.TakeStep();
            forLoop.TakeStep();

            Assert.IsFalse(forLoop.IsOver);
        }

        [TestMethod]
        public void IsOver_AtEnd_IsTrue()
        {
            var i = new WriteableExpressionVariable(0);
            Expression variable = i.expression;
            Expression from = Expression.Constant(0);
            Expression to = Expression.Constant(2);

            var forLoop = new ForLoop(variable, from, to);
            forLoop.TakeStep();
            forLoop.TakeStep();
            forLoop.TakeStep();

            Assert.IsTrue(forLoop.IsOver);
        }

        [TestMethod]
        public void TakeStep_WhenCalled_ChangesVariableValue()
        {
            var i = new WriteableExpressionVariable(0);
            Expression variable = i.expression;
            Expression from = Expression.Constant(0);
            Expression to = Expression.Constant(2);

            var forLoop = new ForLoop(variable, from, to);
            var notExpected = i.value;

            forLoop.TakeStep();
            var actual = i.value;

            Assert.AreNotEqual(notExpected, actual);
        }

        [TestMethod]
        public void IsBelongsToInterval_WithVariableInsideDirectInterval_ReturnsTrue()
        {
            Expression variable = Expression.Constant(1);
            Expression from = Expression.Constant(0);
            Expression to = Expression.Constant(2);

            var condition = ForLoop.IsBelongsToInterval(variable, from, to);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void IsBelongsToInterval_WithVariableInsideInverseInterval_ReturnsTrue()
        {
            Expression variable = Expression.Constant(1);
            // `from > to` here, it means inverse interval.
            Expression from = Expression.Constant(2);
            Expression to = Expression.Constant(0);

            var condition = ForLoop.IsBelongsToInterval(variable, from, to);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void IsBelongsToInterval_WithVariableSameAsFrom_ReturnsTrue()
        {
            Expression variable = Expression.Constant(0);
            Expression from = Expression.Constant(0);
            Expression to = Expression.Constant(2);

            var condition = ForLoop.IsBelongsToInterval(variable, from, to);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void IsBelongsToInterval_WithVariableSameAsTo_ReturnsTrue()
        {
            Expression variable = Expression.Constant(2);
            Expression from = Expression.Constant(0);
            Expression to = Expression.Constant(2);

            var condition = ForLoop.IsBelongsToInterval(variable, from, to);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void IsBelongsToInterval_WithVariableLessThanFrom_ReturnsFalse()
        {
            Expression variable = Expression.Constant(-1);
            Expression from = Expression.Constant(0);
            Expression to = Expression.Constant(2);

            var condition = ForLoop.IsBelongsToInterval(variable, from, to);

            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void IsBelongsToInterval_WithVariableGreaterThanTo_ReturnsFalse()
        {
            Expression variable = Expression.Constant(3);
            Expression from = Expression.Constant(0);
            Expression to = Expression.Constant(2);

            var condition = ForLoop.IsBelongsToInterval(variable, from, to);

            Assert.IsFalse(condition);
        }
    }
}
