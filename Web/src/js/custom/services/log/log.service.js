'use strict';
angular.module('naut').factory('logService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var logServiceFactory = {};

        var _getlogs = function (model) {

            return $http.post(serviceBase + 'api/log/FilterLog', model).then(function (response) {
                return response;
            });

        };

        
        logServiceFactory.getlogs = _getlogs;
        
        return logServiceFactory;
    }]);