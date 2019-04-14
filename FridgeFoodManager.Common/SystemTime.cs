using System;

namespace FridgeFoodManager.Common
{
    public class SystemTime
    {
        public static DateTime Now => NowFunc();
            
        public static Func<DateTime> NowFunc = () => DateTime.Now;
    }
}
