using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourierKata.Tests
{
    [TestClass]
    public abstract class BaseTests<T>
        where T : class
    {
        [TestInitialize]
        public virtual void Initialize() { }
    }
}