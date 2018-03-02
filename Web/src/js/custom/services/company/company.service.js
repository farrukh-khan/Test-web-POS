/// <reference path="company.service.js" />
'use strict';
angular.module('naut').factory('companyService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var companyServiceFactory = {};

        var _getCompanys = function () {

            return $http.get(serviceBase + 'api/Company/GetCompanys').then(function (response) {
                return response;
            });

        };


        var _getCustomerData = function () {

            return $http.get(serviceBase + 'api/Company/GetCustomerData').then(function (response) {
                return response;
            });

        };




        var _getCompanyById = function (id) {

            return $http.get(serviceBase + 'api/Company/GetCompanyById/?id=' + id).then(function (response) {
                return response;
            });

        };



        var _companySubmit = function (model) {

            return $http.post(serviceBase + 'api/Company/CompanySubmit/', model).then(function (response) {
                return response;
            });

        };


        var _updateCompanySubmit = function (model) {

            return $http.post(serviceBase + 'api/Company/UpdateCompanySubmit/', model).then(function (response) {
                return response;
            });

        };


        var _rolePermissionSubmit = function (model) {

            return $http.post(serviceBase + 'api/Company/RolePermissionSubmit/', model).then(function (response) {
                return response;
            });

        };



        var _delete = function (id) {

            return $http.get(serviceBase + 'api/Company/DeleteCompany/?id=' + id).then(function (response) {
                return response;
            });

        };

        

        companyServiceFactory.updateCompanySubmit = _updateCompanySubmit;
        companyServiceFactory.rolePermissionSubmit = _rolePermissionSubmit;
        companyServiceFactory.getCustomerData = _getCustomerData;
        companyServiceFactory.getCompanyById = _getCompanyById;
        companyServiceFactory.getCompanys = _getCompanys;
        companyServiceFactory.companySubmit = _companySubmit;
        companyServiceFactory.delete = _delete;

        return companyServiceFactory;
    }]);