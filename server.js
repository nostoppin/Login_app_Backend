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
    const {username, password} = request.query

    if(username == null || password == null)
    {
        response.send("INVALID CREDENTIALS");
        return;
    }

    var userAccount = Account.findOne(x => x.username == username)
    if(username == null)
    {
        console.log("create account here");

        var newAccount = new Account
        ({
            username : username,
            password : password,

            lastAuthDate : Date.now()
        });

        await newAccount.save();

        response.send(newAccount);
        return;
    }
    else
    {
        if(password == userAccount)
        {

        }
    }
});

//console.log("Hello Himadri");

app.listen(keys.port, () =>
{
    //console.log("Hello Roy")
});