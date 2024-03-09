namespace BaseCodeAPI.Src.Models
{
    public class InstanceModel
    {
        private static readonly object FLockObject = new();
        private static object FResult;

        public static void SaveInstance(object result)
        {
            lock (FLockObject)
            {
                FResult = result;
            }
        }

        public static object LoadInstance()
        {
            lock (FLockObject)
            {
                return FResult;
            }
        }
    }
}
