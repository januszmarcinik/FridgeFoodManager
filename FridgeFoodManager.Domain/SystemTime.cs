using System;

namespace FridgeFoodManager.Domain
{
    public class SystemTime
    {
        public static DateTime Now => NowFunc();
            
        public static Func<DateTime> NowFunc = () => DateTime.Now;
    }
}
