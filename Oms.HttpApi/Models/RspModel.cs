using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class RspModel<T>
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public bool Flag { get; set; }
        /// <summary>
        /// 返回编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 返回信息（界面提示）
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 详细数据
        /// </summary>
        public T Data { get; set; }



        /// <summary>
        /// 成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static RspModel<T> Success<T>(T data, string message = "")
        {
            return new RspModel<T>() { Flag = true, Code = "200", Message = message, Data = data };
        }



        /// <summary>
        /// 失败
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static RspModel<T> Fail<T>(string message = "操作失败")
        {
            return new RspModel<T>() { Flag = false, Code = "500", Message = message };
        }

        /// <summary>
        /// 获取页面返回值
        /// </summary>
        /// <param name="total"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static RspPaging<T> Pageing<T>(T data, int total)
        {
            return new RspPaging<T> { Flag = true, Code = "200", Total = total, Data = data };
        }
    }

    /// <summary>
    /// 返回值对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RspModel : RspModel<object>
    {
        /// <summary>
        /// 失败
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static RspModel Fail(string message = "操作失败")
        {
            return new RspModel() { Flag = false, Code = "500", Message = message };
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static RspModel Success(object data = null, string message = "")
        {
            return new RspModel() { Flag = true, Code = "200", Message = message, Data = data };
        }
    }

    /// <summary>
    /// 分页返回对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RspPaging<T> : RspModel<T>
    {
        /// <summary>
        /// 数据总行数
        /// </summary>
        public long Total { get; set; }
    }
}
