
function App() {
    var init = function () {
        Feed();
        $.connection.hub.logging = true;
        $.connection.hub.start()
            .done(function() {
                console.log("Connected!");
                $(document).trigger("Connected");
            })
            .fail(function() { console.log("Could not Connect!"); });
    };

    init();
};