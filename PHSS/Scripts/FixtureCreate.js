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

                    $("#TeamModel1Id").empty();
                    $("#TeamModel2Id").empty();


                    $.each(data, function (i, team) {

                        var Options = '<option value = "' + team.TeamModelId + '">' + team.TeamName + "</option>";
                        $("#TeamModel1Id").append(Options);
                        $("#TeamModel2Id").append(Options);

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

                    $("#TeamModel1Id").empty();
                    $("#TeamModel2Id").empty();


                    $.each(data, function (i, team) {

                        var Options = '<option value = "' + team.TeamModelId + '">' + team.TeamName + "</option>";
                        $("#TeamModel1Id").append(Options);
                        $("#TeamModel2Id").append(Options);

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