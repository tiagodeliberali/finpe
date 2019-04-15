namespace Finpe.CashFlow
{
    public abstract class ClassifiedTransactionLine : TransactionLine
    {
        public ClassifiedTransactionLine(TransactionLineInfo info) : base(info)
        {
            Classify(ClassificationInfo.NotClassified);
        }

        public string Category { get; private set; }
        public string Responsible { get; private set; }
        public Importance Importance { get; private set; }

        public void Classify(ClassificationInfo classification)
        {
            Category = classification.Category;
            Responsible = classification.Responsible;
            Importance = classification.Importance;
        }

        public ClassificationInfo GetClassification()
        {
            return new ClassificationInfo(Category, Responsible, Importance);
        }
    }
}
