'use strict';
angular.module('naut').factory('productService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var productServiceFactory = {};

        var _getProducts = function (iCustomer) {

            return $http.get(serviceBase + 'api/Product/GetProducts?iCustomer=' + iCustomer).then(function (response) {
                return response;
            });

        };

        var _getProductById = function (id) {

            return $http.get(serviceBase + 'api/Product/GetProductById/?id=' + id).then(function (response) {
                return response;
            });

        };



        var _productSubmit = function (model) {

            return $http.post(serviceBase + 'api/Product/ProductSubmit/', model).then(function (response) {
                return response;
            });

        };


        productServiceFactory.getProductById = _getProductById;
        productServiceFactory.getProducts = _getProducts;
        productServiceFactory.productSubmit = _productSubmit;


        return productServiceFactory;
    }]);