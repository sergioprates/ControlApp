controlApp.controller('clientsController', ['$scope', 'callGetApi', '$location', function ($scope, callGetApi, $location) {
    $scope.filter = new String();
    $scope.clients = [];
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

    $scope.edit = function (acronym) {
        $location.path("/home/addClient/" + acronym);
    };

    $scope.changePagination = function () {
        var begin = ($scope.currentPage - 1) * $scope.itemsPerPage;
        var end = begin + $scope.itemsPerPage;

        $scope.paged = {
            clients: $scope.clients.slice(begin, end)
        }
    };

    $scope.searchClient = function () {
        try {
            callGetApi('api/client/search/' + $scope.filter, function (results) {
                if (results.clients.length > 0) {
                    $scope.clients = [];
                    $scope.currentPage = 1;
                    $scope.clients = results.clients;
                    $scope.closeAlert(0);
                    $scope.hasResults = true;
                }
                else {
                    $scope.clients = [];
                    $scope.closeAlert(0);
                    $scope.addAlert('Nenhum resultado encontrado.', 'danger');
                    $scope.currentPage = 0;
                    $scope.hasResults = false;
                }

                $scope.changePagination();

            }, function (data, status, headers, config) {
                $scope.clients = [];
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