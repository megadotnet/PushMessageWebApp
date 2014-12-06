function Feed() {
    var chat = undefined;

    var init = function () {
    
        // Reference the auto-generated proxy for the hub.
        chat = $.connection.feedHub;
        // Create a function that the hub can call back to display messages.
        chat.client.receive = function (item) {
            var selector = "ul.feed-list li[data-id=" + item.Id + "]";
            if (!($(selector).length > 0)) {
                $("ul.feed-list").prepend($(".feed-template").render(item));
                $("ul.feed-list li:gt(3)").remove();
            }

            $.messager.show({
                title: 'Tips',
                msg: item.MSG_CONTENT,
                showType: 'show'          
            });

     
        };

        // Start the connection.
        $.connection.hub.start().done(function () {
            chat.server.init();
        });

    };

    init();
};