namespace atpLib.Messages.zUp
{
    public class zUPSetCurrentLimitMessage : zUpMessage
    {
        public static string OUTPUT_MESSAGE = "CUR";
        public zUPSetCurrentLimitMessage(float limit)
            : base(OUTPUT_MESSAGE)
        {
            this.currentLimit = limit;
        }

        public float currentLimit { get; set; }

        public override string[] parametersToStringArray()
        {
            return new string[] { currentLimit.ToString("00.00") };
        }
    }
}
