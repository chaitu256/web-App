$(document).ready(function () {
var x = 0;
var s = "";

console.log("Hello plural sight");

var theForm = $("#theForm");
theForm.hidden = true;
var button = $("#buybutton");
button.on("click", function () {
    console.log("Buying Item");
});

var productInfo = $(".product-props li");
//var listItems = productInfo.item[0].children;
productInfo.on("click", function () {

    console.log("you clicked on" + $(this).text());

    });

    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");

    $loginToggle.on("click", function() {

        $popupForm.toggle();
    });
        
});
