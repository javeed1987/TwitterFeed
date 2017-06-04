$(document).ready(function () {
    LoadTwitterFeed();
});

function LoadTwitterFeed() {   
    $('#twitter-feeds').html('');

    $.ajax({
        url: "/TwitterFeed/GetTwitterFeedDetails",
        data: {},
        type: "POST",
        contentType: "application/json",
        dataType: "json",      
        success: function (result) {           
                displayTweets(result);           
        },
        error: function (xhr, status) {
            alert(status + " - " + xhr.responseText);
        }
    });

    // cancel the Pageload function
    return false;
}

function displayTweets(json) {
 
    // Loop through every tweet content
    for (var i = 0; i < json.length; i++) {
        // Check if search text has a value, if so then compare with tweet content
        if (json[i].text.indexOf($("#inputSerach").val()) != -1 || $("#inputSerach").val() == '') {
            $("#twitter-feeds")
             .append('<div>'
             // Tweet header - Includes ProfileImage, USer Name, Screen Name and Tweet time
            + '<div><a href="' + json[i].user.profile_image_url + '" ><img src="' + json[i].user.profile_image_url + '" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'
                    + '<span>'
                    + '<strong>' + json[i].user.name + '</strong>'
                    + '‏</span>'
                    + '&nbsp;<span>@' + json[i].user.screen_name + '</span>'
                    + '<small>' + json[i].created_at.substring(0, 16) + ''
                    + '</small>'
            + '</div>'
            // Tweet Content
            + '<div>'
                    + '<span>' + json[i].text + '</span>'
            + '</div>'
            );

            if (json[i].entities.hasOwnProperty('media')) {
                for (var j = 0; j < json[i].entities.media.length; j++) {
                    $("#twitter-feeds").append('<div>'
                        //+ '<div data-image-url="' + json[i].entities.media[j].media_url + '" data-element-context="platform_photo_card">'
                        + '<img src="' + json[i].entities.media[j].media_url + '" alt="" style="width: 30%; height: 30%; top: -0px; left: 7.5%">'
                        + '</div>'
                        );
                }
            }                 
            // Retweet Count
            $("#twitter-feeds").append('<div>'
                + json[i].retweet_count + ' <span style="font-size: 10px;font-style: oblique"> ReTweets</span></span>'
                + '</div></div>');
       }
    }

}