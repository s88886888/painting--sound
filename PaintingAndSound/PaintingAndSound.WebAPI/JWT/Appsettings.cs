using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PaintingAndSound.WebAPI.JWT
{
    public class AppSettings
    {
        public static JwtSetting JwtSetting { get; set; }

        /// <summary>
        /// 初始化jwt配置
        /// </summary>
        /// <param name="configuration"></param>
        public static void Init(IConfiguration configuration)
        {
            JwtSetting = new JwtSetting();
            configuration.Bind("JwtSetting", JwtSetting);
        }
    }
}
