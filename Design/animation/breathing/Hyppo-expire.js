(function(window) {
Hyppo1 = function() {
	this.initialize();
}
Hyppo1._SpriteSheet = new SpriteSheet({images: ["Hyppo-expire.png"], frames: [[0,0,130,195,0,62.95,136.35],[130,0,130,191,0,62.95,132.35],[260,0,130,188,0,62.95,129.35],[0,195,130,185,0,62.95,126.35],[130,195,130,181,0,62.95,122.35],[260,195,130,178,0,62.95,119.35]]});
var Hyppo1_p = Hyppo1.prototype = new BitmapAnimation();
Hyppo1_p.BitmapAnimation_initialize = Hyppo1_p.initialize;
Hyppo1_p.initialize = function() {
	this.BitmapAnimation_initialize(Hyppo1._SpriteSheet);
	this.paused = false;
}
window.Hyppo1 = Hyppo1;
}(window));

