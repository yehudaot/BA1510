namespace atpLib.Messages.zUp
{
    public class zUpGetVoltageMessage : zUpMessage
    {
        public static string OUTPUT_MESSAGE = "VOL";
        public zUpGetVoltageMessage()
            : base(OUTPUT_MESSAGE) 
        {
        }

        public override string[] parametersToStringArray()
        {
            return new string[] { "?" };            
        }
    }
}
