app.service("FinalProjService", function ($http) {
    this.initVoteSet = function () {
        return $http.get("/Home/initVoteSet");
    }

    //Load Positions
    this.loadPositionsData = function () {
        return $http.get("/Home/loadPositions");
    }

    this.loadCandidatesData = function () {
        return $http.get("/Home/loadCandidates");
    }

    this.getStud = function (studNum) {
        var response = $http({
            method: "post",
            url: "/Home/getStud",
            params: {
                uStudNum: studNum
            }
        });
        return response;
    }
    this.studLogin = function (studNum) {
        var response = $http({
            method: "post",
            url: "/Home/studLogin",
            params: {
                uStudNum: studNum
            }
        });
        return response;
    }
    this.studValidate = function (studId, voteSet) {
        var response = $http({
            method: "post",
            url: "/Home/studValidate",
            params: {
                uStudId: studId,
                setNum: voteSet
            }
        });
        return response;
    }

    this.adminLogin = function (email, pass) {
        var response = $http({
            method: "post",
            url: "/Home/adminLogin",
            params: {
                uEmail: email,
                uPass: pass
            }
        });
        return response;
    }

    this.submitVote = function (studId, voteSet, voteData) {
        var response = $http({
            method: "post",
            url: "/Home/submitVote",
            params: {
                uStudId: studId,
                setNum: voteSet,
                voteData: voteData
            }
        });
        return response;
    }

    this.submitTally = function (voteSet, tallyData) {
        var response = $http({
            method: "post",
            url: "/Home/submitTally",
            data: tallyData,
            params: {
                setNum: voteSet
            }
        });
        return response;
    }

    this.submitStudLog = function (studNum, lAction) {
        var response = $http({
            method: "post",
            url: "/Home/submitStudLog",
            params: {
                uStudNum: studNum,
                lAction: lAction
            }
        });
        return response;
    }

    this.loadAccountDash = function () {
        return $http.get("/Home/loadAccountDash");
    }

    this.loadLogDash = function () {
        return $http.get("/Home/loadLogDash");
    }

    this.loadCandiDash = function () {
        return $http.get("/Home/loadCandiDash");
    }
    this.loadPartyDash = function () {
        return $http.get("/Home/loadPartyDash");
    }
    this.loadStudDash = function () {
        return $http.get("/Home/loadStudDash");
    }
    this.loadVoteDash = function () {
        return $http.get("/Home/loadVoteDash");
    }


});
