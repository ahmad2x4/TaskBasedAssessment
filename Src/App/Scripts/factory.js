
angular.module('app.factory', []).factory('incidentFactory', [
    'constants', '$http', '$q', '$window', function (constants, $http, $q, $window) {

        return {
            'getIncidentLookupData': function () {

                var deferred = $q.defer();

                $http.get(constants.LookUpDataPath)
                    .then(function (result) {
                        deferred.resolve(result.data);                        
                    },
                          function (error) {
                              deferred.reject(error);
                              throw { message: error }
                          }
                        );

                return deferred.promise;
            },
            'getIncidents': function () {
                var deferred = $q.defer();

                $http.get(constants.IncidentAPIPath)
                    .then(function (result) {
                        deferred.resolve(result.data);
                    },
                          function (error) {
                              deferred.reject(error);
                              throw { message: error }
                          }
                        );

                return deferred.promise;
            },
            'getIncident': function (id) {
                var deferred = $q.defer();
                
                $http.get(constants.IncidentAPIPath + "/" + id)
                    .then(function (result) {
                        deferred.resolve(result.data);
                    },
                          function (error) {
                              deferred.reject(error);
                              throw { message: error }
                          }
                        );

                return deferred.promise;
            },
            'createIncident': function (incident) {
              return  $http({
                    method: "post",
                    url: constants.IncidentAPIPath,
                    data: angular.toJson(incident)
                });
            },
            'updateIncident': function (incident) {
                return $http({
                    method: "put",
                    url: constants.IncidentAPIPath,
                    data: angular.toJson(incident)
                });
            },
            'deleteIncident': function (id) {
               
                return $http({
                    method: "delete",
                    url: constants.IncidentAPIPath + "/" + id.toString()
                });

            }
        }; 

    }]);
