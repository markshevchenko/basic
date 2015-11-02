namespace LearningBasic.Test.Parsing
{
    using System.Text;
    using LearningBasic.Parsing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TextReaderExtensionsTests : BaseTests
    {
        [TestMethod]
        public void IsEof_WithEmptyInputStream_IsTrue()
        {
            using (var reader = MakeReader(""))
            {
                Assert.IsTrue(reader.IsEof());
            }
        }

        [TestMethod]
        public void IsEof_WithNonEmptyInputStream_IsFalse()
        {
            using (var reader = MakeReader("non empty stream"))
            {
                Assert.IsFalse(reader.IsEof());
            }
        }

        [TestMethod]
        public void SkipIf_WithExpectedCharacter_ReturnsTrue()
        {
            using (var reader = MakeReader("abc"))
            {
                Assert.IsTrue(reader.SkipIf('a'));
            }
        }

        [TestMethod]
        public void SkipIf_WithExpectedCharacter_SkipsCharacter()
        {
            using (var reader = MakeReader("abc"))
            {
                reader.SkipIf('a');
                Assert.AreEqual('b', reader.Read());
            }
        }

        [TestMethod]
        public void SkipIf_WithUnexpectedCharacter_ReturnsFalse()
        {
            using (var reader = MakeReader("abc"))
            {
                Assert.IsFalse(reader.SkipIf('b'));
            }
        }

        [TestMethod]
        public void SkipIf_WithUnexpectedCharacter_KeepsCharacter()
        {
            using (var reader = MakeReader("abc"))
            {
                reader.SkipIf('!');
                Assert.AreEqual('a', reader.Read());
            }
        }

        [TestMethod]
        public void SkipIf_WithEof_ReturnsFalse()
        {
            using (var reader = MakeReader(""))
            {
                Assert.IsFalse(reader.SkipIf('a'));
            }
        }

        [TestMethod]
        public void SkipWhile_WithExpectedCharacters_SkipsCharacters()
        {
            using (var reader = MakeReader("0123456789abcdef"))
            {
                reader.SkipWhile(char.IsDigit);
                Assert.AreEqual('a', reader.Read());
            }
        }

        [TestMethod]
        public void TakeIf_WithExpectedCharacter_ReturnsTrue()
        {
            using (var reader = MakeReader("abc"))
            {
                var builder = new StringBuilder();
                Assert.IsTrue(reader.TakeIf('a', builder));
            }
        }

        [TestMethod]
        public void TakeIf_WithExpectedCharacter_ReadsCharacter()
        {
            using (var reader = MakeReader("abc"))
            {
                var builder = new StringBuilder();

                reader.TakeIf('a', builder);

                Assert.AreEqual('b', reader.Read());
            }
        }

        [TestMethod]
        public void TakeIf_WithExpectedCharacter_AppendsCharacterToBuilder()
        {
            using (var reader = MakeReader("abc"))
            {
                var builder = new StringBuilder();

                reader.TakeIf('a', builder);

                Assert.AreEqual("a", builder.ToString());
            }
        }

        [TestMethod]
        public void TakeIf_WithUnexpectedCharacter_ReturnsFalse()
        {
            using (var reader = MakeReader("abc"))
            {
                var builder = new StringBuilder();
                Assert.IsFalse(reader.TakeIf('b', builder));
            }
        }

        [TestMethod]
        public void TakeIf_WithUnexpectedCharacter_DoesntReadCharacter()
        {
            using (var reader = MakeReader("abc"))
            {
                var builder = new StringBuilder();

                reader.TakeIf('b', builder);

                Assert.AreEqual('a', reader.Read());
            }
        }

        [TestMethod]
        public void TakeIf_WithUnexpectedCharacter_DoesntAppendCharacterToBuilder()
        {
            using (var reader = MakeReader("abc"))
            {
                var builder = new StringBuilder();

                reader.TakeIf('b', builder);

                Assert.AreEqual("", builder.ToString());
            }
        }

        [TestMethod]
        public void TakeIf_WithEof_ReturnsFalse()
        {
            using (var reader = MakeReader(""))
            {
                var builder = new StringBuilder();

                Assert.IsFalse(reader.TakeIf(c => true, builder));
            }
        }

        [TestMethod]
        public void TakeWhile_WithAllExpectedCharacters_ReadsAllCharacters()
        {
            using (var reader = MakeReader("aBcEeFgH"))
            {
                var builder = new StringBuilder();

                reader.TakeWhile(char.IsLetter, builder);

                const int Eof = -1;

                Assert.AreEqual(Eof, reader.Peek());
            }
        }

        [TestMethod]
        public void TakeWhile_WithAllExpectedCharacters_AppendsAllCharactersToBuilder()
        {
            using (var reader = MakeReader("aBcEeFgH"))
            {
                var builder = new StringBuilder();

                reader.TakeWhile(char.IsLetter, builder);

                Assert.AreEqual("aBcEeFgH", builder.ToString());
            }
        }

        [TestMethod]
        public void TakeWhile_WithExpectedCharacters_ReadsCharacters()
        {
            using (var reader = MakeReader("abcd4567"))
            {
                var builder = new StringBuilder();

                reader.TakeWhile(char.IsLetter, builder);

                var nextCharacterInStream = reader.Read();
                Assert.AreEqual('4', nextCharacterInStream);
            }
        }

        [TestMethod]
        public void TakeWhile_WithExpectedCharacters_AppendsCharactersToBuilder()
        {
            using (var reader = MakeReader("abcd4567"))
            {
                var builder = new StringBuilder();

                reader.TakeWhile(char.IsLetter, builder);

                Assert.AreEqual("abcd", builder.ToString());
            }
        }
    }
}
