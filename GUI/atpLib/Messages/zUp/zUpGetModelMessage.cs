namespace atpLib.Messages.zUp
{
    public class zUPGetModelMessage : zUpMessage
    {
        public static string OUTPUT_MESSAGE = "MDL";
        public zUPGetModelMessage()
            : base(OUTPUT_MESSAGE) 
        {
        }

        public override string[] parametersToStringArray()
        {
            return new string[] { "?" };            
        }
    }
}
