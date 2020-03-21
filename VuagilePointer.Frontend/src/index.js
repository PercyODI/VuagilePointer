//@ts-check
/**
 * @typedef User
 * @property {String} firstName
 * @property {String} lastName
 * @property {String} authName
 */

import $ from "jquery";
import "./index.css";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

const appConsts = {
    // @ts-ignore
    hostUrl: __hostUrl__
}

$(
    function () {
        $.ajax(`${appConsts.hostUrl}/api/userinfo`)
            .done(data => {
                renderUserLogin(data);
                console.dir(data);
            });



        const connection = new HubConnectionBuilder()
            .withUrl(`${appConsts.hostUrl}/vuagileHub`)
            .configureLogging(LogLevel.Information)
            .build();

        connection.start().then(function () {
            console.log("connected");
        })
    }
);

/**
 * @param {User[]} users
 */
function renderUserLogin(users) {
    $("#loadingDiv").hide();
    let workingDiv = $("#workingDiv");
    workingDiv.children().remove();

    // @ts-ignore
    var template = $(require("./templates/userLogin.hbs")({ users: users }));
    template.find("#loginBtn").click(() => { logInWithUser(template.find("#loginSelect option:checked").attr("val")) });

    workingDiv.append(template);
}

/**
 * @param {string} selectedAuthName
 */
function logInWithUser(selectedAuthName) {
    console.log(selectedAuthName);
    $("#loadingDiv").show();
    $("#workingDiv").hide();
}