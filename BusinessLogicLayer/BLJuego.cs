﻿using DataAccessLayer;
using EpPathFinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System.ServiceModel;
using Shared.Entities;

namespace BusinessLogicLayer
{
    public class BLJuego : IBLJuego
    {
        private IDALJuego _dal;

        public BLJuego(IDALJuego dal)
        {
            _dal = dal;
        }

        public void ActualizarJuego(Juego j)
        {
            throw new NotImplementedException();
        }

        public Juego GetAllDataJuego(string tenant)
        {
            return _dal.GetJuego(tenant);
        }
    }
}