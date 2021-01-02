namespace Mh.Enums
{
    public enum FicheTypeFakes
    {
        // Credit
        xSaleReceipt = 0,
        xSaleInvoice = 400,
        xOutgoingPayment = 101,
        xOutgoingMoney = 201,
        xPurchaseReturnReceipt = 3,
        xPurchaseReturnInvoice = 403,
        xDebted = 500,

        // Debt
        xPurchaseReceipt = 1,
        xPurchaseInvoice = 401,
        xIncomingPayment = 100,
        xIncomingMoney = 200,
        xSaleReturnReceipt = 2,
        xSaleReturnInvoice = 402,
        xCredited = 501,

        // Both
        xVirementMoney = 202
    }
}