
function clickAddRow(stringClassNameButton) {
    $("." + stringClassNameButton).click(function (e) {
        e.preventDefault();
        var stringTableId = $(this).closest('table').attr('id');
        var focusString = addRowString(stringTableId);

    });
};


function clickAddRowResult(stringClassNameButton) {
    $("." + stringClassNameButton).click(function (e) {
        e.preventDefault();
        var stringTableId = $(this).closest('table').attr('id');
        //console.log(stringTableId);
        addRowResultString(stringTableId);
        
    });
};

function clickAddRowResultMulitSelect(stringClassNameButton, stringClassNameSelect) {
    $("." + stringClassNameButton).click(function (e) {
        e.preventDefault();
        var stringTableId = $(this).closest('table').attr('id');
        addRowResultMultiSelectString(stringTableId, stringClassNameSelect);

    });
};

function clickCheckAll(stringClassNameButton, stringTableCheckName) {
    $("." + stringClassNameButton).click(function (index, value) {
        var stringTableId = $(this).closest('table').attr('id');
        var value = this.checked;
        CheckedAllCheckBox(stringTableCheckName, stringTableId, value);
    });
};

function CheckedAllCheckBox(stringCheckClass, tableName, valueCheck) {
    var table = document.getElementById(tableName);
    var ntbodies = table.getElementsByTagName("tbody");
    $(ntbodies).find("." + stringCheckClass).each(function (index, value) {
        value.checked = valueCheck;
    });
}
function clickAddMultiRow(stringClassNameButton, stringClassTable) {
    $("." + stringClassNameButton).click(function (e) {
        addRowMultiTable(stringClassTable);
    });
}

function addRowMultiTable(stringClassTable) {
    $("." + stringClassTable).each(function (index, value) {
        var id = value.id;
        addRowString(id);
    })
}
function clickAddResultMultiRow(stringClassNameButton, stringClassTable) {
    $("." + stringClassNameButton).click(function (e) {
        addRowResultMultiTable(stringClassTable);
    });
}

function addRowResultMultiTable(stringClassTable) {
    $("." + stringClassTable).each(function (index, value) {
        var id = value.id;
        addRowResultString(id);
    })
}

function addRowString(stringTableName) {
    if (stringTableName !== undefined && stringTableName.length > 0) {
        var table = document.getElementById(stringTableName);
        return addRow(table);
    }
};

function addRowResultString(stringTableName) {
    if (stringTableName !== undefined && stringTableName.length > 0) {
        var table = document.getElementById(stringTableName);
        //console.log(table);
        addRowResult(table);
    }
};

function addRowResultMultiSelectString(stringTableName, stringClassNameSelect) {
    if (stringTableName !== undefined && stringTableName.length > 0) {
        var table = document.getElementById(stringTableName);
        addRowResultMultiSelect(table, stringClassNameSelect);
    }
};

function addRow(table) {
    //console.log("addRow");
    var tblId = table.id;
    var trname = table.id + "-tr[0]";
    var ntbodies = table.getElementsByTagName("tbody");
    var tr = document.getElementById(trname);
    var ntrs = table.getElementsByTagName("tr");
    var rowindex = ntrs.length - 1;
    var rowname = table.id + "-tr[" + rowindex + "]";
    var clonetr = $(tr).clone(true).attr("id", rowname).find("input, select").val("").end();
    $(clonetr).removeAttr("style");
    $(clonetr).find("input, select").each(function (index, value) {
        var idname = value.id;
        var name = value.name;
        if (idname !== undefined && idname.length > 0) {
            var strname = replaceLastIndex(name, "[0]", "[" + rowindex + "]")
            var strId = replaceLastIndex(idname, "_0_", "_" + rowindex + "_")
            $(this).attr('id', strId);
            $(this).attr('name', strname);
        };
    });
    $(clonetr).find("a").each(function (index, value) {
        var id = value.id;
        //console.log("addRowId: " + id)
        if (id.includes("deleteButton")) {
            $(this).removeAttr("href");            
        }
        if (id.includes("usersButton")) {
            $(this).removeAttr("href");
        }
        if (id.includes("membersButton")) {
            //console.log("find membersButton");
            $(this).remove();
            //$(this).removeAttr("href");
        }
    });
    $(ntbodies).append(clonetr);
    Indexierung(tblId);
    var trname1 = table.id + "-tr[" + rowindex + "]";
    document.getElementById(trname1).scrollIntoView()
};

function addRowResult(table) {
    //console.log("addRowResult");
    var tblId = table.id;
    var trname = table.id + "-tr[0]";
    var ntbodies = table.getElementsByTagName("tbody");
    var tr = document.getElementById(trname);
    var ntrs = table.getElementsByTagName("tr");
    var rowindex = ntrs.length - 1;
    var rowname = table.id + "-tr[" + rowindex + "]";
    var clonetr = $(tr).clone(true).attr("id", rowname).find("input, select, textarea").end();
    $(clonetr).removeAttr("style");
    $(clonetr).find("input, select, textarea").each(function (index, value) {
        var idname = value.id;
        var name = value.name;
        if (idname !== undefined && idname.length > 0) {
            var strname = replaceLastIndex(name, "[0]", "[" + rowindex + "]")
            var strId = replaceLastIndex(idname, "0__", "" + rowindex + "__")
            $(this).attr('id', strId);
            $(this).attr('name', strname);
        };
        if ($(this).attr('type') == 'checkbox') {
            var value = $(this).attr('value');
            if ((value === "true")) { $(this).checked }
        }
    });
    $(clonetr).find("a").each(function (index, value) {
        var id = value.id;
        //console.log("id" + id);
        if (id.includes("deleteButton")) {
            $(this).removeAttr("href");
        }
        if (id.includes("usersButton")) {
            $(this).removeAttr("href");
        }
        if (id.includes("membersButton")) {
            $(this).remove();
        }
    });
    $(ntbodies).append(clonetr);
    Indexierung(tblId);
    var trname1 = table.id + "-tr[" + rowindex + "]";
    document.getElementById(trname1).scrollIntoView()
    return rowindex;
};

function addRowResultMultiSelect(table, stringClassNameSelect) {
    destoryChosen(table.id, stringClassNameSelect)
    var tblId = table.id;
    var trname = table.id + "-tr[0]";
    var ntbodies = table.getElementsByTagName("tbody");
    var tr = document.getElementById(trname);
    var ntrs = table.getElementsByTagName("tr");
    var rowindex = ntrs.length - 1;
    var rowname = table.id + "-tr[" + rowindex + "]";
    var clonetr = $(tr).clone(true).attr("id", rowname).find("input, select, textarea").end();
    $(clonetr).removeAttr("style");
    $(clonetr).find("input, select, textarea").each(function (index, value) {
        var idname = value.id;
        var name = value.name;
        if (idname !== undefined && idname.length > 0) {
            var strname = replaceLastIndex(name, "[0]", "[" + rowindex + "]")
            var strId = replaceLastIndex(idname, "0__", "" + rowindex + "__")
            $(this).attr('id', strId);
            $(this).attr('name', strname);
        };
        if ($(this).attr('type') == 'checkbox') {
            var value = $(this).attr('value');
            if ((value === "true")) { $(this).checked }
        }
    });
    $(clonetr).find("select." + stringClassNameSelect).each(function (index, value) {
        var id = value.id;
        $(this).chosen({
            'no_results_text': 'keine Ergebnisse',
            'width': '100%',
            'search_contains': true,
            'placeholder_text_multiple': ' ',
            'display_selected_options': false
        }
        );
    });
    
    
    $(ntbodies).append(clonetr);    
    Indexierung(tblId);
    enableChosen(table.id, stringClassNameSelect);
    var trname1 = table.id + "-tr[" + rowindex + "]";
    document.getElementById(trname1).scrollIntoView()

};

function enableSelect2Focus(stringClassName) {
    $('[class^="' + stringClassName + '"')
        .focus(function () {
            $(this).select2({
                tags: true,
            });
        })
}

function enableChosen(tableName, stringClassNameChosen) {

    var table = document.getElementById(tableName);
    var ntbodies = table.getElementsByTagName("tbody");
    $(ntbodies).find("select." + stringClassNameChosen).each(function (index, value) {
        var id = value.id;
        //console.log(id);
        $(this).chosen({
            'no_results_text': 'keine Ergebnisse',
            'width': '100%',
            'search_contains': true,
            'placeholder_text_multiple': ' ',
            'display_selected_options': false
        }
        );
    });
}

function destoryChosen(tableName, stringClassNameChosen) {

    var table = document.getElementById(tableName);
    var ntbodies = table.getElementsByTagName("tbody");
    $(ntbodies).find("select." + stringClassNameChosen).each(function (index, value) {
        var id = value.id;
        //console.log(id);
        $(this).chosen("destroy");
    });
}


function clickRemoveRow(stringClassName) {
    $("." + stringClassName).click(function () {
        var row_count = $(this).closest('table').find("tr").length - 2;
        var stringTableId = $(this).closest('table').attr('id');
        if (row_count == 0) {
            addRowString(stringTableId);
        }
        $(this).closest('tr').remove();
        Indexierung(stringTableId);
        return false;
    });
};

function clickRemoveMultiRow(stringClassNameButton, stringClassTable) {
    $("." + stringClassNameButton).click(function (e) {
        var intIndex = $(e.currentTarget).closest('tr').index();
        removeRowMulti(stringClassTable, intIndex);
    });
}

function removeRowMulti(stringClassTable, intIndex) {
    $("." + stringClassTable).each(function (index, value) {
        
        var id = value.id;
        //console.log(id);
       removeRowString(id, intIndex);
    })
}

function removeRowString(stringTableName, intRowIndex) {
    if (intRowIndex == 1) { intRowIndex = 1; }
    intRowIndex = intRowIndex + 1;
    //console.log(stringTableName);
    if (stringTableName !== undefined && stringTableName.length > 0) {
        $("#" + stringTableName).find("tr:eq(" + (intRowIndex).toString() + ")").remove();
        Indexierung(stringTableName);
    }
}

function Indexierung(tableName) {
    var table = document.getElementById(tableName);
    var ntbodies = table.getElementsByTagName("tbody");
    var indexRow = 0;
    var firstId=[];
    var firstName = [];
    $(ntbodies).find("tr").each(function (index, value) {
        indexRow = index;
        var id = value.id;
        var search = id.substring(id.indexOf("["), id.indexOf("]") + 1);
        $(this).attr('id', id.replace(search, "[" + indexRow + "]"));

        $(this).find("input, select, textarea").each(function (index, value) {
            if (indexRow == 0) {
                firstId[index] = value.id;
                firstName[index] = value.name;
            }
            if (firstId[index] !== undefined && firstId[index].length > 0) {
                var strName = replaceLastIndex(firstName[index], "[0]", "[" + indexRow + "]");
                var strId = replaceLastIndex(firstId[index], "0__", indexRow + "__")
                if (strId !== undefined && strId.length > 0) {
                    $(this).attr('id', strId);
                }
                if (strName !== undefined && strName.length > 0) {
                    $(this).attr('name', strName);
                }
                if (id.includes("deleteButton-click") && indexRow > 0) {
                    $(this).removeAttr("href");
                }
                if (value.id.includes("__Sort")) {
                    $(this).attr('value', indexRow);
                    //console.log("sortId: " + value.id);
                }
            }
        });
    });
};

function replaceLastIndex(stringValue, stringSuch, stringReplace) {
    const lastIndex = stringValue.lastIndexOf(stringSuch);
    const replacement = stringReplace;
    let replaced;
    if (lastIndex !== -1) {
        replaced =
            stringValue.substring(0, lastIndex) +
            replacement +
            stringValue.substring(lastIndex + stringSuch.length);
    }
    return replaced;
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

function FocusLastId(stringTableId, stringId) {
    if (stringId != null && stringId.length > 0) {
        //var trId = stringTableId + "-tr[" + stringId + "]";
        //console.log("trId: " + trId);
        document.getElementById(trId).scrollIntoView();
    }
}
function ScrollToLastId(stringTableId, stringId) {
    if (stringId != null && stringId.length > 0) {
        var trId = stringTableId + "-tr[" + stringId + "]";
        //console.log("trId: " + trId);
        var scrollDiv = document.getElementById(trId).offsetTop;
        //console.log(scrollDiv);
        window.scrollTo({ top: scrollDiv, behavior: 'smooth' });
    }
}
