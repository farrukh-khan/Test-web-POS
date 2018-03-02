/// <reference path="bank.service.js" />
'use strict';
angular.module('naut').factory('bankService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var bankServiceFactory = {};

        var _getbanks = function (clientId) {

            return $http.get(serviceBase + 'api/bank/Getbanks?clientId=' + clientId).then(function (response) {
                return response;
            });

        };


        var _getFilterBanks = function (model) {

            return $http.post(serviceBase + 'api/bank/FilterBank', model).then(function (response) {
                return response;
            });

        };


        var _getPdfReport = function (model) {

            return $http.post(serviceBase + 'api/bank/PdfReport', model, { responseType: 'arraybuffer' }).then(function (response) {
                return response;
            });

        };


        var _getExcelReport = function (model) {

            return $http.post(serviceBase + 'api/bank/ExcelReport', model, { responseType: 'arraybuffer' }).then(function (response) {
                return response;
            });

        };





        var _getbankById = function (id, clientId) {

            return $http.get(serviceBase + 'api/bank/GetbankById/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };



        var _bankSubmit = function (model) {

            return $http.post(serviceBase + 'api/bank/bankSubmit/', model).then(function (response) {
                return response;
            });

        };


        var _delete = function (id, clientId) {

            return $http.get(serviceBase + 'api/bank/Deletebank/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };


        var _getCountries = function () {

            return $http.get(serviceBase + 'api/Company/GetCountries').then(function (response) {
                return response;
            });

        };


        bankServiceFactory.getPdfReport = _getPdfReport;
        bankServiceFactory.getExcelReport = _getExcelReport;
        bankServiceFactory.getFilterBanks = _getFilterBanks;
        bankServiceFactory.getCountries = _getCountries;
        bankServiceFactory.getbankById = _getbankById;
        bankServiceFactory.getbanks = _getbanks;
        bankServiceFactory.bankSubmit = _bankSubmit;
        bankServiceFactory.delete = _delete;

        return bankServiceFactory;
    }]);