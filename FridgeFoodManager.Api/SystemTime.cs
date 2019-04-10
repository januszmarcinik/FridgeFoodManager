using System;

namespace FridgeFoodManager.Api
{
    public class SystemTime
    {
        public static DateTime Now => NowFunc();
            
        public static Func<DateTime> NowFunc = () => DateTime.Now;
    }
}
