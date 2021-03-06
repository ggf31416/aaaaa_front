﻿using DataAccessLayer.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer
{
    public class DALUsuario : IDALUsuario
    {
        const string connectionstring = "mongodb://40.84.2.155";
        private static IMongoClient client = new MongoClient(connectionstring);
        private string nombreJuego;
        private IMongoDatabase database;
        private IMongoCollection<ClienteJuego> collection;
        //private IMongoCollection<JugadorConexion> conexionesJug;
        private IMongoCollection<Shared.Entities.FechaCantidad> collectionFechaCantidad;

        public DALUsuario() { }

        public DALUsuario(string nombreJuego)
        {
            this.nombreJuego = nombreJuego;
            database = client.GetDatabase(nombreJuego);
            collection = database.GetCollection<ClienteJuego>("usuario");
            //conexionesJug = database.GetCollection<JugadorConexion>("usuario");
            collectionFechaCantidad = database.GetCollection<Shared.Entities.FechaCantidad>("fecha_cantidad");
        }
        
        private void inicializarUsuario(ClienteJuego cliente)
        {
            cliente.creacion = DateTime.UtcNow;
            collection.InsertOne(cliente);
            IDALConstruccion iDALConstruccion = new DALConstruccion();
            iDALConstruccion.InicializarConstruccion(cliente.id, this.nombreJuego);
        }

        public bool login(Shared.Entities.ClienteJuego client)
        {
            var query = from usuario in collection.AsQueryable<ClienteJuego>()
                        where usuario.id == client.clienteId
                        select usuario;
            if (query.Count() == 0)
            {
                return false;
            }
            else
            {
                ClienteJuego c = query.First();
                c.token = client.token;
                collection.ReplaceOne(cliente => cliente.id == c.id, c);
                Shared.Entities.FechaCantidad fechaCantidad = new Shared.Entities.FechaCantidad();
                fechaCantidad.cantidad = 1;
                fechaCantidad.fecha = DateTime.UtcNow;
                collectionFechaCantidad.InsertOne(fechaCantidad);
                return true;
            }
        }

        public void register(Shared.Entities.ClienteJuego client)
        {
            ClienteJuego c = new ClienteJuego();
            c.id = client.clienteId;
            c.nombre = client.nombre;
            c.apellido = client.apellido;
            c.username = client.username;
            c.atacable = true;
            inicializarUsuario(c);
        }

        public bool authenticate(Shared.Entities.ClienteJuego client)
        {
            var query = from usuario in collection.AsQueryable<ClienteJuego>()
                        where usuario.id == client.clienteId && usuario.token == client.token
                        select usuario;
            return query.Count() > 0;
        }

        public void logout(Shared.Entities.ClienteJuego client)
        {
            ClienteJuego updateCliente = new ClienteJuego();
            updateCliente.id = client.clienteId;
            updateCliente.nombre = client.nombre;
            updateCliente.apellido = client.apellido;
            updateCliente.username = client.username;
            updateCliente.token = null;
            collection.ReplaceOne(cliente => cliente.id == updateCliente.id, updateCliente);
        }

        //SOCIALES
        public List<Shared.Entities.ClienteJuego> GetJugadoresAtacables(string Tenant, string ClienteId)
        {
            database = client.GetDatabase(Tenant);
            collection = database.GetCollection<ClienteJuego>("usuario");
            // descomentar para resetear atacabilidad:
            //collection.UpdateMany(c => c.atacable == false, Builders<ClienteJuego>.Update.Set(c => c.atacable, true));
            var query = from usuario in collection.AsQueryable<ClienteJuego>()
                        where usuario.atacable == true
                        select usuario;

            List<Shared.Entities.ClienteJuego> res = new List<Shared.Entities.ClienteJuego>();
            foreach(var q in query)
            {
                Shared.Entities.ClienteJuego cj = new Shared.Entities.ClienteJuego();
                cj.nombre = q.nombre;
                cj.apellido = q.apellido;
                cj.token = q.token;
                cj.clienteId = q.id;
                cj.username = q.username;
                res.Add(cj);
            }

            return res;
        }

        //CLANES
        public void CrearClan(string NombreClan, string Tenant, string IdJugador)
        {
            database = client.GetDatabase(Tenant);
            collection = database.GetCollection<ClienteJuego>("usuario");
            IMongoCollection<Clan> clanCollection = database.GetCollection<Clan>("clan");

            Clan newClan = new Clan();
            newClan.AdministradorId = IdJugador;
            newClan.Nombre = NombreClan;
            newClan.IdJugadores = new List<string>();

            newClan.IdJugadores.Add(IdJugador);

            clanCollection.InsertOne(newClan);

            IMongoCollection<ClienteJuego> clientCollection = database.GetCollection<ClienteJuego>("usuario");

            var query = from usuario in collection.AsQueryable<ClienteJuego>()
                        where usuario.id == IdJugador
                        select usuario;

            if(query.Count() > 0)
            {
                ClienteJuego cj = query.First();
                cj.clan = NombreClan;

                clientCollection.ReplaceOne(cliente => cliente.id == cj.id, cj);
            }
        }

        public bool AbandonarClan(string Tenant, string IdJugador)
        {
            database = client.GetDatabase(Tenant);
            collection = database.GetCollection<ClienteJuego>("usuario");
            IMongoCollection<Clan> clanCollection = database.GetCollection<Clan>("clan");

            var query = from usuario in collection.AsQueryable<ClienteJuego>()
                        where usuario.id == IdJugador
                        select usuario;

            if (query.Count() > 0)
            {
                ClienteJuego cj = query.First();
                
                var queryClan = from clan in clanCollection.AsQueryable<Clan>()
                                where clan.Nombre == cj.clan
                                select clan;

                if(queryClan.Count() > 0)
                {
                    Clan c = queryClan.First();
                    c.IdJugadores.Remove(IdJugador);
                    ReplaceOneResult resultClan = clanCollection.ReplaceOne(clan => clan.Nombre == c.Nombre, c);

                    cj.clan = null;
                    ReplaceOneResult resultClient = collection.ReplaceOne(cliente => cliente.id == cj.id, cj);

                    return (resultClan.ModifiedCount == 1) && (resultClient.ModifiedCount == 1);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<Shared.Entities.ClienteJuego> GetJugadoresSinClan(string Tenant, string IdJugador)
        {
            database = client.GetDatabase(Tenant);
            collection = database.GetCollection<ClienteJuego>("usuario");

            var query = from usuario in collection.AsQueryable<ClienteJuego>()
                        where usuario.clan == null
                        select usuario;

            List<Shared.Entities.ClienteJuego> res = new List<Shared.Entities.ClienteJuego>();
            if(query.Count() > 0)
            {
                foreach(var q in query)
                {
                    Shared.Entities.ClienteJuego cj = new Shared.Entities.ClienteJuego();
                    cj.nombre = q.nombre;
                    cj.apellido = q.apellido;
                    cj.token = q.token;
                    cj.clienteId = q.id;
                    cj.username = q.username;
                    res.Add(cj);
                }
            }

            return res;
        }

        public bool AgregarJugadorClan(Shared.Entities.ClienteJuego Jugador, string Tenant, string IdJugador)
        {
            database = client.GetDatabase(Tenant);
            collection = database.GetCollection<ClienteJuego>("usuario");

            IMongoCollection<Clan> clanCollection = database.GetCollection<Clan>("clan");


            string nombreClan = GetClanJugador(Tenant, IdJugador);

            var queryNuevo = from usuario in collection.AsQueryable<ClienteJuego>()
                        where usuario.id == Jugador.clienteId
                        select usuario;

            ClienteJuego nuevoMiembro = queryNuevo.First();

            nuevoMiembro.clan = nombreClan;

            ReplaceOneResult resultNuevoMiembro = collection.ReplaceOne(cliente => cliente.id == nuevoMiembro.id, nuevoMiembro);

            var queryClan = from clan in clanCollection.AsQueryable<Clan>()
                            where clan.Nombre == nombreClan
                            select clan;

            Clan clanAModificar = queryClan.First();

            clanAModificar.IdJugadores.Add(nuevoMiembro.id);

            ReplaceOneResult resultClan = clanCollection.ReplaceOne(clan => clan.Nombre == clanAModificar.Nombre, clanAModificar);

            return (resultClan.ModifiedCount == 1) && (resultNuevoMiembro.ModifiedCount == 1);           
        }

        public List<Shared.Entities.ClienteJuego> GetJugadoresEnElClan(string Tenant, string IdJugador)
        {
            database = client.GetDatabase(Tenant);
            collection = database.GetCollection<ClienteJuego>("usuario");
            IMongoCollection<Clan> clanCollection = database.GetCollection<Clan>("clan");

            List<Shared.Entities.ClienteJuego> res = new List<Shared.Entities.ClienteJuego>();

            var queryClan = from clan in clanCollection.AsQueryable<Clan>()
                            where clan.Nombre == GetClanJugador(Tenant, IdJugador)
                            select clan;

            Clan clanObjeto = queryClan.First();

            var queryUsuarios = from usuario in collection.AsQueryable<ClienteJuego>()
                                where clanObjeto.IdJugadores.Contains(usuario.id)
                                select usuario;

            foreach(var q in queryUsuarios)
            {
                Shared.Entities.ClienteJuego cj = new Shared.Entities.ClienteJuego();
                cj.nombre = q.nombre;
                cj.apellido = q.apellido;
                cj.token = q.token;
                cj.clienteId = q.id;
                cj.username = q.username;
                res.Add(cj);
            }

            return res;
        }
        
        public bool EliminarJugadorClan(Shared.Entities.ClienteJuego Jugador, string Tenant, string IdJugador)
        {
            database = client.GetDatabase(Tenant);
            collection = database.GetCollection<ClienteJuego>("usuario");
            IMongoCollection<Clan> clanCollection = database.GetCollection<Clan>("clan");

            var queryClan = from clan in clanCollection.AsQueryable<Clan>()
                            where clan.Nombre == GetClanJugador(Tenant, IdJugador)
                            select clan;

            Clan clanAModificar = queryClan.First();
            clanAModificar.IdJugadores.Remove(Jugador.clienteId);

            ReplaceOneResult resultClan = clanCollection.ReplaceOne(clan => clan.Nombre == clanAModificar.Nombre, clanAModificar);

            var queryUsuario = from usuario in collection.AsQueryable<ClienteJuego>()
                               where usuario.id == Jugador.clienteId
                               select usuario;

            ClienteJuego cj = new ClienteJuego();
            cj.clan = null;

            ReplaceOneResult resultUsuario = collection.ReplaceOne(cliente => cliente.id == cj.id, cj);

            return (resultClan.ModifiedCount == 1) && (resultUsuario.ModifiedCount == 1);
        }

        public bool SoyAdministrador(string Tenant, string IdJugador)
        {
            database = client.GetDatabase(Tenant);
            collection = database.GetCollection<ClienteJuego>("usuario");
            IMongoCollection<Clan> clanCollection = database.GetCollection<Clan>("clan");

            var queryClan = from clan in clanCollection.AsQueryable<Clan>()
                            where clan.Nombre == GetClanJugador(Tenant, IdJugador)
                            select clan;

            return queryClan.First().AdministradorId == IdJugador;
        }

        public string GetClanJugador(string Tenant, string IdJugador)
        {
            database = client.GetDatabase(Tenant);
            collection = database.GetCollection<ClienteJuego>("usuario");

            var query = from usuario in collection.AsQueryable<ClienteJuego>()
                        where usuario.id == IdJugador
                        select usuario;

            if(query.Count() > 0)
            {
                return query.First().clan;
            }
            else
            {
                return null;
            }
        }

        public int EnviarRecursos(List<Shared.Entities.RecursoAsociado> tributos, string IdJugadorDestino, string Tenant, string IdJugador)
        {
            database = client.GetDatabase(Tenant);
            collection = database.GetCollection<ClienteJuego>("usuario");
            IMongoCollection<Shared.Entities.Juego> collectionJuego = database.GetCollection<Shared.Entities.Juego>("juego_usuario");

            if (GetClanJugador(Tenant, IdJugadorDestino) != GetClanJugador(Tenant, IdJugador))
            {
                return 1;//El Jugador al que le quiere enviar recursos no esta en su clan
            }
            else
            {
                DALJuego dalJuego = new DALJuego();
                Shared.Entities.Juego dataJugador = dalJuego.GetJuegoUsuario(Tenant, IdJugador);

                bool suficientesRecursos = true;
                foreach(var ra in tributos)
                {
                    if(dataJugador.DataJugador.EstadoRecursos[ra.IdRecurso.ToString()].Total < ra.Valor)
                    {
                        suficientesRecursos = false;
                        break;
                    }
                    dataJugador.DataJugador.EstadoRecursos[ra.IdRecurso.ToString()].Total -= ra.Valor;
                }
                if (suficientesRecursos)
                {
                    ReplaceOneResult resultJugador = collectionJuego.ReplaceOne(juego => juego.IdJugador == IdJugador, dataJugador);

                    Shared.Entities.Juego dataJugadorDestino = dalJuego.GetJuegoUsuario(Tenant, IdJugadorDestino);

                    foreach (var ra in tributos)
                    {
                        dataJugadorDestino.DataJugador.EstadoRecursos[ra.IdRecurso.ToString()].Total += ra.Valor;
                    }
                    ReplaceOneResult resultJugadorDestino = collectionJuego.ReplaceOne(juego => juego.IdJugador == IdJugadorDestino, dataJugadorDestino);

                    if ((resultJugador.ModifiedCount == 1) && (resultJugadorDestino.ModifiedCount == 1)) {
                        return 0;//Operacion realizada con exito
                    }
                    else
                    {
                        return 3;//Se produjo un error al guardar en la base de datos
                    }
                }
                else
                {
                    return 2;//No tiene suficientes recursos para enviar
                }
            }
        }

        public bool SetAtacableJugador(string Tenant, string IdJugador, bool atacable)
        {
            database = client.GetDatabase(Tenant);
            collection = database.GetCollection<ClienteJuego>("usuario");
            
            var builder = Builders<ClienteJuego>.Update;
            var res = collection.UpdateOne(c => c.id == IdJugador, builder.Set(c => c.atacable, atacable));
            return res.IsModifiedCountAvailable && res.ModifiedCount == 1;
        }

        public string GetUserName(string Tenant,string IdJugador)
        {
            database = client.GetDatabase(Tenant);
            collection = database.GetCollection<ClienteJuego>("usuario");
            string userName = collection.AsQueryable<ClienteJuego>().Where(c => c.id == IdJugador).Select(c => c.username).FirstOrDefault();
            return userName;
        }
    }
}
