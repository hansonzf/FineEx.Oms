namespace Oms.Application.Contracts
{
    public class Region
    {
        /// <summary>
        /// 地区编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 地区名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 下级地区
        /// </summary>
        public List<Region> Children { get; set; }


    }
}
