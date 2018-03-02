
'use strict';
angular.module('naut').controller('companyController', ['$scope', '$location', '$modal', '$window', 'companyService', 'localStorageService', 'ngAuthSettings', 'modalService', 'roleService', 'authService', 'permissionService', '$route',

    function ($scope, $location, $modal, $window, companyService, localStorageService, ngAuthSettings, modalService, roleService, authService, permissionService, $route) {


        $scope.model = {
            id: 0,
            iCustomer: '',
            countryId: '',
            name: "",
            description: "",
            address1: "",
            address2: "",
            Phone1: "",
            Phone2: "",
            countryName: "",
            isActive: true,
            roleName: 'administrator',
            Permission: [],
            User: {

                id: 0,
                firstName: "",
                lastName: "",
                isActive: true,
                email: "",
                password: "",
                confirmPassword: "",
            },

        };

        $scope.cModel = {
            id: 0,
            iCustomer: 27994,
            countryId: 0,
            name: "",
            description: "",
            address1: "",
            address2: "",
            Phone1: "",
            Phone2: "",
            isActive: true,
        };
        $scope.selectedAll = false;
        if ($location.path() == "/app/companies") {
            companyService.getCompanys().then(function (response) {
                $scope.companyList = response.data.table0;


            },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });
        }


        if ($location.path() == "/app/company") {

            companyService.getCustomerData().then(function (response) {

                $scope.cusList = response.data[0];
                $scope.countryList = response.data[1];
                $scope.permissionList = response.data[2];

                


            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });
        }

        // get edit data
        if ($location.path() == "/app/editcompany") {





            var id = $location.search().id;

            if (id != 0) {

                companyService.getCompanyById(id).then(function (response) {

                    $scope.customerList = response.data.table3;

                    $scope.countryList = response.data.table4;

                    $scope.cModel = response.data.table0[0];

                    $scope.rolesList = response.data.table1;

                    $scope.userList = response.data.table2;


                },
             function (err) {

                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });
            }

            // login user



            $scope.loginUser = function (email, password) {

                $scope.loginData = {
                    userName: email,
                    password: password,
                    rememberMe: false,
                    useRefreshTokens: false,
                    isLogOffUser: false,
                    message: ""
                };


                authService.superAdminlogin($scope.loginData).then(function (response) {

                    $location.path('/app/dashboard');
                    $location.search('id', null)
                    $window.location.reload();
                },
             function (err) {

                 $.toaster({ title: 'Error', priority: 'danger', message: err.error_description });

             });



            }


            // delete role

            $scope.roleDelete = function (id) {



                var modalOptions = {
                    actionButtonText: 'OK',
                    headerText: 'Confirm',
                    bodyText: "Are you sure, you want to delete?"
                };

                modalService.showModal({}, modalOptions).then(function (result) {
                    roleService.delete(id, $location.search().id).then(function (response) {
                        $scope.rolesList = response.data;


                    },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });
                });


            };


            // delete user
            $scope.deleteUser = function (id) {



                var modalOptions = {
                    actionButtonText: 'OK',
                    headerText: 'Confirm',
                    bodyText: "Are you sure, you want to delete?"
                };

                modalService.showModal({}, modalOptions).then(function (result) {
                    authService.deleteUser(id, $location.search().id).then(function (response) {
                        $scope.userList = response.data;
                    },
          function (err) {
              $.toaster({ title: 'Error', priority: 'danger', message: err.data });
          });
                });



            };
        }



        $scope.companyEditSubmit = function (isValid) {


            if (!isValid) {
                return false;
            }

            companyService.updateCompanySubmit($scope.cModel).then(function (response) {

                window.location.href = '#/app/companies';

            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });

        };


        $scope.cancel = function () {

            window.location.href = '#/admin/companies';
        },


        $scope.companySubmit = function (isValid) {

            //if (!isValid) {
            //    return false;
            //}

            var temp = $scope.permissionList;

            $scope.model.permission = temp;

            companyService.companySubmit($scope.model).then(function (response) {



                window.location.href = '#/app/companies';

            },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });

        };




        // company role
        if ($location.path() == "/app/companyrole") {


            var id = $location.search().id;
            var companyId = $location.search().companyid;


            $scope.roleModel = {
                id: "0",
                roleName: "",
                clientId: companyId
            };
            var roleid = 0;

            if (id) {
                if (id == 0) {
                    roleid = 6;
                }
                else {
                    roleid = id;

                    $scope.roleModel.id = id;
                }

            }

            permissionService.getPermissions(roleid).then(function (response) {

                $scope.roleModel.permissionList = response.data[1];

                if (roleid != 6) {
                    $scope.roleModel.roleName = $scope.roleModel.permissionList[0].role;
                    $scope.roleModel.id = $scope.roleModel.permissionList[0].roleId;

                }


            },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });




            $scope.cancelRole = function () {

                window.location.href = '#/app/editcompany?id=' + companyId;
            },



            // submit company Role


            $scope.roleSubmit = function (isValid) {

                if (!isValid) {
                    return false;

                }

                var temp = $scope.roleModel.permissionList;

                $scope.roleModel.permissionList = temp;



                companyService.rolePermissionSubmit($scope.roleModel).then(function (response) {

                    window.location.href = '#/app/editcompany?id=' + companyId;

                },
             function (err) {
                 $.toaster({ title: 'Error', priority: 'danger', message: err.data });
             });


            };


        }




        $scope.deleteCompany = function (id) {




            var modalOptions = {
                actionButtonText: 'OK',
                headerText: 'Confirm',
                bodyText: "Are you sure, you want to delete?"
            };

            modalService.showModal({}, modalOptions).then(function (result) {

                companyService.delete(id).then(function (response) {
                    $scope.companyList = response.data;
                },
         function (err) {
             $.toaster({ title: 'Error', priority: 'danger', message: err.data });
         });



            });


        };

        $scope.selectAllCheck = function () {

            if ($scope.selectedAll) {
                $scope.selectedAll = true;
            } else {
                $scope.selectedAll = false;
            }

            



            angular.forEach($scope.roleModel.permissionList, function (c) {

                angular.forEach(c.actions, function (a) {

                    angular.forEach(a.permissionModel, function (p) {
                        p.isAllowed = $scope.selectedAll;
                    });

                });

            });

        };


        $scope.selectAllCheckCompany = function () {

            if ($scope.selectedAll) {
                $scope.selectedAll = true;
            } else {
                $scope.selectedAll = false;
            }





            angular.forEach($scope.permissionList, function (c) {

                angular.forEach(c.actions, function (a) {

                    angular.forEach(a.permissionModel, function (p) {
                        p.isAllowed = $scope.selectedAll;
                    });

                });

            });

        };

        


    }]);