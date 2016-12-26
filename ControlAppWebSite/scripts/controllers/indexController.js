controlApp.controller('indexController', ['$scope', 'authenticate', '$location', function ($scope, authenticate,$location) {

    $scope.alerts = [];

    $scope.addAlert = function (message) {
        $scope.alerts.push({ type: 'danger', msg: message });
    };

    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };

    $scope.doAuth = function () {        
        authenticate($scope.user.Login, $scope.user.Password, function (result) {
            window.localStorage.setItem('controlAppToken', result.access_token);
            $location.path("/home");
        }, function (data, status, headers, config) {
            if (data != null && data.error_description != '' && data.error_description != null) {
                $scope.alerts = [];
                $scope.addAlert(data.error_description);
            }
            else {
                $scope.alerts = [];
                $scope.addAlert('Não foi possível completar a requisição. Por favor tente novamente.');
            }
        });
    };
}]);