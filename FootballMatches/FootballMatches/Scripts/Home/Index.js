function selectView(view) {
    $('.display').not('#' + view + "Display").hide();
    $('#' + view + "Display").show();
}
function getData() {
    $.ajax({
        type: "GET",
        url: "/api/MatchesApi",
        success: function (data) {
            $('#tableBody').empty();
            for (var i = 0; i < data.length; i++) {
                $('#tableBody').append('<tr><td><input id="id" name="id" type="radio"'
                + 'value="' + data[i].MatchId + '" /></td>'
                + '<td>' + data[i].City + '</td>'
                + '<td>' + data[i].Stadium + '</td></tr>');
            }
            $('input:radio')[0].checked = "checked";
            selectView("summary");
        }
    });
}
$(document).ready(function () {
    selectView("summary");
    getData();
    $("button").click(function (e) {
        var selectedRadio = $('input:radio:checked');
        switch (e.target.id) {
            case "refresh":
                getData();
                break;
            case "delete":
                $.ajax({
                    type: "DELETE",
                    url: "/api/MatchesApi/" + selectedRadio.attr('value'),
                    success: function (data) {
                        selectedRadio.closest('tr').remove();
                    }
                });
                break;
            case "add":
                selectView("add");
                break;
            case "edit":
                {
                    $.ajax({
                        type: "GET",
                        url: "/api/MatchesApi/" + selectedRadio.attr('value'),
                        success: function (data) {
                            $('#editMatchId').val(data.MatchId);
                            $('#editCity').val(data.City);
                            $('#editStadium').val(data.Stadium);
                            selectView("edit");
                        }
                    });
                    break;
                }
            case "teams":

                location = "/Teams/Index/" + selectedRadio.attr('value');
                    
                break;
            case "submitEdit":
                $.ajax({
                    type: "PUT",
                    url: "/api/MatchesApi/" + selectedRadio.attr('value'),
                    data: $('#editForm').serialize(),
                    success: function (result) {
                            var cells = selectedRadio.closest('tr').children();
                            cells[1].innerText = $('#editCity').val();
                            cells[2].innerText = $('#editStadium').val();
                            selectView("summary");
                    }
                });
                break;
        }
    });
});