var categoryModule = {

    name: 'category',

    displayTemplate: function () {
        if (!this.cachedDisplayTemplate) {
            this.cachedDisplayTemplate = $('#categoryTemplate').html();

            Mustache.parse(this.cachedDisplayTemplate);
        }

        return this.cachedDisplayTemplate;
    },

    storeSelectionTemplate: function () {
        if (!this.cachedStoreSelectionTemplate) {
            this.cachedStoreSelectionTemplate = $('#storeSelectionTemplate').html();

            Mustache.parse(this.cachedStoreSelectionTemplate);
        }

        return this.cachedStoreSelectionTemplate;
    },

    addOrUpdateTemplate: function () {
        if (!this.cachedUpdateTemplate) {
            this.cachedUpdateTemplate = $('#addOrUpdateTemplate').html();

            Mustache.parse(this.cachedUpdateTemplate);
        }

        return this.cachedUpdateTemplate;
    },

    uris: {
        getUri: 'http://localhost:5001/api/v1.0/categories',
        createUri: 'http://localhost:5001/api/v1.0/categories',
        deleteSelectedUri: 'http://localhost:5001/api/v1.0/categories/entities',
        updateUri: 'http://localhost:5001/api/v1.0/categories'
    },

    initialize: function () {
        var self = this;
        var tokenStorage = new TokenStorage();

        $.subscribe('stores/change', function (e, payload) {
            e.preventDefault();

            $.ajax({
                url: 'http://localhost:5001/api/v1.0/categories/stores/' + payload,
                dataType: 'json',
                beforeSend: beforeSendHook,
                success: function (data) {
                    renderCategory(data);                     
                }
            });
        });

        function renderCategory(data) {
            if (data && data.length > 0) {
                var view = '';

                for (var i = 0; i < data.length; i++) {
                    view += self.renderTemplate(self.storeSelectionTemplate(), data[i]);
                }

                $('#categories').html(view);
            }
        }


        function beforeSendHook(xhr, settings) {
            var storedToken = tokenStorage.getToken();

            if (storedToken) {
                xhr.setRequestHeader('Authorization', storedToken.token_type + ' ' + storedToken.access_token);
            }
        };
    },

    render: function (data) {
        var storeBody = $('#categoryBody');
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
        $('#categoryBody').append(self.renderTemplate(self.displayTemplate(), data));
    },

    updateHandler: function (data) {
        var self = this;
        var newHtml = self.renderTemplate(self.displayTemplate(), data);

        $('tr#' + data.id).replaceWith(newHtml);
    },

    deleteHandler: function (data) {
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                $('#categoryBody tr#' + data[i]).remove();
            }
        }
    },

    renderstores: function (data) {
        var self = this;
        var view = '';

        for (var i = 0; i < data.length; i++) {
            view += self.renderTemplate(self.storeSelectionTemplate(), data[i]);
        }

        $('#stores').html(view);
    },

    renderTemplate: function (template, data) {
        return Mustache.render(template, data);
    }
}
