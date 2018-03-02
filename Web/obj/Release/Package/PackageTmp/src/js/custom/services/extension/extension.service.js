'use strict';
angular.module('naut').factory('extensionService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var extensionServiceFactory = {};

        var _getextensions = function (model) {

            return $http.post(serviceBase + 'api/Extension/GetExtensions', model).then(function (response) {
                return response;
            });

        };

        var _getextensionById = function (id, iCustomer, clientId) {

            return $http.get(serviceBase + 'api/extension/GetExtensionById/?id=' + id + '&iCustomer=' + iCustomer + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };



        var _extensionSubmit = function (model) {

            return $http.post(serviceBase + 'api/Extension/extensionSubmit/', model).then(function (response) {
                return response;
            });

        };


        var _delete = function (id) {

            return $http.get(serviceBase + 'api/extension/Deleteextension/?id=' + id).then(function (response) {
                return response;
            });

        };
        extensionServiceFactory.getextensionById = _getextensionById;
        extensionServiceFactory.getextensions = _getextensions;
        extensionServiceFactory.extensionSubmit = _extensionSubmit;
        extensionServiceFactory.delete = _delete;

        return extensionServiceFactory;
    }]);