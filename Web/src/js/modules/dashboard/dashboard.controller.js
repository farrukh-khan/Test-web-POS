/**=========================================================
 * Module: DashboardController.js
 =========================================================*/

(function() {
    'use strict';

    angular
        .module('naut')
        .controller('DashboardController', DashboardController);
    
    DashboardController.$inject = ['$scope', 'colors', 'flotOptions', '$timeout'];
    function DashboardController($scope, colors, flotOptions, $timeout) {

      // Some numbers for demo
      $scope.loadProgressValues = function() {
        $scope.progressVal = [0,0,0,0];
        // helps to show animation when values change
        $timeout(function(){
          $scope.progressVal[0] = 60;
          $scope.progressVal[1] = 34;
          $scope.progressVal[2] = 22;
          $scope.progressVal[3] = 76;
        });
      };

      // Pie Charts
      // ----------------------------------- 

      $scope.piePercent1 = 75;
      $scope.piePercent2 = 50;
      $scope.pieOptions = {
          animate:{
              duration: 700,
              enabled: true
          },
          barColor: colors.byName('info'),
          // trackColor: colors.byName('inverse'),
          scaleColor: false,
          lineWidth: 10,
          lineCap: 'circle'
      };

      // Dashboard charts
      // ----------------------------------- 

      // Spline chart
      $scope.splineChartOpts = angular.extend({}, flotOptions['default'], {
        series: {
          lines: {
            show: false
          },
          splines: {
            show: true,
            tension: 0.4,
            lineWidth: 2,
            fill: 0.5
          },
        },
        yaxis: {max: 50}
      });
      $scope.splineData = getSplineData();

      function getSplineData() {
        return [{
          'label': 'Campaing1',
          'color': colors.byName($scope.app.theme.name),
          'data': [
            ['1', 28],['2', 30],['3', 32],['4', 33],['5', 33],['6', 32],['7', 31],['8', 30],['9', 29],['10', 28],['11', 28],['12', 29],['13', 30],['14', 29],['15', 28]
          ]
        }, {
          'label': 'Campaing2',
          'color': colors.byName($scope.app.theme.name),
          'data': [
            ['1', 12],['2', 14],['3', 20],['4', 16],['5', 18],['6', 14],['7', 19],['8', 24],['9', 18],['10', 14],['11', 16],['12', 15],['13', 14],['14', 16],['15', 18]
          ]
        }, {
          'label': 'Campaing3',
          'color': colors.byName($scope.app.theme.name),
          'data': [
            ['1', 6],['2', 8],['3', 7],['4', 6],['5', 7],['6', 10],['7', 9],['8', 8],['9', 10],['10', 9],['11', 6],['12', 8],['13', 9],['14', 10],['15', 10]
          ]
        }];
      }

      $scope.$watch('app.theme.name', function(val) {
        $scope.splineData = getSplineData();
      });

      $scope.areaSplineSeries = [false, true, true];


      // Small line chart
      // ----------------------------------- 

      $scope.smallChartOpts = angular.extend({}, flotOptions['default'], {
        points: {
          show: true,
          radius: 1
        },
        series: {
          lines: {
            show: true,
            fill: 1,
            lineWidth: 1,
            fillColor: { colors: [ { opacity: 0.4 }, { opacity: 0.4 } ] }
          }
        },
        grid: {
            show: false
        },
        legend: {
            show: false
        },
        xaxis: {
            tickDecimals: 0
        }
      });
      $scope.smallChartData1 = [{
        'label': '',
        'color': colors.byName('success'),
        'data': [
          ['1', 8],['2', 10],['3', 12],['4', 13],['5', 13],['6', 12],['7', 11],['8', 10],['9', 9],['10', 8],['11', 8],['12', 9],['13', 10],['14', 9],['15', 8]
        ]
      }];
      $scope.smallChartData2 = [{
        'label': '',
        'color': colors.byName('warning'),
        'data': [
          ['1', 9],['2', 10],['3', 9],['4', 11],['5', 12],['6', 11],['7', 10],['8', 9],['9', 8],['10', 8],['11', 8],['12', 10],['13', 12],['14', 13],['15', 13]
        ]
      }];
      // Sparkline
      // ----------------------------------- 
      
      $scope.sparkValues1 = [2,3,4,6,6,5,6,7,8,9,10];
      $scope.sparkValues2 = [2,3,4,1,2,3,5,4,9,6,1];
      $scope.sparkValues3 = [6,5,1,2,6,9,8,7,4,5,6,9];
      $scope.sparkOptions = {
        barColor:      colors.byName('gray'),
        height:        15,
        barWidth:      5,
        barSpacing:    3,
        chartRangeMin: 0
      };

      $scope.sparkValuesLine = [1,3,4,7,5,9,4,4,7,5,9,6,4];
      $scope.sparkOptionsLine = {
        chartRangeMin: 0,
        type:               'line',
        height:             '80',
        width:              '100%',
        lineWidth:          '2',
        lineColor:          colors.byName('purple'),
        spotColor:          '#888',
        minSpotColor:       colors.byName('purple'),
        maxSpotColor:       colors.byName('purple'),
        fillColor:          '',
        highlightLineColor: '#fff',
        spotRadius:         '3',
        resize:             'true'
      };

    }
})();
