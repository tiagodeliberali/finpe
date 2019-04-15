namespace Finpe.CashFlow
{
    public class ClassificationInfo
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
    }
}
