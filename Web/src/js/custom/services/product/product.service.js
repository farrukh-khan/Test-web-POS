'use strict';
angular.module('naut').factory('productService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var productServiceFactory = {};





        var _getProducts = function (model) {

            return $http.post(serviceBase + 'api/Product/GetProducts', model).then(function (response) {
                return response;
            });

        };

        var _productSubmit = function (model) {

            return $http.post(serviceBase + 'api/Product/ProductSubmit/', model).then(function (response) {
                return response;
            });

        };
        var _delete = function (id, clientId) {

            return $http.get(serviceBase + 'api/Productr/DeleteProductr/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };

        var _getExcelReport = function (model) {

            return $http.post(serviceBase + 'api/Productr/ExcelReport', model, { responseType: 'arraybuffer' }).then(function (response) {
                return response;
            });

        };


        productServiceFactory.getProducts = _getProducts;
        productServiceFactory.productSubmit = _productSubmit;
        productServiceFactory.delete = _delete;
        productServiceFactory.getExcelReport = _getExcelReport;

        return productServiceFactory;
    }]);