namespace atpLib.Messages.zUp
{
    public class zUPGetCurrentMessage : zUpMessage
    {
        public static string OUTPUT_MESSAGE = "CUR";
        public zUPGetCurrentMessage()
            : base(OUTPUT_MESSAGE) 
        {
        }

        public override string[] parametersToStringArray()
        {
            return new string[] { "?" };            
        }
    }
}
