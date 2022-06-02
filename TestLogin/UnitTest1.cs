using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic_dll;
using HospitalClient;
namespace TestLogin
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            BusinessLogic.UserName = "John";
            BusinessLogic.PassWord = "John@19";
            bool result = BusinessLogic.UserPass(BusinessLogic.UserName,BusinessLogic.PassWord);
            Assert.IsTrue(result);
        }
    }
}
