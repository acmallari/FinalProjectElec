app.controller("FinalProjController", function ($scope, FinalProjService) {

    $scope.initVoteSet = function () {
        var getData = FinalProjService.initVoteSet();


        getData.then(function (ReturnedData) {
            $scope.curSet = ReturnedData.data;
        });
    }


    //Load Positions
    $scope.loadPositions = function () {
        var getData = FinalProjService.loadPositionsData();


        getData.then(function (ReturnedData) {
            $scope.positionData = ReturnedData.data;
        });
    };

    $scope.loadPositions();

    $scope.loadParties = function () {
        var getData = FinalProjService.loadPartiesData();


        getData.then(function (ReturnedData) {
            $scope.partyData = ReturnedData.data;
        });
    };

    $scope.loadParties();

    //Load Candidate Members
    $scope.loadCandidates = function () {
        var getData = FinalProjService.loadCandidatesData();

        getData.then(function (ReturnedData) {
            $scope.candidateData = ReturnedData.data;
        });
    };

    $scope.loadCandidates();

    $scope.studLogin = function (voteSet) {
        var uStudNum = $scope.uStudNum
        var getData = FinalProjService.getStud(uStudNum);
        getData.then(function (ReturnedSData) {
            var studId = ReturnedSData.data.studId;

            var postData = FinalProjService.studLogin(uStudNum);
            var validateData = FinalProjService.studValidate(studId, voteSet);

            postData.then(function (ReturnedData) {
                var returnValue = Number(ReturnedData.data);
                if (returnValue == 0) {
                    Swal.fire({
                        title: "Invalid Student Number",
                        icon: "error"
                    });
                }
                else if (returnValue == 1) {
                    validateData.then(function (ReturnedVData) {
                        var returnVValue = Number(ReturnedVData.data);
                        if (returnVValue == 0) {
                            Swal.fire({
                                title: "Only One Vote Per Set",
                                icon: "warning"
                            });
                        } else if (returnVValue == 1) {
                            sessionStorage.setItem("studNum", uStudNum);
                            sessionStorage.setItem("studId", studId);
                            window.location.href = "/Home/Votingpage";
                            //alert("Success")
                        }
                    });
                }
            });
        });
    }

    $scope.adminLogin = function () {
        var email = $scope.uEmail;
        var pass = $scope.uPass

        var postData = FinalProjService.adminLogin(email, pass);
        postData.then(function (ReturnedData) {
            var returnValue = Number(ReturnedData.data);
            if (returnValue == 0) {
                Swal.fire({
                    title: "Invalid Email or Password",
                    icon: "error"
                });
            } else if (returnValue == 1) {
                sessionStorage.setItem("uEmail", email);
                window.location.href = "/Home/Dashboard";
                //alert("Success")
            }
        });
    }

    $scope.pos = {};

    $scope.submitVote = function (voteSet) {
        var studId = JSON.parse(sessionStorage.getItem('studId'));
        var studNum = JSON.parse(sessionStorage.getItem('studNum'));

        var tallyData = [
            { candidate_id: $scope.pos[1] },
            { candidate_id: $scope.pos[2] },
            { candidate_id: $scope.pos[3] },
            { candidate_id: $scope.pos[4] },
            { candidate_id: $scope.pos[5] },
            { candidate_id: $scope.pos[6] }
        ];

        var president = $scope.pos[1];
        var vicePresident = $scope.pos[2];
        var secretary = $scope.pos[3];
        var treasurer = $scope.pos[4];
        var auditor = $scope.pos[5];
        var publicRelationsOfficer = $scope.pos[6];

        if ($scope.pos[1] == undefined || $scope.pos[2] == undefined || $scope.pos[3] == undefined || $scope.pos[4] == undefined || $scope.pos[5] == undefined || $scope.pos[6] == undefined) {
            alert("Answer all the fields");
        } else {
            var voteData = president + ", " + vicePresident + ", " + secretary + ", " + treasurer + ", " + auditor + ", " + publicRelationsOfficer;

            var postVData = FinalProjService.submitVote(studId, voteSet, voteData);

            var postTData = FinalProjService.submitTally(voteSet, tallyData);

            var lAction = "Has voted";

            var postLog = FinalProjService.submitStudLog(studNum, lAction);

            sessionStorage.removeItem('studId');
            sessionStorage.removeItem('studNum');

            window.location.href = "/Home/Homepage";
        }
    }

    $scope.loadAccountDash = function () {
        var getData = FinalProjService.loadAccountDash();


        getData.then(function (ReturnedData) {
            $scope.accountData = ReturnedData.data;

            $(document).ready(function () {
                $('#accountTable').DataTable({
                    layout: {
                        topStart: {

                        }
                    },
                    pagelength: 10,
                    bLengthChange: false,
                    language: {

                        paginate: {
                            first: 'First',
                            previous: 'Previous',
                            next: 'Next',
                            last: 'Last'
                        }
                    },
                    columnDefs: [
                        {
                            targets: [0, 1, 2, 3, 4],
                            classname: 'dt-center'
                        },
                        {
                            targets: [3, 4],
                            render: function (data, type, row, meta) {
                                if ((type === 'display' || type === 'filter') && ((meta.col === 3) || (meta.col === 4))) {
                                    return moment(data).format('YYYY-MM-DD HH:mm:ss');
                                }
                                return data;
                            }
                        }
                    ]
                })
            })
        });
    };

    $scope.loadAccountDash();

    $scope.loadLogDash = function () {
        var getData = FinalProjService.loadLogDash();


        getData.then(function (ReturnedData) {
            $scope.logData = ReturnedData.data;

            $(document).ready(function () {
                $('#logTable').DataTable({
                    layout: {
                        topStart: {

                        }
                    },
                    pagelength: 10,
                    bLengthChange: false,
                    language: {

                        paginate: {
                            first: 'First',
                            previous: 'Previous',
                            next: 'Next',
                            last: 'Last'
                        }
                    },
                    columnDefs: [
                        {
                            targets: [0, 1, 2, 3, 4, 5],
                            classname: 'dt-center'
                        },
                        {
                            targets: [4, 5],
                            render: function (data, type, row, meta) {
                                if ((type === 'display' || type === 'filter') && ((meta.col === 4) || (meta.col === 5))) {
                                    return moment(data).format('YYYY-MM-DD HH:mm:ss');
                                }
                                return data;
                            }
                        }
                    ]
                })
            })
        });
    };

    $scope.loadLogDash();

    $scope.loadCandiDash = function () {
        var getData = FinalProjService.loadCandiDash();


        getData.then(function (ReturnedData) {
            $scope.candiData = ReturnedData.data;

            $(document).ready(function () {
                $('#candiTable').DataTable({
                    layout: {
                        topStart: {

                        }
                    },
                    pagelength: 10,
                    bLengthChange: false,
                    language: {

                        paginate: {
                            first: 'First',
                            previous: 'Previous',
                            next: 'Next',
                            last: 'Last'
                        }
                    },
                    columnDefs: [
                        {
                            targets: [0, 1, 2, 3, 4, 5],
                            classname: 'dt-center'
                        },
                        {
                            targets: [4, 5],
                            render: function (data, type, row, meta) {
                                if ((type === 'display' || type === 'filter') && ((meta.col === 4) || (meta.col === 5))) {
                                    return moment(data).format('YYYY-MM-DD HH:mm:ss');
                                }
                                return data;
                            }
                        }
                    ]
                })
            })
        });
    };

    $scope.loadCandiDash();


    $scope.loadPartyDash = function () {
        var getData = FinalProjService.loadPartyDash();


        getData.then(function (ReturnedData) {
            $scope.partyData = ReturnedData.data;

            $(document).ready(function () {
                $('#partyTable').DataTable({
                    layout: {
                        topStart: {

                        }
                    },
                    pagelength: 10,
                    bLengthChange: false,
                    language: {

                        paginate: {
                            first: 'First',
                            previous: 'Previous',
                            next: 'Next',
                            last: 'Last'
                        }
                    },
                    columnDefs: [
                        {
                            targets: [0, 1, 2, 3, 4],
                            classname: 'dt-center'
                        },
                        {
                            targets: [3, 4],
                            render: function (data, type, row, meta) {
                                if ((type === 'display' || type === 'filter') && ((meta.col === 3) || (meta.col === 4))) {
                                    return moment(data).format('YYYY-MM-DD HH:mm:ss');
                                }
                                return data;
                            }
                        }
                    ]
                })
            })
        });
    };

    $scope.loadPartyDash();

    $scope.loadStudDash = function () {
        var getData = FinalProjService.loadStudDash();


        getData.then(function (ReturnedData) {
            $scope.studData = ReturnedData.data;

            $(document).ready(function () {
                $('#studTable').DataTable({
                    layout: {
                        topStart: {

                        }
                    },
                    pagelength: 10,
                    bLengthChange: false,
                    language: {

                        paginate: {
                            first: 'First',
                            previous: 'Previous',
                            next: 'Next',
                            last: 'Last'
                        }
                    },
                    columnDefs: [
                        {
                            targets: [0, 1, 2, 3],
                            classname: 'dt-center'
                        },
                        {
                            targets: [2, 3],
                            render: function (data, type, row, meta) {
                                if ((type === 'display' || type === 'filter') && ((meta.col === 2) || (meta.col === 3))) {
                                    return moment(data).format('YYYY-MM-DD HH:mm:ss');
                                }
                                return data;
                            }
                        }
                    ]
                })
            })
        });
    };

    $scope.loadStudDash();

    $scope.loadVoteDash = function () {
        var getData = FinalProjService.loadVoteDash();


        getData.then(function (ReturnedData) {
            $scope.voteData = ReturnedData.data;

            $(document).ready(function () {
                $('#voteTable').DataTable({
                    layout: {
                        topStart: {

                        }
                    },
                    pagelength: 10,
                    bLengthChange: false,
                    language: {

                        paginate: {
                            first: 'First',
                            previous: 'Previous',
                            next: 'Next',
                            last: 'Last'
                        }
                    },
                    columnDefs: [
                        {
                            targets: [0, 1, 2, 3, 4, 5],
                            classname: 'dt-center'
                        },
                        {
                            targets: [4, 5],
                            render: function (data, type, row, meta) {
                                if ((type === 'display' || type === 'filter') && ((meta.col === 4) || (meta.col === 5))) {
                                    return moment(data).format('YYYY-MM-DD HH:mm:ss');
                                }
                                return data;
                            }
                        }
                    ]
                })
            })
        });
    };

    $scope.loadVoteDash();

    $scope.loadPiePres = function () {
        var getSet = FinalProjService.initVoteSet();
        getSet.then(function (ReturnedSData) {
            var voteSet = ReturnedSData.data.set_num
            var getData = FinalProjService.loadPiePres(voteSet);

            getData.then(function (ReturnedData) {
                var presData = ReturnedData.data;

                $scope.preslabels = presData.map(item => item.candidate_name);
                $scope.presdata = presData.map(item => item.tally_value);
            });
        });
    }

    $scope.loadPieVP = function () {
        var getSet = FinalProjService.initVoteSet();
        getSet.then(function (ReturnedSData) {
            var voteSet = ReturnedSData.data.set_num
            var getData = FinalProjService.loadPieVP(voteSet);

            getData.then(function (ReturnedData) {
                var vpData = ReturnedData.data;

                $scope.vplabels = vpData.map(item => item.candidate_name);
                $scope.vpdata = vpData.map(item => item.tally_value);
            });
        });
    }

    $scope.loadPieSec = function () {
        var getSet = FinalProjService.initVoteSet();
        getSet.then(function (ReturnedSData) {
            var voteSet = ReturnedSData.data.set_num
            var getData = FinalProjService.loadPieSec(voteSet);

            getData.then(function (ReturnedData) {
                var secData = ReturnedData.data;

                $scope.seclabels = secData.map(item => item.candidate_name);
                $scope.secdata = secData.map(item => item.tally_value);
            });
        });
    }

    $scope.loadPieTrea = function () {
        var getSet = FinalProjService.initVoteSet();
        getSet.then(function (ReturnedSData) {
            var voteSet = ReturnedSData.data.set_num
            var getData = FinalProjService.loadPieTrea(voteSet);

            getData.then(function (ReturnedData) {
                var treaData = ReturnedData.data;

                $scope.trealabels = treaData.map(item => item.candidate_name);
                $scope.treadata = treaData.map(item => item.tally_value);
            });
        });
    }

    $scope.loadPieAud = function () {
        var getSet = FinalProjService.initVoteSet();
        getSet.then(function (ReturnedSData) {
            var voteSet = ReturnedSData.data.set_num
            var getData = FinalProjService.loadPieAud(voteSet);

            getData.then(function (ReturnedData) {
                var audData = ReturnedData.data;

                $scope.audlabels = audData.map(item => item.candidate_name);
                $scope.auddata = audData.map(item => item.tally_value);
            });
        });
    }

    $scope.loadPiePRO = function () {
        var getSet = FinalProjService.initVoteSet();
        getSet.then(function (ReturnedSData) {
            var voteSet = ReturnedSData.data.set_num
            var getData = FinalProjService.loadPiePRO(voteSet);

            getData.then(function (ReturnedData) {
                var proData = ReturnedData.data;

                $scope.prolabels = proData.map(item => item.candidate_name);
                $scope.prodata = proData.map(item => item.tally_value);
            });
        });
    }

    $scope.addAccount = function () {
        var email = $scope.uEmail;
        var pass = $scope.uPass

        var postData = FinalProjService.addAccount(email, pass);

        var uEmail = JSON.parse(sessionStorage.getItem('uEmail'));
        var lAction = "added an Account";
        var postLog = FinalProjService.submitAdminLog(uEmail, lAction);

        location.reload();
    }

    $scope.addParty = function () {
        var name = $scope.pName;
        var camp = $scope.pCamp

        var postData = FinalProjService.addParty(name, camp);

        var uEmail = JSON.parse(sessionStorage.getItem('uEmail'));
        var lAction = "added a Party";
        var postLog = FinalProjService.submitAdminLog(uEmail, lAction);
        location.reload();
    }

    $scope.addStud = function () {
        var sNum = $scope.studNum;

        var postData = FinalProjService.addStud(sNum);

        var uEmail = JSON.parse(sessionStorage.getItem('uEmail'));
        var lAction = "added a Student";
        var postLog = FinalProjService.submitAdminLog(uEmail, lAction);
        location.reload();
    }

    $scope.addCandi = function () {
        var cFName = $scope.fName;
        var cLName = $scope.lName;
        var cParty = $scope.party;
        var cPos = $scope.position;

        var postData = FinalProjService.addCandi(cFName, cLName, cParty, cPos);
        var uEmail = JSON.parse(sessionStorage.getItem('uEmail'));
        var lAction = "added a Candidate";
        var postLog = FinalProjService.submitAdminLog(uEmail, lAction);
        location.reload();
    }

    $scope.loadEditAcc = function (accid) {
        var getData = FinalProjService.loadEditAcc(accid);

        getData.then(function (ReturnedData) {
            $scope.accEditData = ReturnedData.data;
            $scope.uEditEmail = ReturnedData.data.accEmail
            $scope.uEditPass = ReturnedData.data.accPass

            setTimeout(() => {
                M.updateTextFields();
            }, 0);
        });
    }

    $scope.editAccount = function (accid) {
        var email = $scope.uEditEmail;
        var pass = $scope.uEditPass

        var postData = FinalProjService.editAccount(accid, email, pass);

        var uEmail = JSON.parse(sessionStorage.getItem('uEmail'));
        var lAction = "edited an Account";
        var postLog = FinalProjService.submitAdminLog(uEmail, lAction);
        location.reload();
    }

    $scope.loadEditCandi = function (candid) {
        var getData = FinalProjService.loadEditCandi(candid);

        getData.then(function (ReturnedData) {
            $scope.candiEditData = ReturnedData.data;
            $scope.editfName = ReturnedData.data.candFname
            $scope.editlName = ReturnedData.data.candLname
            $scope.editParty = ReturnedData.data.candParty
            $scope.editPosition = ReturnedData.data.candPos

            setTimeout(() => {
                M.updateTextFields();
            }, 0);
        });
    }

    $scope.editCandi = function (candid) {
        var fname = $scope.editfName;
        var lname = $scope.editlName;
        var party = $scope.editParty;
        var pos = $scope.editPosition;

        var postData = FinalProjService.editCandi(candid, fname, lname, party, pos);

        var uEmail = JSON.parse(sessionStorage.getItem('uEmail'));
        var lAction = "edited a Candidate";
        var postLog = FinalProjService.submitAdminLog(uEmail, lAction);
        location.reload();
    }

    $scope.loadEditParty = function (partyid) {
        var getData = FinalProjService.loadEditParty(partyid);

        getData.then(function (ReturnedData) {
            $scope.partyEditData = ReturnedData.data;
            $scope.editpName = ReturnedData.data.partyName
            $scope.editpCamp = ReturnedData.data.partyCamp

            setTimeout(() => {
                M.updateTextFields();
            }, 0);
        });
    }

    $scope.editParty = function (partyid) {
        var pName = $scope.editpName;
        var pCamp = $scope.editpCamp

        var postData = FinalProjService.editParty(partyid, pName, pCamp);
        var uEmail = JSON.parse(sessionStorage.getItem('uEmail'));
        var lAction = "edited a Party";
        var postLog = FinalProjService.submitAdminLog(uEmail, lAction);
        location.reload();
    }

    $scope.loadEditStud = function (studid) {
        var getData = FinalProjService.loadEditStud(studid);

        getData.then(function (ReturnedData) {
            $scope.studEditData = ReturnedData.data;
            $scope.editstudNum = ReturnedData.data.studNum

            setTimeout(() => {
                M.updateTextFields();
            }, 0);
        });
    }

    $scope.editStud = function (studid) {
        var studNum = $scope.editstudNum;

        var postData = FinalProjService.editStud(studid, studNum);

        var uEmail = JSON.parse(sessionStorage.getItem('uEmail'));
        var lAction = "edited a Student";
        var postLog = FinalProjService.submitAdminLog(uEmail, lAction);
        location.reload();
    }

    $scope.archiveAcc = function (accid) {
        var postData = FinalProjService.archiveAcc(accid);

        var uEmail = JSON.parse(sessionStorage.getItem('uEmail'));
        var lAction = "archived an Account";
        var postLog = FinalProjService.submitAdminLog(uEmail, lAction);
        location.reload();
    }
    $scope.archiveCandi = function (candid) {
        var postData = FinalProjService.archiveCandi(candid);

        var uEmail = JSON.parse(sessionStorage.getItem('uEmail'));
        var lAction = "archived a Candidate";
        var postLog = FinalProjService.submitAdminLog(uEmail, lAction);
        location.reload();
    }
    $scope.archiveParty = function (partyid) {
        var postData = FinalProjService.archiveParty(partyid);

        var uEmail = JSON.parse(sessionStorage.getItem('uEmail'));
        var lAction = "archived a Party";
        var postLog = FinalProjService.submitAdminLog(uEmail, lAction);
        location.reload();
    }
    $scope.archiveStud = function (studid) {
        var postData = FinalProjService.archiveStud(studid);

        var uEmail = JSON.parse(sessionStorage.getItem('uEmail'));
        var lAction = "archived a Student";
        var postLog = FinalProjService.submitAdminLog(uEmail, lAction);
        location.reload();
    }

    $scope.logout = function () {
        sessionStorage.removeItem('uEmail');
    }

});
