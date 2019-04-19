using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Finpe.CashFlow
{
    public class ClassificationInfo : ValueObject
    {
        public string Category { get; private set; }
        public string Responsible { get; private set; }
        public Importance Importance { get; private set; }
        
        public ClassificationInfo(string category, string responsible, Importance importance)
        {
            Category = category;
            Responsible = responsible;
            Importance = importance;
        }

        public static ClassificationInfo NotClassified = new ClassificationInfo("", "", Importance.NotDefined);
        public static string ResponsibleAll = "Todos";

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Category;
            yield return Responsible;
            yield return Importance;
        }
    }
}
