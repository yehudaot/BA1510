namespace atpLib.Messages.zUp
{
    public class zUPSetAddressMessage : zUpMessage
    {
        public static string OUTPUT_MESSAGE = "ADR";
        public zUPSetAddressMessage(int address = 1)
            : base(OUTPUT_MESSAGE) 
        {
            this.address = address;
        }

        public int address { get; set; }

        public override string[] parametersToStringArray()
        {
            return new string[] { address.ToString("00") };            
        }
    }
}
