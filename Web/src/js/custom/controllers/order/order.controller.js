
angular.module('naut').controller('orderController', ['$scope', '$location', '$modal', '$window', 'localStorageService', 'ngAuthSettings', 'modalService', '$timeout', '$filter', 'productService',

    function ($scope, $location, $modal, $window, localStorageService, ngAuthSettings, modalService, $timeout, $filter, productService) {



        $scope.model = {
            id: '',
            prefix: 'Mr',
            name: '',
            fname: '',
            age: '',
            sex: 'M',
            mrNo: '',
            mobile: '',
            cnic: '',
            refName: '',
            clientId: 0,
            testObj: undefined,
            locationObj: undefined,
            subTotal: 0.00,
            otherCharges: 0.00,
            adjustment: 0.00,
            total: 0.00,
            paid: 0.00,
            balance: 0.00,
            discount: 0.00

        };



        $scope.searchFunc = function () {
            $scope.search = {
                id: '',
                mrNo: ''
            };

            var authData = localStorageService.get('authorizationData');
            if (authData) {

                $scope.search.clientId = authData.clientId;
            }
        };

        $scope.system = {
            alertMsg: '',
            startRowNumber: 1,
            endRowNumber: 10
        };


        $scope.sysModel = {
            CreatedBy: ''
        };




        // real
        $scope.permissionAccessLevel = {
            isAdd: true,
            isEdit: true,
            isDelete: true
        };



        $scope.searchFunc();

        var permLevel = localStorageService.get('permissionData');
        if (permLevel) {
            $scope.permissionAccessLevel = permLevel;
        }


        var authData = localStorageService.get('authorizationData');
        if (authData) {

            $scope.model.clientId = authData.clientId;
            $scope.search.clientId = authData.clientId;
            $scope.sysModel.CreatedBy = authData.userName;


        }

        $scope.calculate = function () {

            $scope.model.total = ((parseFloat($scope.model.subTotal) - parseFloat($scope.model.discount)) + parseFloat($scope.model.otherCharges) + parseFloat($scope.model.adjustment));
            if (parseFloat($scope.model.paid) > parseFloat($scope.model.total) && parseFloat($scope.model.total) > 0) {
                $scope.model.balance = 0;
                $scope.model.paid = $scope.model.total;
            }
            else {
                $scope.model.balance = $scope.model.total - $scope.model.paid;
            }

        };



        $scope.calculate();

        $scope.filter = function () {

            $scope.getDataList(1);

        };

        $scope.testGrid = [];
        $scope.getDataList = function (page) {

            $scope.testList = [];

            $scope.testList.push({ code: 1, name: "LIVER FUNCTION", price: 700, reportDate: '10-07-2017' });
            $scope.testList.push({ code: 2, name: "HEPATITIS BE ANTIGeN", price: 1000, reportDate: '06-07-2017' });
            $scope.testList.push({ code: 3, name: "CBC", price: 600, reportDate: '08-07-2017' });


            $scope.productSearch = {
                id: '0',
                Search: "",
                companyId: 1,
                pageSize: -1
            };


            productService.getProducts($scope.productSearch).then(function (response) {

                $scope.testList = response.data.table0;


            },
          function (err) {
              $.toaster({ title: 'Error', priority: 'danger', message: err.data });
          });














            $scope.locatonList = [];

            $scope.locatonList.push({ code: 1, name: "Karachi" });
            $scope.locatonList.push({ code: 2, name: "Hyderbad" });
            $scope.locatonList.push({ code: 3, name: "Thatha" });
            $scope.locatonList.push({ code: 4, name: "Badin" });
            $scope.locatonList.push({ code: 5, name: "Larkana" });
            $scope.locatonList.push({ code: 6, name: "Sukker" });
            $scope.locatonList.push({ code: 7, name: "Mirpurkhas" });


            //$scope.search.page = page;

            //customerService.getFilterCustomers($scope.search).then(function (response) {

            //    $scope.customerList = response.data[0];
            //    $scope.model.total = parseInt(response.data[1]);
            //    $scope.model.totalPage = parseInt(response.data[1] / $scope.search.pageSize);

            //},
            //  function (err) {
            //      $.toaster({ title: 'Error', priority: 'danger', message: err.data });
            //  });

        }

        $scope.getDataList();

        $scope.cancel = function () {

            window.location.href = '#/app/customers';
        },


        $scope.customerSubmit = function (isValid) {

            if (!isValid) {
                return false;
            }
            //   $scope.model.CreatedBy = $scope.sysModel.CreatedBy;
            //   customerService.customerSubmit($scope.model).then(function (response) {
            //       window.location.href = '#/app/customers';
            //   },
            //function (err) {
            //    $.toaster({ title: 'Error', priority: 'danger', message: err.data });
            //});

        };



        $scope.testChange = function () {

            $scope.filteredItems = $filter('filter')($scope.testGrid, function (item) { return item.code == $scope.model.testObj.code; });
            if ($scope.filteredItems.length == 0) {
                $scope.testGrid.push($scope.model.testObj);
            }

            var total = 0;
            if ($scope.testGrid) {
                angular.forEach($scope.testGrid, function (item) {
                    total += item.saleRate;
                });
            }
            $scope.model.subTotal = total;
            $scope.calculate();
        };

        $scope.remove = function (item) {
            var index = $scope.testGrid.indexOf(item);
            $scope.testGrid.splice(index, 1);
        }

        $scope.reset = function () {

        };



        $scope.pagingAction = function (page) {
            $scope.system.startRowNumber = (page) * 10,
            $scope.system.endRowNumber = ((page) * 10) + 10;

            $scope.getDataList(page);
        };


    }



]);
