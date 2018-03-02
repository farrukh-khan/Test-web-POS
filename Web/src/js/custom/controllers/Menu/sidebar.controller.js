'use strict';
angular.module('naut').controller('sidebarController', ['$scope', '$location', '$modal', '$window', 'localStorageService', 'ngAuthSettings', 'initService', '$sce',

    function ($scope, $location, $modal, $window, localStorageService, ngAuthSettings, initService, $sce) {


        $scope.model =
                {
                    userType: 1,
                    iCustomer: 0,
                    roleId: 0
                };


        var authData = localStorageService.get('authorizationData');
        if (authData) {
            $scope.model.userType = authData.userType;
            //$scope.model.iCustomer = authData.iCustomer;
            $scope.model.roleId = authData.roleId;
        }

        var html = "";

        if ($scope.model.userType == 1) {
            html += '<li ui-sref-active="active">';
            html += '<a href="#/app/companies" title="Customers" ripple="">';
            html += '<em class="sidebar-item-icon icon-globe"></em>';
            html += '<span translate="Companies">Companies</span>';
            html += '</a>';
            html += '</li>';


          


            $scope.bindNav = $sce.trustAsHtml(html);
        }
        else {


            initService.getNavigation($scope.model.roleId).then(function (response) {

                for (var i = 0; i < response.data.length; i++) {

                    var parent = response.data[i];




                    if (parent.category.toUpperCase() == "DASHBOARD" || parent.category.toUpperCase() == "REPORT CATALOGUE") {

                        html += '<li ui-sref-active="active">';
                        html += '<a href="' + parent.url + '" title="' + parent.category + '" ripple="">';
                        html += '<em class="sidebar-item-icon ' + parent.icon + '"></em>';
                        html += '<span translate="' + parent.category + '">' + parent.category + '</span>';
                        html += '</a>';
                        html += '</li>';
                    }
                    else {
                        html += '<li ng-class="{active:$state.includes(' + parent.category + ')}" >';

                        html += '<a href="" title="' + parent.category + '" ripple="">';
                        html += '<em class="sidebar-item-icon ' + parent.icon + '""></em>';
                        html += '     <em class="sidebar-item-caret fa pull-right fa-angle-right"></em>';
                        html += '<span translate="' + parent.category + '">' + parent.category + '</span>';
                        html += '</a>';


                        for (var j = 0; j < parent.actions.length; j++) {

                            var child = parent.actions[j];

                            html += '     <ul  class="nav sidebar-subnav" >';
                            html += '<li ui-sref-active="active">';
                            html += '                     <a href="' + child.url + '" title="' + child.action + '" ripple="">';
                            html += '<em class="sidebar-item-icon"></em>';
                            html += '    <span translate="' + child.action + '">' + child.action + '</span>';
                            html += '                     </a>';
                            html += '</li>';

                            html += '             </ul>';
                        }


                    }


                }

                $scope.bindNav = $sce.trustAsHtml(html);


            },
           function (err) {
               $.toaster({ title: 'Error', priority: 'danger', message: err.data });
           });


        }







    }
]);
