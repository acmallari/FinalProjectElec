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
                    alert("Invalid");
                }
                else if (returnValue == 1) {
                    validateData.then(function (ReturnedVData) {
                        var returnVValue = Number(ReturnedVData.data);
                        if (returnVValue == 0) {
                            alert("Only one vote per set");
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
                alert("Invalid");
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

});
