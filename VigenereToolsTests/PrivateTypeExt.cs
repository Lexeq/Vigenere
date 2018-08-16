using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace VigenereToolsTests
{
    public static class PrivateTypeExtensions
    {
        public static object InvokeStaticExt(this PrivateType instance, string name, params object[] args)
        {
            try
            {
                return instance.InvokeStatic(name, args);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        public static object InvokeExt(this PrivateObject instance, string name, params object[] args)
        {
            try
            {
                return instance.Invoke(name, args);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
