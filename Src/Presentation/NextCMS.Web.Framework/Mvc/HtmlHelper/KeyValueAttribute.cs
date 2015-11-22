using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextCMS.Web.Framework
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class KeyValueAttribute : Attribute
    {
        private string _Text = "Text";
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        private string _Value = "Value";
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private string _Disable = "Disable";
        public string Disable
        {
            get { return _Disable; }
            set { _Disable = value; }
        }

        public string DisplayProperty { get; set; }
    }
}