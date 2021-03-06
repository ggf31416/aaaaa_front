﻿using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IBLConstruccion
    {
        bool ConstruirEdificio(CEInputData ceid, string Tenant, string NombreJugador);
        int EntrenarUnidad(EUInputData euid, string Tenant, string NombreJugador);
    }
}
