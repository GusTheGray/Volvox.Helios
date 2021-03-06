﻿(function($) {

    $.fn.populateGuildChannels = function(guildId) {
        return this.each(function() {
            const selectElement = $(this);

            selectElement.empty();

            selectElement.append('<option selected="true" disabled hidden>Select a channel</option>');
            selectElement.prop("selectedIndex", 0);
            selectElement.prop("disabled", false);

            const url = `/api/GetGuildChannels?guildId=${guildId}`;

            $.getJSON(url, function (data) {
                $.each(data, function (key, entry) {
                    selectElement.append($("<option></option>").attr("value", entry.id).text(entry.name));
                });
            });
        });
    };

    $.fn.populateSettings = function() {
        return this.each(function() {
            const element = $(this);
            
            const url = "/api/GetUserAdminGuilds";

            $.getJSON(url, function (data) {
                element.empty();
                
                $.each(data, function (key, entry) {
                    element.append($('<a class="dropdown-item"></a>').attr("href", `/Settings/${entry.id}`).text(entry.name));
                });
            });
        });
    };

}(jQuery));