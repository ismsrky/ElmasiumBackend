using System.ComponentModel;

namespace Mh.Enums
{
    public enum ApiConnCloseReasons
    {
        [Description("Normal")]
        Normal = 1000,

        [Description("Away")]
        Away = 1001,

        [Description("ProtocolError")]
        ProtocolError = 1002,

        [Description("UnsupportedData")]
        UnsupportedData = 1003,

        [Description("Undefined")]
        Undefined = 1004,

        [Description("NoStatus")]
        NoStatus = 1005,

        [Description("Abnormal")]
        Abnormal = 1006,

        [Description("InvalidData")]
        InvalidData = 1007,

        [Description("PolicyViolation")]
        PolicyViolation = 1008,

        [Description("TooBig")]
        TooBig = 1009,

        [Description("MandatoryExtension")]
        MandatoryExtension = 1010,

        [Description("ServerError")]
        ServerError = 1011,

        [Description("TlsHandshakeFailure")]
        TlsHandshakeFailure = 1015,
    }
}