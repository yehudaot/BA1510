namespace atpLib.Devices
{
    public abstract class SingeltonDevice<T> where T : Device
    {
        protected static T device { get; set; }

        public static void CreateInstance(T d)
        {
            device = d;
        }

        public static T getInstance()
        {
            if (device == null)
            {
                throw new DeviceNotInitException();
            }
            return device;
        }
    }
}
