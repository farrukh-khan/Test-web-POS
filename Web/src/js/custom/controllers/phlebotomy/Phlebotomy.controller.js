'use strict';
angular.module('naut').
    controller('phlebotomyController',
            ['$scope', '$location', '$modal', '$window', 'localStorageService', 'ngAuthSettings', 'modalService', '$timeout', 'PhlebotomyService', 'productService', '$filter',

    function ($scope, $location, $modal, $window, localStorageService, ngAuthSettings, modalService, $timeout, PhlebotomyService, productService, $filter) {

        // system setting start
        $scope.model = {
            id: '', code: '', name: '', description: '', sRate: 0, companyId: '', CreatedBy: '', products: []
        };

      

    }
            ]);


