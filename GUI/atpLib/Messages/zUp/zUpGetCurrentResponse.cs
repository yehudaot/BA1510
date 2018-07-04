namespace atpLib.Messages.zUp
{
    public class zUpGetCurrentResponse : zUpResponse
    {
        public string current { get; set; }

        public override void parametesFromRawResponse()
        {
            current = rawResponse.Substring(2).Trim();
        }

        public override object getUniqueIdentifier()
        {
            return "AA";
        }
    }
}
