
function enableAutoCompleteArtikelSelectBoxFocus(stringClassName, moduleId, stringSelectBoxClass = null, boolCheck = false) {
    $('[class^="' + stringClassName + '"')
        .focus(function () {
            enableAutoCompleteArtikelSelectBox($(this), moduleId, stringSelectBoxClass);
        })
        .focusout(function () {
            $(this).autocomplete("destroy");
            if (boolCheck) { checkInputId($(this)); }
        });
}

function enableAutoCompleteArtikelSelectBox($element, moduleId, stringSelectBoxClass) {
    var id = $element.get(0).id + "Id";
    var gewicht = $element.get(0).id + "Gewicht";
    if (stringSelectBoxClass != null) {
        var ex = $element.closest('table').find("." + stringSelectBoxClass).first().val()
        //console.log(ex);
        if (ex != null) {
            moduleId = moduleId + "." + ex; /*console.log("ModuleID: " + moduleId)*/;
        }
    }
    $element.autocomplete({
        minLength: 2,
        source: function (request, response) {
            $.ajax({
                url: '/api/autocomplete/ArtikelAutoComplete',
                data: {
                    "search": request.term,
                    "moduleId": moduleId,
                },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (event, ui) {
            $("#" + id).val(ui.item.val);
            $("#" + gewicht).val(ui.item.einzelgewicht);
            $(this).val(ui.item.label);
        }
    });
}

function enableAutoCompleteArtikelFocus(stringClassName, moduleId, stringDivClass = null, boolCheck) {
    $('[class^="' + stringClassName + '"')
        .focus(function () {
            enableAutoCompleteArtikelInput($(this), moduleId, stringDivClass)
        })
        .focusout(function () {
            $(this).autocomplete("destroy");
            if (boolCheck) { checkInputId($(this)); }
        });
}

function enableAutoCompleteArtikelInput($element, moduleId, stringDivClass) {
    //console.log($element);
    var id = $element.get(0).id + "Id";
    var gewicht = $element.get(0).id + "Gewicht";

    if (stringDivClass != null) {
        var ex = $element.closest('td').find("div." + stringDivClass).attr('name')
        if (ex != null) {
            moduleId = moduleId + "." + ex;
            //console.log("ModuleID: " + moduleId);
        }
    }
    //console.log(id);
    $element.autocomplete({
        minLength: 2,
        source: function (request, response) {
            $.ajax({
                url: '/api/autocomplete/ArtikelAutoComplete',
                data: {
                    "search": request.term,
                    "moduleId": moduleId,
                },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (event, ui) {dotnet new install Microsoft.NET.Runtime.WebAssembly.Templates
            $("#" + id).val(ui.item.val);
            //$("#" + gewicht).val(ui.item.einzelgewicht);
            $(this).val(ui.item.label);
        },
        open: function () {
            $("#" + id).val(0);
        },
    });
}
function enableAutoCompleteArtikeInputFocus(stringClassName, moduleId, stringDivClass = null, boolCheck=false) {
    $('[class^="' + stringClassName + '"')
        .focus(function () {
            enableAutoCompleteArtikelInputField($(this), moduleId, stringDivClass)
        })
        .focusout(function () {
            $(this).autocomplete("destroy");
            if (boolCheck) { checkInputId($(this)); }
            
        });
}

function enableAutoCompleteArtikelInputField($element, moduleId, stringDivClass) {
    //console.log($element);
    var id = $element.get(0).id + "Id";
    var gewicht = $element.get(0).id + "Gewicht";
    console.log(id);

    if (stringDivClass != null) {
        //console.log("stringDivClass: " + stringDivClass);
        var ex = $element.closest('tr').find("input." + stringDivClass).attr('value')
        if (ex != null) {
            moduleId = moduleId + "." + ex;
            //console.log("ModuleID: " + moduleId);
        }
    }
    
    $element.autocomplete({
        minLength: 2,
        source: function (request, response) {
            $.ajax({
                url: '/api/autocomplete/ArtikelAutoComplete',
                data: {
                    "search": request.term,
                    "moduleId": moduleId,
                },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (event, ui) {
            $("#" + id).val(ui.item.val);
            $("#" + gewicht).val(ui.item.einzelgewicht);
            $(this).val(ui.item.label);
        },
        open: function () {
            $("#" + id).val(0);
        },
    });
}

function enableAutoCompleteLieferantFocus(stringClassName, moduleId, stringDivClass = null, boolCheck = false) {
    $('[class^="' + stringClassName + '"')
        .focus(function () {
            enableAutoCompleteLieferantInput($(this), moduleId, stringDivClass)
        })
        .focusout(function () {
            $(this).autocomplete("destroy");
            if (boolCheck) { checkInputId($(this)); }
            

        });
}

function enableAutoCompleteLieferantInput($element, moduleId, stringDivClass) {
    //console.log($element);
    var id = $element.get(0).id + "Id";
    //console.log(stringDivClass)
    if (stringDivClass != null) {
        var ex = $element.closest('tr').find("input." + stringDivClass).attr('value')
        if (ex != null) {
            moduleId = moduleId + "." + ex;
            //console.log("ModuleID: " + moduleId);
        }
    }

    //console.log(id);
    $element.autocomplete({
        minLength: 2,
        source: function (request, response) {
            $.ajax({
                url: '/api/autocomplete/LieferantAutoComplete',
                data: {
                    "search": request.term,
                    "moduleId": moduleId,
                },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (event, ui) {
            $("#" + id).val(ui.item.val);
            $(this).val(ui.item.label);
        },
        open: function () {
            $("#" + id).val(0);
        },

    });
}

function enableAutoCompleteModuleFocus(stringClassName, moduleId, stringDivClass = null) {
    $('[class^="' + stringClassName + '"')
        .focus(function () {
            //console.log(moduleId);
            enableAutoCompleteModuleInput($(this), moduleId, stringDivClass)
        })
        .focusout(function () {
            $(this).autocomplete("destroy");
        });
}

function enableAutoCompleteModuleInput($element, moduleId, stringDivClass) {

    if (stringDivClass != null) {
        var ex = $element.closest('td').find("div." + stringDivClass).attr('name')
        if (ex != null) {
            moduleId = moduleId + "." + ex;
            //console.log("ModuleID: " + moduleId);
        }
    }
    var id = $element.get(0).id + "Id";
    //console.log("ModuleId: " + moduleId);

    //console.log(moduleId);
    $element.autocomplete({
        minLength: 2,
        source: function (request, response) {
            $.ajax({
                url: '/api/autocomplete/ModuleAutoComplete',
                data: {
                    "search": request.term,
                    "moduleId": moduleId,
                },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (event, ui) {
            $("#" + id).val(ui.item.val);
            $(this).val(ui.item.label);
        },
        open: function () {
            $("#" + id).val(0);
        },
    });
}
function enableArtikelAutoCompleteModuleFocus(stringClassName, moduleId, varianteId=0, stringDivClass = null) {
    $('[class^="' + stringClassName + '"')
        .focus(function () {
            //console.log(moduleId);
            enableArtikeAutolCompleteModuleInput($(this), moduleId, varianteId, stringDivClass)
        })
        .focusout(function () {
            $(this).autocomplete("destroy");
        });
}

function enableArtikeAutolCompleteModuleInput($element, moduleId, varianteId, stringDivClass) {

    if (stringDivClass != null) {
        var ex = $element.closest('td').find("div." + stringDivClass).attr('name')
        if (ex != null) {
            moduleId = moduleId + "." + ex;
            //console.log("ModuleID: " + moduleId);
        }
    }

    //console.log(moduleId);
    $element.autocomplete({
        minLength: 2,
        source: function (request, response) {
            $.ajax({
                url: '/api/autocomplete/ModuleAutoComplete',
                data: {
                    "search": request.term,
                    "moduleId": moduleId,
                    "varianteId": varianteId
                },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (event, ui) {
            $(this).val(ui.item.label);
        }
    });
}

function checkInputId($element) {
    var id = $element.get(0).id + "Id";
    if ($("#" + id).length == 0) {
        //console.log("not found: " + id);
        return;
    }
    var artikelId = $("#" + id).val();
    //console.log("ArtkelId: " + artikelId);
    if (artikelId == 0) {
        $("#" + id).closest("td").addClass('td-error');
        //alert("Fehler kein Artikel ausgewählt");
        return;
    }
    if ($("#" + id).closest("td").hasClass('td-error')) {
        $("#" + id).closest("td").removeClass('td-error')
    }
}

