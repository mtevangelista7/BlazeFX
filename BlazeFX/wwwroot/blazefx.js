window.blazeFX = {
    applyAnimation: function (element, classAttribute, styleAttribute) {
        if (element) {
            element.className = classAttribute;
            element.style.cssText = styleAttribute;
        }
    }
};