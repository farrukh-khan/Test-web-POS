'use strict';
angular.module('naut').controller('importController', ['$scope', '$location', '$modal', '$window', 'importService', 'localStorageService', 'ngAuthSettings', 'modalService','$upload',

    function ($scope, $location, $modal, $window, importService, localStorageService, ngAuthSettings, modalService, $upload) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        $scope.model = {
            file: '',
            clientId: ''

        };
        $scope.ajaxProgress = false;
        $scope.isShowRecord = false;
        

        var permLevel = localStorageService.get('permissionData');
        if (permLevel) {


            $scope.permissionAccessLevel = permLevel;
        }

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            $scope.model.clientId = authData.clientId;
            //$scope.sysModel.CreatedBy = authData.userName;
        }


        $scope.updateImportData = function (type) {

            
            importService.updateImportData(type).then(function (response) {

                
                //$scope.countryList = response.data;

            },
                   function (err) {
                       $.toaster({ title: 'Error', priority: 'danger', message: err.data });
                   });
            

        };





        $scope.upload = [];
        $scope.fileUploadObj = { testString1: "Test string 1", testString2: "Test string 2" };



        $scope.onFileSelect = function ($files) {
            //$files: an array of files selected, each file has name, size, and type.
            $scope.ajaxProgress = true;
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                (function (index) {
                    $scope.upload[index] = $upload.upload({
                        url: serviceBase + 'api/import/Upload/', // webapi url
                        method: "POST",
                        data: { fileUploadObj: $scope.fileUploadObj },
                        file: $file
                    }).progress(function (evt) {
                        // get upload percentage
                        //console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));

                        $scope.fileProgress = "Uploaded: " + parseInt(100.0 * evt.loaded / evt.total) + "%";
                    }).success(function (data, status, headers, config) {
                        // file is uploaded successfully

                        $scope.ajaxProgress = false;
                        $scope.isShowRecord = true;
                        $scope.dataList = data;
                        $.toaster({ title: 'Info', priority: 'success', message: "File Imported Successfully." });

                        console.log(data);
                    }).error(function (data, status, headers, config) {
                        $scope.ajaxProgress = false;
                        console.log(data);

                //        $.toaster({ title: 'Error', priority: 'danger', message: data });
                    });
                })(i);
            }
        };

        $scope.abortUpload = function (index) {
            $scope.upload[index].abort();
        };


        $scope.getKeys = function (obj) {



            var key;
            var keys = [];
            for (key in obj) {
                if (key === "$$hashKey") break; //angular adds new keys to the object
                if (obj.hasOwnProperty(key)) keys.push(key);
            }



            return keys;
        };
        
    }
]);
