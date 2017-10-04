var ModuleLocator = function () {
    this.modules = [];
}

ModuleLocator.prototype.register = function (module) {
    var tracker = -1;
    for (var i = 0; i < this.modules.length; i++) {
        if (module.name == this.modules[i].name) {
            tracker = i;
            break;
        }
    }

    if (tracker >= 0)
        this.modules.splice(1, tracker);

    this.modules.push(module);
}

ModuleLocator.prototype.resolve = function (name) {
    for (var i = 0; i < this.modules.length; i++) {
        if (name == this.modules[i].name)
            return this.modules[i];
    }

    return null;
}

