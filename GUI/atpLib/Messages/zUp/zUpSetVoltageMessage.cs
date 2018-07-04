namespace atpLib.Messages.zUp
{
    public class zUPSetVoltagetMessage : zUpMessage
    {
        public static string OUTPUT_MESSAGE = "VOL";
        public zUPSetVoltagetMessage(float voltage) : base(OUTPUT_MESSAGE) 
        {
            this.voltage = voltage;
        }

        public float voltage { get; set; }

        public override string[] parametersToStringArray()
        {
            return new string[] { voltage.ToString("00.00") };            
        }
    }
}
