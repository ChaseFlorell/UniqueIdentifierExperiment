using System;

namespace UniqueIdentifierExperiment
{    public class UniqueOnAttribute : Attribute
    {
        private const string AnnotationName = "UniqueColumns";
        private readonly string[] _columns;

        public UniqueOnAttribute(string[] columns)
        {
            _columns = columns;
        }

        public static string GetAnnotationName => AnnotationName;

        public string[] GetColumns()
        {
            return _columns;
        }
    }
}