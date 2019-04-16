using System.Collections.Generic;
using Finpe.CashFlow;

namespace Finpe.Visualization
{
    public interface IViewerPipeline
    {
        void ProcessLines(List<TransactionLine> statements);
    }
}
