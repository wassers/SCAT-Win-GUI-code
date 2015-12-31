// Define the overlay, derived from google.maps.OverlayView
function mouseOverDiv(opt_options) {
 // Initialization
 this.setValues(opt_options);

 // mouseOverDiv specific
 var span = this.span_ = document.createElement('span');
 span.style.cssText = 'position: relative; left: +20%; bottom: +100%; ' +
                      'white-space: nowrap; border: none; ' +
                      'background-color: #FFFFE0; font-family:arial,sans-serif;' +
                      'font-weight:bold; font-size:small; color:blue';
 //var link1 = document.createElement('a');                      
 //link1.text = "Hide";
 span.appendChild(document.createTextNode("Hide"));
 var div = this.div_ = document.createElement('div');
 div.appendChild(span);
 div.style.cssText = 'position: absolute; display: none';
};
mouseOverDiv.prototype = new google.maps.OverlayView;

mouseOverDiv.prototype.addElement = function(elmText) {
	//this.span_
}

// Implement onAdd
mouseOverDiv.prototype.onAdd = function() {
 var pane = this.getPanes().overlayLayer;
 pane.appendChild(this.div_);

 // Ensures the mouseOverDiv is redrawn if the text or position is changed.
 var me = this;
 this.listeners_ = [
   google.maps.event.addListener(this, 'position_changed',
       function() { me.draw(); }),
   google.maps.event.addListener(this, 'text_changed',
       function() { me.draw(); })
 ];
};

// Implement onRemove
mouseOverDiv.prototype.onRemove = function() {
 this.div_.parentNode.removeChild(this.div_);

 // mouseOverDiv is removed from the map, stop updating its position/text.
 for (var i = 0, I = this.listeners_.length; i < I; ++i) {
   google.maps.event.removeListener(this.listeners_[i]);
 }
};

// Implement draw
mouseOverDiv.prototype.draw = function() {
 var projection = this.getProjection();
 var position = projection.fromLatLngToDivPixel(this.get('position'));

 var div = this.div_;
 div.style.left = position.x + 'px';
 div.style.top = position.y + 'px';
 div.style.display = 'block';

 this.span_.innerHTML = this.get('text').toString();
};