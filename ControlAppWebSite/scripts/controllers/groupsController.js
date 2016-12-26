controlApp.controller('groupsController', ['$scope', 'callGetApi', '$location', function ($scope, callGetApi, $location) {
    $scope.filter = new String();
    $scope.groups = [];
    $scope.currentPage = 0;
    $scope.itemsPerPage = 5;
    $scope.maxSize = 5;
    $scope.hasResults = false;

    $scope.alerts = [
        { type: 'success', msg: 'Utilize o campo acima para realizar uma busca.' }
    ];
    $scope.addAlert = function (message, tp) {
        $scope.alerts.push({ type: tp, msg: message });
    };

    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };

    $scope.editGroup = function (login) {
        $location.path("/home/addGroup/" + login);
    };

    $scope.changePagination = function () {
        var begin = ($scope.currentPage - 1) * $scope.itemsPerPage;
        var end = begin + $scope.itemsPerPage;

        $scope.paged = {
            groups: $scope.groups.slice(begin, end)
        }
    };

    $scope.searchGroup = function () {
        try {
            callGetApi('api/group/search/' + $scope.filter, function (results) {
                if (results.groups.length > 0) {
                    $scope.groups = [];
                    $scope.currentPage = 1;
                    $scope.groups = results.groups;
                    $scope.closeAlert(0);
                    $scope.hasResults = true;
                }
                else {
                    $scope.groups = [];
                    $scope.closeAlert(0);
                    $scope.addAlert('Nenhum resultado encontrado.', 'danger');
                    $scope.currentPage = 0;
                    $scope.hasResults = false;
                }

                $scope.changePagination();

            }, function (data, status, headers, config) {
                $scope.groups = [];
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