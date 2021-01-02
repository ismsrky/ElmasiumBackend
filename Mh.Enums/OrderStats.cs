namespace Mh.Enums
{
    public enum OrderStats
    {
        // IsEndPoint: 0 IsRequireNotes: 0 
        xPending = 0,

        // IsEndPoint: 0 IsRequireNotes: 0 
        xAccepted = 1,

        // IsEndPoint: 1 IsRequireNotes: 1 
        xRejected = 2,

        // IsEndPoint: 1 IsRequireNotes: 0 
        xPulledBack = 3,

        // IsEndPoint: 0 IsRequireNotes: 0 
        xPreparing = 4,

        // IsEndPoint: 0 IsRequireNotes: 1 
        xShippedByCourier = 5,

        // IsEndPoint: 1 IsRequireNotes: 1 
        xShippedByCargo = 6,

        // IsEndPoint: 1 IsRequireNotes: 1 
        xProblemOccurred = 7,

        // IsEndPoint: 1 IsRequireNotes: 0 
        xDelivered = 8,

        // IsEndPoint: 1 IsRequireNotes: 0 
        xDeliveredByHand = 9,

        // IsEndPoint: 0 IsRequireNotes: 0 
        xPendingReturn = 10
    }
}