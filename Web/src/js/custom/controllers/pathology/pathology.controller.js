'use strict';
angular.module('naut').
    controller('pathologyController',
            ['$scope', '$location', '$modal', '$window', 'localStorageService', 'ngAuthSettings', 'modalService', '$timeout', 'pathologyService', 'productService', '$filter',

    function ($scope, $location, $modal, $window, localStorageService, ngAuthSettings, modalService, $timeout, pathologyService, productService, $filter) {

        // system setting start
        $scope.model = {
            id: '', code: '', name: '', description: '', sRate: 0, companyId: '', CreatedBy: '', products: []
        };

      

    }
            ]);


