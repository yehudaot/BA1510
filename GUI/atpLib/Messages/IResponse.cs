namespace atpLib.Messages
{
    public interface IResponse
    {
        //public void parametesFromData();
        //public void fromByteArray(Byte[] arr);
        //public void fromByteArray(Byte[] header, Byte[] data);

        /// <summary>
        /// Each class that inherit this one should be able to return a unique id that will be used when a repsone
        /// is parsed. this id will be used to create a new instance of the inhereted class
        /// </summary>
        /// <returns></returns>
        object getUniqueIdentifier();
    }
}

