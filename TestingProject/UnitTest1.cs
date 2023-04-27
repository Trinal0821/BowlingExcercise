using BowlingScore;
using System.Text;

namespace TestingProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void checkMultipleInputs()
        {
            Bowling bowling = new Bowling("");
            Assert.AreEqual(1, bowling.checkInput("X", 1));

            Assert.AreEqual(0, bowling.checkInput("/", 1));
            Assert.AreEqual(-1, bowling.checkInput("5", 1));
        }

        [TestMethod]
        public void checkFrames1()
        {
            Bowling bowling = new Bowling("");

            Assert.AreEqual(1, bowling.checkFrames1(1, "X", 1));
            Assert.AreEqual(0, bowling.checkFrames1(1, "/", 1));
            Assert.AreEqual(-1, bowling.checkFrames1(1, "5", 1));
        }

        [TestMethod]
        public void checkFrames2() 
        {
            Bowling bowling = new Bowling("");

            Assert.AreEqual(0, bowling.checkFrames2(1, "8", "2", 1));
            Assert.AreEqual(0, bowling.checkFrames2(1, "5", "5", 1));
            Assert.AreEqual(0, bowling.checkFrames2(1,"2", "/", 1));
            Assert.AreEqual(-1, bowling.checkFrames2(1, "3", "5", 5));
        }
    }
}