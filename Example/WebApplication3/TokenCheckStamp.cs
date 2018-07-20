using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Until;
using WebApplication3.Controllers;

namespace WebApplication3
{
    public class TokenCheckStamp: ITokenCheck
    {
        public static DateTime ConvertTimeStampToDateTime(long timeStamp)
        {
            DateTime defaultTime = new DateTime(1970, 1, 1, 0, 0, 0);
            long defaultTick = defaultTime.Ticks;
            long timeTick = defaultTick + timeStamp * 10000;
            //// 东八区 要加上8个小时
            DateTime dt = new DateTime(timeTick).AddHours(8);
            return dt;
        }

        public void Valid(object entity)
        {
            if (entity is InformattorStamp informattorStamp)
            {
                long stamp = informattorStamp.Stamp;
                var time = ConvertTimeStampToDateTime(stamp);
                var now = DateTime.Now;

                if (time < now)
                {
                    var timestamp = now.Subtract(time);

                    if (timestamp.Minutes >= 2)
                        throw new Exception("时间戳和服务器时间不能偏差2分钟");
                }
                else if (time > now)
                {
                    throw new Exception("时间戳不能大于服务器时间");
                }
            }
        }
    }
}
