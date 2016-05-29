﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Shared.Entities
{
    [DataContract]
    public class Costo
    {
        [DataMember]
        public int IdEntidad { get; set; }
        [DataMember]
        public int IdRecurso { get; set; }
        [DataMember]
        public int Valor { get; set; }
    }
}
