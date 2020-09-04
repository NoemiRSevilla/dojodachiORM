$(document).ready(function(){
    console.log("Document is ready!");
    $("#feed").submit(function(event){
        event.preventDefault();
        $.ajax({
            method: 'POST',
            url: "/feed",
        })
        .done(function(response){
            $("body").html(response);
        });

    });

    $("#play").submit(function(event){
        event.preventDefault();
        $.ajax({
            method: 'POST',
            url: "/play",
        })
        .done(function(response){
            $("body").html(response);
        });

    });

    $("#work").submit(function(event){
        event.preventDefault();
        $.ajax({
            method: 'POST',
            url: "/work",
        })
        .done(function(response){
            $("body").html(response);
        });

    });

    $("#sleep").submit(function(event){
        event.preventDefault();
        $.ajax({
            method: 'POST',
            url: "/sleep",
        })
        .done(function(response){
            $("body").html(response);
        });

    });
});