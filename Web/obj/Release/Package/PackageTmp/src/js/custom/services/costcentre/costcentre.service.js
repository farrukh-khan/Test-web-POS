/// <reference path="costcentre.service.js" />
'use strict';
angular.module('naut').factory('costcentreService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var costcentreServiceFactory = {};

        var _getCostCentres = function (clientId) {

            return $http.get(serviceBase + 'api/CostCentre/GetCostCentres?clientId=' + clientId).then(function (response) {
                return response;
            });

        };

        var _getCostCentreById = function (id, clientId) {

            return $http.get(serviceBase + 'api/CostCentre/GetCostCentreById/?id=' + id+'&clientId='+ clientId).then(function (response) {
                return response;
            });

        };



        var _costcentreSubmit = function (model) {

            return $http.post(serviceBase + 'api/CostCentre/CostCentreSubmit/', model).then(function (response) {
                return response;
            });

        };


        var _delete = function (id, clientId) {

            return $http.get(serviceBase + 'api/CostCentre/DeleteCostCentre/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };
        costcentreServiceFactory.getCostCentreById = _getCostCentreById;
        costcentreServiceFactory.getCostCentres = _getCostCentres;
        costcentreServiceFactory.costcentreSubmit = _costcentreSubmit;
        costcentreServiceFactory.delete = _delete;

        return costcentreServiceFactory;
    }]);