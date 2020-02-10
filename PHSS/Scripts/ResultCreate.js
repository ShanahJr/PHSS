$(document).ready(function () {
    $("#DivisionId").change(function () {
        var OptionId = $('#DivisionId').val();


        $.ajax(
            {
                type: 'GET',
                url: '/Website/Division/',
                dataType: 'json',
                data: { Id: OptionId },
                success: function (data) {

                    LoadData(data);

                }// success
                //,
                //error: function (ex) {
                //    var r = jQuery.parseJSON(response.responseText);
                //    alert("Message: " + r.Message);
                //    alert("StackTrace: " + r.StackTrace);
                //    alert("ExceptionType: " + r.ExceptionType);
                //}
            });
        return false;

    }); // end of Division on change function


    $("#AgeGroupId").change(function () {

        var UnderId = $('#AgeGroupId').val();

        $.ajax(
            {

                type: 'GET',
                url: '/Website/AgeGroup/',
                dataType: 'json',
                data: { id: UnderId },
                success: function (data) {

                    LoadData(data);

                },// success
            });

    }); // end of Division on change function

})//End of document.ready function


// Function to load data into the tables

function LoadData(data) {



    if (data.Logs.length == 0) {
        $("#LogStandings").empty();
        $("#LogStandings").append('<h2 style="font-weight : 900; color : white; margin-bottom : 20px;">Log Standings</h2>');
        $("#LogStandings").append('<p style = "color : white;">Sorry, there are no Logs to view at the moment. Please visit us again to keep in to be kept in the loop.</p>');
    }
    else {

        $("#LogStandings").empty();
        $("#LogStandings").append('<h2 style="font-weight : 900; color : white; margin-bottom : 20px;">Logs</h2>');
        $("#LogStandings").append('<table id = "TheLog"></table>');

        var LogHeadings = "<thead>" +
            "<tr>/" +
            "<th> Position</th>" +
            "<th>Team Name</th>" +
            "<th>Played</th>" +
            "<th>Win</th>" +
            "<th>Draw</th>" +
            "<th>Lost</th>" +
            "<th>For</th>" +
            "<th>Against</th>" +
            "<th>Goal Difference</th>" +
            "<th>Points</th>" +
            "</tr >" +
            "</thead>";

        $("#TheLog").append(LogHeadings);
        var position = 0;

        //For each log, enter the data into a row and output it to the log table
        $.each(data.Logs, function (i, log) {
            position++;
            var TeamName = log.Team.TeamName;
            var Logs =
                "<tr>" +
                "<td>" + position + "</td>" +
                "<td>" + log.Team.TeamName + "</td>" +
                "<td>" + log.Played + "</td>" +
                "<td>" + log.Win + "</td>" +
                "<td>" + log.Draw + "</td>" +
                "<td>" + log.Lost + "</td>" +
                "<td>" + log.GoalsFor + "</td>" +
                "<td>" + log.GoalsAgainst + "</td>" +
                "<td>" + log.GoalDifference + "</td>" +
                "<td>" + log.Points + "</td>" +
                "</tr>";

            $('#TheLog').append(Logs);
        });// For each

    }//End of log else



    //If fixture object is empty then display a message on the view, else, load the data
    if (data.Fixtures.length == 0) {
        $("#Fixtures").empty();
        $("#Fixtures").append('<h2 style="font-weight : 900; color : white; margin-bottom : 20px;">Fixtures</h2>');
        $("#Fixtures").append('<p style = "color : white;">Sorry, there are no fixtures to view at the moment. Please visit us again to find out when your favortite team is playing</p>');
    }
    else {


        $("#Fixtures").empty();
        $("#Fixtures").append('<h2 style="font-weight : 900; color : white; margin-bottom : 20px;">Fixtures</h2>');
        $("#Fixtures").append('<table id = "TheFixture"></table>');

        var FixtureHeadings = "<tr>" +
            "<th> Date & Time</th>" +
            "<th>Home Team</th>" +
            "<th>Away Team</th>" +
            "<th>Venue</th>" +
            "</tr >";

        $("#TheFixture").append(FixtureHeadings);

        $.each(data.Fixtures, function (i, fixture) {

            var CurrentFixture =
                "<tr>" +
                "<td>" + fixture.Matchdate + "</td>" +
                "<td>" + fixture.TeamModel.TeamName + "</td>" +
                "<td>" + fixture.TeamModel1.TeamName + "</td>" +
                "<td>" + fixture.Location.MatchLocation + "</td>" +
                "</tr>";

            $('#TheFixture').append(CurrentFixture);

        });// for each fixture in fixture list

    }//end of fixture else



    //If result object is empty then display a message on the view, else, load the data
    if (data.Results.length == 0) {
        $("#Results").empty();
        $("#Results").append('<h2 style="font-weight : 900; color : white; margin-bottom : 20px;">Results</h2>');
        $("#Results").append('<p style = "color : white;">Sorry, there are no Results to view at the moment. Please visit our website again to find out how your team of choice is performing.</p>');
    }
    else {
        // Clear the Result table when the division or age group option is changed and replace the headings
        $("#Results").empty();
        $("#Results").append('<table id = "TheResult"></table>');

        var ResultHeadings =
            "<th>Date & Time</th>" +
            "<th>Home Team</th>" +
            "<th>Score</th>" +
            "<th>Away Team</th>";

        $("#TheResult").append(ResultHeadings)

        $.each(data.Results, function (i, result) {
            position++;

            var CurrentResult = "<tr>" +
                "<td>" + result.Fixtures.Matchdate + "</td>" +
                "<td>" + result.Fixtures.TeamModel.TeamName + "</td>" +
                "<td>" + result.Team1Score + " - " + result.Team2Score + "</td>" +
                "<td>" + result.Fixtures.TeamModel1.TeamName + "</td>" +
                "</tr>";

            $('#TheResult').append(CurrentResult);
        });// For each result in result list

    } //end of result else

}