
'use strict';
angular.module('naut').factory('caseTransactionService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var caseTransactionServiceFactory = {};

        var _getcaseTransactions = function (clientId) {

            return $http.get(serviceBase + 'api/caseTransaction/GetcaseTransactions?clientId=' + clientId).then(function (response) {
                return response;
            });

        };


        var _getFiltercaseTransactions = function (model) {

            return $http.post(serviceBase + 'api/caseTransaction/FiltercaseTransaction', model).then(function (response) {
                return response;
            });

        };


        var _getPdfReport = function (model) {

            return $http.post(serviceBase + 'api/caseTransaction/PdfReport', model, { responseType: 'arraybuffer' }).then(function (response) {
                return response;
            });

        };


        var _getExcelReport = function (model) {

            return $http.post(serviceBase + 'api/caseTransaction/ExcelReport', model, { responseType: 'arraybuffer' }).then(function (response) {
                return response;
            });

        };





        var _getcaseTransactionById = function (id, clientId) {

            return $http.get(serviceBase + 'api/caseTransaction/GetcaseTransactionById/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };


        var _getcaseTransactionByCaseId = function (accuntNo, clientId) {

            return $http.get(serviceBase + 'api/caseTransaction/GetcaseTransactionByCaseId/?accuntNo=' + accuntNo + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };

        var _customerInfoByCifNo = function (accountNo,cifNo, clientId) {

            return $http.get(serviceBase + 'api/caseTransaction/CustomerInfoByCifNo/?accountNo=' + accountNo + '&cifNo=' + cifNo + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };





        var _caseTransactionSubmit = function (model) {

            return $http.post(serviceBase + 'api/caseTransaction/caseTransactionSubmit/', model).then(function (response) {
                return response;
            });

        };


        var _delete = function (id, clientId) {

            return $http.get(serviceBase + 'api/caseTransaction/DeletecaseTransaction/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };


        var _getCountries = function () {

            return $http.get(serviceBase + 'api/Company/GetCountries').then(function (response) {
                return response;
            });

        };


        
        caseTransactionServiceFactory.customerInfoByCifNo = _customerInfoByCifNo;
        caseTransactionServiceFactory.getcaseTransactionByCaseId = _getcaseTransactionByCaseId;
        caseTransactionServiceFactory.getPdfReport = _getPdfReport;
        caseTransactionServiceFactory.getExcelReport = _getExcelReport;
        caseTransactionServiceFactory.getFiltercaseTransactions = _getFiltercaseTransactions;
        caseTransactionServiceFactory.getCountries = _getCountries;
        caseTransactionServiceFactory.getcaseTransactionById = _getcaseTransactionById;
        caseTransactionServiceFactory.getcaseTransactions = _getcaseTransactions;
        caseTransactionServiceFactory.caseTransactionSubmit = _caseTransactionSubmit;
        caseTransactionServiceFactory.delete = _delete;

        return caseTransactionServiceFactory;
    }]);