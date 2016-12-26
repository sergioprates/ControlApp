controlApp.controller('homeController', ['$scope', function ($scope) {

    $scope.class = 'active';
    $scope.toggleClass = function () {
        if ($scope.class == 'active') {
            $scope.class = '';
        }
        else {
            $scope.class = 'active';
        }



    };

}]);