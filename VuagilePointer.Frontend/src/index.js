//@ts-check

import $ from "jquery";
import "./index.css";
import {HubConnectionBuilder, LogLevel} from "@microsoft/signalr";

$(
    function(){
        var users = $.ajax("https://localhost:44397/api/userinfo")
            .done(data => {
                console.dir(data);
            })

        const connection = new HubConnectionBuilder()
            .withUrl("https://localhost:44397/vuagileHub")
            .configureLogging(LogLevel.Information)
            .build();

        connection.start().then(function() {
            console.log("connected");
        })
    }
);