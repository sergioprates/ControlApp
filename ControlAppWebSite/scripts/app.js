'use strict';

var controlApp = angular.module('controlApp', ['ui.bootstrap', 'ui.bootstrap.tpls', 'ui.router', 'door3.css', 'ngAnimate', 'jcs-autoValidate', 'angular-loading-bar', 'ngSanitize', 'ui.select'])
.run([
        'bootstrap3ElementModifier',
        function (bootstrap3ElementModifier) {
            bootstrap3ElementModifier.enableValidationStateIcons(true);
        }])
.controller('navbarController', ['$scope', function ($scope) {
    $scope.isCollapsed = true;
}]).config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
    $locationProvider.html5Mode(true);
    $stateProvider
    .state('default', {
        url: "/default",
        templateUrl: "default.html",
        css: [
            'bootstrap/css/bootstrap.min.css',
            'css/landing-page.css',
            'bootstrap/fonts/font-awesome/css/font-awesome.min.css',
            'bootstrap/fonts/font.css',
            'css/animate.css',
            'css/loading-bar.min.css'
        ]

    })
    .state('home', {
        url: "/home",
        templateUrl: "home.html",
        css: [
            'bootstrap/css/bootstrap.min.css',
            'css/home.css',
            'css/animate.css',
            'css/loading-bar.min.css'
        ]
    })
    .state('home.users', {
        url: "/users",
        templateUrl: "users.html",
        css: [
            'bootstrap/css/bootstrap.min.css',
            'css/home.css',
            'css/animate.css',
            'css/loading-bar.min.css'
        ]
    })
    .state('home.groups', {
        url: "/groups",
        templateUrl: "groups.html",
        css: [
            'bootstrap/css/bootstrap.min.css',
            'css/home.css',
            'css/animate.css',
            'css/loading-bar.min.css'
        ]
    })
     .state('home.applications', {
         url: "/applications",
         templateUrl: "applications.html",
         css: [
             'bootstrap/css/bootstrap.min.css',
             'css/home.css',
             'css/animate.css',
            'css/loading-bar.min.css'
         ]
     })
     .state('home.clients', {
            url: "/clients",
            templateUrl: "clients.html",
            css: [
                'bootstrap/css/bootstrap.min.css',
                'css/home.css',
                'css/animate.css',
               'css/loading-bar.min.css'
            ]
        })
     .state('home.addUser', {
         url: "/addUser/:login",
         templateUrl: "addUser.html",
         css: [
             'bootstrap/css/bootstrap.min.css',
             'css/home.css',
             'css/animate.css',
            'css/loading-bar.min.css'
         ]
     })
    .state('home.addGroup', {
        url: "/addGroup/:group",
        templateUrl: "addGroup.html",
        css: [
            'bootstrap/css/bootstrap.min.css',
            'css/home.css',
            'css/animate.css',
           'css/loading-bar.min.css'
        ]
    })
    .state('home.addClient', {
        url: "/addClient/:acronym",
        templateUrl: "addClient.html",
        css: [
            'bootstrap/css/bootstrap.min.css',
            'css/home.css',
            'css/animate.css',
            'css/loading-bar.min.css',
            'css/select2.css'
        ]
    })
    .state('home.addApplication', {
        url: "/addApplication/:hash",
        templateUrl: "addApplication.html",
        css: [
            'bootstrap/css/bootstrap.min.css',
            'css/home.css',
            'css/animate.css',
           'css/loading-bar.min.css'
        ]
    });

    $urlRouterProvider.otherwise("/default");

}).directive('a', function () {
    return {
        restrict: 'E',
        link: function (scope, elem, attrs) {
            if (attrs.ngClick || attrs.href === '' || attrs.href === '#') {
                elem.on('click', function (e) {
                    e.preventDefault();
                });
            }
        }
    };
}).directive('back', ['$window', function ($window) {
    return {
        restrict: 'A',
        link: function (scope, elem, attrs) {
            elem.bind('click', function () {
                $window.history.back();
            });
        }
    };
}]).directive('mustcontainword', [
                function () {
                    return {
                        restrict: 'A',
                        require: 'ngModel',
                        link: function (scope, elm, attrs, ctrl) {
                            var validateFn = function (viewValue) {
                                if (ctrl.$isEmpty(viewValue) || viewValue.toLowerCase().indexOf(attrs.mustcontainword.toLowerCase()) === -1) {
                                    ctrl.$setValidity('mustcontainword', false);
                                    return undefined;
                                } else {
                                    ctrl.$setValidity('mustcontainword', true);
                                    return viewValue;
                                }
                            };

                            ctrl.$parsers.push(validateFn);
                            ctrl.$formatters.push(validateFn);
                        }
                    }
                }])
.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            items.forEach(function (item) {
                var itemMatches = false;

                var keys = Object.keys(props);
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});