/// <reference path="department.service.js" />
'use strict';
angular.module('naut').factory('departmentService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var departmentServiceFactory = {};

        var _getDepartments = function (clientId) {

            return $http.get(serviceBase + 'api/Department/GetDepartments?clientId='+ clientId).then(function (response) {
                return response;
            });

        };

        var _getDepartmentById = function (id, clientId) {

            return $http.get(serviceBase + 'api/Department/GetDepartmentById/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };



        var _departmentSubmit = function (model) {

            return $http.post(serviceBase + 'api/Department/DepartmentSubmit/', model).then(function (response) {
                return response;
            });

        };


        var _delete = function (id, clientId) {

            return $http.get(serviceBase + 'api/Department/DeleteDepartment/?id=' + id + '&clientId=' + clientId).then(function (response) {
                return response;
            });

        };
        departmentServiceFactory.getDepartmentById = _getDepartmentById;
        departmentServiceFactory.getDepartments = _getDepartments;
        departmentServiceFactory.departmentSubmit = _departmentSubmit;
        departmentServiceFactory.delete = _delete;

        return departmentServiceFactory;
    }]);