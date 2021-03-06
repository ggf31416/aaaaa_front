﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class JugadorShared
    {
        public Dictionary<int, ConjuntoUnidades> Unidades { get; set; } = new Dictionary<int, ConjuntoUnidades>();
        //public List<Edificio> Edificios { get; set; } = new List<Edificio>();
        public List<TipoEdificio> TipoEdificios { get; set; }
        public List<TipoUnidad> TipoUnidades { get; set; }
        public Dictionary<int, CantidadRecurso> Recursos { get; set; }  // clave Recurso.ID
        public JugadorTecnologia Tecnologias { get; set; }
        public DateTime ultimaActualizacionRecursos;

    }
}

