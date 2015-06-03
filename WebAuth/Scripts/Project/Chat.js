function Chat() {
    var chatHub = undefined;
    
    var init = function () {
        $(".chat-submit").on("click", sendMessage);
        $(document).on("Connected", function () { chatHub.init(); });

        chatHub = $.connection.chatHub;

        chatHub.receiveChat = function (value) {
            console.log('Server called addMessage(' + value + ')');
            $("ul.chat-list").prepend($(".chat-template").render(value));
            $("ul.chat-list li:gt(2)").remove();
        };
    };

    var sendMessage = function() {
        chatHub.send($(".chat-message").val());
    };

    init();
};