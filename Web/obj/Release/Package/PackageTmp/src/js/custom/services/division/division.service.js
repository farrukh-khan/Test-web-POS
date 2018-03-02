'use strict';
angular.module('naut').factory('divisionService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var divisionServiceFactory = {};

        var _getDivisions = function (clientId) {

            return $http.get(serviceBase + 'api/Division/GetDivisions?clientId='+ clientId).then(function (response) {
                return response;
            });

        };

        var _getDivisionById = function (id, clientId) {

            return $http.get(serviceBase + 'api/Division/GetDivisionById/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };



        var _divisionSubmit = function (model) {

            return $http.post(serviceBase + 'api/Division/DivisionSubmit/', model).then(function (response) {
                return response;
            });

        };


        var _delete = function (id, clientId) {

            return $http.get(serviceBase + 'api/Division/DeleteDivision/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };
        divisionServiceFactory.getDivisionById = _getDivisionById;
        divisionServiceFactory.getDivisions = _getDivisions;
        divisionServiceFactory.divisionSubmit = _divisionSubmit;
        divisionServiceFactory.delete = _delete;

        return divisionServiceFactory;
    }]);