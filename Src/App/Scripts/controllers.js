///
/// Controllers
angular.module('app.controllers', ['auth0'])
    .controller('FirstCtrl', [
    '$scope', function ($scope) {
    }])
    .controller('SecondCtrl', [
    '$scope', function ($scope) {
    }])
    .controller('UserInfoCtrl', [
    '$scope','auth', function ($scope,auth) {
        $scope.auth = auth;
        $scope.logout = function () {
            auth.signout();
            location.href = '/login';
        }
    }])
    .controller('IncidentCtrl', [
    '$scope', 'incidentFactory', function ($scope, factory) {

        var getIncidents = function () {
            factory.getIncidents().then(function (result) {
                $scope.incidents = result.incidents;
            });
        }
        getIncidents();

        $scope.setOrderField = function ($event) {

            var field = $($event.target).data('field');

            if ($scope.orderField === field) {
                $scope.isReverseOrder = !$scope.isReverseOrder;
            } else {
                $scope.orderField = field;
            }
        };

        $scope.delete = function (id) {

            if (confirm('Do you really want to delete this incident?')) {
                factory.deleteIncident(id).success(function () {
                    getIncidents();
                })
                   .error(function (error) {
                       throw { message: error.message };
                   });
            }
        }
    }])
    .controller('LoginCtrl', [
        '$scope', 'auth', function ($scope, authProvider, $location) {

            $scope.login = function () {
                authProvider.signin({
                    //popup: true
            });
            }

        }])
    .controller('IncidentCreateCtrl', [
    '$scope', 'incidentFactory', '$routeParams', function ($scope, factory, $routeParams) {

        $scope.isEdit = false;
        $scope.action = "Create";
        $scope.incidentName = "New incident";
        $scope.submitted = false;

        factory.getIncidentLookupData().then(function (result) {
            $scope.AlertLevels = result.alertLevels;
            $scope.Status = result.status;
            $scope.Type = result.type;
        });

        if ($routeParams.id != null) {
            $scope.isEdit = true;
            $scope.action = 'Update';

            factory.getIncident($routeParams.id).then(function (result) {
                $scope.incident = result;
                $scope.incidentName = $scope.incident.name;
            });
        }

        $scope.create = function ($event, incident) {
            $scope.submitted = true;

            if ($scope.incidentForm.$valid) {
                var btn = $($event.target);
                $scope.isEdit ? btn.button('Updating') : btn.button('Creating');

                if ($scope.isEdit) {
                    factory.updateIncident(incident).success(function () {
                        location.href = '#/incident';
                    })
                    .error(function (error) {
                        throw { message: error.message };
                    });
                }
                else {
                    factory.createIncident(incident).success(function () {
                        location.href = '#/incident';
                    })
                .error(function (error) {
                    throw { message: error.message };
                });

                }
            }
        }

    }]);
;
