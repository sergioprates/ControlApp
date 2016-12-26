﻿controlApp.controller('addUserController', ['$scope', 'callPostApiJSON', '$stateParams', 'callGetApi', '$modal', function ($scope, callPostApiJSON, $stateParams, callGetApi, $modal) {
    $scope.model = {};
    $scope.alerts = [];
    $scope.hasUser = false;

    $scope.addAlert = function (message, tp) {
        $scope.alerts.push({ type: tp, msg: message });
    };

    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };

    $scope.confirmDelete = function () {
        $scope.modalInstance = $modal.open({
            animation: true,
            backdrop: 'static',
            templateUrl: 'deleteModal.html',
            controller: 'addUserDeleteController',
            size: 'sm'
        });

        $scope.modalInstance.result.then(function (obj) {
            $scope.alerts = [];
            $scope.addAlert(obj.msg, obj.tp);
            if (obj.tp == 'success') {
                $scope.model = {};
                $stateParams.login = '';
            }
        }, function () {
            $scope.alerts = [];
            $scope.addAlert('Ocorreu algo inesperado na aplicação.', 'danger');
        });
    };

    if ($stateParams.login != '') {
        try {
            $scope.hasUser = true;
            callGetApi('api/user/' + $stateParams.login, function (results) {
                $scope.model = results.user;
            }, function (data) {
                if (data != null) {
                    if (data.Message != undefined) {
                        $scope.addAlert(data.Message, 'danger');
                    }
                    else {
                        $scope.addAlert(data, 'danger');
                    }
                }
                else {
                    $scope.addAlert('Ocorreu um problema ao realizar a requisição. Por favor tente novamente.', 'danger');
                }
            });
        }
        catch (e) {
            $scope.alerts = [];
            $scope.addAlert(e, 'danger');
        }
    }


    $scope.manageURL = function () {
        if ($stateParams.login != '') {
            $scope.callUserApi('api/user/update');
        }
        else {
            $scope.callUserApi('api/user');
        }
    };

    $scope.callUserApi = function (url) {
        try {
            callPostApiJSON(url, $scope.model, function (result) {
                $scope.alerts = [];
                $scope.addAlert(result.msg, 'success');
            },
                function (data, status, headers, config) {
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

controlApp.controller('addUserDeleteController', ['$scope', '$stateParams', 'callGetApi', '$modalInstance', function ($scope, $stateParams, callGetApi, $modalInstance) {
    $scope.isLoading = false;

    $scope.delete = function () {
        try {
            $scope.isLoading = true;
            callGetApi('api/user/delete/' + $stateParams.login, function (results) {
                $modalInstance.close($scope.createMessageObj(results.msg, 'success'));
            }, function (data) {
                if (data != null) {
                    if (data.Message != undefined) {
                        $scope.isLoading = false;
                        $modalInstance.close($scope.createMessageObj(data.Message, 'danger'));
                    }
                    else {
                        $scope.isLoading = false;
                        $modalInstance.close($scope.createMessageObj(data, 'danger'));
                    }
                }
                else {
                    $scope.isLoading = false;
                    $modalInstance.close($scope.createMessageObj('Ocorreu um problema ao realizar a requisição. Por favor tente novamente.', 'danger'));

                }
            });
        }
        catch (e) {
            $scope.isLoading = false;
            $modalInstance.close($scope.createMessageObj(e, 'danger'));
        }
    };

    $scope.createMessageObj = function (msg, tp) {
        return {
            msg: msg,
            tp: tp
        };
    };

    $scope.cancelDelete = function () {
        $modalInstance.dismiss('cancel');
    };
}]);