namespace NextCMS.Core.Domain.Configuration
{
    /// <summary>
    /// 设置表
    /// </summary>
    public partial class Setting : BaseEntity
    {
        public Setting() { }
        
        public Setting(string name, string value) 
        {
            this.Name = name;
            this.Value = value;
        }
        
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
