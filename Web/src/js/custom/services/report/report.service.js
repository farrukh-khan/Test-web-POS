'use strict';
angular.module('naut').factory('reportService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var ServiceFactory = {};

        var _getReportCatalogues = function () {

            return $http.get(serviceBase + 'api/Report/GetReportCatalogues/').then(function (response) {
                return response;
            });

        };


        var _getReportPdf = function (model) {

            return $http.post(serviceBase + 'api/Report/GetReportPdf/', model, { responseType: 'arraybuffer' }).then(function (response) {
                return response;
            });

        };



        var _getReportExcel = function (model) {

            return $http.post(serviceBase + 'api/Report/GetReportExcel/', model, { responseType: 'arraybuffer' }).then(function (response) {
                return response;
            });

        };







        var _getReport = function (model) {

            return $http.post(serviceBase + 'api/Report/GetReport/', model).then(function (response) {
                return response;
            });

        };


        var _getReportInfo = function (id, clientId) {

            return $http.get(serviceBase + 'api/Report/GetReportInfo/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });


        };





        var _getReportFilters = function (iCustomer, clientId) {

            return $http.get(serviceBase + 'api/Report/GetFiltersData?iCustomer=' + iCustomer + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };

        
        ServiceFactory.getReportExcel = _getReportExcel;
        ServiceFactory.getReportPdf = _getReportPdf;
        ServiceFactory.getReportInfo = _getReportInfo;
        ServiceFactory.getReportFilters = _getReportFilters;
        ServiceFactory.getReportCatalogues = _getReportCatalogues;
        ServiceFactory.getReport = _getReport;
        return ServiceFactory;
    }]);