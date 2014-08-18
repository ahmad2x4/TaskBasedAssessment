///
/// AngularJS Modules
/// -------------------------------------------------------------------------------------------------------------------
/// <reference path="_references.ts" />
// Declare app level module which depends on filters, and services
angular
    .module('app', ['ngRoute', 'app.constant', 'app.filters', 'app.services', 'app.factory', 'app.directives', 'app.controllers', 'auth0'])
    .config([
    '$locationProvider', '$routeProvider', 'constants', 'authProvider', '$httpProvider', function ($locationProvider, $routeProvider, constants, authProvider, $httpProvider) {

        $locationProvider.html5Mode(true);

        $routeProvider
            .when('/view1', { templateUrl: 'views/view1', controller: 'FirstCtrl' })
            .when('/view2', { templateUrl: 'views/view2', controller: 'SecondCtrl' })
            .when('/incident', { templateUrl: 'views/incident', controller: 'IncidentCtrl' })
            .when('/incident/create', { templateUrl: 'views/incidentcreate', controller: 'IncidentCreateCtrl', requiresLogin: true })
            .when('/incident/:id', { templateUrl: 'views/incidentcreate', controller: 'IncidentCreateCtrl', requiresLogin: true })
            .when('/login', { templateUrl: 'views/Login', controller: 'LoginCtrl' })
            .when('/userinfo', { templateUrl: 'views/UserInfo', controller: 'UserInfoCtrl', requiresLogin: true })
            .otherwise({ redirectTo: '/view1' });
        authProvider.init({
            domain: constants.AUTH0_DOMAIN,
            clientID: constants.AUTH0_CLIENT_ID,
            callbackURL: location.href,
            loginUrl: '/login'
        });

        $httpProvider.interceptors.push('authInterceptor');

        authProvider.on('loginSuccess', function ($location) {
            $location.path('/userinfo');
        });

        authProvider.on('loginFailure', function ($log, error) {
            $log('Error logging in', error);
        });
}]);
