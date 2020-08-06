var base_path = 'https://localhost:44314/';

var poke = function () {
    var controler = function () {
        return {
            Main: "#main"
        }
    }

    var details = function () {
        $.ajax({
            type: "POST",
            url: base_path + "Poke/Details",
            cache: false
        }).done(function (data) {
            if (data) {
                adicionarHtmlPopup('popup_poke', 'form_poke', 'Detalhes');
                $("#form_poke").html(data);

                montarModalComEfeitoEClickBotaoClose('#form_poke', null, 1200).on("dialogclose", function (event, ui) {
                    $('#form_poke').remove();
                });
                $(controler().Main).appendTo(data);
            }
            else {
                alert('Nenhum pokemon encontrado...');
            }
        }).fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Nenhum pokemon encontrado...');
        });
    }

    var adicionarHtmlPopup = function (idPopup, idForm, titulo) {
        var popup = '#' + idPopup;
        var form = '#' + idForm;

        var html = "<div id=" + "'" + idPopup + "'" + ">" +
            "<div id=" + "'" + idForm + "'" + " title=" + "'" + titulo + "'" + " style='display:none;'></div>" +
            "</div>";

        $(popup).remove();

        $(controler().Main).append(html);

        bindEventos(popup, form);

        return $(popup);
    }

    var montarModalComEfeitoEClickBotaoClose = function (IdDivComHashtag) {
        return $(IdDivComHashtag).dialog({

            autoOpen: true, show: "puff", hide: "puff",
            open: function (event, ui) {
                $(this).parent().children().children('.ui-dialog-titlebar-close').click(function () {

                });
            },
        });
    }

    var bindEventos = function (idPopup, idForm) {
        $(idForm).dialog({
            autoOpen: false, closeText: "",
            width: 960,
            modal: true,

            open: function () {
                $(".ui-widget-overlay").appendTo(idPopup);
                $(this).dialog("widget").appendTo(idPopup);
            },
            close: function () {
                $(".ui-widget-overlay").remove();
                $(idForm).dialog("close");
            }
        });
    }

    return {
        details: details
    }
}