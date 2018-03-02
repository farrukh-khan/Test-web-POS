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
      //'angular.morris-chart',
    'angularFileUpload',
    'alexjoffroy.angular-loaders',
     'mdo-angular-cryptography'
    
    

      
      //'angular-loading-bar'


  ]);

app.constant('ngAuthSettings', {

    apiServiceBaseUri: 'http://localhost:7627/',
    clientId: 'SR_LOCAL',
    //apiServiceBaseUri: 'http://192.168.1.238:8085/',
    //clientId: 'SR_LIVE',
});

app.config(function ($httpProvider) {

    $httpProvider.interceptors.push('authInterceptorService');
});

app.config(['$cryptoProvider', function($cryptoProvider){
    $cryptoProvider.setCryptographyKey('ABCD123');
}]);

app.run(['authService', function (authService) {

    authService.fillAuthData();


}]);

app.directive('myEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.myEnter);
                });

                event.preventDefault();
            }
        });
    };
});


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
})