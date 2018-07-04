namespace atpLib.Messages.zUp
{
    public class zUpGetModelResponse : zUpResponse
    {
        public string model { get; set; }

        public override void parametesFromRawResponse()
        {
            model = rawResponse.Trim();
        }

        public override object getUniqueIdentifier()
        {
            return "Ne";
        }
    }
}
