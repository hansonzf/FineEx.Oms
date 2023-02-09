using Oms.Application.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Oms.HttpApi.Controllers
{
    public class BaseController : AbpController
    {
        public CurrentUser CurrentUser
        {
            get
            {
                var userId = "";
                var userNo = "";
                var companyId = "";
                var userName = "";
                var phone = "";
                var tenantId = "";
                var tokenValues = HttpContext.Request.Headers["Authorization"];
                if (tokenValues.Any())
                {
                    var token = tokenValues[0]?.Replace("Bearer ", "");
                    var handler = new JwtSecurityTokenHandler();
                    var payload = handler.ReadJwtToken(token).Payload;
                    var claims = payload.Claims;
                    userId = claims.FirstOrDefault(_ => _.Type == "sub")?.Value;
                    userNo = claims.FirstOrDefault(_ => _.Type == "userno")?.Value;
                    companyId = claims.FirstOrDefault(_ => _.Type == "companyid")?.Value ?? "";
                    userName = claims.FirstOrDefault(_ => _.Type == "name")?.Value ?? "";
                    phone = claims.FirstOrDefault(_ => _.Type == "phone_number")?.Value ?? "";
                }
                var temp = new CurrentUser
                {
                    UserId =  userId.ToInt(0),
                    UserNo = userNo,
                    Phone = phone,
                    UserName = userName,
                    CompanyId = companyId.ToInt(0),
                    TenantId = tenantId
                };
                return temp;
            }
        }
        /// <summary>
        /// 当前登录公司id
        /// </summary>
        public long CompanyId
        {
            get
            {
                return this.CurrentUser.CompanyId;
            }
        }
        /// <summary>
        /// 当前登录用户工号
        /// </summary>
        public long UserNo
        {
            get
            {
                return this.CurrentUser.CompanyId;
            }
        }

        /// <summary>
        /// 当前登录用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return this.CurrentUser.UserName;
            }
        }
    }

    public static class StringExtension
    {
        /// <summary>
        /// 字符串转short
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static short ToShort(this string str, short defValue = 0)
        {
            if (short.TryParse(str, out short ret))
            {
                return ret;
            }

            return defValue;
        }
        /// <summary>
        /// 字符串转int
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static int ToInt(this string str, int defValue = 0)
        {
            if (int.TryParse(str, out int ret))
            {
                return ret;
            }

            return defValue;
        }
        /// <summary>
        /// 字符串转int64
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static long ToInt64(this string str, long defValue = 0)
        {
            if (long.TryParse(str, out long ret))
            {
                return ret;
            }

            return defValue;
        }
        /// <summary>
        /// 字符串转decimal
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, decimal defValue = 0)
        {
            if (decimal.TryParse(str, out decimal ret))
            {
                return ret;
            }

            return defValue;
        }

        /// <summary>
        /// 字符串转bool
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static bool ToBool(this string str, bool defValue = false)
        {
            if (bool.TryParse(str, out bool ret))
            {
                return ret;
            }

            return defValue;
        }
        /// <summary>
        /// 强转为DateTime类型(无异常返回)
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object value)
        {
            try
            {
                if (((value != null) && (value != DBNull.Value)) && !string.IsNullOrEmpty(value.ToString()) && DateTime.TryParse(value.ToString(), out DateTime num))
                {
                    return num;
                }
            }
            catch
            {
                return DateTime.Now;
            }

            return DateTime.Now;
        }
    }
}
