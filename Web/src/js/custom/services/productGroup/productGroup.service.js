'use strict';
angular.module('naut').factory('productGroupService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var productGroupServiceFactory = {};



        var _getProductGroups = function (model) {

            return $http.post(serviceBase + 'api/ProductGroup/GetProductGroups', model).then(function (response) {
                return response;
            });

        };

        var _productGroupSubmit = function (model) {

            return $http.post(serviceBase + 'api/ProductGroup/ProductGroupSubmit/', model).then(function (response) {
                return response;
            });

        };
        var _delete = function (id, clientId) {

            return $http.get(serviceBase + 'api/ProductGroupr/DeleteProductGroupr/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };

        var _getExcelReport = function (model) {

            return $http.post(serviceBase + 'api/ProductGroupr/ExcelReport', model, { responseType: 'arraybuffer' }).then(function (response) {
                return response;
            });

        };


        productGroupServiceFactory.getProductGroups = _getProductGroups;
        productGroupServiceFactory.productGroupSubmit = _productGroupSubmit;
        productGroupServiceFactory.delete = _delete;
        productGroupServiceFactory.getExcelReport = _getExcelReport;

        return productGroupServiceFactory;
    }]);