var mediaModule = {

    name: 'media',

    displayTemplate: function () {
        if (!this.cachedDisplayTemplate) {
            this.cachedDisplayTemplate = $('#storeTemplate').html();

            Mustache.parse(this.cachedDisplayTemplate);
        }

        return this.cachedDisplayTemplate;
    },

    addOrUpdateTemplate: function () {
        if (!this.cachedUpdateTemplate) {
            this.cachedUpdateTemplate = $('#addOrUpdateTemplate').html();

            Mustache.parse(this.cachedUpdateTemplate);
        }

        return this.cachedUpdateTemplate;
    },

    uris: {
        getUri: 'http://localhost:5001/api/v1.0/media',
        createUri: 'http://localhost:5001/api/v1.0/media/',
        deleteSelectedUri: 'http://localhost:5001/api/v1.0/media/entities',
        updateUri: 'http://localhost:5001/api/v1.0/media'
    },

    render: function (data) {
        var storeBody = $('#storeBody');
        var rendered = '';

        for (var i = 0; i < data.length; i++) {
            storeBody.append(this.renderTemplate(this.displayTemplate(), data[i]));
        }
    },

    addOrUpdateInit: function (id) {

        var data = ObjectUtils.serizeObject(id, { name: '', description: '', id: emptyGuid() });

        $('#createOrUpdateContainer').html(this.renderTemplate(this.addOrUpdateTemplate(), data));

        $('#createOrUpdateContainer').removeClass('hidden');
    },

    createHandler: function (data) {
        var self = this;
        $('#storeBody').append(self.renderTemplate(self.displayTemplate(), data));
    },

    updateHandler: function (data) {
        var self = this;
        var newHtml = self.renderTemplate(self.displayTemplate(), data);

        $('tr#' + data.id).replaceWith(newHtml);
    },

    deleteHandler: function (data) {
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                $('#storeBody tr#' + data[i]).remove();
            }
        }
    },

    renderTemplate: function (template, data) {
        return Mustache.render(template, data);
    }
}
