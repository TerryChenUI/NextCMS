﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextCMS.Core.Domain.Catalog
{
    public partial class Vote : BaseEntity
    {
        public string IPAddress { get; set; }
        public DateTime CreatedOnDate { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
