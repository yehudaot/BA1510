namespace atpLib.Messages.zUp
{
    public class zUpGetVoltageResponse : zUpResponse
    {
        public string voltage { get; set; }

        public override void parametesFromRawResponse()
        {
            voltage = rawResponse.Substring(2).Trim();
        }

        public override object getUniqueIdentifier()
        {
            return "AV";
        }
    }
}
