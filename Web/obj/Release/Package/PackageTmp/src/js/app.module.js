/*!
 * 
 * Naut - Bootstrap Admin Theme + AngularJS
 * 
 * Author: @geedmo
 * Website: http://geedmo.com
 * License: https://wrapbootstrap.com/help/licenses
 * 
 */

var app = angular
  .module('naut', [
    'ngRoute',
    'ngAnimate',
    'ngStorage',
    'ngCookies',
    'ngSanitize',
    'ngResource',
    'LocalStorageModule',
    'ui.bootstrap',
    'ui.router',
    'ui.utils',
    'oc.lazyLoad',
    'cfp.loadingBar',
    'tmh.dynamicLocale',
    'pascalprecht.translate',
      'brantwills.paging',
      'n3-pie-chart',
      'angular.morris-chart'
    
  ]);
app.value('backendServerUrl', 'http://webdevstaging.cloudapp.net:8085/');

app.constant('ngAuthSettings', {

    //apiServiceBaseUri: 'http://localhost:7627/',
    //clientId: 'SR_LOCAL',
    apiServiceBaseUri: 'http://webdevstaging.cloudapp.net:8085/',
    clientId: 'SR_LIVE',
});
app.config(function ($httpProvider) {

    $httpProvider.interceptors.push('authInterceptorService');
});


app.run(['authService', function (authService) {

    authService.fillAuthData();


}]);


app.directive('jqdatepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs) {
            element.datepicker({
                dateFormat: 'dd/mm/yy',
                onSelect: function (date) {
                    scope.date = date;
                    scope.$apply();
                }
            });
        }
    };
});