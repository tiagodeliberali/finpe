namespace Finpe.CashFlow
{
    public abstract class ClassifiedTransactionLine : TransactionLine
    {
        public virtual string Category { get; private set; }
        public virtual string Responsible { get; private set; }
        public virtual Importance Importance { get; private set; }

        public ClassifiedTransactionLine(TransactionLineInfo info) : base(info)
        {
            Classify(ClassificationInfo.NotClassified);
        }

        public ClassifiedTransactionLine(TransactionLineInfo info, ClassificationInfo classification) : base(info)
        {
            Classify(classification);
        }

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
