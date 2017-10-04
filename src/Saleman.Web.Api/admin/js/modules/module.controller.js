'use strict';

var ModuleController = function (moduleLocator) {
    this.tokenStorage = new TokenStorage();
    this.moduleLocator = moduleLocator;
}

ModuleController.prototype.activeModule = function (key) {
    this.activeModule = this.moduleLocator.resolve(key);

    if (this.activeModule && typeof (this.activeModule.initialize) == 'function') {
        this.activeModule.initialize();
    }
};

ModuleController.prototype.initialize = function () {
    var self = this;

    registerEvent();

    (function firstLoad(controller) {
        if (controller.activeModule) {
            $.ajax({
                url: controller.activeModule.uris.getUri,
                dataType: 'json',
                beforeSend: function (xhr, settings) {
                    beforeSendHook(controller, xhr, settings);
                },
                success: function (data) {

                    if (typeof controller.activeModule.render == 'function') {
                        controller.activeModule.render(data);
                    }

                    registerEvent();
                }
            });
        }
    })(self);

    function beforeSendHook(controller, xhr, settings) {
        var storedToken = controller.tokenStorage.getToken();

        if (storedToken) {
            xhr.setRequestHeader('Authorization', storedToken.token_type + ' ' + storedToken.access_token);
        }
    };

    function registerEvent() {
        registerFormEvents();
        registerOtherEvents();
        registerActions();
    }

    function registerOtherEvents() {
        var checkbox = $('#title-checkbox');
        checkbox.off('change', handleSelectItems);
        checkbox.on('change', handleSelectItems);

        var newButton = $('#newItem');
        newButton.off('click', handleNewAction);
        newButton.on('click', handleNewAction);
    }

    function registerFormEvents() {
        var createForm = $('#createForm');
        createForm.off('submit', handleCreate);
        createForm.on('submit', handleCreate);
    }

    function registerActions() {
        var selectedDeleteButton = $('#deleteSelected');
        selectedDeleteButton.off('click', handleSelectedDelete);
        selectedDeleteButton.on('click', handleSelectedDelete);

        var editButtons = $('span.data__action-edit');
        editButtons.off('click', handleEdit);
        editButtons.on('click', handleEdit);

        var deleteButtons = $('span.data__action-delete');
        deleteButtons.off('click', handleDelete);
        deleteButtons.on('click', handleDelete);
    }

    function handleNewAction() {
        self.activeModule.addOrUpdateInit();

        bindingSelections();

        registerFormEvents();
    }

    function handleSelectItems() {
        var self = this;
        var state = $(self).prop('checked');
        $('.data-checkbox').each(function (index, element) {
            $(element).prop('checked', state);
        });
    }

    function handleEdit(e) {
        e.preventDefault();

        var id = $(this).attr('data-id');

        self.activeModule.addOrUpdateInit(id);

        registerFormEvents();

        registerActions();
    }

    function registerStoreChanged() {
        $('#stores').on('change', function (e) {
            $.publish('stores/change', [$(this).val()]);
        });
    }

    function bindingSelections() {
        $.ajax({
            url: 'http://localhost:5001/api/v1.0/stores/owner',
            dataType: 'json',
            beforeSend: function (xhr, settings) {
                beforeSendHook(self, xhr, settings);
            },
            success: function (data) {

                if (typeof self.activeModule.renderstores == 'function') {
                    self.activeModule.renderstores(data);

                    registerStoreChanged();
                }                
            }
        });
    }
    
    function handleDelete(e) {
        e.preventDefault();

        var id = $(this).attr('data-id');

        deleteItems([id]);
    };

    function handleSelectedDelete(e) {
        e.preventDefault();

        var request = [];
        $('.data-checkbox').each(function (index, element) {
            if ($(element).prop('checked')) {
                request.push($(element).attr('data-id'));
            }
        });

        deleteItems(request);
    }

    function handleCreate(e) {
        var createForm = $('#createForm');
        e.preventDefault();
        var postRequest = metaFactory(createForm);
        var uri = isEmptyGuid(postRequest.id)
            ? self.activeModule.uris.createUri
            : self.activeModule.uris.updateUri;
        var method = isEmptyGuid(postRequest.id) ? 'POST' : 'PUT';

        $.ajax({
            url: uri,
            type: method,
            data: postRequest.data,
            contentType: postRequest.contentType,
            processData: postRequest.processData,

            beforeSend: function (xhr, settings) {
                beforeSendHook(self, xhr, settings);
            },
            success: function (data) {

                if (isEmptyGuid(postRequest.id)) {
                    self.activeModule.createHandler(data);
                } else {
                    self.activeModule.updateHandler(data);
                }

                registerEvent();
            }
        })

        function metaFactory(form) {
            var data, contentType, processData;
            var filesItems = $('input:file', form);
            if (filesItems.length > 0) {
                data = new FormData($(form)[0]);
                contentType = false;
                processData = false;
            } else {
                data = JSON.stringify(objectifyForm(form.serializeArray()));
                contentType = 'application/json';
                processData = true;
            }

            return { data: data, contentType: contentType, processData: processData, id: $('input#id', form).val() };
        }
    }

    function deleteItems(ids) {

        if (ids.length > 0) {
            $.ajax({
                url: self.activeModule.uris.deleteSelectedUri,
                type: 'DELETE',
                data: JSON.stringify(ids),
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr, settings) {
                    beforeSendHook(self, xhr, settings);
                },
                success: function (data) {

                    if (typeof self.activeModule.deleteHandler == 'function') {
                        if (data.status) {
                            self.activeModule.deleteHandler(ids);
                        }
                    }
                }
            })
        }
    }
};
