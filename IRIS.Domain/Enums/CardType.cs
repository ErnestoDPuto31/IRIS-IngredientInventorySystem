using System;
using System.Collections.Generic;
using System.Text;

namespace IRIS.Domain.Enums
{
    public enum CardType
    {
        EmptyItems,
        LowStockItems,
        PendingRequests,
        ApprovedRequests,

        WellStockedItems,
        TotalRequests,
        TotalIngredients,
        TotalTransactions,
        ApprovalRate
    }

}
