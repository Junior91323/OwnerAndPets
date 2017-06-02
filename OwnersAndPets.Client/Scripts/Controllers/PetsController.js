var PetsController = function ($scope, $http, $routeParams) {
    $scope.models = {
        helloAngular: 'pets'
    };

    $scope.itemsByPage = 3;
    $scope.totalItems = 0;
    $scope.currentPage = 1;

    $scope.pageChanged = function () {
        RefreshData();
    };


    var RefreshData = function () {

        $http.get(AppSettings.Api.Owners + $routeParams.id).then(function successCallback(data) {
            if (data != null) {
                // console.log(data);
                $scope.Owner = data.data;
            }
        }, function errorCallback(error) {
            if (error != null && error != undefined) {
                if (error.data != null && error.data.ModelState != null)
                    AppSettings.Utils.ShowMessage("danger", AppSettings.Utils.ParseModelState(error.data.ModelState));
                else
                    AppSettings.Utils.ShowMessage("danger", error.statusText);
            }
        });

        $http.get(AppSettings.Api.Pets + '?page=' + $scope.currentPage + '&itemsCount=' + $scope.itemsByPage + '&ownerId=' + $routeParams.id).then(function successCallback(data) {
            if (data != null) {
                //console.log(data);
                $scope.data = data.data.Items;
                $scope.totalItems = data.data.PageInfo.TotalItems;
                $scope.owner = data.data.owner;

            }
        }, function errorCallback(error) {
            if (error != null && error != undefined) {
                if (error.data != null && error.data.ModelState != null)
                    AppSettings.Utils.ShowMessage("danger", AppSettings.Utils.ParseModelState(error.data.ModelState));
                else
                    AppSettings.Utils.ShowMessage("danger", error.statusText);
            }
        });
    }

    if ($routeParams.id != null && $routeParams.id != undefined) {
        RefreshData();
    }
    $scope.AddPet = function (pet, form) {
        if (form.$valid) {
            pet.OwnerId = $routeParams.id;
            $http.post(AppSettings.Api.Pets, pet).then(function (data) {
                if (data != null) {
                    RefreshData();
                    $scope.pet = {};
                    AppSettings.Utils.ShowMessage("success", "Added!");
                }
            }, function (error) {
                if (error != null && error != undefined) {
                    if (error.data != null && error.data.ModelState != null)
                        AppSettings.Utils.ShowMessage("danger", AppSettings.Utils.ParseModelState(error.data.ModelState));
                    else
                        AppSettings.Utils.ShowMessage("danger", error.statusText);
                }
            });
        }
    };

    $scope.DeletePet = function (id) {
        if (id != 0 && confirm("Are you sure?")) {
            $http.delete(AppSettings.Api.Pets + id).then(function (data) {
                if (data != null) {
                    RefreshData();
                    AppSettings.Utils.ShowMessage("warning", "Deleted!");
                }
            }, function (error) {
                if (error != null && error != undefined) {
                    if (error.data != null && error.data.ModelState != null)
                        AppSettings.Utils.ShowMessage("danger", AppSettings.Utils.ParseModelState(error.data.ModelState));
                    else
                        AppSettings.Utils.ShowMessage("danger", error.statusText);
                }
            });
        }
    };
}

// The $inject property of every controller (and pretty much every other type of object in Angular) needs to be a string array equal to the controllers arguments, only as strings
PetsController.$inject = ['$scope', '$http', '$routeParams'];