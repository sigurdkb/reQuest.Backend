function setupWebViewJavascriptBridge(callback) {


    if (window.WebViewJavascriptBridge) {
        return callback(WebViewJavascriptBridge);
    }

    if (window.WVJBCallbacks) {
        return window.WVJBCallbacks.push(callback);
    }

    window.WVJBCallbacks = [callback];

    var WVJBIframe = document.createElement('iframe');
    WVJBIframe.style.display = 'none';
    WVJBIframe.src = 'https://__bridge_loaded__';
    document.documentElement.appendChild(WVJBIframe);
    setTimeout(function() {
        document.documentElement.removeChild(WVJBIframe)
    }, 0)
}

function onBridgeReady(bridge) {

    // alert("Bridge is ready");

    bridge.registerHandler('onHandShake', function(data, responseCallback) {
        alert("Oh no a dirty ios app");
        responseCallback(data)
    })

//   let btn = document.getElementById('pushButton');
//   btn.onclick = function(e) {
    // e.preventDefault()

    bridge.callHandler('getPushToken', null, function responseCallback(responseData) {
        var pushToken = responseData.token;
        // alert(responseData.token);
        $.ajax({
            url: './registerpushtoken',
            contentType: "application/json",
            type: 'POST',
            data: JSON.stringify(pushToken),      
            dataType: 'json',
            success: function (data) {
                console(data);
                }
        });
    })
//   }
}

setupWebViewJavascriptBridge(onBridgeReady);

// var pushToken = 'samplepushtoken';

// $.ajax({
//     url: './registerpushtoken',
//     contentType: "application/json",
//     type: 'POST',
//     data: JSON.stringify(pushToken),      
//     dataType: 'json',
//     success: function (data) {
//         console(data);
//         }
// });
