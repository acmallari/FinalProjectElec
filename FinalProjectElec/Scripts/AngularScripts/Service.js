app.service("FinalProjService", function ($http) {
    this.initVoteSet = function () {
        return $http.get("/Home/initVoteSet");
    }

    //Load Positions
    this.loadPositionsData = function () {
        return $http.get("/Home/loadPositions");
    }

    this.loadPartiesData = function () {
        return $http.get("/Home/loadParties");
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

    this.loadPiePres = function (voteSet) {
        var response = $http({
            method: "post",
            url: "/Home/loadPiePres",
            params: {
                setNum: voteSet
            }
        });
        return response;
    }

    this.loadPieVP = function (voteSet) {
        var response = $http({
            method: "post",
            url: "/Home/loadPieVP",
            params: {
                setNum: voteSet
            }
        });
        return response;
    }

    this.loadPieSec = function (voteSet) {
        var response = $http({
            method: "post",
            url: "/Home/loadPieSec",
            params: {
                setNum: voteSet
            }
        });
        return response;
    }

    this.loadPieTrea = function (voteSet) {
        var response = $http({
            method: "post",
            url: "/Home/loadPieTrea",
            params: {
                setNum: voteSet
            }
        });
        return response;
    }

    this.loadPieAud = function (voteSet) {
        var response = $http({
            method: "post",
            url: "/Home/loadPieAud",
            params: {
                setNum: voteSet
            }
        });
        return response;
    }

    this.loadPiePRO = function (voteSet) {
        var response = $http({
            method: "post",
            url: "/Home/loadPiePRO",
            params: {
                setNum: voteSet
            }
        });
        return response;
    }

    this.addAccount = function (email, pass) {
        var response = $http({
            method: "post",
            url: "/Home/addAccount",
            params: {
                uEmail: email,
                uPass: pass
            }
        });
        return response;
    }
    this.addParty = function (name, camp) {
        var response = $http({
            method: "post",
            url: "/Home/addParty",
            params: {
                pName: name,
                pCamp: camp
            }
        });
        return response;
    }
    this.addStud = function (sNum) {
        var response = $http({
            method: "post",
            url: "/Home/addStud",
            params: {
                studNum: sNum
            }
        });
        return response;
    }
    this.addCandi = function (cFName, cLName, cParty, cPos) {
        var response = $http({
            method: "post",
            url: "/Home/addCandi",
            params: {
                fName: cFName,
                lName: cLName,
                party: cParty,
                pos: cPos
            }
        });
        return response;
    }

    this.loadEditAcc = function (accid) {
        var response = $http({
            method: "post",
            url: "/Home/loadEditAcc",
            params: {
                accid: accid
            }
        });
        return response;
    }
    this.editAccount = function (accid, email, pass) {
        var response = $http({
            method: "post",
            url: "/Home/editAccount",
            params: {
                accid: accid,
                uEmail: email,
                uPass: pass
            }
        });
        return response;
    }

    this.loadEditCandi = function (candid) {
        var response = $http({
            method: "post",
            url: "/Home/loadEditCandi",
            params: {
                candid: candid
            }
        });
        return response;
    }

    this.editCandi = function (candid, fname, lname, party, pos) {
        var response = $http({
            method: "post",
            url: "/Home/editCandi",
            params: {
                candid: candid,
                fname: fname,
                lname: lname,
                party: party,
                pos: pos
            }
        });
        return response;
    }

    this.loadEditParty = function (partyid) {
        var response = $http({
            method: "post",
            url: "/Home/loadEditParty",
            params: {
                partyid: partyid
            }
        });
        return response;
    }

    this.editParty = function (partyid, pName, pCamp) {
        var response = $http({
            method: "post",
            url: "/Home/editParty",
            params: {
                partyid: partyid,
                pName: pName,
                pCamp: pCamp
            }
        });
        return response;
    }

    this.loadEditStud = function (studid) {
        var response = $http({
            method: "post",
            url: "/Home/loadEditStud",
            params: {
                studid: studid
            }
        });
        return response;
    }

    this.editStud = function (studid, studNum) {
        var response = $http({
            method: "post",
            url: "/Home/editStud",
            params: {
                studid: studid,
                studNum: studNum
            }
        });
        return response;
    }

    this.archiveAcc = function (accid) {
        var response = $http({
            method: "post",
            url: "/Home/archiveAcc",
            params: {
                accid: accid,
            }
        });
        return response;
    }
    this.archiveCandi = function (candid) {
        var response = $http({
            method: "post",
            url: "/Home/archiveCandi",
            params: {
                candid: candid,
            }
        });
        return response;
    }
    this.archiveParty = function (partyid) {
        var response = $http({
            method: "post",
            url: "/Home/archiveParty",
            params: {
                partyid: partyid,
            }
        });
        return response;
    }
    this.archiveStud = function (studid) {
        var response = $http({
            method: "post",
            url: "/Home/archiveStud",
            params: {
                studid: studid,
            }
        });
        return response;
    }

    this.submitAdminLog = function (uEmail, lAction) {
        var response = $http({
            method: "post",
            url: "/Home/submitAdminLog",
            params: {
                uEmail: uEmail,
                lAction: lAction
            }
        });
        return response;
    }


});
