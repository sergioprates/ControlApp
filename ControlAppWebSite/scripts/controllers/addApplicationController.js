controlApp.controller('addApplicationController', ['$scope', 'callPostApiJSON', '$stateParams', 'callGetApi', '$modal', 'clearText', function ($scope, callPostApiJSON, $stateParams, callGetApi, $modal, clearText) {
    $scope.model = { Permissions: []};
    $scope.alerts = [];
    $scope.View = { Permission: {Name:'', Active: false}};
    $scope.hasApplication = false;

    $scope.alertsPermissions = [];
    $scope.currentPagePermission = 0;
    $scope.itemsPerPagePermission = 5;

    $scope.addAlertPermission = function (message, tp) {
        $scope.alertsPermissions.push({ type: tp, msg: message });
    };

    $scope.closeAlertPermission = function (index) {
        $scope.alertsPermissions.splice(index, 1);
    };

    $scope.changePaginationPermission = function () {
        var begin = ($scope.currentPagePermission - 1) * $scope.itemsPerPagePermission;
        var end = begin + $scope.itemsPerPagePermission;

        $scope.pagedPermission = {
            permissions: $scope.model.Permissions.slice(begin, end)
        }
    };


    $scope.addApplicationPermission = function () {
        var feature = clearText($scope.View.Permission.Name);
        var existPermission = _.find($scope.model.Permissions, function (permission) {
            
            if (permission.Feature == feature) {
                return permission;
            }
        });

        if (existPermission != undefined) {
            $scope.closeAlertPermission(0);
            $scope.addAlertPermission('Esta permissão já existe para essa aplicação. Por favor modifique o nome da permissão.', 'danger');
        }
        else
        {
            var permission = {
                Name: $scope.View.Permission.Name,
                Active: $scope.View.Permission.Active,
                Feature: feature
            };

            $scope.model.Permissions.push(permission);
            $scope.model.Permissions = _.sortBy($scope.model.Permissions, 'Name');
            $scope.currentPagePermission = 1;
            $scope.changePaginationPermission();
            $scope.View = { Permission: { Name: '', Active: false } };
        }
    };

    $scope.getActiveApplications = function () {
        try {
            callGetApi('api/application/active', function (results) {
                $scope.applications = results.applications;
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
    };

    $scope.changeGridApplication = function (item, model) {

        if ($scope.model.Applications.length > 0) {
            $scope.currentPageApplication = 1;
        }
        else {
            $scope.currentPageApplication = 0;
        }

        $scope.model.Applications = _.sortBy($scope.model.Applications, 'Name');

        $scope.changePaginationApplication();
    };




    ///


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
            controller: 'addApplicationDeleteController',
            size: 'sm'
        });

        $scope.modalInstance.result.then(function (obj) {
            $scope.alerts = [];
            $scope.addAlert(obj.msg, obj.tp);
            if (obj.tp == 'success') {
                $scope.model = {};
                $stateParams.hash = '';
            }
        }, function () {
            $scope.alerts = [];
            $scope.addAlert('Ocorreu algo inesperado na aplicação.', 'danger');
        });
    };

    if ($stateParams.hash != '') {
        try {
            $scope.hasApplication = true;
            callGetApi('api/application/' + $stateParams.hash, function (results) {
                $scope.model = results.application;

                if ($scope.model.Permissions != undefined && $scope.model.Permissions.length > 0) {
                    $scope.currentPagePermission = 1;
                    $scope.changePaginationPermission();
                }

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
        if ($stateParams.hash != '') {
            $scope.callApplicationApi('api/application/update');
        }
        else {
            $scope.callApplicationApi('api/application');
        }
    };

    $scope.callApplicationApi = function (url) {
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

controlApp.controller('addApplicationDeleteController', ['$scope', '$stateParams', 'callGetApi', '$modalInstance', function ($scope, $stateParams, callGetApi, $modalInstance) {
    $scope.isLoading = false;

    $scope.delete = function () {
        try {
            $scope.isLoading = true;
            callGetApi('api/application/delete/' + $stateParams.hash, function (results) {
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