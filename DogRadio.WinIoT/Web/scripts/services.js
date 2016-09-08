'use strict';

var app = angular.module('radioServices', []);

app.factory('Playlist', ['$http', '$q',
    function($http, $q) {
        function play(index) {
            var deferred = $q.defer();

            $http.post('api/control/play/' + (parseInt(index) + 1))
                .then(function(data) {
                    deferred.resolve(data);
                },
                function(err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function deleteStation(url) {
            var deferred = $q.defer();

            $http.delete('api/playlist', { data: { 'url': url }, headers: { 'Content-Type': 'application/json' } })
                .then(function(data) {
                    deferred.resolve(data);
                },
                function(err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function addStation(url) {
            var deferred = $q.defer();

            $http.post('api/playlist', { 'url': url }, { headers: { 'Content-Type': 'application/json' } })
                .then(function(data) {
                    deferred.resolve(data);
                },
                function(err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function getPlaylist() {
            var deferred = $q.defer();

            $http.get('api/playlist')
                .then(function(data) {
                    deferred.resolve(data);
                },
                function(err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        return {
            play: play,
            delete: deleteStation,
            addStation: addStation,
            getPlaylist: getPlaylist
        };
    }]);



app.factory('Controls', ['$http', '$q',
    function($http, $q) {
        function runCommand(command) {
            var deferred = $q.defer();

            $http.post('api/control/' + command)
                .then(function(data) {
                    deferred.resolve(data);
                },
                function(err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function setVolume(value) {
            var deferred = $q.defer();

            $http.put('api/volume/' + value)
                .then(function(data) {
                    deferred.resolve(data);
                },
                function(err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function getStatus(command) {
            var deferred = $q.defer();

            $http.get('api/status')
                .then(function(data) {
                    deferred.resolve(data);
                },
                function(err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function getPlaylist() {
            var deferred = $q.defer();

            $http.get('api/playlist')
                .then(function(data) {
                    deferred.resolve(data);
                },
                function(err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        return {
            run: runCommand,
            setVolume: setVolume,
            getStatus: getStatus,
            getPlaylist: getPlaylist
        };
    }]);

app.factory('Power', ['$http', '$q',
    function($http, $q) {
        function setState(index, state) {
            var deferred = $q.defer();

            $http.put('api/power/' + index + '/' + state)
                .then(function(data) {
                    deferred.resolve(data);
                },
                function(err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function getState(index) {
            var deferred = $q.defer();

            $http.get('api/power/' + index)
                .then(function(data) {
                    deferred.resolve(data);
                },
                function(err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        return {
            setState,
            getState
        };
    }]);