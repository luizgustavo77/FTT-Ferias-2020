var base_path = 'https://localhost:44368/';

$(document).ready(function () {
    $("#btnPikachu").hide();
    $("#pikachu").hide();
});

var local = '';

var jsBehindPage = function () {
    var controler = function () {
        return {
            Secreto: "#secreto"
        }
    }

    var nothigHere = function (Id) {
        $.ajax({
            type: "POST",
            url: base_path + "Home/Search",
            data: {
                'Id': Id
            },
            cache: false
        }).done(function (data) {
            if (data) {
                $(controler().Secreto).append('<div class="pokemonSelect col-md-4" ><h2 class="text-center">' + data.name + '</h2>'
                    + '<img src=' + data.sprites + ' style="width:100%;" /></div >');
            }
            else {
                alert('Nenhum pokemon encontrado...');
            }
        }).fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Nenhum pokemon encontrado...');
        });
    }

    return {
        nothigHere: nothigHere
    }
}