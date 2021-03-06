﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization;
using DataAccessLayer.Entities;
using DataAccessLayer.Exceptions;

namespace DataAccessLayer
{
    class DALJugadorInfo
    {

        const string connectionstring = "mongodb://40.84.2.155";
        private static IMongoClient client = new MongoClient(connectionstring);
        private IMongoDatabase database;
        private IMongoCollection<ConjuntosUnidadMongo> conjuntos;
        private IMongoCollection<JugadorRecursoMongo> recursos;
        private IMongoCollection<JugadorTecnologiasMongo> tecnologias;
        private string idJuego;

        public DALJugadorInfo(string idJuego)
        {
            this.idJuego = idJuego;
            //database = client.GetDatabase(idJuego.ToString());
            setDB(idJuego);
            //collection = database.GetCollection<JugadorInfo>("construccion");
        }

        private void setDB(string idJuego)
        {
            this.idJuego = idJuego;
            database = client.GetDatabase(idJuego.ToString());
        }

        private IMongoCollection<ConjuntosUnidadMongo> coleccionUnidades()
        {
            return database.GetCollection<ConjuntosUnidadMongo>("jugUnidades");
        }

        private IMongoCollection<JugadorRecursoMongo> coleccionRecursos()
        {
            return database.GetCollection<JugadorRecursoMongo>("jugRecursos");
        }

        private IMongoCollection<JugadorTecnologiasMongo> coleccionTecnologias()
        {
            return database.GetCollection<JugadorTecnologiasMongo>("jugTecnologias");
        }

        private void inicializarColeccionTipos(string idJugador)
        {
            conjuntos = coleccionUnidades();
            conjuntos.InsertOneAsync(new ConjuntosUnidadMongo() { IdJugador = idJugador });
        }

        private void inicializarRecursos(string idJugador) {
            recursos = coleccionRecursos();
            recursos.InsertOneAsync(new JugadorRecursoMongo() { IdJugador = idJugador });
        }

        private void inicializarColleccionTecnologias(string idJugador)
        {
            tecnologias = coleccionTecnologias();
            tecnologias.InsertOneAsync(new JugadorTecnologiasMongo() { IdJugador = idJugador });
        }


        public void actualizarColeccionUnidades(ConjuntosUnidadMongo doc)
        {
            conjuntos = coleccionUnidades();
            conjuntos.ReplaceOneAsync(c => c.IdJugador == doc.IdJugador, doc);
        }

        public void cambiarCantidadUnidad(string idJugador,Shared.Entities.ConjuntoUnidades cu)
        {
            conjuntos = coleccionUnidades();

            var filter = Builders<ConjuntosUnidadMongo>.Filter.Where(x => x.IdJugador == idJugador && x.Unidades.Any(u => u.UnidadId == cu.UnidadId));
            var update = Builders<ConjuntosUnidadMongo>.Update.Set(x => x.Unidades[-1].Cantidad, cu.Cantidad);
            var result = conjuntos.UpdateOneAsync(filter, update).Result;
        }

        public void actualizarRecursos(JugadorRecursoMongo doc)
        {
            recursos = coleccionRecursos();
            recursos.ReplaceOneAsync(c => c.IdJugador == doc.IdJugador, doc);
        }

        public void actualizarTecnologias(JugadorTecnologiasMongo doc)
        {
            tecnologias = coleccionTecnologias();
            tecnologias.ReplaceOneAsync(x => x.IdJugador == doc.IdJugador, doc);
        }

        public void inicializarJugadorInfo(string idUsuario,string idJuego)
        {
            setDB(idJuego);
            inicializarColeccionTipos(idUsuario);
            inicializarColleccionTecnologias(idUsuario);
            inicializarRecursos(idUsuario);
        }

        private Task<ConjuntosUnidadMongo> obtenerConjuntoUnidades(string idJugador)
        {
            return coleccionUnidades().AsQueryable().FirstOrDefaultAsync(c => c.IdJugador == idJugador);
        }

        private Task<JugadorRecursoMongo> obtenerRecursos(string idJugador)
        {
            return coleccionRecursos().AsQueryable().FirstOrDefaultAsync(c => c.IdJugador == idJugador);
        }

        private Task<JugadorTecnologiasMongo> obtenerTecnologias(string idJugador)
        {
            return coleccionTecnologias().AsQueryable().FirstOrDefaultAsync(c => c.IdJugador == idJugador);
        }

        public async Task<Shared.Entities.JugadorShared> obtenerInfoJugador(string idJugador)
        {
            var taskUnidades = obtenerConjuntoUnidades(idJugador);
            var taskTecnologias = obtenerTecnologias(idJugador);
            var taskRecursos = obtenerRecursos(idJugador);
            var unidades = await taskUnidades;
            var tecnologias = await taskTecnologias;
            var recursos = await taskRecursos;
            var info = new Shared.Entities.JugadorShared();
            info.Unidades = unidades.Unidades.ToDictionary(u => u.UnidadId);
            info.Recursos = recursos.Recursos;
            info.ultimaActualizacionRecursos = recursos.ultimaActualizacion;
            return info;
        }


    }
}
