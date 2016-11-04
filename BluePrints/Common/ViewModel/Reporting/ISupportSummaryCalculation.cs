using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.Common.ViewModel.Reporting
{
    public interface ISupportSummaryCalculation
    {
        DefaultSummaryCalculation SummarizablePrincipal { get; set; }
    }
}
