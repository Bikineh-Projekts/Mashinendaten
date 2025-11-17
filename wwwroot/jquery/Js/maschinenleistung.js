function updateAllRow(stringClassNameButton, stringClassName, stringTableName) {
    $("." + stringClassNameButton).click(function (e) {
        e.preventDefault();
        updateRowName(stringClassName, stringTableName);
    });
}

function clickAddUpdateRow(stringClassNameButton, stringTableName) {

    $("." + stringClassNameButton).click(function (e) {
        e.preventDefault();
        updateRow0("unternehmen", stringTableName);
        updateRow0("schicht", stringTableName);
        updateRow0("maschine", stringTableName);
        updateRow0("datumVon", stringTableName);
        updateRow0("datumBis", stringTableName);
    });

    clickAddRowResult(stringClassNameButton);
}

function updateRowName(stringClassNameRow, stringTableName) {
    var tableName = '#' + stringTableName;
    var stringTableName = $(tableName).attr('id');
    //console.log("TableId: " + stringTableName);
    if (stringTableName !== undefined && stringTableName.length > 0) {
        var table = document.getElementById(stringTableName);
        var stringmask = '#' + stringClassNameRow + "Id";
        var maskValue = $(stringmask).val();
        var maskText = $(stringmask + ' option:selected').text();
        if (maskText.length < 1) { maskText = maskValue; }
        $(table).find("input." + stringClassNameRow).each(function (index, value) {
            value.value = maskValue;
        });
        $(table).find("label." + stringClassNameRow).each(function (index, value) {
            $(this).empty();
            $(this).append(maskText);
        });
    }
}

function updateRow0(stringClassNameRow, stringTableName) {
    var tableName = '#' + stringTableName;
    var stringTableName = $(tableName).attr('id');
    //console.log("TableId: " + stringTableName);
    if (stringTableName !== undefined && stringTableName.length > 0) {
        var stringmask = '#' + stringClassNameRow + "Id";
        var maskValue = $(stringmask).val();
        //console.log("maskValue: "+maskValue);
        var maskText = $(stringmask + ' option:selected').text();
        if (maskText.length < 1) { maskText = maskValue; }
        var table = document.getElementById(stringTableName);
        var row = $(table).find("tbody tr").first();
        $(row).find("input." + stringClassNameRow).each(function (index, value) {
            value.value = maskValue;

        });
        $(row).find("label." + stringClassNameRow).each(function (index, value) {
            $(this).empty();
            $(this).append(maskText);
        });
    }
}

function calcGewichtPackung(stringClassName) {
    $("." + stringClassName).change(function (e) {
        var value = (e.currentTarget).value;
        var id = (e.currentTarget).id;
        if (id.includes("Packung")) {
            var artikelGewicht = document.getElementById(id.replace("Packung", "ArtikelGewicht")).value;
            var element = document.getElementById(id.replace("Packung", "Gewicht"));
            if (element.value == 0) {
                var result = value * artikelGewicht;
                element.value = result;
            }
        } else if (id.includes("Gewicht")) {
            var artikelGewicht = document.getElementById(id.replace("Gewicht", "ArtikelGewicht")).value;
            var element = document.getElementById(id.replace("Gewicht", "Packung"));
            if (artikelGewicht > 0) {
                var result = value / artikelGewicht;
                if (element.value == 0) {
                    element.value = (Math.floor(result)).toFixed(4);
                }
            }
        }
    });
}
function calcGewichtPackungApi(stringClassName) {
    $("." + stringClassName).change(function (e) {
        var value = (e.currentTarget).value;
        var id = (e.currentTarget).id;
        if (id.includes("Packung")) {
            //console.log("Gewicht: " + value);
            calcPackung(value, id);
        } else if (id.includes("Gewicht")) {
            //console.log("Packung: " + value);
            calcGewicht(value, id);
        }
    });
}


function calcPackung(value, stringId) {
    var elemet = document.getElementById(stringId.replace("Packung", "Gewicht"));
    var artikel = document.getElementById(stringId.replace("Packung", "ArtikelId"));
    //console.log("artikelId: " + artikel.value);
    if (elemet.value == 0) {
        $.ajax({
            type: "POST",
            url: '/api/tools/CalcGewicht',
            data: {
                "artikelId": artikel.value,
                "packung": value
            },
            dataType: "json",
            success: function (result) {
                elemet.value = result.data;
            },
            error: function (data) {
            }
        });
    }

}
function calcGewicht(value, stringId) {
    var elemet = document.getElementById(stringId.replace("Gewicht", "Packung"));
    var artikel = document.getElementById(stringId.replace("Gewicht", "ArtikelId"));
    //console.log("artikelId: " + artikel.value);
    if (elemet.value == 0) {
        $.ajax({
            type: "POST",
            url: '/api/tools/CalcPackung',
            data: {
                "artikelId": artikel.value,
                "gewicht": value
            },
            dataType: "json",
            success: function (result) {
                elemet.value = result.data;
            },
            error: function (data) {
            }
        });
    }

}


function checkDauerPause(stringClassName) {

    $("." + stringClassName).focusout(function (e) {
        var id = (e.currentTarget).id;
        var intIndexOf = id.lastIndexOf("__");
        let replaceString;
        if (intIndexOf !== -1) {
            replaceString = id.substring(0, intIndexOf) + "__";
            var datetimeVon = new Date(document.getElementById(replaceString + 'DatumVon').value + 'T' + document.getElementById(replaceString + 'DatumVonTime').value + 'Z');
            var datetimeBis = new Date(document.getElementById(replaceString + 'DatumBis').value + 'T' + document.getElementById(replaceString + 'DatumBisTime').value + 'Z');
            var pause = document.getElementById(replaceString + 'Pause').value * 60000;
            var dauer = datetimeBis - datetimeVon - pause;
            if (dauer <= 0) {

                $("#" + replaceString + 'DatumVon').closest("td").addClass('td-error');
                $("#" + replaceString + 'DatumBis').closest("td").addClass('td-error');
                $("#" + replaceString + 'Pause').closest("td").addClass('td-error');

                /*document.getElementById(replaceString + 'DatumVonTime').setAttribute('style', 'color: red; max-width:100px');
                document.getElementById(replaceString + 'DatumBisTime').setAttribute('style', 'color: red; max-width:100px');
                document.getElementById(replaceString + 'Pause').setAttribute('style', 'color:red; max-width:50px');
                alert('Fehler bei der Zeiteingabe');*/
                return;
            }
            if ($("#" + replaceString + 'DatumVon').closest("td").hasClass('td-error')) {
                $("#" + replaceString + 'DatumVon').closest("td").removeClass('td-error');
                $("#" + replaceString + 'DatumBis').closest("td").removeClass('td-error');
                $("#" + replaceString + 'Pause').closest("td").removeClass('td-error');
            }

            /*var red = document.getElementById(replaceString + 'DatumVonTime').getAttribute('style');
            var redSearch = red.search("color");

            if (redSearch > -1) {
                document.getElementById(replaceString + 'DatumVonTime').setAttribute('style', 'max-width:100px');
                document.getElementById(replaceString + 'DatumBisTime').setAttribute('style', 'max-width:100px');
                document.getElementById(replaceString + 'Pause').setAttribute('style', 'max-width:50px');
            }*/
        }
    });
}
function checkDauer(stringClassName) {

    $("." + stringClassName).focusout(function (e) {
        var id = (e.currentTarget).id;
        var intIndexOf = id.lastIndexOf("__");
        let replaceString;
        if (intIndexOf !== -1) {
            
            replaceString = id.substring(0, intIndexOf) + "__";

            var datetimeVon = new Date(document.getElementById(replaceString + 'DatumVon').value + 'T' + document.getElementById(replaceString + 'DatumVonTime').value + 'Z');
            var datetimeBis = new Date(document.getElementById(replaceString + 'DatumBis').value + 'T' + document.getElementById(replaceString + 'DatumBisTime').value + 'Z');
            var dauer = datetimeBis - datetimeVon;
            if (dauer <= 0) {
                $("#" + replaceString + 'DatumVon').closest("td").addClass('td-error');
                $("#" + replaceString + 'DatumBis').closest("td").addClass('td-error');
                //document.getElementById(replaceString + 'DatumVonTime').setAttribute('style', 'color: red; max-width:100px');
                //document.getElementById(replaceString + 'DatumBisTime').setAttribute('style', 'color: red; max-width:100px');
                //alert('Fehler bei der Zeiteingabe');
                return;
            }
            if ($("#" + replaceString + 'DatumVon').closest("td").hasClass('td-error')) {
                $("#" + replaceString + 'DatumVon').closest("td").removeClass('td-error')
                $("#" + replaceString + 'DatumBis').closest("td").removeClass('td-error')
            }
            /*if (!$("#" + replaceString + 'DatumVon').closest("td").has('td-error')) {
                $("#" + replaceString + 'DatumVon').closest("td").addClass('td-error');
                $("#" + replaceString + 'DatumBis').closest("td").addClass('td-error');
                console.log("Has Class");
            }
            /*var red = document.getElementById(replaceString + 'DatumVonTime').getAttribute('style');
            var redSearch = red.search("color");

            if (redSearch > -1) {
                document.getElementById(replaceString + 'DatumVonTime').setAttribute('style', 'max-width:100px');
                document.getElementById(replaceString + 'DatumBisTime').setAttribute('style', 'max-width:100px');
            }*/
        }
    });
}

function checkArtikelFocusOut(stringClassName) {
    $("." + stringClassName).focusout(function (e) {
        var id = (e.currentTarget).id;
        var intIndexOf = id.lastIndexOf("__");
        //document.getElementById(replaceString + 'ArtikelId').value = 0;
        let replaceString;
        if (intIndexOf !== -1) {
            replaceString = id.substring(0, intIndexOf) + "__";
            var artikelId = document.getElementById(replaceString + 'ArtikelId').value;
            if (artikelId == 0) {
                $("#" + replaceString + 'ArtikelId').closest("td").addClass('td-error');
                //alert("Fehler kein Artikel ausgewählt");
                return;
            }
            if ($("#" + replaceString + 'ArtikelId').closest("td").hasClass('td-error')) {
                $("#" + replaceString + 'ArtikelId').closest("td").removeClass('td-error')
            }
        }

    });
}

function checkStoerung(stringClassName) {
    $("." + stringClassName).focusout(function (e) {
        var id = (e.currentTarget).id;
        var value = (e.currentTarget).value;
        var intIndexOf = id.lastIndexOf("__");
        let replaceString;
        if (intIndexOf !== -1) {
            replaceString = id.substring(0, intIndexOf) + "__";
        } else {
            //console.log("id false");
            return;
        }
        if (value > 1) {
            document.getElementById(replaceString + 'Stoerung_Zeit').setAttribute('type', 'text');
            document.getElementById(replaceString + 'Stoerung_Grund').removeAttribute('hidden');

        } else {
            document.getElementById(replaceString + 'Stoerung_Zeit').setAttribute('type', 'hidden');
            document.getElementById(replaceString + 'Stoerung_Grund').setAttribute('hidden', '');
        }
    });
}