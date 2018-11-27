﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract(IsReference = true)]
    public enum AbstractEnum
    {
        [EnumMember]
        NotAbstract,
        [EnumMember]
        Abstract
    }
}
