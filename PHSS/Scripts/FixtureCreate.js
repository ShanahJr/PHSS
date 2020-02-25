//The purpose of script is to change the available teams, by the fixture creation section,
//according to the Age Group and Division Selected

$(document).ready(function () {

    //On division selection change
    $("#DivisionId").change(function () {

        var DivisionId = $("#DivisionId").val();

        $.ajax(
            {
                type: 'GET',
                url: '/FixtureModels/DivisionSelect/',
                dataType: 'json',
                data: { DivisionId: DivisionId },
                success: function (data) {

                    $("#Team1Id").empty();
                    $("#Team2Id").empty();


                    $.each(data, function (i, team) {

                        var Options = '<option value = "' + team.TeamId + '">' + team.TeamName + "</option>";
                        $("#Team1Id").append(Options);
                        $("#Team2Id").append(Options);

                    });//end of for each team



                }// success

                //,
                //error: function (ex) {
                //    var r = jQuery.parseJSON(response.responseText);
                //    alert("Message: " + r.Message);
                //    alert("StackTrace: " + r.StackTrace);
                //    alert("ExceptionType: " + r.ExceptionType);
                //}

            }); //end of ajax call

        return false;

    }) // end of Division Selector on change function

    //On Age Group Selection Change
    $("#UnderId").change(function () {

        var UnderId = $("#UnderId").val();

        $.ajax(
            {
                type: 'GET',
                url: '/FixtureModels/UnderSelect/',
                dataType: 'json',
                data: { UnderId: UnderId },
                success: function (data) {

                    $("#Team1Id").empty();
                    $("#Team2Id").empty();


                    $.each(data, function (i, team) {

                        var Options = '<option value = "' + team.TeamId + '">' + team.TeamName + "</option>";
                        $("#Team1Id").append(Options);
                        $("#Team2Id").append(Options);

                    });//end of for each team



                }// success

                //,
                //error: function (ex) {
                //    var r = jQuery.parseJSON(response.responseText);
                //    alert("Message: " + r.Message);
                //    alert("StackTrace: " + r.StackTrace);
                //    alert("ExceptionType: " + r.ExceptionType);
                //}

            }); //end of ajax call

        return false;

    }) // end of Division Selector on change function

})// End of document.ready