var base_path = 'https://localhost:44314/';

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

    var nothigHere = function () {
        $.ajax({
            type: "POST",
            url: base_path + "Home/Search",
            data: {
                'Id': local
            },
            cache: false
        }).done(function (data) {
            if (data) {
                $(controler().Secreto).append('<div class="pokemonSelect col-md-4" onclick="jsBehindPage().gotcha(' + data.id + ')"><h2 class="text-center PokemonTitulo">' + data.name + '</h2>'
                    + '<img src=' + data.sprites.front_default + ' style="width:100%;" /></div >');
            }
            else {
                alert('Nenhum pokemon encontrado...');
            }
        }).fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Nenhum pokemon encontrado...');
        });
    }

    var gotcha = function (Id) {
        window.location = base_path + 'Poke/Index?id=' + Id;
    }

    return {
        nothigHere: nothigHere,
        gotcha: gotcha
    }
}