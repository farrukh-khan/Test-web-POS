'use strict';
angular.module('naut').factory('categoryService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var categoryServiceFactory = {};





        var _getcategorys = function (model) {

            return $http.post(serviceBase + 'api/category/Getcategorys', model).then(function (response) {
                return response;
            });

        };

        var _categorySubmit = function (model) {

            return $http.post(serviceBase + 'api/category/categorySubmit/', model).then(function (response) {
                return response;
            });

        };
        var _delete = function (id, clientId) {

            return $http.get(serviceBase + 'api/categoryr/Deletecategoryr/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };

        var _getExcelReport = function (model) {

            return $http.post(serviceBase + 'api/categoryr/ExcelReport', model, { responseType: 'arraybuffer' }).then(function (response) {
                return response;
            });

        };


        categoryServiceFactory.getcategorys = _getcategorys;
        categoryServiceFactory.categorySubmit = _categorySubmit;
        categoryServiceFactory.delete = _delete;
        categoryServiceFactory.getExcelReport = _getExcelReport;

        return categoryServiceFactory;
    }]);