using System.Runtime.Serialization;

namespace Tests.EditMode.ObjectFactory
{
    public static class TestableObjectFactory
    {
        public static T Create<T>()
        {
            return (T) FormatterServices.GetUninitializedObject(typeof(T));
        }
    }
}