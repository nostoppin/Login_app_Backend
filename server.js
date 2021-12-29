//acq pkg
const express = require("express");
const res = require("express/lib/response");
const mongoose = require('mongoose');
const keys = require('./Config/keys');

//connect2DB
mongoose.connect(keys.mongoURI, { useNewUrlParser: true, useUnifiedTopology: true});

//setupDB Model
require('./model/Account')
//useDB
const account = mongoose.model('accounts');

const app = express();
const port = 12233;
// var myNum = 2;
// myNum = "two";

app.get('/account', async (request, response) =>
{
    const {r_username, r_password} = request.query

    if(r_username == null || r_password == null)
    {
        response.send("INVALID CREDENTIALS");
        return;
    }

    var userAccount = await account.findOne({username: r_username})
    
    if(r_username == null)
    {
        console.log("create account here");

        var newAccount = new account
        ({
            username : r_username,
            password : r_password,

            lastAuthDate : Date.now()
        });

        await newAccount.save();

        response.send(newAccount);
        return;
    }
    else
    {
        if(r_password == userAccount.password)
        {
            userAccount.lastAuthDate = Date.now();
            await userAccount.save(); 

            response.send(userAccount);
            return;
        }
    }

    response.send("invalid cred");
    return;
});

//console.log("Hello Himadri");

app.listen(keys.port, () =>
{
    console.log("Listening on server:" + keys.port);
});