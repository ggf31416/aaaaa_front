﻿(function () {
    'use strict';
    angular.module('juego').service('juegoService', ["$http", "$q", "$rootScope", juegoService]);

    function juegoService($http, $q, $rootScope) {
        //var jugador = "jugador1";

        function postAccion(json) {
            return $http({
                method: 'POST',
                dataType: 'text',
                url: '/' + $rootScope.nombreJuego + "/Tablero/Accion",
                data: { data: JSON.stringify(json) }
            });
        }

        this.crearEdificioEnTablero = function (id, input_x, input_y,jugador) {
            //return $http.post("Tablero/JugarUnidad", JSON.stringify({ "Id": id, "PosX": input_x, "PosY": input_y }));
            return postAccion({ "A": "AddEd", "J": jugador, "Id": id, "PosX": input_x, "PosY": input_y });
        }

        this.GetEstadoBatalla = function(){
            return $http.get('/' + $rootScope.nombreJuego + "/Tablero/obtenerEstadoBatalla")
            //return postAccion({"A": "GetEstadoBatalla","J" :""});
        }



        this.posicionarUnidad = function (idTipo, idUnidad,input_x, input_y,jugador){
            return postAccion({ "A": "AddUnidad", "J": jugador, "Id": idTipo, "IdUn": idUnidad, "PosX": input_x, "PosY": input_y });
        }

        this.construirUnidad = function (idTipo,jugador){
            return postAccion({ "A": "BU", "J": jugador, "Id": idTipo});
        }

        this.moverUnidad = function (id, input_x, input_y,jugador){
            //return $http.post('/' + $rootScope.nombreJuego + "/Tablero/Accion", JSON.stringify({ "A": "MoveUnidad", "J": jugador, "Id": id, "PosX": input_x, "PosY": input_y }));
            return postAccion({ "A": "MoveUnidad", "J": jugador, "Id": id, "PosX": input_x, "PosY": input_y });
        }

        this.registrarJugador = function (jugador,juego) {
            return $http({
                method: 'POST',
                dataType: 'text',
                url: '/' + $rootScope.nombreJuego + "/Tablero/RegistrarJugador",
                data: { "jugador": jugador }
            });
        }


        this.getListaEnemigos = function (jugador,juego) {
            var promise = $http.get("/Tablero/GetListaDeJugadoresAtacables",{params: {"jugador": jugador}}).then(
                function (data) {
                    if (data.data.success == false) {
                        throw new Error(data.data.responseText);
                        console.log("Error al cargar enemigos atacables: " + data.data.responseText + " msg" + data.data.msg);
                    }
                    return data.data.ret;
                }).catch(
                    function (err) {
                        console.log("Error al cargar enemigos atacables: " + err);
                });
             return promise;
        }

        this.iniciarAtaque = function(ataqueJson){
            return $http.post('/' + $rootScope.nombreJuego + "/Tablero/iniciarAtaque", JSON.stringify(ataqueJson));


        }

        
    }

})();