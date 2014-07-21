using System.Collections.Generic;
using NUnit.Framework;

namespace KataTask
{
    [TestFixture]
    public class KataMethodsTest
    {
        [Test]
        public void DictinaryReplacer_with_empty_string_and_dictionary()
        {
            // arrange
            const string message = "";
            var dictionary = new Dictionary<string, string>();

            // act
            var output = KataMethods.DictinaryReplacer(message, dictionary);

            // assert
            Assert.AreEqual("", output);
        }

        [Test]
        public void DictinaryReplacer_with_one_string_and_dictionary()
        {
            // arrange
            const string message = "$temp$";
            var dictionary = new Dictionary<string, string> {{"temp", "temporary"}};

            // act
            var output = KataMethods.DictinaryReplacer(message, dictionary);

            // assert
            Assert.AreEqual("temporary", output);
        }

        [Test]
        public void DictinaryReplacer_with_full_message_and_dictionary()
        {
            // arrange
            const string message = "$temp$ here comes the name $name$";
            var dictionary = new Dictionary<string, string>
            {
                { "temp", "temporary" },
                { "name", "John Doe"}
            };

            // act
            var output = KataMethods.DictinaryReplacer(message, dictionary);

            // assert
            Assert.AreEqual("temporary here comes the name John Doe", output);
        }

        [Test]
        public void MinesweeperTest_with_zero_line()
        {
            // arrange
            var input = new List<string>
            {
                "0 0"
            };

            // act
            var output = KataMethods.Minesweeper(input);

            // assert
            Assert.AreEqual(0, output.Count);
        }

        [Test]
        public void MinesweeperTest()
        {
            // arrange
            var input = new List<string>
            {
                "4 4",
                "*...",
                "....",
                ".*..",
                "....",
                "3 5",
                "**...",
                ".....",
                ".*...",
                "0 0"
            };

            // act
            var output = KataMethods.Minesweeper(input);

            // assert
            Assert.AreEqual("Field#1:", output[0]);
            Assert.AreEqual("*100", output[1]);
            Assert.AreEqual("2210", output[2]);
            Assert.AreEqual("1*10", output[3]);
            Assert.AreEqual("1110", output[4]);
            Assert.AreEqual("Field#2:", output[5]);
            Assert.AreEqual("**100", output[6]);
            Assert.AreEqual("33200", output[7]);
            Assert.AreEqual("1*100", output[8]);
        }
    }
}