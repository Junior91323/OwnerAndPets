var OwnersController = function ($scope, $http, $routeParams, $filter, testFactory) {
    $scope.models = {
        helloAngular: 'owners'
    };

    $scope.itemsByPage = 3;
    $scope.totalItems = 0;
    $scope.currentPage = 1;

    $scope.pageChanged = function () {
        RefreshData();
    };

    function RefreshData() {
        //console.log(AppSettings.Api.Owners);
        $http.get(AppSettings.Api.Owners + '?page=' + $scope.currentPage + '&itemsCount=' + $scope.itemsByPage).then(function successCallback(data) {
            if (data != null) {
                //console.log(data);
                $scope.data = data.data.Items;
                $scope.totalItems = data.data.PageInfo.TotalItems;
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
    RefreshData();

    $scope.AddOwner = function (owner, form) {
        if (form.$valid) {
            //console.log(form);
            $http.post(AppSettings.Api.Owners, owner).then(function (data) {
                if (data != null) {
                    RefreshData();
                    $scope.owner = {};
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

    $scope.DeleteOwner = function (id) {
        if (id != 0 && confirm("Are you sure?")) {
            $http.delete(AppSettings.Api.Owners + id).then(function (data) {
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

OwnersController.$inject = ['$scope', '$http', '$routeParams', '$filter'];