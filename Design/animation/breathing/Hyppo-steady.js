(function(window) {
Hyppo1 = function() {
	this.initialize();
}
Hyppo1._SpriteSheet = new SpriteSheet({images: ["Hyppo-steady.png"], frames: [[0,0,130,191,0,62.95,132.85],[130,0,130,191,0,62.95,132.85],[260,0,130,190,0,62.95,131.85],[0,191,130,190,0,62.95,131.85],[130,191,130,191,0,62.95,132.85],[0,0,130,191,0,62.95,132.85]]});
var Hyppo1_p = Hyppo1.prototype = new BitmapAnimation();
Hyppo1_p.BitmapAnimation_initialize = Hyppo1_p.initialize;
Hyppo1_p.initialize = function() {
	this.BitmapAnimation_initialize(Hyppo1._SpriteSheet);
	this.paused = false;
}
window.Hyppo1 = Hyppo1;
}(window));

