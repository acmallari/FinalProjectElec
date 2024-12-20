app.controller("FinalProjController", function ($scope, FinalProjService) {

    //Load Positions
    $scope.loadPositions = function () {
        var getData = FinalProjService.loadPositionsData();


        getData.then(function (ReturnedData) {
            $scope.positionData = ReturnedData.data;
        });
    };

    $scope.loadPositions();

    //Load Candidate Members
    $scope.loadCandidates = function () {
        var getData = FinalProjService.loadCandidatesData();

        getData.then(function (ReturnedData) {
            $scope.candidateData = ReturnedData.data;
        });
    };

    $scope.loadCandidates();



});
