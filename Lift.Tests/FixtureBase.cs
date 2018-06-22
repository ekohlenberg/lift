using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lift.Tests
{

    [TestClass()]
    public abstract class FixtureBase
    {

        private TransactionScope _scope;

        [TestInitialize()]
        public virtual void Initialize()
        {
            _scope = new TransactionScope(TransactionScopeOption.RequiresNew);
        }

        [TestCleanup()]
        public virtual void Cleanup()
        {
            _scope.Dispose();
        }

    }

}