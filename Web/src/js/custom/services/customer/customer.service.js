'use strict';
angular.module('naut').factory('customerService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var customerServiceFactory = {};

        var _getcustomers = function (clientId) {

            return $http.get(serviceBase + 'api/customer/Getcustomers?clientId='+ clientId).then(function (response) {
                return response;
            });

        };

        var _getFilterCustomers = function (model) {

            return $http.post(serviceBase + 'api/Customer/FilterCustomer', model).then(function (response) {
                return response;
            });

        };


        var _getPdfReport = function (model) {

            return $http.post(serviceBase + 'api/Customer/PdfReport', model, { responseType: 'arraybuffer' }).then(function (response) {
                return response;
            });

        };


        var _getExcelReport = function (model) {

            return $http.post(serviceBase + 'api/Customer/ExcelReport', model, { responseType: 'arraybuffer' }).then(function (response) {
                return response;
            });

        };


        var _getcustomerById = function (id, clientId) {

            return $http.get(serviceBase + 'api/customer/GetcustomerById/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };



        var _customerSubmit = function (model) {

            return $http.post(serviceBase + 'api/customer/customerSubmit/', model).then(function (response) {
                return response;
            });

        };


        var _delete = function (id, clientId) {

            return $http.get(serviceBase + 'api/customer/Deletecustomer/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };

        customerServiceFactory.getPdfReport = _getPdfReport;
        customerServiceFactory.getExcelReport = _getExcelReport;
        customerServiceFactory.getFilterCustomers = _getFilterCustomers;

        customerServiceFactory.getcustomerById = _getcustomerById;
        customerServiceFactory.getcustomers = _getcustomers;
        customerServiceFactory.customerSubmit = _customerSubmit;
        customerServiceFactory.delete = _delete;

        return customerServiceFactory;
    }]);