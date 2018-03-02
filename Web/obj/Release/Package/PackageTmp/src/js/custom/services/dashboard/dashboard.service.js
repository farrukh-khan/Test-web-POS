'use strict';
angular.module('naut').factory('dashboardService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var dashboardServiceFactory = {};

        var _getDashboard = function (iCustomer) {

            return $http.get(serviceBase + 'api/Dashboard/GetDashboard?iCustomer=' + iCustomer).then(function (response) {
                return response;
            });

        };

        var _getActiveCalls = function (iCustomer) {

            return $http.get(serviceBase + 'api/Dashboard/GetActiveCalls?iCustomer=' + iCustomer).then(function (response) {
                return response;
            });

        };

        dashboardServiceFactory.getDashboard = _getDashboard;
        dashboardServiceFactory.getActiveCalls = _getActiveCalls;
        

        return dashboardServiceFactory;
    }]);