(function(window) {
Hyppo1 = function() {
	this.initialize();
}
Hyppo1._SpriteSheet = new SpriteSheet({images: ["Hyppo-inspire.png"], frames: [[0,0,130,191,0,62.95,132.35],[130,0,130,192,0,62.95,133.35],[260,0,130,193,0,62.95,134.35],[0,193,130,193,0,62.95,134.35],[130,193,130,194,0,62.95,135.35],[260,193,130,195,0,62.95,136.35]]});
var Hyppo1_p = Hyppo1.prototype = new BitmapAnimation();
Hyppo1_p.BitmapAnimation_initialize = Hyppo1_p.initialize;
Hyppo1_p.initialize = function() {
	this.BitmapAnimation_initialize(Hyppo1._SpriteSheet);
	this.paused = false;
}
window.Hyppo1 = Hyppo1;
}(window));

