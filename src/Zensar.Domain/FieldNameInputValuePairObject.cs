﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Domain
{
    public class FieldNameInputValuePairObject
    {
        public string CustNo { get; set; }
        public IList<FieldNameInputValuePair> FieldNameInputValuePairList { get; set; }
    }
}
