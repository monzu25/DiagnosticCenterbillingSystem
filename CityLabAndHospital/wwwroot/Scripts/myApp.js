var myApp = angular.module('myApp', ['ui-router']);
app.config(function ($stateProvider, $urlRouterProvider) {
    $stateProvider.state('ShowData', {
        url: '/ShowData',
        template: '../Page/Show.html'
    })
        .state('EditData', {
            url: '/EditData',
            templateUrl: '../Page/Edit.html'
        })


});

app.factory("myFactory",function)