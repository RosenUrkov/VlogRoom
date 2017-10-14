$(() => {
    $.connection.hub.start().done(() => {
        const notifications = $.connection.notifications;

        notifications.client.receveNotification = (notification) => {
            console.log(notification);
            $("ul.cl-effect-2")
                .append($("<li>")
                    .append($("<a>")
                        .attr("href", "#")
                        .text(notification)
                    )
                );
        };

        $(".container").on("click", "#subscribe-button", (ev) => {
            const userId = $(".heading").attr("id");
            notifications.server.addNotification(userId);
        });
    });
});