namespace Basic.Tests.Runtime
{
    using Basic.Runtime;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BuiltInFunctionsTests
    {
        [TestMethod]
        public void ASC_WithEmptyString_Returns0()
        {
            var actual = BuiltInFunctions.ASC("");

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void ASC_WithA_Returns65()
        {
            var actual = BuiltInFunctions.ASC("A");
            const int wellKnownAsciiCodeOfLetterA = 65;

            Assert.AreEqual(wellKnownAsciiCodeOfLetterA, actual);
        }

        [TestMethod]
        public void ASC_WithABC_Returns65()
        {
            var actual = BuiltInFunctions.ASC("ABC");
            const int wellKnownAsciiCodeOfLetterA = 65;

            // B and C shold be ignored.
            Assert.AreEqual(wellKnownAsciiCodeOfLetterA, actual);
        }

        [TestMethod]
        public void MID_WithABCDEFAnd3_RemovesLeft2Characters()
        {
            var actual = BuiltInFunctions.MID("ABCDEF", 3);

            Assert.AreEqual("CDEF", actual);
        }

        [TestMethod]
        public void MID_WithABCDEFAnd2And3_Returns2And3And4Characters()
        {
            var actual = BuiltInFunctions.MID("ABCDEF", 2, 3);

            Assert.AreEqual("BCD", actual);
        }

        [TestMethod]
        public void INSTR_WithXYXYXYAndYX_Returns2()
        {
            var actual = BuiltInFunctions.INSTR("XYXYXY", "YX");

            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void INSTR_With3AndXYXYXYAndYX_Returns4()
        {
            var actual = BuiltInFunctions.INSTR(3, "XYXYXY", "YX");

            Assert.AreEqual(4, actual);
        }

        [TestMethod]
        public void INSTRREV_WithXYXYXYAndYX_Returns4()
        {
            var actual = BuiltInFunctions.INSTRREV("XYXYXY", "YX");

            Assert.AreEqual(4, actual);
        }

        [TestMethod]
        public void INSTRREV_With3AndXYXYXYAndYX_Returns2()
        {
            var actual = BuiltInFunctions.INSTRREV(3, "XYXYXY", "YX");

            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void JOIN_WithoutDelimiter_UsesSpaceAsDelimiter()
        {
            var actual = BuiltInFunctions.JOIN(new object[] { 1, 2, 3 });

            Assert.AreEqual("1 2 3", actual);
        }

        [TestMethod]
        public void SPLIT_WithoutDelimiter_UsesSpaceAsDelimiter()
        {
            var actual = BuiltInFunctions.SPLIT("1 2 3");

            CollectionAssert.AreEqual(new[] { "1", "2", "3" }, actual);
        }

        [TestMethod]
        public void MAX_WithEmptyArray_ThrowsException()
        {
            var actual = BuiltInFunctions.MAX(new object[0]);
        }

        [TestMethod]
        public void MIN_WithEmptyArray_ThrowsException()
        {
            var actual = BuiltInFunctions.MIN(new object[0]);
        }

        [TestMethod]
        public void CEIL_WithInteger_ReturnsSameInteger()
        {
            var actual = BuiltInFunctions.CEIL(-12345);

            Assert.AreEqual(-12345, actual);
        }

        [TestMethod]
        public void FLOOR_WithInteger_ReturnsSameInteger()
        {
            var actual = BuiltInFunctions.FLOOR(-12345);

            Assert.AreEqual(-12345, actual);
        }
    }
}
