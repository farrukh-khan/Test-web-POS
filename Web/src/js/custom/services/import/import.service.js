'use strict';
angular.module('naut').factory('importService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var importServiceFactory = {};

        var _importFile = function (model) {

            return $http.post(serviceBase + 'api/import/ImportFile=', model).then(function (response) {
                return response;
            });

        };




        var _importSubmit = function (model) {

            return $http.post(serviceBase + 'api/import/importSubmit/', model).then(function (response) {
                return response;
            });

        };



        var _updateImportData = function (type) {

            return $http.get(serviceBase + 'api/import/updateImportData/?type=' + type).then(function (response) {
                return response;
            });

        };

    
        
        importServiceFactory.updateImportData = _updateImportData;
        importServiceFactory.importSubmit = _importSubmit;
        importServiceFactory.importFile = _importFile;

        return importServiceFactory;
    }]);