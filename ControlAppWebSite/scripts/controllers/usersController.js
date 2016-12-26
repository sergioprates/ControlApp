controlApp.controller('usersController', ['$scope', 'callGetApi', '$location', function ($scope, callGetApi, $location) {
    $scope.filter = new String();
    $scope.users = [];
    $scope.currentPage = 0;
    $scope.itemsPerPage = 5;
    $scope.maxSize = 5;
    $scope.hasResults = false;

    $scope.alerts = [
        {type: 'success', msg: 'Utilize o campo acima para realizar uma busca.'}
    ];
    $scope.addAlert = function (message, tp) {
        $scope.alerts.push({ type: tp, msg: message });
    };

    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };

    $scope.editUser = function (login) {
        $location.path("/home/addUser/" + login);
    };

    $scope.changePagination = function () {
        var begin = ($scope.currentPage - 1) * $scope.itemsPerPage;
        var end = begin + $scope.itemsPerPage;

        $scope.paged = {
            users: $scope.users.slice(begin, end)
        }
    };

    $scope.searchUser = function () {
        try
        {
            callGetApi('api/user/search/' + $scope.filter, function (results) {
                if (results.users.length > 0) {
                    $scope.users = [];
                    $scope.currentPage = 1;
                    $scope.users = results.users;
                    $scope.closeAlert(0);
                    $scope.hasResults = true;
                }
                else {
                    $scope.users = [];
                    $scope.closeAlert(0);
                    $scope.addAlert('Nenhum resultado encontrado.', 'danger');
                    $scope.currentPage = 0;
                    $scope.hasResults = false;
                }

                $scope.changePagination();

            }, function (data, status, headers, config) {
                $scope.users = [];
                $scope.currentPage = 0;
                $scope.closeAlert(0);
                $scope.alerts = [];
                if (data == null || data.Message == undefined) {
                    if (data != null && data != '') {
                        $scope.addAlert(data, 'danger');
                    }
                    else {
                        $scope.addAlert('Não foi possível completar a requisição. Por favor tente novamente.', 'danger');
                    }
                }
                else {
                    $scope.addAlert(data.Message, 'danger');
                }
            });
        }
        catch (e) {
            $scope.alerts = [];
            $scope.addAlert(e, 'danger');
        }
    };
}]);