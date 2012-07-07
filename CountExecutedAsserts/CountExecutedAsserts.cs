using System;
using NUnit.Core.Extensibility;
using NUnit.Core;

namespace CountExecutedAsserts
{
    [NUnitAddin(Description="Count all asserts have been executed in this test session.")]
    public class CountExecutedAsserts: IAddin, EventListener
    {
        private int totalAssertsExecutedInThisRun = 0;

        #region EventListener Members
        public void RunFinished(Exception exception) { }
        public void RunFinished(TestResult result) 
        {
            Console.WriteLine("Different tests executed:" + totalAssertsExecutedInThisRun.ToString());
        }
        public void RunStarted(string name, int testCount) 
        {
            totalAssertsExecutedInThisRun = 0;
        }
        public void SuiteFinished(TestResult result) { }
        public void SuiteStarted(TestName testName) { }
        public void TestFinished(TestResult result) 
        {
            totalAssertsExecutedInThisRun = totalAssertsExecutedInThisRun + result.AssertCount;
        }
        public void TestOutput(TestOutput testOutput) { }
        public void TestStarted(TestName testName) { }
        public void UnhandledException(Exception exception) { }
        #endregion
        
        #region IAddin Members
        public bool Install(IExtensionHost host)
        {
            IExtensionPoint listeners = host.GetExtensionPoint( "EventListeners" );
            if ( listeners == null )
                return false;

            listeners.Install( this );
            return true;
        }
        #endregion
    }
}
