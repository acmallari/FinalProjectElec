app.service("FinalProjService", function ($http) {

    //Load Positions
    this.loadPositionsData = function () {
        return $http.get("/Home/loadPositions");
    }
    this.loadCandidatesData = function () {
        return $http.get("/Home/loadCandidates");
    }




});
