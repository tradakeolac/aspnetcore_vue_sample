function objectifyForm(formArray) {//serialize data function

  var returnArray = {};
  for (var i = 0; i < formArray.length; i++){
    returnArray[formArray[i]['name']] = formArray[i]['value'];
  }
  return returnArray;
}

var defaultGuid = '00000000-0000-0000-0000-000000000000';

function emptyGuid() {
    return defaultGuid;
}

function isEmptyGuid(guid) {
    if (!guid) return true;
    return guid == defaultGuid;
}

var ObjectUtils = {};

ObjectUtils.serizeObject = function(rowId, defaultValue) {
    if (rowId) {
        var row = $('tr#' + rowId);
        var rowData = $('td.data__property', row);

        var jsonData = '{';
        rowData.each(function (i, e) {
            jsonData += '"' + $(e).attr('data-property') + '": "' + $(e).attr('data-value') + '"';

            if (i < rowData.length - 1)
                jsonData += ',';
        });

        jsonData += '}';
        jsonData = jsonData.replace(/\\/g, "\\\\");

        data = JSON.parse(jsonData);
    } else {
        data = defaultValue;
    }

    return data;
}