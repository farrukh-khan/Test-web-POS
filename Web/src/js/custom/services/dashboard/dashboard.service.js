'use strict';
angular.module('naut').factory('dashboardService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var dashboardServiceFactory = {};

        var _getDashboard = function (model) {

            return $http.post(serviceBase + 'api/Dashboard/GetDashboard', model).then(function (response) {
                return response;
            });

        };


        var _getChartReport = function (model) {

            return $http.post(serviceBase + 'api/Dashboard/GetChartReport', model).then(function (response) {
                return response;
            });

        };

        var _getDistrictDetail = function (model) {

            return $http.post(serviceBase + 'api/Dashboard/GetDistrictDetail', model).then(function (response) {
                return response;
            });

        };




        dashboardServiceFactory.getDistrictDetail = _getDistrictDetail;
        dashboardServiceFactory.getChartReport = _getChartReport;
        dashboardServiceFactory.getDashboard = _getDashboard;

        return dashboardServiceFactory;
    }]);