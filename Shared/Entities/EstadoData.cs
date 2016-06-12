﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Shared.Entities
{
    [DataContract]
    public class EstadoData
    {
        public enum EstadoEnum {A,
            C,
            Puedo,
            NoPuedo
        };

        [DataMember]
        public EstadoEnum Estado { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Cantidad { get; set; }
        
        [DataMember]
        public DateTime Fin { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonIgnore]
        [DataMember]
        public long Faltante
        {
            get {
                return (long)((Fin - DateTime.UtcNow).TotalMilliseconds);
            }
            protected set { }
        }
    }
}
