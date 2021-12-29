//acq pkg
const express = require("express");
const mongoose = require('mongoose');
mongoose.connect('mongodb://localhost:27017/test');

const app = express();
const port = 12233;
// var myNum = 2;
// myNum = "two";

app.get('/auth', async (request, response) =>
{
    console.log(request.query);
    response.send("hello !!");
});

//console.log("Hello Himadri");

app.listen(port, () =>
{
    //console.log("Hello Roy")
});