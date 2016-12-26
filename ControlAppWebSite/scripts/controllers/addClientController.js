controlApp.controller('addClientController', ['$scope', 'callPostApiJSON', '$stateParams', 'callGetApi', '$modal', function ($scope, callPostApiJSON, $stateParams, callGetApi, $modal) {


    //ALL TABS
    $scope.model = { Applications: [] };
    $scope.groupsActive = false;
    $scope.usersActive = false;
    $scope.permissionsActive = false;
    $scope.permissionsDisabled = true;
    $scope.usersDisabled = true;
    $scope.groupsDisabled = true;


    $scope.confirmDelete = function () {
        $scope.modalInstance = $modal.open({
            animation: true,
            backdrop: 'static',
            templateUrl: 'deleteModal.html',
            controller: 'addClientDeleteController',
            size: 'sm'
        });

        $scope.modalInstance.result.then(function (obj) {
            $scope.alerts = [];
            $scope.addAlert(obj.msg, obj.tp);
            if (obj.tp == 'success') {
                $scope.model = {};
                $stateParams.acronym = '';
            }
        }, function () {
            $scope.alerts = [];
            $scope.addAlert('Ocorreu algo inesperado na aplicação.', 'danger');
        });
    };

    $scope.manageURL = function () {
        if ($stateParams.acronym != '') {
            $scope.callClientApi('api/client/update');
        }
        else {
            $scope.callClientApi('api/client');
        }
    };

    $scope.callClientApi = function (url) {
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

    $scope.getAllUsers = function () {
        callGetApi('api/user', function (results) {
            $scope.AllUsers = results.users;
            $scope.changePaginationUsers();
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
    };



    //END ALL TABS

    //CLIENT TAB
    $scope.alerts = [];
    $scope.hasClient = false;
    $scope.addAlert = function (message, tp) {
        $scope.alerts.push({ type: tp, msg: message });
    };

    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };

    //END CLIENT TAB

    //APPLICATION TAB
    $scope.alertsApplication = [];
    $scope.hasApplication = false;
    $scope.applications = [];
    $scope.currentPageApplication = 0;
    $scope.itemsPerPageApplication = 5;

    $scope.addAlertApplication = function (message, tp) {
        $scope.alertsApplication.push({ type: tp, msg: message });
    };

    $scope.closeAlertApplication = function (index) {
        $scope.alertsApplication.splice(index, 1);
    };

    $scope.changePaginationApplication = function () {
        var begin = ($scope.currentPageApplication - 1) * $scope.itemsPerPageApplication;
        var end = begin + $scope.itemsPerPageApplication;

        $scope.pagedApplication = {
            applications: $scope.model.Applications.slice(begin, end)
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

    $scope.manageApplicationGroups = function (application) {
        $scope.contextApplication = application
        $scope.contextGroups = application.Groups;
        $scope.groupsActive = true;
        $scope.groupsDisabled = false;
    };

    //END APPLICATION TAB

    //USERS TAB
    $scope.alertsUser = [];
    $scope.AllUsers = [];
    $scope.contextUsers = [];
    $scope.currentPageUser = 0;
    $scope.itemsPerPageUser = 5;

    $scope.changeUsersActiveTab = function () {
        $scope.usersActive = false;
        $scope.usersDisabled = true;
    };

    $scope.addAlertUser = function (message, tp) {
        $scope.alertsUser.push({ type: tp, msg: message });
    };

    $scope.closeAlertUser = function (index) {
        $scope.alertsUser.splice(index, 1);
    };



    $scope.changePaginationUsers = function () {
        var begin = ($scope.currentPageUser - 1) * $scope.itemsPerPageUser;
        var end = begin + $scope.itemsPerPageUser;
        var users = [];
        _.map($scope.contextGroup, function (group) {
            if ($scope.contextGroup.Hash == group.Hash) {
                users = _.sortBy(group.Users, 'Name');
            }
            return group;
        });

        $scope.pagedUser = {
            users: users.slice(begin, end)
        }
    };


    $scope.changeGridUser = function (item, model) {

        if ($scope.contextUsers.length > 0) {
            $scope.currentPageUser = 1;
        }
        else {
            $scope.currentPageUser = 0;
        }

        $scope.contextUsers = _.sortBy($scope.contextUsers, 'Name');

        $scope.changePaginationUsers();
    };

    //END USERS TAB

    //GROUPS TAB
    $scope.alertsGroup = [];
    $scope.View = {};
    $scope.currentPageGroup = 0;
    $scope.itemsPerPageGroup = 5;

    $scope.manageGroupUsers = function (group) {
        $scope.contextGroup = group;

        if (group.Users == undefined) {
            $scope.contextUsers = [];
        }
        else {
            $scope.contextUsers = group.Users;
        }
        $scope.usersActive = true;
        $scope.usersDisabled = false;
    };

    $scope.addAlertGroup = function (message, tp) {
        $scope.alertsGroup.push({ type: tp, msg: message });
    };

    $scope.closeAlertGroups = function (index) {
        $scope.alertsGroup.splice(index, 1);
    };

    $scope.changeGroupActiveTab = function () {
        $scope.groupsActive = false;
        $scope.groupsDisabled = true;
    };

    $scope.addApplicationGroup = function () {
        var group = {
            Name: $scope.View.groupName,
            Active: true
        };
        var success = true;

        $scope.model.Applications = _.map($scope.model.Applications, function (application) {
            if ($scope.contextApplication.Hash == application.Hash) {

                var existGroup = _.find(application.Groups, function (groupItem) {
                    return groupItem.Name == group.Name;
                });

                if (existGroup == undefined) {
                    application.Groups.push(group);
                    application.Groups = _.sortBy(application.Groups, 'Name');
                }
                else {
                    $scope.closeAlertGroups(0);
                    $scope.addAlertGroup('Este grupo já existe para essa aplicação.', 'danger');
                    success = false;
                }
            }
            return application;
        });

        $scope.View.groupName = '';
        $scope.currentPageGroup = 1;
        $scope.changePaginationGroups();
        if (success) {
            $scope.closeAlertGroups(0);
            $scope.addAlertGroup('Grupo cadastrado com sucesso!', 'success');
        }

    };

    $scope.changePaginationGroups = function () {
        var begin = ($scope.currentPageGroup - 1) * $scope.itemsPerPageGroup;
        var end = begin + $scope.itemsPerPageGroup;
        var groups = [];
        _.map($scope.model.Applications, function (application) {
            if ($scope.contextApplication.Hash == application.Hash) {
                groups = _.sortBy(application.Groups, 'Name');
            }
            return application;
        });

        $scope.pagedGroup = {
            groups: groups.slice(begin, end)
        }
    };

    //END GROUPS TAB















    //PAGE LOAD Methods

    $scope.getActiveApplications();

    $scope.getAllUsers();

    if ($scope.model.Applications.length == 0) {
        $scope.alertsApplication = [
        { type: 'danger', msg: 'Nenhuma aplicação cadastrada para este cliente. Para adicionar aplicações basta digitar o nome e selecioná-las no campo abaixo.' }
        ];
    }
    else {
        $scope.alertsApplication = [
        { type: 'success', msg: 'Para adicionar aplicações a este cliente, basta digitar o nome, selecioná-las no campo abaixo.' }
        ];
    }

    if ($stateParams.acronym != '') {
        try {
            $scope.hasClient = true;
            callGetApi('api/client/' + $stateParams.acronym, function (results) {
                $scope.model = results.client;
                $scope.model.disabled = true;
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

    
}]);

//DELETE

controlApp.controller('addClientDeleteController', ['$scope', '$stateParams', 'callGetApi', '$modalInstance', function ($scope, $stateParams, callGetApi, $modalInstance) {
    $scope.isLoading = false;

    $scope.delete = function () {
        try {
            $scope.isLoading = true;
            callGetApi('api/client/delete/' + $stateParams.acronym, function (results) {
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