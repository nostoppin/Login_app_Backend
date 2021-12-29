const express = require("express");
//acq pkg

const app = express();
const port = 12233;
// var myNum = 2;
// myNum = "two";

app.get('/auth', async (request, response) =>
{
    console.log(request.query);
    response.send("hello " + request.query.userid + "!!");
});

//console.log("Hello Himadri");

app.listen(port, () =>
{
    //console.log("Hello Roy")
});