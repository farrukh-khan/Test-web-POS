
'use strict';
angular.module('naut').factory('customerAgentService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var customerAgentServiceFactory = {};

        var _getcustomerAgents = function (model) {

            return $http.post(serviceBase + 'api/customerAgent/GetCustomerAgents',model).then(function (response) {
                return response;
            });

        };


        var _getFiltercustomerAgents = function (model) {

            return $http.post(serviceBase + 'api/customerAgent/FiltercustomerAgent', model).then(function (response) {
                return response;
            });

        };


        var _getPdfReport = function (model) {

            return $http.post(serviceBase + 'api/customerAgent/PdfReport', model, { responseType: 'arraybuffer' }).then(function (response) {
                return response;
            });

        };


        var _getExcelReport = function (model) {

            return $http.post(serviceBase + 'api/customerAgent/ExcelReport', model, { responseType: 'arraybuffer' }).then(function (response) {
                return response;
            });

        };





        var _getcustomerAgentById = function (id, clientId) {

            return $http.get(serviceBase + 'api/customerAgent/GetcustomerAgentById/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };



        var _customerAgentSubmit = function (model) {

            return $http.post(serviceBase + 'api/customerAgent/customerAgentSubmit/', model).then(function (response) {
                return response;
            });

        };


        var _delete = function (id, clientId) {

            return $http.get(serviceBase + 'api/customerAgent/DeletecustomerAgent/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };


        var _getCountries = function () {

            return $http.get(serviceBase + 'api/Company/GetCountries').then(function (response) {
                return response;
            });

        };


        customerAgentServiceFactory.getPdfReport = _getPdfReport;
        customerAgentServiceFactory.getExcelReport = _getExcelReport;
        customerAgentServiceFactory.getFiltercustomerAgents = _getFiltercustomerAgents;
        customerAgentServiceFactory.getCountries = _getCountries;
        customerAgentServiceFactory.getcustomerAgentById = _getcustomerAgentById;
        customerAgentServiceFactory.getcustomerAgents = _getcustomerAgents;
        customerAgentServiceFactory.customerAgentSubmit = _customerAgentSubmit;
        customerAgentServiceFactory.delete = _delete;

        return customerAgentServiceFactory;
    }]);