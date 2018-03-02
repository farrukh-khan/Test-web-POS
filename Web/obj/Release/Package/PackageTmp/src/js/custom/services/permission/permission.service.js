'use strict';
angular.module('naut').factory('permissionService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var permissionServiceFactory = {};

        var _getPermissions = function (roleId) {

            return $http.get(serviceBase + 'api/Permission/GetPermissions?roleId=' + roleId).then(function (response) {
                return response;
            });

        };

        var _getPermissionByRoleId = function (id) {

            return $http.get(serviceBase + 'api/Permission/GetPermissionByRoleId/?id=' + id).then(function (response) {
                return response;
            });

        };



        var _permissionSubmit = function (model) {

            return $http.post(serviceBase + 'api/Permission/PermissionSubmit/', model).then(function (response) {
                return response;
            });

        };


        var _delete = function (id) {

            return $http.get(serviceBase + 'api/Permission/DeletePermission/?id=' + id).then(function (response) {
                return response;
            });

        };
        permissionServiceFactory.getPermissionByRoleId = _getPermissionByRoleId;
        permissionServiceFactory.getPermissions = _getPermissions;
        permissionServiceFactory.permissionSubmit = _permissionSubmit;
        permissionServiceFactory.delete = _delete;

        return permissionServiceFactory;
    }]);