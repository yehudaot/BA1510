namespace atpLib.Messages.zUp
{
    public class zUPOutputMessage : zUpMessage
    {
        public static string OUTPUT_MESSAGE = "OUT";
        public zUPOutputMessage(bool enable) : base(OUTPUT_MESSAGE) 
        {
            this.enable = enable;
        }

        public bool enable { get; set; }

        public override string[] parametersToStringArray()
        {
            return new string[] { enable ? "1" : "0" };            
        }
    }
}
