'use strict';
angular.module('naut').factory('systemSettingService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var systemSettingServiceFactory = {};

        var _getSystemSettings = function (iCustomer) {

            return $http.get(serviceBase + 'api/SystemSetting/GetSystemSettings?iCustomer=' + iCustomer).then(function (response) {
                return response;
            });

        };

        var _getSystemSettingById = function (id) {

            return $http.get(serviceBase + 'api/SystemSetting/GetSystemSettingById/?id=' + id).then(function (response) {
                return response;
            });

        };



        var _systemSettingSubmit = function (model) {

            return $http.post(serviceBase + 'api/SystemSetting/SystemSettingSubmit/', model).then(function (response) {
                return response;
            });

        };


        var _delete = function (id) {

            return $http.get(serviceBase + 'api/SystemSetting/DeleteSystemSetting/?id=' + id).then(function (response) {
                return response;
            });

        };
        systemSettingServiceFactory.getSystemSettingById = _getSystemSettingById;
        systemSettingServiceFactory.getSystemSettings = _getSystemSettings;
        systemSettingServiceFactory.systemSettingSubmit = _systemSettingSubmit;
        systemSettingServiceFactory.delete = _delete;

        return systemSettingServiceFactory;
    }]);