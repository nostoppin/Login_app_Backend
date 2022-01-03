//acq pkg
const express = require("express");
const response = require("express/lib/response");
const mongoose = require('mongoose');
const keys = require('./Config/keys');

const bodyParser = require('body-parser');


//connect2DB
mongoose.connect(keys.mongoURI, { useNewUrlParser: true, useUnifiedTopology: true});
const app = express();
const port = 12233;

app.use(bodyParser.urlencoded({extended: false}));

//setupDB Model
require('./model/Account')

//setupRoutes
require('./routes/authRoutes')(app);

//console.log("Hello Himadri");

app.listen(keys.port, () =>
{
    console.log("Listening on server:" + keys.port);
});